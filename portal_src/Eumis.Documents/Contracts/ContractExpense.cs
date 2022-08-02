using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractExpense
    {
        public string gid { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }
        public bool isActive { get; set; }
        public string programmePriorityCode { get; set; }
        public int orderNum { get; set; }
        public decimal euPercent { get; set; }
        public ContractEnumNomenclature financeSource { get; set; }
        public ContractEnumNomenclature aidMode { get; set; }
        public List<ContractExpenseDetails> details { get; set; }
    }
}
