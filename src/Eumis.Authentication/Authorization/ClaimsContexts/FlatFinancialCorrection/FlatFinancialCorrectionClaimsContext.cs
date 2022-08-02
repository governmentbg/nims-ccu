using Autofac.Features.AttributeFilters;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Data.Measures.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.FinancialCorrection
{
    internal class FlatFinancialCorrectionClaimsContext : ClaimsContext, IFlatFinancialCorrectionClaimsContext
    {
        private int flatFinancialCorrectionId;

        private IClaimsCache claimsCache;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;

        public FlatFinancialCorrectionClaimsContext(
            int flatFinancialCorrectionId,
            [KeyFilter(ClaimsCaches.FlatFinancialCorrection)]IClaimsCache claimsCache,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository)
            : base(claimsCache)
        {
            this.flatFinancialCorrectionId = flatFinancialCorrectionId;
            this.claimsCache = claimsCache;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
        }

        public int FlatFinancialCorrectionId
        {
            get
            {
                return this.flatFinancialCorrectionId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.flatFinancialCorrectionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.flatFinancialCorrectionsRepository.GetProgrammeId(this.FlatFinancialCorrectionId));
            }
        }
    }
}
