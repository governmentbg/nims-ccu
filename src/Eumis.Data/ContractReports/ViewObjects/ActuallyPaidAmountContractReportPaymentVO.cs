using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.ContractReports.ViewObjects
{
    public class ActuallyPaidAmountContractReportPaymentVO
    {
        public int ContractReportPaymentId { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportPaymentStatus StatusName { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportPaymentType? PaymentTypeName { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public decimal? RequestedAmount { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
