using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.AnnualAccountReports.ViewObjects
{
    public class AnnualAccountReportFinancialCorrectionVO
    {
        public int ContractReportFinancialCorrectionId { get; set; }

        public int AnnualAccountReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialCorrectionStatus Status { get; set; }

        public string Notes { get; set; }

        public DateTime CreateDate { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNum { get; set; }

        public string ProcedureName { get; set; }

        public int ReportOrderNum { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public IList<ContractReportFinancialCorrectionCSDsVO> ContractReportFinancialCorrectionCSDs { get; set; }
    }
}
