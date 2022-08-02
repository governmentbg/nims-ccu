using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ContractAuthorityCommunicationSentEvent : IDomainEvent
    {
        public int ContractId { get; set; }

        public int ContractCommunicationXmlId { get; set; }
    }
}
