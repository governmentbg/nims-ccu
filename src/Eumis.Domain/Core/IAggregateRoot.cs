using System;

namespace Eumis.Domain
{
    public interface IAggregateRoot
    {
        byte[] Version { get; set; }

        DateTime CreateDate { get; set; }

        DateTime ModifyDate { get; set; }
    }
}
