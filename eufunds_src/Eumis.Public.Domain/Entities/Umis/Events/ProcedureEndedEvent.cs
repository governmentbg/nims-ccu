using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ProcedureEndedEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
