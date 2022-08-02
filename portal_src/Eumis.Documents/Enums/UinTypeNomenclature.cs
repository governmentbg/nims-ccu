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
    public class UinTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.UinTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.UinTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly UinTypeNomenclature UIC = new UinTypeNomenclature { ResourceKey = "UIC", Code = "eik" };
        public static readonly UinTypeNomenclature Bulstat = new UinTypeNomenclature { ResourceKey = "Bulstat", Code = "bulstat" };
        public static readonly UinTypeNomenclature Freelancers = new UinTypeNomenclature { ResourceKey = "Freelancers", Code = "personalBulstat" };
        public static readonly UinTypeNomenclature Foreign = new UinTypeNomenclature { ResourceKey = "Foreign", Code = "foreign" };

        public static readonly UinTypeNomenclature Egn = new UinTypeNomenclature { ResourceKey = "Egn", Code = "egn" };
        public static readonly UinTypeNomenclature ForeignPerson = new UinTypeNomenclature { ResourceKey = "ForeignPerson", Code = "foreignPerson" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<UinTypeNomenclature>() { UIC, Bulstat, Freelancers, Foreign }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Code, NameEN = e.NameEN });
        }

        public IEnumerable<LocalizedSelectListItem> GetTechnicalReportTeamItems()
        {
            return new List<UinTypeNomenclature>() { Egn, ForeignPerson }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Code, NameEN = e.NameEN });
        }
    }
}
