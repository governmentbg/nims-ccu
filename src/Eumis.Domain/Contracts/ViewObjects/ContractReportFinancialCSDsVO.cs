using Eumis.Common.Json;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportFinancialCSDsVO
    {
        public int ContractReportFinancialCorrectionId { get; set; }

        public int ContractReportFinancialCSDBudgetItemId { get; set; }

        public int ContractReportFinancialCSDId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public string ContractRegNum { get; set; }

        public int ReportNum { get; set; }

        public DateTime? ReportSubmitDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CostSupportingDocumentType Type { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int PaymentVersionNum { get; set; }

        public int PaymentVersionSubNum { get; set; }

        public DateTime? PaymentSubmitDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CostSupportingDocumentCompanyType CompanyType { get; set; }

        public string CompanyUin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType CompanyUinType { get; set; }

        public string CompanyName { get; set; }

        public decimal BfpTotalAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? CorrectedApprovedBfpTotalAmount { get; set; }

        public decimal? CorrectedApprovedTotalAmount { get; set; }
    }
}
