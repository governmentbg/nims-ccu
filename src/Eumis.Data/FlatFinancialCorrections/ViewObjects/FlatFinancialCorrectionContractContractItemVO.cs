using System;
using Eumis.Domain.Contracts;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.FlatFinancialCorrections.ViewObjects
{
    public class FlatFinancialCorrectionContractContractItemVO : FlatFinancialCorrectionItemVO
    {
        // ContractContract
        public DateTime SignDate { get; set; }

        public string Number { get; set; }

        public decimal TotalAmountExcludingVAT { get; set; }

        public decimal VATAmountIfEligible { get; set; }

        public decimal TotalFundedValue { get; set; }

        public int NumberAnnexes { get; set; }

        public decimal CurrentAnnexTotalAmount { get; set; }

        // ContractContractor
        public string Uin { get; set; }

        public UinType UinType { get; set; }

        public string Name { get; set; }

        public string ContractContractorCompany { get; set; }

        public string Seat { get; set; }

        // Contract
        public ContractStatus ContractStatus { get; set; }

        public string ProcedureName { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNumber { get; set; }

        public string ContractCompany { get; set; }

        public string CompanyKidCode { get; set; }
    }
}
