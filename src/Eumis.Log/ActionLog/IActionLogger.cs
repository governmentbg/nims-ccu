using System;
using System.Threading;
using System.Threading.Tasks;
using Eumis.Domain.ActionLogs;

namespace Eumis.Log.ActionLogger
{
    public interface IActionLogger
    {
        void LogSuccessfulLoginAction(string username);

        void LogUnsuccessfulLoginAction(string username);

        void LogAction(Type action, int? aggregateRootId, int? childRootId, object postData, object responseData);

        Task LogActionAsync(Type action, int? aggregateRootId, int? childRootId, object postData, object responseData, CancellationToken ct = default(CancellationToken));

        void LogAction(ActionLogType actionLogType, string action, int? aggregateRootId, int? childRootId, object postData, object responseData);

        Task LogActionAsync(ActionLogType actionLogType, string action, int? aggregateRootId, int? childRootId, object postData, object responseData, CancellationToken ct = default(CancellationToken));
    }
}
