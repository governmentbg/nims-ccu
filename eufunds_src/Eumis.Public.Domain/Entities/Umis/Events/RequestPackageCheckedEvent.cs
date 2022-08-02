using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class RequestPackageCheckedEvent : IDomainEvent
    {
        public int RequestPackageId { get; set; }
    }
}
