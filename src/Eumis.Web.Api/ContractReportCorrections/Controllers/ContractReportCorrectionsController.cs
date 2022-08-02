using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.ContractReportCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportCorrections.Repositories;
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

namespace Eumis.Web.Api.ContractReportCorrections.Controllers
{
    [RoutePrefix("api/contractReportCorrections")]
    public class ContractReportCorrectionsController : ApiController
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
        private IContractReportCorrectionsRepository contractReportCorrectionsRepository;
        private IContractReportCorrectionService contractReportCorrectionService;
        private ICertReportCheckService certReportCheckService;

        public ContractReportCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            IUsersRepository usersRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository,
            IContractReportCorrectionsRepository contractReportCorrectionsRepository,
            IContractReportCorrectionService contractReportCorrectionService,
            ICertReportCheckService certReportCheckService,
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
            this.contractReportCorrectionsRepository = contractReportCorrectionsRepository;
            this.contractReportCorrectionService = contractReportCorrectionService;
            this.certReportCheckService = certReportCheckService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportCorrectionVO> GetContractReportCorrections(
            int? programmeId = null,
            ContractReportCorrectionType? type = null,
            ContractReportCorrectionStatus? status = null)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(id => id == programmeId).ToArray();
            }

            return this.contractReportCorrectionsRepository.GetContractReportCorrections(programmeIds, this.accessContext.UserId, type, status);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportCorrection")]
        public IList<ContractReportCorrectionVO> GetContractReportCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportCorrectionsRepository.GetContractReportCorrectionsForProjectDossier(contractId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportCertifiedAmountCorrections")]
        public IList<ContractReportCertifiedAmountCorrectionVO> GetContractReportCertifiedAmountCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportCorrectionsRepository.GetContractReportCertifiedAmountCorrectionsForProjectDossier(contractId);
        }

        [HttpGet]
        [Route("new")]
        public NewContractReportCorrectionDO NewContractReportCorrection()
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionListActions.Create);

            return new NewContractReportCorrectionDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.Create))]
        public object CreateContractReportCorrection(NewContractReportCorrectionDO newDoc)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionListActions.Create);

            var newContractReportCorrection = this.contractReportCorrectionService.CreateContractReportCorrection(
                this.accessContext.UserId,
                newDoc.Type.Value,
                newDoc.Sign.Value,
                newDoc.Date.Value,
                newDoc.ProgrammeId,
                newDoc.ProgrammePriorityId,
                newDoc.ProcedureId,
                newDoc.ContractId,
                newDoc.ContractReportPaymentId);

            return new { ContractReportCorrectionId = newContractReportCorrection.ContractReportCorrectionId };
        }

        [Route("{contractReportCorrectionId:int}/info")]
        public ContractReportCorrectionInfoVO GetContractReportCorrectionInfo(int contractReportCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.View, contractReportCorrectionId);

            return this.contractReportCorrectionsRepository.GetInfo(contractReportCorrectionId);
        }

        [Route("{contractReportCorrectionId:int}/data")]
        public ContractReportCorrectionBasicDataVO GetContractReportCorrectionBasicData(int contractReportCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.View, contractReportCorrectionId);

            return this.contractReportCorrectionsRepository.GetBasicData(contractReportCorrectionId);
        }

        [Route("{contractReportCorrectionId:int}")]
        public ContractReportCorrectionDO GetContractReportCorrectionData(int contractReportCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.View, contractReportCorrectionId);

            var contractReportCorrection = this.contractReportCorrectionsRepository.Find(contractReportCorrectionId);

            var contractReportCorrectionElementNumber = this.contractReportCorrectionService.GetContractReportCorrectionElementNumber(contractReportCorrection);

            string certCheckedByUser = string.Empty;

            if (contractReportCorrection.CertCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportCorrection.CertCheckedByUserId.Value);
                certCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (contractReportCorrection.Type == ContractReportCorrectionType.PaymentVerified || contractReportCorrection.Type == ContractReportCorrectionType.AdvanceCovered)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportCorrection.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string paymentCheckedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    paymentCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportCorrectionDO(contractReportCorrectionElementNumber, contractReportCorrection, certCheckedByUser, payment, paymentCheck, paymentCheckedByUser);
            }

            return new ContractReportCorrectionDO(contractReportCorrectionElementNumber, contractReportCorrection, certCheckedByUser);
        }

        [Route("~/api/certReports/{certReportId:int}/corrections/{contractReportCorrectionId:int}/correction")]
        public ContractReportCorrectionDO GetCertReportCorrectionContractReportCorrection(int certReportId, int contractReportCorrectionId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));
            this.relationsRepository.AssertCertReportHasContractReportCorrection(certReportId, contractReportCorrectionId);

            var contractReportCorrection = this.contractReportCorrectionsRepository.Find(contractReportCorrectionId);

            var contractReportCorrectionElementNumber = this.contractReportCorrectionService.GetContractReportCorrectionElementNumber(contractReportCorrection);

            string certCheckedByUser = string.Empty;

            if (contractReportCorrection.CertCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportCorrection.CertCheckedByUserId.Value);
                certCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (contractReportCorrection.Type == ContractReportCorrectionType.PaymentVerified || contractReportCorrection.Type == ContractReportCorrectionType.AdvanceCovered)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportCorrection.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string paymentCheckedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    paymentCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportCorrectionDO(contractReportCorrectionElementNumber, contractReportCorrection, certCheckedByUser, payment, paymentCheck, paymentCheckedByUser);
            }

            return new ContractReportCorrectionDO(contractReportCorrectionElementNumber, contractReportCorrection, certCheckedByUser);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/corrections/{contractReportCorrectionId:int}/correction")]
        public ContractReportCorrectionDO GetAnnualAccountReportCorrectionContractReportCorrection(int annualAccountReportId, int contractReportCorrectionId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasContractReportCorrection(annualAccountReportId, contractReportCorrectionId);

            var contractReportCorrection = this.contractReportCorrectionsRepository.Find(contractReportCorrectionId);

            var contractReportCorrectionElementNumber = this.contractReportCorrectionService.GetContractReportCorrectionElementNumber(contractReportCorrection);

            string certCheckedByUser = string.Empty;

            if (contractReportCorrection.CertCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportCorrection.CertCheckedByUserId.Value);
                certCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (contractReportCorrection.Type == ContractReportCorrectionType.PaymentVerified || contractReportCorrection.Type == ContractReportCorrectionType.AdvanceCovered)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportCorrection.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string paymentCheckedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    paymentCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportCorrectionDO(contractReportCorrectionElementNumber, contractReportCorrection, certCheckedByUser, payment, paymentCheck, paymentCheckedByUser);
            }

            return new ContractReportCorrectionDO(contractReportCorrectionElementNumber, contractReportCorrection, certCheckedByUser);
        }

        [HttpPut]
        [Route("{contractReportCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.Edit.ContractReportCorrectionData), IdParam = "contractReportCorrectionId")]
        public void UpdateContractReportCorrectionData(int contractReportCorrectionId, ContractReportCorrectionDO contractReportCorrectionDO)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            var contractReportCorrection = this.contractReportCorrectionsRepository.FindForUpdate(contractReportCorrectionId, contractReportCorrectionDO.Version);

            contractReportCorrection.UpdateData(
                contractReportCorrectionDO.Sign.Value,
                contractReportCorrectionDO.Date.Value,
                contractReportCorrectionDO.Description,
                contractReportCorrectionDO.Reason,
                contractReportCorrectionDO.CorrectionType,
                contractReportCorrectionDO.FinancialCorrectionId,
                contractReportCorrectionDO.IrregularityId,
                contractReportCorrectionDO.FlatFinancialCorrectionId,
                contractReportCorrectionDO.CorrectedApprovedEuAmount,
                contractReportCorrectionDO.CorrectedApprovedTotalAmount,
                contractReportCorrectionDO.CorrectedApprovedCrossAmount,
                contractReportCorrectionDO.CorrectedApprovedSelfAmount);
            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("~/api/certReports/{certReportId:int}/corrections/{contractReportCorrectionId:int}/certUpdate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.CertUpdate), IdParam = "contractReportCorrectionId")]
        public void CertUpdateContractReportCorrection(int certReportId, int contractReportCorrectionId, ContractReportCorrectionDO contractReportCorrectionDO)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.relationsRepository.AssertCertReportHasContractReportCorrection(certReportId, contractReportCorrectionId);

            this.certReportCheckService.UpdateContractReportCorrection(
                certReportId,
                contractReportCorrectionId,
                contractReportCorrectionDO.Version,
                contractReportCorrectionDO.UncertifiedCorrectedApprovedEuAmount,
                contractReportCorrectionDO.UncertifiedCorrectedApprovedBgAmount,
                contractReportCorrectionDO.UncertifiedCorrectedApprovedBfpTotalAmount,
                contractReportCorrectionDO.UncertifiedCorrectedApprovedCrossAmount,
                contractReportCorrectionDO.UncertifiedCorrectedApprovedSelfAmount,
                contractReportCorrectionDO.UncertifiedCorrectedApprovedTotalAmount,
                contractReportCorrectionDO.CertifiedCorrectedApprovedEuAmount,
                contractReportCorrectionDO.CertifiedCorrectedApprovedBgAmount,
                contractReportCorrectionDO.CertifiedCorrectedApprovedBfpTotalAmount,
                contractReportCorrectionDO.CertifiedCorrectedApprovedCrossAmount,
                contractReportCorrectionDO.CertifiedCorrectedApprovedSelfAmount,
                contractReportCorrectionDO.CertifiedCorrectedApprovedTotalAmount);
        }

        [HttpPost]
        [Route("{contractReportCorrectionId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.ChangeStatusToEntered), IdParam = "contractReportCorrectionId")]
        public void EnterContractReportCorrection(int contractReportCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCorrection = this.contractReportCorrectionsRepository.FindForUpdate(contractReportCorrectionId, vers);

            if (contractReportCorrection.IsActivated)
            {
                contractReportCorrection.ChangeStatusToEntered(this.accessContext.UserId);
            }
            else
            {
                this.countersRepository.CreateContractReportCorrectionCounter(contractReportCorrection.ProgrammeId);
                var regNum = this.countersRepository.GetNextContractReportCorrectionNumber(contractReportCorrection.ProgrammeId);

                contractReportCorrection.ChangeStatusToEntered(this.accessContext.UserId, regNum);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportCorrectionId:int}/canEnter")]
        public ErrorsDO CanEnterContractReportCorrection(int contractReportCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            var errors = this.contractReportCorrectionService.CanEnterContractReportCorrection(contractReportCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportCorrectionId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.ChangeStatusToDraft), IdParam = "contractReportCorrectionId")]
        public void MakeDraft(int contractReportCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCorrection = this.contractReportCorrectionsRepository.FindForUpdate(contractReportCorrectionId, vers);

            contractReportCorrection.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportCorrectionId:int}/canSetToDraft")]
        public ErrorsDO CanMakeDraft(int contractReportCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            var errors = this.contractReportCorrectionService.CanMakeDraftContractReportCorrection(contractReportCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportCorrectionId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.ChangeStatusToRemoved), IdParam = "contractReportCorrectionId")]
        public void MakeRemoved(int contractReportCorrectionId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCorrection = this.contractReportCorrectionsRepository.FindForUpdate(contractReportCorrectionId, vers);

            contractReportCorrection.ChangeStatusToDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractReportCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.Delete), IdParam = "contractReportCorrectionId")]
        public void Delete(int contractReportCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCorrection = this.contractReportCorrectionsRepository.FindForUpdate(contractReportCorrectionId, vers);

            this.contractReportCorrectionsRepository.Remove(contractReportCorrection);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/corrections/{contractReportCorrectionId:int}/changeCertStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.ChangeCertStatusToEnded), IdParam = "contractReportCorrectionId")]
        public void ChangeContractReportCorrectionCertStatusToEnded(int certReportId, int contractReportCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasContractReportCorrection(certReportId, contractReportCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportCorrectionCertStatus(contractReportCorrectionId, vers, ContractReportCorrectionCertStatus.Ended);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/corrections/{contractReportCorrectionId:int}/changeCertStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.ChangeCertStatusToDraft), IdParam = "contractReportCorrectionId")]
        public void ChangeContractReportCorrectionCertStatusToDraft(int certReportId, int contractReportCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasContractReportCorrection(certReportId, contractReportCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportCorrectionCertStatus(contractReportCorrectionId, vers, ContractReportCorrectionCertStatus.Draft);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/corrections/{contractReportCorrectionId:int}/canChangeCertStatusToEnded")]
        public ErrorsDO CanChangeContractReportCorrectionCertStatusToEnded(int certReportId, int contractReportCorrectionId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasContractReportCorrection(certReportId, contractReportCorrectionId);

            var errors = this.certReportCheckService.CanChangeContractReportCorrectionCertStatusToEnded(contractReportCorrectionId);

            return new ErrorsDO(errors);
        }
    }
}
