using Eumis.Data.Monitorstat.Contracts;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Monitorstat;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.Repositories
{
    public interface IProcedureMonitorstatDocumentsRepository : IAggregateRepository<ProcedureMonitorstatDocument>
    {
        void CreateProcedureDcuments(int procedureId, int[] reportIds);

        IList<ProcedureMonitorstatDocumentVO> GetProcedureDocuments(int procedureId);

        IList<ProcedureMonitorstatDocumentVO> GetUnattachedReports(int procedureId, MonitorstatYear? year, int? surveyId);

        IList<ReportDO> GetProcedureInquiryReports(int procedureId);
    }
}
