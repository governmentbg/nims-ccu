using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class EvalSessionSheetCanceledEvent : IDomainEvent
    {
        public int EvalSessionSheetUserId { get; set; }
    }
}
