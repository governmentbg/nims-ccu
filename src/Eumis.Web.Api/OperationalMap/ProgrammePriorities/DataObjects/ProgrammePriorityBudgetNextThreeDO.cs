using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.DataObjects
{
    public class ProgrammePriorityBudgetNextThreeDO
    {
        public ProgrammePriorityBudgetNextThreeDO()
        {
        }

        public ProgrammePriorityBudgetNextThreeDO(decimal amountWithAdvances, decimal amountWithoutAdvances)
        {
            this.AmountWithAdvances = amountWithAdvances;
            this.AmountWithoutAdvances = amountWithoutAdvances;
        }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? AmountWithAdvances { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? AmountWithoutAdvances { get; set; }
    }
}
