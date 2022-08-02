using System;
using System.Collections.Generic;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using UinTypeEnum = Eumis.Public.Domain.Entities.Umis.NonAggregates.UinType;

namespace Eumis.Public.Data.UmisVOs
{
    public class StatisticContractVO
    {
        private string companyUin;

        public int ContractId { get; set; }

        public string Name { get; set; }

        public string NameEN { get; set; }

        public string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.NameEN) ? this.Name : this.NameEN;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public string Description { get; set; }

        public string DescriptionEN { get; set; }

        public string TransDescription
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.DescriptionEN) ? this.Description : this.DescriptionEN;
                }
                else
                {
                    return this.Description;
                }
            }
        }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        private string TransCompanyName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.CompanyNameAlt) ? this.CompanyName : this.CompanyNameAlt;
                }
                else
                {
                    return this.CompanyName;
                }
            }
        }

        public string TransCompanyFullName
        {
            get
            {
                if (this.CompanyUinType == UinTypeEnum.PersonalBulstat)
                {
                    return Helper.AnonymizeName(this.TransCompanyName);
                }
                else
                {
                    return this.CompanyUin + " " + this.TransCompanyName;
                }
            }
        }

        public string CompanyUin
        {
            get => this.CompanyUinType == UinTypeEnum.PersonalBulstat ? string.Empty : this.companyUin;
            set => this.companyUin = value;
        }

        public UinTypeEnum CompanyUinType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public IEnumerable<string> NutsFullPathNames { get; set; }

        public IEnumerable<string> NutsFullPathNamesAlt { get; set; }

        public IEnumerable<string> TransNutsFullPathNames
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.NutsFullPathNamesAlt ?? this.NutsFullPathNames;
                }
                else
                {
                    return this.NutsFullPathNames;
                }
            }
        }

        public decimal ContractedEuAmount { get; set; }

        public decimal ContractedBgAmount { get; set; }

        public decimal ContractedSelfAmount { get; set; }

        public decimal ContractedBFPAmount
        {
            get
            {
                return this.ContractedEuAmount + this.ContractedBgAmount;
            }
        }

        public decimal ContractedTotalAmount
        {
            get
            {
                return this.ContractedBFPAmount + this.ContractedSelfAmount;
            }
        }

        public int? MonthsDuration
        {
            get
            {
                if (this.StartDate.HasValue && this.CompletionDate.HasValue)
                {
                    return DataUtils.GetMonthsDuration(this.StartDate.Value, this.CompletionDate.Value);
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal ContractedEuAmountPercentage
        {
            get
            {
                if (this.ContractedBFPAmount != 0)
                {
                    return this.ContractedEuAmount / this.ContractedBFPAmount * 100;
                }

                return default(decimal);
            }
        }

        public IEnumerable<InterventionCategory> InterventionCategories { get; set; }
    }
}
