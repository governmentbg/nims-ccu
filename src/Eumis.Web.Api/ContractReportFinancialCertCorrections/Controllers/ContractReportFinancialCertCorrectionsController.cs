using Eumis.ApplicationServices.Services.ContractReportFinancialCertCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportFinancialCertCorrections.Repositories;
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

namespace Eumis.Web.Api.ContractReportFinancialCertCorrections.Controllers
{
    [RoutePrefix("api/contractReportFinancialCertCorrections")]
    public class ContractReportFinancialCertCorrectionsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository;
        private IContractReportFinancialChecksRepository contractReportFinancialChecksRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;
        private IContractReportFinancialCertCorrectionService contractReportFinancialCertCorrectionService;

        public ContractReportFinancialCertCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository,
            IContractReportFinancialChecksRepository contractReportFinancialChecksRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportsRepository contractReportsRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository,
            IContractReportFinancialCertCorrectionService contractReportFinancialCertCorrectionService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportFinancialCertCorrectionsRepository = contractReportFinancialCertCorrectionsRepository;
            this.contractReportFinancialChecksRepository = contractReportFinancialChecksRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
            this.contractReportFinancialCertCorrectionService = contractReportFinancialCertCorrectionService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportFinancialCertCorrectionVO> GetContractReportFinancialCertCorrections(string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractReportFinancialCertCorrectionsRepository.GetContractReportFinancialCertCorrections(programmeIds, contractRegNum, fromDate, toDate);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportFinancialCertCorrection")]
        public IList<ContractReportFinancialCertCorrectionVO> GetContractReportFinancialCertCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportFinancialCertCorrectionsRepository.GetContractReportFinancialCertCorrectionsForProjectDossier(contractId);
        }

        [Route("{contractReportFinancialCertCorrectionId:int}")]
        public ContractReportFinancialCertCorrectionDO GetContractReportFinancialCertCorrection(int contractReportFinancialCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.View, contractReportFinancialCertCorrectionId);

            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(contractReportFinancialCertCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(financialCertCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialCertCorrection.ContractReportId);

            string username = string.Empty;

            if (financialCertCorrection.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCertCorrection.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCertCorrectionDO(financialCertCorrection, financial, payment, username);
        }

        [Route("~/api/certReports/{certReportId:int}/financialCertCorrections/{contractReportFinancialCertCorrectionId:int}/financialCertCorrection")]
        public ContractReportFinancialCertCorrectionDO GetCertReportContractReportFinancialCertCorrection(int certReportId, int contractReportFinancialCertCorrectionId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasFinancialCertCorrectionCSD(certReportId, contractReportFinancialCertCorrectionId);

            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(contractReportFinancialCertCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(financialCertCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialCertCorrection.ContractReportId);

            string username = string.Empty;

            if (financialCertCorrection.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCertCorrection.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCertCorrectionDO(financialCertCorrection, financial, payment, username);
        }

        [Route("{contractReportFinancialCertCorrectionId:int}/info")]
        public ContractReportFinancialCertCorrectionInfoDO GetContractReportInfo(int contractReportFinancialCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.View, contractReportFinancialCertCorrectionId);

            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(contractReportFinancialCertCorrectionId);

            return new ContractReportFinancialCertCorrectionInfoDO(financialCertCorrection);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCertCorrection.Create))]
        public object CreateContractReportFinancialCertCorrection(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionListActions.Create);

            var newContractReportFinancialCertCorrection = this.contractReportFinancialCertCorrectionService.CreateContractReportFinancialCertCorrection(contractNum, contractReportNum);

            return new { ContractReportFinancialCertCorrectionId = newContractReportFinancialCertCorrection.ContractReportFinancialCertCorrectionId };
        }

        [HttpPut]
        [Route("{contractReportFinancialCertCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCertCorrection.Edit.BasicData), IdParam = "contractReportFinancialCertCorrectionId")]
        public void UpdateContractReportFinancialCertCorrection(int contractReportFinancialCertCorrectionId, ContractReportFinancialCertCorrectionDO contractReportFinancialCertCorrection)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            this.contractReportFinancialCertCorrectionService.UpdateContractReportFinancialCertCorrection(
                contractReportFinancialCertCorrection.ContractReportFinancialCertCorrectionId,
                contractReportFinancialCertCorrection.Version,
                contractReportFinancialCertCorrection.CertCorrectionDate,
                contractReportFinancialCertCorrection.File != null ? contractReportFinancialCertCorrection.File.Key : (Guid?)null,
                contractReportFinancialCertCorrection.Notes);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCertCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCertCorrection.Delete), IdParam = "contractReportFinancialCertCorrectionId")]
        public void DeleteContractReportFinancialCertCorrection(int contractReportFinancialCertCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Delete, contractReportFinancialCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCertCorrectionService.DeleteContractReportFinancialCertCorrection(contractReportFinancialCertCorrectionId, vers);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractReportFinancialCertCorrection(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionListActions.Create);

            var errors = this.contractReportFinancialCertCorrectionService.CanCreateContractReportFinancialCertCorrection(contractNum, contractReportNum);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialCertCorrectionId:int}/canDelete")]
        public ErrorsDO CanDeleteContractReportFinancialCertCorrection(int contractReportFinancialCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            var errors = this.contractReportFinancialCertCorrectionService.CanDeleteContractReportFinancialCertCorrection(contractReportFinancialCertCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialCertCorrectionId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCertCorrection.ChangeStatusToEnded), IdParam = "contractReportFinancialCertCorrectionId")]
        public void ChangeContractReportFinancialCertCorrectionStatusToEnded(int contractReportFinancialCertCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCertCorrectionService.ChangeContractReportFinancialCertCorrectionStatus(contractReportFinancialCertCorrectionId, vers, Domain.Contracts.ContractReportFinancialCertCorrectionStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportFinancialCertCorrectionId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportFinancialCertCorrectionStatusToEnded(int contractReportFinancialCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            var errors = this.contractReportFinancialCertCorrectionService.CanChangeContractReportFinancialCertCorrectionStatusToEnded(contractReportFinancialCertCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialCertCorrectionId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCertCorrection.ChangeStatusToDraft), IdParam = "contractReportFinancialCertCorrectionId")]
        public void ChangeContractReportFinancialCertCorrectionStatusToDraft(int contractReportFinancialCertCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCertCorrectionService.ChangeContractReportFinancialCertCorrectionStatus(contractReportFinancialCertCorrectionId, vers, Domain.Contracts.ContractReportFinancialCertCorrectionStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportFinancialCertCorrectionId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeContractReportFinancialCertCorrectionStatusToDraft(int contractReportFinancialCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            var errors = this.contractReportFinancialCertCorrectionService.CanChangeContractReportFinancialCertCorrectionStatusToDraft(contractReportFinancialCertCorrectionId);

            return new ErrorsDO(errors);
        }
    }
}
