using Autofac.Features.AttributeFilters;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.Contract
{
    internal class ContractClaimsContext : ClaimsContext, IContractClaimsContext
    {
        private int contractId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;

        public ContractClaimsContext(
            int contractId,
            [KeyFilter(ClaimsCaches.Contract)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository)
            : base(claimsCache)
        {
            this.contractId = contractId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
        }

        public int ContractId
        {
            get
            {
                return this.contractId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.contractId));
            }
        }
    }
}
