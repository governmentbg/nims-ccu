using Eumis.Common.Json;
using Eumis.Domain.CertReports.DataObjects;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Debts.ViewObjects
{
    public class ContractDebtVO
    {
        public int ContractDebtId { get; set; }

        public int OrderNum { get; set; }

        public string ContractRegNumber { get; set; }

        public string CompanyName { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractDebtExecutionStatus? ExecutionStatus { get; set; }

        public DateTime? ModifyDate { get; set; }

        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? TotalInterestAmount { get; set; }

        public string CertReportNumber { get; set; }

        public bool IsRemoved { get; set; }

        // used for cert report snapshot
        public CertReportContractDebtDO CertReportContractDebt { get; set; }
    }
}
