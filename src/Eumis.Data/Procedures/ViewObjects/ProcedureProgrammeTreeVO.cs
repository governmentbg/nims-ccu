using System.Collections.Generic;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureProgrammeTreeVO
    {
        public int ProgrammeId { get; set; }

        public string Name { get; set; }

        public IList<ProcedureProgrammePriorityTreeVO> ProgrammePriorities { get; set; }
    }
}
