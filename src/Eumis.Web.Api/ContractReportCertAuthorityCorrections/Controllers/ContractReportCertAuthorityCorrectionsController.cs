using Eumis.ApplicationServices.Services.ContractReportCertAuthorityCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportCertAuthorityCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Counters;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/contractReportCertAuthorityCorrections")]
    public class ContractReportCertAuthorityCorrectionsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICountersRepository countersRepository;
        private IUsersRepository usersRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportPaymentChecksRepository contractReportPaymentChecksRepository;
        private IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository;
        private IContractReportCertAuthorityCorrectionService contractReportCertAuthorityCorrectionService;

        public ContractReportCertAuthorityCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            IUsersRepository usersRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository,
            IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository,
            IContractReportCertAuthorityCorrectionService contractReportCertAuthorityCorrectionService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.countersRepository = countersRepository;
            this.usersRepository = usersRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportPaymentChecksRepository = contractReportPaymentChecksRepository;
            this.contractReportCertAuthorityCorrectionsRepository = contractReportCertAuthorityCorrectionsRepository;
            this.contractReportCertAuthorityCorrectionService = contractReportCertAuthorityCorrectionService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportCertAuthorityCorrectionVO> GetContractReportCertAuthorityCorrections(
            int? programmeId = null,
            ContractReportCertAuthorityCorrectionType? type = null,
            ContractReportCertAuthorityCorrectionStatus? status = null)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(id => id == programmeId).ToArray();
            }

            return this.contractReportCertAuthorityCorrectionsRepository.GetContractReportCertAuthorityCorrections(programmeIds, type, status);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportCertAuthorityCorrections")]
        public IList<ContractReportCertAuthorityCorrectionVO> GetContractReportCertAuthorityCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportCertAuthorityCorrectionsRepository.GetContractReportCertAuthorityCorrectionsForProjectDossier(contractId);
        }

        [HttpGet]
        [Route("new")]
        public NewContractReportCertAuthorityCorrectionDO NewContractReportCertAuthorityCorrection()
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionListActions.Create);

            return new NewContractReportCertAuthorityCorrectionDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityCorrections.Create))]
        public object CreateContractReportCertAuthorityCorrection(NewContractReportCertAuthorityCorrectionDO newDoc)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionListActions.Create);

            var newContractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionService.CreateContractReportCertAuthorityCorrection(
                this.accessContext.UserId,
                newDoc.Type.Value,
                newDoc.Sign.Value,
                newDoc.Date.Value,
                newDoc.ProgrammeId,
                newDoc.ProgrammePriorityId,
                newDoc.ProcedureId,
                newDoc.ContractId,
                newDoc.ContractReportPaymentId);

            return new { ContractReportCertAuthorityCorrectionId = newContractReportCertAuthorityCorrection.ContractReportCertAuthorityCorrectionId };
        }

        [Route("{contractReportCertAuthorityCorrectionId:int}/info")]
        public ContractReportCertAuthorityCorrectionInfoVO GetContractReportCertAuthorityCorrectionInfo(int contractReportCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.View, contractReportCertAuthorityCorrectionId);

            return this.contractReportCertAuthorityCorrectionsRepository.GetInfo(contractReportCertAuthorityCorrectionId);
        }

        [Route("{contractReportCertAuthorityCorrectionId:int}/data")]
        public ContractReportCertAuthorityCorrectionBasicDataVO GetContractReportCertAuthorityCorrectionBasicData(int contractReportCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.View, contractReportCertAuthorityCorrectionId);

            return this.contractReportCertAuthorityCorrectionsRepository.GetBasicData(contractReportCertAuthorityCorrectionId);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/certAuthorityCorrections/{contractReportCertAuthorityCorrectionId:int}/certAuthorityCorrection")]
        public ContractReportCertAuthorityCorrectionDO GetAnnualAccountReportFinancialCorrectionContractReportCorrection(int annualAccountReportId, int contractReportCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertifiedCorrection(annualAccountReportId, contractReportCertAuthorityCorrectionId);

            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.Find(contractReportCertAuthorityCorrectionId);

            if (contractReportCertAuthorityCorrection.Type == ContractReportCertAuthorityCorrectionType.PaymentCertified)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportCertAuthorityCorrection.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string checkedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportCertAuthorityCorrectionDO(contractReportCertAuthorityCorrection, payment, paymentCheck, checkedByUser);
            }

            return new ContractReportCertAuthorityCorrectionDO(contractReportCertAuthorityCorrection);
        }

        [Route("{contractReportCertAuthorityCorrectionId:int}")]
        public ContractReportCertAuthorityCorrectionDO GetContractReportCertAuthorityCorrectionData(int contractReportCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.View, contractReportCertAuthorityCorrectionId);

            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.Find(contractReportCertAuthorityCorrectionId);

            if (contractReportCertAuthorityCorrection.Type == ContractReportCertAuthorityCorrectionType.PaymentCertified)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportCertAuthorityCorrection.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string checkedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportCertAuthorityCorrectionDO(contractReportCertAuthorityCorrection, payment, paymentCheck, checkedByUser);
            }

            return new ContractReportCertAuthorityCorrectionDO(contractReportCertAuthorityCorrection);
        }

        [HttpPut]
        [Route("{contractReportCertAuthorityCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityCorrections.Edit.ContractReportCertAuthorityCorrectionData), IdParam = "contractReportCertAuthorityCorrectionId")]
        public void UpdateContractReportCertAuthorityCorrectionData(int contractReportCertAuthorityCorrectionId, ContractReportCertAuthorityCorrectionDO contractReportCertAuthorityCorrectionDO)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.FindForUpdate(contractReportCertAuthorityCorrectionId, contractReportCertAuthorityCorrectionDO.Version);

            contractReportCertAuthorityCorrection.UpdateData(
                contractReportCertAuthorityCorrectionDO.Sign.Value,
                contractReportCertAuthorityCorrectionDO.Date.Value,
                contractReportCertAuthorityCorrectionDO.Description,
                contractReportCertAuthorityCorrectionDO.Reason,
                contractReportCertAuthorityCorrectionDO.CertifiedEuAmount,
                contractReportCertAuthorityCorrectionDO.CertifiedBgAmount,
                contractReportCertAuthorityCorrectionDO.CertifiedCrossAmount,
                contractReportCertAuthorityCorrectionDO.CertifiedSelfAmount);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityCorrectionId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityCorrections.ChangeStatusToEntered), IdParam = "contractReportCertAuthorityCorrectionId")]
        public void EnterContractReportCertAuthorityCorrection(int contractReportCertAuthorityCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.FindForUpdate(contractReportCertAuthorityCorrectionId, vers);

            if (contractReportCertAuthorityCorrection.IsActivated)
            {
                contractReportCertAuthorityCorrection.ChangeStatusToEntered(this.accessContext.UserId);
            }
            else
            {
                this.countersRepository.CreateContractReportCertAuthorityCorrectionCounter(contractReportCertAuthorityCorrection.ProgrammeId);
                var regNum = this.countersRepository.GetNextContractReportCertAuthorityCorrectionNumber(contractReportCertAuthorityCorrection.ProgrammeId);

                contractReportCertAuthorityCorrection.ChangeStatusToEntered(this.accessContext.UserId, regNum);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityCorrectionId:int}/canEnter")]
        public ErrorsDO CanEnterContractReportCertAuthorityCorrection(int contractReportCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            var errors = this.contractReportCertAuthorityCorrectionService.CanEnterContractReportCertAuthorityCorrection(contractReportCertAuthorityCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityCorrectionId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityCorrections.ChangeStatusToDraft), IdParam = "contractReportCertAuthorityCorrectionId")]
        public void MakeDraft(int contractReportCertAuthorityCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.FindForUpdate(contractReportCertAuthorityCorrectionId, vers);

            contractReportCertAuthorityCorrection.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityCorrectionId:int}/canSetToDraft")]
        public ErrorsDO CanMakeDraft(int contractReportCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            var errors = this.contractReportCertAuthorityCorrectionService.CanMakeDraftContractReportCertAuthorityCorrection(contractReportCertAuthorityCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityCorrectionId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityCorrections.ChangeStatusToRemoved), IdParam = "contractReportCertAuthorityCorrectionId")]
        public void MakeRemoved(int contractReportCertAuthorityCorrectionId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.FindForUpdate(contractReportCertAuthorityCorrectionId, vers);

            contractReportCertAuthorityCorrection.ChangeStatusToDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractReportCertAuthorityCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityCorrections.Delete), IdParam = "contractReportCertAuthorityCorrectionId")]
        public void Delete(int contractReportCertAuthorityCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.FindForUpdate(contractReportCertAuthorityCorrectionId, vers);

            this.contractReportCertAuthorityCorrectionsRepository.Remove(contractReportCertAuthorityCorrection);

            this.unitOfWork.Save();
        }
    }
}
