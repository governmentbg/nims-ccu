namespace Eumis.Data.Monitoring.ViewObjects
{
    public class MonitoringIndicatorPeriodAmountVO
    {
        public MonitoringIndicatorPeriodAmountVO(
            decimal cumulativeAmountTotal,
            decimal? cumulativeAmountMen,
            decimal? cumulativeAmountWomen,
            decimal periodAmountTotal,
            decimal? periodAmountMen,
            decimal? periodAmountWomen)
        {
            this.AmountTotal = cumulativeAmountTotal + periodAmountTotal;
            this.AmountMen = cumulativeAmountMen.HasValue || periodAmountMen.HasValue ?
                (cumulativeAmountMen ?? 0) + (periodAmountMen ?? 0) : (decimal?)null;
            this.AmountWomen = cumulativeAmountWomen.HasValue || periodAmountWomen.HasValue ?
                (cumulativeAmountWomen ?? 0) + (periodAmountWomen ?? 0) : (decimal?)null;
        }

        public MonitoringIndicatorPeriodAmountVO(decimal? amountTotal, decimal? amountMen, decimal? amountWomen)
        {
            this.AmountTotal = amountTotal;
            this.AmountMen = amountMen;
            this.AmountWomen = amountWomen;
        }

        public decimal? AmountTotal { get; set; }

        public decimal? AmountMen { get; set; }

        public decimal? AmountWomen { get; set; }
    }
}
