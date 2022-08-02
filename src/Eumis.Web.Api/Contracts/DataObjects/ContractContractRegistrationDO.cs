using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Core;

namespace Eumis.Web.Api.Contracts.DataObjects
{
    public class ContractContractRegistrationDO
    {
        public ContractContractRegistrationDO()
        {
            this.ContractRegistration = new ContractRegistrationDO();
        }

        public ContractContractRegistrationDO(byte[] version)
            : this()
        {
            this.Version = version;
        }

        public ContractContractRegistrationDO(ContractRegistration contractRegistration, ContractsContractRegistration contractsContractRegistration, byte[] version)
        {
            this.ContractRegistration = new ContractRegistrationDO(contractRegistration);

            this.File = new FileDO
            {
                Key = contractsContractRegistration.File.Key,
                Name = contractsContractRegistration.File.FileName,
            };
            this.IsActive = contractsContractRegistration.IsActive;

            this.Version = version;
        }

        public ContractRegistrationDO ContractRegistration { get; set; }

        public FileDO File { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }
    }
}
