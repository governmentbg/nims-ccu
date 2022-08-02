using Autofac.Features.AttributeFilters;
using Eumis.Data.Irregularities.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.IrregularitySignal
{
    internal class IrregularitySignalClaimsContext : ClaimsContext, IIrregularitySignalClaimsContext
    {
        private int irregularitySignalId;

        private IClaimsCache claimsCache;
        private IIrregularitySignalsRepository irregularitySignalsRepository;

        public IrregularitySignalClaimsContext(
            int irregularitySignalId,
            [KeyFilter(ClaimsCaches.IrregularitySignal)]IClaimsCache claimsCache,
            IIrregularitySignalsRepository irregularitySignalsRepository)
            : base(claimsCache)
        {
            this.irregularitySignalId = irregularitySignalId;
            this.claimsCache = claimsCache;
            this.irregularitySignalsRepository = irregularitySignalsRepository;
        }

        public int IrregularitySignalId
        {
            get
            {
                return this.irregularitySignalId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.irregularitySignalId,
                    new ClaimKey("ProgrammeId"),
                    () => this.irregularitySignalsRepository.GetProgrammeId(this.irregularitySignalId));
            }
        }

        public int? ContractId
        {
            get
            {
                return this.GetClaim(
                    this.irregularitySignalId,
                    new ClaimKey("ContractId"),
                    () => this.irregularitySignalsRepository.GetContractId(this.irregularitySignalId));
            }
        }
    }
}
