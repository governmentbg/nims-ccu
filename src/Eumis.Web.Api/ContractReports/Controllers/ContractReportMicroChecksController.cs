using Eumis.ApplicationServices.Services.ContractReportMicro;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts.ContractReportMicros;
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
    [RoutePrefix("api/contractReports/{contractReportId:int}/microChecks")]
    public class ContractReportMicroChecksController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportMicroChecksRepository contractReportMicroChecksRepository;
        private IContractReportMicrosRepository contractReportMicrosRepository;
        private IContractReportsRepository contractReportsRepository;
        private IUsersRepository usersRepository;
        private IContractReportMicroService contractReportMicroService;

        public ContractReportMicroChecksController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportMicroChecksRepository contractReportMicroChecksRepository,
            IContractReportMicrosRepository contractReportMicrosRepository,
            IContractReportsRepository contractReportsRepository,
            IUsersRepository usersRepository,
            IContractReportMicroService contractReportMicroService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportMicroChecksRepository = contractReportMicroChecksRepository;
            this.contractReportMicrosRepository = contractReportMicrosRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.usersRepository = usersRepository;
            this.contractReportMicroService = contractReportMicroService;
        }

        [Route("")]
        public IList<ContractReportMicroCheckVO> GetContractReporMicroChecks(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportMicroChecksRepository.GetContractReportMicroChecks(contractReportId);
        }

        [Route("{contractReportMicroCheckId:int}")]
        public ContractReportMicroCheckDO GetContractReportMicroCheck(int contractReportId, int contractReportMicroCheckId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            var microCheck = this.contractReportMicroChecksRepository.Find(contractReportMicroCheckId);

            var micro = this.contractReportMicrosRepository.Find(microCheck.ContractReportMicroId);

            string checkedByUser = null;

            if (microCheck.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(microCheck.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportMicroCheckDO(microCheck, micro, checkedByUser);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicroCheck.Create), IdParam = "contractReportId")]
        public object CreateContractReportMicroCheck(int contractReportId, ContractReportMicroType type)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            var newContractReportMicroCheck = this.contractReportMicroService.CreateContractReportMicroCheck(contractReportId, type);

            return new { ContractReportMicroCheckId = newContractReportMicroCheck.ContractReportMicroCheckId };
        }

        [HttpPut]
        [Route("{contractReportMicroCheckId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicroCheck.Update), IdParam = "contractReportId", ChildIdParam = "contractReportMicroCheckId")]
        public void UpdateContractReportMicroCheck(int contractReportId, int contractReportMicroCheckId, ContractReportMicroCheckDO checkDO)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            this.contractReportMicroService.UpdateContractReportMicroCheck(
                contractReportMicroCheckId,
                checkDO.Version,
                checkDO.Approval,
                checkDO.CheckedDate,
                checkDO.File == null ? (Guid?)null : checkDO.File.Key);
        }

        [HttpDelete]
        [Route("{contractReportMicroCheckId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicroCheck.Delete), IdParam = "contractReportId", ChildIdParam = "contractReportMicroCheckId")]
        public void DeleteContractReportMicroCheck(int contractReportId, int contractReportMicroCheckId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);
            this.contractReportMicroService.DeleteContractReportMicroCheck(contractReportId, contractReportMicroCheckId, vers);
        }

        [HttpPost]
        [Route("{contractReportMicroCheckId:int}/changeStatusToActive")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicroCheck.ChangeStatusToActive), IdParam = "contractReportId", ChildIdParam = "contractReportMicroCheckId")]
        public void ChangeContractReportMicroCheckStatusToActive(int contractReportId, int contractReportMicroCheckId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);
            this.contractReportMicroService.ActivateContractReportMicroCheck(contractReportMicroCheckId, vers);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractReportMicroCheck(int contractReportId, ContractReportMicroType type)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            var errors = this.contractReportMicroService.CanCreateContractReportMicroCheck(contractReportId, type);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportMicroCheckId:int}/canChangeStatusToActive")]
        public ErrorsDO CanChangeContractReportMicroCheckStatusToActive(int contractReportId, int contractReportMicroCheckId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            var check = this.contractReportMicroChecksRepository.Find(contractReportMicroCheckId);

            return new ErrorsDO(check.CanChangeCStatusToActive());
        }
    }
}
