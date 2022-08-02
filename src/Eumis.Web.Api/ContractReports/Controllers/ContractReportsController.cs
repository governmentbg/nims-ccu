using Eumis.ApplicationServices.Services.ContractReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Print;
using Eumis.Web.Api.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReports")]
    public class ContractReportsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPrintManager printManager;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportPaymentChecksRepository contractReportPaymentChecksRepository;
        private IContractReportService contractReportService;
        private IPermissionsRepository permissionsRepository;

        public ContractReportsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPrintManager printManager,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository,
            IContractReportService contractReportService,
            IPermissionsRepository permissionsRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.printManager = printManager;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportPaymentChecksRepository = contractReportPaymentChecksRepository;
            this.contractReportService = contractReportService;
            this.permissionsRepository = permissionsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportVO> GetContractReports(
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null)
        {
            this.authorizer.AssertCanDo(ContractReportListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractReportPermissions.CanRead);

            return this.contractReportsRepository.GetContractReports(programmeIds, contractRegNum, procedureId, contractReportNum);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportRequestedAmounts")]
        public IList<ContractReportRequestedAmountVO> GetContractReportRequestedAmountsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportsRepository.GetContractReportRequestedAmountsForProjectDossier(contractId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportApprovedAmounts")]
        public IList<ContractReportApprovedAmountVO> GetContractReportApprovedAmountsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportsRepository.GetContractReportApprovedAmountsForProjectDossier(contractId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportCertifiedAmounts")]
        public IList<ContractReportCertifiedAmountVO> GetContractReportCertifiedAmountsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportsRepository.GetContractReportCertifiedAmountsForProjectDossier(contractId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportsWithTechnical")]
        public IList<ContractReportVO> GetContractReportWithTechnicalForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportsRepository.GetContractReportWithTechnicalForProjectDossier(contractId);
        }

        [Route("~/api/contractReportFinancialCorrections/contractReports")]
        public IList<ContractReportVO> GetContractReportsForContractReportFinancialCorrection(
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionListActions.Create);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractReportsRepository.GetContractReportsForFinancial(programmeIds, contractRegNum, procedureId, contractReportNum, this.accessContext.UserId);
        }

        [Route("~/api/contractReportTechnicalCorrections/contractReports")]
        public IList<ContractReportVO> GetContractReportsForContractReportTechnicalCorrection(
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionListActions.Create);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractReportsRepository.GetContractReportsForTechnical(programmeIds, this.accessContext.UserId, contractRegNum, procedureId, contractReportNum);
        }

        [Route("~/api/contractReportFinancialRevalidations/contractReports")]
        public IList<ContractReportVO> GetContractReportsForContractReportFinancialRevalidation(
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationListActions.Create);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractReportsRepository.GetContractReportsForFinancial(programmeIds, contractRegNum, procedureId, contractReportNum);
        }

        [Route("~/api/contractReportFinancialCertCorrections/contractReports")]
        public IList<ContractReportVO> GetContractReportsForContractReportFinancialCertCorrection(
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionListActions.Create);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractReportsRepository.GetContractReportsForFinancial(programmeIds, contractRegNum, procedureId, contractReportNum);
        }

        [Route("~/api/contractReportCertAuthorityFinancialCorrections/contractReports")]
        public IList<ContractReportVO> GetContractReportsForContractReportCertAuthorityFinancialCorrection(
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionListActions.Create);

            var programmeIds = System.Array.Empty<int>();

            return this.contractReportsRepository.GetContractReportsForFinancial(programmeIds, contractRegNum, procedureId, contractReportNum);
        }

        [Route("~/api/contractReportRevalidationCertAuthorityFinancialCorrections/contractReports")]
        public IList<ContractReportVO> GetContractReportsForContractReportRevalidationCertAuthorityFinancialCorrection(
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionListActions.Create);

            var programmeIds = System.Array.Empty<int>();

            return this.contractReportsRepository.GetContractReportsForFinancial(programmeIds, contractRegNum, procedureId, contractReportNum);
        }

        [Route("~/api/financialCorrections/{financialCorrectionId:int}/contractReports")]
        public IList<ContractReportVO> GetFinancialCorrectionContractReports(int financialCorrectionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.View, financialCorrectionId);

            return this.contractReportsRepository.GetFinancialCorrectionContractReports(financialCorrectionId);
        }

        [Route("checks")]
        public IList<ContractReportVO> GetContractReportChecksContractReports(string contractRegNum = null)
        {
            this.authorizer.AssertCanDo(ContractReportCheckListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractReportsRepository.GetContractReportChecksContractReports(programmeIds, this.accessContext.UserId, contractRegNum);
        }

        [Route("{contractReportId:int}")]
        public ContractReportDO GetContractReport(int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportFinancialCorrectionListActions.Search, null),
                Tuple.Create<Enum, int?>(ContractReportCertAuthorityFinancialCorrectionListActions.Search, null));

            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsNotDraftFromBeneficiary();

            return new ContractReportDO(contractReport);
        }

        [Route("new")]
        public ContractReportDO GetNewContractReport(string contractNum)
        {
            var contract = this.contractsRepository.FindByRegNumber(contractNum);

            this.authorizer.AssertCanDo(ContractReportListActions.Create);

            return new ContractReportDO()
            {
                ContractId = contract.ContractId,
                Status = ContractReportStatus.Draft,
                Source = Source.AdministrativeAuthority,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Create))]
        public object CreateContractReport(ContractReportDO contractReport)
        {
            this.authorizer.AssertCanDo(ContractReportListActions.Create);

            var newContractReport = this.contractReportService.CreateContractReport(
                contractReport.ContractId,
                contractReport.ReportType.Value,
                contractReport.OtherRegistration,
                contractReport.StoragePlace,
                contractReport.SubmitDate,
                contractReport.SubmitDeadline);

            return new { ContractReportId = newContractReport.ContractReportId };
        }

        [HttpPost]
        [Route("canCreate")]
        public object CanCreateContractReport(int contractId)
        {
            this.authorizer.AssertCanDo(ContractReportListActions.Create);

            var errorList = this.contractReportService.CanCreateContractReport(contractId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{contractReportId:int}/canUpdate")]
        public object CanUpdateContractReport(int contractReportId, ContractReportDO contractReport)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var errorList = this.contractReportService.CanChangeContractReportType(contractReportId, contractReport.ReportType.Value);

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Route("{contractReportId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.BasicData), IdParam = "contractReportId")]
        public void UpdateContractReport(int contractReportId, ContractReportDO contractReport)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            this.contractReportService.UpdateContractReport(
                contractReportId,
                contractReport.Version,
                contractReport.ReportType.Value,
                contractReport.OtherRegistration,
                contractReport.StoragePlace,
                contractReport.SubmitDate,
                contractReport.SubmitDeadline);
        }

        [HttpPut]
        [Route("{contractReportId:int}/checkUpdate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.BasicCheckData), IdParam = "contractReportId")]
        public void UpdateContractReportCheck(int contractReportId, ContractReportDO contractReport)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            this.contractReportService.UpdateContractReportCheck(
                contractReportId,
                contractReport.Version,
                contractReport.CheckedDate);
        }

        [HttpDelete]
        [Route("{contractReportId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Delete), IdParam = "contractReportId")]
        public void DeleteContractReport(int contractReportId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Delete, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.DeleteContractReport(contractReportId, vers);
        }

        [HttpPost]
        [Route("{contractReportId:int}/canDelete")]
        public object CanDeleteContractReport(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Delete, contractReportId);

            var errorList = this.contractReportService.CanDeleteContractReport(contractReportId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{contractReportId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.ChangeStatusToDraft), IdParam = "contractReportId")]
        public void ChangeContractReportStatusToDraft(int contractReportId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportStatus(contractReportId, vers, Domain.Contracts.ContractReportStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportId:int}/changeStatusToEntered")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.ChangeStatusToEntered), IdParam = "contractReportId")]
        public void ChangeContractReportStatusToEntered(int contractReportId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportStatus(contractReportId, vers, Domain.Contracts.ContractReportStatus.Entered);
        }

        [HttpPost]
        [Route("{contractReportId:int}/changeStatusToSentChecked")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.ChangeStatusToSentChecked), IdParam = "contractReportId")]
        public void ChangeContractReportStatusToSentChecked(int contractReportId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportActions.MarkAsChecked, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportStatus(contractReportId, vers, Domain.Contracts.ContractReportStatus.SentChecked);
        }

        [HttpPost]
        [Route("{contractReportId:int}/changeStatusToAccepted")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.ChangeStatusToAccepted), IdParam = "contractReportId")]
        public void ChangeContractReportStatusToAccepted(int contractReportId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.MarkAsAccepted, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportStatus(contractReportId, vers, Domain.Contracts.ContractReportStatus.Accepted);
        }

        [HttpPost]
        [Route("{contractReportId:int}/changeStatusToRefused")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.ChangeStatusToRefused), IdParam = "contractReportId")]
        public void ChangeContractReportStatusToRefused(int contractReportId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.MarkAsRefused, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportStatus(contractReportId, vers, Domain.Contracts.ContractReportStatus.Refused, confirm.Note);
        }

        [HttpGet]
        [Route("{contractReportId:int}/canChangeStatusToUnchecked")]
        public ErrorsDO CanChangeContractReportStatusToUnchecked(int contractReportId)
        {
            var errorList = new List<string>();

            try
            {
                this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);
            }
            catch (Exception)
            {
                errorList.Add("Можете да вкарате в проверка само поредни пакети отчетни документи");
            }

            errorList.AddRange(this.contractReportService.CanChangeContractReportStatusToUnchecked(contractReportId));

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{contractReportId:int}/changeStatusToUnchecked")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.ChangeStatusToUnchecked), IdParam = "contractReportId")]
        public void ChangeContractReportStatusToUnchecked(int contractReportId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.MarkAsUnchecked, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportStatus(contractReportId, vers, Domain.Contracts.ContractReportStatus.Unchecked);
        }

        [HttpPost]
        [Route("{contractReportId:int}/canChangeStatusToEntered")]
        public ErrorsDO CanChangeContractReportStatusToEntered(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var errors = this.contractReportService.CanEnterContractReport(contractReportId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportId:int}/canChangeStatusToAccepted")]
        public ErrorsDO CanChangeContractReportStatusToAccepted(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.MarkAsAccepted, contractReportId);

            var errors = this.contractReportService.CanAcceptContractReport(contractReportId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportId:int}/canChangeStatusToRefused")]
        public ErrorsDO CanChangeContractReportStatusToRefused(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.MarkAsRefused, contractReportId);

            var errors = this.contractReportService.CanRefuseContractReport(contractReportId);

            return new ErrorsDO(errors);
        }

        [Route("{contractReportId:int}/info")]
        public ContractReportInfoDO GetContractReportInfo(int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            var actualContractReportCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(contractReportId);
            var hasReturnedDocuments = this.contractReportsRepository.HasReturnedContractReportDocuments(contractReportId);

            return new ContractReportInfoDO(contractReport, actualContractReportCheck != null, hasReturnedDocuments);
        }

        [HttpGet]
        [Route("isRegNumExisting")]
        public bool IsContractReportNumExisting(string contractReportNum)
        {
            return this.contractReportsRepository.IsContractReportNumExisting(contractReportNum);
        }

        [HttpPost]
        [Route("{contractReportId:int}/returnStatusToUnchecked")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.ReturnStatusToUnchecked), IdParam = "contractReportId")]
        public void ReturnContractReportStatusToUnchecked(int contractReportId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ReturnContractReportStatusToUnchecked(contractReportId, vers);
        }

        [HttpPost]
        [Route("{contractReportId:int}/canReturnStatusToUnchecked")]
        public ErrorsDO CanReturnContractReportStatusToUnchecked(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            var errors = this.contractReportService.CanReturnContractReportStatusToUnchecked(contractReportId);

            return new ErrorsDO(errors);
        }

        [Route("{contractReportId:int}/sapData")]
        public IList<ContractReportSAPDataVO> GetContractReportSAPData(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportsRepository.GetContractReportSAPData(contractReportId);
        }

        [HttpGet]
        [Route("{contractReportId:int}/sapData/print")]
        public HttpResponseMessage Print(int contractReportId, int? programmePriorityId = null)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            var sapData = this.contractReportsRepository.GetContractReportSAPData(contractReportId);

            if (programmePriorityId.HasValue)
            {
                sapData = sapData.Where(t => t.ProgrammePriorityId == programmePriorityId).ToList();
            }

            JObject context = JObject.FromObject(new { items = sapData });

            var pdfBytes = this.printManager.Print(TemplateType.ContractReportSAPData, PrintType.PDF, context);

            HttpResponseMessage responseMessage = this.Request.CreateResponse(HttpStatusCode.OK);
            responseMessage.Content = new ByteArrayContent(pdfBytes);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            responseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
            responseMessage.Content.Headers.ContentDisposition.FileName = "sapData.pdf";

            return responseMessage;
        }
    }
}
