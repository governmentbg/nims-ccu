using System.Collections.Generic;

namespace Eumis.Public.Web.Models.Maps
{
    public class MapDataModel : IMarkupMapModel
    {
        public MapDataModel(int id, string name, string nameAlt, List<MapRegionDataModel> regions)
        {
            this.Id = id;
            this.Name = name;
            this.NameAlt = nameAlt;
            this.Regions = regions;
        }

        public int Id { get; set; }

        public string Name { get; private set; }

        public string NameAlt { get; private set; }

        public List<MapRegionDataModel> Regions { get; set; }
    }
}