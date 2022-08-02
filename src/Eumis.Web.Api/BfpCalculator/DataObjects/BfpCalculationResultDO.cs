using System;
using Eumis.Common.Json;
using Eumis.Data.Companies.PortalViewObjects;
using Eumis.Domain.Companies;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Web.Api.BfpCalculator.DataObjects
{
    public class BfpCalculationResultDO
    {
        public BfpCalculationResultDO()
        {
        }

        public BfpCalculationResultDO(decimal euAmount, decimal bgAmount)
        {
            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
        }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal EuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal BgAmount { get; set; }
    }
}
