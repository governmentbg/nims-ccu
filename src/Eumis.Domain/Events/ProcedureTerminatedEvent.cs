using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ProcedureTerminatedEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
