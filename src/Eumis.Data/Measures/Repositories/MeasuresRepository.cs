using Eumis.Common.Db;
using Eumis.Data.Measures.ViewObjects;
using Eumis.Domain.Indicators;
using Eumis.Domain.Measures;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Measures.Repositories
{
    internal class MeasuresRepository : AggregateRepository<Measure>, IMeasuresRepository
    {
        public MeasuresRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<MeasuresVO> GetMeasures()
        {
            return (from measure in this.unitOfWork.DbContext.Set<Measure>()
                    select new MeasuresVO
                    {
                        MeasureId = measure.MeasureId,
                        ShortName = measure.ShortName,
                        Name = measure.Name,
                        NameAlt = measure.NameAlt,
                    })
                .ToList();
        }

        public IList<string> CanDeleteMeasure(int measureId)
        {
            var errors = new List<string>();

            if ((from m in this.unitOfWork.DbContext.Set<Measure>()
                 join i in this.unitOfWork.DbContext.Set<Indicator>() on m.MeasureId equals i.MeasureId
                 where m.MeasureId == measureId
                 select m).Any())
            {
                errors.Add("Мерната единица не може да бъде изтрита, защото е свързана към индикатор");
            }

            return errors;
        }
    }
}
