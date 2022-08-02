using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportRevalidationCertAuthorityCorrectionDO
    {
        public ContractReportRevalidationCertAuthorityCorrectionDO()
        {
        }

        public ContractReportRevalidationCertAuthorityCorrectionDO(
            ContractReportRevalidationCertAuthorityCorrection contractReportRevalidationCertAuthorityCorrection,
            ContractReportPayment contractReportPayment = null,
            ContractReportPaymentCheck contractReportPaymentCheck = null,
            string checkedByUser = null)
        {
            this.ContractReportRevalidationCertAuthorityCorrectionId = contractReportRevalidationCertAuthorityCorrection.ContractReportRevalidationCertAuthorityCorrectionId;
            this.ProgrammeId = contractReportRevalidationCertAuthorityCorrection.ProgrammeId;
            this.ProgrammePriorityId = contractReportRevalidationCertAuthorityCorrection.ProgrammePriorityId;
            this.ProcedureId = contractReportRevalidationCertAuthorityCorrection.ProcedureId;
            this.ContractId = contractReportRevalidationCertAuthorityCorrection.ContractId;
            this.Type = contractReportRevalidationCertAuthorityCorrection.Type;
            this.Sign = contractReportRevalidationCertAuthorityCorrection.Sign;
            this.Date = contractReportRevalidationCertAuthorityCorrection.Date;
            this.Description = contractReportRevalidationCertAuthorityCorrection.Description;
            this.Reason = contractReportRevalidationCertAuthorityCorrection.Reason;
            this.CertifiedRevalidatedEuAmount = contractReportRevalidationCertAuthorityCorrection.CertifiedRevalidatedEuAmount;
            this.CertifiedRevalidatedBgAmount = contractReportRevalidationCertAuthorityCorrection.CertifiedRevalidatedBgAmount;
            this.CertifiedRevalidatedCrossAmount = contractReportRevalidationCertAuthorityCorrection.CertifiedRevalidatedCrossAmount;
            this.CertifiedRevalidatedBfpTotalAmount = contractReportRevalidationCertAuthorityCorrection.CertifiedRevalidatedBfpTotalAmount;
            this.CertifiedRevalidatedSelfAmount = contractReportRevalidationCertAuthorityCorrection.CertifiedRevalidatedSelfAmount;
            this.CertifiedRevalidatedTotalAmount = contractReportRevalidationCertAuthorityCorrection.CertifiedRevalidatedTotalAmount;
            this.Version = contractReportRevalidationCertAuthorityCorrection.Version;

            if (contractReportPaymentCheck != null)
            {
                this.ContractReportPaymentCheck = new ContractReportPaymentCheckDO(contractReportPaymentCheck, contractReportPayment, checkedByUser);
            }
        }

        public int ContractReportRevalidationCertAuthorityCorrectionId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public ContractReportRevalidationCertAuthorityCorrectionType? Type { get; set; }

        public Sign? Sign { get; set; }

        public DateTime? Date { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedCrossAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedTotalAmount { get; set; }

        public byte[] Version { get; set; }

        public ContractReportPaymentCheckDO ContractReportPaymentCheck { get; set; }
    }
}
