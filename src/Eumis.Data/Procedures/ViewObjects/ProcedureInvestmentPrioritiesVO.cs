using System.Collections.Generic;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureInvestmentPrioritiesVO
    {
        public int ProcedureId { get; set; }

        public int InvestmentPriorityId { get; set; }

        public string InvestmentPriorityName { get; set; }

        public bool HasLinkedSpecificTarget { get; set; }

        public IList<string> ProgrammePrioritiesNames { get; set; }

        public byte[] Version { get; set; }
    }
}
