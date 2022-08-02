using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.OperationalMap.Directions.Repositories
{
    internal class DirectionNomsRepository : EntityNomsRepository<Direction, EntityNomVO>
    {
        public DirectionNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.DirectionId,
                q => q.Name,
                q => q.NameAlt,
                q => new EntityNomVO
                {
                    NomValueId = q.DirectionId,
                    Name = q.Name,
                    NameAlt = q.NameAlt,
                })
        {
        }
    }
}
