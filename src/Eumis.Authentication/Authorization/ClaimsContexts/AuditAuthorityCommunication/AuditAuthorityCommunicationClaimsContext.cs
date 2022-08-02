using Autofac.Features.AttributeFilters;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.AuditAuthorityCommunication
{
    internal class AuditAuthorityCommunicationClaimsContext : ClaimsContext, IAuditAuthorityCommunicationClaimsContext
    {
        private int communicationId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;

        public AuditAuthorityCommunicationClaimsContext(
            int communicationId,
            [KeyFilter(ClaimsCaches.AuditAuthorityCommunication)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository)
            : base(claimsCache)
        {
            this.communicationId = communicationId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
        }

        public int ContractCommunicationId
        {
            get
            {
                return this.communicationId;
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.communicationId,
                    new ClaimKey("ContractId"),
                    () => this.contractCommunicationXmlsRepository.GetContractId(this.communicationId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.communicationId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
