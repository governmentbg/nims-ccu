using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.CertReports.ViewObjects.SummaryVOs
{
    public class CertReportAnnex4aResultVO
    {
        public ICollection<CertReportAnnex4aVO> Items { get; set; }

        public decimal? TotalContractAmountWithoutVAT { get; set; }

        public decimal? TotalContractVATAmountIfEligible { get; set; }

        public decimal? TotalContractTotalAmount { get; set; }

        public decimal? TotalBfpTotalAmount { get; set; }

        public decimal? TotalSelfAmount { get; set; }

        public decimal? TotalUnapprovedByCorrectionBfpTotalAmount { get; set; }

        public decimal? TotalUnapprovedByCorrectionSelfAmount { get; set; }

        public decimal? TotalUnapprovedBfpTotalAmount { get; set; }

        public decimal? TotalUnapprovedSelfAmount { get; set; }

        public decimal? TotalApprovedBfpTotalAmount { get; set; }

        public decimal? TotalApprovedSelfAmount { get; set; }

        public decimal? TotalRevalidatedBfpTotalAmount { get; set; }

        public decimal? TotalRevalidatedSelfAmount { get; set; }

        public decimal? TotalDebtReimbursedAmount { get; set; }

        public decimal? TotalCorrectedApprovedBfpTotalAmountNoCert { get; set; }

        public decimal? TotalCorrectedApprovedSelfAmountNoCert { get; set; }

        public decimal? TotalCorrectedApprovedBfpTotalAmountCert { get; set; }

        public decimal? TotalCorrectedApprovedSelfAmountCert { get; set; }

        public decimal? TotalIncludedInCertReportBfpTotalAmount { get; set; }

        public decimal? TotalIncludedInCertReportSelfAmount { get; set; }
    }
}
