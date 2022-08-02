using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class BfpAmount
    {
        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public void SetAttributes(decimal? euAmount, decimal? bgAmount)
        {
            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.TotalAmount = euAmount.HasValue || bgAmount.HasValue ?
                (euAmount ?? 0) + (bgAmount ?? 0) :
                (decimal?)null;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class BfpAmountMap : ComplexTypeConfiguration<BfpAmount>
    {
        public BfpAmountMap()
        {
        }
    }
}
