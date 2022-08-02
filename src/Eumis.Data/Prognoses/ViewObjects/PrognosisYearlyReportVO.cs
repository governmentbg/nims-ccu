using System.Collections.Generic;

namespace Eumis.Data.Prognoses.ViewObjects
{
    public class PrognosisYearlyReportVO
    {
        public int ProgrammePriorityId { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public string ProgrammePriorityName { get; set; }

        public IList<PrognosisYearlyReportItemVO> ReportItems { get; set; }
    }
}
