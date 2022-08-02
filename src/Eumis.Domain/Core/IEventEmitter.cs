using System.Collections.Generic;

namespace Eumis.Domain.Core
{
    public interface IEventEmitter
    {
        ICollection<IDomainEvent> Events { get; set; }
    }
}
