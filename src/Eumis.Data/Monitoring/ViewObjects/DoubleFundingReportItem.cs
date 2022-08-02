namespace Eumis.Data.Monitoring.ViewObjects
{
    public class DoubleFundingReportItem
    {
        public int ContractId { get; set; }

        public string BeneficiaryName { get; set; }

        public string BeneficiaryUin { get; set; }

        public string ContractPartnerName { get; set; }

        public string ContractPartnerUin { get; set; }

        public string ContractRegNum { get; set; }

        public decimal? ContractTotalAmount { get; set; }

        public decimal? ContractBfpAmount { get; set; }
    }
}
