using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ContractRegistrationPasswordRecoveredEvent : IDomainEvent
    {
        public int ContractRegistrationId { get; set; }
    }
}
