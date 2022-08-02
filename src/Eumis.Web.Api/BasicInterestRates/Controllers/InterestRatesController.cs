using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.BasicInterestRates.Repositories;
using Eumis.Domain.BasicInterestRates;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.BasicInterestRates.DataObjects;
using System;
using System.Web.Http;

namespace Eumis.Web.Api.BasicInterestRates.Controllers
{
    [RoutePrefix("api/basicInterestRates/{basicInterestRateId:int}/interestRates")]
    public class InterestRatesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IBasicInterestRatesRepository basicInterestRatesRepository;
        private IAuthorizer authorizer;

        public InterestRatesController(
            IUnitOfWork unitOfWork,
            IBasicInterestRatesRepository basicInterestRatesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.basicInterestRatesRepository = basicInterestRatesRepository;
            this.authorizer = authorizer;
        }

        [Route("{interestRateId:int}")]
        public InterestRateDO GetInterestRate(int basicInterestRateId, int interestRateId)
        {
            this.authorizer.AssertCanDo(BasicInterestRateActions.View, basicInterestRateId);

            var basicInterestRate = this.basicInterestRatesRepository.Find(basicInterestRateId);
            var interestRate = basicInterestRate.FindInterestRate(interestRateId);

            return new InterestRateDO(interestRate, basicInterestRate.Version);
        }

        [HttpPut]
        [Route("{interestRateId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.BasicInterestRates.Edit.InterestRates.Edit), IdParam = "basicInterestRateId", ChildIdParam = "interestRateId")]
        public void UpdateInterestRate(int basicInterestRateId, int interestRateId, InterestRateDO interestRateDO)
        {
            this.authorizer.AssertCanDo(BasicInterestRateActions.Edit, basicInterestRateId);

            BasicInterestRate basicInterestRate = this.basicInterestRatesRepository.FindForUpdate(
                basicInterestRateId,
                interestRateDO.Version);
            basicInterestRate.UpdateInterestRate(interestRateId, interestRateDO.Rate.Value);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public InterestRateDO NewInterestRate(int basicInterestRateId)
        {
            this.authorizer.AssertCanDo(BasicInterestRateListActions.Create);

            BasicInterestRate basicInterestRate = this.basicInterestRatesRepository.Find(basicInterestRateId);

            return new InterestRateDO(basicInterestRateId, basicInterestRate.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.BasicInterestRates.Edit.InterestRates.Create), IdParam = "basicInterestRateId")]
        public void CreateInterestRate(int basicInterestRateId, InterestRateDO interestRate)
        {
            this.authorizer.AssertCanDo(BasicInterestRateListActions.Create);

            BasicInterestRate basicInterestRate = this.basicInterestRatesRepository.FindForUpdate(
                basicInterestRateId,
                interestRate.Version);
            basicInterestRate.AddInterestRate(interestRate.Date.Value, interestRate.Rate.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{interestRateId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.BasicInterestRates.Edit.InterestRates.Delete), IdParam = "basicInterestRateId", ChildIdParam = "interestRateId")]
        public void DeleteInterestRate(int basicInterestRateId, int interestRateId, string version)
        {
            this.authorizer.AssertCanDo(BasicInterestRateActions.Edit, basicInterestRateId);

            byte[] vers = System.Convert.FromBase64String(version);
            BasicInterestRate basicInterestRate = this.basicInterestRatesRepository.FindForUpdate(basicInterestRateId, vers);

            basicInterestRate.RemoveInterestRate(interestRateId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("isCorrectDate")]
        public bool IsCorrectInterestRateDate(int basicInterestRateId, [FromBody] DateTime date)
        {
            this.authorizer.AssertCanDo(BasicInterestRateActions.Edit, basicInterestRateId);

            BasicInterestRate basicInterestRate = this.basicInterestRatesRepository.Find(basicInterestRateId);

            return basicInterestRate.IsCorrectInterestRateDate(date);
        }
    }
}
