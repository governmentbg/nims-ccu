using System.Collections.Generic;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;

namespace Eumis.Data.EvalSessions.Repositories
{
    public interface IEvalSessionReportsRepository : IAggregateRepository<EvalSessionReport>
    {
        IList<EvalSessionReportVO> GetEvalSessionReports(int evalSessionId);
    }
}
