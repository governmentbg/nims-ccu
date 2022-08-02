using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class EvalSessionStatusChangedEvent : IDomainEvent
    {
        public EvalSessionStatusChangedEvent()
        {
        }

        public EvalSessionStatusChangedEvent(int evalSessionId)
            : this()
        {
            this.EvalSessionId = evalSessionId;
        }

        public int EvalSessionId { get; set; }
    }
}
