using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCertifiedAmountCorrectionVO
    {
        public int ContractReportCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public string ContractRegNum { get; set; }

        public int ReportOrderNum { get; set; }

        public string RegNumber { get; set; }

        public decimal? CertifiedCorrectedApprovedBfpTotalAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedSelfAmount { get; set; }

        public string CertReportNumber { get; set; }

        public string Description { get; set; }
    }
}
