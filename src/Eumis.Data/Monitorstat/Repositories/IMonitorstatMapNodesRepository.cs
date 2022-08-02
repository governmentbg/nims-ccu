using Eumis.Data.Monitorstat.Contracts;
using Eumis.Data.Monitorstat.ViewObjects;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Monitorstat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Monitorstat.Repositories
{
    public interface IMonitorstatMapNodesRepository : IAggregateRepository<MonitorstatMapNode>
    {
        bool MapNodeHasMapping(int mapNodeId, MonitorstatMapNodeType type);

        ProgrammeDO GetProgrammeRequest(int programmeId);

        ProgrammePriorityDO GetProgrammePriorityRequest(int programmePriorityId, Guid programmeGid);

        ProcedureDO GetProcedureRequest(int procedureId, int programmePriorityId, DateTime? listingDate);

        Guid GetOperationalProgrammeGid(int mapNodeId);
    }
}
