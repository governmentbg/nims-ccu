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
    public class ActivityStatusNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.ActivityStatusNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.ActivityStatusNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly ActivityStatusNomenclature NotStarted = new ActivityStatusNomenclature { ResourceKey = "NotStarted", Id = "notStarted" };
        public static readonly ActivityStatusNomenclature InProgress = new ActivityStatusNomenclature { ResourceKey = "InProgress", Id = "inProgress" };
        public static readonly ActivityStatusNomenclature Completed = new ActivityStatusNomenclature { ResourceKey = "Completed", Id = "completed" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<ActivityStatusNomenclature>() {
                NotStarted,
                InProgress,
                Completed
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Id, NameEN = e.NameEN }).ToList();
        }
    }
}
