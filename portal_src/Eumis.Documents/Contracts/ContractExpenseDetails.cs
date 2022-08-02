using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractExpenseDetails
    {
        public string gid { get; set; }
        public string note { get; set; }
        public int orderNum { get; set; }
    }
}
