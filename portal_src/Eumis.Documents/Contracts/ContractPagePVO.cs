using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractPagePVO<T>
    {
        public List<T> results { get; set; }
        public int count { get; set; }
    }
}
