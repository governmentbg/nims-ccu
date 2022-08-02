using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCertAuthorityFinancialCorrectionVO
    {
        public int ContractReportCertAuthorityFinancialCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCertAuthorityFinancialCorrectionStatus Status { get; set; }

        public string Notes { get; set; }

        public DateTime CreateDate { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNum { get; set; }

        public string ProcedureName { get; set; }

        public int ReportOrderNum { get; set; }

        public decimal? CertifiedApprovedBfpTotalAmount { get; set; }

        public decimal? CertifiedApprovedSelfAmount { get; set; }

        public int AnnualAccountReportOrderNum { get; set; }
    }
}
