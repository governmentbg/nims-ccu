using Autofac.Features.AttributeFilters;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.SpotChecks.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.SpotCheck
{
    internal class SpotCheckClaimsContext : ClaimsContext, ISpotCheckClaimsContext
    {
        private int spotCheckId;

        private IClaimsCache claimsCache;
        private ISpotChecksRepository spotChecksRepository;

        public SpotCheckClaimsContext(
            int spotCheckId,
            [KeyFilter(ClaimsCaches.SpotCheck)]IClaimsCache claimsCache,
            ISpotChecksRepository spotChecksRepository)
            : base(claimsCache)
        {
            this.spotCheckId = spotCheckId;
            this.claimsCache = claimsCache;
            this.spotChecksRepository = spotChecksRepository;
        }

        public int SpotCheckId
        {
            get
            {
                return this.spotCheckId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.spotCheckId,
                    new ClaimKey("ProgrammeId"),
                    () => this.spotChecksRepository.GetProgrammeId(this.spotCheckId));
            }
        }

        public int? ContractId
        {
            get
            {
                return this.GetClaim(
                    this.spotCheckId,
                    new ClaimKey("ContractId"),
                    () => this.spotChecksRepository.GetContractId(this.spotCheckId));
            }
        }
    }
}
