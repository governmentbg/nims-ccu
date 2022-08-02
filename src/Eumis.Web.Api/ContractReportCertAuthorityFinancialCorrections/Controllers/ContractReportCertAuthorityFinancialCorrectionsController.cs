using Eumis.ApplicationServices.Services.ContractReportCertAuthorityFinancialCorrectionService;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
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

namespace Eumis.Web.Api.ContractReportCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/contractReportCertAuthorityFinancialCorrections")]
    public class ContractReportCertAuthorityFinancialCorrectionsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportCertAuthorityFinancialCorrectionsRepository contractReportCertAuthorityFinancialCorrectionsRepository;
        private IContractReportFinancialChecksRepository contractReportFinancialChecksRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;
        private IContractReportCertAuthorityFinancialCorrectionService contractReportCertAuthorityFinancialCorrectionService;

        public ContractReportCertAuthorityFinancialCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportCertAuthorityFinancialCorrectionsRepository contractReportCertAuthorityFinancialCorrectionsRepository,
            IContractReportFinancialChecksRepository contractReportFinancialChecksRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportsRepository contractReportsRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository,
            IContractReportCertAuthorityFinancialCorrectionService contractReportCertAuthorityFinancialCorrectionService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportCertAuthorityFinancialCorrectionsRepository = contractReportCertAuthorityFinancialCorrectionsRepository;
            this.contractReportFinancialChecksRepository = contractReportFinancialChecksRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
            this.contractReportCertAuthorityFinancialCorrectionService = contractReportCertAuthorityFinancialCorrectionService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportCertAuthorityFinancialCorrectionVO> GetContractReportCertAuthorityFinancialCorrections(string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            return this.contractReportCertAuthorityFinancialCorrectionsRepository.GetContractReportCertAuthorityFinancialCorrections(programmeIds, contractRegNum, fromDate, toDate);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportCertAuthorityFinancialCorrections")]
        public IList<ContractReportCertAuthorityFinancialCorrectionVO> GetContractReportCertAuthorityFinancialCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportCertAuthorityFinancialCorrectionsRepository.GetContractReportCertAuthorityFinancialCorrectionsForProjectDossier(contractId);
        }

        [Route("{contractReportCertAuthorityFinancialCorrectionId:int}")]
        public ContractReportCertAuthorityFinancialCorrectionDO GetContractReportCertAuthorityFinancialCorrection(int contractReportCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.View, contractReportCertAuthorityFinancialCorrectionId);

            var certAuthorityfinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(contractReportCertAuthorityFinancialCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(certAuthorityfinancialCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(certAuthorityfinancialCorrection.ContractReportId);

            string username = string.Empty;

            if (certAuthorityfinancialCorrection.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(certAuthorityfinancialCorrection.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportCertAuthorityFinancialCorrectionDO(certAuthorityfinancialCorrection, financial, payment, username);
        }

        [Route("~/api/certReports/{certReportId:int}/financialCertCorrections/{contractReportCertAuthorityFinancialCorrectionId:int}/certAuthorityFinancialCorrection")]
        public ContractReportCertAuthorityFinancialCorrectionDO GetCertReportContractCertAuthorityReportFinancialCorrection(int certReportId, int contractReportCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasFinancialCertCorrectionCSD(certReportId, contractReportCertAuthorityFinancialCorrectionId);

            var certAuthorityfinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(contractReportCertAuthorityFinancialCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(certAuthorityfinancialCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(certAuthorityfinancialCorrection.ContractReportId);

            string username = string.Empty;

            if (certAuthorityfinancialCorrection.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(certAuthorityfinancialCorrection.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportCertAuthorityFinancialCorrectionDO(certAuthorityfinancialCorrection, financial, payment, username);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/certAuthorityFinancialCorrections/{contractReportCertAuthorityFinancialCorrectionId:int}/financialCorrection")]
        public ContractReportCertAuthorityFinancialCorrectionDO GetAnnualAccountReportFinancialCorrectionContractReportFinancialCorrection(int annualAccountReportId, int contractReportCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.CanDo(AnnualAccountReportActions.View, annualAccountReportId);

            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(contractReportCertAuthorityFinancialCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(certAuthorityFinancialCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(certAuthorityFinancialCorrection.ContractReportId);

            string username = string.Empty;

            if (certAuthorityFinancialCorrection.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(certAuthorityFinancialCorrection.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportCertAuthorityFinancialCorrectionDO(certAuthorityFinancialCorrection, financial, payment, username);
        }

        [Route("{contractReportCertAuthorityFinancialCorrectionId:int}/info")]
        public ContractReportCertAuthorityFinancialCorrectionInfoDO GetContractReportInfo(int contractReportCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.View, contractReportCertAuthorityFinancialCorrectionId);

            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(contractReportCertAuthorityFinancialCorrectionId);

            return new ContractReportCertAuthorityFinancialCorrectionInfoDO(certAuthorityFinancialCorrection);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityFinancialCorrection.Create))]
        public object CreateContractReportCertAuthorityFinancialCorrection(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionListActions.Create);

            var newContractReportCertAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionService.CreateContractReportCertAuthorityFinancialCorrection(contractNum, contractReportNum);

            return new { ContractReportCertAuthorityFinancialCorrectionId = newContractReportCertAuthorityFinancialCorrection.ContractReportCertAuthorityFinancialCorrectionId };
        }

        [HttpPut]
        [Route("{contractReportCertAuthorityFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityFinancialCorrection.Edit.BasicData), IdParam = "contractReportCertAuthorityFinancialCorrectionId")]
        public void UpdateContractReportCertAuthorityFinancialCorrection(int contractReportCertAuthorityFinancialCorrectionId, ContractReportCertAuthorityFinancialCorrectionDO contractReportCertAuthorityFinancialCorrection)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            this.contractReportCertAuthorityFinancialCorrectionService.UpdateContractReportCertAuthorityFinancialCorrection(
                contractReportCertAuthorityFinancialCorrection.ContractReportCertAuthorityFinancialCorrectionId,
                contractReportCertAuthorityFinancialCorrection.Version,
                contractReportCertAuthorityFinancialCorrection.CertCorrectionDate,
                contractReportCertAuthorityFinancialCorrection.File != null ? contractReportCertAuthorityFinancialCorrection.File.Key : (Guid?)null,
                contractReportCertAuthorityFinancialCorrection.Notes);
        }

        [HttpDelete]
        [Route("{contractReportCertAuthorityFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityFinancialCorrection.Delete), IdParam = "contractReportCertAuthorityFinancialCorrectionId")]
        public void DeleteContractReportCertAuthorityFinancialCorrection(int contractReportCertAuthorityFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Delete, contractReportCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportCertAuthorityFinancialCorrectionService.DeleteContractReportCertAuthorityFinancialCorrection(contractReportCertAuthorityFinancialCorrectionId, vers);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractReportCertAuthorityFinancialCorrection(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionListActions.Create);

            var errors = this.contractReportCertAuthorityFinancialCorrectionService.CanCreateContractReportCertAuthorityFinancialCorrection(contractNum, contractReportNum);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityFinancialCorrectionId:int}/canDelete")]
        public ErrorsDO CanDeleteContractReportCertAuthorityFinancialCorrection(int contractReportCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            var errors = this.contractReportCertAuthorityFinancialCorrectionService.CanDeleteContractReportCertAuthorityFinancialCorrection(contractReportCertAuthorityFinancialCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityFinancialCorrectionId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityFinancialCorrection.ChangeStatusToEnded), IdParam = "contractReportCertAuthorityFinancialCorrectionId")]
        public void ChangeContractReportChertAuthorityFinancialCorrectionStatusToEnded(int contractReportCertAuthorityFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportCertAuthorityFinancialCorrectionService.ChangeContractReportCertAuthorityFinancialCorrectionStatus(contractReportCertAuthorityFinancialCorrectionId, vers, Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityFinancialCorrectionId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportCertAuthorityFinancialCorrectionStatusToEnded(int contractReportCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            var errors = this.contractReportCertAuthorityFinancialCorrectionService.CanChangeContractReportCertAuthorityFinancialCorrectionStatusToEnded(contractReportCertAuthorityFinancialCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityFinancialCorrectionId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityFinancialCorrection.ChangeStatusToDraft), IdParam = "contractReportCertAuthorityFinancialCorrectionId")]
        public void ChangeContractReportCertAuthorityFinancialCorrectionStatusToDraft(int contractReportCertAuthorityFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportCertAuthorityFinancialCorrectionService.ChangeContractReportCertAuthorityFinancialCorrectionStatus(contractReportCertAuthorityFinancialCorrectionId, vers, Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityFinancialCorrectionId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeContractCertAuthorityReportFinancialCorrectionStatusToDraft(int contractReportCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            var errors = this.contractReportCertAuthorityFinancialCorrectionService.CanChangeContractReportCertAuthorityFinancialCorrectionStatusToDraft(contractReportCertAuthorityFinancialCorrectionId);

            return new ErrorsDO(errors);
        }
    }
}
