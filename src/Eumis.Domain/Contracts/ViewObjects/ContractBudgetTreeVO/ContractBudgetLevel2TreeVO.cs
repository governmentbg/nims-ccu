using System;
using System.Collections.Generic;

namespace Eumis.Data.ContractReports.ViewObjects.ContractBudgetTree
{
    public class ContractBudgetLevel2TreeVO
    {
        public int ContractBudgetLevel2Id { get; set; }

        public Guid Gid { get; set; }

        public string DisplayName { get; set; }

        public int OrderNum { get; set; }

        public decimal EuAmount { get; set; }

        public decimal BgAmount { get; set; }

        public decimal SelfAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public IList<ContractBudgetLevel3TreeVO> Level3Items { get; set; }
    }
}
