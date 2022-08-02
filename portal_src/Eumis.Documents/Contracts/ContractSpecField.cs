using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractSpecField
    {
        public string gid { get; set; }
        public string title { get; set; }
        public string titleAlt { get; set; }
        public string description { get; set; }
        public string descriptionAlt { get; set; }
        public bool isRequired { get; set; }
        public bool isActive { get; set; }
        public int maxLength { get; set; }
    }
}
