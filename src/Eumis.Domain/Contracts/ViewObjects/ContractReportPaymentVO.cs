using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportPaymentVO
    {
        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportPaymentStatus Status { get; set; }

        public ContractReportPaymentStatus StatusName { get; set; }

        public string StatusNote { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportPaymentType? PaymentType { get; set; }

        public DateTime? RegDate { get; set; }

        public string OtherRegistration { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? SubmitDeadline { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime? ReturnDate { get; set; }

        public decimal? RequestedAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public string ContractRegistrationEmail { get; set; }
    }
}
