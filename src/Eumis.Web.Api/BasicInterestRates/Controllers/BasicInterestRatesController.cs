using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.BasicInterestRates.Repositories;
using Eumis.Data.BasicInterestRates.ViewObjects;
using Eumis.Domain.BasicInterestRates;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.BasicInterestRates.DataObjects;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.BasicInterestRates.Controllers
{
    [RoutePrefix("api/basicInterestRates")]
    public class BasicInterestRatesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IBasicInterestRatesRepository basicInterestRatesRepository;
        private IAuthorizer authorizer;

        public BasicInterestRatesController(
            IUnitOfWork unitOfWork,
            IBasicInterestRatesRepository basicInterestRatesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.basicInterestRatesRepository = basicInterestRatesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<BasicInterestRateVO> GetBasicInterestRates()
        {
            this.authorizer.AssertCanDo(BasicInterestRateListActions.Search);

            return this.basicInterestRatesRepository.GetBasicInterestRates();
        }

        [Route("{basicInterestRateId:int}")]
        public BasicInterestRateDO GetBasicInterestRate(int basicInterestRateId)
        {
            this.authorizer.AssertCanDo(BasicInterestRateActions.View, basicInterestRateId);

            var basicInterestRate = this.basicInterestRatesRepository.Find(basicInterestRateId);

            return new BasicInterestRateDO(basicInterestRate);
        }

        [HttpPut]
        [Route("{basicInterestRateId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.BasicInterestRates.Edit.BasicData), IdParam = "basicInterestRateId")]
        public void UpdateBasicInterestRate(int basicInterestRateId, BasicInterestRateDataDO basicInterestRateData)
        {
            this.authorizer.AssertCanDo(BasicInterestRateActions.Edit, basicInterestRateId);

            BasicInterestRate oldBasicInterestRate = this.basicInterestRatesRepository.FindForUpdate(
                basicInterestRateId,
                basicInterestRateData.Version);
            oldBasicInterestRate.UpdateBasicInterestRate(basicInterestRateData.Name);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public BasicInterestRateDataDO NewBasicInterestRate()
        {
            this.authorizer.AssertCanDo(BasicInterestRateListActions.Create);

            return new BasicInterestRateDataDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.BasicInterestRates.Create))]
        public BasicInterestRateDO CreateBasicInterestRate(BasicInterestRateDataDO basicInterestRateData)
        {
            this.authorizer.AssertCanDo(BasicInterestRateListActions.Create);

            BasicInterestRate newBasicInterestRate = new BasicInterestRate(basicInterestRateData.Name);

            this.basicInterestRatesRepository.Add(newBasicInterestRate);

            this.unitOfWork.Save();

            return new BasicInterestRateDO(newBasicInterestRate);
        }

        [HttpDelete]
        [Route("{basicInterestRateId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.BasicInterestRates.Delete), IdParam = "basicInterestRateId")]
        public void DeleteBasicInterestRate(int basicInterestRateId, string version)
        {
            this.authorizer.AssertCanDo(BasicInterestRateActions.Delete, basicInterestRateId);

            byte[] vers = System.Convert.FromBase64String(version);
            BasicInterestRate oldBasicInterestRate = this.basicInterestRatesRepository.FindForUpdate(basicInterestRateId, vers);

            this.basicInterestRatesRepository.Remove(oldBasicInterestRate);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{basicInterestRateId:int}/canDelete")]
        public ErrorsDO CanDeleteBasicInterestRate(int basicInterestRateId)
        {
            this.authorizer.AssertCanDo(BasicInterestRateActions.View, basicInterestRateId);

            var errorList = this.basicInterestRatesRepository.CanDeleteBasicInterestRate(basicInterestRateId);

            return new ErrorsDO(errorList);
        }
    }
}
