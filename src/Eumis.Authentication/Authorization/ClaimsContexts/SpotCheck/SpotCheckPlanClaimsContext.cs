using Autofac.Features.AttributeFilters;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.SpotChecks.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.SpotCheck
{
    internal class SpotCheckPlanClaimsContext : ClaimsContext, ISpotCheckPlanClaimsContext
    {
        private int spotCheckPlanId;

        private IClaimsCache claimsCache;
        private ISpotCheckPlansRepository spotCheckPlansRepository;

        public SpotCheckPlanClaimsContext(
            int spotCheckPlanId,
            [KeyFilter(ClaimsCaches.SpotCheckPlan)]IClaimsCache claimsCache,
            ISpotCheckPlansRepository spotCheckPlansRepository)
            : base(claimsCache)
        {
            this.spotCheckPlanId = spotCheckPlanId;
            this.claimsCache = claimsCache;
            this.spotCheckPlansRepository = spotCheckPlansRepository;
        }

        public int SpotCheckPlanId
        {
            get
            {
                return this.spotCheckPlanId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.spotCheckPlanId,
                    new ClaimKey("ProgrammeId"),
                    () => this.spotCheckPlansRepository.GetProgrammeId(this.spotCheckPlanId));
            }
        }

        public int? ContractId
        {
            get
            {
                return this.GetClaim(
                    this.spotCheckPlanId,
                    new ClaimKey("ContractId"),
                    () => this.spotCheckPlansRepository.GetContractId(this.spotCheckPlanId));
            }
        }
    }
}
