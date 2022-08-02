using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Data.ProgrammeGroups.ViewObjects
{
    public class FinanceSourceBudgetTotalsVO
    {
        public FinanceSource FinanceSource { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
