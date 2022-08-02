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
    public class AuditTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.AuditTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.AuditTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly AuditTypeNomenclature Court = new AuditTypeNomenclature { ResourceKey = "Court", Id = "court" };
        public static readonly AuditTypeNomenclature Internal = new AuditTypeNomenclature { ResourceKey = "Internal", Id = "internal" };
        public static readonly AuditTypeNomenclature Verification = new AuditTypeNomenclature { ResourceKey = "Verification", Id = "verification" };
        public static readonly AuditTypeNomenclature Other = new AuditTypeNomenclature { ResourceKey = "Other", Id = "other" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<AuditTypeNomenclature>() {
                Court,
                Internal,
                Verification,
                Other
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Id, NameEN = e.NameEN }).ToList();
        }
    }
}
