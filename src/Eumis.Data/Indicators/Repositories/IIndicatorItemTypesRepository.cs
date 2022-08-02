using Eumis.Data.Indicators.ViewObjects;
using Eumis.Domain.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Indicators.Repositories
{
    public interface IIndicatorItemTypesRepository : IAggregateRepository<IndicatorItemType>
    {
        IList<IndicatorItemTypesVO> GetIndicatorTypes();

        IList<string> CanDeleteIndicatorType(int indicatorItemTypeId);
    }
}
