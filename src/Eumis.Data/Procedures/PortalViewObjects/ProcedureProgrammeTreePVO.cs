using System.Collections.Generic;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureProgrammeTreePVO
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public IList<ProcedureProgrammePriorityTreePVO> ProgrammePriorities { get; set; }
    }
}
