using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ProcedureCanceledEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
