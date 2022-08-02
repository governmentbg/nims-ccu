using System;
using System.Collections.Generic;
using System.Linq;
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
    [RoutePrefix("api/contractreg/contracts/{contractGid:guid}/packages/{contractReportGid:guid}/payment")]
    public class ContractReportPayments1Controller : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportService contractReportService;
        private IAccessContext accessContext;

        public ContractReportPayments1Controller(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IContractReportsRepository contractReportsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IProceduresRepository proceduresRepository,
            IContractReportService contractReportService,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportService = contractReportService;
            this.accessContext = accessContext;
        }

        [Route("{contractReportPaymentGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ContractReportPaymentDO GetContractReportPayment(Guid contractGid, Guid contractReportGid, Guid contractReportPaymentGid)
        {
            var reportPayment = this.contractReportPaymentsRepository.Find(contractReportPaymentGid);

            var canEnterErrors = this.contractReportService.CanChangeContractReportPaymentStatusToEntered(reportPayment.ContractReportId);

            var procedureId = this.contractReportsRepository.GetContractReportProcedureId(reportPayment.ContractReportId);

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

            var procedureContractReportDocuments = this.proceduresRepository.FindProcedureReportDocuments(procedureId, procedureContractReportDocumentType.Value);

            return new ContractReportPaymentDO(reportPayment, contractReportHasAdvanceVerificationPayment, procedureContractReportDocuments, canEnterErrors);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO CreateContractReportPayment(Guid contractGid, Guid contractReportGid, ContractReportPaymentType type)
        {
            var reportPayment = this.contractReportService.CreateContractReportPayment(contractReportGid, type);

            var response = new XmlDO
            {
                Gid = reportPayment.Gid,
                ModifyDate = reportPayment.ModifyDate,
                Version = reportPayment.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportPayment.Create),
                reportPayment.ContractReportId,
                null,
                null,
                response);

            return response;
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportPaymentGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO UpdateContractReportPayment(Guid contractGid, Guid contractReportGid, Guid contractReportPaymentGid, XmlDO contractReportPayment)
        {
            var reportPayment = this.contractReportService.UpdateContractReportPayment(contractReportPaymentGid, contractReportPayment.Version, contractReportPayment.Xml);

            var response = new XmlDO
            {
                Gid = reportPayment.Gid,
                ModifyDate = reportPayment.ModifyDate,
                Version = reportPayment.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportPayment.UpdateXml),
                reportPayment.ContractReportId,
                reportPayment.ContractReportPaymentId,
                null,
                null);

            return response;
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportPaymentGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DeleteContractReportPayment(Guid contractGid, Guid contractReportGid, Guid contractReportPaymentGid, VersionDO version)
        {
            var reportPayment = this.contractReportService.DeleteContractReportPayment(contractReportPaymentGid, version.Version);

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportPayment.Delete),
                reportPayment.ContractReportId,
                reportPayment.ContractReportPaymentId,
                null,
                null);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportPaymentGid:guid}/enter")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO EnterContractReportPayment(Guid contractGid, Guid contractReportGid, Guid contractReportPaymentGid, XmlDO contractReportPayment)
        {
            var reportPayment = this.contractReportService.ChangeContractReportPaymentStatus(contractReportPaymentGid, contractReportPayment.Version, ContractReportPaymentStatus.Entered);

            var response = new XmlDO
            {
                Gid = reportPayment.Gid,
                ModifyDate = reportPayment.ModifyDate,
                Version = reportPayment.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportPayment.ChangeStatusToEntered),
                reportPayment.ContractReportId,
                reportPayment.ContractReportPaymentId,
                contractReportPayment,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportPaymentGid:guid}/makedraft")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO DraftContractReportPayment(Guid contractGid, Guid contractReportGid, Guid contractReportPaymentGid, XmlDO contractReportPayment)
        {
            var reportPayment = this.contractReportService.ChangeContractReportPaymentStatus(contractReportPaymentGid, contractReportPayment.Version, ContractReportPaymentStatus.Draft);

            var response = new XmlDO
            {
                Gid = reportPayment.Gid,
                ModifyDate = reportPayment.ModifyDate,
                Version = reportPayment.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportPayment.ChangeStatusToDraft),
                reportPayment.ContractReportId,
                reportPayment.ContractReportPaymentId,
                contractReportPayment,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportPaymentGid:guid}/makeactual")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO ActualContractReportPayment(Guid contractGid, Guid contractReportGid, Guid contractReportPaymentGid, XmlDO contractReportPayment)
        {
            var regId = this.accessContext.ContractRegistrationId;
            var reportPayment = this.contractReportService.ChangeContractReportPaymentStatus(contractReportPaymentGid, contractReportPayment.Version, ContractReportPaymentStatus.Actual, regId);

            var response = new XmlDO
            {
                Gid = reportPayment.Gid,
                ModifyDate = reportPayment.ModifyDate,
                Version = reportPayment.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportPayment.ChangeStatusToActual),
                reportPayment.ContractReportId,
                reportPayment.ContractReportPaymentId,
                contractReportPayment,
                response);

            return response;
        }

        [HttpPost]
        [Route("canCreate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public object CanCreateContractReportPayment(Guid contractGid, Guid contractReportGid, ContractReportPaymentType type)
        {
            var errorList = this.contractReportService.CanCreateContractReportPayment(contractReportGid, type);

            return new ErrorsDO(errorList);
        }
    }
}
