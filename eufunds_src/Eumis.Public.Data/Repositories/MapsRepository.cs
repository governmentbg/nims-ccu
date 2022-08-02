using Eumis.Public.Data.Core;
using Eumis.Public.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Data.Repositories
{
    internal class MapsRepository : Repository, IMapsRepository
    {
        public MapsRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        public Map GetMapById(int mapId)
        {
            return this.unitOfWork.DbContext.Set<Map>()
                .Where(m => m.Id == mapId)
                .SingleOrDefault();
        }

        public IEnumerable<Map> GetAllMapsByType(MapTypeEnum type)
        {
            return this.unitOfWork.DbContext.Set<Map>().Where(m => m.Type == (byte)type);
        }

        public IEnumerable<MapRegion> GetAllMapRegionsByType(MapTypeEnum type)
        {
            IEnumerable<int> maps = this.unitOfWork.DbContext.Set<Map>().Where(m => m.Type == (byte)type).Select(m => m.Id);
            return this.unitOfWork.DbContext.Set<MapRegion>().Where(reg => maps.Contains(reg.MapId));
        }

        public IQueryable<MapRegion> GetAllMapRegions()
        {
            return this.unitOfWork.DbContext.Set<MapRegion>()
                .AsQueryable();
        }
    }
}
