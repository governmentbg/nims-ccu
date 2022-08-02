using System.Collections.Generic;
using System.Linq;
using Eumis.Public.Common;
using Eumis.Public.Common.Localization;

namespace Eumis.Public.Web.InfrastructureClasses
{
    public class MapRegionHelper
    {
        public static readonly MapRegionHelper International = new MapRegionHelper { Id = Common.Configuration.PR_INTERNATIONAL_ID, ParentId = Common.Configuration.PR_INTERNATIONAL_ID, Name = "Международен", NameAlt = "International" };
        public static readonly MapRegionHelper Bulgaria = new MapRegionHelper { Id = Common.Configuration.PR_DEFAULT_ID, ParentId = Common.Configuration.PR_BULGARIA_ID, Name = "България", NameAlt = "Bulgaria" };
        public static readonly MapRegionHelper NorthWest = new MapRegionHelper { Id = 19, ParentId = 13, Name = "Северозападен", NameAlt = "North-West Region" };
        public static readonly MapRegionHelper NorthCentral = new MapRegionHelper { Id = 18, ParentId = 11, Name = "Северен централен", NameAlt = "North Central Region" };
        public static readonly MapRegionHelper NorthEast = new MapRegionHelper { Id = 17, ParentId = 12, Name = "Североизточен", NameAlt = "North-East Region" };
        public static readonly MapRegionHelper SouthWest = new MapRegionHelper { Id = 15, ParentId = 27, Name = "Югозападен", NameAlt = "South-West Region" };
        public static readonly MapRegionHelper SouthCentral = new MapRegionHelper { Id = 20, ParentId = 25, Name = "Южен централен", NameAlt = "South Central Region" };
        public static readonly MapRegionHelper SouthEast = new MapRegionHelper { Id = 16, ParentId = 26, Name = "Югоизточен", NameAlt = "South-East Region" };

        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.NameAlt;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public static IEnumerable<MapRegionHelper> GetAll()
        {
            return new List<MapRegionHelper>()
            {
                Bulgaria,
                NorthWest,
                NorthCentral,
                NorthEast,
                SouthWest,
                SouthCentral,
                SouthEast,
            }.ToList();
        }
    }
}