using Eumis.Domain.Procedures.Validation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class Level3DecimalAttrubuteParameter : CustomNCalcExpressionParameter
    {
        private static readonly Dictionary<string, ParameterType> AliasTypeDictionary = new Dictionary<string, ParameterType>()
        {
            // Numeric parameters
            { "БФП",  ParameterType.GrandAmount },
            { "Grand",  ParameterType.GrandAmount },
            { "СФ",  ParameterType.SelfAmount },
            { "Self",  ParameterType.SelfAmount },
            { "Общо",  ParameterType.TotalAmount },
            { "Total",  ParameterType.TotalAmount },
        };

        public Level3DecimalAttrubuteParameter()
        {
            this.InitFields(
                string.Format(
                    "Атрибут на детайл от бюджета. Връща числова стойност на детайл на бюджет. (Формат: {0})",
                    GetParameterAliasDescription()),
                "Атрибут на детайл от беджета",
                NCalcType.Float,
                false,
                null,
                null,
                null,
                null);
        }

        internal enum ParameterType
        {
            GrandAmount,
            SelfAmount,
            TotalAmount,
        }

        public static Dictionary<string, List<decimal>> GetContextValue(NCalcEvaluationContext evaluationContext)
        {
            Dictionary<string, List<decimal>> returnValue = new Dictionary<string, List<decimal>>();

            foreach (var attributeType in AliasTypeDictionary)
            {
                List<decimal> list = new List<decimal>();

                foreach (var programmeBudget in evaluationContext.Budget.ProgrammeBudgetCollection)
                {
                    foreach (var programmeExpenseBudget in programmeBudget.ProgrammeExpenseBudgetCollection)
                    {
                        foreach (var programmeDetailsExpenseBudget in programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection)
                        {
                            list.Add(GetLevel3Parameter(attributeType.Value, programmeDetailsExpenseBudget));
                        }
                    }
                }

                returnValue.Add(attributeType.Key, list);
            }

            return returnValue;
        }

        public static List<decimal> GetExactParameterValue(ParameterType level3ParameterType, NCalcEvaluationContext evaluationContext)
        {
            List<decimal> list = new List<decimal>();

            foreach (var programmeBudget in evaluationContext.Budget.ProgrammeBudgetCollection)
            {
                foreach (var programmeExpenseBudget in programmeBudget.ProgrammeExpenseBudgetCollection)
                {
                    foreach (var programmeDetailsExpenseBudget in programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection)
                    {
                        list.Add(GetLevel3Parameter(level3ParameterType, programmeDetailsExpenseBudget));
                    }
                }
            }

            return list;
        }

        private static decimal GetLevel3Parameter(ParameterType parameterType, Rio.ProgrammeDetailsExpenseBudget programmeDetailsExpenseBudget)
        {
            decimal value;

            switch (parameterType)
            {
                case ParameterType.GrandAmount:
                    value = programmeDetailsExpenseBudget.GrandAmount;
                    break;
                case ParameterType.SelfAmount:
                    value = programmeDetailsExpenseBudget.SelfAmount;
                    break;
                case ParameterType.TotalAmount:
                    value = programmeDetailsExpenseBudget.TotalAmount;
                    break;
                default:
                    throw new Exception("Invalid Level3Parameter.");
            }

            return value;
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
