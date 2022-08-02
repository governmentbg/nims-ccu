using System.Collections.Generic;

namespace Eumis.Public.Data.UmisVOs
{
    public class OperationalProgramGroupVO
    {
        public OperationalProgramGroupVO()
        {
            this.OperationalPrograms = new List<ProgrammeBudgetDetailedVO>();
            this.Totals = new OperationalProgramTotalsVO();
        }

        public OperationalProgramGroupVO(string groupName)
        {
            this.GroupName = groupName;
        }

        public OperationalProgramGroupVO(string groupName, List<ProgrammeBudgetDetailedVO> operationalPrograms)
        {
            this.GroupName = groupName;
            this.OperationalPrograms = operationalPrograms;
            this.Totals = new OperationalProgramTotalsVO(operationalPrograms);
        }

        public string GroupName { get; set; }

        public List<ProgrammeBudgetDetailedVO> OperationalPrograms { get; set; }

        public OperationalProgramTotalsVO Totals { get; set; }
    }
}