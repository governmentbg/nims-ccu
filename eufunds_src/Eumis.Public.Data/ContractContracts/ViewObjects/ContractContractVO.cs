using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;

namespace Eumis.Public.Data.ContractContracts.ViewObjects
{
    public class ContractContractVO
    {
        public string ContractContractNumber { get; set; }

        public string ContractProcurementPlanName { get; set; }

        public string ContractDifferentiatedPositions { get; set; }

        public int ContractId { get; set; }

        public string ContractName { get; set; }

        public string ContractNameAlt { get; set; }

        public virtual string ContractTransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.ContractNameAlt;
                }
                else
                {
                    return this.ContractName;
                }
            }
        }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public string CompanyTransName
        {
            get
            {
                string transName;

                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    transName = string.IsNullOrEmpty(this.CompanyNameAlt) ? this.CompanyName : this.CompanyNameAlt;
                }
                else
                {
                    transName = this.CompanyName;
                }

                if (this.CompanyUinType == UinType.PersonalBulstat)
                {
                    transName = Helper.AnonymizeName(transName);
                }

                return transName;
            }
        }

        public string CompanyUin { get; set; }

        public UinType? CompanyUinType { get; set; }

        public string CompanyTypeName { get; set; }

        public string CompanyTypeNameAlt { get; set; }

        public virtual string CompanyTypeTransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.CompanyTypeNameAlt) ? this.CompanyTypeName : this.CompanyTypeNameAlt;
                }
                else
                {
                    return this.CompanyTypeName;
                }
            }
        }

        public string ContractContractorName { get; set; }

        public string ContractContractorNameAlt { get; set; }

        public string ContractContractorTransName
        {
            get
            {
                string transName;

                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    transName = string.IsNullOrEmpty(this.ContractContractorNameAlt) ? this.ContractContractorName : this.ContractContractorNameAlt;
                }
                else
                {
                    transName = this.ContractContractorName;
                }

                if (this.ContractContractorUinType == UinType.PersonalBulstat)
                {
                    transName = Helper.AnonymizeName(transName);
                }

                return transName;
            }
        }

        public string ContractContractorUin { get; set; }

        public UinType? ContractContractorUinType { get; set; }

        public decimal TotalFundedValue { get; set; }

        public string ErrandAreaName { get; set; }

        public string ErrandLegalActName { get; set; }

        public string ErrandTypeName { get; set; }

        public DateTime ContractContractEndDate { get; set; }
    }
}
