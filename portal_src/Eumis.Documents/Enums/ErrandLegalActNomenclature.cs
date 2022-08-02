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
    public class ErrandLegalActNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.ErrandLegalActNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.ErrandLegalActNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly ErrandLegalActNomenclature ZOP = new ErrandLegalActNomenclature { ResourceKey = "ZOP", Code = "92af17f1-9243-4f7e-be07-7a2629688d1b" };
        public static readonly ErrandLegalActNomenclature PMS = new ErrandLegalActNomenclature { ResourceKey = "PMS", Code = "7e9b44e8-742b-45e5-b967-7b7feec6e18a" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<ErrandLegalActNomenclature>() {
                ZOP,
                PMS
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Code, NameEN = e.NameEN }).ToList();
        }
    }
}
