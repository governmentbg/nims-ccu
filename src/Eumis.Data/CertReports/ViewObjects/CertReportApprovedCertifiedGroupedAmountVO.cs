namespace Eumis.Data.CertReports.ViewObjects
{
    public class CertReportApprovedCertifiedGroupedAmountVO
    {
        public int CertReportId { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? CertifiedEuAmount { get; set; }

        public decimal? CertifiedBgAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public decimal? CertifiedTotalAmount { get; set; }
    }
}
