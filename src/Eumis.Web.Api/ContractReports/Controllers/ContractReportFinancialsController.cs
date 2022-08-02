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
    [RoutePrefix("api/contractReports/{contractReportId:int}/financials")]
    public class ContractReportFinancialsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportService contractReportService;

        public ContractReportFinancialsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportService = contractReportService;
        }

        [Route("")]
        public IList<ContractReportFinancialVO> GetContractReportFinancials(int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));

            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsNotDraftFromBeneficiary();

            return this.contractReportFinancialsRepository.GetContractReportFinancials(contractReportId);
        }

        [Route("{contractReportFinancialId:int}")]
        public ContractReportFinancialDO GetContractReportFinancial(int contractReportId, int contractReportFinancialId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));

            var financial = this.contractReportFinancialsRepository.FindWithoutIncludes(contractReportFinancialId);

            var contractReport = this.contractReportsRepository.FindWithoutIncludes(contractReportId);

            if (contractReport.Source == Source.Beneficiary && (financial.Status == ContractReportFinancialStatus.Draft || financial.Status == ContractReportFinancialStatus.Entered))
            {
                throw new UnauthorizedAccessException("Cannot get ContractReportFinancial with status 'Draft' or Entered when the ContractReport has source 'Beneficiary'");
            }

            return new ContractReportFinancialDO(financial);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancial.Create), IdParam = "contractReportId")]
        public object CreateContractReportFinancial(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            var newContractReportFinancial = this.contractReportService.CreateContractReportFinancial(contractReportId);

            return new { ContractReportFinancialId = newContractReportFinancial.ContractReportFinancialId };
        }

        [HttpDelete]
        [Route("{contractReportFinancialId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancial.Delete), IdParam = "contractReportId", ChildIdParam = "contractReportFinancialId")]
        public void DeleteContractReportFinancial(int contractReportId, int contractReportFinancialId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.DeleteContractReportFinancial(contractReportId, contractReportFinancialId, vers);
        }

        [HttpPost]
        [Route("{contractReportFinancialId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancial.ChangeStatusToDraft), IdParam = "contractReportId", ChildIdParam = "contractReportFinancialId")]
        public void ChangeContractReportFinancialStatusToDraft(int contractReportId, int contractReportFinancialId, string version)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.EditFinancial, contractReportId));

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportFinancialStatus(contractReportId, contractReportFinancialId, vers, Domain.Contracts.ContractReportFinancialStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportFinancialId:int}/changeStatusToReturned")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancial.ChangeStatusToReturned), IdParam = "contractReportId", ChildIdParam = "contractReportFinancialId")]
        public void ChangeContractReportFinancialStatusToReturned(int contractReportId, int contractReportFinancialId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportFinancialStatusToReturned(contractReportId, contractReportFinancialId, vers, confirm.Note);
        }

        [HttpPost]
        [Route("{contractReportFinancialId:int}/changeStatusToActual")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancial.ChangeStatusToActual), IdParam = "contractReportId", ChildIdParam = "contractReportFinancialId")]
        public void ChangeContractReportFinancialStatusToActual(int contractReportId, int contractReportFinancialId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportFinancialStatus(contractReportId, contractReportFinancialId, vers, Domain.Contracts.ContractReportFinancialStatus.Actual);
        }

        [HttpPost]
        [Route("{contractReportFinancialId:int}/canChangeStatusToReturned")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO CanChangeContractReportFinancialStatusToReturned(int contractReportId, int contractReportFinancialId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportService.CanChangeContractReportFinancialStatusToReturned(contractReportId, contractReportFinancialId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("canCreate")]
        public object CanCreateContractReportFinancial(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var errorList = this.contractReportService.CanCreateContractReportFinancial(contractReportId);

            return new ErrorsDO(errorList);
        }
    }
}
