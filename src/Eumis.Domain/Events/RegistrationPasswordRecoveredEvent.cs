using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class RegistrationPasswordRecoveredEvent : IDomainEvent
    {
        public int RegistrationId { get; set; }
    }
}
