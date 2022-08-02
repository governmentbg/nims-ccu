using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ProcedureCanceledEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
