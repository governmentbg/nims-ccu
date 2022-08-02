using Eumis.ApplicationServices.Services.ContractReportRevalidationCertAuthorityCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportRevalidationCertAuthorityCorrections.Repositories;
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
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/contractReportRevalidationCertAuthorityCorrections")]
    public class ContractReportRevalidationCertAuthorityCorrectionsController : ApiController
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
        private IContractReportRevalidationCertAuthorityCorrectionsRepository contractReportRevalidationCertAuthorityCorrectionsRepository;
        private IContractReportRevalidationCertAuthorityCorrectionService contractReportRevalidationCertAuthorityCorrectionService;

        public ContractReportRevalidationCertAuthorityCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            IUsersRepository usersRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository,
            IContractReportRevalidationCertAuthorityCorrectionsRepository contractReportRevalidationCertAuthorityCorrectionsRepository,
            IContractReportRevalidationCertAuthorityCorrectionService contractReportRevalidationCertAuthorityCorrectionService,
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
            this.contractReportRevalidationCertAuthorityCorrectionsRepository = contractReportRevalidationCertAuthorityCorrectionsRepository;
            this.contractReportRevalidationCertAuthorityCorrectionService = contractReportRevalidationCertAuthorityCorrectionService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportRevalidationCertAuthorityCorrectionVO> GetContractReportRevalidationCertAuthorityCorrections(
            int? programmeId = null,
            ContractReportRevalidationCertAuthorityCorrectionType? type = null,
            ContractReportRevalidationCertAuthorityCorrectionStatus? status = null)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(id => id == programmeId).ToArray();
            }

            return this.contractReportRevalidationCertAuthorityCorrectionsRepository.GetContractReportRevalidationCertAuthorityCorrections(programmeIds, type, status);
        }

        [HttpGet]
        [Route("new")]
        public NewContractReportRevalidationCertAuthorityCorrectionDO NewContractReportRevalidationCertAuthorityCorrection()
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionListActions.Create);

            return new NewContractReportRevalidationCertAuthorityCorrectionDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityCorrections.Create))]
        public object CreateContractReportRevalidationCertAuthorityCorrection(NewContractReportRevalidationCertAuthorityCorrectionDO newDoc)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionListActions.Create);

            var newContractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionService.CreateContractReportRevalidationCertAuthorityCorrection(
                this.accessContext.UserId,
                newDoc.Type.Value,
                newDoc.Sign.Value,
                newDoc.Date.Value,
                newDoc.ProgrammeId,
                newDoc.ProgrammePriorityId,
                newDoc.ProcedureId,
                newDoc.ContractId,
                newDoc.ContractReportPaymentId);

            return new { ContractReportRevalidationCertAuthorityCorrectionId = newContractReportRevalidationCertAuthorityCorrection.ContractReportRevalidationCertAuthorityCorrectionId };
        }

        [Route("{contractReportRevalidationCertAuthorityCorrectionId:int}/info")]
        public ContractReportRevalidationCertAuthorityCorrectionInfoVO GetContractReportRevalidationCertAuthorityCorrectionInfo(int contractReportRevalidationCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.View, contractReportRevalidationCertAuthorityCorrectionId);

            return this.contractReportRevalidationCertAuthorityCorrectionsRepository.GetInfo(contractReportRevalidationCertAuthorityCorrectionId);
        }

        [Route("{contractReportRevalidationCertAuthorityCorrectionId:int}/data")]
        public ContractReportRevalidationCertAuthorityCorrectionBasicDataVO GetContractReportRevalidationCertAuthorityCorrectionBasicData(int contractReportRevalidationCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.View, contractReportRevalidationCertAuthorityCorrectionId);

            return this.contractReportRevalidationCertAuthorityCorrectionsRepository.GetBasicData(contractReportRevalidationCertAuthorityCorrectionId);
        }

        [Route("{contractReportRevalidationCertAuthorityCorrectionId:int}")]
        public ContractReportRevalidationCertAuthorityCorrectionDO GetContractReportRevalidationCertAuthorityCorrectionData(int contractReportRevalidationCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.View, contractReportRevalidationCertAuthorityCorrectionId);

            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.Find(contractReportRevalidationCertAuthorityCorrectionId);

            if (contractReportRevalidationCertAuthorityCorrection.Type == ContractReportRevalidationCertAuthorityCorrectionType.PaymentRevalidated)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportRevalidationCertAuthorityCorrection.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string checkedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportRevalidationCertAuthorityCorrectionDO(contractReportRevalidationCertAuthorityCorrection, payment, paymentCheck, checkedByUser);
            }

            return new ContractReportRevalidationCertAuthorityCorrectionDO(contractReportRevalidationCertAuthorityCorrection);
        }

        [HttpPut]
        [Route("{contractReportRevalidationCertAuthorityCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityCorrections.Edit.ContractReportRevalidationCertAuthorityCorrectionData), IdParam = "contractReportRevalidationCertAuthorityCorrectionId")]
        public void UpdateContractReportRevalidationCertAuthorityCorrectionData(int contractReportRevalidationCertAuthorityCorrectionId, ContractReportRevalidationCertAuthorityCorrectionDO contractReportRevalidationCertAuthorityCorrectionDO)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityCorrectionId, contractReportRevalidationCertAuthorityCorrectionDO.Version);

            contractReportRevalidationCertAuthorityCorrection.UpdateData(
                contractReportRevalidationCertAuthorityCorrectionDO.Sign.Value,
                contractReportRevalidationCertAuthorityCorrectionDO.Date.Value,
                contractReportRevalidationCertAuthorityCorrectionDO.Description,
                contractReportRevalidationCertAuthorityCorrectionDO.Reason,
                contractReportRevalidationCertAuthorityCorrectionDO.CertifiedRevalidatedEuAmount,
                contractReportRevalidationCertAuthorityCorrectionDO.CertifiedRevalidatedBgAmount,
                contractReportRevalidationCertAuthorityCorrectionDO.CertifiedRevalidatedCrossAmount,
                contractReportRevalidationCertAuthorityCorrectionDO.CertifiedRevalidatedSelfAmount);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityCorrectionId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityCorrections.ChangeStatusToEntered), IdParam = "contractReportRevalidationCertAuthorityCorrectionId")]
        public void EnterContractReportRevalidationCertAuthorityCorrection(int contractReportRevalidationCertAuthorityCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityCorrectionId, vers);

            if (contractReportRevalidationCertAuthorityCorrection.IsActivated)
            {
                contractReportRevalidationCertAuthorityCorrection.ChangeStatusToEntered(this.accessContext.UserId);
            }
            else
            {
                this.countersRepository.CreateContractReportRevalidationCertAuthorityCorrectionCounter(contractReportRevalidationCertAuthorityCorrection.ProgrammeId);
                var regNum = this.countersRepository.GetNextContractReportRevalidationCertAuthorityCorrectionNumber(contractReportRevalidationCertAuthorityCorrection.ProgrammeId);

                contractReportRevalidationCertAuthorityCorrection.ChangeStatusToEntered(this.accessContext.UserId, regNum);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityCorrectionId:int}/canEnter")]
        public ErrorsDO CanEnterContractReportRevalidationCertAuthorityCorrection(int contractReportRevalidationCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            var errors = this.contractReportRevalidationCertAuthorityCorrectionService.CanEnterContractReportRevalidationCertAuthorityCorrection(contractReportRevalidationCertAuthorityCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityCorrectionId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityCorrections.ChangeStatusToDraft), IdParam = "contractReportRevalidationCertAuthorityCorrectionId")]
        public void MakeDraft(int contractReportRevalidationCertAuthorityCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityCorrectionId, vers);

            contractReportRevalidationCertAuthorityCorrection.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityCorrectionId:int}/canSetToDraft")]
        public ErrorsDO CanMakeDraft(int contractReportRevalidationCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            var errors = this.contractReportRevalidationCertAuthorityCorrectionService.CanMakeDraftContractReportRevalidationCertAuthorityCorrection(contractReportRevalidationCertAuthorityCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityCorrectionId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityCorrections.ChangeStatusToRemoved), IdParam = "contractReportRevalidationCertAuthorityCorrectionId")]
        public void MakeRemoved(int contractReportRevalidationCertAuthorityCorrectionId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityCorrectionId, vers);

            contractReportRevalidationCertAuthorityCorrection.ChangeStatusToDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractReportRevalidationCertAuthorityCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityCorrections.Delete), IdParam = "contractReportRevalidationCertAuthorityCorrectionId")]
        public void Delete(int contractReportRevalidationCertAuthorityCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityCorrectionId, vers);

            this.contractReportRevalidationCertAuthorityCorrectionsRepository.Remove(contractReportRevalidationCertAuthorityCorrection);

            this.unitOfWork.Save();
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/certAuthorityRevalidationCorrections/{contractReportRevalidationCertAuthorityCorrectionId:int}/certAuthorityRevalidationCorrection")]
        public ContractReportRevalidationCertAuthorityCorrectionDO GetAnnualAccountReportRevalidationCorrectionData(int annualAccountReportId, int contractReportRevalidationCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertifiedRevalidationCorrection(annualAccountReportId, contractReportRevalidationCertAuthorityCorrectionId);

            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.Find(contractReportRevalidationCertAuthorityCorrectionId);

            if (contractReportRevalidationCertAuthorityCorrection.Type == ContractReportRevalidationCertAuthorityCorrectionType.PaymentRevalidated)
            {
                var payment = this.contractReportPaymentsRepository.Find(contractReportRevalidationCertAuthorityCorrection.ContractReportPaymentId.Value);

                var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(payment.ContractReportId);

                string checkedByUser = string.Empty;

                if (paymentCheck.CheckedByUserId.HasValue)
                {
                    var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                    checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
                }

                return new ContractReportRevalidationCertAuthorityCorrectionDO(contractReportRevalidationCertAuthorityCorrection, payment, paymentCheck, checkedByUser);
            }

            return new ContractReportRevalidationCertAuthorityCorrectionDO(contractReportRevalidationCertAuthorityCorrection);
        }
    }
}
