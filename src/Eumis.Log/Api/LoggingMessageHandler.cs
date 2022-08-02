using Eumis.Common.Config;
using Eumis.Common.Log;
using Eumis.Log.NLog;
using Eumis.Log.Owin;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Log.Api
{
    public class LoggingMessageHandler : DelegatingHandler
    {
        public static readonly bool EnableRequestLogging = bool.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Log:EnableRequestLogging"));

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var owinContext = request.GetOwinContext();
            var logger = (NLogLogger)owinContext.Get<ILogger>(LoggingMiddleware.LoggerOwinEnvKey);

            string requestBody = await request.Content.ReadAsStringAsync();

            var result = await base.SendAsync(request, cancellationToken);

            // do the logging after the other request handlers have executed
            // and possible set the ContainsSensitiveData flag of the owin context
            if (!owinContext.ContainsSensitiveData())
            {
                logger.LogRequest(requestBody);

                if (EnableRequestLogging && result.Content != null)
                {
                    string responseBody = await result.Content.ReadAsStringAsync();
                    logger.LogResponse(responseBody);
                }
            }

            return result;
        }
    }
}
