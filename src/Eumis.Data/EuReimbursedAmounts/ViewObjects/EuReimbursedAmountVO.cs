using System;
using Eumis.Common.Json;
using Eumis.Domain.EuReimbursedAmounts;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.EuReimbursedAmounts.ViewObjects
{
    public class EuReimbursedAmountVO
    {
        public int EuReimbursedAmountId { get; set; }

        public string ProgrammeName { get; set; }

        public EuReimbursedAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EuReimbursedAmountStatus StatusDescr { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EuReimbursedAmountPaymentType? PaymentType { get; set; }

        public DateTime? Date { get; set; }

        public decimal? EuTranche { get; set; }
    }
}
