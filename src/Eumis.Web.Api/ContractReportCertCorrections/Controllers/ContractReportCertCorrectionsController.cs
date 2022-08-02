using Eumis.ApplicationServices.Services.ContractReportCertCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportCertAuthorityCorrections.Repositories;
using Eumis.Data.ContractReportCertCorrections.Repositories;
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

namespace Eumis.Web.Api.ContractReportCertCorrections.Controllers
{
    [RoutePrefix("api/contractReportCertCorrections")]
    public class ContractReportCertCorrectionsController : ApiController
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
        private IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository;
        private IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository;
        private IContractReportCertCorrectionService contractReportCertCorrectionService;

        public ContractReportCertCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            IUsersRepository usersRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository,
            IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository,
            IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository,
            IContractReportCertCorrectionService contractReportCertCorrectionService,
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
            this.contractReportCertCorrectionsRepository = contractReportCertCorrectionsRepository;
            this.contractReportCertAuthorityCorrectionsRepository = contractReportCertAuthorityCorrectionsRepository;
            this.contractReportCertCorrectionService = contractReportCertCorrectionService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportCertCorrectionVO> GetContractReportCertCorrections(
            int? programmeId = null,
            ContractReportCertCorrectionType? type = null,
            ContractReportCertCorrectionStatus? status = null)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(id => id == programmeId).ToArray();
            }

            return this.contractReportCertCorrectionsRepository.GetContractReportCertCorrections(programmeIds, type, status);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportCertCorrection")]
        public IList<ContractReportCertCorrectionVO> GetContractReportCertCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportCertCorrectionsRepository.GetContractReportCertCorrectionsForProjectDossier(contractId);
        }

        [HttpGet]
        [Route("new")]
        public NewContractReportCertCorrectionDO NewContractReportCertCorrection()
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionListActions.Create);

            return new NewContractReportCertCorrectionDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertCorrections.Create))]
        public object CreateContractReportCertCorrection(NewContractReportCertCorrectionDO newDoc)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionListActions.Create);

            var newContractReportCertCorrection = this.contractReportCertCorrectionService.CreateContractReportCertCorrection(
                this.accessContext.UserId,
                newDoc.Type.Value,
                newDoc.Sign.Value,
                newDoc.Date.Value,
                newDoc.ProgrammeId,
                newDoc.ProgrammePriorityId,
                newDoc.ProcedureId,
                newDoc.ContractId,
                newDoc.ContractReportPaymentId);

            return new { ContractReportCertCorrectionId = newContractReportCertCorrection.ContractReportCertCorrectionId };
        }

        [Route("{contractReportCertCorrectionId:int}/info")]
        public ContractReportCertCorrectionInfoVO GetContractReportCertCorrectionInfo(int contractReportCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.View, contractReportCertCorrectionId);

            return this.contractReportCertCorrectionsRepository.GetInfo(contractReportCertCorrectionId);
        }

        [Route("{contractReportCertCorrectionId:int}/data")]
        public ContractReportCertCorrectionBasicDataVO GetContractReportCertCorrectionBasicData(int contractReportCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.View, contractReportCertCorrectionId);

            return this.contractReportCertCorrectionsRepository.GetBasicData(contractReportCertCorrectionId);
        }

        [Route("{contractReportCertCorrectionId:int}")]
        public ContractReportCertCorrectionDO GetContractReportCertCorrectionData(int contractReportCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.View, contractReportCertCorrectionId);

            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.Find(contractReportCertCorrectionId);

            if (contractReportCertCorrection.Type == ContractReportCertCorrectionType.PaymentCertified)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportCertCorrection.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string checkedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportCertCorrectionDO(contractReportCertCorrection, payment, paymentCheck, checkedByUser);
            }

            return new ContractReportCertCorrectionDO(contractReportCertCorrection);
        }

        [Route("~/api/certReports/{certReportId:int}/certCorrections/{contractReportCertCorrectionId:int}/certCorrection")]
        public ContractReportCertCorrectionDO GetCertReportRevalidationContractReportCertCorrection(int certReportId, int contractReportCertCorrectionId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));
            this.relationsRepository.AssertCertReportHasContractReportCertCorrection(certReportId, contractReportCertCorrectionId);

            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.Find(contractReportCertCorrectionId);

            if (contractReportCertCorrection.Type == ContractReportCertCorrectionType.PaymentCertified)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportCertCorrection.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string checkedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportCertCorrectionDO(contractReportCertCorrection, payment, paymentCheck, checkedByUser);
            }

            return new ContractReportCertCorrectionDO(contractReportCertCorrection);
        }

        [HttpPut]
        [Route("{contractReportCertCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertCorrections.Edit.ContractReportCertCorrectionData), IdParam = "contractReportCertCorrectionId")]
        public void UpdateContractReportCertCorrectionData(int contractReportCertCorrectionId, ContractReportCertCorrectionDO contractReportCertCorrectionDO)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.FindForUpdate(contractReportCertCorrectionId, contractReportCertCorrectionDO.Version);

            contractReportCertCorrection.UpdateData(
                contractReportCertCorrectionDO.Sign.Value,
                contractReportCertCorrectionDO.Date.Value,
                contractReportCertCorrectionDO.Description,
                contractReportCertCorrectionDO.Reason,
                contractReportCertCorrectionDO.CertifiedEuAmount,
                contractReportCertCorrectionDO.CertifiedBgAmount,
                contractReportCertCorrectionDO.CertifiedCrossAmount,
                contractReportCertCorrectionDO.CertifiedSelfAmount);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportCertCorrectionId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertCorrections.ChangeStatusToEntered), IdParam = "contractReportCertCorrectionId")]
        public void EnterContractReportCertCorrection(int contractReportCertCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.FindForUpdate(contractReportCertCorrectionId, vers);

            if (contractReportCertCorrection.IsActivated)
            {
                contractReportCertCorrection.ChangeStatusToEntered(this.accessContext.UserId);
            }
            else
            {
                this.countersRepository.CreateContractReportCertCorrectionCounter(contractReportCertCorrection.ProgrammeId);
                var regNum = this.countersRepository.GetNextContractReportCertCorrectionNumber(contractReportCertCorrection.ProgrammeId);

                contractReportCertCorrection.ChangeStatusToEntered(this.accessContext.UserId, regNum);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportCertCorrectionId:int}/canEnter")]
        public ErrorsDO CanEnterContractReportCertCorrection(int contractReportCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            var errors = this.contractReportCertCorrectionService.CanEnterContractReportCertCorrection(contractReportCertCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportCertCorrectionId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertCorrections.ChangeStatusToDraft), IdParam = "contractReportCertCorrectionId")]
        public void MakeDraft(int contractReportCertCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.FindForUpdate(contractReportCertCorrectionId, vers);

            contractReportCertCorrection.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportCertCorrectionId:int}/canSetToDraft")]
        public ErrorsDO CanMakeDraft(int contractReportCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            var errors = this.contractReportCertCorrectionService.CanMakeDraftContractReportCertCorrection(contractReportCertCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportCertCorrectionId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertCorrections.ChangeStatusToRemoved), IdParam = "contractReportCertCorrectionId")]
        public void MakeRemoved(int contractReportCertCorrectionId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.FindForUpdate(contractReportCertCorrectionId, vers);

            contractReportCertCorrection.ChangeStatusToDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractReportCertCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertCorrections.Delete), IdParam = "contractReportCertCorrectionId")]
        public void Delete(int contractReportCertCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.FindForUpdate(contractReportCertCorrectionId, vers);

            this.contractReportCertCorrectionsRepository.Remove(contractReportCertCorrection);

            this.unitOfWork.Save();
        }
    }
}
