using System.Collections.Generic;
using Eumis.Data.InterestSchemes.ViewObjects;
using Eumis.Domain.InterestSchemes;

namespace Eumis.Data.InterestSchemes.Repositories
{
    public interface IInterestSchemesRepository : IAggregateRepository<InterestScheme>
    {
        IList<InterestSchemeVO> GetInterestSchemes();

        IList<string> CanDelete(int interestSchemeId);
    }
}
