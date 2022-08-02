using NLog;

namespace Eumis.Public.Common.NLog
{
    public class NLogLogger : ILogger
    {
        private readonly Logger logger = LogManager.GetLogger("database");

        public void Trace(string message)
        {
            this.logger.Trace(message);
        }

        public void Debug(string message)
        {
            this.logger.Debug(message);
        }

        public void Info(string message)
        {
            this.logger.Info(message);
        }

        public void Warn(string message)
        {
            this.logger.Warn(message);
        }

        public void Error(string message)
        {
            this.logger.Error(message);
        }

        public void Fatal(string message)
        {
            this.logger.Fatal(message);
        }
    }
}