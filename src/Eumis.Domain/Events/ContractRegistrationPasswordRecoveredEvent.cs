using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ContractRegistrationPasswordRecoveredEvent : IDomainEvent
    {
        public int ContractRegistrationId { get; set; }
    }
}
