using Autofac.Features.AttributeFilters;
using Eumis.Data.Contracts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractSpendingPlan
{
    internal class ContractSpendingPlanClaimsContext : ClaimsContext, IContractSpendingPlanClaimsContext
    {
        private int spendingPlanId;

        private IClaimsCache claimsCache;
        private IContractsRepository contractsRepository;
        private IContractSpendingPlansRepository contractSpendingPlansRepository;

        public ContractSpendingPlanClaimsContext(
            int spendingPlanId,
            [KeyFilter(ClaimsCaches.ContractSpendingPlan)]IClaimsCache claimsCache,
            IContractsRepository contractsRepository,
            IContractSpendingPlansRepository contractSpendingPlansRepository)
            : base(claimsCache)
        {
            this.spendingPlanId = spendingPlanId;
            this.claimsCache = claimsCache;
            this.contractsRepository = contractsRepository;
            this.contractSpendingPlansRepository = contractSpendingPlansRepository;
        }

        public int ContractSpendingPlanId
        {
            get
            {
                return this.spendingPlanId;
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.spendingPlanId,
                    new ClaimKey("ContractId"),
                    () => this.contractSpendingPlansRepository.GetContractId(this.spendingPlanId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.spendingPlanId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
