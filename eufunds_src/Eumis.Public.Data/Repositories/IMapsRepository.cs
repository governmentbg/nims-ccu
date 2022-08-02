using Eumis.Public.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Data.Repositories
{
    public interface IMapsRepository
    {
        Map GetMapById(int mapId);

        IEnumerable<Map> GetAllMapsByType(MapTypeEnum type);

        IEnumerable<MapRegion> GetAllMapRegionsByType(MapTypeEnum type);

        IQueryable<MapRegion> GetAllMapRegions();
    }
}
