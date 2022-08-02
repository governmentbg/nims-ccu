using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.NotificationEvents
{
    public class ContractNotificationEvent : DispatchResolver, INotificationEvent
    {
        public ContractNotificationEvent(NotificationEventType notificationEvent, int dispatcherId, int contractId)
            : base(notificationEvent, dispatcherId)
        {
            this.ContractId = contractId;
        }

        public ContractNotificationEvent(NotificationEventType notificationEvent, int dispatcherId, int parentId, int contractId)
            : base(notificationEvent, parentId, dispatcherId)
        {
            this.ContractId = contractId;
        }

        public ContractNotificationEvent(NotificationEventType notificationEvent, int contractId, int dispatcherId, DispatchParameterIdentifier dispatchParameterName)
            : base(notificationEvent, contractId, dispatcherId, dispatchParameterName)
        {
            this.ContractId = contractId;
        }

        public int ContractId { get; set; }
    }
}
