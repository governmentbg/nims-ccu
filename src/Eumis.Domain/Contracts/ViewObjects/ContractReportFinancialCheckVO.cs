using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportFinancialCheckVO
    {
        public int ContractReportFinancialCheckId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialCheckStatus Status { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime CreateDate { get; set; }

        public int FinancialVersionNum { get; set; }

        public int FinancialVersionSubNum { get; set; }
    }
}
