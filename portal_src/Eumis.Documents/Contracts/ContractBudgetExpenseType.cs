using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractBudgetExpenseType
    {
        public string gid { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }
        public int orderNum { get; set; }
        public List<ContractExpense> expenses { get; set; }
        public bool isActive { get; set; }
    }
}
