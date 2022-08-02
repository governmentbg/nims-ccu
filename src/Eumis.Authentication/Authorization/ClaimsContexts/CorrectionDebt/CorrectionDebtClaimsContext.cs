using Autofac.Features.AttributeFilters;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.FlatFinancialCorrections.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.CorrectionDebt
{
    internal class CorrectionDebtClaimsContext : ClaimsContext, ICorrectionDebtClaimsContext
    {
        private int correctionDebtId;

        private IClaimsCache claimsCache;
        private ICorrectionDebtsRepository correctionDebtsRepository;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;

        public CorrectionDebtClaimsContext(
            int correctionDebtId,
            [KeyFilter(ClaimsCaches.CorrectionDebt)]IClaimsCache claimsCache,
            ICorrectionDebtsRepository correctionDebtsRepository,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository)
            : base(claimsCache)
        {
            this.correctionDebtId = correctionDebtId;
            this.claimsCache = claimsCache;
            this.correctionDebtsRepository = correctionDebtsRepository;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
        }

        public int CorrectionDebtId
        {
            get
            {
                return this.correctionDebtId;
            }
        }

        public int FlatFinancialCorrectionId
        {
            get
            {
                return this.GetClaim(
                    this.correctionDebtId,
                    new ClaimKey("ContractId"),
                    () => this.correctionDebtsRepository.GetFlatFinancialCorrectionId(this.correctionDebtId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.correctionDebtId,
                    new ClaimKey("ProgrammeId"),
                    () => this.flatFinancialCorrectionsRepository.GetProgrammeId(this.FlatFinancialCorrectionId));
            }
        }
    }
}
