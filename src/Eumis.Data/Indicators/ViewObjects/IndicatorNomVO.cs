using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Indicators;

namespace Eumis.Data.Indicators.ViewObjects
{
    public class IndicatorNomVO : EntityNomVO
    {
        public int IndicatorId { get; set; }

        public int ProgrammeId { get; set; }

        public int MeasureId { get; set; }

        public bool HasGenderDivision { get; set; }

        public int? SourceMapNodeId { get; set; }
    }
}
