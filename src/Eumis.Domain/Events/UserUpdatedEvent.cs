using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class UserUpdatedEvent : IDomainEvent
    {
        public int UserId { get; set; }
    }
}
