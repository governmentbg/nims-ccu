using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class EvalSessionSheetCanceledEvent : IDomainEvent
    {
        public int EvalSessionSheetUserId { get; set; }
    }
}
