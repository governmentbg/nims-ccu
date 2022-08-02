using System.Collections.Generic;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrrByQuarterReportVO
    {
        public IList<ReportIrregularityVersionVO> NewReportedIrregularities { get; set; }

        public IList<ReportIrregularityVersionVO> NewNotReportedIrregularities { get; set; }

        public IList<ReportIrregularityVersionVO> SubsequentReportedVersions { get; set; }

        public IList<ReportIrregularityVersionVO> SubsequentNotReportedVersions { get; set; }

        public IList<ReportIrregularityVersionVO> PreviousReportedVersions { get; set; }

        public IList<ReportIrregularityVersionVO> PreviousNotReportedVersions { get; set; }
    }
}
