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
    public class AuditKindNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.AuditKindNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.AuditKindNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly AuditKindNomenclature Primary = new AuditKindNomenclature { ResourceKey = "Primary", Id = "primary" };
        public static readonly AuditKindNomenclature Subsequent = new AuditKindNomenclature { ResourceKey = "Subsequent", Id = "subsequent" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<AuditKindNomenclature>() {
                Primary,
                Subsequent
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Id, NameEN = e.NameEN }).ToList();
        }
    }
}
