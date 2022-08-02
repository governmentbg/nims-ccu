using Eumis.Domain.Procedures.Validation.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class GroupLevelParameter : CustomNCalcExpressionParameter
    {
        private static readonly Dictionary<string, GroupSubTreeParameterType> AliasTypeDictionary = new Dictionary<string, GroupSubTreeParameterType>()
        {
            { "Ниво 1",  GroupSubTreeParameterType.Level1Group },
            { "Level 1",  GroupSubTreeParameterType.Level1Group },
            { "Ниво 2",  GroupSubTreeParameterType.Level2Group },
            { "Level 2",  GroupSubTreeParameterType.Level2Group },
        };

        public GroupLevelParameter()
        {
            this.InitFields(
                string.Format(
                    "Параметър ниво на групиране. Указва на какво ниво да се групиранат елементите на бюджета. (Формат: {0})",
                    GetParameterAliasDescription()),
                "Параметър ниво на групиране",
                NCalcType.NCalcExpressionParameter,
                false,
                null,
                null,
                null,
                null);
        }

        private enum GroupSubTreeParameterType
        {
            Level1Group,
            Level2Group,
        }

        public static bool MatchParameterAlias(string parameterName)
        {
            return AliasTypeDictionary.Any(e => e.Key.ToLower().Trim().Replace(" ", string.Empty) == parameterName.ToLower().Trim().Replace(" ", string.Empty));
        }

        public static List<string> GetContextValue(string groupLevelName, NCalcEvaluationContext evaluationContext)
        {
            GroupSubTreeParameterType parameterType = GetGroupSubTreeParameterType(groupLevelName);

            List<string> resultList = new List<string>();

            if (parameterType == GroupSubTreeParameterType.Level1Group)
            {
                int level1Index = 0;
                foreach (var programmeBudget in evaluationContext.Budget.ProgrammeBudgetCollection)
                {
                    level1Index++;
                    resultList.Add(RomanNumeralsConverter.ConvertArabicToRoman(level1Index));
                }
            }
            else if (parameterType == GroupSubTreeParameterType.Level2Group)
            {
                int level2Index = 0;
                foreach (var programmeBudget in evaluationContext.Budget.ProgrammeBudgetCollection)
                {
                    foreach (var programmeExpenseBudget in programmeBudget.ProgrammeExpenseBudgetCollection)
                    {
                        level2Index++;
                        resultList.Add(level2Index.ToString());
                    }
                }
            }

            return resultList;
        }

        private static GroupSubTreeParameterType GetGroupSubTreeParameterType(string parameterName)
        {
            return AliasTypeDictionary.Single(e => e.Key.ToLower().Trim().Replace(" ", string.Empty) == parameterName.ToLower().Trim().Replace(" ", string.Empty)).Value;
        }

        private static string GetParameterAliasDescription()
        {
            string description = string.Empty;

            for (int i = 0; i < AliasTypeDictionary.Count; i++)
            {
                description += string.Format("'[{0}]'{1}", AliasTypeDictionary.ToList()[i].Key, i != AliasTypeDictionary.Count - 1 ? ", " : string.Empty);
            }

            return description;
        }

        public override bool MatchName(string parameterName)
        {
            return MatchParameterAlias(parameterName);
        }
    }
}
