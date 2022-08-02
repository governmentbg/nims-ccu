using Autofac.Features.AttributeFilters;
using Eumis.Data.ActuallyPaidAmounts.Repositories;
using Eumis.Data.Prognoses.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.Prognosis
{
    internal class ProcedurePrognosisClaimsContext : ClaimsContext, IProcedurePrognosisClaimsContext
    {
        private int prognosisId;

        private IClaimsCache claimsCache;
        private IPrognosesRepository prognosesRepository;

        public ProcedurePrognosisClaimsContext(
            int prognosisId,
            [KeyFilter(ClaimsCaches.ProcedurePrognosis)]IClaimsCache claimsCache,
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
                    () => this.prognosesRepository.GetProcedurePrognosisProgrammeId(this.prognosisId));
            }
        }
    }
}
