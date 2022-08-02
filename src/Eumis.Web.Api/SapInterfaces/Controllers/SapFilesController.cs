using Eumis.ApplicationServices.Services.SapInterfaces;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.SapInterfaces.Repositories;
using Eumis.Data.SapInterfaces.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.SapInterfaces;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.SapInterfaces.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.SapInterfaces.Controllers
{
    [RoutePrefix("api/sapFiles")]
    public class SapFilesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private ISapFilesRepository sapFilesRepository;
        private ISapFileService sapFileService;
        private IUserClaimsContext currentUserClaimsContext;

        public SapFilesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            ISapFilesRepository sapFilesRepository,
            ISapFileService sapFileService,
            UserClaimsContextFactory userClaimsContextFactory)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.sapFilesRepository = sapFilesRepository;
            this.sapFileService = sapFileService;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("")]
        public IList<SapFileVO> GetSapFiles(SapFileStatus? status = null, SapFileType? type = null)
        {
            this.authorizer.AssertCanDo(SapFileListActions.Search);

            return this.sapFilesRepository.GetSapFiles(status, type);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.SapFiles.Create))]
        public object CreateSapFile(SapFileDO file)
        {
            this.authorizer.AssertCanDo(SapFileListActions.Create);

            var sapFile = this.sapFileService.CreateSapFile(file.File.Key, file.File.Name, file.Type);

            return new { SapFileId = sapFile?.SapFileId };
        }

        [Route("{sapFileId:int}")]
        public SapFileDO GetSapFileData(int sapFileId)
        {
            this.authorizer.AssertCanDo(SapFileActions.View, sapFileId);

            var sapFile = this.sapFilesRepository.Find(sapFileId);
            var user = string.Format("{0} ({1})", this.currentUserClaimsContext.Fullname, this.currentUserClaimsContext.Username);

            return new SapFileDO(sapFile, user);
        }

        [Route("{sapFileId:int}/info")]
        public SapFileInfoVO GetSapFileInfo(int sapFileId)
        {
            this.authorizer.AssertCanDo(SapFileActions.View, sapFileId);

            return this.sapFilesRepository.GetSapFileInfo(sapFileId);
        }

        [Route("{sapFileId:int}/paidAmounts")]
        public IList<SapFilePaidAmountVO> GetSapFilePaidAmounts(int sapFileId)
        {
            this.authorizer.AssertCanDo(SapFileActions.View, sapFileId);

            return this.sapFilesRepository.GetSapPaidAmounts(sapFileId);
        }

        [Route("{sapFileId:int}/distributedLimits")]
        public IList<SapFileDistributedLimitVO> GetSapFileDistributedLimits(int sapFileId)
        {
            this.authorizer.AssertCanDo(SapFileActions.View, sapFileId);

            return this.sapFilesRepository.GetSapDistributedLimits(sapFileId);
        }

        [HttpPost]
        [Route("{sapFileId:int}/import")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.SapFiles.Import), IdParam = "sapFileId")]
        public void ImportSapFile(int sapFileId, string version)
        {
            this.authorizer.AssertCanDo(SapFileActions.Import, sapFileId);

            byte[] vers = System.Convert.FromBase64String(version);
            var sapFile = this.sapFilesRepository.FindForUpdate(sapFileId, vers);

            this.sapFileService.ImportSapFile(sapFile);
        }

        [HttpDelete]
        [Route("{sapFileId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.SapFiles.Delete), IdParam = "sapFileId")]
        public void DeleteSapFile(int sapFileId, string version)
        {
            this.authorizer.AssertCanDo(SapFileActions.Edit, sapFileId);

            byte[] vers = System.Convert.FromBase64String(version);
            var sapFile = this.sapFilesRepository.FindForUpdate(sapFileId, vers);

            this.sapFileService.DeleteSapFile(sapFile);
        }
    }
}
