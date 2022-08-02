using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ProcedureQaActivatedEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
