using System;
using System.IO;
using System.Web.Mvc;
using Eumis.Public.Web.InfrastructureClasses.Maps;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Eumis.Public.Web.Controllers
{
    /// <summary>
    /// Provides map and data async info.
    /// </summary>
    public partial class BgMapsController : Controller
    {
        public BgMapsController(IMapsContainer container, IMapsDataExtractorGeneric<BgMapDataType> dataExtractor)
        {
            this.MapContainer = container;
            this.MapDataExtractor = dataExtractor;
        }

        public IMapsContainer MapContainer { get; set; }

        public IMapsDataExtractorGeneric<BgMapDataType> MapDataExtractor { get; set; }

#pragma warning disable CA1720 // Identifier contains type name
        public virtual ActionResult Single(string id)
#pragma warning restore CA1720 // Identifier contains type name
        {
            return new JsonMapsResult(this.MapContainer.GetMap(id));
        }

        public virtual ActionResult All()
        {
            return new JsonMapsResult(this.MapContainer.GetAll());
        }

        public virtual ActionResult DataSingle(string id, BgMapDataType dataType)
        {
            return new JsonMapsResult(this.MapDataExtractor.ExtractDataForMap(dataType, int.Parse(id)));
        }

        public virtual ActionResult DataAll(BgMapDataType dataType)
        {
            return new JsonMapsResult(this.MapDataExtractor.ExtractDataForAllMaps(dataType));
        }

        private void GenerateMapsJsFiles(BgMapDataType dataType)
        {
            JsonMapsResult all = (JsonMapsResult)this.All();
            JsonMapsResult dataAll = (JsonMapsResult)this.DataAll(dataType);

            var fileNameMaps = AppDomain.CurrentDomain.BaseDirectory + "Scripts\\" + "map\\" + "Map.js";
            var fileNameMapsData = AppDomain.CurrentDomain.BaseDirectory + "Scripts\\" + "map\\" + "MapData.js";

            var mapsJson = JsonConvert.SerializeObject(
                all.Maps,
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented });
            var mapsDataJson = JsonConvert.SerializeObject(
                dataAll.Maps,
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented });

            using (var writer = new StreamWriter(fileNameMaps))
            {
                writer.WriteLine("// Generated at {0}", DateTime.Now);
                writer.WriteLine();
                writer.WriteLine("var BG_MAP =");
                writer.Write(mapsJson);
            }

            using (var writer = new StreamWriter(fileNameMapsData))
            {
                writer.WriteLine("// Generated at {0}", DateTime.Now);
                writer.WriteLine();
                writer.WriteLine("var BG_MAP_DATA =");
                writer.Write(mapsDataJson);
            }
        }
    }
}
