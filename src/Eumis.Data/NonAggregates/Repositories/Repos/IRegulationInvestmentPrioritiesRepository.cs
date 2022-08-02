using Eumis.Data.Core;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Repos
{
    public interface IRegulationInvestmentPrioritiesRepository : IRepository
    {
        RegulationInvestmentPriority GetRegulationInvestmentPriority(int investmentPriorityId);
    }
}
