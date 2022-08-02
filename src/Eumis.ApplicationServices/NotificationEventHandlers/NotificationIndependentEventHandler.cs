using Eumis.Data.Notifications.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.NotificationEvents;
using Eumis.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.NotificationEventHandlers
{
    public class NotificationIndependentEventHandler : NotificationEventHandler<ProgrammeIndependentEvent>
    {
        private INotificationEntriesRepository notificationEntriesRepository;

        public NotificationIndependentEventHandler(INotificationEntriesRepository notificationEntriesRepository)
        {
            this.notificationEntriesRepository = notificationEntriesRepository;
        }

        public override void Handle(ProgrammeIndependentEvent independaentEvent)
        {
            var notificationEvent = this.notificationEntriesRepository.GetNotificationEvent(independaentEvent.EventType);
            var entry = new NotificationEntry(independaentEvent, notificationEvent);

            this.notificationEntriesRepository.Add(entry);
        }
    }
}
