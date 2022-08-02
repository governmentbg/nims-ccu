using Autofac.Features.AttributeFilters;
using Eumis.Data.Procedures.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ProcedureMassCommunication
{
    internal class ProcedureMassCommunicationClaimsContext : ClaimsContext, IProcedureMassCommunicationClaimsContext
    {
        private int procedureMassCommunicationId;

        private IProceduresRepository proceduresRepository;
        private IProcedureMassCommunicationsRepository communicationsRepository;

        public ProcedureMassCommunicationClaimsContext(
            int procedureMassCommunicationId,
            [KeyFilter(ClaimsCaches.ProcedureMassCommunication)]IClaimsCache claimsCache,
            IProceduresRepository proceduresRepository,
            IProcedureMassCommunicationsRepository communicationsRepository)
            : base(claimsCache)
        {
            this.procedureMassCommunicationId = procedureMassCommunicationId;
            this.proceduresRepository = proceduresRepository;
            this.communicationsRepository = communicationsRepository;
        }

        public int ProcedureId
        {
            get
            {
                return this.GetClaim(
                    this.procedureMassCommunicationId,
                    new ClaimKey("ProcedureId"),
                    () => this.communicationsRepository.GetCommunicationProcedureId(this.procedureMassCommunicationId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.ProcedureId,
                    new ClaimKey("ProgrammeId"),
                    () => this.proceduresRepository.GetPrimaryProcedureProgrammeId(this.ProcedureId));
            }
        }
    }
}
