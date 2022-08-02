using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ProcedureSetToDraftEvent : IDomainEvent
    {
        public int ProcedureId { get; set; }
    }
}
