using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ProjectRegisteredEvent : IDomainEvent
    {
        public int RegProjectXmlId { get; set; }
    }
}
