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
    public class PaymentRequestTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.PaymentRequestTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.PaymentRequestTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
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

        public static readonly PaymentRequestTypeNomenclature Advance = new PaymentRequestTypeNomenclature { ResourceKey = "Advance", Id = "advance" };
        public static readonly PaymentRequestTypeNomenclature AdvanceVerification = new PaymentRequestTypeNomenclature { ResourceKey = "AdvanceVerification", Id = "advanceVerification" };
        public static readonly PaymentRequestTypeNomenclature Intermediate = new PaymentRequestTypeNomenclature { ResourceKey = "Intermediate", Id = "intermediate" };
        public static readonly PaymentRequestTypeNomenclature Final = new PaymentRequestTypeNomenclature { ResourceKey = "Final", Id = "final" };

        public IEnumerable<PaymentRequestTypeNomenclature> GetItems()
        {
            return new List<PaymentRequestTypeNomenclature>() {
                Advance,
                AdvanceVerification,
                Intermediate,
                Final
            };
        }

        public IEnumerable<LocalizedSelectListItem> GetAdvanceItems()
        {
            return new List<PaymentRequestTypeNomenclature>() {
                Advance,
                AdvanceVerification
            }.Select(e => new LocalizedSelectListItem() { Value = e.Id, Name = e.Name, NameEN = e.NameEN });
        }

        public IEnumerable<LocalizedSelectListItem> GetFinalItems()
        {
            return new List<PaymentRequestTypeNomenclature>() {
                Intermediate,
                Final
            }.Select(e => new LocalizedSelectListItem() { Value = e.Id, Name = e.Name, NameEN = e.NameEN });
        }


    }
}
