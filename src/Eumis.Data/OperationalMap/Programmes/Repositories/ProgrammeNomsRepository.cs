using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.OperationalMap.Programmes.Repositories
{
    internal class ProgrammeNomsRepository : Repository, IProgrammeNomsRepository
    {
        public ProgrammeNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Programme>()
                    where p.MapNodeId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = p.MapNodeId,
                        Name = p.Code + " " + p.Name,
                    })
                    .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return this.GetProgrammeNoms(term, offset, limit);
        }

        public IEnumerable<EntityNomVO> GetProgrammeNoms(string term, int offset = 0, int? limit = null, int? procedureId = null, int[] programmeIds = null)
        {
            var predicate =
                PredicateBuilder.True<Programme>()
                .AndStringContains(p => p.Code + " " + p.Name, term);

            if (programmeIds != null)
            {
                predicate = predicate.And(pr => programmeIds.Contains(pr.MapNodeId));
            }

            var programmes = from p in this.unitOfWork.DbContext.Set<Programme>().Where(predicate)
                             select p;

            if (procedureId.HasValue)
            {
                programmes = from p in programmes
                             join pp in this.unitOfWork.DbContext.Set<ProcedureProgramme>() on p.MapNodeId equals pp.ProgrammeId
                             where pp.ProcedureId == procedureId.Value
                             select p;
            }

            return (from p in programmes
                    select new EntityNomVO
                    {
                        NomValueId = p.MapNodeId,
                        Name = p.Code + " " + p.Name,
                    })
                    .OrderBy(p => p.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
