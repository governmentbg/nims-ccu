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
    public class ProductKindNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.ProductKindNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.ProductKindNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly ProductKindNomenclature Loan = new ProductKindNomenclature { Id = "loan", ResourceKey = "Loan" };
        public static readonly ProductKindNomenclature Microloan = new ProductKindNomenclature { Id = "microloan", ResourceKey = "Microloan" };
        public static readonly ProductKindNomenclature Guarantee = new ProductKindNomenclature { Id = "guarantee", ResourceKey = "Guarantee" };
        public static readonly ProductKindNomenclature Investment = new ProductKindNomenclature { Id = "investment", ResourceKey = "Investment" };
        public static readonly ProductKindNomenclature QuasiInvestment = new ProductKindNomenclature { Id = "quasiInvestment", ResourceKey = "QuasiInvestment" };
        public static readonly ProductKindNomenclature Subsidies = new ProductKindNomenclature { Id = "Subsidies", ResourceKey = "Subsidies" };
        public static readonly ProductKindNomenclature GuaranteeSubsidies = new ProductKindNomenclature { Id = "guaranteeSubsidies", ResourceKey = "GuaranteeSubsidies" };
        public static readonly ProductKindNomenclature TechnicalSupport = new ProductKindNomenclature { Id = "technicalSupport", ResourceKey = "TechnicalSupport" };
        public static readonly ProductKindNomenclature Other = new ProductKindNomenclature { Id = "other", ResourceKey = "Other" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<ProductKindNomenclature>() {
                Loan,
                Microloan,
                Guarantee,
                Investment,
                QuasiInvestment,
                Subsidies,
                GuaranteeSubsidies,
                TechnicalSupport,
                Other
            }.Select(e => new LocalizedSelectListItem() { Value = e.Id, Name = e.Name, NameEN = e.NameEN });
        }
    }
}
