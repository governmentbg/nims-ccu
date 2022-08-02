using Eumis.Common.Json;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportFinancialCSDBudgetItemsVO
    {
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

        public YesNoNonApplicable AdvancePayment { get; set; }

        public YesNoNonApplicable CrossFinancing { get; set; }

        public string BudgetDetailName { get; set; }

        public string ContractActivityName { get; set; }

        public decimal BgAmount { get; set; }

        public decimal SelfAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public int ProgrammePriorityId { get; set; }

        public bool? CostSupportingDocumentApproved { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialCSDBudgetItemStatus Status { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialCSDBudgetItemCertStatus? CertStatus { get; set; }

        public decimal? CertifiedApprovedEuAmount { get; set; }

        public decimal? CertifiedApprovedBgAmount { get; set; }

        public decimal? CertifiedApprovedSelfAmount { get; set; }

        public decimal? CertifiedApprovedTotalAmount { get; set; }

        public byte[] Version { get; set; }

        // used for cert report snapshot
        public ContractReportFinancialCSDBudgetItemDO ContractReportFinancialCSDBudgetItem { get; set; }
    }
}
