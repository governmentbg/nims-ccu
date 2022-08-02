using Autofac.Features.AttributeFilters;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Debts.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractDebt
{
    internal class ContractDebtClaimsContext : ClaimsContext, IContractDebtClaimsContext
    {
        private int contractDebtId;

        private IClaimsCache claimsCache;
        private IContractDebtsRepository contractDebtsRepository;
        private IContractsRepository contractsRepository;

        public ContractDebtClaimsContext(
            int contractDebtId,
            [KeyFilter(ClaimsCaches.ContractDebt)]IClaimsCache claimsCache,
            IContractDebtsRepository contractDebtsRepository,
            IContractsRepository contractsRepository)
            : base(claimsCache)
        {
            this.contractDebtId = contractDebtId;
            this.claimsCache = claimsCache;
            this.contractDebtsRepository = contractDebtsRepository;
            this.contractsRepository = contractsRepository;
        }

        public int ContractDebtId
        {
            get
            {
                return this.contractDebtId;
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.contractDebtId,
                    new ClaimKey("ContractId"),
                    () => this.contractDebtsRepository.GetContractId(this.contractDebtId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.contractDebtId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
