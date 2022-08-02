using Autofac.Features.AttributeFilters;
using Eumis.Data.ActuallyPaidAmounts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ActuallyPaidAmount
{
    internal class ActuallyPaidAmountClaimsContext : ClaimsContext, IActuallyPaidAmountClaimsContext
    {
        private int actuallyPaidAmountId;

        private IClaimsCache claimsCache;
        private IActuallyPaidAmountsRepository actuallyPaidAmountsRepository;

        public ActuallyPaidAmountClaimsContext(
            int actuallyPaidAmountId,
            [KeyFilter(ClaimsCaches.ActuallyPaidAmount)]IClaimsCache claimsCache,
            IActuallyPaidAmountsRepository actuallyPaidAmountsRepository)
            : base(claimsCache)
        {
            this.actuallyPaidAmountId = actuallyPaidAmountId;
            this.claimsCache = claimsCache;
            this.actuallyPaidAmountsRepository = actuallyPaidAmountsRepository;
        }

        public int ActuallyPaidAmountId
        {
            get
            {
                return this.actuallyPaidAmountId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.actuallyPaidAmountId,
                    new ClaimKey("ProgrammeId"),
                    () => this.actuallyPaidAmountsRepository.GetProgrammeId(this.actuallyPaidAmountId));
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.actuallyPaidAmountId,
                    new ClaimKey("ContractId"),
                    () => this.actuallyPaidAmountsRepository.GetContractId(this.actuallyPaidAmountId));
            }
        }
    }
}
