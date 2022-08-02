using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class RegistrationPasswordRecoveredEvent : IDomainEvent
    {
        public int RegistrationId { get; set; }
    }
}
