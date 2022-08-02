using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Web.Api.BfpCalculator.DataObjects
{
    public class BfpCalculationInputDO
    {
        public BfpCalculationInputDO()
        {
        }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal BfpTotalAmount { get; set; }
    }
}
