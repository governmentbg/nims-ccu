using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ContractProcurementXmlActivatedEvent : IDomainEvent
    {
        public int ContractId { get; set; }

        public int ContractProcurementXmlId { get; set; }
    }
}
