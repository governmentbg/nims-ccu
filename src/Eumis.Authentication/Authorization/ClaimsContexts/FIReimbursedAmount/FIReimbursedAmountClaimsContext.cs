using Autofac.Features.AttributeFilters;
using Eumis.Data.FIReimbursedAmounts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.FIReimbursedAmount
{
    internal class FIReimbursedAmountClaimsContext : ClaimsContext, IFIReimbursedAmountClaimsContext
    {
        private int fiReimbursedAmountId;

        private IClaimsCache claimsCache;
        private IFIReimbursedAmountsRepository fiReimbursedAmountsRepository;

        public FIReimbursedAmountClaimsContext(
            int fiReimbursedAmountId,
            [KeyFilter(ClaimsCaches.FIReimbursedAmount)]IClaimsCache claimsCache,
            IFIReimbursedAmountsRepository fiReimbursedAmountsRepository)
            : base(claimsCache)
        {
            this.fiReimbursedAmountId = fiReimbursedAmountId;
            this.claimsCache = claimsCache;
            this.fiReimbursedAmountsRepository = fiReimbursedAmountsRepository;
        }

        public int FIReimbursedAmountId
        {
            get
            {
                return this.fiReimbursedAmountId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.fiReimbursedAmountId,
                    new ClaimKey("ProgrammeId"),
                    () => this.fiReimbursedAmountsRepository.GetProgrammeId(this.fiReimbursedAmountId));
            }
        }
    }
}
