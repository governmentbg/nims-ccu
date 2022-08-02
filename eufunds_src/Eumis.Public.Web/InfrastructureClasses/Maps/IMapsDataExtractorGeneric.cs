using System.Collections.Generic;
using Eumis.Public.Web.Models.Maps;

namespace Eumis.Public.Web.InfrastructureClasses.Maps
{
    public interface IMapsDataExtractorGeneric<T>
    {
        MapDataModel ExtractDataForMap(T dataType, int mapId);

        List<MapDataModel> ExtractDataForAllMaps(T dataType);
    }
}