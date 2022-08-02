using Eumis.Common.Json;
using Eumis.Domain.Core;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class ConcludedContractsReportItem
    {
        public string ContractContractRegNum { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public DateTime? ContractDate { get; set; }

        public string ContractRegNum { get; set; }

        public string ContractCompanyName { get; set; }

        public string ContractCompanyUin { get; set; }

        public string ContractCompanyType { get; set; }

        public string ContractCompanyLegalType { get; set; }

        public string ContractContractName { get; set; }

        public string ErrandArea { get; set; }

        public string ErrandLegalAct { get; set; }

        public string ErrandType { get; set; }

        public decimal TotalFundedValue { get; set; }

        public decimal VATAmountIfEligible { get; set; }

        public string Subcontractors { get; set; }

        public string UnionMembers { get; set; }

        public decimal? ReportedTotalAmount { get; set; }

        public decimal? ReportedBfpAmount { get; set; }

        public decimal? ReportedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? ApprovedBfpAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? CertifiedTotalAmount { get; set; }

        public decimal? CertifiedBfpAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public decimal? FinancialCorrectionTotalAmount { get; set; }

        public decimal? FinancialCorrectionBfpAmount { get; set; }

        public decimal? FinancialCorrectionEuAmount { get; set; }

        public decimal? FinancialCorrectionBgAmount { get; set; }

        public decimal? FinancialCorrectionSelfAmount { get; set; }
    }
}
