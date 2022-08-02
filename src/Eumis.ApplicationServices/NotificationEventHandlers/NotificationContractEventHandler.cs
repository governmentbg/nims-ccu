using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.NotificationEvents;
using Eumis.Domain.Notifications;

namespace Eumis.ApplicationServices.NotificationEventHandlers
{
    public class NotificationContractEventHandler : NotificationEventHandler<ContractNotificationEvent>
    {
        private IContractsRepository contractsRepository;
        private INotificationEntriesRepository notificationEntriesRepository;
        private IProceduresRepository proceduresRepository;

        public NotificationContractEventHandler(
            INotificationEntriesRepository notificationEntriesRepository,
            IProceduresRepository proceduresRepository,
            IContractsRepository contractsRepository)
        {
            this.notificationEntriesRepository = notificationEntriesRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractsRepository = contractsRepository;
        }

        public override void Handle(ContractNotificationEvent contractNotificationEvent)
        {
            var contract = this.contractsRepository.FindWithoutIncludes(contractNotificationEvent.ContractId);

            var programmePriorityId = this.proceduresRepository.GetRelatedProgrammePriority(contract.ProgrammeId, contract.ProcedureId);

            var notificationEvent = this.notificationEntriesRepository.GetNotificationEvent(contractNotificationEvent.EventType);
            var entry = new NotificationEntry(contractNotificationEvent, notificationEvent);

            entry.ContractId = contractNotificationEvent.ContractId;
            entry.ProcedureId = contract.ProcedureId;
            entry.ProgrammePriorityId = programmePriorityId;
            entry.ProgrammeId = contract.ProgrammeId;

            this.notificationEntriesRepository.Add(entry);
        }
    }
}
