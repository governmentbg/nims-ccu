using System.Collections.Generic;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureProgrammePriorityTreePVO
    {
        public int Number { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public IList<ProcedureTreePVO> Procedures { get; set; }
    }
}
