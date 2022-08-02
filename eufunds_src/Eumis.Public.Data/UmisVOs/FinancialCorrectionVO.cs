using Eumis.Public.Resources;

namespace Eumis.Public.Data.UmisVOs
{
    public class FinancialCorrectionVO
    {
        private string contractorName;

        public string ImposingReason { get; set; }

        public decimal? Percent { get; set; }

        public decimal? BfpAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public string ContractorName
        {
            get => this.contractorName ?? Texts.Project_Details_FinancialCorrections_NoContractor;
            set => this.contractorName = value;
        }
    }
}
