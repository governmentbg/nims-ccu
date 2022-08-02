using System;
using System.Linq;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Documents.ContractReports.DataObjects;

namespace Eumis.PortalIntegration.Api.Documents.ContractReports.Controllers
{
    [RoutePrefix("api/contractReportTechnicals")]
    public class ContractReportTechnicalsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IContractReportTechnicalsRepository contractReportTechnicalsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportService contractReportService;

        public ContractReportTechnicalsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IContractReportTechnicalsRepository contractReportTechnicalsRepository,
            IContractReportsRepository contractReportsRepository,
            IProceduresRepository proceduresRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.contractReportTechnicalsRepository = contractReportTechnicalsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportService = contractReportService;
        }

        [Route("{contractReportTechnicalGid:guid}")]
        public XmlDO GetContractReportTechnical(Guid contractReportTechnicalGid)
        {
            var reportTechnical = this.contractReportTechnicalsRepository.Find(contractReportTechnicalGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(reportTechnical);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, reportTechnical.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, reportTechnical.ContractReportId));

            return new XmlDO
            {
                Xml = reportTechnical.Xml,
                Version = reportTechnical.Version,
            };
        }

        [Route("{contractReportTechnicalGid:guid}/edit")]
        public ContractReportDocumentXmlDO GetContractReportTechnicalForEdit(Guid contractReportTechnicalGid)
        {
            var reportTechnical = this.contractReportTechnicalsRepository.Find(contractReportTechnicalGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(reportTechnical);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, reportTechnical.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, reportTechnical.ContractReportId));

            var loadedXml = this.contractReportService.GetContractReportTechnicalXmlForEdit(reportTechnical);

            var canEnterErrors = this.contractReportService.CanChangeContractReportTechnicalStatusToEntered(reportTechnical.ContractReportId);

            var procedureId = this.contractReportsRepository.GetContractReportProcedureId(reportTechnical.ContractReportId);
            var procedureContractReportDocuments = this.proceduresRepository.FindProcedureReportDocuments(procedureId, ProcedureContractReportDocumentType.TechnicalReportDocument);

            return new ContractReportTechnicalDocumentXmlDO(loadedXml, reportTechnical.Version, canEnterErrors, procedureContractReportDocuments);
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportTechnicalGid:guid}")]
        public XmlDO UpdateContractReportTechnical(Guid contractReportTechnicalGid, XmlDO contractReportTechnical)
        {
            var rt = this.contractReportTechnicalsRepository.Find(contractReportTechnicalGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(rt);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, rt.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.EditTechnical, rt.ContractReportId));

            var reportTechnical = this.contractReportService.UpdateContractReportTechnical(
                rt.ContractReportId,
                rt.ContractReportTechnicalId,
                contractReportTechnical.Version,
                contractReportTechnical.Xml);

            var response = new XmlDO
            {
                ModifyDate = reportTechnical.ModifyDate,
                Version = reportTechnical.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnical.UpdateXml),
                reportTechnical.ContractReportId,
                reportTechnical.ContractReportTechnicalId,
                contractReportTechnical,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportTechnicalGid:guid}/enter")]
        public XmlDO EnterContractReportTechnical(Guid contractReportTechnicalGid, XmlDO contractReportTechnical)
        {
            var rt = this.contractReportTechnicalsRepository.Find(contractReportTechnicalGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(rt);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, rt.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.EditTechnical, rt.ContractReportId));

            var reportTechnical = this.contractReportService.ChangeContractReportTechnicalStatus(
                rt.ContractReportId,
                rt.ContractReportTechnicalId,
                contractReportTechnical.Version,
                ContractReportTechnicalStatus.Entered);

            var response = new XmlDO
            {
                ModifyDate = reportTechnical.ModifyDate,
                Version = reportTechnical.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnical.ChangeStatusToEntered),
                reportTechnical.ContractReportId,
                reportTechnical.ContractReportTechnicalId,
                contractReportTechnical,
                response);

            return response;
        }

        private void AssertIsNotDraftOrEnteredFromBeneficiary(ContractReportTechnical technical)
        {
            var contractReport = this.contractReportsRepository.Find(technical.ContractReportId);

            if (contractReport.Source == Source.Beneficiary && (technical.Status == ContractReportTechnicalStatus.Draft || technical.Status == ContractReportTechnicalStatus.Entered))
            {
                throw new UnauthorizedAccessException("Cannot get ContractReportTechnical with status 'Draft' or Entered when the ContractReport has source 'Beneficiary'");
            }
        }
    }
}
