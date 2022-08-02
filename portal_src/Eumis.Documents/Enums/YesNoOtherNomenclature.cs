using Eumis.Common;
using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class YesNoOtherNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.YesNoOtherNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.YesNoOtherNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly YesNoOtherNomenclature Yes = new YesNoOtherNomenclature { ResourceKey = "Yes", Code = "yes" };
        public static readonly YesNoOtherNomenclature No = new YesNoOtherNomenclature { ResourceKey = "No", Code = "no" };
        public static readonly YesNoOtherNomenclature Other = new YesNoOtherNomenclature { ResourceKey = "Other", Code = "other" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<YesNoOtherNomenclature>() { Yes, No, Other }
                .Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Code, NameEN = e.NameEN, Text = SystemLocalization.GetDisplayName(e.Name, e.NameEN) })
                .ToList();
        }
    }
}
