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
    public class IsVatEligibleNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.IsVatEligibleNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.IsVatEligibleNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly IsVatEligibleNomenclature No = new IsVatEligibleNomenclature { ResourceKey = "No", Code = "0" };
        public static readonly IsVatEligibleNomenclature Yes = new IsVatEligibleNomenclature { ResourceKey = "Yes", Code = "1" };
        public static readonly IsVatEligibleNomenclature Other = new IsVatEligibleNomenclature { ResourceKey = "Other", Code = "2" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<IsVatEligibleNomenclature>() { Yes, No, Other }
                .Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Code, NameEN = e.NameEN, Text = SystemLocalization.GetDisplayName(e.Name, e.NameEN) })
                .ToList();
        }
    }
}
