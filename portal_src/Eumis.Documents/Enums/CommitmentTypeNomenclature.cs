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
    public class CommitmentTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.CommitmentTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.CommitmentTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly CommitmentTypeNomenclature EmploymentContract = new CommitmentTypeNomenclature { ResourceKey = "EmploymentContract", Id = "employmentContract" };
        public static readonly CommitmentTypeNomenclature CivilContract = new CommitmentTypeNomenclature { ResourceKey = "CivilContract", Id = "civilContract" };
        public static readonly CommitmentTypeNomenclature Order = new CommitmentTypeNomenclature { ResourceKey = "Order", Id = "order" };
        public static readonly CommitmentTypeNomenclature Other = new CommitmentTypeNomenclature { ResourceKey = "Other", Id = "other" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<CommitmentTypeNomenclature>() {
                EmploymentContract,
                CivilContract,
                Order,
                Other
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Id, NameEN = e.NameEN }).ToList();
        }
    }
}
