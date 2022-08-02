using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EuReimbursedAmounts
{
    public class IrregularityExpenses
    {
        public decimal? BfpEuAmount { get; set; }

        public decimal? BfpBgAmount { get; set; }

        public decimal? BfpTotalAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        public decimal? TotalAmount { get; set; }

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
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class IrregularityExpensesMap : ComplexTypeConfiguration<IrregularityExpenses>
    {
        public IrregularityExpensesMap()
        {
        }
    }
}
