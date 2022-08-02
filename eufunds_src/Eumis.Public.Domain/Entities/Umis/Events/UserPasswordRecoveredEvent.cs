using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class UserPasswordRecoveredEvent : IDomainEvent
    {
        public int UserId { get; set; }
    }
}
