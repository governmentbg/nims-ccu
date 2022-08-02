using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractProcedureApplicationSection
    {
        public ContractEnumNomenclature section { get; set; }

        public bool isSelected { get; set; }

        public int orderNum { get; set; }

        public IList<ContractProcedureApplicationSectionAdditionalSetting> additionalSettings { get; set; }
    }
}
