using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ProcedureSetToDraftEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
