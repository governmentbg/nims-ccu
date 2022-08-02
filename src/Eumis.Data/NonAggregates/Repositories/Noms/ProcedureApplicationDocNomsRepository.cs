using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Procedures;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ProcedureApplicationDocNomsRepository : EntityNomsRepository<ProcedureApplicationDoc, EntityNomVO>, IProcedureApplicationDocNomsRepository
    {
        public ProcedureApplicationDocNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ProcedureApplicationDocId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.ProcedureApplicationDocId,
                    Name = t.Name,
                })
        {
        }

        public IList<EntityNomVO> GetProcedureApplicationDocNoms(
            int procedureId,
            string term,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<ProcedureApplicationDoc>()
                .AndEquals(pad => pad.ProcedureId, procedureId)
                .AndEquals(pad => pad.IsActivated, true)
                .AndEquals(pad => pad.IsActive, true);

            return this.unitOfWork.DbContext.Set<ProcedureApplicationDoc>()
                .Where(predicate)
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
