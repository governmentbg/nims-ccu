using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.MapNodes;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class MapNodeNomsRepository : EntityNomsRepository<MapNode, EntityNomVO>
    {
        public MapNodeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.MapNodeId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.MapNodeId,
                    Name = t.Code + " " + t.Name,
                })
        {
        }
    }
}
