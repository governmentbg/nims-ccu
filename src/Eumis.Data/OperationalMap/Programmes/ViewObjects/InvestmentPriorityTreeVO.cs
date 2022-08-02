using System.Collections.Generic;

namespace Eumis.Data.OperationalMap.Programmes.ViewObjects
{
    public class InvestmentPriorityTreeVO
    {
        public int InvestmentPriorityId { get; set; }

        public string Name { get; set; }

        public IList<SpecificTargetTreeVO> SpecificTargets { get; set; }
    }
}
