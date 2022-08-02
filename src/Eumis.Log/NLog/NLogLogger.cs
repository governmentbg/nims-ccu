using Autofac;
using Autofac.Features.Indexed;
using Autofac.Integration.Owin;
using Eumis.Common.Auth;
using Eumis.Common.Log;
using Eumis.Log.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Eumis.Log.NLog
{
    public class NLogLogger : ILogger
    {
        public static readonly string PortalIntegrationLoggerName = "Eumis.PortalIntegration.Host";
        public static readonly string IntegrationLoggerName = "Eumis.Integration.Host";
        private static readonly global::NLog.Logger InfoLogger = global::NLog.LogManager.GetLogger("infoLogger");
        private static readonly global::NLog.Logger RequestLogger = global::NLog.LogManager.GetLogger("requestLogger");
        private static readonly global::NLog.Logger ResponseLogger = global::NLog.LogManager.GetLogger("responseLogger");

        private IDictionary<string, Func<IOwinContext, string>> customProperties;
        private IOwinContext owinContext;
        private IAccessContext accessContext;

        public NLogLogger(string appName)
        {
            this.AppName = appName;
        }

        public NLogLogger(string appName, IOwinContext owinContext, IDictionary<string, Func<IOwinContext, string>> customProperties)
        {
            this.AppName = appName;
            this.RequestId = Guid.NewGuid();
            this.RequestDate = DateTime.Now;

            this.owinContext = owinContext;
            this.customProperties = customProperties ?? new Dictionary<string, Func<IOwinContext, string>>();
            var accessContexts = owinContext.GetAutofacLifetimeScope().Resolve<IIndex<string, IAccessContext>>();

            if (accessContexts.TryGetValue(AuthenticationTypes.Cookie, out IAccessContext ac) && ac.IsAuthenticated)
            {
                this.accessContext = ac;
            }
            else if (accessContexts.TryGetValue(AuthenticationTypes.Bearer, out ac) && ac.IsAuthenticated)
            {
                this.accessContext = ac;
            }
        }

        internal string AppName { get; set; }

        internal Guid RequestId { get; set; }

        internal DateTime RequestDate { get; set; }

        internal void LogRequest(string requestBody)
        {
            if (string.IsNullOrEmpty(requestBody))
            {
                return;
            }

            if (this.owinContext == null)
            {
                throw new InvalidOperationException("The logger is not associated with a request.");
            }

            var logEvent = new global::NLog.LogEventInfo
            {
                TimeStamp = this.RequestDate,
                Level = global::NLog.LogLevel.Info,
                Message = requestBody,
            };

            logEvent.Properties["RequestId"] = this.RequestId.ToString();

            RequestLogger.Log(logEvent);
        }

        internal void LogResponse(string responseBody)
        {
            if (string.IsNullOrEmpty(responseBody))
            {
                return;
            }

            if (this.owinContext == null)
            {
                throw new InvalidOperationException("The logger is not associated with a request.");
            }

            var logEvent = new global::NLog.LogEventInfo
            {
                TimeStamp = this.RequestDate,
                Level = global::NLog.LogLevel.Info,
                Message = responseBody,
            };

            logEvent.Properties["RequestId"] = this.RequestId.ToString();

            ResponseLogger.Log(logEvent);
        }

        public void Log(LogLevel logLevel, string message)
        {
            this.Log(logLevel, message, null);
        }

        public void Log(LogLevel logLevel, string message, Exception exception)
        {
            InfoLogger.Log(this.CreateLogEventInfo(logLevel, message, exception));
        }

        // this method should be thread safe and should not modify the logger!!!
        private global::NLog.LogEventInfo CreateLogEventInfo(
            LogLevel level,
            string message,
            Exception exception)
        {
            global::NLog.LogLevel nlogLevel;
            switch (level)
            {
                case LogLevel.Off:
                    nlogLevel = global::NLog.LogLevel.Off;
                    break;
                case LogLevel.Fatal:
                    nlogLevel = global::NLog.LogLevel.Fatal;
                    break;
                case LogLevel.Error:
                    nlogLevel = global::NLog.LogLevel.Error;
                    break;
                case LogLevel.Warn:
                    nlogLevel = global::NLog.LogLevel.Warn;
                    break;
                case LogLevel.Info:
                    nlogLevel = global::NLog.LogLevel.Info;
                    break;
                case LogLevel.Debug:
                    nlogLevel = global::NLog.LogLevel.Debug;
                    break;
                case LogLevel.Trace:
                    nlogLevel = global::NLog.LogLevel.Trace;
                    break;
                default:
                    throw new Exception("Unknown log level");
            }

            var logEvent = new global::NLog.LogEventInfo
            {
                TimeStamp = this.owinContext != null ? this.RequestDate : DateTime.Now,
                Level = nlogLevel,
                Message = message,
                Exception = exception,
            };
            logEvent.Properties["AppName"] = this.AppName;

            if (this.owinContext != null)
            {
                logEvent.Properties["RemoteIpAddress"] = this.GetRemoteAddress();
                logEvent.Properties["Method"] = this.owinContext.Request.Method;
                logEvent.Properties["PathAndQuery"] = this.owinContext.Request.Uri.PathAndQuery;
                logEvent.Properties["UserAgent"] = this.owinContext.Request.Headers["User-Agent"];
                logEvent.Properties["RequestId"] = this.RequestId.ToString();
                logEvent.Properties["UserId"] = this.accessContext != null && this.accessContext.IsUser ? this.accessContext.UserId : (int?)null;
                logEvent.Properties["RegistrationId"] = this.accessContext != null && this.accessContext.IsRegistration ? this.accessContext.RegistrationId : (int?)null;
                logEvent.Properties["ContractRegistrationId"] = this.accessContext != null && this.accessContext.IsContractRegistration ? this.accessContext.ContractRegistrationId : (int?)null;
                logEvent.Properties["ContractAccessCodeId"] = this.accessContext != null && this.accessContext.IsContractAccessCode ? this.accessContext.ContractAccessCodeId : (int?)null;

                double? elapsedMilliseconds = null;
                long? startTicks = this.owinContext.Get<long?>(LoggingMiddleware.TimerTicksAtEntryOwinEnvKey);
                if (startTicks.HasValue)
                {
                    double millisecondsPerTick = 1000d / Stopwatch.Frequency;
                    elapsedMilliseconds = (long)Math.Floor((Stopwatch.GetTimestamp() - startTicks.Value) * millisecondsPerTick);
                }

                logEvent.Properties["ElapsedMilliseconds"] = elapsedMilliseconds;

                string status;
                if (this.owinContext.Request.CallCancelled.IsCancellationRequested)
                {
                    status = "Cancelled";
                }
                else
                {
                    status = string.Format("{0}: {1}", this.owinContext.Response.StatusCode, this.owinContext.Response.ReasonPhrase);
                }

                logEvent.Properties["Status"] = status;

                foreach (var prop in this.customProperties)
                {
                    logEvent.Properties[prop.Key] = prop.Value(this.owinContext);
                }
            }

            return logEvent;
        }

        internal string GetRemoteAddress()
        {
            IOwinRequest request = this.owinContext.Request;
            if (this.AppName == NLogLogger.PortalIntegrationLoggerName && request.Headers.ContainsKey("X-Original-Client-IP"))
            {
                return this.owinContext.Request.Headers["X-Original-Client-IP"];
            }

#if !REVERSE_PROXY_MODE
            return request.RemoteIpAddress;
#else
            string ipAddress = request.Headers["X-Forwarded-For"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                return request.RemoteIpAddress;
            }

            return ipAddress;
#endif
        }
    }
}
