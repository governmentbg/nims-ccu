using System;
namespace Eumis.Documents.Contracts
{
    public class ContractApprovedIndicator
    {
        public decimal approvedCumulativeAmountMen { get; set; }

        public decimal approvedCumulativeAmountTotal { get; set; }

        public decimal approvedCumulativeAmountWomen { get; set; }

        public string contractIndicatorGid { get; set; }
    }
}