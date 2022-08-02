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
    public class CompletionStatusNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.CompletionStatus.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.CompletionStatus.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly CompletionStatusNomenclature Active = new CompletionStatusNomenclature { ResourceKey = "Active", Id = "active" };
        public static readonly CompletionStatusNomenclature Paused = new CompletionStatusNomenclature { ResourceKey = "Paused", Id = "paused" };
        public static readonly CompletionStatusNomenclature Monitored = new CompletionStatusNomenclature { ResourceKey = "Monitored", Id = "monitored" };
        public static readonly CompletionStatusNomenclature Canceled = new CompletionStatusNomenclature { ResourceKey = "Canceled", Id = "canceled" };
        public static readonly CompletionStatusNomenclature Ended = new CompletionStatusNomenclature { ResourceKey = "Ended", Id = "ended" };
        public static readonly CompletionStatusNomenclature Concluded = new CompletionStatusNomenclature { ResourceKey = "Concluded", Id = "concluded" };
        public static readonly CompletionStatusNomenclature Suspended = new CompletionStatusNomenclature { ResourceKey = "Suspended", Id = "suspended" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<CompletionStatusNomenclature>() {
                Active,
                Paused,
                Monitored,
                Canceled,
                Ended,
                Concluded,
                Suspended
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Id, NameEN = e.NameEN }).ToList();
        }
    }
}
