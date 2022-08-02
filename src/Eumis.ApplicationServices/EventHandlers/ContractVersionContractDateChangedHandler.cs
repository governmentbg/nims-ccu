using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Events;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ContractVersionContractDateChangedHandler : Eumis.Domain.Core.EventHandler<ContractVersionContractDateChangedEvent>
    {
        private IContractsRepository contractsRepository;
        private IContractVersionsRepository contractVersionsRepository;

        public ContractVersionContractDateChangedHandler(
            IContractsRepository contractsRepository,
            IContractVersionsRepository contractVersionsRepository)
        {
            this.contractsRepository = contractsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
        }

        public override void Handle(ContractVersionContractDateChangedEvent e)
        {
            var contractVersion = this.contractVersionsRepository.FindWithoutIncludes(e.ContractVersionId);

            if (contractVersion.VersionType == Domain.Contracts.ContractVersionType.NewContract)
            {
                var contract = this.contractsRepository.Find(contractVersion.ContractId);
                contract.UpdateContractDate(contractVersion.ContractDate);
            }
        }
    }
}
