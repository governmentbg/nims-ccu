using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.CertReports.ViewObjects.SummaryVOs
{
    public class CertReportAnnex4aVO
    {
        public string ContractRegNumber { get; set; }

        public int PaymentVersionNum { get; set; }

        public string ContractNumber { get; set; }

        public string ContractContractor { get; set; }

        public decimal? ContractAmountWithoutVAT { get; set; }

        public decimal? ContractVATAmountIfEligible { get; set; }

        public decimal? ContractTotalAmount { get; set; }

        public decimal? BfpTotalAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        public decimal? UnapprovedByCorrectionBfpTotalAmount { get; set; }

        public decimal? UnapprovedByCorrectionSelfAmount { get; set; }

        public decimal? UnapprovedBfpTotalAmount { get; set; }

        public decimal? UnapprovedSelfAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? RevalidatedBfpTotalAmount { get; set; }

        public decimal? RevalidatedSelfAmount { get; set; }

        public decimal? DebtReimbursedAmount { get; set; }

        public decimal? CorrectedApprovedBfpTotalAmountNoCert { get; set; }

        public decimal? CorrectedApprovedSelfAmountNoCert { get; set; }

        public decimal? CorrectedApprovedBfpTotalAmountCert { get; set; }

        public decimal? CorrectedApprovedSelfAmountCert { get; set; }

        public decimal? IncludedInCertReportBfpTotalAmount { get; set; }

        public decimal? IncludedInCertReportSelfAmount { get; set; }

        public string CertReportNumber { get; set; }
    }
}
