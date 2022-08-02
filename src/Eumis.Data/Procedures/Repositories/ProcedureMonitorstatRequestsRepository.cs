using Eumis.Common.Db;
using Eumis.Data.Linq;
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
    internal class ProcedureMonitorstatRequestsRepository : AggregateRepository<ProcedureMonitorstatRequest>, IProcedureMonitorstatRequestsRepository
    {
        public ProcedureMonitorstatRequestsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ProcedureMonitorstatRequestsVO> GetProcedureRequests(int procedureId)
        {
            return this.Set()
                .Where(x => x.ProcedureId == procedureId)
                .Select(x => new ProcedureMonitorstatRequestsVO
                {
                    ProcedureMonitorstatRequestId = x.ProcedureMonitorstatRequestId,
                    Status = x.Status,
                    ProcedureId = x.ProcedureId,
                    ExecutionStartDate = x.ExecutionStartDate,
                    ExecutionEndDate = x.ExecutionEndDate,
                    Version = x.Version,
                })
                .ToList();
        }

        public bool ProcedureHasMonitorstatRequests(int procedureId)
        {
            return this.Set()
                .Any(x => x.ProcedureId == procedureId && x.Status == ProcedureMonitorstatRequestStatus.Sent);
        }
     }
}
