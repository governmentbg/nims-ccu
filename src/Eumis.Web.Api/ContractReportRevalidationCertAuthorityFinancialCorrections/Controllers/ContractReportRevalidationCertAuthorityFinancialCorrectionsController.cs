using Eumis.ApplicationServices.Services.ContractReportRevalidationCertAuthorityFinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Permissions;
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

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/contractReportRevalidationCertAuthorityFinancialCorrections")]
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionsController : ApiController
    {
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository contractReportRevalidationCertAuthorityFinancialCorrectionsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;
        private IContractReportRevalidationCertAuthorityFinancialCorrectionService contractReportRevalidationCertAuthorityFinancialCorrectionService;

        public ContractReportRevalidationCertAuthorityFinancialCorrectionsController(
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository contractReportRevalidationCertAuthorityFinancialCorrectionsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository,
            IContractReportRevalidationCertAuthorityFinancialCorrectionService contractReportRevalidationCertAuthorityFinancialCorrectionService)
        {
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository = contractReportRevalidationCertAuthorityFinancialCorrectionsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
            this.contractReportRevalidationCertAuthorityFinancialCorrectionService = contractReportRevalidationCertAuthorityFinancialCorrectionService;
        }

        [Route("")]
        public IList<ContractReportRevalidationCertAuthorityFinancialCorrectionVO> GetContractReportRevalidationCertAuthorityFinancialCorrections(string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            return this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.GetContractReportRevalidationCertAuthorityFinancialCorrections(programmeIds, contractRegNum, fromDate, toDate);
        }

        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}")]
        public ContractReportRevalidationCertAuthorityFinancialCorrectionDO GetContractReportRevalidationCertAuthorityFinancialCorrection(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.View, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            var certAuthorityfinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Find(contractReportRevalidationCertAuthorityFinancialCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(certAuthorityfinancialCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(certAuthorityfinancialCorrection.ContractReportId);

            return new ContractReportRevalidationCertAuthorityFinancialCorrectionDO(certAuthorityfinancialCorrection, financial, payment);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/certAuthorityRevalidationFinancialCorrections/{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/financialCorrection")]
        public ContractReportRevalidationCertAuthorityFinancialCorrectionDO GetAnnualAccountReportContractReportRevalidationCertAuthorityFinancialCorrection(int annualAccountReportId, int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.CanDo(AnnualAccountReportActions.View, annualAccountReportId);

            var certAuthorityfinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Find(contractReportRevalidationCertAuthorityFinancialCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(certAuthorityfinancialCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(certAuthorityfinancialCorrection.ContractReportId);

            return new ContractReportRevalidationCertAuthorityFinancialCorrectionDO(certAuthorityfinancialCorrection, financial, payment);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityFinancialCorrection.Create))]
        public object CreateContractReportRevalidationCertAuthorityFinancialCorrection(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionListActions.Create);

            var newContractReportRevalidationCertAuthorityFinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionService.CreateContractReportRevalidationCertAuthorityFinancialCorrection(contractNum, contractReportNum);

            return new { ContractReportRevalidationCertAuthorityFinancialCorrectionId = newContractReportRevalidationCertAuthorityFinancialCorrection.ContractReportRevalidationCertAuthorityFinancialCorrectionId };
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractReportRevalidationCertAuthorityFinancialCorrection(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionListActions.Create);

            var errors = this.contractReportRevalidationCertAuthorityFinancialCorrectionService.CanCreateContractReportRevalidationCertAuthorityFinancialCorrection(contractNum, contractReportNum);

            return new ErrorsDO(errors);
        }

        [HttpDelete]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityFinancialCorrection.Delete), IdParam = "contractReportRevalidationCertAuthorityFinancialCorrectionId")]
        public void DeleteContractReportRevalidationCertAuthorityFinancialCorrection(int contractReportRevalidationCertAuthorityFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Delete, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionService.DeleteContractReportRevalidationCertAuthorityFinancialCorrection(contractReportRevalidationCertAuthorityFinancialCorrectionId, vers);
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/canDelete")]
        public ErrorsDO CanDeleteContractReportRevalidationCertAuthorityFinancialCorrection(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            var errors = this.contractReportRevalidationCertAuthorityFinancialCorrectionService.CanDeleteContractReportRevalidationCertAuthorityFinancialCorrection(contractReportRevalidationCertAuthorityFinancialCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityFinancialCorrection.ChangeStatusToEnded), IdParam = "contractReportRevalidationCertAuthorityFinancialCorrectionId")]
        public void ChangeContractReportChertAuthorityFinancialCorrectionStatusToEnded(int contractReportRevalidationCertAuthorityFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionService.ChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatus(contractReportRevalidationCertAuthorityFinancialCorrectionId, vers, Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrectionStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatusToEnded(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            var errors = this.contractReportRevalidationCertAuthorityFinancialCorrectionService.CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatusToEnded(contractReportRevalidationCertAuthorityFinancialCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityFinancialCorrection.ChangeStatusToDraft), IdParam = "contractReportRevalidationCertAuthorityFinancialCorrectionId")]
        public void ChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatusToDraft(int contractReportRevalidationCertAuthorityFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionService.ChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatus(contractReportRevalidationCertAuthorityFinancialCorrectionId, vers, Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrectionStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeContractCertAuthorityReportFinancialCorrectionStatusToDraft(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            var errors = this.contractReportRevalidationCertAuthorityFinancialCorrectionService.CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatusToDraft(contractReportRevalidationCertAuthorityFinancialCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPut]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityFinancialCorrection.Edit.BasicData), IdParam = "contractReportRevalidationCertAuthorityFinancialCorrectionId")]
        public void UpdateContractReportRevalidationCertAuthorityFinancialCorrection(int contractReportRevalidationCertAuthorityFinancialCorrectionId, ContractReportRevalidationCertAuthorityFinancialCorrectionDO contractReportRevalidationCertAuthorityFinancialCorrection)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionService.UpdateContractReportRevalidationCertAuthorityFinancialCorrection(
                contractReportRevalidationCertAuthorityFinancialCorrection.ContractReportRevalidationCertAuthorityFinancialCorrectionId,
                contractReportRevalidationCertAuthorityFinancialCorrection.Version,
                contractReportRevalidationCertAuthorityFinancialCorrection.CertCorrectionDate,
                contractReportRevalidationCertAuthorityFinancialCorrection.File != null ? contractReportRevalidationCertAuthorityFinancialCorrection.File.Key : (Guid?)null,
                contractReportRevalidationCertAuthorityFinancialCorrection.Notes);
        }

        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/info")]
        public ContractReportRevalidationCertAuthorityFinancialCorrectionInfoDO GetContractReportInfo(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.View, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            var certAuthorityFinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Find(contractReportRevalidationCertAuthorityFinancialCorrectionId);

            return new ContractReportRevalidationCertAuthorityFinancialCorrectionInfoDO(certAuthorityFinancialCorrection);
        }
    }
}
