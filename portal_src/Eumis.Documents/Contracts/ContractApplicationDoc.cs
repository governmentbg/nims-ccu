using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractApplicationDoc
    {
        public string gid { get; set; }
        public string name { get; set; }
        public bool isRequired { get; set; }
        public bool isOriginal { get; set; }
        public bool isActive { get; set; }
        public bool isSignatureRequired { get; set; }
        public string extension { get; set; }
    }
}
