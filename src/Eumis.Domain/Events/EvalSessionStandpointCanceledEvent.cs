using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class EvalSessionStandpointCanceledEvent : IDomainEvent
    {
        public int EvalSessionStandpointUserId { get; set; }
    }
}
