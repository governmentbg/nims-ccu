using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ContractVersionContractDateChangedEvent : IDomainEvent
    {
        public int ContractVersionId { get; set; }
    }
}
