using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO
    {
        public int ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId { get; set; }

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

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Sign? Sign { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus Status { get; set; }

        public decimal? CertifiedEuAmount { get; set; }

        public decimal? CertifiedBgAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public decimal? CertifiedTotalAmount { get; set; }

        public decimal? CorrectedRevalidatedEuAmount { get; set; }

        public decimal? CorrectedRevalidatedBgAmount { get; set; }

        public decimal? CorrectedRevalidatedSelfAmount { get; set; }

        public decimal? CorrectedRevalidatedTotalAmount { get; set; }

        public byte[] Version { get; set; }
    }
}
