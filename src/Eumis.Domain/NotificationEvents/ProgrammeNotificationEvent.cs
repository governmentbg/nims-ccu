using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Core
{
    public class ProgrammeNotificationEvent : DispatchResolver, INotificationEvent
    {
        public ProgrammeNotificationEvent(NotificationEventType notificationEventType, int dispatchId, int programmeId)
            : base(notificationEventType, dispatchId)
        {
            this.ProgrammeId = programmeId;
        }

        public int ProgrammeId { get; set; }
    }
}
