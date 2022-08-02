using Eumis.Common.Db;
using Eumis.Data.Indicators.ViewObjects;
using Eumis.Domain.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Indicators.Repositories
{
    internal class IndicatorItemTypesRepository : AggregateRepository<IndicatorItemType>, IIndicatorItemTypesRepository
    {
        public IndicatorItemTypesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<string> CanDeleteIndicatorType(int indicatorItemTypeId)
        {
            var usedInIndicators = (from i in this.unitOfWork.DbContext.Set<Indicator>().Where(t => t.IndicatorItemTypeId == indicatorItemTypeId)
                                    join it in this.unitOfWork.DbContext.Set<IndicatorItemType>() on i.IndicatorItemTypeId equals it.IndicatorItemTypeId
                                    select i)
                                   .ToList();

            return usedInIndicators.Select(i => $"Видът се ипозлзва в индикатор: {i.Name}").ToList();
        }

        public IList<IndicatorItemTypesVO> GetIndicatorTypes()
        {
            var list = this.Set().Select(t => new IndicatorItemTypesVO
            {
                IndicatorItemTypeId = t.IndicatorItemTypeId,
                Name = t.Name,
                NameAlt = t.NameAlt,
            }).ToList();

            return list;
        }
    }
}
