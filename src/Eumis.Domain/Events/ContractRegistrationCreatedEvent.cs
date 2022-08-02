using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ContractRegistrationCreatedEvent : IDomainEvent
    {
        public string Email { get; set; }

        public string ActivationCode { get; set; }
    }
}
