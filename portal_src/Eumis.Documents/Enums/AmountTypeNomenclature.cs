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
    public class AmountTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.AmountTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.AmountTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly AmountTypeNomenclature Big = new AmountTypeNomenclature { ResourceKey = "Big", Code = "0" };
        public static readonly AmountTypeNomenclature Infrastructure = new AmountTypeNomenclature { ResourceKey = "Infrastructure", Code = "1" };
        public static readonly AmountTypeNomenclature Other = new AmountTypeNomenclature { ResourceKey = "Other", Code = "2" };
        public static readonly AmountTypeNomenclature FinancialInstruments = new AmountTypeNomenclature { ResourceKey = "FinancialInstruments", Code = "3" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<AmountTypeNomenclature>() { Big, Infrastructure, FinancialInstruments, Other }
                .Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Code, NameEN = e.NameEN })
                .ToList();
        }
    }
}
