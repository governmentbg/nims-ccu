using System;
using System.Collections.Generic;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;
using UinTypeEnum = Eumis.Public.Domain.Entities.Umis.NonAggregates.UinType;

namespace Eumis.Public.Data.UmisVOs
{
    public class Operations508ReportVO
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

        public string PostCode { get; set; }

        public string CountryName { get; set; }

        public string CountryNameAlt { get; set; }

        public string TransCountryName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English && !string.IsNullOrWhiteSpace(this.CountryNameAlt))
                {
                    return this.CountryNameAlt;
                }
                else
                {
                    return this.CountryName;
                }
            }
        }

        public string ProgrammePriorityName { get; set; }

        public string ProgrammePriorityNameAlt { get; set; }

        public string TransProgrammePriorityName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English && !string.IsNullOrWhiteSpace(this.ProgrammePriorityNameAlt))
                {
                    return this.ProgrammePriorityNameAlt;
                }
                else
                {
                    return this.ProgrammePriorityName;
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

        public IEnumerable<string> VesselIdentifiers { get; set; }
    }
}
