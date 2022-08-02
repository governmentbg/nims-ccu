using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Web.Models.Maps
{
    public class MapModel : IMarkupMapModel
    {
        public MapModel(int id, IEnumerable<MapRegionModel> regions)
        {
            this.Id = id;
            this.MapData = regions.ToList().AsReadOnly();
        }

        public int Id { get; private set; }

        public IReadOnlyList<MapRegionModel> MapData { get; private set; }
    }
}