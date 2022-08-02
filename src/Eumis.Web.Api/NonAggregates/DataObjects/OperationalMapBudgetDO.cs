using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Web.Api.OperationalMap.Programmes.DataObjects
{
    public class OperationalMapBudgetDO
    {
        public OperationalMapBudgetDO()
        {
        }

        public OperationalMapBudgetDO(decimal bgAmount)
        {
            this.BgAmount = bgAmount;
        }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BgAmount { get; set; }
    }
}
