using Eumis.Common.Json;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportFinancialRevalidationCSDsVO
    {
        public int ContractReportFinancialRevalidationCSDId { get; set; }

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

        public bool? CostSupportingDocumentApproved { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? RevalidatedEuAmount { get; set; }

        public decimal? RevalidatedBgAmount { get; set; }

        public decimal? RevalidatedSelfAmount { get; set; }

        public decimal? RevalidatedTotalAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Sign? Sign { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialRevalidationCSDStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialRevalidationCSDCertStatus? CertStatus { get; set; }

        public decimal? CertifiedRevalidatedEuAmount { get; set; }

        public decimal? CertifiedRevalidatedBgAmount { get; set; }

        public decimal? CertifiedRevalidatedSelfAmount { get; set; }

        public decimal? CertifiedRevalidatedTotalAmount { get; set; }

        public byte[] Version { get; set; }

        // used for cert report snapshot
        public ContractReportFinancialRevalidationCSDDO ContractReportFinancialRevalidationCSD { get; set; }
    }
}
