using Eumis.Data.Measures.ViewObjects;
using Eumis.Domain.Measures;
using System.Collections.Generic;

namespace Eumis.Data.Measures.Repositories
{
    public interface IMeasuresRepository : IAggregateRepository<Measure>
    {
        IList<MeasuresVO> GetMeasures();

        IList<string> CanDeleteMeasure(int measureId);
    }
}
