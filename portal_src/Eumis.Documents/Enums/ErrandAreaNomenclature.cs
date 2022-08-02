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
    public class ErrandAreaNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.ErrandAreaNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.ErrandAreaNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly ErrandAreaNomenclature Delivery = new ErrandAreaNomenclature { ResourceKey = "Delivery", Code = "0" };
        public static readonly ErrandAreaNomenclature Service = new ErrandAreaNomenclature { ResourceKey = "Service", Code = "1" };
        public static readonly ErrandAreaNomenclature Building = new ErrandAreaNomenclature { ResourceKey = "Building", Code = "2" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<ErrandAreaNomenclature>() {
                Delivery,
                Service,
                Building
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Code, NameEN = e.NameEN }).ToList();
        }
    }
}
