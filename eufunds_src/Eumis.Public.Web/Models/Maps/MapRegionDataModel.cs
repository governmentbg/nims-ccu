namespace Eumis.Public.Web.Models.Maps
{
    public class MapRegionDataModel
    {
        public MapRegionDataModel(int id, string name, string nameAlt)
        {
            this.RegionId = id;
            this.Name = name;
            this.NameAlt = nameAlt;
        }

        public int RegionId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public int Drilldown { get; set; }
    }
}