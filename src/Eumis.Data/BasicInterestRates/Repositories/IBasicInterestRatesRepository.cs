using System.Collections.Generic;
using Eumis.Data.BasicInterestRates.ViewObjects;
using Eumis.Domain.BasicInterestRates;

namespace Eumis.Data.BasicInterestRates.Repositories
{
    public interface IBasicInterestRatesRepository : IAggregateRepository<BasicInterestRate>
    {
        IList<BasicInterestRateVO> GetBasicInterestRates();

        IList<string> CanDeleteBasicInterestRate(int basicInterestRateId);
    }
}
