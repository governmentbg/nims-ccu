using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    [Serializable]
    public class ContractPublicNomenclature
    {
        public string code { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }
        public bool isActive { get; set; }
    }
}
