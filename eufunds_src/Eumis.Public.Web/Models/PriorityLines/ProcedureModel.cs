using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Localization;

namespace Eumis.Public.Web.Models.PriorityLines
{
    public class ProcedureModel
    {
        public int ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string ProcedureCode { get; set; }

        public decimal BudgetTotalAmount { get; set; }

        public decimal ContractedEuAmount { get; set; }

        public decimal ContractedBgAmount { get; set; }

        public decimal ContractedSelfAmount { get; set; }

        public decimal ContractedTotalAmount { get; set; }

        public decimal ContractedPercent
        {
            get
            {
                if (this.BudgetTotalAmount != 0)
                {
                    return DataUtils.Percent(this.ContractedTotalAmount / this.BudgetTotalAmount);
                }

                return default(decimal);
            }
        }

        public decimal PayedEuAmount { get; set; }

        public decimal PayedBgAmount { get; set; }

        public decimal PayedTotalAmount { get; set; }

        public decimal PayedPercent
        {
            get
            {
                if (this.BudgetTotalAmount != 0)
                {
                    return DataUtils.Percent(this.PayedTotalAmount / this.BudgetTotalAmount);
                }

                return default(decimal);
            }
        }

        public string TransProcedureName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ProcedureNameAlt) ? this.ProcedureName : this.ProcedureNameAlt;
                }
                else
                {
                    return this.ProcedureName;
                }
            }
        }
    }
}