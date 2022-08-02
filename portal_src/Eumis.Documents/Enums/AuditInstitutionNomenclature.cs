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
    public class AuditInstitutionNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.AuditInstitutionNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.AuditInstitutionNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly AuditInstitutionNomenclature Court = new AuditInstitutionNomenclature { ResourceKey = "Court", Id = "court" };
        public static readonly AuditInstitutionNomenclature Internal = new AuditInstitutionNomenclature { ResourceKey = "Internal", Id = "internal" };
        public static readonly AuditInstitutionNomenclature Verification = new AuditInstitutionNomenclature { ResourceKey = "Verification", Id = "verification" };
        public static readonly AuditInstitutionNomenclature Other = new AuditInstitutionNomenclature { ResourceKey = "Other", Id = "other" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<AuditInstitutionNomenclature>() {
                Court,
                Internal,
                Verification,
                Other
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Id, NameEN = e.NameEN }).ToList();
        }
    }
}
