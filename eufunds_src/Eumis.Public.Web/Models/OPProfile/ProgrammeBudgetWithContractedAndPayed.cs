using Eumis.Public.Common.Helpers;

namespace Eumis.Public.Web.Models.OPProfile
{
    public class ProgrammeBudgetWithContractedAndPayed
    {
        public string Year { get; set; }

        public decimal Budget { get; set; }

        public decimal BudgetEuAmount { get; set; }

        public decimal BudgetBgAmount { get; set; }

        public decimal Contracted { get; set; }

        public decimal ContractedBgAmount { get; set; }

        public decimal ContractedEuAmount { get; set; }

        public decimal Payed { get; set; }

        public decimal PayedBgAmount { get; set; }

        public decimal PayedEuAmount { get; set; }

        public decimal ContractedPercentExec
        {
            get
            {
                if (this.Budget != 0)
                {
                    return DataUtils.Percent((this.ContractedBgAmount + this.ContractedEuAmount) / this.Budget);
                }

                return default(decimal);
            }
        }

        public decimal PayedPercentExec
        {
            get
            {
                if (this.Budget != 0)
                {
                    return DataUtils.Percent(this.Payed / this.Budget);
                }

                return default(decimal);
            }
        }
    }
}