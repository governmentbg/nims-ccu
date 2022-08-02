using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.ContractProcurements.DataObjects;

namespace Eumis.PortalIntegration.Api.Portal.ContractReports.Controllers
{
    [RoutePrefix("api/contractreg/contracts/{contractGid:guid}/packages/{contractReportGid:guid}/finplan")]
    public class ContractReportFinancials1Controller : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportService contractReportService;

        public ContractReportFinancials1Controller(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IProceduresRepository proceduresRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportService = contractReportService;
        }

        [Route("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ContractReportFinancialDO> GetLastContractReportFinancialAsync(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            if (this.accessContext.IsUser)
            {
                var contractReport = await this.contractReportsRepository.FindAsync(contractReportGid, ct);
                this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReport.ContractReportId);
            }
            else if (this.accessContext.IsContractRegistration)
            {
                // TODO check permissions for contractGid
            }
            else
            {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            var reportFinancial = await this.contractReportFinancialsRepository.GetLastContractReportFinancialAsync(contractReportGid, ct);

            return new ContractReportFinancialDO(reportFinancial);
        }

        [Route("{contractReportFinancialGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ContractReportFinancialDO> GetContractReportFinancialAsync(Guid contractGid, Guid contractReportGid, Guid contractReportFinancialGid, CancellationToken ct)
        {
            var reportFinancial = await this.contractReportFinancialsRepository.FindAsync(contractReportFinancialGid, ct);

            return new ContractReportFinancialDO(reportFinancial);
        }

        [Route("{contractReportFinancialGid:guid}/edit")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ContractReportFinancialDO> GetContractReportFinancialForEditAsync(Guid contractGid, Guid contractReportGid, Guid contractReportFinancialGid, CancellationToken ct)
        {
            var reportFinancial = await this.contractReportFinancialsRepository.FindAsync(contractReportFinancialGid, ct);

            var loadedXml = await this.contractReportService.GetContractReportFinancialXmlForEditAsync(reportFinancial, ct);

            var canEnterErrors = await this.contractReportService.CanChangeContractReportFinancialStatusToEnteredAsync(reportFinancial.ContractReportId, ct);

            var procedureId = await this.contractReportsRepository.GetContractReportProcedureIdAsync(reportFinancial.ContractReportId, ct);

            // TODO Export procedure document to cached procedure object and remove them from DO
            var procedureContractReportDocuments = await this.proceduresRepository.FindProcedureReportDocumentsAsync(procedureId, ProcedureContractReportDocumentType.FinancialReportDocument, ct);

            return new ContractReportFinancialDO(reportFinancial, procedureContractReportDocuments, loadedXml, canEnterErrors);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> CreateContractReportFinancialAsync(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            var reportFinancial = await this.contractReportService.CreateContractReportFinancialAsync(contractReportGid, ct);

            var response = new XmlDO
            {
                Gid = reportFinancial.Gid,
                ModifyDate = reportFinancial.ModifyDate,
                Version = reportFinancial.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportFinancial.Create),
                reportFinancial.ContractReportId,
                null,
                null,
                response);

            return response;
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportFinancialGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> UpdateContractReportFinancialAsync(Guid contractGid, Guid contractReportGid, Guid contractReportFinancialGid, XmlDO contractReportFinancial, CancellationToken ct)
        {
            var reportFinancial = await this.contractReportService.UpdateContractReportFinancialAsync(contractReportFinancialGid, contractReportFinancial.Version, contractReportFinancial.Xml, ct);

            var response = new XmlDO
            {
                Gid = reportFinancial.Gid,
                ModifyDate = reportFinancial.ModifyDate,
                Version = reportFinancial.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportFinancial.UpdateXml),
                reportFinancial.ContractReportId,
                reportFinancial.ContractReportFinancialId,
                null,
                null);

            return response;
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportFinancialGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task DeleteContractReportFinancialAsync(Guid contractGid, Guid contractReportGid, Guid contractReportFinancialGid, VersionDO version, CancellationToken ct)
        {
            var reportFinancial = await this.contractReportService.DeleteContractReportFinancialAsync(contractReportFinancialGid, version.Version, ct);

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportFinancial.Delete),
                reportFinancial.ContractReportId,
                reportFinancial.ContractReportFinancialId,
                null,
                null);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportFinancialGid:guid}/enter")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> EnterContractReportFinancialAsync(Guid contractGid, Guid contractReportGid, Guid contractReportFinancialGid, XmlDO contractReportFinancial, CancellationToken ct)
        {
            var reportFinancial = await this.contractReportService.ChangeContractReportFinancialStatusAsync(contractReportFinancialGid, contractReportFinancial.Version, ContractReportFinancialStatus.Entered, (int?)null, ct);

            var response = new XmlDO
            {
                Gid = reportFinancial.Gid,
                ModifyDate = reportFinancial.ModifyDate,
                Version = reportFinancial.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportFinancial.ChangeStatusToEntered),
                reportFinancial.ContractReportId,
                reportFinancial.ContractReportFinancialId,
                contractReportFinancial,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportFinancialGid:guid}/makedraft")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> DraftContractReportFinancialAsync(Guid contractGid, Guid contractReportGid, Guid contractReportFinancialGid, XmlDO contractReportFinancial, CancellationToken ct)
        {
            var reportFinancial = await this.contractReportService.ChangeContractReportFinancialStatusAsync(contractReportFinancialGid, contractReportFinancial.Version, ContractReportFinancialStatus.Draft, (int?)null, ct);

            var response = new XmlDO
            {
                Gid = reportFinancial.Gid,
                ModifyDate = reportFinancial.ModifyDate,
                Version = reportFinancial.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportFinancial.ChangeStatusToDraft),
                reportFinancial.ContractReportId,
                reportFinancial.ContractReportFinancialId,
                contractReportFinancial,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportFinancialGid:guid}/makeactual")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> ActualContractReportFinancialAsync(Guid contractGid, Guid contractReportGid, Guid contractReportFinancialGid, XmlDO contractReportFinancial, CancellationToken ct)
        {
            var regId = this.accessContext.ContractRegistrationId;
            var reportFinancial = await this.contractReportService.ChangeContractReportFinancialStatusAsync(contractReportFinancialGid, contractReportFinancial.Version, ContractReportFinancialStatus.Actual, regId, ct);

            var response = new XmlDO
            {
                Gid = reportFinancial.Gid,
                ModifyDate = reportFinancial.ModifyDate,
                Version = reportFinancial.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportFinancial.ChangeStatusToActual),
                reportFinancial.ContractReportId,
                reportFinancial.ContractReportFinancialId,
                contractReportFinancial,
                response);

            return response;
        }

        [HttpPost]
        [Route("canCreate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ErrorsDO> CanCreateContractReportFinancialAsync(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            var errorList = await this.contractReportService.CanCreateContractReportFinancialAsync(contractReportGid, ct);

            return new ErrorsDO(errorList);
        }
    }
}
