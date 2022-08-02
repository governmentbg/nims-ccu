using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    public class FinanceSourceNomenclature
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public const string EuropeanSocialFundId = "europeanSocialFund";
        public const string YouthEmploymentInitiativeId = "youthEmploymentInitiative";
        public const string EuropeanRegionalDevelopmentFundId = "europeanRegionalDevelopmentFund";
        public const string CohesionFundId = "cohesionFund";
        public const string FundForEuropeanAidToTheMostDeprivedId = "FundForEuropeanAidToTheMostDeprived";

        public static readonly FinanceSourceNomenclature EuropeanSocialFund = new FinanceSourceNomenclature { Id = EuropeanSocialFundId, Name = "ЕСФ" };
        public static readonly FinanceSourceNomenclature YouthEmploymentInitiative = new FinanceSourceNomenclature { Id = YouthEmploymentInitiativeId, Name = "ИМЗ" };
        public static readonly FinanceSourceNomenclature EuropeanRegionalDevelopmentFund = new FinanceSourceNomenclature { Id = EuropeanRegionalDevelopmentFundId, Name = "ЕФРР" };
        public static readonly FinanceSourceNomenclature CohesionFund = new FinanceSourceNomenclature { Id = CohesionFundId, Name = "КФ" };
        public static readonly FinanceSourceNomenclature FundForEuropeanAidToTheMostDeprived = new FinanceSourceNomenclature { Id = FundForEuropeanAidToTheMostDeprivedId, Name = "ФЕПНЛ" };

        public IEnumerable<FinanceSourceNomenclature> GetItems()
        {
            return new List<FinanceSourceNomenclature>() {
                EuropeanSocialFund,
                YouthEmploymentInitiative,
                EuropeanRegionalDevelopmentFund,
                CohesionFund,
                FundForEuropeanAidToTheMostDeprived
            };
        }
    }
}
