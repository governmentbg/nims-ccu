using System;
using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.ActuallyPaidAmounts.ViewObjects
{
    public class ActuallyPaidAmountVO
    {
        public int AmountId { get; set; }

        public string ProgrammeName { get; set; }

        public string ContractRegNumber { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ActuallyPaidAmountStatus StatusDescr { get; set; }

        public ActuallyPaidAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public PaymentReason PaymentReason { get; set; }

        public DateTime? PaymentDate { get; set; }

        public decimal? PaidBfpTotalAmount { get; set; }

        public int? ContractReportPaymentNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Domain.Contracts.ContractReportPaymentType? ContractReportPaymentType { get; set; }

        public string ProgrammePriorityName { get; set; }
    }
}
