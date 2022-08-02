using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ProcedureTerminatedEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
