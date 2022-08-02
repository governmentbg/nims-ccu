using System;

namespace Eumis.Common.Log
{
    public interface ILogger
    {
        void Log(LogLevel logLevel, string message);

        void Log(LogLevel logLevel, string message, Exception exception);
    }
}
