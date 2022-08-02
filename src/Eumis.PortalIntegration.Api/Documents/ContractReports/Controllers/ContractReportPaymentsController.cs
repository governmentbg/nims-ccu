using System;
using System.Collections.Generic;
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
    [RoutePrefix("api/contractReportPayments")]
    public class ContractReportPaymentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportService contractReportService;

        public ContractReportPaymentsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportsRepository contractReportsRepository,
            IProceduresRepository proceduresRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportService = contractReportService;
        }

        [Route("{contractReportPaymentGid:guid}")]
        public ContractReportDocumentXmlDO GetContractReportPayment(Guid contractReportPaymentGid)
        {
            var reportPayment = this.contractReportPaymentsRepository.Find(contractReportPaymentGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(reportPayment);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, reportPayment.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, reportPayment.ContractReportId));

            var canEnterErrors = this.contractReportService.CanChangeContractReportPaymentStatusToEntered(reportPayment.ContractReportId);

            ProcedureContractReportDocumentType? procedureContractReportDocumentType = null;
            bool? contractReportHasAdvanceVerificationPayment = null;

            switch (reportPayment.PaymentType)
            {
                case ContractReportPaymentType.Advance:
                case ContractReportPaymentType.AdvanceVerification:
                    procedureContractReportDocumentType = ProcedureContractReportDocumentType.AdvancePaymentDocument;

                    contractReportHasAdvanceVerificationPayment = !this.contractReportPaymentsRepository.CanCreateAdvanceVerificationPayment(reportPayment.ContractId);

                    break;
                case ContractReportPaymentType.Intermediate:
                    procedureContractReportDocumentType = ProcedureContractReportDocumentType.IntermediatePaymentDocument;

                    break;
                case ContractReportPaymentType.Final:
                    procedureContractReportDocumentType = ProcedureContractReportDocumentType.FinalPaymentDocument;

                    break;
            }

            var procedureId = this.contractReportsRepository.GetContractReportProcedureId(reportPayment.ContractReportId);
            var procedureContractReportDocuments = this.proceduresRepository.FindProcedureReportDocuments(procedureId, procedureContractReportDocumentType.Value);

            return new ContractReportPaymentDocumentXmlDO(reportPayment.Xml, reportPayment.Version, contractReportHasAdvanceVerificationPayment, canEnterErrors, procedureContractReportDocuments);
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportPaymentGid:guid}")]
        public XmlDO UpdateContractReportPayment(Guid contractReportPaymentGid, XmlDO contractReportPayment)
        {
            var rp = this.contractReportPaymentsRepository.Find(contractReportPaymentGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(rp);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, rp.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.EditFinancial, rp.ContractReportId));

            var reportPayment = this.contractReportService.UpdateContractReportPayment(
                rp.ContractReportId,
                rp.ContractReportPaymentId,
                contractReportPayment.Version,
                contractReportPayment.Xml);

            var response = new XmlDO
            {
                ModifyDate = reportPayment.ModifyDate,
                Version = reportPayment.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.ContractReports.Edit.ContractReportPayment.UpdateXml),
                reportPayment.ContractReportId,
                reportPayment.ContractReportPaymentId,
                contractReportPayment,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportPaymentGid:guid}/enter")]
        public XmlDO EnterContractReportPayment(Guid contractReportPaymentGid, XmlDO contractReportPayment)
        {
            var rp = this.contractReportPaymentsRepository.Find(contractReportPaymentGid);

            this.AssertIsNotDraftOrEnteredFromBeneficiary(rp);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, rp.ContractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.EditFinancial, rp.ContractReportId));

            var reportPayment = this.contractReportService.ChangeContractReportPaymentStatus(
                rp.ContractReportId,
                rp.ContractReportPaymentId,
                contractReportPayment.Version,
                ContractReportPaymentStatus.Entered);

            var response = new XmlDO
            {
                ModifyDate = reportPayment.ModifyDate,
                Version = reportPayment.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.ContractReports.Edit.ContractReportPayment.ChangeStatusToEntered),
                reportPayment.ContractReportId,
                reportPayment.ContractReportPaymentId,
                contractReportPayment,
                response);

            return response;
        }

        private void AssertIsNotDraftOrEnteredFromBeneficiary(ContractReportPayment payment)
        {
            var contractReport = this.contractReportsRepository.Find(payment.ContractReportId);

            if (contractReport.Source == Source.Beneficiary && (payment.Status == ContractReportPaymentStatus.Draft || payment.Status == ContractReportPaymentStatus.Entered))
            {
                throw new UnauthorizedAccessException("Cannot get ContractReportPayment with status 'Draft' or Entered when the ContractReport has source 'Beneficiary'");
            }
        }
    }
}
