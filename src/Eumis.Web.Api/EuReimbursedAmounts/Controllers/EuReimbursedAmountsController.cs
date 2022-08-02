using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.EuReimbursedAmount;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.EuReimbursedAmounts.Repositories;
using Eumis.Data.EuReimbursedAmounts.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.EuReimbursedAmounts;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EuReimbursedAmounts.DataObjects;

namespace Eumis.Web.Api.EuReimbursedAmounts.Controllers
{
    [RoutePrefix("api/euReimbursedAmounts")]
    public class EuReimbursedAmountsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IEuReimbursedAmountsRepository euReimbursedAmountsRepository;
        private IEuReimbursedAmountService euReimbursedAmountService;

        public EuReimbursedAmountsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IEuReimbursedAmountsRepository euReimbursedAmountsRepository,
            IEuReimbursedAmountService euReimbursedAmountService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.euReimbursedAmountsRepository = euReimbursedAmountsRepository;
            this.euReimbursedAmountService = euReimbursedAmountService;
        }

        [Route("")]
        public IList<EuReimbursedAmountVO> GetEuReimbursedAmounts(EuReimbursedAmountStatus? status = null)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            return this.euReimbursedAmountsRepository.GetEuReimbursedAmounts(programmeIds, status);
        }

        [HttpGet]
        [Route("new")]
        public NewEuReimbursedAmountDO CreateEuReimbursedAmount()
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountListActions.Create);

            return new NewEuReimbursedAmountDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EuReimbursedAmounts.Create))]
        public object CreateEuReimbursedAmount(NewEuReimbursedAmountDO newAmountDO)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountListActions.Create);

            if (!this.euReimbursedAmountService.CanCreate(this.accessContext.UserId, newAmountDO.ProgrammeId.Value))
            {
                throw new DomainValidationException("Cannot create EuReimbursedAmount");
            }

            var newEuReimbursedAmount = new EuReimbursedAmount(
                newAmountDO.ProgrammeId.Value);

            this.euReimbursedAmountsRepository.Add(newEuReimbursedAmount);
            this.unitOfWork.Save();

            return new { EuReimbursedAmountId = newEuReimbursedAmount.EuReimbursedAmountId };
        }

        [Route("{euReimbursedAmountId:int}/info")]
        public EuReimbursedAmountInfoVO GetEuReimbursedAmountInfo(int euReimbursedAmountId)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.View, euReimbursedAmountId);

            return this.euReimbursedAmountsRepository.GetInfo(euReimbursedAmountId);
        }

        [Route("{euReimbursedAmountId:int}")]
        public EuReimbursedAmountDO GetEuReimbursedAmountData(int euReimbursedAmountId)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.View, euReimbursedAmountId);

            var euReimbursedAmount = this.euReimbursedAmountsRepository.Find(euReimbursedAmountId);

            return new EuReimbursedAmountDO(euReimbursedAmount);
        }

        [HttpPut]
        [Route("{euReimbursedAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EuReimbursedAmounts.Edit.AmountData), IdParam = "euReimbursedAmountId")]
        public void UpdateEuReimbursedAmountData(int euReimbursedAmountId, EuReimbursedAmountDO euReimbursedAmountDO)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.Edit, euReimbursedAmountId);

            var euReimbursedAmount = this.euReimbursedAmountsRepository.FindForUpdate(euReimbursedAmountId, euReimbursedAmountDO.Version);

            euReimbursedAmount.UpdateData(
                euReimbursedAmountDO.PaymentType,
                euReimbursedAmountDO.Date,
                euReimbursedAmountDO.PaymentAppNum,
                euReimbursedAmountDO.PaymentAppSentDate,
                euReimbursedAmountDO.PaymentAppDateFrom,
                euReimbursedAmountDO.PaymentAppDateTo,
                euReimbursedAmountDO.CertExpensesBfpEuAmountLv,
                euReimbursedAmountDO.CertExpensesBfpBgAmountLv,
                euReimbursedAmountDO.CertExpensesSelfAmountLv,
                euReimbursedAmountDO.CertExpensesBfpEuAmountEuro,
                euReimbursedAmountDO.CertExpensesBfpBgAmountEuro,
                euReimbursedAmountDO.CertExpensesSelfAmountEuro,
                euReimbursedAmountDO.EuTranche,
                euReimbursedAmountDO.Note);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{euReimbursedAmountId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EuReimbursedAmounts.ChangeStatusToEntered), IdParam = "euReimbursedAmountId")]
        public void EnterEuReimbursedAmount(int euReimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.Edit, euReimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var euReimbursedAmount = this.euReimbursedAmountsRepository.FindForUpdate(euReimbursedAmountId, vers);

            euReimbursedAmount.ChangeStatusToEntered();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{euReimbursedAmountId:int}/canEnter")]
        [Transaction]
        public ErrorsDO EnterEuReimbursedAmount(int euReimbursedAmountId)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.Edit, euReimbursedAmountId);

            var euReimbursedAmount = this.euReimbursedAmountsRepository.Find(euReimbursedAmountId);

            var errors = euReimbursedAmount.CanChangeStatusToEntered();

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{euReimbursedAmountId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EuReimbursedAmounts.ChangeStatusToDraft), IdParam = "euReimbursedAmountId")]
        public void MakeDraft(int euReimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.Edit, euReimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var euReimbursedAmount = this.euReimbursedAmountsRepository.FindForUpdate(euReimbursedAmountId, vers);

            euReimbursedAmount.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{euReimbursedAmountId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EuReimbursedAmounts.ChangeStatusToRemoved), IdParam = "euReimbursedAmountId")]
        public void MakeDraft(int euReimbursedAmountId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.Edit, euReimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var euReimbursedAmount = this.euReimbursedAmountsRepository.FindForUpdate(euReimbursedAmountId, vers);

            euReimbursedAmount.ChangeStatusToRemoved(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{euReimbursedAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EuReimbursedAmounts.Delete), IdParam = "euReimbursedAmountId")]
        public void DeleteEuReimbursedAmount(int euReimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(EuReimbursedAmountActions.Edit, euReimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var euReimbursedAmount = this.euReimbursedAmountsRepository.FindForUpdate(euReimbursedAmountId, vers);

            this.euReimbursedAmountsRepository.Remove(euReimbursedAmount);

            this.unitOfWork.Save();
        }
    }
}
