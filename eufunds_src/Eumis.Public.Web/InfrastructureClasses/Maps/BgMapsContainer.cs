using System.Collections.Generic;
using System.Linq;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Domain.Entities;
using Eumis.Public.Web.Models.Maps;

namespace Eumis.Public.Web.InfrastructureClasses.Maps
{
    public class BgMapsContainer : IMapsContainer
    {
        private Dictionary<string, MapModel> maps;

        public BgMapsContainer(IMapsRepository mapRepo)
        {
            this.MapRepo = mapRepo;
            this.ReadMaps();
        }

        public IMapsRepository MapRepo { get; set; }

        public MapModel GetMap(string id)
        {
            return this.maps[id];
        }

        public IReadOnlyList<MapModel> GetAll()
        {
            return this.maps.Values.ToList().AsReadOnly();
        }

        private void ReadMaps()
        {
            IEnumerable<Map> maps = this.MapRepo.GetAllMapsByType(MapTypeEnum.BgNuts);
            IEnumerable<MapRegion> regions = this.MapRepo.GetAllMapRegionsByType(MapTypeEnum.BgNuts);

            this.maps = new Dictionary<string, MapModel>();
            foreach (Map m in maps)
            {
                this.maps.Add(m.Id.ToString(), new MapModel(
                    m.Id,
                    regions.Where(reg => reg.MapId == m.Id).Select(
                        reg => new MapRegionModel(reg.Id, reg.Path))));
            }
        }
    }
}