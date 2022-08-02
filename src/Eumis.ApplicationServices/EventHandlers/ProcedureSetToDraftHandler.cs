using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Events;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ProcedureSetToDraftHandler : Eumis.Domain.Core.EventHandler<ProcedureSetToDraftEvent>
    {
        private IProcedureVersionsRepository procedureVersionsRepository;

        public ProcedureSetToDraftHandler(IProcedureVersionsRepository procedureVersionsRepository)
        {
            this.procedureVersionsRepository = procedureVersionsRepository;
        }

        public override void Handle(ProcedureSetToDraftEvent e)
        {
            var prevVersion = this.procedureVersionsRepository.GetLastVersion(e.ProcedureId);
            if (prevVersion != null)
            {
                prevVersion.DeactivateVersion();
            }
        }
    }
}
