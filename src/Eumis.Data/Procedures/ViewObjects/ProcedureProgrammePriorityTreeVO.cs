using System.Collections.Generic;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureProgrammePriorityTreeVO
    {
        public int ProgrammeId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public string Name { get; set; }

        public IList<ProcedureTreeVO> Procedures { get; set; }
    }
}
