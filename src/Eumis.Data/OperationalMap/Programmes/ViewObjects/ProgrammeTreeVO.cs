using System.Collections.Generic;

namespace Eumis.Data.OperationalMap.Programmes.ViewObjects
{
    public class ProgrammeTreeVO
    {
        public int ProgrammeId { get; set; }

        public string Name { get; set; }

        public IList<ProgrammePriorityTreeVO> ProgrammePriorities { get; set; }
    }
}
