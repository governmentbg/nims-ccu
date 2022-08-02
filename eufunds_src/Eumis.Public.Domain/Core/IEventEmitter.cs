using System.Collections.Generic;

namespace Eumis.Public.Domain.Core
{
    public interface IEventEmitter
    {
        ICollection<IDomainEvent> Events { get; set; }
    }
}
