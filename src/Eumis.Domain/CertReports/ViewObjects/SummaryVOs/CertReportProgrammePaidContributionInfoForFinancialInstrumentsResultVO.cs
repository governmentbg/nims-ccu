using System.Collections.Generic;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportProgrammePaidContributionInfoForFinancialInstrumentsResultVO
    {
        public int Version { get; } = 2;

        public IList<ProgrammePaidContributionInfoForFinancialInstrumentsVO> Items { get; set; }

        public ProgrammePaidContributionInfoForFinancialInstrumentsVO Total { get; set; }
    }
}
