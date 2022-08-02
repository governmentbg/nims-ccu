using System;
using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Web.Api.Prognoses.DataObjects
{
    public class PrognosisDO
    {
        public PrognosisDO()
        {
        }

        public PrognosisDO(Prognosis prognosis)
        {
            this.PrognosisId = prognosis.PrognosisId;
            this.Level = prognosis.Level;
            this.ProgrammeId = prognosis.ProgrammeId;
            this.ProgrammePriorityId = prognosis.ProgrammePriorityId;
            this.ProcedureId = prognosis.ProcedureId;

            this.Status = prognosis.Status;
            this.Year = prognosis.Year;
            this.Month = prognosis.Month;

            this.ContractedEuAmount = prognosis.Contracted.EuAmount;
            this.ContractedBgAmount = prognosis.Contracted.BgAmount;
            this.ContractedBfpAmount = prognosis.Contracted.TotalAmount;

            this.PaymentEuAmount = prognosis.Payment.EuAmount;
            this.PaymentBgAmount = prognosis.Payment.BgAmount;
            this.PaymentBfpAmount = prognosis.Payment.TotalAmount;

            this.AdvancePaymentEuAmount = prognosis.AdvancePayment.EuAmount;
            this.AdvancePaymentBgAmount = prognosis.AdvancePayment.BgAmount;
            this.AdvancePaymentBfpAmount = prognosis.AdvancePayment.TotalAmount;

            this.AdvanceVerPaymentEuAmount = prognosis.AdvanceVerPayment.EuAmount;
            this.AdvanceVerPaymentBgAmount = prognosis.AdvanceVerPayment.BgAmount;
            this.AdvanceVerPaymentBfpAmount = prognosis.AdvanceVerPayment.TotalAmount;

            this.IntermediatePaymentEuAmount = prognosis.IntermediatePayment.EuAmount;
            this.IntermediatePaymentBgAmount = prognosis.IntermediatePayment.BgAmount;
            this.IntermediatePaymentBfpAmount = prognosis.IntermediatePayment.TotalAmount;

            this.FinalPaymentEuAmount = prognosis.FinalPayment.EuAmount;
            this.FinalPaymentBgAmount = prognosis.FinalPayment.BgAmount;
            this.FinalPaymentBfpAmount = prognosis.FinalPayment.TotalAmount;

            this.ApprovedEuAmount = prognosis.Approved.EuAmount;
            this.ApprovedBgAmount = prognosis.Approved.BgAmount;
            this.ApprovedBfpAmount = prognosis.Approved.TotalAmount;

            this.CertifiedEuAmount = prognosis.Certified.EuAmount;
            this.CertifiedBgAmount = prognosis.Certified.BgAmount;
            this.CertifiedBfpAmount = prognosis.Certified.TotalAmount;

            this.IsActivated = prognosis.IsActivated;
            this.DeleteNote = prognosis.DeleteNote;
            this.CreateDate = prognosis.CreateDate;
            this.ModifyDate = prognosis.ModifyDate;
            this.Version = prognosis.Version;
        }

        public int PrognosisId { get; set; }

        public PrognosisLevel Level { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public PrognosisStatus Status { get; set; }

        public Year Year { get; set; }

        public Month Month { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ContractedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ContractedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ContractedBfpAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaymentEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaymentBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaymentBfpAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? AdvancePaymentEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? AdvancePaymentBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? AdvancePaymentBfpAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? AdvanceVerPaymentEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? AdvanceVerPaymentBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? AdvanceVerPaymentBfpAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? IntermediatePaymentEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? IntermediatePaymentBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? IntermediatePaymentBfpAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? FinalPaymentEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? FinalPaymentBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? FinalPaymentBfpAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedBfpAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedBfpAmount { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
