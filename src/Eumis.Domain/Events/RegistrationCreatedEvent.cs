using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class RegistrationCreatedEvent : IDomainEvent
    {
        public string Email { get; set; }

        public string ActivationCode { get; set; }
    }
}
