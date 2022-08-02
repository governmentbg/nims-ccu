using System;

namespace Eumis.Public.Domain.Entities.Umis
{
    public interface IAggregateRoot
    {
        byte[] Version { get; set; }

        DateTime CreateDate { get; set; }

        DateTime ModifyDate { get; set; }
    }
}
