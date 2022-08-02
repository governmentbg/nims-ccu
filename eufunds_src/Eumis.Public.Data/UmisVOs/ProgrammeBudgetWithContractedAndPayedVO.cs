using System.Collections.Generic;

namespace Eumis.Public.Data.UmisVOs
{
    public class ProgrammeBudgetWithContractedAndPayedVO
    {
        public decimal BudgetTotal { get; set; }

        public decimal ContractedTotal { get; set; }

        public decimal PayedTotal { get; set; }

        public IList<ProgrammeBudgetWithContractedAndPayedChildVO> Items { get; set; }
    }
}
