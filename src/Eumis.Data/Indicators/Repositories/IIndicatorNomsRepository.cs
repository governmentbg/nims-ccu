using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Indicators.ViewObjects;
using Eumis.Domain.Indicators;

namespace Eumis.Data.Indicators.Repositories
{
    public interface IIndicatorNomsRepository : IEntityNomsRepository<Indicator, IndicatorNomVO>
    {
        IEnumerable<IndicatorNomVO> GetUnusedMapNodeIndicators(
            int mapNodeId,
            string term,
            int offset = 0,
            int? limit = null);

        IEnumerable<IndicatorNomVO> GetUnusedProcedureIndicators(
            int procedureId,
            string term,
            int offset = 0,
            int? limit = null);
    }
}