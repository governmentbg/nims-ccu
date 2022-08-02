using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Domain.ActionLogs
{
    public interface IActionLogsAddRepository
    {
        void InsertInDb(ActionLog actionLog);

        Task InsertInDbAsync(ActionLog actionLog, CancellationToken ct);
    }
}
