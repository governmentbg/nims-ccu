using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractSpendingPlanPagePVO : ContractPagePVO<ContractSpendingPlanPVO>
    {
        public bool canCreate { get; set; }
    }

    public class ContractSpendingPlanPVO
    {
        public Guid gid { get; set; }

        public ContractEnumNomenclature source { get; set; }
                                        
        public ContractEnumNomenclature status { get; set; }

        public DateTime? createDate { get; set; }

        public DateTime? modifyDate { get; set; }
    }
}
