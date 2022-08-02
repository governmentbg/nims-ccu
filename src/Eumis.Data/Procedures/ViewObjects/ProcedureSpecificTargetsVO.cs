using System.Collections.Generic;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureSpecificTargetsVO
    {
        public int ProcedureId { get; set; }

        public int SpecificTargetId { get; set; }

        public string SpecificTargetName { get; set; }

        public string InvestmentPriorityName { get; set; }

        public List<string> ProgrammePrioritiesNames { get; set; }

        public byte[] Version { get; set; }
    }
}
