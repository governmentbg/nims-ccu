using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.ExpenseTypes;

namespace Eumis.Data.NonAggregates.Repositories.Suggestions
{
    public interface IExpenseSubTypeSuggestionsRepository : IEntitySuggestionsRepository<ExpenseSubType>
    {
        IList<string> GetExpenseSubTypeSuggestions(int expenseTypeId, string term, int offset = 0, int? limit = null);
    }
}
