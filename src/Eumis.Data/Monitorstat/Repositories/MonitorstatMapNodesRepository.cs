using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.Monitorstat.Contracts;
using Eumis.Data.Monitorstat.ViewObjects;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Monitorstat;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Monitorstat.Repositories
{
    internal class MonitorstatMapNodesRepository : AggregateRepository<MonitorstatMapNode>, IMonitorstatMapNodesRepository
    {
        public MonitorstatMapNodesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool MapNodeHasMapping(int mapNodeId, MonitorstatMapNodeType type)
        {
            return this.unitOfWork.DbContext.Set<MonitorstatMapNode>()
                .Where(x => x.MapNodeId == mapNodeId && x.Type == type)
                .Any();
        }

        public ProgrammeDO GetProgrammeRequest(int programmeId)
        {
            return this.unitOfWork.DbContext.Set<MapNode>()
                .Where(x => x.MapNodeId == programmeId)
                .Select(x => new ProgrammeDO
                {
                    Code = x.Code,
                    Name = x.Name,
                    Abbreviation = x.ShortName,
                    Description = x.Name,
                })
                .Single();
        }

        public ProgrammePriorityDO GetProgrammePriorityRequest(int programmePriorityId, Guid programmeGid)
        {
            return (from mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>().Where(x => x.ProgrammePriorityId == programmePriorityId)
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on mnr.MapNodeId equals pp.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    select new ProgrammePriorityDO
                    {
                        Code = pp.Code,
                        Name = pp.Name,
                        ProgrammeCode = p.Code,
                        ProgrammeIdentifier = programmeGid,
                        Definition = pp.Description,
                    }).Single();
        }

        public ProcedureDO GetProcedureRequest(int procedureId, int programmePriorityId, DateTime? listingDate)
        {
            var parentData = this.GetProgrammePriorityRequest(programmePriorityId, Guid.Empty);

            return this.unitOfWork.DbContext.Set<Procedure>()
                .Where(x => x.ProcedureId == procedureId)
                .Select(x => new ProcedureDO
                {
                    StartDate = listingDate.Value,
                    Code = x.Code,
                    Name = x.Name,
                    ProgrammeCode = parentData.ProgrammeCode,
                    ProgrammePriorityCode = parentData.Code,
                })
                .Single();
        }

        public Guid GetOperationalProgrammeGid(int mapNodeId)
        {
            return this.unitOfWork.DbContext.Set<MonitorstatMapNode>()
                .Where(x => x.MapNodeId == mapNodeId && x.Type == MonitorstatMapNodeType.Programme)
                .Select(x => x.MonitorstatGid)
                .FirstOrDefault();
        }
    }
}
