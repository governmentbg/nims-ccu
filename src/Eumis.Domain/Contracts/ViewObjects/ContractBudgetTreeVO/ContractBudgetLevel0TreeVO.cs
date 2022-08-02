using System.Collections.Generic;

namespace Eumis.Data.ContractReports.ViewObjects.ContractBudgetTree
{
    public class ContractBudgetLevel0TreeVO
    {
        public int ProgrammeId { get; set; }

        public string DisplayName { get; set; }

        public decimal EuAmount { get; set; }

        public decimal BgAmount { get; set; }

        public decimal SelfAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public IList<ContractBudgetLevel1TreeVO> Level1Items { get; set; }
    }
}
