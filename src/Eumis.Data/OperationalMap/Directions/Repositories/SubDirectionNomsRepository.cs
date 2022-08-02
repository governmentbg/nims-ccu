using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.OperationalMap.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.OperationalMap.Directions.Repositories
{
    internal class SubDirectionNomsRepository : EntityNomsRepository<SubDirection, EntityNomVO>, ISubDirectionNomsRepository
    {
        public SubDirectionNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.SubDirectionId,
                q => q.Name,
                q => q.NameAlt,
                q => new EntityNomVO
                {
                    NomValueId = q.SubDirectionId,
                    Name = q.Name,
                    NameAlt = q.NameAlt,
                })
        {
        }

        public override IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return new List<EntityNomVO>();
        }

        public IEnumerable<EntityNomVO> GetNoms(int directionId, string term, int offset = 0, int? limit = null)
        {
            return (from sd in this.unitOfWork.DbContext.Set<SubDirection>()
                    where sd.DirectionId == directionId
                    select new EntityNomVO
                    {
                        NomValueId = sd.SubDirectionId,
                        Name = sd.Name,
                        NameAlt = sd.NameAlt,
                    })
                    .ToList()
                    .Where(t => string.IsNullOrEmpty(term) ? true : t.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
