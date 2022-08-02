using Eumis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class GuidanceModuleNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.GuidanceModuleNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly GuidanceModuleNomenclature InternalSystem = new GuidanceModuleNomenclature { ResourceKey = "InternalSystem", Id = "internalSystem" };
        public static readonly GuidanceModuleNomenclature ApplicationsPortal = new GuidanceModuleNomenclature { ResourceKey = "ApplicationsPortal", Id = "applicationsPortal" };
        public static readonly GuidanceModuleNomenclature ReportsPortal = new GuidanceModuleNomenclature { ResourceKey = "ReportsPortal", Id = "reportsPortal" };
        public static readonly GuidanceModuleNomenclature PublicPortal = new GuidanceModuleNomenclature { ResourceKey = "PublicPortal", Id = "publicPortal" };

        public IEnumerable<SerializableSelectListItem> GetItems()
        {
            return new List<GuidanceModuleNomenclature>() {
                InternalSystem,
                ApplicationsPortal,
                ReportsPortal,
                PublicPortal
            }.Select(e => new SerializableSelectListItem() { Text = e.Name, Value = e.Id }).ToList();
        }
    }
}
