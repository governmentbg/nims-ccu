using Eumis.Common.Json;
using Eumis.Domain.FIReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.FIReimbursedAmounts.ViewObjects
{
    public class FIReimbursedAmountVO
    {
        public int AmountId { get; set; }

        public string ProgrammeName { get; set; }

        public string ContractRegNumber { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FIReimbursedAmountStatus StatusDescr { get; set; }

        public FIReimbursedAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FIReimbursementType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Reimbursement Reimbursement { get; set; }

        public DateTime ReimbursementDate { get; set; }
    }
}
