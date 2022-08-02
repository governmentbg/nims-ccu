using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.ActuallyPaidAmounts.ViewObjects
{
    public class ActuallyPaidAmountBasicDataVO
    {
        public int PaidAmountId { get; set; }

        public string RegNumber { get; set; }

        public ActuallyPaidAmountStatus Status { get; set; }

        public bool IsActivated { get; set; }

        public string IsDeletedNote { get; set; }

        public int ProgrammeId { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public int? ContractReportPaymentId { get; set; }

        public int? PaymentVersionNum { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RequestedAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpTotalAmount { get; set; }

        public DateTime? CheckedDate { get; set; }

        public ActuallyPaidAmountCreationType CreationType { get; set; }

        public byte[] Version { get; set; }
    }
}
