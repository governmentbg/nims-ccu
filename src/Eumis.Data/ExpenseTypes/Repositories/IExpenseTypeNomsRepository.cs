using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.ExpenseTypes;

namespace Eumis.Data.ExpenseTypes.Repositories
{
    public interface IExpenseTypeNomsRepository : IEntityNomsRepository<ExpenseType, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetExpenseTypeNoms(int procedureId, int programmeId, string term, int offset = 0, int? limit = null);
    }
}
