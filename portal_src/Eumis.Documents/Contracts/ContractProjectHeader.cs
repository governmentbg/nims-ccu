using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{ 
    [Serializable]
    public class ContractProjectHeader
    {
        public string regNumber { get; set; }
        public DateTime? regDate { get; set; }
    }
}
