using Eumis.Domain.Procedures.Validation.Abstract;
using NCalc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class CandidateUinParameter : CustomNCalcExpressionParameter
    {
        private static readonly List<string> ParameterAliases = new List<string>
        {
            "Номер",
            "Candidate uin",
        };

        public CandidateUinParameter()
        {
            this.InitFields(
                string.Format(
                    "Параметър 'Номер'. Връща стойноста полето 'Номер'. (Формат: {0})",
                    GetParameterAliasDescription()),
                "Параметър 'Номер'.",
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

            if (evaluationContext.Candidate.Uin != null &&
                !string.IsNullOrWhiteSpace(evaluationContext.Candidate.Uin))
            {
                value = evaluationContext.Candidate.Uin;
            }

            args.Result = value;
        }
    }
}
