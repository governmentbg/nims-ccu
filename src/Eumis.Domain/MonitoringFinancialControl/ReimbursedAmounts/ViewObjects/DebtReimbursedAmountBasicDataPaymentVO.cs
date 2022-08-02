using System;
using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects
{
    public class DebtReimbursedAmountBasicDataPaymentVO
    {
        public int? PaymentVersionNum { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RequestedAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedTotalAmount { get; set; }

        public DateTime? CheckedDate { get; set; }
    }
}
