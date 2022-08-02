using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportPaymentCheckVO
    {
        public int ContractReportPaymentCheckId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportPaymentCheckStatus Status { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime CreateDate { get; set; }

        public int PaymentVersionNum { get; set; }

        public int PaymentVersionSubNum { get; set; }
    }
}
