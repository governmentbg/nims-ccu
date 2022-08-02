using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class MarkerChangesNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.MarkerChangesNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly MarkerChangesNomenclature Changes = new MarkerChangesNomenclature { ResourceKey = "Changes", Id = "changes" };
        public static readonly MarkerChangesNomenclature NoChanges = new MarkerChangesNomenclature { ResourceKey = "NoChanges", Id = "noChanges" };

        public IEnumerable<MarkerChangesNomenclature> GetItems()
        {
            return new List<MarkerChangesNomenclature>() {
                Changes,
                NoChanges
            };
        }
    }
}
