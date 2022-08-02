using System;
using System.Collections.Generic;
using Eumis.Data.Indicators.ViewObjects;
using Eumis.Domain.Indicators;

namespace Eumis.Data.Indicators.Repositories
{
    public interface IIndicatorsRepository : IAggregateRepository<Indicator>
    {
        IList<Indicator> FindAll(int[] ids);

        IList<IndicatorsVO> GetIndicators(int[] programmeIds);

        IList<string> CanDeleteIndicator(int indicatorId);

        int GetIndicatorIdByGid(Guid gid);
    }
}
