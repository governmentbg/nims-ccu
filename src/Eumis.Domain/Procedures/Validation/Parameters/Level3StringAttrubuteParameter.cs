using Eumis.Domain.Procedures.Validation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class Level3StringAttrubuteParameter : CustomNCalcExpressionParameter
    {
        private static readonly Dictionary<string, ParameterType> AliasTypeDictionary = new Dictionary<string, ParameterType>()
        {
            { "01",  ParameterType.InterventionFieldCode },
            { "02",  ParameterType.FormOfFinanceCode },
            { "03",  ParameterType.TerritorialDimensionCode },
            { "04",  ParameterType.TerritorialDeliveryMechanismCode },
            { "05",  ParameterType.ThematicObjectiveCode },
            { "06",  ParameterType.ESFSecondaryThemeCode },
            { "07",  ParameterType.EconomicDimensionCode },
            { "NUTS2",  ParameterType.Nuts2 },
            { "Приоритетна ос",  ParameterType.ProgrammePriority },
            { "Programme priority",  ParameterType.ProgrammePriority },
            { "Финансов източник",  ParameterType.FinanceSource },
            { "Finance source",  ParameterType.FinanceSource },
            { "Режим на помощта",  ParameterType.AidMode },
            { "Aid mode",  ParameterType.AidMode },
        };

        public Level3StringAttrubuteParameter()
        {
            this.InitFields(
                string.Format(
                    "Атрибут на детайл от бюджета. Връща текстова стойност на детайл на бюджет. (Формат: {0})",
                    GetParameterAliasDescription()),
                "Атрибут на детайл от беджета",
                NCalcType.String,
                false,
                null,
                null,
                null,
                null);
        }

        internal enum ParameterType
        {
            InterventionFieldCode,
            FormOfFinanceCode,
            TerritorialDimensionCode,
            TerritorialDeliveryMechanismCode,
            ThematicObjectiveCode,
            ESFSecondaryThemeCode,
            EconomicDimensionCode,
            Nuts2,
            ProgrammePriority,
            FinanceSource,
            AidMode,
        }

        public static Dictionary<string, List<string>> GetContextValue(NCalcEvaluationContext evaluationContext)
        {
            Dictionary<string, List<string>> returnValue = new Dictionary<string, List<string>>();

            foreach (var attributeType in AliasTypeDictionary)
            {
                List<string> list = new List<string>();

                foreach (var programmeBudget in evaluationContext.Budget.ProgrammeBudgetCollection)
                {
                    foreach (var programmeExpenseBudget in programmeBudget.ProgrammeExpenseBudgetCollection)
                    {
                        foreach (var programmeDetailsExpenseBudget in programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection)
                        {
                            list.Add(GetLevel3Parameter(attributeType.Value, programmeExpenseBudget, programmeDetailsExpenseBudget));
                        }
                    }
                }

                returnValue.Add(attributeType.Key, list);
            }

            return returnValue;
        }

        public static KeyValuePair<string, List<string>> GetExactParameterValue(ParameterType level3ParameterType, NCalcEvaluationContext evaluationContext)
        {
            var attribute = AliasTypeDictionary.Single(e => e.Value == level3ParameterType);

            List<string> list = new List<string>();

            foreach (var programmeBudget in evaluationContext.Budget.ProgrammeBudgetCollection)
            {
                foreach (var programmeExpenseBudget in programmeBudget.ProgrammeExpenseBudgetCollection)
                {
                    foreach (var programmeDetailsExpenseBudget in programmeExpenseBudget.ProgrammeDetailsExpenseBudgetCollection)
                    {
                        list.Add(GetLevel3Parameter(level3ParameterType, programmeExpenseBudget, programmeDetailsExpenseBudget));
                    }
                }
            }

            return new KeyValuePair<string, List<string>>(attribute.Key, list);
        }

        private static string GetLevel3Parameter(ParameterType parameterType, Rio.ProgrammeExpenseBudget programmeExpenseBudget, Rio.ProgrammeDetailsExpenseBudget programmeDetailsExpenseBudget)
        {
            string value;

            switch (parameterType)
            {
                case ParameterType.Nuts2:
                    value = string.Empty;
                    var nutsFullPath = programmeDetailsExpenseBudget.Nuts?.FullPath;
                    if (!string.IsNullOrWhiteSpace(nutsFullPath))
                    {
                        string[] fullPath = nutsFullPath.Split(new char[] { ',' });
                        if (fullPath.Length >= 3)
                        {
                            value = fullPath[2].Trim();
                        }
                    }

                    break;
                case ParameterType.ProgrammePriority:
                    value = programmeExpenseBudget.ProgrammePriorityCode ?? string.Empty;
                    break;
                case ParameterType.AidMode:
                    value = programmeExpenseBudget.AidMode.Description ?? string.Empty;
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
