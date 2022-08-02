using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ProcedureActivatedEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
