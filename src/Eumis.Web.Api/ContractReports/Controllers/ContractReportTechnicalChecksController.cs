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
    [RoutePrefix("api/contractReports/{contractReportId:int}/technicalChecks")]
    public class ContractReportTechnicalChecksController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportTechnicalChecksRepository contractReportTechnicalChecksRepository;
        private IContractReportTechnicalsRepository contractReportTechnicalsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IUsersRepository usersRepository;
        private IContractReportService contractReportService;

        public ContractReportTechnicalChecksController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportTechnicalChecksRepository contractReportTechnicalChecksRepository,
            IContractReportTechnicalsRepository contractReportTechnicalsRepository,
            IContractReportsRepository contractReportsRepository,
            IUsersRepository usersRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportTechnicalChecksRepository = contractReportTechnicalChecksRepository;
            this.contractReportTechnicalsRepository = contractReportTechnicalsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.usersRepository = usersRepository;
            this.contractReportService = contractReportService;
        }

        [Route("")]
        public IList<ContractReportTechnicalCheckVO> GetContractReportTechnicalChecks(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportTechnicalChecksRepository.GetContractReportTechnicalChecks(contractReportId);
        }

        [Route("{contractReportTechnicalCheckId:int}")]
        public ContractReportTechnicalCheckDO GetContractReportTechnicalCheck(int contractReportId, int contractReportTechnicalCheckId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            var technicalCheck = this.contractReportTechnicalChecksRepository.Find(contractReportTechnicalCheckId);

            var technical = this.contractReportTechnicalsRepository.Find(technicalCheck.ContractReportTechnicalId);

            string checkedByUser = string.Empty;

            if (technicalCheck.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(technicalCheck.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportTechnicalCheckDO(technicalCheck, technical, checkedByUser);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnicalCheck.Create), IdParam = "contractReportId")]
        public object CreateContractReportTechnicalCheck(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            var newContractReportTechnicalCheck = this.contractReportService.CreateContractReportTechnicalCheck(contractReportId);

            return new { ContractReportTechnicalCheckId = newContractReportTechnicalCheck.ContractReportTechnicalCheckId };
        }

        [HttpPut]
        [Route("{contractReportTechnicalCheckId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnicalCheck.Update), IdParam = "contractReportId", ChildIdParam = "contractReportTechnicalCheckId")]
        public void UpdateContractReportTechnicalCheck(int contractReportId, int contractReportTechnicalCheckId, ContractReportTechnicalCheckDO contractReportTechnicalCheck)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            this.contractReportService.UpdateContractReportTechnicalCheck(
                contractReportTechnicalCheckId,
                contractReportTechnicalCheck.Version,
                contractReportTechnicalCheck.Approval,
                contractReportTechnicalCheck.File != null ? contractReportTechnicalCheck.File.Key : (Guid?)null,
                contractReportTechnicalCheck.CheckedDate);
        }

        [HttpDelete]
        [Route("{contractReportTechnicalCheckId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnicalCheck.Delete), IdParam = "contractReportId", ChildIdParam = "contractReportTechnicalCheckId")]
        public void DeleteContractReportTechnicalCheck(int contractReportId, int contractReportTechnicalCheckId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.DeleteContractReportTechnicalCheck(contractReportId, contractReportTechnicalCheckId, vers);
        }

        [HttpPost]
        [Route("{contractReportTechnicalCheckId:int}/changeStatusToActive")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnicalCheck.ChangeStatusToActive), IdParam = "contractReportId", ChildIdParam = "contractReportTechnicalCheckId")]
        public void ChangeContractReportTechnicalCheckStatusToActive(int contractReportId, int contractReportTechnicalCheckId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportTechnicalCheckStatus(contractReportId, contractReportTechnicalCheckId, vers, Domain.Contracts.ContractReportTechnicalCheckStatus.Active);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractReportTechnicalCheck(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            var errors = this.contractReportService.CanCreateContractReportTechnicalCheck(contractReportId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportTechnicalCheckId:int}/canChangeStatusToActive")]
        public ErrorsDO CanChangeContractReportTechnicalCheckStatusToActive(int contractReportId, int contractReportTechnicalCheckId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            var errors = this.contractReportService.CanChangeContractReportTechnicalCheckStatusToActive(contractReportTechnicalCheckId);

            return new ErrorsDO(errors);
        }
    }
}
