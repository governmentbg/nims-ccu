using Eumis.Domain.Procedures.Validation.Abstract;
using NCalc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class BooleanValueParameter : CustomNCalcExpressionParameter
    {
        private static readonly Dictionary<string, bool> AliasTypeDictionary = new Dictionary<string, bool>()
        {
            { "Да",  true },
            { "Винаги",  true },
            { "Всички",  true },
            { "All",  true },
            { "True",  true },
            { "Не",  false },
            { "False",  false },
        };

        public BooleanValueParameter()
        {
            this.InitFields(
                string.Format(
                    "Параметър булева стойност. Указва с-т истина или лъжа. (Формат: {0})",
                    GetParameterAliasDescription()),
                "Параметър булева стойност",
                NCalcType.Boolean,
                true,
                this.Evaluate,
                null,
                null,
                null);
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

        private bool GetParameterValue(string parameterName)
        {
            return AliasTypeDictionary.Single(e => e.Key.ToLower().Trim().Replace(" ", string.Empty) == parameterName.ToLower().Trim().Replace(" ", string.Empty)).Value;
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "parameter is required from delegate type")]
        private void Evaluate(string parameterName, ParameterArgs args, NCalcEvaluationContext evaluationContext)
        {
            args.Result = this.GetParameterValue(parameterName);
        }
    }
}
