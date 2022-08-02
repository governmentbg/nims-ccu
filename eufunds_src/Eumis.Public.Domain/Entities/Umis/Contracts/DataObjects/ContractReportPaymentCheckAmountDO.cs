namespace Eumis.Public.Domain.Entities.Umis.Contracts.DataObjects
{
    public class ContractReportPaymentCheckAmountDO
    {
        public int ContractReportPaymentCheckAmountId { get; set; }
        public int ContractReportPaymentCheckId { get; set; }

        public decimal? PaidEuAmount { get; set; }
        public decimal? PaidBgAmount { get; set; }
        public decimal? PaidBfpTotalAmount { get; set; }
        public decimal? PaidCrossAmount { get; set; }
    }
}
