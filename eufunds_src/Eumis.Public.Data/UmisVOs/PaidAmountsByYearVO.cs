namespace Eumis.Public.Data.UmisVOs
{
    public class PaidAmountsByYearVO
    {
        public int Year { get; set; }

        public decimal PaidEuAmount { get; set; }

        public decimal PaidBgAmount { get; set; }

        public decimal ContractedAmount { get; set; }
    }
}
