using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ContractVersionActivatedEvent : IDomainEvent
    {
        public int ContractVersionId { get; set; }
    }
}
