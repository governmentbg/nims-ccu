using System;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractContractItemVO
    {
        public int ItemId { get; set; }

        // ContractContract
        public DateTime SignDate { get; set; }

        public string Number { get; set; }

        // ContractContractor
        public string ContractContractorCompany { get; set; }

        public string Seat { get; set; }

        // Contract
        public string ProcedureName { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNumber { get; set; }

        public string ContractCompany { get; set; }

        public string CompanyKidCode { get; set; }
    }
}
