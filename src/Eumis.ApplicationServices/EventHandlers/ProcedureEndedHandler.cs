using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Events;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ProcedureEndedHandler : Eumis.Domain.Core.EventHandler<ProcedureEndedEvent>
    {
        private IProcedureVersionsRepository procedureVersionsRepository;

        public ProcedureEndedHandler(IProcedureVersionsRepository procedureVersionsRepository)
        {
            this.procedureVersionsRepository = procedureVersionsRepository;
        }

        public override void Handle(ProcedureEndedEvent e)
        {
            var prevVersion = this.procedureVersionsRepository.GetLastVersion(e.ProcedureId);

            prevVersion.DeactivateVersion();
        }
    }
}
