using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class FinancialExecutionTable3ReportItem
    {
        public FinancialExecutionTable3ReportItem(
            string programme,
            decimal currYearGroup1PaymentsEuAmount,
            decimal currYearGroup2PaymentsEuAmount,
            decimal nextYearPaymentsEuAmount)
        {
            this.Programme = programme;
            this.RegionCategory = RegionCategory.LessDevelopedRegions;

            this.CurrYearGroup1PaymentsEuAmount = currYearGroup1PaymentsEuAmount;
            this.CurrYearGroup2PaymentsEuAmount = currYearGroup2PaymentsEuAmount;
            this.NextYearPaymentsEuAmount = nextYearPaymentsEuAmount;
        }

        public string Programme { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public RegionCategory RegionCategory { get; set; }

        public decimal CurrYearGroup1PaymentsEuAmount { get; set; }

        public decimal CurrYearGroup2PaymentsEuAmount { get; set; }

        public decimal NextYearPaymentsEuAmount { get; set; }
    }
}
