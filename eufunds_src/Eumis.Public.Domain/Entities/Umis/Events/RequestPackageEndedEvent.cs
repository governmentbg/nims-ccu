using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class RequestPackageEndedEvent : IDomainEvent
    {
        public int RequestPackageId { get; set; }

        public int[] UserIds { get; set; }
    }
}
