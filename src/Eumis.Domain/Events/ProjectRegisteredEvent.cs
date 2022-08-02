using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ProjectRegisteredEvent : IDomainEvent
    {
        public int RegProjectXmlId { get; set; }
    }
}
