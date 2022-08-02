using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Domain.Entities;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Web.Models.Maps;

namespace Eumis.Public.Web.InfrastructureClasses.Maps
{
    public class BgMapsDataExtractor : IMapsDataExtractorGeneric<BgMapDataType>
    {
        private IEnumerable<Map> maps;
        private IEnumerable<MapRegion> mapRegions;

        public BgMapsDataExtractor(IMapsRepository mapsRepository)
        {
            this.MapsRepository = mapsRepository;
        }

        private IMapsRepository MapsRepository { get; set; }

        public MapDataModel ExtractDataForMap(BgMapDataType dataType, int mapId)
        {
            MapDataModel data = null;

            if (dataType == BgMapDataType.NameOnly)
            {
                Map domainMap = this.MapsRepository.GetMapById(mapId);
                if (domainMap != null)
                {
                    MapDataModel map = new MapDataModel(
                        mapId,
                        domainMap.Name,
                        domainMap.NameAlt,
                        new List<MapRegionDataModel>());

                    var regions = this.mapRegions.Where(m => m.MapId == mapId);

                    foreach (var r in regions)
                    {
                        MapRegionDataModel reg = new MapRegionDataModel(r.Id, r.Name, r.NameAlt);

                        if (r.NutsLevel != Domain.Entities.Umis.NonAggregates.NutsLevel.Municipality)
                        {
                            var drilldown = this.GetChildMapId(r.NutsLevel, r.RegionId);
                            reg.Drilldown = drilldown;
                        }

                        map.Regions.Add(reg);
                    }

                    data = map;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format("Map id '{0}' not found!", mapId));
                }
            }
            else
            {
                this.ThrowArgumentNotSupported(dataType);
            }

            return data;
        }

        public List<MapDataModel> ExtractDataForAllMaps(BgMapDataType dataType)
        {
            List<MapDataModel> data = new List<MapDataModel>();

            if (dataType == BgMapDataType.NameOnly)
            {
                this.maps = this.MapsRepository.GetAllMapsByType(MapTypeEnum.BgNuts);
                this.mapRegions = this.MapsRepository.GetAllMapRegions();

                foreach (var map in this.maps)
                {
                    data.Add(this.ExtractDataForMap(BgMapDataType.NameOnly, map.Id));
                }
            }
            else
            {
                this.ThrowArgumentNotSupported(dataType);
            }

            return data;
        }

        private void ThrowArgumentNotSupported(BgMapDataType dataType)
        {
            this.ThrowArgumentNotSupported("dataType", ((int)dataType).ToString());
        }

        private void ThrowArgumentNotSupported(string name, string value)
        {
            throw new ArgumentException(string.Format("Provided {1} '{0}' is not supported!", value, name));
        }

        private int GetChildMapId(NutsLevel nutsLevel, int regionId)
        {
            int mapid = 0;

            var map = this.maps
                .Where(m => m.NutsLevel == nutsLevel && m.RegionId == regionId)
                .FirstOrDefault();

            if (map != null)
            {
                mapid = map.Id;
            }

            return mapid;
        }
    }
}