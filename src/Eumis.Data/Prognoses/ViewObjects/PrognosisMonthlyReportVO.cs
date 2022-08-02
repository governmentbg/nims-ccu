using System.Collections.Generic;

namespace Eumis.Data.Prognoses.ViewObjects
{
    public class PrognosisMonthlyReportVO
    {
        public int ProgrammePriorityId { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public string ProgrammePriorityName { get; set; }

        public IList<PrognosisMonthlyReportItemVO> ReportItems { get; set; }
    }
}
