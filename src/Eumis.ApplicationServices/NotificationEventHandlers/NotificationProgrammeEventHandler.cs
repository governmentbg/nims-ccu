using Eumis.Data.Indicators.Repositories;
using Eumis.Data.Notifications.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Notifications;

namespace Eumis.ApplicationServices.NotificationEventHandlers
{
    public class NotificationProgrammeEventHandler : NotificationEventHandler<ProgrammeNotificationEvent>
    {
        private INotificationEntriesRepository notificationEntriesRepository;

        public NotificationProgrammeEventHandler(
            INotificationEntriesRepository notificationEntriesRepository)
        {
            this.notificationEntriesRepository = notificationEntriesRepository;
        }

        public override void Handle(ProgrammeNotificationEvent programmeEvent)
        {
            var notificationEvent = this.notificationEntriesRepository.GetNotificationEvent(programmeEvent.EventType);
            var entry = new NotificationEntry(programmeEvent, notificationEvent);
            entry.ProgrammeId = programmeEvent.ProgrammeId;
            this.notificationEntriesRepository.Add(entry);
        }
    }
}
