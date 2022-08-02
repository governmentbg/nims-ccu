namespace Eumis.Public.Data.UmisVOs
{
    public class ProgrammeBudgetWithContractedAndPayedChildVO
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
    }
}
