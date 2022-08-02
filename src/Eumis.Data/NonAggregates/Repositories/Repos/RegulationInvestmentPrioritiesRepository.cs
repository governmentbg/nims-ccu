using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Repos
{
    internal class RegulationInvestmentPrioritiesRepository : Repository, IRegulationInvestmentPrioritiesRepository
    {
        public RegulationInvestmentPrioritiesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public RegulationInvestmentPriority GetRegulationInvestmentPriority(int investmentPriorityId)
        {
            return (from ip in this.unitOfWork.DbContext.Set<RegulationInvestmentPriority>()
                    where ip.InvestmentPriorityId == investmentPriorityId
                    select ip).SingleOrDefault();
        }
    }
}
