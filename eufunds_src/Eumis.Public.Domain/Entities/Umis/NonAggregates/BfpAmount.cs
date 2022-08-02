using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class BfpAmount
    {
        public void SetAttributes(decimal? euAmount, decimal? bgAmount)
        {
            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.TotalAmount = euAmount.HasValue || bgAmount.HasValue ?
                (euAmount ?? 0) + (bgAmount ?? 0) :
                (decimal?)null;
        }

        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? TotalAmount { get; set; }
    }

    public class BfpAmountMap : ComplexTypeConfiguration<BfpAmount>
    {
        public BfpAmountMap()
        {
        }
    }
}
