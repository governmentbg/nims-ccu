using R_10000;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractDirectionPair
    {
        public PrivateNomenclature direction { get; set; }

        public PrivateNomenclature subDirection { get; set; }
    }
}
