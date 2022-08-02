using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.AnnualAccountReports.ViewObjects
{
    public class AnnualAccountReportCertRevalidationFinancialCorrectionVO
    {
        public int ContractReportRevalidationCertAuthorityFinancialCorrectionId { get; set; }

        public int AnnualAccountReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationCertAuthorityFinancialCorrectionStatus Status { get; set; }

        public string Notes { get; set; }

        public DateTime CreateDate { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNum { get; set; }

        public string ProcedureName { get; set; }

        public int ReportOrderNum { get; set; }

        public decimal? CorrectedBfpTotalAmount { get; set; }

        public decimal? CorrectedSelfAmount { get; set; }
    }
}
