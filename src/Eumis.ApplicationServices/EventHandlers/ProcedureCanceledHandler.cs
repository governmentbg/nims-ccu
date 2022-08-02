using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Events;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ProcedureCanceledHandler : Eumis.Domain.Core.EventHandler<ProcedureCanceledEvent>
    {
        private IProcedureVersionsRepository procedureVersionsRepository;

        public ProcedureCanceledHandler(IProcedureVersionsRepository procedureVersionsRepository)
        {
            this.procedureVersionsRepository = procedureVersionsRepository;
        }

        public override void Handle(ProcedureCanceledEvent e)
        {
            var prevVersion = this.procedureVersionsRepository.GetLastVersion(e.ProcedureId);
            if (prevVersion != null)
            {
                prevVersion.DeactivateVersion();
            }
        }
    }
}
