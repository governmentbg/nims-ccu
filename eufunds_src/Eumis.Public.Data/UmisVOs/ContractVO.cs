using System;
using System.Collections.Generic;
using System.Text;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using UinTypeEnum = Eumis.Public.Domain.Entities.Umis.NonAggregates.UinType;

namespace Eumis.Public.Data.UmisVOs
{
    public class ContractVO
    {
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

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public string TransCompanyName
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

        public string TransCompanyFullName
        {
            get
            {
                if (this.CompanyUinType == UinTypeEnum.PersonalBulstat)
                {
                    return this.TransCompanyName;
                }
                else
                {
                    return this.CompanyUin + " " + this.TransCompanyName;
                }
            }
        }

        public string CompanyUin { get; set; }

        public UinTypeEnum CompanyUinType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public int? MonthsDuration { get; set; }

        public string BeneficiarySeatCountry { get; set; }

        public string BeneficiarySeatCountryAlt { get; set; }

        public string TransBeneficiarySeatCountry
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.BeneficiarySeatCountryAlt) ? this.BeneficiarySeatCountry : this.BeneficiarySeatCountryAlt;
                }
                else
                {
                    return this.BeneficiarySeatCountry;
                }
            }
        }

        public int? BeneficiarySeatCountryId { get; set; }

        public int? BeneficiarySeatSettlementId { get; set; }

        public string BeneficiarySeatSettlement { get; set; }

        public string BeneficiarySeatSettlementAlt { get; set; }

        public string TransBeneficiarySeatSettlement
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.BeneficiarySeatSettlementAlt) ? this.BeneficiarySeatSettlement : this.BeneficiarySeatSettlementAlt;
                }
                else
                {
                    return this.BeneficiarySeatSettlement;
                }
            }
        }

        public string BeneficiarySeatAddress { get; set; }

        public string BeneficiarySeatPostCode { get; set; }

        public string BeneficiarySeatStreet { get; set; }

        public NutsLevel? NutsLevel { get; set; }

        public IEnumerable<string> NutsFullPathNames { get; set; }

        public IEnumerable<string> NutsFullPathNamesEN { get; set; }

        public IEnumerable<string> TransNutsFullPathNames
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.NutsFullPathNamesEN;
                }

                return this.NutsFullPathNames;
            }
        }

        public decimal ContractedEuAmount { get; set; }

        public decimal ContractedBgAmount { get; set; }

        public decimal ContractedSelfAmount { get; set; }

        public decimal PaidEuAmount { get; set; }

        public decimal PaidBgAmount { get; set; }

        public string RegNumber { get; set; }

        public ContractExecutionStatus? ExecutionStatus { get; set; }

        public bool IsHistoric { get; set; }

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

        public decimal PaidTotalAmount
        {
            get
            {
                return this.PaidEuAmount + this.PaidBgAmount;
            }
        }

        public string StatusDescription
        {
            get
            {
                return this.ExecutionStatus.HasValue ? this.ExecutionStatus.GetEnumDescription() : string.Empty;
            }
        }

        public string Seat
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(this.TransBeneficiarySeatCountry);

                if (!string.IsNullOrWhiteSpace(this.TransBeneficiarySeatSettlement))
                {
                    sb.Append(", " + this.TransBeneficiarySeatSettlement);
                }

                if (!string.IsNullOrWhiteSpace(this.BeneficiarySeatPostCode))
                {
                    sb.Append(", " + this.BeneficiarySeatPostCode);
                }

                if (!string.IsNullOrWhiteSpace(this.BeneficiarySeatStreet))
                {
                    sb.Append(", " + this.BeneficiarySeatStreet);
                }

                if (!string.IsNullOrWhiteSpace(this.BeneficiarySeatAddress))
                {
                    sb.Append(", " + this.BeneficiarySeatAddress);
                }

                return sb.ToString();
            }
        }
    }
}
