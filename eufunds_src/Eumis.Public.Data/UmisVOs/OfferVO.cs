using System.Collections.Generic;

namespace Eumis.Public.Data.UmisVOs
{
    public class OfferVO
    {
        public string ProcurementPlanName { get; set; }

        public decimal Amount { get; set; }

        public List<ContractDifferentiatedPositionVO> ContractDifferentiatedPositions { get; set; }
    }
}
