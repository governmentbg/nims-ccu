using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class UserActivatedEvent : IDomainEvent
    {
        public int UserId { get; set; }

        public string NewPasswordCode { get; set; }
    }
}
