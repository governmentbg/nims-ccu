using System;
using System.Collections.Generic;
using System.Linq;

using Eumis.Common;
using Eumis.Common.Localization;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class CostSupportingDocumentTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.CostSupportingDocumentTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.Bulgarian));
            }
        }

        public string NameEN
        {
            get
            {
                return App_LocalResources.CostSupportingDocumentTypeNomenclature.ResourceManager.GetString(ResourceKey, SystemLocalization.GetLanguageCulture(LocalizationLanguage.English));
            }
        }

        public string Id { get; set; }

        public string ResourceKey { get; set; }

        public static readonly CostSupportingDocumentTypeNomenclature Invoice = new CostSupportingDocumentTypeNomenclature { ResourceKey = "Invoice", Id = "invoice" };
        public static readonly CostSupportingDocumentTypeNomenclature DebitNotification = new CostSupportingDocumentTypeNomenclature { ResourceKey = "DebitNotification", Id = "debitNotification" };
        public static readonly CostSupportingDocumentTypeNomenclature CreditNotification = new CostSupportingDocumentTypeNomenclature { ResourceKey = "CreditNotification", Id = "creditNotification" };
        public static readonly CostSupportingDocumentTypeNomenclature CashReceipt = new CostSupportingDocumentTypeNomenclature { ResourceKey = "CashReceipt", Id = "cashReceipt" };
        public static readonly CostSupportingDocumentTypeNomenclature PaidAmountsAccount = new CostSupportingDocumentTypeNomenclature { ResourceKey = "PaidAmountsAccount", Id = "paidAmountsAccount" };
        public static readonly CostSupportingDocumentTypeNomenclature Payroll = new CostSupportingDocumentTypeNomenclature { ResourceKey = "Payroll", Id = "payroll" };
        public static readonly CostSupportingDocumentTypeNomenclature CountryTravelOrder = new CostSupportingDocumentTypeNomenclature { ResourceKey = "CountryTravelOrder", Id = "countryTravelOrder" };
        public static readonly CostSupportingDocumentTypeNomenclature AdvanceReport = new CostSupportingDocumentTypeNomenclature { ResourceKey = "AdvanceReport", Id = "advanceReport" };
        public static readonly CostSupportingDocumentTypeNomenclature VATProtocol = new CostSupportingDocumentTypeNomenclature { ResourceKey = "VATProtocol", Id = "vATProtocol" };
        public static readonly CostSupportingDocumentTypeNomenclature CostCashReceipts = new CostSupportingDocumentTypeNomenclature { ResourceKey = "CostCashReceipts", Id = "costCashReceipts" };
        public static readonly CostSupportingDocumentTypeNomenclature Other = new CostSupportingDocumentTypeNomenclature { ResourceKey = "Other", Id = "other" };

        public IEnumerable<LocalizedSelectListItem> GetItems()
        {
            return new List<CostSupportingDocumentTypeNomenclature>() {
                Invoice,
                CreditNotification,
                CashReceipt,
                PaidAmountsAccount,
                Payroll,
                CountryTravelOrder,
                AdvanceReport,
                VATProtocol,
                CostCashReceipts,
                Other
            }.Select(e => new LocalizedSelectListItem() { Name = e.Name, Value = e.Id, NameEN = e.NameEN }).ToList();
        }
    }
}
