using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;

namespace Eumis.Public.Data.UmisVOs
{
    public class ProgrammeBudgetDetailedVO
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.NameAlt) ? this.Name : this.NameAlt;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public int PortalOrderNum { get; set; }

        public decimal BudgetTotal { get; set; }

        public decimal BudgetEU { get; set; }

        public decimal BudgetNational { get; set; }

        public int ProjectsCount { get; set; }

        public int ContractsCount { get; set; }

        public bool IncludeSelfAmount { get; set; }

        public decimal ContractTotal
        {
            get
            {
                return this.IncludeSelfAmount ?
                    this.ContractEU + this.ContractNational + this.ContractSelf :
                    this.ContractEU + this.ContractNational;
            }
        }

        public decimal ContractBFP
        {
            get
            {
                return this.ContractEU + this.ContractNational;
            }
        }

        public decimal ContractPercent
        {
            get
            {
                if (this.BudgetTotal != 0)
                {
                    return DataUtils.Percent(this.ContractBFP / this.BudgetTotal);
                }

                return default(decimal);
            }
        }

        public decimal ContractEU { get; set; }

        public decimal ContractNational { get; set; }

        public decimal ContractSelf { get; set; }

        public decimal PaidBFP
        {
            get
            {
                return this.PaidEU + this.PaidNational;
            }
        }

        public decimal PaidPercent
        {
            get
            {
                if (this.BudgetTotal != 0)
                {
                    return DataUtils.Percent(this.PaidBFP / this.BudgetTotal);
                }

                return default(decimal);
            }
        }

        public decimal PaidEU { get; set; }

        public decimal PaidNational { get; set; }

        public decimal ReceivedTotal { get; set; }

        public decimal ReceivedPercent
        {
            get
            {
                if (this.BudgetEU != 0)
                {
                    return DataUtils.Percent(this.ReceivedTotal / this.BudgetEU);
                }

                return default(decimal);
            }
        }

        public int? ProgrammeGroupId { get; set; }
    }
}
