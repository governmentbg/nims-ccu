using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.NonAggregates.Repositories.Suggestions;
using Eumis.Domain.ExpenseTypes;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Suggestions
{
    [RoutePrefix("api/suggestions/expenseSubTypes")]
    public class ExpenseSubTypeSuggestionsController : EntitySuggestionsController<ExpenseSubType>
    {
        private IExpenseSubTypeSuggestionsRepository expenseSubTypeSuggestionsRepository;

        public ExpenseSubTypeSuggestionsController(IExpenseSubTypeSuggestionsRepository expenseSubTypeSuggestionsRepository)
            : base(expenseSubTypeSuggestionsRepository)
        {
            this.expenseSubTypeSuggestionsRepository = expenseSubTypeSuggestionsRepository;
        }

        [Route("")]
        public IList<string> GetExpenseSubTypeSuggestions(int expenseTypeId, string term, int offset = 0, int? limit = null)
        {
            return this.expenseSubTypeSuggestionsRepository.GetExpenseSubTypeSuggestions(expenseTypeId, term, offset = 0, limit);
        }
    }
}
