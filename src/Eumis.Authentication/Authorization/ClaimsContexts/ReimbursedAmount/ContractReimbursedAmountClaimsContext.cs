using Autofac.Features.AttributeFilters;
using Eumis.Data.ReimbursedAmounts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ReimbursedAmount
{
    internal class ContractReimbursedAmountClaimsContext : ClaimsContext, IContractReimbursedAmountClaimsContext
    {
        private int reimbursedAmountId;

        private IClaimsCache claimsCache;
        private IContractReimbursedAmountsRepository reimbursedAmountsRepository;

        public ContractReimbursedAmountClaimsContext(
            int reimbursedAmountId,
            [KeyFilter(ClaimsCaches.ContractReimbursedAmount)]IClaimsCache claimsCache,
            IContractReimbursedAmountsRepository reimbursedAmountsRepository)
            : base(claimsCache)
        {
            this.reimbursedAmountId = reimbursedAmountId;
            this.claimsCache = claimsCache;
            this.reimbursedAmountsRepository = reimbursedAmountsRepository;
        }

        public int ReimbursedAmountId
        {
            get
            {
                return this.reimbursedAmountId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.reimbursedAmountId,
                    new ClaimKey("ProgrammeId"),
                    () => this.reimbursedAmountsRepository.GetProgrammeId(this.reimbursedAmountId));
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.reimbursedAmountId,
                    new ClaimKey("ContractId"),
                    () => this.reimbursedAmountsRepository.GetContractId(this.reimbursedAmountId));
            }
        }
    }
}
