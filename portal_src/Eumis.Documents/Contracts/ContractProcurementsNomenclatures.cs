using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractProcurementsNomenclatures
    {
        public List<ContractPrivateNomenclature> irregularities { get; set; }
        public List<ContractPrivateNomenclature> financialCorrections { get; set; }
    }
}
