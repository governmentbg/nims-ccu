using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class RequestPackageEnteredEvent : IDomainEvent
    {
        public int RequestPackageId { get; set; }
    }
}
