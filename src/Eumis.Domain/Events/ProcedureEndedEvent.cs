using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ProcedureEndedEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
