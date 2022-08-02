using System.Collections.Generic;
using Eumis.Data.ExpenseTypes.ViewObjects;
using Eumis.Domain.ExpenseTypes;

namespace Eumis.Data.ExpenseTypes.Repositories
{
    public interface IExpenseTypesRepository : IAggregateRepository<ExpenseType>
    {
        IList<ExpenseTypeVO> GetExpenseTypes();

        IList<string> CanDeleteExpenseType(int expenseTypeId);
    }
}
