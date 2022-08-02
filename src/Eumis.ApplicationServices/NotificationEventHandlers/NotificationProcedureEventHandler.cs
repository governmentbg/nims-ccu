using Eumis.Data.Notifications.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.NotificationEvents;
using Eumis.Domain.Notifications;

namespace Eumis.ApplicationServices.NotificationEventHandlers
{
    public class NotificationProcedureEventHandler : NotificationEventHandler<ProcedureNotificationEvent>
    {
        private INotificationEntriesRepository notificationEntriesRepository;
        private IProceduresRepository proceduresRepository;

        public NotificationProcedureEventHandler(
            INotificationEntriesRepository notificationEntriesRepository,
            IProceduresRepository proceduresRepository)
        {
            this.notificationEntriesRepository = notificationEntriesRepository;
            this.proceduresRepository = proceduresRepository;
        }

        public override void Handle(ProcedureNotificationEvent procedureStatusChangedEvent)
        {
            var procedureData = this.proceduresRepository.GetProcedureParentData((int)procedureStatusChangedEvent.ProcedureId);
            var notificationEvent = this.notificationEntriesRepository.GetNotificationEvent(procedureStatusChangedEvent.EventType);

            var entry = new NotificationEntry(procedureStatusChangedEvent, notificationEvent);
            entry.ProcedureId = procedureStatusChangedEvent.ProcedureId;
            entry.ProgrammePriorityId = procedureData.ProgrammePriorityId;
            entry.ProgrammeId = procedureData.ProgrammeId;

            this.notificationEntriesRepository.Add(entry);
        }
    }
}
