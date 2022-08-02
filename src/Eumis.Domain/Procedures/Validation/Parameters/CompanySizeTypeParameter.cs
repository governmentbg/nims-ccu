using Eumis.Domain.Procedures.Validation.Abstract;
using NCalc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class CompanySizeTypeParameter : CustomNCalcExpressionParameter
    {
        private static readonly List<string> ParameterAliases = new List<string>
        {
            "Категория/статус на предприятието",
            "Company size type",
        };

        public CompanySizeTypeParameter()
        {
            this.InitFields(
                string.Format("Параметър 'Категория/статус на предприятието'. Връща стойноста полето 'Категория/статус на предприятието'. (Формат: {0})", GetParameterAliasDescription()),
                "Параметър 'Категория/статус на предприятието'.",
                NCalcType.String,
                true,
                this.Evaluate,
                null,
                null,
                null);
        }

        private static string GetParameterAliasDescription()
        {
            string description = string.Empty;

            for (int i = 0; i < ParameterAliases.Count; i++)
            {
                description += string.Format("'[{0}]'{1}", ParameterAliases[i], i != ParameterAliases.Count - 1 ? ", " : string.Empty);
            }

            return description;
        }

        public override bool MatchName(string parameterName)
        {
            return ParameterAliases.Any(e => e.ToLower().Trim().Replace(" ", string.Empty) == parameterName.ToLower().Trim().Replace(" ", string.Empty));
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "parameter is required from delegate type")]
        private void Evaluate(string parameterName, ParameterArgs args, NCalcEvaluationContext evaluationContext)
        {
            string value = string.Empty;

            if (evaluationContext.Candidate.CompanySizeType != null &&
                !string.IsNullOrWhiteSpace(evaluationContext.Candidate.CompanySizeType.Name))
            {
                value = evaluationContext.Candidate.CompanySizeType.Name;
            }

            args.Result = value;
        }
    }
}
