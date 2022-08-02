using Eumis.Common.Json;
using Eumis.Domain.CertReports.DataObjects;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects
{
    public class DebtReimbursedAmountVO
    {
        public int AmountId { get; set; }

        public string ProgrammeName { get; set; }

        public string DebtRegNumber { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ReimbursedAmountStatus StatusDescr { get; set; }

        public ReimbursedAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ReimbursementType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Reimbursement Reimbursement { get; set; }

        public DateTime ReimbursementDate { get; set; }

        public decimal? PrincipalEuAmount { get; set; }

        public decimal? PrincipalBgAmount { get; set; }

        public decimal? PrincipalTotalAmount { get; set; }

        public decimal? InterestEuAmount { get; set; }

        public decimal? InterestBgAmount { get; set; }

        public decimal? InterestTotalAmount { get; set; }

        // used for cert report snapshot
        public CertReportDebtReimbursedAmountDO CertReportDebtReimbursedAmount { get; set; }
    }
}
