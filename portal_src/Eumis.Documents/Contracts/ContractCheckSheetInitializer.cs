using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractCheckSheetInitializer : ContractInitializer
    {
        public int? contractId { get; set; }

        public int? contractProcurementXmlId { get; set; }

        public List<ContractProcurementPlanPVO> procurementPlans { get; set; }
    }
}
