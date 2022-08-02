using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ContractReportStatusChangedEvent : IDomainEvent
    {
        public int ContractReportId { get; set; }
        public ContractReportStatus Status { get; set; }
    }
}
