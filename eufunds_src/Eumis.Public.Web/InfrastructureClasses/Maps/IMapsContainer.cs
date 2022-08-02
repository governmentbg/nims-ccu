using System.Collections.Generic;
using Eumis.Public.Web.Models.Maps;

namespace Eumis.Public.Web.InfrastructureClasses.Maps
{
    public interface IMapsContainer
    {
        MapModel GetMap(string id);

        IReadOnlyList<MapModel> GetAll();
    }
}