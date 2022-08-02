using Autofac.Features.AttributeFilters;
using Eumis.Data.Irregularities.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.Irregularity
{
    internal class IrregularityClaimsContext : ClaimsContext, IIrregularityClaimsContext
    {
        private int irregularityId;

        private IClaimsCache claimsCache;
        private IIrregularitiesRepository irregularitiesRepository;

        public IrregularityClaimsContext(
            int irregularityId,
            [KeyFilter(ClaimsCaches.Irregularity)]IClaimsCache claimsCache,
            IIrregularitiesRepository irregularitiesRepository)
            : base(claimsCache)
        {
            this.irregularityId = irregularityId;
            this.claimsCache = claimsCache;
            this.irregularitiesRepository = irregularitiesRepository;
        }

        public int IrregularityId
        {
            get
            {
                return this.irregularityId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.irregularityId,
                    new ClaimKey("ProgrammeId"),
                    () => this.irregularitiesRepository.GetProgrammeId(this.irregularityId));
            }
        }

        public int? ContractId
        {
            get
            {
                return this.GetClaim(
                    this.irregularityId,
                    new ClaimKey("ContractId"),
                    () => this.irregularitiesRepository.GetContractId(this.irregularityId));
            }
        }

        public bool IsIrregularityAssociatedWithFinancialCorrection()
        {
            return this.GetClaim(
                this.irregularityId,
                new ClaimKey("IsIrregularityAssociatedWithFinancialCorrection", this.irregularityId.ToString()),
                () => this.irregularitiesRepository.HasFinancialCorrections(this.irregularityId));
        }
    }
}
