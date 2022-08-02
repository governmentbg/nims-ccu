using System;
using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Debts.ViewObjects
{
    public class ContractDebtVersionVO
    {
        public int ContractDebtVersionId { get; set; }

        public int ContractDebtId { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractDebtVersionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractDebtExecutionStatus? ExecutionStatus { get; set; }

        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}