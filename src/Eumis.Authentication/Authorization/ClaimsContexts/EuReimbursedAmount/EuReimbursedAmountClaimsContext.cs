using Autofac.Features.AttributeFilters;
using Eumis.Data.EuReimbursedAmounts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.EuReimbursedAmount
{
    internal class EuReimbursedAmountClaimsContext : ClaimsContext, IEuReimbursedAmountClaimsContext
    {
        private int euReimbursedAmountId;

        private IClaimsCache claimsCache;
        private IEuReimbursedAmountsRepository euReimbursedAmountsRepository;

        public EuReimbursedAmountClaimsContext(
            int euReimbursedAmountId,
            [KeyFilter(ClaimsCaches.EuReimbursedAmount)]IClaimsCache claimsCache,
            IEuReimbursedAmountsRepository euReimbursedAmountsRepository)
            : base(claimsCache)
        {
            this.euReimbursedAmountId = euReimbursedAmountId;
            this.claimsCache = claimsCache;
            this.euReimbursedAmountsRepository = euReimbursedAmountsRepository;
        }

        public int EuReimbursedAmountId
        {
            get
            {
                return this.euReimbursedAmountId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.euReimbursedAmountId,
                    new ClaimKey("ProgrammeId"),
                    () => this.euReimbursedAmountsRepository.GetProgrammeId(this.euReimbursedAmountId));
            }
        }
    }
}
