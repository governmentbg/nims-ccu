using Eumis.ApplicationServices.Services.ContractReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReports/{contractReportId:int}/payments")]
    public class ContractReportPaymentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportService contractReportService;

        public ContractReportPaymentsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportService = contractReportService;
        }

        [Route("")]
        public IList<ContractReportPaymentVO> GetContractReportPayments(int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));

            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsNotDraftFromBeneficiary();

            return this.contractReportPaymentsRepository.GetContractReportPayments(contractReportId);
        }

        [Route("{contractReportPaymentId:int}")]
        public ContractReportPaymentDO GetContractReportPayment(int contractReportId, int contractReportPaymentId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));

            var payment = this.contractReportPaymentsRepository.FindWithoutIncludes(contractReportPaymentId);

            var contractReport = this.contractReportsRepository.FindWithoutIncludes(contractReportId);

            if (contractReport.Source == Source.Beneficiary && (payment.Status == ContractReportPaymentStatus.Draft || payment.Status == ContractReportPaymentStatus.Entered))
            {
                throw new UnauthorizedAccessException("Cannot get ContractReportPayment with status 'Draft' or Entered when the ContractReport has source 'Beneficiary'");
            }

            return new ContractReportPaymentDO(payment);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportPayment.Create), IdParam = "contractReportId")]
        public object CreateContractReportPayment(int contractReportId, ContractReportPaymentType type)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            var newContractReportPayment = this.contractReportService.CreateContractReportPayment(contractReportId, type);

            return new { ContractReportPaymentId = newContractReportPayment.ContractReportPaymentId };
        }

        [HttpDelete]
        [Route("{contractReportPaymentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportPayment.Delete), IdParam = "contractReportId", ChildIdParam = "contractReportPaymentId")]
        public void DeleteContractReportPayment(int contractReportId, int contractReportPaymentId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.DeleteContractReportPayment(contractReportId, contractReportPaymentId, vers);
        }

        [HttpPost]
        [Route("{contractReportPaymentId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportPayment.ChangeStatusToDraft), IdParam = "contractReportId", ChildIdParam = "contractReportPaymentId")]
        public void ChangeContractReportPaymentStatusToDraft(int contractReportId, int contractReportPaymentId, string version)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.EditFinancial, contractReportId));

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportPaymentStatus(contractReportId, contractReportPaymentId, vers, Domain.Contracts.ContractReportPaymentStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportPaymentId:int}/changeStatusToReturned")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportPayment.ChangeStatusToReturned), IdParam = "contractReportId", ChildIdParam = "contractReportPaymentId")]
        public void ChangeContractReportPaymentStatusToReturned(int contractReportId, int contractReportPaymentId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportPaymentStatusToReturned(contractReportId, contractReportPaymentId, vers, confirm.Note);
        }

        [HttpPost]
        [Route("{contractReportPaymentId:int}/changeStatusToActual")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportPayment.ChangeStatusToActual), IdParam = "contractReportId", ChildIdParam = "contractReportPaymentId")]
        public void ChangeContractReportPaymentStatusToActual(int contractReportId, int contractReportPaymentId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportPaymentStatus(contractReportId, contractReportPaymentId, vers, Domain.Contracts.ContractReportPaymentStatus.Actual);
        }

        [HttpPost]
        [Route("{contractReportPaymentId:int}/canChangeStatusToReturned")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO CanChangeContractReportPaymentStatusToReturned(int contractReportId, int contractReportPaymentId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportService.CanChangeContractReportPaymentStatusToReturned(contractReportId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("canCreate")]
        public object CanCreateContractReportPayment(int contractReportId, ContractReportPaymentType type)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var errorList = this.contractReportService.CanCreateContractReportPayment(contractReportId, type);

            return new ErrorsDO(errorList);
        }
    }
}
