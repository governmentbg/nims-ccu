using System;

namespace Eumis.Public.Data.Core
{
    public class DataException : Exception
    {
        public DataException()
        {
        }

        public DataException(string message)
            : base(message)
        {
        }
    }
}
