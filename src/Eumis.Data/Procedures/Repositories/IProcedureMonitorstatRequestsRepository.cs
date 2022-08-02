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
    public interface IProcedureMonitorstatRequestsRepository : IAggregateRepository<ProcedureMonitorstatRequest>
    {
        IList<ProcedureMonitorstatRequestsVO> GetProcedureRequests(int procedureId);

        bool ProcedureHasMonitorstatRequests(int procedureId);
    }
}
