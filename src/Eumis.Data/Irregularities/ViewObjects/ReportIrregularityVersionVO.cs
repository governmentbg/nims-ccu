using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class ReportIrregularityVersionVO
    {
        public int IrregularityId { get; set; }

        public int IrregularityVersionId { get; set; }

        public int OrderNum { get; set; }

        public string IrregularityRegNumber { get; set; }

        public Quarter? ReportQuarter { get; set; }

        public Year? ReportYear { get; set; }

        public bool ShouldReportToOlaf { get; set; }

        public IrregularityReasonNotReportingToOlaf? ReasonNotReportingToOlaf { get; set; }
    }
}
