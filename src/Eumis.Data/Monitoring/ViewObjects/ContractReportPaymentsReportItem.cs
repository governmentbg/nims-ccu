using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class ContractReportPaymentsReportItem
    {
        public string ContractRegNum { get; set; }

        public int? ReportNum { get; set; }

        public int? PaymentNum { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyName { get; set; }

        public DateTime? RegDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportPaymentType? PaymentType { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportStatus? PaymentStatus { get; set; }

        public decimal? PaymentTotalAmount { get; set; }

        public decimal? PaymentApprovedAmount { get; set; }

        public decimal? PaymentPaidAmount { get; set; }

        public DateTime? PaymentCheckDate { get; set; }

        public decimal? PaymentCertifiedAmount { get; set; }

        public decimal? PaymentActuallyPaidAmount { get; set; }

        public DateTime? PaymentActuallyPaidDate { get; set; }

        public decimal? PaymentReimbursedAmount { get; set; }

        public DateTime? PaymentReimbursementDate { get; set; }
    }
}
