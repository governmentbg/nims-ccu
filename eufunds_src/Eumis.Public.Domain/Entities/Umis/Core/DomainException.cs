using System;

namespace Eumis.Public.Domain.Entities.Umis
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }
    }
}
