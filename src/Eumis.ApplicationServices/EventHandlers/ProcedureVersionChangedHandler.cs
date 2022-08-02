using Eumis.ApplicationServices.Services.ProcedureVersion;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Events;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ProcedureVersionChangedHandler : IEventHandler
    {
        private IProcedureVersionService procedureVersionService;

        public ProcedureVersionChangedHandler(IProcedureVersionService procedureVersionService)
        {
            this.procedureVersionService = procedureVersionService;
        }

        public void Handle(IDomainEvent e)
        {
            if (e is ProcedureActivatedEvent)
            {
                this.Handle(((ProcedureActivatedEvent)e).ProcedureId);
            }
            else if (e is ProcedureQaActivatedEvent)
            {
                this.Handle(((ProcedureQaActivatedEvent)e).ProcedureId);
            }
        }

        private void Handle(int procedureId)
        {
            this.procedureVersionService.CreateProcedureVersion(procedureId);
        }
    }
}
