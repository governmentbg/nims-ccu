using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class EvalSessionStandpointCanceledEvent : IDomainEvent
    {
        public int EvalSessionStandpointUserId { get; set; }
    }
}
