using Autofac.Features.AttributeFilters;
using Eumis.Data.Irregularities.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.Irregularity
{
    internal class IrregularityVersionClaimsContext : ClaimsContext, IIrregularityVersionClaimsContext
    {
        private int irregularityVersionId;

        private IClaimsCache claimsCache;
        private IIrregularityVersionsRepository irregularityVersionsRepository;
        private IIrregularityClaimsContext irregularityClaimsContext;

        public IrregularityVersionClaimsContext(
            int irregularityVersionId,
            [KeyFilter(ClaimsCaches.IrregularityVersion)]IClaimsCache claimsCache,
            IIrregularityVersionsRepository irregularityVersionsRepository,
            IrregularityClaimsContextFactory irregularityClaimsContextFactory)
            : base(claimsCache)
        {
            this.irregularityVersionId = irregularityVersionId;
            this.claimsCache = claimsCache;
            this.irregularityVersionsRepository = irregularityVersionsRepository;

            this.irregularityClaimsContext = (IIrregularityClaimsContext)irregularityClaimsContextFactory(this.IrregularityId);
        }

        public int IrregularityVersionId
        {
            get
            {
                return this.irregularityVersionId;
            }
        }

        public int IrregularityId
        {
            get
            {
                return this.GetClaim(
                    this.irregularityVersionId,
                    new ClaimKey("IrregularityId"),
                    () => this.irregularityVersionsRepository.GetIrregularityId(this.irregularityVersionId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.irregularityVersionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.irregularityVersionsRepository.GetProgrammeId(this.irregularityVersionId));
            }
        }

        public bool IsIrregularityAssociatedWithFinancialCorrection()
        {
            return this.irregularityClaimsContext.IsIrregularityAssociatedWithFinancialCorrection();
        }
    }
}
