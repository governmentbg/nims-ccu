using Eumis.Data.Contracts.Repositories;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.NotificationEvents;
using Eumis.Domain.Notifications;

namespace Eumis.ApplicationServices.NotificationEventHandlers
{
    public class NotificationEvalSessionEventHandler : NotificationEventHandler<EvalSessionNotificationEvent>
    {
        private IEvalSessionsRepository evalSessionsRepository;
        private INotificationEntriesRepository notificationEntriesRepository;
        private IProceduresRepository proceduresRepository;

        public NotificationEvalSessionEventHandler(
            INotificationEntriesRepository notificationEntriesRepository,
            IProceduresRepository proceduresRepository,
            IEvalSessionsRepository evalSessionsRepository)
        {
            this.notificationEntriesRepository = notificationEntriesRepository;
            this.proceduresRepository = proceduresRepository;
            this.evalSessionsRepository = evalSessionsRepository;
        }

        public override void Handle(EvalSessionNotificationEvent evalSessionEvent)
        {
            var evalSession = this.evalSessionsRepository.FindWithoutIncludes(evalSessionEvent.EvalSessionId);

            var notificationEvent = this.notificationEntriesRepository.GetNotificationEvent(evalSessionEvent.EventType);
            var entry = new NotificationEntry(evalSessionEvent, notificationEvent);

            entry.ProcedureId = evalSession.ProcedureId;

            var procedureData = this.proceduresRepository.GetProcedureParentData(entry.ProcedureId.Value);
            entry.ProgrammePriorityId = procedureData.ProgrammePriorityId;
            entry.ProgrammeId = procedureData.ProgrammeId;
            entry.DispatcherId = evalSessionEvent.DispatcherId;

            this.notificationEntriesRepository.Add(entry);
        }
    }
}
