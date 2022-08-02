using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ContractVersionActivatedEvent : IDomainEvent
    {
        public int ContractVersionId { get; set; }
    }
}
