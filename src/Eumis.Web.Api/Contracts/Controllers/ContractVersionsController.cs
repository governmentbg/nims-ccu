using Eumis.ApplicationServices.Services.ContractVersionXml;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Relations;
using Eumis.Data.Counters;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Print;
using Eumis.Web.Api.Contracts.DataObjects;
using Eumis.Web.Api.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{contractId:int}/versions")]
    public class ContractVersionsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private ICountersRepository countersRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IContractsRepository contractsRepository;
        private IContractVersionXmlService contractVersionXmlService;
        private IPrintManager printManager;
        private IUserClaimsContext currentUserClaimsContext;
        private UserClaimsContextFactory userClaimsContextFactory;

        public ContractVersionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            ICountersRepository countersRepository,
            IContractVersionsRepository contractVersionsRepository,
            IContractsRepository contractsRepository,
            IContractVersionXmlService contractVersionXmlService,
            IPrintManager printManager,
            UserClaimsContextFactory userClaimsContextFactory,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.countersRepository = countersRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.contractsRepository = contractsRepository;
            this.contractVersionXmlService = contractVersionXmlService;
            this.printManager = printManager;
            this.userClaimsContextFactory = userClaimsContextFactory;
            this.relationsRepository = relationsRepository;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("")]
        public IList<ContractVersionVO> GetContractVersions(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.contractVersionsRepository.GetContractVersions(contractId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/versions")]
        public IList<ContractVersionVO> GetProjectDossierContractVersions(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractVersionsRepository.GetContractVersions(contractId);
        }

        [Route("{versionId:int}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ContractVersionDO GetVersion(int contractId, int versionId)
        {
            this.authorizer.AssertCanDo(ContractVersionActions.View, versionId);

            var version = this.contractVersionsRepository.Find(versionId);

            var userClaimsContext = this.userClaimsContextFactory(version.CreatedByUserId);
            var username = string.Format("{0} ({1})", userClaimsContext.Fullname, userClaimsContext.Username);

            return new ContractVersionDO(version, username);
        }

        [Route("{versionId:int}/sapData")]
        public ContractVersionSAPDataVO GetContractVersionSAPData(int contractId, int versionId, ContractVersionStatus status)
        {
            this.authorizer.AssertCanDo(ContractVersionActions.View, versionId);

            this.relationsRepository.AssertContractHasVersion(contractId, versionId);

            if (status == ContractVersionStatus.Draft || status == ContractVersionStatus.Entered)
            {
                throw new InvalidOperationException("Cannot create SAP report for contract version in status 'draft' or 'entered'.");
            }

            return this.contractVersionsRepository.GetContractVersionSAPData(versionId);
        }

        [HttpGet]
        [Route("{versionId:int}/sapData/print")]
        public HttpResponseMessage Print(int contractId, int versionId)
        {
            this.authorizer.AssertCanDo(ContractVersionActions.View, versionId);

            this.relationsRepository.AssertContractHasVersion(contractId, versionId);

            var sapData = this.contractVersionsRepository.GetContractVersionSAPData(versionId);

            JObject context = JObject.FromObject(new { item = sapData });

            var pdfBytes = this.printManager.Print(TemplateType.ContractVersionSAPData, PrintType.PDF, context);

            HttpResponseMessage responseMessage = this.Request.CreateResponse(HttpStatusCode.OK);
            responseMessage.Content = new ByteArrayContent(pdfBytes);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            responseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
            responseMessage.Content.Headers.ContentDisposition.FileName = "sapData.pdf";

            return responseMessage;
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/versions/{versionId:int}")]
        public ContractVersionDO GetProjectDossierVersion(int projectId, int contractId, int versionId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);
            this.relationsRepository.AssertContractHasVersion(contractId, versionId);

            var version = this.contractVersionsRepository.Find(versionId);

            var userClaimsContext = this.userClaimsContextFactory(version.CreatedByUserId);
            var username = string.Format("{0} ({1})", userClaimsContext.Fullname, userClaimsContext.Username);

            return new ContractVersionDO(version, username);
        }

        [HttpGet]
        [Route("new")]
        public ContractVersionDO NewVersion(int contractId, ContractVersionType type)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var username = string.Format("{0} ({1})", this.currentUserClaimsContext.Fullname, this.currentUserClaimsContext.Username);

            return new ContractVersionDO(type, username);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Versions.Create), IdParam = "contractId")]
        public object CreateVersion(int contractId, ContractVersionDO versionDO)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            if (this.contractVersionXmlService.CanCreateVersion(contractId, versionDO.VersionType).Count != 0)
            {
                throw new InvalidOperationException("Cannot create new contract version.");
            }

            var lastContractVersion = this.contractVersionsRepository.GetLastVersion(contractId);

            if (lastContractVersion.Status != ContractVersionStatus.Active)
            {
                throw new Exception("To create a new version the last contract version should be active.");
            }

            var numberData = this.contractsRepository.GetContractData(contractId);

            var newVersion = new ContractVersionXml(
                numberData,
                lastContractVersion,
                this.accessContext.UserId,
                versionDO.VersionType,
                versionDO.CreateNote);

            this.contractVersionsRepository.Add(newVersion);

            this.unitOfWork.Save();

            return new { ContractId = newVersion.ContractId, ContractVersionId = newVersion.ContractVersionXmlId };
        }

        [HttpPut]
        [Route("{versionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Versions.Edit), IdParam = "contractId", ChildIdParam = "versionId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateVersion(int contractId, int versionId, ContractVersionDO versionDO)
        {
            this.authorizer.AssertCanDo(ContractVersionActions.Edit, versionId);

            var version = this.contractVersionsRepository.FindForUpdate(versionId, versionDO.Version);

            version.SetAttributes(versionDO.CreateNote);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{versionId:int}/technicalEdit")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Versions.TechnicalEdit), IdParam = "contractId", ChildIdParam = "versionId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void TechnicalEdit(int contractId, int versionId, ContractVersionDO versionDO)
        {
            this.authorizer.AssertCanDo(ContractVersionActions.Edit, versionId);

            var version = this.contractVersionsRepository.FindForUpdate(versionId, versionDO.Version);

            if (!versionDO.ContractDate.HasValue)
            {
                throw new Exception("ContractDate is null");
            }

            version.ChangeContractDate(versionDO.ContractDate.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{versionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Versions.Delete), IdParam = "contractId", ChildIdParam = "versionId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DeleteVersion(int contractId, int versionId, string version)
        {
            this.authorizer.AssertCanDo(ContractVersionActions.Delete, versionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractVersion = this.contractVersionsRepository.FindForUpdate(versionId, vers);

            this.contractVersionsRepository.Remove(contractVersion);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{versionId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Versions.ChangeStatusToDraft), IdParam = "contractId", ChildIdParam = "versionId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void ChnageStatusToDraft(int contractId, int versionId, string version)
        {
            this.authorizer.AssertCanDo(ContractVersionActions.ChangeStatusToDraft, versionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var contractVersion = this.contractVersionsRepository.FindForUpdate(versionId, vers);

            contractVersion.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{versionId:int}/markAsChecked")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Versions.MarkAsChecked), IdParam = "contractId", ChildIdParam = "versionId")]
        public void MarkAsChecked(int contractId, int versionId, string version)
        {
            this.authorizer.AssertCanDo(ContractVersionActions.MarkAsChecked, versionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var contractVersion = this.contractVersionsRepository.FindForUpdate(versionId, vers);
            contractVersion.ChangeStatusToActive();

            this.unitOfWork.Save();

            var versionsToArchive = this.contractVersionsRepository.GetNonArchivedVersions(contractId)
                .Where(v => v.ContractVersionXmlId != contractVersion.ContractVersionXmlId);

            foreach (var cv in versionsToArchive)
            {
                cv.ChangeStatusToArchived();
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateVersion(int contractId, ContractVersionType type)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var errorList = this.contractVersionXmlService.CanCreateVersion(contractId, type);

            return new ErrorsDO(errorList);
        }
    }
}
