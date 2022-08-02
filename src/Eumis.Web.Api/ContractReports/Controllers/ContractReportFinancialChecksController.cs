using Eumis.ApplicationServices.Services.ContractReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Users.Repositories;
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
    [RoutePrefix("api/contractReports/{contractReportId:int}/financialChecks")]
    public class ContractReportFinancialChecksController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportFinancialChecksRepository contractReportFinancialChecksRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IUsersRepository usersRepository;
        private IContractReportService contractReportService;

        public ContractReportFinancialChecksController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportFinancialChecksRepository contractReportFinancialChecksRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportsRepository contractReportsRepository,
            IUsersRepository usersRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportFinancialChecksRepository = contractReportFinancialChecksRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.usersRepository = usersRepository;
            this.contractReportService = contractReportService;
        }

        [Route("")]
        public IList<ContractReportFinancialCheckVO> GetContractReportFinancialChecks(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportFinancialChecksRepository.GetContractReportFinancialChecks(contractReportId);
        }

        [Route("{contractReportFinancialCheckId:int}")]
        public ContractReportFinancialCheckDO GetContractReportFinancialCheck(int contractReportId, int contractReportFinancialCheckId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            var financialCheck = this.contractReportFinancialChecksRepository.Find(contractReportFinancialCheckId);

            var financial = this.contractReportFinancialsRepository.Find(financialCheck.ContractReportFinancialId);

            string checkedByUser = string.Empty;

            if (financialCheck.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCheck.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCheckDO(financialCheck, financial, checkedByUser);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCheck.Create), IdParam = "contractReportId")]
        public object CreateContractReportFinancialCheck(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var newContractReportFinancialCheck = this.contractReportService.CreateContractReportFinancialCheck(contractReportId);

            return new { ContractReportFinancialCheckId = newContractReportFinancialCheck.ContractReportFinancialCheckId };
        }

        [HttpPut]
        [Route("{contractReportFinancialCheckId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCheck.Update), IdParam = "contractReportId", ChildIdParam = "contractReportFinancialCheckId")]
        public void UpdateContractReportFinancialCheck(int contractReportId, int contractReportFinancialCheckId, ContractReportFinancialCheckDO contractReportFinancialCheck)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            this.contractReportService.UpdateContractReportFinancialCheck(
                contractReportFinancialCheckId,
                contractReportFinancialCheck.Version,
                contractReportFinancialCheck.Approval,
                contractReportFinancialCheck.File != null ? contractReportFinancialCheck.File.Key : (Guid?)null,
                contractReportFinancialCheck.CheckedDate);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCheckId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCheck.Delete), IdParam = "contractReportId", ChildIdParam = "contractReportFinancialCheckId")]
        public void DeleteContractReportFinancialCheck(int contractReportId, int contractReportFinancialCheckId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.DeleteContractReportFinancialCheck(contractReportId, contractReportFinancialCheckId, vers);
        }

        [HttpPost]
        [Route("{contractReportFinancialCheckId:int}/changeStatusToActive")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCheck.ChangeStatusToActive), IdParam = "contractReportId", ChildIdParam = "contractReportFinancialCheckId")]
        public void ChangeContractReportFinancialCheckStatusToActive(int contractReportId, int contractReportFinancialCheckId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportFinancialCheckStatus(contractReportId, contractReportFinancialCheckId, vers, Domain.Contracts.ContractReportFinancialCheckStatus.Active);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractReportFinancialCheck(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportService.CanCreateContractReportFinancialCheck(contractReportId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialCheckId:int}/canChangeStatusToActive")]
        public ErrorsDO CanChangeContractReportFinancialCheckStatusToActive(int contractReportId, int contractReportFinancialCheckId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportService.CanChangeContractReportFinancialCheckStatusToActive(contractReportFinancialCheckId);

            return new ErrorsDO(errors);
        }
    }
}
