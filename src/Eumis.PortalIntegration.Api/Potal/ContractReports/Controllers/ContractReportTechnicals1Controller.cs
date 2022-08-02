using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractReport;
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
    [RoutePrefix("api/contractreg/contracts/{contractGid:guid}/packages/{contractReportGid:guid}/techplan")]
    public class ContractReportTechnicals1Controller : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IContractReportTechnicalsRepository contractReportTechnicalsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportService contractReportService;
        private IAccessContext accessContext;

        public ContractReportTechnicals1Controller(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IContractReportTechnicalsRepository contractReportTechnicalsRepository,
            IContractReportsRepository contractReportsRepository,
            IProceduresRepository proceduresRepository,
            IContractReportService contractReportService,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.contractReportTechnicalsRepository = contractReportTechnicalsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportService = contractReportService;
            this.accessContext = accessContext;
        }

        [Route("{contractReportTechnicalGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ContractReportTechnicalDO> GetContractReportTechnicalAsync(Guid contractGid, Guid contractReportGid, Guid contractReportTechnicalGid, CancellationToken ct)
        {
            var reportTechnical = await this.contractReportTechnicalsRepository.FindAsync(contractReportTechnicalGid, ct);

            return new ContractReportTechnicalDO(reportTechnical);
        }

        [Route("{contractReportTechnicalGid:guid}/edit")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ContractReportTechnicalDO> GetContractReportTechnicalForEdit(Guid contractGid, Guid contractReportGid, Guid contractReportTechnicalGid, CancellationToken ct)
        {
            var reportTechnical = await this.contractReportTechnicalsRepository.FindAsync(contractReportTechnicalGid, ct);

            var loadedXml = await this.contractReportService.GetContractReportTechnicalXmlForEditAsync(reportTechnical, ct);

            var canEnterErrors = await this.contractReportService.CanChangeContractReportTechnicalStatusToEnteredAsync(reportTechnical.ContractReportId, ct);

            var procedureId = await this.contractReportsRepository.GetContractReportProcedureIdAsync(reportTechnical.ContractReportId, ct);

            var procedureContractReportDocuments = await this.proceduresRepository.FindProcedureReportDocumentsAsync(procedureId, ProcedureContractReportDocumentType.TechnicalReportDocument, ct);

            return new ContractReportTechnicalDO(reportTechnical, procedureContractReportDocuments, loadedXml, canEnterErrors);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> CreateContractReportTechnicalAsync(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            var reportTechnical = await this.contractReportService.CreateContractReportTechnicalAsync(contractReportGid, ct);

            var response = new XmlDO
            {
                Gid = reportTechnical.Gid,
                ModifyDate = reportTechnical.ModifyDate,
                Version = reportTechnical.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportTechnical.Create),
                reportTechnical.ContractReportId,
                null,
                null,
                response);

            return response;
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportTechnicalGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> UpdateContractReportTechnical(Guid contractGid, Guid contractReportGid, Guid contractReportTechnicalGid, XmlDO contractReportTechnical, CancellationToken ct)
        {
            var reportTechnical = await this.contractReportService.UpdateContractReportTechnicalAsync(contractReportTechnicalGid, contractReportTechnical.Version, contractReportTechnical.Xml, ct);

            var response = new XmlDO
            {
                Gid = reportTechnical.Gid,
                ModifyDate = reportTechnical.ModifyDate,
                Version = reportTechnical.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportTechnical.UpdateXml),
                reportTechnical.ContractReportId,
                reportTechnical.ContractReportTechnicalId,
                null,
                null);

            return response;
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportTechnicalGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task DeleteContractReportTechnicalAsync(Guid contractGid, Guid contractReportGid, Guid contractReportTechnicalGid, VersionDO version, CancellationToken ct)
        {
            var reportTechnical = await this.contractReportService.DeleteContractReportTechnicalAsync(contractReportTechnicalGid, version.Version, ct);

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportTechnical.Delete),
                reportTechnical.ContractReportId,
                reportTechnical.ContractReportTechnicalId,
                null,
                null);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportTechnicalGid:guid}/enter")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> EnterContractReportTechnicalAsync(Guid contractGid, Guid contractReportGid, Guid contractReportTechnicalGid, XmlDO contractReportTechnical, CancellationToken ct)
        {
            var reportTechnical = await this.contractReportService.ChangeContractReportTechnicalStatusAsync(contractReportTechnicalGid, contractReportTechnical.Version, ContractReportTechnicalStatus.Entered, (int?)null, ct);

            var response = new XmlDO
            {
                Gid = reportTechnical.Gid,
                ModifyDate = reportTechnical.ModifyDate,
                Version = reportTechnical.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportTechnical.ChangeStatusToEntered),
                reportTechnical.ContractReportId,
                reportTechnical.ContractReportTechnicalId,
                contractReportTechnical,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportTechnicalGid:guid}/makedraft")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> DraftContractReportTechnicalAsync(Guid contractGid, Guid contractReportGid, Guid contractReportTechnicalGid, XmlDO contractReportTechnical, CancellationToken ct)
        {
            var reportTechnical = await this.contractReportService.ChangeContractReportTechnicalStatusAsync(contractReportTechnicalGid, contractReportTechnical.Version, ContractReportTechnicalStatus.Draft, (int?)null, ct);

            var response = new XmlDO
            {
                Gid = reportTechnical.Gid,
                ModifyDate = reportTechnical.ModifyDate,
                Version = reportTechnical.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportTechnical.ChangeStatusToDraft),
                reportTechnical.ContractReportId,
                reportTechnical.ContractReportTechnicalId,
                contractReportTechnical,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportTechnicalGid:guid}/makeactual")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<XmlDO> ActualContractReportTechnicalAsync(Guid contractGid, Guid contractReportGid, Guid contractReportTechnicalGid, XmlDO contractReportTechnical, CancellationToken ct)
        {
            var regId = this.accessContext.ContractRegistrationId;
            var reportTechnical = await this.contractReportService.ChangeContractReportTechnicalStatusAsync(contractReportTechnicalGid, contractReportTechnical.Version, ContractReportTechnicalStatus.Actual, regId, ct);

            var response = new XmlDO
            {
                Gid = reportTechnical.Gid,
                ModifyDate = reportTechnical.ModifyDate,
                Version = reportTechnical.Version,
            };

            await this.actionLogger.LogActionAsync(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportTechnical.ChangeStatusToActual),
                reportTechnical.ContractReportId,
                reportTechnical.ContractReportTechnicalId,
                contractReportTechnical,
                response);

            return response;
        }

        [HttpPost]
        [Route("canCreate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public async Task<ErrorsDO> CanCreateContractReportTechnicalAsync(Guid contractGid, Guid contractReportGid, CancellationToken ct)
        {
            var errorList = await this.contractReportService.CanCreateContractReportTechnicalAsync(contractReportGid, ct);

            return new ErrorsDO(errorList);
        }
    }
}
