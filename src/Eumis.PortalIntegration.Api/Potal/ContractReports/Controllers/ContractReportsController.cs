using Eumis.ApplicationServices.Services.ContractReport;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.ContractProcurements.DataObjects;
using Eumis.PortalIntegration.Api.Potal.ContractReports.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.ContractReports.Controllers
{
    [RoutePrefix("api/contractreg/contracts/{contractGid:guid}/packages")]
    public class ContractReportsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;
        private IContractReportsRepository contractReportsRepository;
        private IContractsRepository contractsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportTechnicalsRepository contractReportTechnicalsRepository;
        private IContractReportService contractReportService;

        public ContractReportsController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IContractReportsRepository contractReportsRepository,
            IContractsRepository contractsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportTechnicalsRepository contractReportTechnicalsRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.actionLogger = actionLogger;
            this.contractReportsRepository = contractReportsRepository;
            this.contractsRepository = contractsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportTechnicalsRepository = contractReportTechnicalsRepository;
            this.contractReportService = contractReportService;
        }

        [Route("")]
        public async Task<ReportPagePVO> GetContractReportsAsync(Guid contractGid, CancellationToken ct, int offset = 0, int? limit = null)
        {
            var results = await this.contractReportsRepository.GetPortalContractReportsAsync(contractGid, ct, offset, limit);

            var canCreate = !(await this.contractReportService.CanCreateContractReportAsync(contractGid, ct)).Any();

            var canEditSent = !(await this.contractReportService.CanEditSentContractReportAsync(contractGid, ct)).Any();

            return new ReportPagePVO(results, canCreate, canEditSent);
        }

        [Route("{contractReportGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ContractReportDO> GetContractReport(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            var report = await this.contractReportsRepository.FindAsync(contractReportGid, ct);

            return new ContractReportDO(report);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("")]
        public async Task<XmlDO> CreateContractReportAsync(Guid contractGid, ContractReportDO contractReportDO, CancellationToken ct)
        {
            if ((await this.contractReportService.CanCreateContractReportAsync(contractGid, ct)).Any())
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.ExistsDraftReport }));
            }

            var contractId = await this.contractsRepository.GetContractIdAsync(contractGid, ct);

            ContractReport newContractReport = await this.contractReportService.CreateContractReportAsync(
                contractGid,
                contractReportDO.ContractReportType.Value,
                contractReportDO.OtherRegistration,
                contractReportDO.StoragePlace,
                contractReportDO.SubmitDate,
                contractReportDO.SubmitDeadline,
                ct);

            var result = new XmlDO
            {
                Gid = newContractReport.Gid,
                Version = newContractReport.Version,
                ModifyDate = newContractReport.ModifyDate,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Create),
                null,
                null,
                contractReportDO,
                result);

            return result;
        }

        [HttpPost]
        [Route("{contractReportGid:guid}/canUpdate")]
        public async Task<ErrorsDO> CanUpdateContractReportAsync(Guid contractReportGid, ContractReportDO contractReportDO, CancellationToken ct)
        {
            var errorList = await this.contractReportService.CanChangeContractReportTypeAsync(contractReportGid, contractReportDO.ContractReportType.Value, ct);

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> UpdateContractReport(Guid contractGid, Guid contractReportGid, ContractReportDO contractReportDO, CancellationToken ct)
        {
            var report = await this.contractReportService.UpdateContractReportAsync(
                contractReportGid,
                contractReportDO.Version,
                contractReportDO.ContractReportType.Value,
                contractReportDO.OtherRegistration,
                contractReportDO.StoragePlace,
                contractReportDO.SubmitDate,
                contractReportDO.SubmitDeadline,
                ct);

            var response = new XmlDO
            {
                ModifyDate = report.ModifyDate,
                Version = report.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Update),
                report.ContractReportId,
                null,
                contractReportDO,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportGid:guid}/copy")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ContractReportGidDO> CopyContractReportAsync(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            if ((await this.contractReportService.CanCopyContractReportAsync(contractReportGid, ct)).Any())
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.ExistsDraftReport }));
            }

            var newContractReport = await this.contractReportService.CopyContractReportAsync(contractReportGid, ct);
            var res = new ContractReportGidDO
            {
                Gid = newContractReport.Gid,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Copy),
                newContractReport.ContractReportId,
                null,
                null,
                res);

            return res;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportGid:guid}/submit")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task SubmitContractReportAsync(Guid contractGid, Guid contractReportGid, VersionDO version, CancellationToken ct)
        {
            var regId = this.accessContext.ContractRegistrationId;
            var report = await this.contractReportService.ChangeContractReportStatusAsync(contractReportGid, version.Version, ContractReportStatus.SentChecked, regId, ct);

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.ChangeStatusToSent),
                report.ContractReportId,
                null,
                null,
                null);
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task DeleteContractReportAsync(Guid contractGid, Guid contractReportGid, VersionDO version, CancellationToken ct)
        {
            var report = await this.contractReportService.DeleteContractReportAsync(contractReportGid, version.Version, ct);

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Delete),
                report.ContractReportId,
                null,
                null,
                null);
        }

        [HttpPost]
        [Route("canCreate")]
        public async Task<ErrorsDO> CanCreateContractReportAsync(Guid contractGid, CancellationToken ct)
        {
            var errorList = await this.contractReportService.CanCreateContractReportAsync(contractGid, ct);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{contractReportGid:guid}/canCopy")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ErrorsDO> CanCopyContractReport(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            var errorList = new List<string>();

            errorList.AddRange(await this.contractReportService.CanCopyContractReportAsync(contractReportGid, ct));

            errorList.AddRange(await this.contractReportService.HasAdvanceVerificationPaymentAsync(contractReportGid, ct));

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{contractReportGid:guid}/canSubmit")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ErrorsDO> CanSubmitContractReport(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            var errorList = await this.contractReportService.CanEnterContractReportAsync(contractReportGid, ct);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{contractReportGid:guid}/canDelete")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ErrorsDO> CanDeleteContractReport(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            var errorList = await this.contractReportService.CanDeleteContractReportAsync(contractReportGid, ct);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportGid:guid}/makeDraft")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task DraftContractReport(Guid contractGid, Guid contractReportGid, VersionDO version, CancellationToken ct)
        {
            var report = await this.contractReportService.ChangeContractReportStatusAsync(contractReportGid, version.Version, ContractReportStatus.Draft, (int?)null, ct);

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.ChangeStatusToDraft),
                report.ContractReportId,
                null,
                null,
                null);
        }

        [HttpPost]
        [Route("{contractReportGid:guid}/canMakeDraft")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ErrorsDO> CanDraftContractReportAsync(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            var errorList = await this.contractReportService.CanDraftContractReportAsync(contractReportGid, ct);

            return new ErrorsDO(errorList);
        }
    }
}
