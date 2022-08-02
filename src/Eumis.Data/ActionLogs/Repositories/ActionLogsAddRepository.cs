using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Domain.ActionLogs;

namespace Eumis.Data.ActionLogs.Repositories
{
    internal class ActionLogsAddRepository : Repository, IActionLogsAddRepository
    {
        public ActionLogsAddRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void InsertInDb(ActionLog actionLog)
        {
            var insertSql =
                @"INSERT INTO [ActionLogs] (
                    [ActionLogType],
                    [Action],
                    [AggregateRootId],
                    [ChildRootId],
                    [Username],
                    [RegistrationEmail],
                    [ContractRegistrationEmail],
                    [ContractAccessCodeEmail],
                    [PostData],
                    [ResponseData],
                    [RawUrl],
                    [RequestId],
                    [RemoteIpAddress],
                    [LogDate]
                ) VALUES (
                    @actionLogType,
                    @action,
                    @aggregateRootId,
                    @childRootId,
                    @username,
                    @registrationEmail,
                    @contractRegistrationEmail,
                    @contractAccessCodeEmail,
                    @postData,
                    @responseData,
                    @rawUrl,
                    @requestId,
                    @remoteIpAddress,
                    @logDate
                );";

            this.ExecuteSqlCommand(
                insertSql,
                new SqlParameter("@actionLogType", (int)actionLog.ActionLogType),
                new SqlParameter("@action", (object)actionLog.Action ?? DBNull.Value),
                new SqlParameter("@aggregateRootId", (object)actionLog.AggregateRootId ?? DBNull.Value),
                new SqlParameter("@childRootId", (object)actionLog.ChildRootId ?? DBNull.Value),
                new SqlParameter("@username", (object)actionLog.Username ?? DBNull.Value),
                new SqlParameter("@registrationEmail", (object)actionLog.RegistrationEmail ?? DBNull.Value),
                new SqlParameter("@contractRegistrationEmail", (object)actionLog.ContractRegistrationEmail ?? DBNull.Value),
                new SqlParameter("@contractAccessCodeEmail", (object)actionLog.ContractAccessCodeEmail ?? DBNull.Value),
                new SqlParameter("@postData", (object)actionLog.PostData ?? DBNull.Value),
                new SqlParameter("@responseData", (object)actionLog.ResponseData ?? DBNull.Value),
                new SqlParameter("@rawUrl", (object)actionLog.RawUrl ?? DBNull.Value),
                new SqlParameter("@requestId", (object)actionLog.RequestId ?? DBNull.Value),
                new SqlParameter("@remoteIpAddress", (object)actionLog.RemoteIpAddress ?? DBNull.Value),
                new SqlParameter("@logDate", (object)actionLog.LogDate ?? DBNull.Value));
        }

        public async Task InsertInDbAsync(ActionLog actionLog, CancellationToken ct = default(CancellationToken))
        {
            var insertSql =
                @"INSERT INTO [ActionLogs] (
                    [ActionLogType],
                    [Action],
                    [AggregateRootId],
                    [ChildRootId],
                    [Username],
                    [RegistrationEmail],
                    [ContractRegistrationEmail],
                    [ContractAccessCodeEmail],
                    [PostData],
                    [ResponseData],
                    [RawUrl],
                    [RequestId],
                    [RemoteIpAddress],
                    [LogDate]
                ) VALUES (
                    @actionLogType,
                    @action,
                    @aggregateRootId,
                    @childRootId,
                    @username,
                    @registrationEmail,
                    @contractRegistrationEmail,
                    @contractAccessCodeEmail,
                    @postData,
                    @responseData,
                    @rawUrl,
                    @requestId,
                    @remoteIpAddress,
                    @logDate
                );";

            await this.ExecuteSqlCommandAsync(
                insertSql,
                ct,
                new SqlParameter("@actionLogType", (int)actionLog.ActionLogType),
                new SqlParameter("@action", (object)actionLog.Action ?? DBNull.Value),
                new SqlParameter("@aggregateRootId", (object)actionLog.AggregateRootId ?? DBNull.Value),
                new SqlParameter("@childRootId", (object)actionLog.ChildRootId ?? DBNull.Value),
                new SqlParameter("@username", (object)actionLog.Username ?? DBNull.Value),
                new SqlParameter("@registrationEmail", (object)actionLog.RegistrationEmail ?? DBNull.Value),
                new SqlParameter("@contractRegistrationEmail", (object)actionLog.ContractRegistrationEmail ?? DBNull.Value),
                new SqlParameter("@contractAccessCodeEmail", (object)actionLog.ContractAccessCodeEmail ?? DBNull.Value),
                new SqlParameter("@postData", (object)actionLog.PostData ?? DBNull.Value),
                new SqlParameter("@responseData", (object)actionLog.ResponseData ?? DBNull.Value),
                new SqlParameter("@rawUrl", (object)actionLog.RawUrl ?? DBNull.Value),
                new SqlParameter("@requestId", (object)actionLog.RequestId ?? DBNull.Value),
                new SqlParameter("@remoteIpAddress", (object)actionLog.RemoteIpAddress ?? DBNull.Value),
                new SqlParameter("@logDate", (object)actionLog.LogDate ?? DBNull.Value));
        }
    }
}
