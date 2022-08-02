using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ContractContractRegistrationActivatedEvent : IDomainEvent
    {
        public int ContractId { get; set; }

        public int ContractRegistrationId { get; set; }
    }
}
