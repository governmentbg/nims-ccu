using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Allowances.Repositories;
using Eumis.Domain.Allowances;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Allowances.DataObjects;
using System;
using System.Web.Http;

namespace Eumis.Web.Api.Allowances.Controllers
{
    [RoutePrefix("api/allowances/{allowanceId:int}/allowanceRates")]
    public class AllowanceRatesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAllowancesRepository allowancesRepository;
        private IAuthorizer authorizer;

        public AllowanceRatesController(
            IUnitOfWork unitOfWork,
            IAllowancesRepository allowancesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.allowancesRepository = allowancesRepository;
            this.authorizer = authorizer;
        }

        [Route("{allowanceRateId:int}")]
        public AllowanceRateDO GetAllowanceRate(int allowanceId, int allowanceRateId)
        {
            this.authorizer.AssertCanDo(AllowanceActions.View, allowanceId);

            var allowance = this.allowancesRepository.Find(allowanceId);
            var allowanceRate = allowance.FindAllowanceRate(allowanceRateId);

            return new AllowanceRateDO(allowanceRate, allowance.Version);
        }

        [HttpPut]
        [Route("{allowanceRateId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Allowances.Edit.AllowanceRates.Edit), IdParam = "allowanceId", ChildIdParam = "allowanceRateId")]
        public void UpdateAllowanceRate(int allowanceId, int allowanceRateId, AllowanceRateDO allowanceRateDO)
        {
            this.authorizer.AssertCanDo(AllowanceActions.Edit, allowanceId);

            Allowance allowance = this.allowancesRepository.FindForUpdate(allowanceId, allowanceRateDO.Version);
            allowance.UpdateAllowanceRate(allowanceRateId, allowanceRateDO.Rate.Value);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public AllowanceRateDO NewAllowanceRate(int allowanceId)
        {
            this.authorizer.AssertCanDo(AllowanceListActions.Create);

            Allowance allowance = this.allowancesRepository.Find(allowanceId);

            return new AllowanceRateDO(allowanceId, allowance.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Allowances.Edit.AllowanceRates.Create), IdParam = "allowanceId")]
        public void CreateAllowanceRate(int allowanceId, AllowanceRateDO allowanceRate)
        {
            this.authorizer.AssertCanDo(AllowanceListActions.Create);

            Allowance allowance = this.allowancesRepository.FindForUpdate(allowanceId, allowanceRate.Version);

            allowance.AddAllowanceRate(allowanceRate.Date.Value, allowanceRate.Rate.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{allowanceRateId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Allowances.Edit.AllowanceRates.Delete), IdParam = "allowanceId", ChildIdParam = "allowanceRateId")]
        public void DeleteAllowanceRate(int allowanceId, int allowanceRateId, string version)
        {
            this.authorizer.AssertCanDo(AllowanceActions.Edit, allowanceId);

            byte[] vers = System.Convert.FromBase64String(version);
            Allowance allowance = this.allowancesRepository.FindForUpdate(allowanceId, vers);

            allowance.RemoveAllowanceRate(allowanceRateId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("isCorrectDate")]
        public bool IsCorrectAllowanceRateDate(int allowanceId, [FromBody] DateTime date)
        {
            this.authorizer.AssertCanDo(AllowanceActions.Edit, allowanceId);

            Allowance allowance = this.allowancesRepository.Find(allowanceId);

            return allowance.IsCorrectAllowanceRateDate(date);
        }
    }
}
