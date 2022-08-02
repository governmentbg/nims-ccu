using Eumis.Domain.NonAggregates;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractDataDO
    {
        public ContractDataDO()
        {
        }

        public ContractDataDO(Contract contract)
        {
            this.ContractId = contract.ContractId;
            this.Name = contract.Name;
            this.ProjectId = contract.ProjectId;
            this.ProgrammeId = contract.ProgrammeId;
            this.ProcedureId = contract.ProcedureId;
            this.AttachedContractId = contract.AttachedContractId;
            this.ContractType = contract.ContractType;
            this.ContractStatus = contract.ContractStatus;
            this.ExecutionStatus = contract.ExecutionStatus;
            this.StartDate = contract.StartDate;
            this.StartConditions = contract.StartConditions;
            this.ContractDate = contract.ContractDate;
            this.RegNumber = contract.RegNumber;
            this.CompanyName = contract.CompanyName;
            this.CompanyUin = contract.CompanyUin;
            this.CompanyUinType = contract.CompanyUinType;
            this.RegistrationType = contract.RegistrationType;
        }

        public int ContractId { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public int? AttachedContractId { get; set; }

        public ContractType ContractType { get; set; }

        public ContractRegistrationType RegistrationType { get; set; }

        public ContractStatus ContractStatus { get; set; }

        public ContractExecutionStatus? ExecutionStatus { get; set; }

        public DateTime? StartDate { get; set; }

        public string StartConditions { get; set; }

        public DateTime? ContractDate { get; set; }

        public string RegNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProgrammeCode { get; set; }

        public string AuthorityUin { get; set; }

        public UinType? AuthorityUinType { get; set; }

        public bool IsPrimaryProgramme { get; set; }

        public string ContractRegNumber { get; set; }

        public string ContractName { get; set; }

        public string ProgrammeName { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureCode { get; set; }
    }
}
