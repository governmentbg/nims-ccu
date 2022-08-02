using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class AcceptanceTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.AcceptanceTypeNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly AcceptanceTypeNomenclature Yes = new AcceptanceTypeNomenclature { ResourceKey = "Yes", Id = "0" };
        public static readonly AcceptanceTypeNomenclature No = new AcceptanceTypeNomenclature { ResourceKey = "No", Id = "1" };
        public static readonly AcceptanceTypeNomenclature NotApplicable = new AcceptanceTypeNomenclature { ResourceKey = "NotApplicable", Id = "2" };

        public IEnumerable<AcceptanceTypeNomenclature> GetItems()
        {
            return new List<AcceptanceTypeNomenclature>() {
                Yes,
                No,
                NotApplicable
            };
        }
    }
}
