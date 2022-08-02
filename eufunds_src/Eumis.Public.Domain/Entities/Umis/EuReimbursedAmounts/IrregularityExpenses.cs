using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EuReimbursedAmounts
{
    public class IrregularityExpenses
    {
        public void SetAttributes(
            decimal? bfpEuAmount,
            decimal? bfpBgAmount,
            decimal? selfAmount)
        {
            this.BfpEuAmount = bfpEuAmount;
            this.BfpBgAmount = bfpBgAmount;
            this.SelfAmount = selfAmount;

            this.BfpTotalAmount = bfpEuAmount.HasValue || bfpBgAmount.HasValue ?
                (bfpEuAmount ?? 0) + (bfpBgAmount ?? 0) :
                (decimal?)null;
            this.TotalAmount = bfpEuAmount.HasValue || bfpBgAmount.HasValue || selfAmount.HasValue ?
                (bfpEuAmount ?? 0) + (bfpBgAmount ?? 0) + (selfAmount ?? 0) :
                (decimal?)null;
        }

        public decimal? BfpEuAmount { get; set; }

        public decimal? BfpBgAmount { get; set; }

        public decimal? BfpTotalAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        public decimal? TotalAmount { get; set; }
    }

    public class IrregularityExpensesMap : ComplexTypeConfiguration<IrregularityExpenses>
    {
        public IrregularityExpensesMap()
        {
        }
    }
}
