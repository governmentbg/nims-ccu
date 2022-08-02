using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportCertCorrectionDO
    {
        public ContractReportCertCorrectionDO()
        {
        }

        public ContractReportCertCorrectionDO(
            ContractReportCertCorrection contractReportCertCorrection,
            ContractReportPayment contractReportPayment = null,
            ContractReportPaymentCheck contractReportPaymentCheck = null,
            string checkedByUser = null)
        {
            this.ContractReportCertCorrectionId = contractReportCertCorrection.ContractReportCertCorrectionId;
            this.ProgrammeId = contractReportCertCorrection.ProgrammeId;
            this.ProgrammePriorityId = contractReportCertCorrection.ProgrammePriorityId;
            this.ProcedureId = contractReportCertCorrection.ProcedureId;
            this.ContractId = contractReportCertCorrection.ContractId;
            this.Type = contractReportCertCorrection.Type;
            this.Sign = contractReportCertCorrection.Sign;
            this.Date = contractReportCertCorrection.Date;
            this.Description = contractReportCertCorrection.Description;
            this.Reason = contractReportCertCorrection.Reason;
            this.CertifiedEuAmount = contractReportCertCorrection.CertifiedEuAmount;
            this.CertifiedBgAmount = contractReportCertCorrection.CertifiedBgAmount;
            this.CertifiedCrossAmount = contractReportCertCorrection.CertifiedCrossAmount;
            this.CertifiedBfpTotalAmount = contractReportCertCorrection.CertifiedBfpTotalAmount;
            this.CertifiedSelfAmount = contractReportCertCorrection.CertifiedSelfAmount;
            this.CertifiedTotalAmount = contractReportCertCorrection.CertifiedTotalAmount;
            this.Version = contractReportCertCorrection.Version;

            if (contractReportPaymentCheck != null)
            {
                this.ContractReportPaymentCheck = new ContractReportPaymentCheckDO(contractReportPaymentCheck, contractReportPayment, checkedByUser);
            }
        }

        public int ContractReportCertCorrectionId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public ContractReportCertCorrectionType? Type { get; set; }

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
