using Eumis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class ApplicationFormTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.ApplicationFormTypeNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly ApplicationFormTypeNomenclature Standard = new ApplicationFormTypeNomenclature { ResourceKey = "Standard", Code = "standard" };
        public static readonly ApplicationFormTypeNomenclature PreliminarySelection = new ApplicationFormTypeNomenclature { ResourceKey = "PreliminarySelection", Code = "preliminarySelection" };
        public static readonly ApplicationFormTypeNomenclature StandardWithPreliminarySelection = new ApplicationFormTypeNomenclature { ResourceKey = "StandardWithPreliminarySelection", Code = "standardWithPreliminarySelection" };
        public static readonly ApplicationFormTypeNomenclature StandardForBudgetLine = new ApplicationFormTypeNomenclature { ResourceKey = "StandardForBudgetLine", Code = "standardForBudgetLine" };
        public static readonly ApplicationFormTypeNomenclature StandardSimplified = new ApplicationFormTypeNomenclature { ResourceKey = "StandardSimplified", Code = "standardSimplified" };

        // Final intermediaries
        public static readonly ApplicationFormTypeNomenclature FOFFinancialAgentsInfo = new ApplicationFormTypeNomenclature { ResourceKey = "FOFFinancialAgentsInfo", Code = "fofFinancialAgentsInfo" };

        // Final recipients
        public static readonly ApplicationFormTypeNomenclature EndUsersInfo = new ApplicationFormTypeNomenclature { ResourceKey = "EndUsersInfo", Code = "endUsersInfo" };
        
        public IEnumerable<SerializableSelectListItem> GetItems()
        {
            return new List<ApplicationFormTypeNomenclature>() { Standard, PreliminarySelection, StandardWithPreliminarySelection, StandardForBudgetLine, StandardSimplified }
                .Select(e => new SerializableSelectListItem() { Text = e.Name, Value = e.Code })
                .ToList();
        }
    }
}
