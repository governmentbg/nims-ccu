using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Events;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ProcedureTerminatedHandler : Eumis.Domain.Core.EventHandler<ProcedureTerminatedEvent>
    {
        private IProcedureVersionsRepository procedureVersionsRepository;

        public ProcedureTerminatedHandler(IProcedureVersionsRepository procedureVersionsRepository)
        {
            this.procedureVersionsRepository = procedureVersionsRepository;
        }

        public override void Handle(ProcedureTerminatedEvent e)
        {
            var prevVersion = this.procedureVersionsRepository.GetLastVersion(e.ProcedureId);

            prevVersion.DeactivateVersion();
        }
    }
}
