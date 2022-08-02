using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportFinancialCertCorrectionVO
    {
        public int ContractReportFinancialCertCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialCertCorrectionStatus Status { get; set; }

        public string Notes { get; set; }

        public DateTime CreateDate { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNum { get; set; }

        public string ProcedureName { get; set; }

        public int ReportOrderNum { get; set; }

        public int? PaymentVersionNum { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public ContractReportFinancialCertCorrectionDO ContractReportFinancialCertCorrection { get; set; }

        public IList<ContractReportFinancialCertCorrectionCSDsVO> ContractReportFinancialCertCorrectionCSDs { get; set; }
    }
}
