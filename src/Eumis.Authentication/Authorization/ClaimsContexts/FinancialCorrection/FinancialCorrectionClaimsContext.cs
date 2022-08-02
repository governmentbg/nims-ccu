using Autofac.Features.AttributeFilters;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.FinancialCorrections.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.FinancialCorrection
{
    internal class FinancialCorrectionClaimsContext : ClaimsContext, IFinancialCorrectionClaimsContext
    {
        private int financialCorrectionId;

        private IClaimsCache claimsCache;
        private IFinancialCorrectionsRepository financialCorrectionsRepository;
        private IContractsRepository contractsRepository;

        public FinancialCorrectionClaimsContext(
            int financialCorrectionId,
            [KeyFilter(ClaimsCaches.FinancialCorrection)]IClaimsCache claimsCache,
            IFinancialCorrectionsRepository financialCorrectionsRepository,
            IContractsRepository contractsRepository)
            : base(claimsCache)
        {
            this.financialCorrectionId = financialCorrectionId;
            this.claimsCache = claimsCache;
            this.financialCorrectionsRepository = financialCorrectionsRepository;
            this.contractsRepository = contractsRepository;
        }

        public int FinancialCorrectionId
        {
            get
            {
                return this.financialCorrectionId;
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.financialCorrectionId,
                    new ClaimKey("ContractId"),
                    () => this.financialCorrectionsRepository.GetContractId(this.financialCorrectionId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.financialCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractsRepository.GetProgrammeId(this.ContractId));
            }
        }
    }
}
