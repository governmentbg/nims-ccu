using Eumis.ApplicationServices.Services.ContractReportFinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
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

namespace Eumis.Web.Api.ContractReportFinancialCorrections.Controllers
{
    [RoutePrefix("api/contractReportFinancialCorrections")]
    public class ContractReportFinancialCorrectionsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository;
        private IContractReportFinancialChecksRepository contractReportFinancialChecksRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;
        private IContractReportFinancialCorrectionService contractReportFinancialCorrectionService;

        public ContractReportFinancialCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository,
            IContractReportFinancialChecksRepository contractReportFinancialChecksRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportsRepository contractReportsRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository,
            IContractReportFinancialCorrectionService contractReportFinancialCorrectionService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportFinancialCorrectionsRepository = contractReportFinancialCorrectionsRepository;
            this.contractReportFinancialChecksRepository = contractReportFinancialChecksRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
            this.contractReportFinancialCorrectionService = contractReportFinancialCorrectionService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportFinancialCorrectionVO> GetContractReportFinancialCorrections(string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractReportFinancialCorrectionsRepository.GetContractReportFinancialCorrections(programmeIds, this.accessContext.UserId, contractRegNum, fromDate, toDate);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportFinancialCorrection")]
        public IList<ContractReportFinancialCorrectionVO> GetContractReportFinancialCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportFinancialCorrectionsRepository.GetContractReportFinancialCorrectionsForProjectDossier(contractId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportCertifiedAmountFinancialCorrections")]
        public IList<ContractReportCertifiedAmountFinancialCorrectionVO> GetContractReportCertifiedAmountFinancialCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportFinancialCorrectionsRepository.GetContractReportCertifiedAmountFinancialCorrectionsForProjectDossier(contractId);
        }

        [Route("~/api/financialCorrections/{financialCorrectionId:int}/contractReportCorrections")]
        public IList<ContractReportFinancialCorrectionVO> GetFinancialCorrectionContractReportFinancialCorrections(int financialCorrectionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.View, financialCorrectionId);

            return this.contractReportFinancialCorrectionsRepository.GetFinancialCorrectionContractReportFinancialCorrections(financialCorrectionId);
        }

        [Route("{contractReportFinancialCorrectionId:int}")]
        public ContractReportFinancialCorrectionDO GetContractReportFinancialCorrection(int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.View, contractReportFinancialCorrectionId);

            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(contractReportFinancialCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(financialCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialCorrection.ContractReportId);

            string username = string.Empty;

            if (financialCorrection.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCorrection.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCorrectionDO(financialCorrection, financial, payment, username);
        }

        [Route("~/api/certReports/{certReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/financialCorrection")]
        public ContractReportFinancialCorrectionDO GetCertReportFinancialCorrectionContractReportFinancialCorrection(int certReportId, int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasFinancialCorrectionCSD(certReportId, contractReportFinancialCorrectionId);

            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(contractReportFinancialCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(financialCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialCorrection.ContractReportId);

            string username = string.Empty;

            if (financialCorrection.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCorrection.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCorrectionDO(financialCorrection, financial, payment, username);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/financialCorrection")]
        public ContractReportFinancialCorrectionDO GetAnnualAccountReportFinancialCorrectionContractReportFinancialCorrection(int annualAccountReportId, int contractReportFinancialCorrectionId)
        {
            this.authorizer.CanDo(AnnualAccountReportActions.View, annualAccountReportId);

            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(contractReportFinancialCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(financialCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialCorrection.ContractReportId);

            string username = string.Empty;

            if (financialCorrection.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCorrection.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCorrectionDO(financialCorrection, financial, payment, username);
        }

        [Route("{contractReportFinancialCorrectionId:int}/info")]
        public ContractReportFinancialCorrectionInfoDO GetContractReportInfo(int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.View, contractReportFinancialCorrectionId);

            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(contractReportFinancialCorrectionId);

            return new ContractReportFinancialCorrectionInfoDO(financialCorrection);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Create))]
        public object CreateContractReportFinancialCorrection(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionListActions.Create);

            var newContractReportFinancialCorrection = this.contractReportFinancialCorrectionService.CreateContractReportFinancialCorrection(contractNum, contractReportNum);

            return new { ContractReportFinancialCorrectionId = newContractReportFinancialCorrection.ContractReportFinancialCorrectionId };
        }

        [HttpPut]
        [Route("{contractReportFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Edit.BasicData), IdParam = "contractReportFinancialCorrectionId")]
        public void UpdateContractReportFinancialCorrection(int contractReportFinancialCorrectionId, ContractReportFinancialCorrectionDO contractReportFinancialCorrection)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            this.contractReportFinancialCorrectionService.UpdateContractReportFinancialCorrection(
                contractReportFinancialCorrection.ContractReportFinancialCorrectionId,
                contractReportFinancialCorrection.Version,
                contractReportFinancialCorrection.CorrectionDate,
                contractReportFinancialCorrection.File != null ? contractReportFinancialCorrection.File.Key : (Guid?)null,
                contractReportFinancialCorrection.Notes);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Delete), IdParam = "contractReportFinancialCorrectionId")]
        public void DeleteContractReportFinancialCorrection(int contractReportFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Delete, contractReportFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCorrectionService.DeleteContractReportFinancialCorrection(contractReportFinancialCorrectionId, vers);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractReportFinancialCorrection(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionListActions.Create);

            var errors = this.contractReportFinancialCorrectionService.CanCreateContractReportFinancialCorrection(contractNum, contractReportNum);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionId:int}/canDelete")]
        public ErrorsDO CanDeleteContractReportFinancialCorrection(int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            var errors = this.contractReportFinancialCorrectionService.CanDeleteContractReportFinancialCorrection(contractReportFinancialCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.ChangeStatusToEnded), IdParam = "contractReportFinancialCorrectionId")]
        public void ChangeContractReportFinancialCorrectionStatusToEnded(int contractReportFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCorrectionService.ChangeContractReportFinancialCorrectionStatus(contractReportFinancialCorrectionId, vers, Domain.Contracts.ContractReportFinancialCorrectionStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportFinancialCorrectionStatusToEnded(int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            var errors = this.contractReportFinancialCorrectionService.CanChangeContractReportFinancialCorrectionStatusToEnded(contractReportFinancialCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.ChangeStatusToDraft), IdParam = "contractReportFinancialCorrectionId")]
        public void ChangeContractReportFinancialCorrectionStatusToDraft(int contractReportFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCorrectionService.ChangeContractReportFinancialCorrectionStatus(contractReportFinancialCorrectionId, vers, Domain.Contracts.ContractReportFinancialCorrectionStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeContractReportFinancialCorrectionStatusToDraft(int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            var errors = this.contractReportFinancialCorrectionService.CanChangeContractReportFinancialCorrectionStatusToDraft(contractReportFinancialCorrectionId);

            return new ErrorsDO(errors);
        }
    }
}
