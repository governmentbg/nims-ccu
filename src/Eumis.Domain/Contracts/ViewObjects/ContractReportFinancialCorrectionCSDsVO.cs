using Eumis.Common.Json;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportFinancialCorrectionCSDsVO
    {
        public int ContractReportFinancialCorrectionCSDId { get; set; }

        public int ContractReportFinancialCSDBudgetItemId { get; set; }

        public int ContractReportFinancialCSDId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CostSupportingDocumentType Type { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public DateTime PaymentDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CostSupportingDocumentCompanyType CompanyType { get; set; }

        public string CompanyUin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType CompanyUinType { get; set; }

        public string CompanyName { get; set; }

        public string ContractContractorName { get; set; }

        public string BudgetDetailName { get; set; }

        public string ContractActivityName { get; set; }

        public decimal EuAmount { get; set; }

        public decimal BgAmount { get; set; }

        public decimal SelfAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public YesNoNonApplicable AdvancePayment { get; set; }

        public YesNoNonApplicable CrossFinancing { get; set; }

        public int ProgrammePriorityId { get; set; }

        public bool? CostSupportingDocumentApproved { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Sign? Sign { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialCorrectionCSDStatus Status { get; set; }

        public decimal? CorrectedApprovedEuAmount { get; set; }

        public decimal? CorrectedApprovedBgAmount { get; set; }

        public decimal? CorrectedApprovedBfpTotalAmount { get; set; }

        public decimal? CorrectedApprovedSelfAmount { get; set; }

        public decimal? CorrectedApprovedTotalAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialCorrectionCSDCertStatus? CertStatus { get; set; }

        public decimal? CertifiedCorrectedApprovedEuAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedBgAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedBfpTotalAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedSelfAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedTotalAmount { get; set; }

        public byte[] Version { get; set; }

        // used for cert report snapshot
        public ContractReportFinancialCorrectionCSDDO ContractReportFinancialCorrectionCSD { get; set; }
    }
}
