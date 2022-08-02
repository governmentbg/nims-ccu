using Autofac.Features.AttributeFilters;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractVersion
{
    internal class ContractVersionClaimsContext : ClaimsContext, IContractVersionClaimsContext
    {
        private int versionId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractVersionsRepository contractVersionsRepository;

        public ContractVersionClaimsContext(
            int versionId,
            [KeyFilter(ClaimsCaches.ContractVersion)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractVersionsRepository contractVersionsRepository)
            : base(claimsCache)
        {
            this.versionId = versionId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
        }

        public int ContractVersionId
        {
            get
            {
                return this.versionId;
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.versionId,
                    new ClaimKey("ContractId"),
                    () => this.contractVersionsRepository.GetContractId(this.versionId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.versionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
