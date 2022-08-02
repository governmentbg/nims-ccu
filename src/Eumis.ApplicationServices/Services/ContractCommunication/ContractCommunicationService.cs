using System;
using System.Collections.Generic;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;

namespace Eumis.ApplicationServices.Services.ContractCommunication
{
    internal class ContractCommunicationService : IContractCommunicationService
    {
        private IContractsRepository contractsRepository;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;

        public ContractCommunicationService(
            IContractsRepository contractsRepository,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository)
        {
            this.contractsRepository = contractsRepository;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
        }

        public bool CanDelete(int communicationId)
        {
            var communication = this.contractCommunicationXmlsRepository.Find(communicationId);

            return communication.Status == ContractCommunicationStatus.Draft;
        }

        public bool CanUpdateCommunication(Guid communicationGid)
        {
            var communication = this.contractCommunicationXmlsRepository.Find(communicationGid);

            return communication.Status == ContractCommunicationStatus.Draft;
        }

        public bool CanActivateCommunication(Guid communicationGid)
        {
            var communication = this.contractCommunicationXmlsRepository.Find(communicationGid);

            return communication.Status == ContractCommunicationStatus.Draft;
        }
    }
}
