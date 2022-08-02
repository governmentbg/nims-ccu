using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrrReportVersionDataVO
    {
        public int IrregularityId { get; set; }

        public int VersionId { get; set; }

        public string IrregularityNum { get; set; }

        public Quarter ReportQuarter { get; set; }

        public Year ReportYear { get; set; }
    }
}
