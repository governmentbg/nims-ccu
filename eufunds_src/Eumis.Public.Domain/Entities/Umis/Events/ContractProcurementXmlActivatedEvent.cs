using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ContractProcurementXmlActivatedEvent : IDomainEvent
    {
        public int ContractId { get; set; }
        public int ContractProcurementXmlId { get; set; }
    }
}
