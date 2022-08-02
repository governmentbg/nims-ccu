using Eumis.Common;
using Eumis.Common.Json;
using Eumis.Data.ActuallyPaidAmounts.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Api.ActuallyPaidAmounts.DataObjects
{
    public class ActuallyPaidAmountDO
    {
        public ActuallyPaidAmountDO()
        {
        }

        public ActuallyPaidAmountDO(ActuallyPaidAmount paidAmount, ActuallyPaidAmountBasicDataVO basicData)
        {
            this.PaidAmountId = paidAmount.ActuallyPaidAmountId;
            this.ProgrammePriorityId = paidAmount.ProgrammePriorityId;
            this.ContractId = paidAmount.ContractId;
            this.PaymentReason = paidAmount.PaymentReason;
            this.PaymentDate = paidAmount.PaymentDate;
            this.Comment = paidAmount.Comment;

            this.PaidBfpEuAmount = paidAmount.PaidBfpEuAmount;
            this.PaidBfpBgAmount = paidAmount.PaidBfpBgAmount;
            this.PaidBfpTotalAmount = paidAmount.PaidBfpTotalAmount;
            this.PaidSelfAmount = paidAmount.PaidSelfAmount;
            this.PaidTotalAmount = paidAmount.PaidTotalAmount;
            this.PaidBfpCrossAmount = paidAmount.PaidBfpCrossAmount;

            this.ContractReportPaymentId = basicData.ContractReportPaymentId;
            this.PaymentVersionNum = basicData.PaymentVersionNum;
            this.PaymentRequestedAmount = basicData.RequestedAmount;
            this.PaymentPaidBfpTotalAmount = basicData.PaidBfpTotalAmount;
            this.PaymentCheckedDate = basicData.CheckedDate;

            this.Version = paidAmount.Version;

            bool manuallyCreated = paidAmount.SapFileId == null;
            this.EditableFields = new Dictionary<string, bool>
            {
                { nameof(this.ProgrammePriorityId), true },
                { nameof(this.PaymentReason),       true },
                { nameof(this.PaymentDate),         manuallyCreated },
                { nameof(this.Comment),             true },
                { nameof(this.PaidBfpEuAmount),     manuallyCreated },
                { nameof(this.PaidBfpBgAmount),     manuallyCreated },
                { nameof(this.PaidSelfAmount),      true },
                { nameof(this.PaidBfpCrossAmount),  true },
            };
            this.EditableFields = this.EditableFields.ToDictionary(kvp => kvp.Key.ToCamelCase(), kvp => kvp.Value);
        }

        public int PaidAmountId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public int ContractId { get; set; }

        public PaymentReason? PaymentReason { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string Comment { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpCrossAmount { get; set; }

        public int? ContractReportPaymentId { get; set; }

        public int? PaymentVersionNum { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaymentRequestedAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaymentPaidBfpTotalAmount { get; set; }

        public DateTime? PaymentCheckedDate { get; set; }

        public IDictionary<string, bool> EditableFields { get; set; }

        public byte[] Version { get; set; }
    }
}
