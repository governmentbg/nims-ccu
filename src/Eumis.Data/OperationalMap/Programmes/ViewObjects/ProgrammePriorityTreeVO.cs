using System.Collections.Generic;

namespace Eumis.Data.OperationalMap.Programmes.ViewObjects
{
    public class ProgrammePriorityTreeVO
    {
        public int ProgrammePriorityId { get; set; }

        public string Name { get; set; }

        public IList<InvestmentPriorityTreeVO> InvestmentPriorities { get; set; }
    }
}
