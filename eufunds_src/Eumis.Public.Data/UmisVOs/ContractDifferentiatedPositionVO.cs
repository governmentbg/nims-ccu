using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Data.UmisVOs
{
    public class ContractDifferentiatedPositionVO
    {
        private string contractorName;
        private string contractorNameAlt;

        public string Name { get; set; }

        public string ContractorName
        {
            get => this.ContractorUinType == UinType.PersonalBulstat ? Helper.AnonymizeName(this.contractorName) : this.contractorName;
            set => this.contractorName = value;
        }

        public string ContractorNameAlt
        {
            get => this.ContractorUinType == UinType.PersonalBulstat ? Helper.AnonymizeName(this.contractorNameAlt) : this.contractorNameAlt;
            set => this.contractorNameAlt = value;
        }

        public UinType? ContractorUinType { get; set; }

        public decimal? TotalFundedValue { get; set; }

        public string TransContractorName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ContractorNameAlt) ? this.ContractorName : this.ContractorNameAlt;
                }
                else
                {
                    return this.ContractorName;
                }
            }
        }
    }
}
