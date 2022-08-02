using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    [Serializable]
    public class ContractPrivateNomenclature
    {
        public string gid { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }
    }
}
