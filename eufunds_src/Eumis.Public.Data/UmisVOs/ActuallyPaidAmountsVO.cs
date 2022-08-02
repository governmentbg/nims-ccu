using Eumis.Public.Common.Localization;

namespace Eumis.Public.Data.UmisVOs
{
    public class ActuallyPaidAmountsVO
    {
        public string ContractRegNumber { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNumber { get; set; }

        public decimal? ContractedTotalAmount { get; set; }

        public decimal? ContractedEuAmount { get; set; }

        public decimal? ContractedBgAmount { get; set; }

        public decimal? ContractedSelfAmount { get; set; }

        public decimal? ActuallyPaidTotalAmount { get; set; }

        public decimal? ActuallyPaidEuAmount { get; set; }

        public decimal? ActuallyPaidBgAmount { get; set; }
    }
}
