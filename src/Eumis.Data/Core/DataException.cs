using System;

namespace Eumis.Data
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
