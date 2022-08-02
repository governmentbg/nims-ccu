using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.OperationalMap.Directions.Repositories
{
    public interface ISubDirectionNomsRepository : IEntityNomsRepository<SubDirection, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetNoms(int directionId, string term, int offset = 0, int? limit = null);
    }
}
