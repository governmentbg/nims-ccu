using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class UserActivatedEvent : IDomainEvent
    {
        public int UserId { get; set; }

        public string NewPasswordCode { get; set; }
    }
}
