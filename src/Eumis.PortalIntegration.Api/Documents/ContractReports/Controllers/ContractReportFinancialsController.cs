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
    [RoutePrefix("api/contractReportFinancials")]
    public class ContractReportFinancialsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportService contractReportService;

        public ContractReportFinancialsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportsRepository contractReportsRepository,
            IProceduresRepository proceduresRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportService = contractReportService;
        }

        [Route("{contractReportFinancialGid:guid}")]
        public XmlDO GetContractReportFinancial(Guid contractReportFinancialGid)
        {
            var reportFinancial = this.contractReportFinancialsRepository.Find(contractReportFinancialGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(reportFinancial);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, reportFinancial.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, reportFinancial.ContractReportId));

            return new XmlDO
            {
                Xml = reportFinancial.Xml,
                Version = reportFinancial.Version,
            };
        }

        [Route("{contractReportFinancialGid:guid}/edit")]
        public ContractReportDocumentXmlDO GetContractReportFinancialForEdit(Guid contractReportFinancialGid)
        {
            var reportFinancial = this.contractReportFinancialsRepository.Find(contractReportFinancialGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(reportFinancial);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, reportFinancial.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, reportFinancial.ContractReportId));

            var loadedXml = this.contractReportService.GetContractReportFinancialXmlForEdit(reportFinancial);

            var canEnterErrors = this.contractReportService.CanChangeContractReportFinancialStatusToEntered(reportFinancial.ContractReportId);

            var procedureId = this.contractReportsRepository.GetContractReportProcedureId(reportFinancial.ContractReportId);
            var procedureContractReportDocuments = this.proceduresRepository.FindProcedureReportDocuments(procedureId, ProcedureContractReportDocumentType.FinancialReportDocument);

            return new ContractReportFinancialDocumentXmlDO(loadedXml, reportFinancial.Version, canEnterErrors, procedureContractReportDocuments);
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportFinancialGid:guid}")]
        public XmlDO UpdateContractReportFinancial(Guid contractReportFinancialGid, XmlDO contractReportFinancial)
        {
            var rf = this.contractReportFinancialsRepository.Find(contractReportFinancialGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(rf);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, rf.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.EditFinancial, rf.ContractReportId));

            var reportFinancial = this.contractReportService.UpdateContractReportFinancial(
                rf.ContractReportId,
                rf.ContractReportFinancialId,
                contractReportFinancial.Version,
                contractReportFinancial.Xml);

            var response = new XmlDO
            {
                ModifyDate = reportFinancial.ModifyDate,
                Version = reportFinancial.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancial.UpdateXml),
                reportFinancial.ContractReportId,
                reportFinancial.ContractReportFinancialId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportFinancialGid:guid}/enter")]
        public XmlDO EnterContractReportFinancial(Guid contractReportFinancialGid, XmlDO contractReportFinancial)
        {
            var rf = this.contractReportFinancialsRepository.Find(contractReportFinancialGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(rf);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, rf.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.EditFinancial, rf.ContractReportId));

            var reportFinancial = this.contractReportService.ChangeContractReportFinancialStatus(
                rf.ContractReportId,
                rf.ContractReportFinancialId,
                contractReportFinancial.Version,
                ContractReportFinancialStatus.Entered);

            var response = new XmlDO
            {
                ModifyDate = reportFinancial.ModifyDate,
                Version = reportFinancial.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancial.ChangeStatusToEntered),
                reportFinancial.ContractReportId,
                reportFinancial.ContractReportFinancialId,
                contractReportFinancial,
                response);

            return response;
        }

        private void AssertIsNotDraftOrEnteredFromBeneficiary(ContractReportFinancial financial)
        {
            var contractReport = this.contractReportsRepository.Find(financial.ContractReportId);

            if (contractReport.Source == Source.Beneficiary && (financial.Status == ContractReportFinancialStatus.Draft || financial.Status == ContractReportFinancialStatus.Entered))
            {
                throw new UnauthorizedAccessException("Cannot get ContractReportFinancial with status 'Draft' or Entered when the ContractReport has source 'Beneficiary'");
            }
        }
    }
}
