using System;

namespace Eumis.Domain
{
    public class HistoricContractImportException : Exception
    {
        public HistoricContractImportException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
