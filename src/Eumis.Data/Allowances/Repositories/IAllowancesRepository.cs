using System.Collections.Generic;
using Eumis.Data.Allowances.ViewObjects;
using Eumis.Domain.Allowances;

namespace Eumis.Data.Allowances.Repositories
{
    public interface IAllowancesRepository : IAggregateRepository<Allowance>
    {
        IList<AllowanceVO> GetAllowances();

        IList<string> CanDeleteAllowance(int allowanceId);
    }
}
