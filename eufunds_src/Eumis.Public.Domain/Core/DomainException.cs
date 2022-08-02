using System;

namespace Eumis.Public.Domain.Core
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }
    }
}
