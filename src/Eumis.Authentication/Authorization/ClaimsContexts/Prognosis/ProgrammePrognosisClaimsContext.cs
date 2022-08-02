using Autofac.Features.AttributeFilters;
using Eumis.Data.ActuallyPaidAmounts.Repositories;
using Eumis.Data.Prognoses.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.Prognosis
{
    internal class ProgrammePrognosisClaimsContext : ClaimsContext, IProgrammePrognosisClaimsContext
    {
        private int prognosisId;

        private IClaimsCache claimsCache;
        private IPrognosesRepository prognosesRepository;

        public ProgrammePrognosisClaimsContext(
            int prognosisId,
            [KeyFilter(ClaimsCaches.ProgrammePrognosis)]IClaimsCache claimsCache,
            IPrognosesRepository prognosesRepository)
            : base(claimsCache)
        {
            this.prognosisId = prognosisId;
            this.claimsCache = claimsCache;
            this.prognosesRepository = prognosesRepository;
        }

        public int PrognosistId
        {
            get
            {
                return this.prognosisId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.prognosisId,
                    new ClaimKey("ProgrammeId"),
                    () => this.prognosesRepository.GetProgrammePrognosisProgrammeId(this.prognosisId));
            }
        }
    }
}
