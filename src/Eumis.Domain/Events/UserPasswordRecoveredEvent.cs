using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class UserPasswordRecoveredEvent : IDomainEvent
    {
        public int UserId { get; set; }
    }
}
