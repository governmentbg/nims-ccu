using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Domain.ExpenseTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Suggestions
{
    internal class ExpenseSubTypeSuggestionsRepository : Repository, IExpenseSubTypeSuggestionsRepository
    {
        public ExpenseSubTypeSuggestionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<string> GetSuggestions(string term, int offset = 0, int? limit = null)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetExpenseSubTypeSuggestions(int expenseTypeId, string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<ExpenseSubType>()
                .And(e => e.ExpenseTypeId == expenseTypeId)
                .AndStringContains(e => e.Name, term);

            return this.unitOfWork.DbContext.Set<ExpenseSubType>()
                .Where(predicate)
                .Select(e => e.Name)
                .OrderBy(e => e)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }
    }
}
