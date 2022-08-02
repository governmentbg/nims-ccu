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
    public class AuditeeTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.AuditeeTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.AuditeeTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly AuditeeTypeNomenclature Beneficiary = new AuditeeTypeNomenclature { ResourceKey = "Beneficiary", Id = "beneficiary" };
        public static readonly AuditeeTypeNomenclature Partner = new AuditeeTypeNomenclature { ResourceKey = "Partner", Id = "partner" };
        public static readonly AuditeeTypeNomenclature Contractor = new AuditeeTypeNomenclature { ResourceKey = "Contractor", Id = "contractor" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<AuditeeTypeNomenclature>() {
                Beneficiary,
                Partner,
                Contractor
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Id, NameEN = e.NameEN }).ToList();
        }
    }
}
