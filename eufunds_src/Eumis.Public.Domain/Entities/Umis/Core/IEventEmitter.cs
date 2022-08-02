using System.Collections.Generic;

namespace Eumis.Public.Domain.Entities.Umis.Core
{
    public interface IEventEmitter
    {
        ICollection<IDomainEvent> Events { get; set; }
    }
}
