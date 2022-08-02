using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.NotificationEvents
{
    public class ProcedureNotificationEvent : DispatchResolver, INotificationEvent
    {
        public ProcedureNotificationEvent(NotificationEventType notificationEvent, int dispatcherId, int procedureId)
            : base(notificationEvent, dispatcherId)
        {
            this.ProcedureId = procedureId;
        }

        public ProcedureNotificationEvent(int procedureId, NotificationEventType notificationEvent)
            : this(notificationEvent, procedureId, procedureId)
        {
        }

        public int ProcedureId { get; set; }
    }
}
