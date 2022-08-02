using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.Irregularity;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Irregularities.DataObjects;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/irregularityVersions/{versionId:int}/documents")]
    public class IrregularityVersionDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IIrregularityVersionsRepository irregularityVersionsRepository;
        private IIrregularityVersionService irregularityVersionService;

        public IrregularityVersionDocumentsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IIrregularityVersionsRepository irregularityVersionsRepository,
            IIrregularityVersionService irregularityVersionService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.irregularityVersionsRepository = irregularityVersionsRepository;
            this.irregularityVersionService = irregularityVersionService;
        }

        [Route("")]
        public IList<IrregularityDocVO> GetVersionDocuments(int versionId)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.View, versionId);

            return this.irregularityVersionsRepository.GetDocuments(versionId);
        }

        [Route("{documentId:int}")]
        public IrregularityVersionDocDO GetVersionDocument(int versionId, int documentId)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.View, versionId);

            var irrVersion = this.irregularityVersionsRepository.Find(versionId);

            var document = irrVersion.GetDocument(documentId);

            return new IrregularityVersionDocDO(document, irrVersion.Status, irrVersion.Version);
        }

        [HttpGet]
        [Route("new")]
        public IrregularityVersionDocDO NewVersionDocument(int versionId)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            var irrVersion = this.irregularityVersionsRepository.Find(versionId);

            return new IrregularityVersionDocDO(versionId, irrVersion.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Versions.Edit.Documents.Create), IdParam = "versionId")]
        public void AddVersionDocument(int versionId, IrregularityVersionDocDO document)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            if (!this.irregularityVersionService.CanEditVersion(versionId))
            {
                throw new InvalidOperationException("Cannot add new document.");
            }

            var irrVersion = this.irregularityVersionsRepository.FindForUpdate(versionId, document.Version);

            irrVersion.AddDocument(
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Versions.Edit.Documents.Edit), IdParam = "versionId", ChildIdParam = "documentId")]
        public void UpdateVersionDocument(int versionId, int documentId, IrregularityVersionDocDO document)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            if (!this.irregularityVersionService.CanEditVersion(versionId))
            {
                throw new InvalidOperationException("Cannot edit document.");
            }

            var irrVersion = this.irregularityVersionsRepository.FindForUpdate(versionId, document.Version);

            irrVersion.UpdateDocument(
                documentId,
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Versions.Edit.Documents.Delete), IdParam = "versionId", ChildIdParam = "documentId")]
        public void DeleteVersionDocument(int versionId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            if (!this.irregularityVersionService.CanEditVersion(versionId))
            {
                throw new InvalidOperationException("Cannot delete document.");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            var irrVersion = this.irregularityVersionsRepository.FindForUpdate(versionId, vers);

            irrVersion.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
