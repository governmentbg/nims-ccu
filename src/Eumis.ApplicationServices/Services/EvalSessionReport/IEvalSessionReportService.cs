using System.Collections.Generic;
using Eumis.Domain.EvalSessions;

namespace Eumis.ApplicationServices.Services.EvalSessionReport
{
    public interface IEvalSessionReportService
    {
        IList<string> CanDelete(int evalSessionReportId);

        IList<string> CanCreate(int evalSessionId);

        Eumis.Domain.EvalSessions.EvalSessionReport CreateReport(
            int evalSessionId,
            EvalSessionReportType type,
            string description);
    }
}
