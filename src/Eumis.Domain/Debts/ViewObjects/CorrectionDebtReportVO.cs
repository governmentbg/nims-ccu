using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Debts.ViewObjects
{
    public class CorrectionDebtReportVO
    {
        public int CorrectionOrderNum { get; set; }

        public DateTime? CorrectionImpositionDate { get; set; }

        public string CorrectionImpositionNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FlatFinancialCorrectionLevel CorrectionLevel { get; set; }

        public decimal DebtBgAmount { get; set; }

        public decimal DebtEuAmount { get; set; }

        public decimal DebtTotalAmount { get; set; }

        public decimal ReimbursedBgAmount { get; set; }

        public decimal ReimbursedEuAmount { get; set; }

        public decimal ReimbursedTotalAmount { get; set; }

        public decimal DeductedBgAmount { get; set; }

        public decimal DeductedEuAmount { get; set; }

        public decimal DeductedTotalAmount { get; set; }

        public decimal RemainingBgAmount { get; set; }

        public decimal RemainingEuAmount { get; set; }

        public decimal RemainingTotalAmount { get; set; }
    }
}
