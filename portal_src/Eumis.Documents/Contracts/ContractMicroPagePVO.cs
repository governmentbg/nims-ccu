using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractMicroPagePVO<T>
    {
        public string versionNum { get; set; }
        public string contractNumber { get; set; }

        public ContractPagePVO<T> items { get; set; }
    }
}
