using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ProcedureActivatedEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
