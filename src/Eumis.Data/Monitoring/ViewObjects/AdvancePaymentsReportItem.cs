using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class AdvancePaymentsReportItem
    {
        public string ContractRegNum { get; set; }

        public string BeneficiaryName { get; set; }

        public decimal? VerifiedValue { get; set; }

        public decimal? VerifiedCosts { get; set; }

        public decimal? VerifiedNonCoveredValue { get; set; }

        public decimal? CertifiedPayment { get; set; }

        public decimal? CertAdvancePaymentExpenses { get; set; }

        public decimal? CertifiedNonCoveredValue { get; set; }

        public string CertReportsWithAdvencePayments { get; set; }

        public string CertReportsWithAdvancePaymentExpenses { get; set; }
    }
}
