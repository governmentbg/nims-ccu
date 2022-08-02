using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularityExpensesDO
    {
        [JsonConverter(typeof(MoneyConverter))]
        public decimal EuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal BgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal SelfAmount { get; set; }
    }
}
