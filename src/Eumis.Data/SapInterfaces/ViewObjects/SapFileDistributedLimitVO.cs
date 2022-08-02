using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.SapInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.SapInterfaces.ViewObjects
{
    public class SapFileDistributedLimitVO
    {
        public int SapDistributedLimitId { get; set; }

        public string ContractSapNum { get; set; }

        public string ContractReportPaymentNum { get; set; }

        public DateTime? ContractReportPaymentDate { get; set; }

        public decimal PaidAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SapPaymentType? PaymentType { get; set; }

        public string Comment { get; set; }

        public bool HasWarning { get; set; }

        public List<string> Warnings { get; set; }

        public bool HasError { get; set; }

        public List<string> Errors { get; set; }

        public bool IsImported { get; set; }

        public int? ActuallyPaidAmountId { get; set; }
    }
}
