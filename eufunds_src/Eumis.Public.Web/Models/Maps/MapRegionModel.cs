namespace Eumis.Public.Web.Models.Maps
{
    public class MapRegionModel
    {
        public MapRegionModel(int id, string path)
        {
            this.RegionId = id;
            this.Path = path;
        }

        public int RegionId { get; private set; }

        public string Path { get; private set; }
    }
}