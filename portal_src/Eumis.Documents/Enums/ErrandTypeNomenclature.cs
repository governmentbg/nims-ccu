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
    public class ErrandTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.ErrandTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.ErrandTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string ResourceKey { get; set; }

        // ZOP
        private static string zopCode = Eumis.Documents.Enums.ErrandLegalActNomenclature.ZOP.Code;

        public static readonly ErrandTypeNomenclature OpenProcedure = new ErrandTypeNomenclature { ResourceKey = "OpenProcedure", Code = "01", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature RestrictedProcedure = new ErrandTypeNomenclature { ResourceKey = "RestrictedProcedure", Code = "02", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature DesignContest = new ErrandTypeNomenclature { ResourceKey = "DesignContest", Code = "03", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature NegotiationWithAnnouncementProcedure = new ErrandTypeNomenclature { ResourceKey = "NegotiationWithAnnouncementProcedure", Code = "04", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature NegotiationWithoutAnnouncementProcedure = new ErrandTypeNomenclature { ResourceKey = "NegotiationWithoutAnnouncementProcedure", Code = "05", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature CompetitiveDialogue = new ErrandTypeNomenclature { ResourceKey = "CompetitiveDialogue", Code = "06", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature Other = new ErrandTypeNomenclature { ResourceKey = "Other", Code = "07", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature CompetitiveProcedureWithNegotiation = new ErrandTypeNomenclature() { ResourceKey = "CompetitiveProcedureWithNegotiation", Code = "20", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature NegotationWithPriorCall = new ErrandTypeNomenclature() { ResourceKey = "NegotationWithPriorCall", Code = "21", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature InnovationPartnership = new ErrandTypeNomenclature() { ResourceKey = "InnovationPartnership", Code = "22", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature NegotationWithoutPriorCall = new ErrandTypeNomenclature() { ResourceKey = "NegotationWithoutPriorCall", Code = "23", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature NegotationWithoutPublicationOfNotices = new ErrandTypeNomenclature() { ResourceKey = "NegotationWithoutPublicationOfNotices", Code = "24", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature PublicContest = new ErrandTypeNomenclature() { ResourceKey = "PublicContest", Code = "25", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature DirectNegotation = new ErrandTypeNomenclature() { ResourceKey = "DirectNegotation", Code = "26", ParentCode = zopCode };
        public static readonly ErrandTypeNomenclature CollectingOffersWithАnnouncement = new ErrandTypeNomenclature() { ResourceKey = "CollectingOffersWithАnnouncement", Code = "27", ParentCode = zopCode };

        // PMS
        private static string pmsCode = Eumis.Documents.Enums.ErrandLegalActNomenclature.PMS.Code;

        public static readonly ErrandTypeNomenclature ChoosingPublicCall = new ErrandTypeNomenclature { ResourceKey = "ChoosingPublicCall", Code = "11", ParentCode = pmsCode };
        public static readonly ErrandTypeNomenclature ChoiceWithoutProcedure = new ErrandTypeNomenclature { ResourceKey = "ChoiceWithoutProcedure", Code = "12", ParentCode = pmsCode };

        public IEnumerable<ErrandTypeNomenclature> GetItems(string parentCode)
        {
            return new List<ErrandTypeNomenclature>() {
                OpenProcedure,
                RestrictedProcedure,
                DesignContest,
                NegotiationWithAnnouncementProcedure,
                NegotiationWithoutAnnouncementProcedure,
                CompetitiveDialogue,
                CompetitiveProcedureWithNegotiation,
                NegotationWithPriorCall,
                InnovationPartnership,
                NegotationWithoutPriorCall,
                NegotationWithoutPublicationOfNotices,
                PublicContest,
                DirectNegotation,
                CollectingOffersWithАnnouncement,
                Other,

                ChoosingPublicCall,
                ChoiceWithoutProcedure
            }
            .Where(e=>e.ParentCode.Equals(parentCode));
        }
    }
}
