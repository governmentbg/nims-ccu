using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractProgrammePriority
    {
        public string programmePriorityGid { get; set; }
        public string programmePriorityCode { get; set; }
        public string programmePriorityName { get; set; }
        public string programmePriorityNameAlt { get; set; }
        public IList<InvestmentPriorityPVO> investmentPriorities { get; set; }

        public IList<ContractEnumNomenclature> financeSources { get; set; }
    }

    public class InvestmentPriorityPVO
    {
        public string investmentPriorityGid { get; set; }

        public string investmentPriorityCode { get; set; }

        public string investmentPriorityName { get; set; }

        public IList<SpecificTargetPVO> specificTargets { get; set; }
    }

    public class SpecificTargetPVO
    {
        public string specificTargetGid { get; set; }

        public string specificTargetCode { get; set; }

        public string specificTargetName { get; set; }
    }
}
