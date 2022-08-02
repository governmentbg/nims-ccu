using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ProcedureQaActivatedEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
