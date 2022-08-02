using Eumis.Common.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    [MetadataType(typeof(NutsLevelNomenclatureMetadata))]
    public class NutsLevelNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.NutsLevelNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.NutsLevelNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string DisplayName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.Name, this.NameEN);
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly NutsLevelNomenclature CountryEU = new NutsLevelNomenclature { ResourceKey = "CountryEU", Id = "country" };
        public static readonly NutsLevelNomenclature ProtectedZone = new NutsLevelNomenclature { ResourceKey = "ProtectedZone", Id = "protectedZone" };
        public static readonly NutsLevelNomenclature Nuts1 = new NutsLevelNomenclature { ResourceKey = "Nuts1", Id = "regionNUTS1" };
        public static readonly NutsLevelNomenclature Nuts2 = new NutsLevelNomenclature { ResourceKey = "Nuts2", Id = "regionNUTS2" };
        public static readonly NutsLevelNomenclature District = new NutsLevelNomenclature { ResourceKey = "District", Id = "district" };
        public static readonly NutsLevelNomenclature Municipality = new NutsLevelNomenclature { ResourceKey = "Municipality", Id = "municipality" };
        public static readonly NutsLevelNomenclature Settlement = new NutsLevelNomenclature { ResourceKey = "Settlement", Id = "settlement" };

        public IEnumerable<NutsLevelNomenclature> GetItems()
        {
            return new List<NutsLevelNomenclature>() {
                CountryEU,
                ProtectedZone,
                Nuts1,
                Nuts2,
                District,
                Municipality,
                Settlement
            };
        }

        internal sealed class NutsLevelNomenclatureMetadata
        {
            //#pragma warning disable 0649

            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }

            //#pragma warning restore 0649
        }
    }
}
