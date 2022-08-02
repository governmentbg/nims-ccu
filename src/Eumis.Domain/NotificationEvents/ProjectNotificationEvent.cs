using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.NotificationEvents
{
    public class ProjectNotificationEvent : DispatchResolver, INotificationEvent
    {
        public ProjectNotificationEvent(NotificationEventType eventType, int projectId, int dispatcherId)
            : base(eventType, projectId, dispatcherId)
        {
            this.ProjectId = projectId;
            this.DispatcherId = dispatcherId;
        }

        public int ProjectId { get; set; }

        public int DispatcherId { get; set; }
    }
}
