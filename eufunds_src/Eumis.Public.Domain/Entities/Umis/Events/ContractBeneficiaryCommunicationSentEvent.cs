using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ContractBeneficiaryCommunicationSentEvent : IDomainEvent
    {
        public int ContractId { get; set; }

        public int ContractCommunicationXmlId { get; set; }
    }
}
