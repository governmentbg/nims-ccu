using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.ContractReportRevalidation;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportRevalidations.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Counters;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidations.Controllers
{
    [RoutePrefix("api/contractReportRevalidations")]
    public class ContractReportRevalidationsController : ApiController
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
        private IContractReportRevalidationsRepository contractReportRevalidationsRepository;
        private IContractReportRevalidationService contractReportRevalidationService;
        private ICertReportCheckService certReportCheckService;

        public ContractReportRevalidationsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            IUsersRepository usersRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository,
            IContractReportRevalidationsRepository contractReportRevalidationsRepository,
            IContractReportRevalidationService contractReportRevalidationService,
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
            this.contractReportRevalidationsRepository = contractReportRevalidationsRepository;
            this.contractReportRevalidationService = contractReportRevalidationService;
            this.certReportCheckService = certReportCheckService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportRevalidationVO> GetContractReportRevalidations(
            int? programmeId = null,
            ContractReportRevalidationType? type = null,
            ContractReportRevalidationStatus? status = null)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(id => id == programmeId).ToArray();
            }

            return this.contractReportRevalidationsRepository.GetContractReportRevalidations(programmeIds, type, status);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportRevalidation")]
        public IList<ContractReportRevalidationVO> GetContractReportRevalidationsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportRevalidationsRepository.GetContractReportRevalidationsForProjectDossier(contractId);
        }

        [HttpGet]
        [Route("new")]
        public NewContractReportRevalidationDO NewContractReportRevalidation()
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationListActions.Create);

            return new NewContractReportRevalidationDO()
            {
                Sign = Sign.Positive,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.Create))]
        public object CreateContractReportRevalidation(NewContractReportRevalidationDO newDoc)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationListActions.Create);

            var newContractReportRevalidation = this.contractReportRevalidationService.CreateContractReportRevalidation(
                this.accessContext.UserId,
                newDoc.Type.Value,
                newDoc.Date.Value,
                newDoc.ProgrammeId,
                newDoc.ProgrammePriorityId,
                newDoc.ProcedureId,
                newDoc.ContractId,
                newDoc.ContractReportPaymentId);

            return new { ContractReportRevalidationId = newContractReportRevalidation.ContractReportRevalidationId };
        }

        [Route("{contractReportRevalidationId:int}/info")]
        public ContractReportRevalidationInfoVO GetContractReportRevalidationInfo(int contractReportRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.View, contractReportRevalidationId);

            return this.contractReportRevalidationsRepository.GetInfo(contractReportRevalidationId);
        }

        [Route("{contractReportRevalidationId:int}/data")]
        public ContractReportRevalidationBasicDataVO GetContractReportRevalidationBasicData(int contractReportRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.View, contractReportRevalidationId);

            return this.contractReportRevalidationsRepository.GetBasicData(contractReportRevalidationId);
        }

        [Route("{contractReportRevalidationId:int}")]
        public ContractReportRevalidationDO GetContractReportRevalidationData(int contractReportRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.View, contractReportRevalidationId);

            var contractReportRevalidation = this.contractReportRevalidationsRepository.Find(contractReportRevalidationId);

            string certCheckedByUser = string.Empty;

            if (contractReportRevalidation.CertCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportRevalidation.CertCheckedByUserId.Value);
                certCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (contractReportRevalidation.Type == ContractReportRevalidationType.PaymentRevalidated)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportRevalidation.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string paymentCheckedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    paymentCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportRevalidationDO(contractReportRevalidation, certCheckedByUser, payment, paymentCheck, paymentCheckedByUser);
            }

            return new ContractReportRevalidationDO(contractReportRevalidation, certCheckedByUser);
        }

        [Route("~/api/certReports/{certReportId:int}/revalidations/{contractReportRevalidationId:int}/revalidation")]
        public ContractReportRevalidationDO GetCertReportRevalidationContractReportRevalidation(int certReportId, int contractReportRevalidationId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasContractReportRevalidation(certReportId, contractReportRevalidationId);

            var contractReportRevalidation = this.contractReportRevalidationsRepository.Find(contractReportRevalidationId);

            string certCheckedByUser = string.Empty;

            if (contractReportRevalidation.CertCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportRevalidation.CertCheckedByUserId.Value);
                certCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (contractReportRevalidation.Type == ContractReportRevalidationType.PaymentRevalidated)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportRevalidation.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string paymentCheckedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    paymentCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportRevalidationDO(contractReportRevalidation, certCheckedByUser, payment, paymentCheck, paymentCheckedByUser);
            }

            return new ContractReportRevalidationDO(contractReportRevalidation, certCheckedByUser);
        }

        [HttpPut]
        [Route("{contractReportRevalidationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.Edit.ContractReportRevalidationData), IdParam = "contractReportRevalidationId")]
        public void UpdateContractReportRevalidationData(int contractReportRevalidationId, ContractReportRevalidationDO contractReportRevalidationDO)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            var contractReportRevalidation = this.contractReportRevalidationsRepository.FindForUpdate(contractReportRevalidationId, contractReportRevalidationDO.Version);

            contractReportRevalidation.UpdateData(
                contractReportRevalidationDO.Date.Value,
                contractReportRevalidationDO.Description,
                contractReportRevalidationDO.Reason,
                contractReportRevalidationDO.RevalidatedEuAmount,
                contractReportRevalidationDO.RevalidatedBgAmount,
                contractReportRevalidationDO.RevalidatedCrossAmount,
                contractReportRevalidationDO.RevalidatedSelfAmount);
            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("~/api/certReports/{certReportId:int}/revalidations/{contractReportRevalidationId:int}/certUpdate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.CertUpdate), IdParam = "contractReportRevalidationId")]
        public void CertUpdateContractReportRevalidation(int certReportId, int contractReportRevalidationId, ContractReportRevalidationDO contractReportRevalidationDO)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.relationsRepository.AssertCertReportHasContractReportRevalidation(certReportId, contractReportRevalidationId);

            this.certReportCheckService.UpdateContractReportRevalidation(
                certReportId,
                contractReportRevalidationId,
                contractReportRevalidationDO.Version,
                contractReportRevalidationDO.UncertifiedRevalidatedEuAmount,
                contractReportRevalidationDO.UncertifiedRevalidatedBgAmount,
                contractReportRevalidationDO.UncertifiedRevalidatedBfpTotalAmount,
                contractReportRevalidationDO.UncertifiedRevalidatedCrossAmount,
                contractReportRevalidationDO.UncertifiedRevalidatedSelfAmount,
                contractReportRevalidationDO.UncertifiedRevalidatedTotalAmount,
                contractReportRevalidationDO.CertifiedRevalidatedEuAmount,
                contractReportRevalidationDO.CertifiedRevalidatedBgAmount,
                contractReportRevalidationDO.CertifiedRevalidatedBfpTotalAmount,
                contractReportRevalidationDO.CertifiedRevalidatedCrossAmount,
                contractReportRevalidationDO.CertifiedRevalidatedSelfAmount,
                contractReportRevalidationDO.CertifiedRevalidatedTotalAmount);
        }

        [HttpPost]
        [Route("{contractReportRevalidationId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.ChangeStatusToEntered), IdParam = "contractReportRevalidationId")]
        public void EnterContractReportRevalidation(int contractReportRevalidationId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportRevalidation = this.contractReportRevalidationsRepository.FindForUpdate(contractReportRevalidationId, vers);

            if (contractReportRevalidation.IsActivated)
            {
                contractReportRevalidation.ChangeStatusToEntered(this.accessContext.UserId);
            }
            else
            {
                this.countersRepository.CreateContractReportRevalidationCounter(contractReportRevalidation.ProgrammeId);
                var regNum = this.countersRepository.GetNextContractReportRevalidationNumber(contractReportRevalidation.ProgrammeId);

                contractReportRevalidation.ChangeStatusToEntered(this.accessContext.UserId, regNum);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportRevalidationId:int}/canEnter")]
        public ErrorsDO CanEnterContractReportRevalidation(int contractReportRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            var errors = this.contractReportRevalidationService.CanEnterContractReportRevalidation(contractReportRevalidationId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportRevalidationId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.ChangeStatusToDraft), IdParam = "contractReportRevalidationId")]
        public void MakeDraft(int contractReportRevalidationId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportRevalidation = this.contractReportRevalidationsRepository.FindForUpdate(contractReportRevalidationId, vers);

            contractReportRevalidation.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportRevalidationId:int}/canSetToDraft")]
        public ErrorsDO CanMakeDraft(int contractReportRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            var errors = this.contractReportRevalidationService.CanMakeDraftContractReportRevalidation(contractReportRevalidationId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportRevalidationId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.ChangeStatusToRemoved), IdParam = "contractReportRevalidationId")]
        public void MakeRemoved(int contractReportRevalidationId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportRevalidation = this.contractReportRevalidationsRepository.FindForUpdate(contractReportRevalidationId, vers);

            contractReportRevalidation.ChangeStatusToDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractReportRevalidationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.Delete), IdParam = "contractReportRevalidationId")]
        public void Delete(int contractReportRevalidationId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportRevalidation = this.contractReportRevalidationsRepository.FindForUpdate(contractReportRevalidationId, vers);

            this.contractReportRevalidationsRepository.Remove(contractReportRevalidation);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/revalidations/{contractReportRevalidationId:int}/changeCertStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.ChangeCertStatusToEnded), IdParam = "contractReportRevalidationId")]
        public void ChangeContractReportRevalidationCertStatusToEnded(int certReportId, int contractReportRevalidationId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasContractReportRevalidation(certReportId, contractReportRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportRevalidationCertStatus(contractReportRevalidationId, vers, ContractReportRevalidationCertStatus.Ended);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/revalidations/{contractReportRevalidationId:int}/changeCertStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.ChangeCertStatusToDraft), IdParam = "contractReportRevalidationId")]
        public void ChangeContractReportRevalidationCertStatusToDraft(int certReportId, int contractReportRevalidationId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasContractReportRevalidation(certReportId, contractReportRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportRevalidationCertStatus(contractReportRevalidationId, vers, ContractReportRevalidationCertStatus.Draft);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/revalidations/{contractReportRevalidationId:int}/canChangeCertStatusToEnded")]
        public ErrorsDO CanChangeContractReportRevalidationCertStatusToEnded(int certReportId, int contractReportRevalidationId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasContractReportRevalidation(certReportId, contractReportRevalidationId);

            var errors = this.certReportCheckService.CanChangeContractReportRevalidationCertStatusToEnded(contractReportRevalidationId);

            return new ErrorsDO(errors);
        }
    }
}
