using System.Collections.Generic;
using System.Linq;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Web.InfrastructureClasses.Charts;

namespace Eumis.Public.Web.Models.OPProfile
{
    public class OPProfileIndexModel
    {
        public List<ProgrammeBudgetBySource> ItemsBySource { get; set; }

        public List<ProgrammeBudgetWithContractedAndPayed> ItemsWithContractedAndPayed { get; set; }

        public IList<FinanceSource> Sources { get; set; }

        public decimal BudgetTotal
        {
            get
            {
                return this.ItemsWithContractedAndPayed.Select(i => i.Budget).Last();
            }
        }

        public decimal BudgetEuAmountTotal
        {
            get
            {
                return this.ItemsWithContractedAndPayed.Select(i => i.BudgetEuAmount).Last();
            }
        }

        public decimal BudgetBgAmountTotal
        {
            get
            {
                return this.ItemsWithContractedAndPayed.Select(i => i.BudgetBgAmount).Last();
            }
        }

        public decimal ContractedTotal
        {
            get
            {
                return this.ItemsWithContractedAndPayed.Select(i => i.Contracted).Last();
            }
        }

        public decimal ContractedEuAmountTotal
        {
            get
            {
                return this.ItemsWithContractedAndPayed.Select(i => i.ContractedEuAmount).Last();
            }
        }

        public decimal ContractedBgAmountTotal
        {
            get
            {
                return this.ItemsWithContractedAndPayed.Select(i => i.ContractedBgAmount).Last();
            }
        }

        public decimal PayedTotal
        {
            get
            {
                return this.ItemsWithContractedAndPayed.Select(i => i.Payed).Last();
            }
        }

        public decimal PayedBgAmountTotal
        {
            get
            {
                return this.ItemsWithContractedAndPayed.Select(i => i.PayedBgAmount).Last();
            }
        }

        public decimal PayedEuAmountTotal
        {
            get
            {
                return this.ItemsWithContractedAndPayed.Select(i => i.PayedEuAmount).Last();
            }
        }

        public decimal ContractedPercentExecTotal
        {
            get
            {
                if (this.BudgetTotal != 0)
                {
                    return DataUtils.Percent((this.ContractedBgAmountTotal + this.ContractedEuAmountTotal) / this.BudgetTotal);
                }

                return default(decimal);
            }
        }

        public decimal PayedPercentExecTotal
        {
            get
            {
                if (this.BudgetTotal != 0)
                {
                    return DataUtils.Percent(this.PayedTotal / this.BudgetTotal);
                }

                return default(decimal);
            }
        }

        public decimal CohesionFundTotal { get; set; }

        public decimal EuropeanRegionalDevelopmentFundTotal { get; set; }

        public decimal EuropeanSocialFundTotal { get; set; }

        public decimal FundForEuropeanAidToTheMostDeprivedTotal { get; set; }

        public decimal YouthEmploymentInitiativeTotal { get; set; }

        public decimal EFMDRTotal { get; set; }

        public decimal EZFRSRTotal { get; set; }

        public decimal FVSTotal { get; set; }

        public decimal FUMITotal { get; set; }

        public decimal OtherTotal { get; set; }

        public decimal EEAFMTotal { get; set; }

        public decimal NFMTotal { get; set; }

        public decimal BgAmountTotal { get; set; }

        public decimal Total
        {
            get
            {
                return this.CohesionFundTotal + this.EuropeanRegionalDevelopmentFundTotal + this.EuropeanSocialFundTotal + this.FundForEuropeanAidToTheMostDeprivedTotal + this.YouthEmploymentInitiativeTotal +
                    this.EFMDRTotal + this.EZFRSRTotal + this.FVSTotal + this.FUMITotal + this.OtherTotal + this.EEAFMTotal + this.NFMTotal + this.BgAmountTotal;
            }
        }

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

        public bool ShowEEAFM
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.EEAFM);
            }
        }

        public bool ShowNFM
        {
            get
            {
                return this.Sources != null && this.Sources.Contains(FinanceSource.NFM);
            }
        }

        public ChartRendererModel FundsChart { get; set; }

        public ChartRendererModel ExecutionsChart { get; set; }

        public ChartRendererModel BudgetChart { get; set; }
    }
}