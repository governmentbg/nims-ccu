using System;
using Eumis.Common.Json;
using Eumis.Domain.EuReimbursedAmounts;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Web.Api.EuReimbursedAmounts.DataObjects
{
    public class EuReimbursedAmountDO
    {
        public EuReimbursedAmountDO()
        {
        }

        public EuReimbursedAmountDO(EuReimbursedAmount euReimbursedAmount)
        {
            this.EuReimbursedAmountId = euReimbursedAmount.EuReimbursedAmountId;
            this.ProgrammeId = euReimbursedAmount.ProgrammeId;
            this.Status = euReimbursedAmount.Status;
            this.IsActivated = euReimbursedAmount.IsActivated;
            this.DeleteNote = euReimbursedAmount.DeleteNote;
            this.PaymentType = euReimbursedAmount.PaymentType;
            this.Date = euReimbursedAmount.Date;

            this.PaymentAppNum = euReimbursedAmount.PaymentAppNum;
            this.PaymentAppSentDate = euReimbursedAmount.PaymentAppSentDate;
            this.PaymentAppDateFrom = euReimbursedAmount.PaymentAppDateFrom;
            this.PaymentAppDateTo = euReimbursedAmount.PaymentAppDateTo;

            this.CertExpensesBfpEuAmountLv = euReimbursedAmount.CertExpensesLv.BfpEuAmount;
            this.CertExpensesBfpBgAmountLv = euReimbursedAmount.CertExpensesLv.BfpBgAmount;
            this.CertExpensesBfpTotalAmountLv = euReimbursedAmount.CertExpensesLv.BfpTotalAmount;
            this.CertExpensesSelfAmountLv = euReimbursedAmount.CertExpensesLv.SelfAmount;
            this.CertExpensesTotalAmountLv = euReimbursedAmount.CertExpensesLv.TotalAmount;

            this.CertExpensesBfpEuAmountEuro = euReimbursedAmount.CertExpensesEuro.BfpEuAmount;
            this.CertExpensesBfpBgAmountEuro = euReimbursedAmount.CertExpensesEuro.BfpBgAmount;
            this.CertExpensesBfpTotalAmountEuro = euReimbursedAmount.CertExpensesEuro.BfpTotalAmount;
            this.CertExpensesSelfAmountEuro = euReimbursedAmount.CertExpensesEuro.SelfAmount;
            this.CertExpensesTotalAmountEuro = euReimbursedAmount.CertExpensesEuro.TotalAmount;

            this.EuTranche = euReimbursedAmount.EuTranche;
            this.Note = euReimbursedAmount.Note;

            this.Version = euReimbursedAmount.Version;
        }

        public int EuReimbursedAmountId { get; set; }

        public int ProgrammeId { get; set; }

        public EuReimbursedAmountStatus Status { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public EuReimbursedAmountPaymentType? PaymentType { get; set; }

        public DateTime? Date { get; set; }

        public string PaymentAppNum { get; set; }

        public DateTime? PaymentAppSentDate { get; set; }

        public DateTime? PaymentAppDateFrom { get; set; }

        public DateTime? PaymentAppDateTo { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertExpensesBfpEuAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertExpensesBfpBgAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertExpensesBfpTotalAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertExpensesSelfAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertExpensesTotalAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertExpensesBfpEuAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertExpensesBfpBgAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertExpensesBfpTotalAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertExpensesSelfAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertExpensesTotalAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? EuTranche { get; set; }

        public string Note { get; set; }

        public byte[] Version { get; set; }
    }
}
