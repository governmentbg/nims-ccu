using Eumis.Domain.FIReimbursedAmounts;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.FIReimbursedAmounts.ViewObjects
{
    public class FIReimbursedAmountBasicDataVO
    {
        public int ReimbursedAmountId { get; set; }

        public string RegNumber { get; set; }

        public FIReimbursedAmountStatus Status { get; set; }

        public bool IsActivated { get; set; }

        public string IsDeletedNote { get; set; }

        public int ProgrammeId { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public byte[] Version { get; set; }
    }
}
