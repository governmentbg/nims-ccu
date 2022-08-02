using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    internal class IrregularityNomsRepository : Repository, IIrregularityNomsRepository
    {
        public IrregularityNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from i in this.unitOfWork.DbContext.Set<Irregularity>()
                    where i.IrregularityId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = i.IrregularityId,
                        Name = "Нередност " + i.RegNumber + (i.Status == IrregularityStatus.Removed ? "(Анулирана)" : string.Empty),
                    })
                    .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<EntityNomVO> GetNoms(int contractId, string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<Irregularity>()
                .AndStringContains(i => "Нередност " + i.RegNumber, term);

            return (from i in this.unitOfWork.DbContext.Set<Irregularity>().Where(predicate)
                    where i.ContractId == contractId && i.Status == IrregularityStatus.Entered
                    select new EntityNomVO
                    {
                        NomValueId = i.IrregularityId,
                        Name = "Нередност " + i.RegNumber,
                    })
                    .OrderBy(p => p.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
