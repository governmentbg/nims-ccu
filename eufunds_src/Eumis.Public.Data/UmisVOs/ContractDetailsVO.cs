using System;
using System.Collections.Generic;
using System.Linq;

using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using UinTypeEnum = Eumis.Public.Domain.Entities.Umis.NonAggregates.UinType;

namespace Eumis.Public.Data.UmisVOs
{
    public class ContractDetailsVO
    {
        public int ContractId { get; set; }

        public string RegNumber { get; set; }

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

        public string TransCompanyName
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

        public IEnumerable<string> Funds { get; set; }

        public int ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeNameEN { get; set; }

        public string TransProgrammeName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ProgrammeNameEN) ? this.ProgrammeName : this.ProgrammeNameEN;
                }
                else
                {
                    return this.ProgrammeName;
                }
            }
        }

        public DateTime? ContractDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public ContractExecutionStatus? ExecutionStatus { get; set; }

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

        public IEnumerable<ContractActivityVO> Activities { get; set; }

        public IEnumerable<ContractPartnerVO> Partners { get; set; }

        public IEnumerable<ContractContractorVO> Contractors { get; set; }

        public IEnumerable<ContractSubcontractorVO> Subcontractors { get; set; }

        public IEnumerable<ContractSubcontractorVO> Members { get; set; }

        public decimal ContractedEuAmount { get; set; }

        public decimal ContractedBgAmount { get; set; }

        public decimal ContractedSelfAmount { get; set; }

        public decimal PaidEuAmount { get; set; }

        public decimal PaidBgAmount { get; set; }

        public bool IsHistoric { get; set; }

        public IEnumerable<ContractIndicatorVO> Indicators { get; set; }

        public IEnumerable<OfferVO> Offers { get; set; }

        public IList<FinanceSource> Sources { get; set; }

        #region Financial Information
        public IEnumerable<FinancialCorrectionVO> FinancialCorrections { get; set; }

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

        public List<ContractedFundsByYearAndSourceVO> ContractedFundsByYearAndSource { get; set; }

        public decimal CohesionFundTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.CohesionFund);
            }
        }

        public decimal EuropeanRegionalDevelopmentFundTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.EuropeanRegionalDevelopmentFund);
            }
        }

        public decimal EuropeanSocialFundTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.EuropeanSocialFund);
            }
        }

        public decimal FundForEuropeanAidToTheMostDeprivedTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.FundForEuropeanAidToTheMostDeprived);
            }
        }

        public decimal YouthEmploymentInitiativeTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.YouthEmploymentInitiative);
            }
        }

        public decimal EFMDRTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.EFMDR);
            }
        }

        public decimal EZFRSRTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.EZFRSR);
            }
        }

        public decimal FVSTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.FVS);
            }
        }

        public decimal FUMITotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.FUMI);
            }
        }

        public decimal OtherTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.Other);
            }
        }

        public decimal BgAmountTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.BgAmount);
            }
        }

        public decimal SelfAmountTotal
        {
            get
            {
                return this.ContractedFundsByYearAndSource.Sum(c => c.SelfAmount);
            }
        }

        public decimal Total
        {
            get
            {
                return this.CohesionFundTotal + this.EuropeanRegionalDevelopmentFundTotal + this.EuropeanSocialFundTotal + this.FundForEuropeanAidToTheMostDeprivedTotal + this.YouthEmploymentInitiativeTotal
                    + this.EFMDRTotal + this.EZFRSRTotal + this.FVSTotal + this.FUMITotal + this.OtherTotal + this.BgAmountTotal + this.SelfAmountTotal;
            }
        }

        public decimal ProcedureShareBgAmount { get; set; }

        public decimal ProcedureShareEuAmount { get; set; }

        public decimal EuPercent
        {
            get
            {
                if (this.ContractedTotalAmount > 0)
                {
                    return this.ProcedureShareEuAmount / (this.ProcedureShareBgAmount + this.ProcedureShareEuAmount) * 100;
                }
                else
                {
                    return 0m;
                }
            }
        }

        #endregion

        #region Show Flags

        public bool ShowERDF
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.EuropeanRegionalDevelopmentFund);
            }
        }

        public bool ShowCF
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.CohesionFund);
            }
        }

        public bool ShowESF
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.EuropeanSocialFund);
            }
        }

        public bool ShowFEAD
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.FundForEuropeanAidToTheMostDeprived);
            }
        }

        public bool ShowYEI
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.YouthEmploymentInitiative);
            }
        }

        public bool ShowNF
        {
            get
            {
                return true;
            }
        }

        public bool ShowSF
        {
            get
            {
                return true;
            }
        }

        public bool ShowEFMDR
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.EFMDR);
            }
        }

        public bool ShowEZFRSR
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.EZFRSR);
            }
        }

        public bool ShowFVS
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.FVS);
            }
        }

        public bool ShowFUMI
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.FUMI);
            }
        }

        public bool ShowOther
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.Other);
            }
        }

        #endregion

        public string StatusDescription
        {
            get
            {
                return this.ExecutionStatus.HasValue ? this.ExecutionStatus.GetEnumDescription() : string.Empty;
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
    }
}
