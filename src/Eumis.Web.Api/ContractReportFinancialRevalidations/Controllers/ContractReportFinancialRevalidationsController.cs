using Eumis.ApplicationServices.Services.ContractReportFinancialRevalidation;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportFinancialRevalidations.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialRevalidations.Controllers
{
    [RoutePrefix("api/contractReportFinancialRevalidations")]
    public class ContractReportFinancialRevalidationsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository;
        private IContractReportFinancialChecksRepository contractReportFinancialChecksRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;
        private IContractReportFinancialRevalidationService contractReportFinancialRevalidationService;

        public ContractReportFinancialRevalidationsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository,
            IContractReportFinancialChecksRepository contractReportFinancialChecksRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportsRepository contractReportsRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository,
            IContractReportFinancialRevalidationService contractReportFinancialRevalidationService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportFinancialRevalidationsRepository = contractReportFinancialRevalidationsRepository;
            this.contractReportFinancialChecksRepository = contractReportFinancialChecksRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
            this.contractReportFinancialRevalidationService = contractReportFinancialRevalidationService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportFinancialRevalidationVO> GetContractReportFinancialRevalidations(string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractReportFinancialRevalidationsRepository.GetContractReportFinancialRevalidations(programmeIds, contractRegNum, fromDate, toDate);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportFinancialRevalidation")]
        public IList<ContractReportFinancialRevalidationVO> GetContractReportFinancialRevalidationsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportFinancialRevalidationsRepository.GetContractReportFinancialRevalidationsForProjectDossier(contractId);
        }

        [Route("{contractReportFinancialRevalidationId:int}")]
        public ContractReportFinancialRevalidationDO GetContractReportFinancialRevalidation(int contractReportFinancialRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.View, contractReportFinancialRevalidationId);

            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.Find(contractReportFinancialRevalidationId);

            var financial = this.contractReportFinancialsRepository.Find(financialRevalidation.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialRevalidation.ContractReportId);

            string username = string.Empty;

            if (financialRevalidation.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialRevalidation.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialRevalidationDO(financialRevalidation, financial, payment, username);
        }

        [Route("~/api/certReports/{certReportId:int}/financialRevalidations/{contractReportFinancialRevalidationId:int}/financialRevalidation")]
        public ContractReportFinancialRevalidationDO GetCertReportContractReportFinancialRevalidation(int certReportId, int contractReportFinancialRevalidationId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasFinancialRevalidationCSD(certReportId, contractReportFinancialRevalidationId);

            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.Find(contractReportFinancialRevalidationId);

            var financial = this.contractReportFinancialsRepository.Find(financialRevalidation.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialRevalidation.ContractReportId);

            string username = string.Empty;

            if (financialRevalidation.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialRevalidation.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialRevalidationDO(financialRevalidation, financial, payment, username);
        }

        [Route("{contractReportFinancialRevalidationId:int}/info")]
        public ContractReportFinancialRevalidationInfoDO GetContractReportInfo(int contractReportFinancialRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.View, contractReportFinancialRevalidationId);

            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.Find(contractReportFinancialRevalidationId);

            return new ContractReportFinancialRevalidationInfoDO(financialRevalidation);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Create))]
        public object CreateContractReportFinancialRevalidation(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationListActions.Create);

            var newContractReportFinancialRevalidation = this.contractReportFinancialRevalidationService.CreateContractReportFinancialRevalidation(contractNum, contractReportNum);

            return new { ContractReportFinancialRevalidationId = newContractReportFinancialRevalidation.ContractReportFinancialRevalidationId };
        }

        [HttpPut]
        [Route("{contractReportFinancialRevalidationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Edit.BasicData), IdParam = "contractReportFinancialRevalidationId")]
        public void UpdateContractReportFinancialRevalidation(int contractReportFinancialRevalidationId, ContractReportFinancialRevalidationDO contractReportFinancialRevalidation)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            this.contractReportFinancialRevalidationService.UpdateContractReportFinancialRevalidation(
                contractReportFinancialRevalidation.ContractReportFinancialRevalidationId,
                contractReportFinancialRevalidation.Version,
                contractReportFinancialRevalidation.RevalidationDate,
                contractReportFinancialRevalidation.File != null ? contractReportFinancialRevalidation.File.Key : (Guid?)null,
                contractReportFinancialRevalidation.Notes);
        }

        [HttpDelete]
        [Route("{contractReportFinancialRevalidationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Delete), IdParam = "contractReportFinancialRevalidationId")]
        public void DeleteContractReportFinancialRevalidation(int contractReportFinancialRevalidationId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Delete, contractReportFinancialRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialRevalidationService.DeleteContractReportFinancialRevalidation(contractReportFinancialRevalidationId, vers);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractReportFinancialRevalidation(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationListActions.Create);

            var errors = this.contractReportFinancialRevalidationService.CanCreateContractReportFinancialRevalidation(contractNum, contractReportNum);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationId:int}/canDelete")]
        public ErrorsDO CanDeleteContractReportFinancialRevalidation(int contractReportFinancialRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            var errors = this.contractReportFinancialRevalidationService.CanDeleteContractReportFinancialRevalidation(contractReportFinancialRevalidationId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.ChangeStatusToEnded), IdParam = "contractReportFinancialRevalidationId")]
        public void ChangeContractReportFinancialRevalidationStatusToEnded(int contractReportFinancialRevalidationId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialRevalidationService.ChangeContractReportFinancialRevalidationStatus(contractReportFinancialRevalidationId, vers, Domain.Contracts.ContractReportFinancialRevalidationStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportFinancialRevalidationStatusToEnded(int contractReportFinancialRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            var errors = this.contractReportFinancialRevalidationService.CanChangeContractReportFinancialRevalidationStatusToEnded(contractReportFinancialRevalidationId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.ChangeStatusToDraft), IdParam = "contractReportFinancialRevalidationId")]
        public void ChangeContractReportFinancialRevalidationStatusToDraft(int contractReportFinancialRevalidationId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialRevalidationService.ChangeContractReportFinancialRevalidationStatus(contractReportFinancialRevalidationId, vers, Domain.Contracts.ContractReportFinancialRevalidationStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeContractReportFinancialRevalidationStatusToDraft(int contractReportFinancialRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            var errors = this.contractReportFinancialRevalidationService.CanChangeContractReportFinancialRevalidationStatusToDraft(contractReportFinancialRevalidationId);

            return new ErrorsDO(errors);
        }
    }
}
