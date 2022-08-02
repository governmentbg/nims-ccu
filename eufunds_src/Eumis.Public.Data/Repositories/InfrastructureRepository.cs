using Autofac.Extras.Attributed;
using Eumis.Public.Data.Core;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.InvestmentPriorities;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammePriorities;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.SpecificTargets;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Data.Repositories
{
    internal class InfrastructureRepository : Repository, IInfrastructureRepository
    {
        public InfrastructureRepository([WithKey(DbKey.Umis)]IUnitOfWork uow, IMapsRepository maps)
            : base(uow)
        {
            this.MapRepo = maps;
        }

        private IMapsRepository MapRepo { get; set; }

        public IEnumerable<Programme> GetAllOps()
        {
            return this.unitOfWork.DbContext.Set<Programme>();
        }

        public ICollection<Programme> GetEfmdrProgrammes()
        {
            return (from p in this.unitOfWork.DbContext.Set<Programme>()
                    join mnfs in this.unitOfWork.DbContext.Set<MapNodeFinanceSource>() on p.MapNodeId equals mnfs.MapNodeId
                    where mnfs.FinanceSource == FinanceSource.EFMDR
                    select p).ToList();
        }

        public IEnumerable<MapNode> GetPriorityAxisForOp(int opId)
        {
            IQueryable<int> opRefs = this.unitOfWork.DbContext.Set<MapNodeRelation>().Where(r => r.ProgrammeId == opId).Select(r => r.MapNodeId);
            return this.unitOfWork.DbContext.Set<ProgrammePriority>()
                .Where(mn => opRefs.Contains(mn.MapNodeId));
        }

        public IEnumerable<MapNode> GetInvestmentPrioritiesForAxis(int axisId)
        {
            IQueryable<int> opRefs = this.unitOfWork.DbContext.Set<MapNodeRelation>().Where(r => r.ProgrammePriorityId == axisId).Select(r => r.MapNodeId);
            return this.unitOfWork.DbContext.Set<InvestmentPriority>()
                .Where(mn => opRefs.Contains(mn.MapNodeId));
        }

        public IEnumerable<MapNode> GetSpecificTargetForInvestment(int investmentId)
        {
            IQueryable<int> opRefs = this.unitOfWork.DbContext.Set<MapNodeRelation>().Where(r => r.ParentMapNodeId == investmentId).Select(r => r.MapNodeId);
            return this.unitOfWork.DbContext.Set<SpecificTarget>()
                .Where(mn => opRefs.Contains(mn.MapNodeId));
        }
    }
}
