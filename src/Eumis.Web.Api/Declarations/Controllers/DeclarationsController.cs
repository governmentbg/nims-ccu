using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Declarations.Repositories;
using Eumis.Data.Declarations.ViewObjects;
using Eumis.Domain;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Declarations.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.Declarations.Controllers
{
    [RoutePrefix("api/declarations")]
    public class DeclarationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IDeclarationsRepository declarationsRepository;
        private IAuthorizer authorizer;
        private UserClaimsContextFactory userClaimsContextFactory;
        private IAccessContext accessContext;
        private IUserClaimsContext currentUserClaimsContext;

        public DeclarationsController(
            IUnitOfWork unitOfWork,
            IDeclarationsRepository declarationsRepository,
            IAuthorizer authorizer,
            UserClaimsContextFactory userClaimsContextFactory,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.declarationsRepository = declarationsRepository;
            this.authorizer = authorizer;
            this.userClaimsContextFactory = userClaimsContextFactory;
            this.accessContext = accessContext;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("")]
        public IList<DeclarationVO> GetDeclarations(
            DeclarationStatus? status = null,
            DateTime? activationDate = null)
        {
            this.authorizer.AssertCanDo(DeclarationListActions.Search);

            return this.declarationsRepository.GetDeclarations(activationDate, status);
        }

        [Route("{declarationId:int}")]
        public DeclarationDO GetDeclaration(int declarationId)
        {
            this.authorizer.AssertCanDo(DeclarationActions.View, declarationId);

            var declaration = this.declarationsRepository.Find(declarationId);

            var userClaimsContext = this.userClaimsContextFactory(declaration.CreatedByUserId);

            return new DeclarationDO(declaration, $"{userClaimsContext.Fullname} ({userClaimsContext.Username})");
        }

        [HttpGet]
        [Route("new")]
        public DeclarationDO NewDeclaration()
        {
            this.authorizer.AssertCanDo(DeclarationListActions.Create);

            return new DeclarationDO($"{this.currentUserClaimsContext.Fullname} ({this.currentUserClaimsContext.Username})");
        }

        [HttpGet]
        [Route("newFile")]
        public DeclarationFileDO NewFile()
        {
            this.authorizer.AssertCanDo(DeclarationListActions.Create);

            return new DeclarationFileDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Declarations.Create))]
        public object CreateDeclaration(DeclarationDO declaration)
        {
            this.authorizer.AssertCanDo(DeclarationListActions.Create);

            var newDeclaration = new Declaration(
                declaration.Name,
                declaration.NameAlt,
                declaration.Content,
                declaration.ContentAlt,
                this.accessContext.UserId,
                declaration.Files.Select(f => new DeclarationFile
                {
                    BlobKey = f.File.Key,
                    Name = f.File.Name,
                    Description = f.Description,
                }).ToList());

            this.declarationsRepository.Add(newDeclaration);

            this.unitOfWork.Save();

            return new { DeclarationId = newDeclaration.DeclarationId };
        }

        [HttpPut]
        [Route("{declarationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Declarations.Edit), IdParam = "declarationId")]
        public void UpdateDeclaration(int declarationId, DeclarationDO declaration)
        {
            this.authorizer.AssertCanDo(DeclarationActions.Edit, declarationId);

            var oldDeclaration = this.declarationsRepository.FindForUpdate(declarationId, declaration.Version);

            oldDeclaration.UpdateAttributes(
                declaration.Name,
                declaration.NameAlt,
                declaration.Content,
                declaration.ContentAlt);

            var newFiles = declaration.Files
                .Where(f => f.Status == FileStatus.Added)
                .Select(f => new DeclarationFile { BlobKey = f.File.Key, Name = f.File.Name, Description = f.Description })
                .ToList();
            oldDeclaration.AddFiles(newFiles);

            var updatedFiles = declaration.Files
                .Where(f => f.Status == FileStatus.Updated)
                .Select(f => Tuple.Create<int, Guid, string, string>(f.DeclarationFileId.Value, f.File.Key, f.File.Name, f.Description))
                .ToList();
            oldDeclaration.UpdateFiles(updatedFiles);

            var removedFileIds = declaration.Files
                .Where(f => f.Status == FileStatus.Removed)
                .Select(f => f.DeclarationFileId.Value)
                .ToList();
            oldDeclaration.RemoveFiles(removedFileIds);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("{declarationId:int}/newPublication")]
        public DeclarationPublishDO NewPublication(int declarationId)
        {
            this.authorizer.AssertCanDo(DeclarationActions.Edit, declarationId);

            var declaration = this.declarationsRepository.Find(declarationId);

            return new DeclarationPublishDO(declaration);
        }

        [HttpPost]
        [Route("{declarationId:int}/publish")]
        [ActionLog(Action = typeof(ActionLogGroups.Declarations.ChangeStatusToPublished), IdParam = "declarationId")]
        public void PublishDeclaration(int declarationId, DeclarationPublishDO publishDO)
        {
            this.authorizer.AssertCanDo(DeclarationActions.Edit, declarationId);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var declaration = this.declarationsRepository.FindForUpdate(declarationId, publishDO.Version);

                declaration.Publish(publishDO.ActivationDate.Value);

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }

        [HttpPost]
        [Route("{declarationId:int}/archive")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Declarations.ChangeStatusToArchived), IdParam = "declarationId")]
        public void ArchiveDeclaration(int declarationId, string version)
        {
            this.authorizer.AssertCanDo(DeclarationActions.Edit, declarationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var declaration = this.declarationsRepository.FindForUpdate(declarationId, vers);

            declaration.Archive();

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{declarationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Declarations.Delete), IdParam = "declarationId")]
        public void DeleteDeclaration(int declarationId, string version)
        {
            this.authorizer.AssertCanDo(DeclarationActions.Edit, declarationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var declaration = this.declarationsRepository.FindForUpdate(declarationId, vers);

            this.declarationsRepository.Remove(declaration);

            this.unitOfWork.Save();
        }
    }
}
