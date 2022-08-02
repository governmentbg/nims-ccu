using Eumis.Domain.Contracts;
using Eumis.Domain.NonAggregates;

namespace Eumis.Web.Api.Contracts.DataObjects
{
    public class ContractDO
    {
        public ContractDO(Contract contract, ContractVersionXml version)
        {
            this.ContractId = contract.ContractId;
            this.Name = contract.Name;
            this.ProgrammeId = contract.ProgrammeId;
            this.ProcedureId = contract.ProcedureId;
            this.AttachedContractId = contract.AttachedContractId;
            this.ContractType = contract.ContractType;
            this.RegNumber = contract.RegNumber;
            this.ContractStatus = contract.ContractStatus;
            this.CompanyName = contract.CompanyName;
            this.CompanyUin = contract.CompanyUin;
            this.CompanyUinType = contract.CompanyUinType;
            this.RegistrationType = contract.RegistrationType;

            this.Version = new FirstVersionDO(version);

            this.Vers = contract.Version;
        }

        public int ContractId { get; set; }

        public string Name { get; set; }

        public int ProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public int? AttachedContractId { get; set; }

        public ContractType ContractType { get; set; }

        public ContractRegistrationType RegistrationType { get; set; }

        public string RegNumber { get; set; }

        public ContractStatus ContractStatus { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public FirstVersionDO Version { get; set; }

        public byte[] Vers { get; set; }
    }
}
