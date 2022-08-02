using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportRevalidationDO
    {
        public ContractReportRevalidationDO()
        {
        }

        public ContractReportRevalidationDO(
            ContractReportRevalidation contractReportRevalidation,
            string certCheckedByUser = null,
            ContractReportPayment contractReportPayment = null,
            ContractReportPaymentCheck contractReportPaymentCheck = null,
            string paymentCheckedByUser = null)
        {
            this.ContractReportRevalidationId = contractReportRevalidation.ContractReportRevalidationId;
            this.ProgrammeId = contractReportRevalidation.ProgrammeId;
            this.ProgrammePriorityId = contractReportRevalidation.ProgrammePriorityId;
            this.ProcedureId = contractReportRevalidation.ProcedureId;
            this.ContractId = contractReportRevalidation.ContractId;
            this.Type = contractReportRevalidation.Type;
            this.Sign = contractReportRevalidation.Sign;
            this.Date = contractReportRevalidation.Date;
            this.Description = contractReportRevalidation.Description;
            this.Reason = contractReportRevalidation.Reason;
            this.RevalidatedEuAmount = contractReportRevalidation.RevalidatedEuAmount;
            this.RevalidatedBgAmount = contractReportRevalidation.RevalidatedBgAmount;
            this.RevalidatedCrossAmount = contractReportRevalidation.RevalidatedCrossAmount;
            this.RevalidatedBfpTotalAmount = contractReportRevalidation.RevalidatedBfpTotalAmount;
            this.RevalidatedSelfAmount = contractReportRevalidation.RevalidatedSelfAmount;
            this.RevalidatedTotalAmount = contractReportRevalidation.RevalidatedTotalAmount;
            this.Version = contractReportRevalidation.Version;

            if (contractReportPaymentCheck != null)
            {
                this.ContractReportPaymentCheck = new ContractReportPaymentCheckDO(contractReportPaymentCheck, contractReportPayment, paymentCheckedByUser);
            }

            this.CertReportId = contractReportRevalidation.CertReportId;

            this.CertStatus = contractReportRevalidation.CertStatus;
            this.CertCheckedByUser = certCheckedByUser;
            this.CertCheckedDate = contractReportRevalidation.CertCheckedDate;
            this.UncertifiedRevalidatedEuAmount = contractReportRevalidation.UncertifiedRevalidatedEuAmount;
            this.UncertifiedRevalidatedBgAmount = contractReportRevalidation.UncertifiedRevalidatedBgAmount;
            this.UncertifiedRevalidatedBfpTotalAmount = contractReportRevalidation.UncertifiedRevalidatedBfpTotalAmount;
            this.UncertifiedRevalidatedCrossAmount = contractReportRevalidation.UncertifiedRevalidatedCrossAmount;
            this.UncertifiedRevalidatedSelfAmount = contractReportRevalidation.UncertifiedRevalidatedSelfAmount;
            this.UncertifiedRevalidatedTotalAmount = contractReportRevalidation.UncertifiedRevalidatedTotalAmount;

            this.CertifiedRevalidatedEuAmount = contractReportRevalidation.CertifiedRevalidatedEuAmount;
            this.CertifiedRevalidatedBgAmount = contractReportRevalidation.CertifiedRevalidatedBgAmount;
            this.CertifiedRevalidatedBfpTotalAmount = contractReportRevalidation.CertifiedRevalidatedBfpTotalAmount;
            this.CertifiedRevalidatedCrossAmount = contractReportRevalidation.CertifiedRevalidatedCrossAmount;
            this.CertifiedRevalidatedSelfAmount = contractReportRevalidation.CertifiedRevalidatedSelfAmount;
            this.CertifiedRevalidatedTotalAmount = contractReportRevalidation.CertifiedRevalidatedTotalAmount;
        }

        public int ContractReportRevalidationId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public ContractReportRevalidationType? Type { get; set; }

        public Sign? Sign { get; set; }

        public DateTime? Date { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedCrossAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedTotalAmount { get; set; }

        public byte[] Version { get; set; }

        public ContractReportPaymentCheckDO ContractReportPaymentCheck { get; set; }

        public int? CertReportId { get; set; }

        public ContractReportRevalidationCertStatus? CertStatus { get; set; }

        public string CertCheckedByUser { get; set; }

        public DateTime? CertCheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedCrossAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedTotalAmount { get; set; }

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
    }
}
