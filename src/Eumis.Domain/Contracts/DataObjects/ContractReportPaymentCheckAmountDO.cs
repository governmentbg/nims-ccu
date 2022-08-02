using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportPaymentCheckAmountDO
    {
        public ContractReportPaymentCheckAmountDO()
        {
        }

        public ContractReportPaymentCheckAmountDO(ContractReportPaymentCheckAmount contractReportPaymentCheckAmount)
        {
            this.ContractReportPaymentCheckAmountId = contractReportPaymentCheckAmount.ContractReportPaymentCheckAmountId;
            this.ContractReportPaymentCheckId = contractReportPaymentCheckAmount.ContractReportPaymentCheckId;

            this.ProgrammePriorityId = contractReportPaymentCheckAmount.ProgrammePriorityId;

            this.ApprovedEuAmount = contractReportPaymentCheckAmount.ApprovedEuAmount;
            this.ApprovedBgAmount = contractReportPaymentCheckAmount.ApprovedBgAmount;
            this.ApprovedBfpTotalAmount = contractReportPaymentCheckAmount.ApprovedBfpTotalAmount;
            this.ApprovedCrossAmount = contractReportPaymentCheckAmount.ApprovedCrossAmount;
            this.ApprovedSelfAmount = contractReportPaymentCheckAmount.ApprovedSelfAmount;
            this.ApprovedTotalAmount = contractReportPaymentCheckAmount.ApprovedTotalAmount;

            this.PaidEuAmount = contractReportPaymentCheckAmount.PaidEuAmount;
            this.PaidBgAmount = contractReportPaymentCheckAmount.PaidBgAmount;
            this.PaidBfpTotalAmount = contractReportPaymentCheckAmount.PaidBfpTotalAmount;
            this.PaidCrossAmount = contractReportPaymentCheckAmount.PaidCrossAmount;
        }

        public int ContractReportPaymentCheckAmountId { get; set; }

        public int ContractReportPaymentCheckId { get; set; }

        public int ProgrammePriorityId { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedCrossAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidCrossAmount { get; set; }
    }
}
