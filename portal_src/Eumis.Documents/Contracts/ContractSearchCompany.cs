using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractSearchCompany 
    {
        public string uin { get; set; }
        public string uinType { get; set; }
        public string procedureCode { get; set; }
        public string token { get; set; }
    }
}
