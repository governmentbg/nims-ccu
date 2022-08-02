using Eumis.Domain.Procedures.Validation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class Level3BoolAttrubuteParameter : CustomNCalcExpressionParameter
    {
        private static readonly Dictionary<string, ParameterType> AliasTypeDictionary = new Dictionary<string, ParameterType>()
        {
            { "Допустим разход",  ParameterType.IsEligableCost },
            { "Is eligable cost",  ParameterType.IsEligableCost },
        };

        public Level3BoolAttrubuteParameter()
        {
            this.InitFields(
                string.Format(
                    "Атрибут на детайл от бюджета. Връща булева стойност на детайл на бюджет. (Формат: {0})",
                    GetParameterAliasDescription()),
                "Атрибут на детайл от беджета",
                NCalcType.Boolean,
                false,
                null,
                null,
                null,
                null);
        }

        internal enum ParameterType
        {
            IsEligableCost,
        }

        public static Dictionary<string, List<bool>> GetContextValue(NCalcEvaluationContext evaluationContext)
        {
            Dictionary<string, List<bool>> returnValue = new Dictionary<string, List<bool>>();

            foreach (var attributeType in AliasTypeDictionary)
            {
                List<bool> list = new List<bool>();

                foreach (var programmeBudget in evaluationContext.Budget.ProgrammeBudgetCollection)
                {
                    foreach (var programmeExpenseBudget in programmeBudget.ProgrammeExpenseBudgetCollection)
                    {
                        foreach (var programmeDetailsExpenseBudget in programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection)
                        {
                            list.Add(true);
                        }
                    }
                }

                returnValue.Add(attributeType.Key, list);
            }

            return returnValue;
        }

        public static KeyValuePair<string, List<bool>> GetExactParameterValue(ParameterType level3ParameterType, NCalcEvaluationContext evaluationContext)
        {
            var attribute = AliasTypeDictionary.Single(e => e.Value == level3ParameterType);

            List<bool> list = new List<bool>();

            foreach (var programmeBudget in evaluationContext.Budget.ProgrammeBudgetCollection)
            {
                foreach (var programmeExpenseBudget in programmeBudget.ProgrammeExpenseBudgetCollection)
                {
                    foreach (var programmeDetailsExpenseBudget in programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection)
                    {
                        list.Add(true);
                    }
                }
            }

            return new KeyValuePair<string, List<bool>>(attribute.Key, list);
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
            return AliasTypeDictionary.Any(e => e.Key.ToLower().Trim().Replace(" ", string.Empty) == parameterName.ToLower().Trim().Replace(" ", string.Empty));
        }
    }
}
