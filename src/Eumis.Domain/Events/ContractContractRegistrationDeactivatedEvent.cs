using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ContractContractRegistrationDeactivatedEvent : IDomainEvent
    {
        public int ContractId { get; set; }

        public int ContractRegistrationId { get; set; }
    }
}
