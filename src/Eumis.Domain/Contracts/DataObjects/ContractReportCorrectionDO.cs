using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportCorrectionDO
    {
        public ContractReportCorrectionDO()
        {
        }

        public ContractReportCorrectionDO(
            ContractReportCorrection contractReportCorrection,
            string certCheckedByUser = null,
            ContractReportPayment contractReportPayment = null,
            ContractReportPaymentCheck contractReportPaymentCheck = null,
            string paymentCheckedByUser = null)
        {
            this.ContractReportCorrectionId = contractReportCorrection.ContractReportCorrectionId;
            this.ProgrammeId = contractReportCorrection.ProgrammeId;
            this.ProgrammePriorityId = contractReportCorrection.ProgrammePriorityId;
            this.ProcedureId = contractReportCorrection.ProcedureId;
            this.ContractId = contractReportCorrection.ContractId;
            this.Type = contractReportCorrection.Type;
            this.Sign = contractReportCorrection.Sign;
            this.Date = contractReportCorrection.Date;
            this.Description = contractReportCorrection.Description;
            this.Reason = contractReportCorrection.Reason;
            this.CorrectionType = contractReportCorrection.CorrectionType;
            this.FinancialCorrectionId = contractReportCorrection.FinancialCorrectionId;
            this.IrregularityId = contractReportCorrection.IrregularityId;
            this.FlatFinancialCorrectionId = contractReportCorrection.FlatFinancialCorrectionId;
            this.CorrectedApprovedEuAmount = contractReportCorrection.CorrectedApprovedEuAmount;
            this.CorrectedApprovedBgAmount = contractReportCorrection.CorrectedApprovedBgAmount;
            this.CorrectedApprovedCrossAmount = contractReportCorrection.CorrectedApprovedCrossAmount;
            this.CorrectedApprovedBfpTotalAmount = contractReportCorrection.CorrectedApprovedBfpTotalAmount;
            this.CorrectedApprovedSelfAmount = contractReportCorrection.CorrectedApprovedSelfAmount;
            this.CorrectedApprovedTotalAmount = contractReportCorrection.CorrectedApprovedTotalAmount;
            this.Version = contractReportCorrection.Version;

            if (contractReportPaymentCheck != null)
            {
                this.ContractReportPaymentCheck = new ContractReportPaymentCheckDO(contractReportPaymentCheck, contractReportPayment, paymentCheckedByUser);
            }

            this.CertReportId = contractReportCorrection.CertReportId;

            this.CertStatus = contractReportCorrection.CertStatus;
            this.CertCheckedByUser = certCheckedByUser;
            this.CertCheckedDate = contractReportCorrection.CertCheckedDate;
            this.UncertifiedCorrectedApprovedEuAmount = contractReportCorrection.UncertifiedCorrectedApprovedEuAmount;
            this.UncertifiedCorrectedApprovedBgAmount = contractReportCorrection.UncertifiedCorrectedApprovedBgAmount;
            this.UncertifiedCorrectedApprovedBfpTotalAmount = contractReportCorrection.UncertifiedCorrectedApprovedBfpTotalAmount;
            this.UncertifiedCorrectedApprovedCrossAmount = contractReportCorrection.UncertifiedCorrectedApprovedCrossAmount;
            this.UncertifiedCorrectedApprovedSelfAmount = contractReportCorrection.UncertifiedCorrectedApprovedSelfAmount;
            this.UncertifiedCorrectedApprovedTotalAmount = contractReportCorrection.UncertifiedCorrectedApprovedTotalAmount;

            this.CertifiedCorrectedApprovedEuAmount = contractReportCorrection.CertifiedCorrectedApprovedEuAmount;
            this.CertifiedCorrectedApprovedBgAmount = contractReportCorrection.CertifiedCorrectedApprovedBgAmount;
            this.CertifiedCorrectedApprovedBfpTotalAmount = contractReportCorrection.CertifiedCorrectedApprovedBfpTotalAmount;
            this.CertifiedCorrectedApprovedCrossAmount = contractReportCorrection.CertifiedCorrectedApprovedCrossAmount;
            this.CertifiedCorrectedApprovedSelfAmount = contractReportCorrection.CertifiedCorrectedApprovedSelfAmount;
            this.CertifiedCorrectedApprovedTotalAmount = contractReportCorrection.CertifiedCorrectedApprovedTotalAmount;
        }

        public ContractReportCorrectionDO(
            string contractReportCorrectionElementNumber,
            ContractReportCorrection contractReportCorrection,
            string certCheckedByUser = null,
            ContractReportPayment contractReportPayment = null,
            ContractReportPaymentCheck contractReportPaymentCheck = null,
            string paymentCheckedByUser = null)
            : this(
                 contractReportCorrection,
                 certCheckedByUser,
                 contractReportPayment,
                 contractReportPaymentCheck,
                 paymentCheckedByUser)
        {
            this.ElementNumber = contractReportCorrectionElementNumber;
        }

        public int ContractReportCorrectionId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public ContractReportCorrectionType? Type { get; set; }

        public Sign? Sign { get; set; }

        public DateTime? Date { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        public string ElementNumber { get; set; }

        public CorrectionTypeExtended? CorrectionType { get; set; }

        public int? FinancialCorrectionId { get; set; }

        public int? IrregularityId { get; set; }

        public int? FlatFinancialCorrectionId { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedCrossAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedTotalAmount { get; set; }

        public byte[] Version { get; set; }

        public ContractReportPaymentCheckDO ContractReportPaymentCheck { get; set; }

        public int? CertReportId { get; set; }

        public ContractReportCorrectionCertStatus? CertStatus { get; set; }

        public string CertCheckedByUser { get; set; }

        public DateTime? CertCheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedCrossAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedCrossAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedTotalAmount { get; set; }
    }
}
