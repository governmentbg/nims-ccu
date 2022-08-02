using Eumis.Common.Auth;
using Eumis.Common.Log;
using Eumis.Domain.ActionLogs;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Log.NLog;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Log.ActionLogger
{
    internal class ActionLogger : IActionLogger
    {
        private IActionLogsAddRepository actionLogsAddRepository;
        private ILogger logger;
        private IOwinContext owinContext;
        private IAccessContext accessContext;

        public ActionLogger(
            IActionLogsAddRepository actionLogsAddRepository,
            ILogger logger,
            IOwinContext owinContext,
            IAccessContext accessContext)
        {
            this.actionLogsAddRepository = actionLogsAddRepository;
            this.logger = logger;
            this.owinContext = owinContext;
            this.accessContext = accessContext;
        }

        public void LogSuccessfulLoginAction(string username)
        {
            this.LogActionInternal(ActionLogType.Internal, ActionLogGroupUtils.GetClassDescriptionKey(typeof(ActionLogGroups.Login.Successful)), null, null, username, null, null, null, null, null);
        }

        public void LogUnsuccessfulLoginAction(string username)
        {
            this.LogActionInternal(ActionLogType.UnsuccessfulLogin, string.Empty, null, null, null, null, null, null, username, null);
        }

        public void LogAction(Type action, int? aggregateRootId, int? childRootId, object postData, object responseData)
        {
            var actionLogGroupDescriptor = ActionLogGroupUtils.GetClassDescriptor(action);

            this.LogAction(
                actionLogGroupDescriptor.ActionLogType,
                actionLogGroupDescriptor.Key,
                aggregateRootId,
                childRootId,
                postData,
                responseData);
        }

        public async Task LogActionAsync(Type action, int? aggregateRootId, int? childRootId, object postData, object responseData, CancellationToken ct = default(CancellationToken))
        {
            var actionLogGroupDescriptor = ActionLogGroupUtils.GetClassDescriptor(action);

            await this.LogActionAsync(
                actionLogGroupDescriptor.ActionLogType,
                actionLogGroupDescriptor.Key,
                aggregateRootId,
                childRootId,
                postData,
                responseData,
                ct);
        }

        public void LogAction(ActionLogType actionLogType, string action, int? aggregateRootId, int? childRootId, object postData, object responseData)
        {
            string postJson = postData != null ? JsonConvert.SerializeObject(postData) : null;
            string responseJson = responseData != null ? JsonConvert.SerializeObject(responseData) : null;

            this.LogActionInternal(
                actionLogType,
                action,
                aggregateRootId,
                childRootId,
                this.accessContext.IsUser ? this.accessContext.Username : null,
                this.accessContext.IsRegistration ? this.accessContext.RegistrationEmail : null,
                this.accessContext.IsContractRegistration ? this.accessContext.ContractRegistrationEmail : null,
                this.accessContext.IsContractAccessCode ? this.accessContext.ContractAccessCodeEmail : null,
                postJson,
                responseJson);
        }

        public async Task LogActionAsync(ActionLogType actionLogType, string action, int? aggregateRootId, int? childRootId, object postData, object responseData, CancellationToken ct = default(CancellationToken))
        {
            string postJson = postData != null ? JsonConvert.SerializeObject(postData) : null;
            string responseJson = responseData != null ? JsonConvert.SerializeObject(responseData) : null;

            await this.LogActionInternalAsync(
                actionLogType,
                action,
                aggregateRootId,
                childRootId,
                this.accessContext.IsUser ? this.accessContext.Username : null,
                this.accessContext.IsRegistration ? this.accessContext.RegistrationEmail : null,
                this.accessContext.IsContractRegistration ? this.accessContext.ContractRegistrationEmail : null,
                this.accessContext.IsContractAccessCode ? this.accessContext.ContractAccessCodeEmail : null,
                postJson,
                responseJson,
                ct);
        }

        private void LogActionInternal(
            ActionLogType actionLogType,
            string action,
            int? aggregateRootId,
            int? childRootId,
            string username,
            string registrationEmail,
            string contractRegistrationEmail,
            string contractAccessCodeEmail,
            string postData,
            string responseData)
        {
            string rawUrl = this.owinContext.Request.Uri.PathAndQuery;
            NLogLogger nLogLogger = (NLogLogger)this.logger;
            Guid requestId = nLogLogger.RequestId;
            string remoteIpAddress = nLogLogger.GetRemoteAddress();

            ActionLog actionLog = new ActionLog(
                actionLogType,
                action,
                aggregateRootId,
                childRootId,
                username,
                registrationEmail,
                contractRegistrationEmail,
                contractAccessCodeEmail,
                postData,
                responseData,
                rawUrl,
                requestId,
                remoteIpAddress);

            this.actionLogsAddRepository.InsertInDb(actionLog);
        }

        private async Task LogActionInternalAsync(
            ActionLogType actionLogType,
            string action,
            int? aggregateRootId,
            int? childRootId,
            string username,
            string registrationEmail,
            string contractRegistrationEmail,
            string contractAccessCodeEmail,
            string postData,
            string responseData,
            CancellationToken ct = default(CancellationToken))
        {
            string rawUrl = this.owinContext.Request.Uri.PathAndQuery;
            NLogLogger nLogLogger = (NLogLogger)this.logger;
            Guid requestId = nLogLogger.RequestId;
            string remoteIpAddress = nLogLogger.GetRemoteAddress();

            ActionLog actionLog = new ActionLog(
                actionLogType,
                action,
                aggregateRootId,
                childRootId,
                username,
                registrationEmail,
                contractRegistrationEmail,
                contractAccessCodeEmail,
                postData,
                responseData,
                rawUrl,
                requestId,
                remoteIpAddress);

            await this.actionLogsAddRepository.InsertInDbAsync(actionLog, ct);
        }
    }
}
