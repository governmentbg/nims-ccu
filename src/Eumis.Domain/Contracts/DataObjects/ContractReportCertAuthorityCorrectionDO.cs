using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportCertAuthorityCorrectionDO
    {
        public ContractReportCertAuthorityCorrectionDO()
        {
        }

        public ContractReportCertAuthorityCorrectionDO(
            ContractReportCertAuthorityCorrection contractReportCertAuthorityCorrection,
            ContractReportPayment contractReportPayment = null,
            ContractReportPaymentCheck contractReportPaymentCheck = null,
            string checkedByUser = null)
        {
            this.ContractReportCertAuthorityCorrectionId = contractReportCertAuthorityCorrection.ContractReportCertAuthorityCorrectionId;
            this.ProgrammeId = contractReportCertAuthorityCorrection.ProgrammeId;
            this.ProgrammePriorityId = contractReportCertAuthorityCorrection.ProgrammePriorityId;
            this.ProcedureId = contractReportCertAuthorityCorrection.ProcedureId;
            this.ContractId = contractReportCertAuthorityCorrection.ContractId;
            this.Type = contractReportCertAuthorityCorrection.Type;
            this.Sign = contractReportCertAuthorityCorrection.Sign;
            this.Date = contractReportCertAuthorityCorrection.Date;
            this.Description = contractReportCertAuthorityCorrection.Description;
            this.Reason = contractReportCertAuthorityCorrection.Reason;
            this.CertifiedEuAmount = contractReportCertAuthorityCorrection.CertifiedEuAmount;
            this.CertifiedBgAmount = contractReportCertAuthorityCorrection.CertifiedBgAmount;
            this.CertifiedCrossAmount = contractReportCertAuthorityCorrection.CertifiedCrossAmount;
            this.CertifiedBfpTotalAmount = contractReportCertAuthorityCorrection.CertifiedBfpTotalAmount;
            this.CertifiedSelfAmount = contractReportCertAuthorityCorrection.CertifiedSelfAmount;
            this.CertifiedTotalAmount = contractReportCertAuthorityCorrection.CertifiedTotalAmount;
            this.Version = contractReportCertAuthorityCorrection.Version;

            if (contractReportPaymentCheck != null)
            {
                this.ContractReportPaymentCheck = new ContractReportPaymentCheckDO(contractReportPaymentCheck, contractReportPayment, checkedByUser);
            }
        }

        public int ContractReportCertAuthorityCorrectionId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public ContractReportCertAuthorityCorrectionType? Type { get; set; }

        public Sign? Sign { get; set; }

        public DateTime? Date { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCrossAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedTotalAmount { get; set; }

        public byte[] Version { get; set; }

        public ContractReportPaymentCheckDO ContractReportPaymentCheck { get; set; }
    }
}
