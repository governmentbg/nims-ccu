using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class YesNoNotApplicableNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.YesNoNotApplicableNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly YesNoNotApplicableNomenclature Yes = new YesNoNotApplicableNomenclature { ResourceKey = "Yes", Id = "yes" };
        public static readonly YesNoNotApplicableNomenclature No = new YesNoNotApplicableNomenclature { ResourceKey = "No", Id = "no" };
        public static readonly YesNoNotApplicableNomenclature NotApplicable = new YesNoNotApplicableNomenclature { ResourceKey = "NotApplicable", Id = "notApplicable" };

        public IEnumerable<YesNoNotApplicableNomenclature> GetItems()
        {
            return new List<YesNoNotApplicableNomenclature>() {
                Yes,
                No,
                NotApplicable
            };
        }
    }
}
