using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportRevalidationCertAuthorityCorrectionBasicDataVO
    {
        public int ContractReportRevalidationCertAuthorityCorrectionId { get; set; }

        public string RegNumber { get; set; }

        public ContractReportRevalidationCertAuthorityCorrectionStatus Status { get; set; }

        public ContractReportRevalidationCertAuthorityCorrectionType Type { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public UinType? CompanyUinType { get; set; }

        public int? ContractReportId { get; set; }

        public int? ContractReportPaymentId { get; set; }

        public int? ReportNum { get; set; }

        public int? PaymentVersionNum { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RequestedAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpTotalAmount { get; set; }

        public DateTime? PaymentCheckedDate { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public byte[] Version { get; set; }
    }
}
