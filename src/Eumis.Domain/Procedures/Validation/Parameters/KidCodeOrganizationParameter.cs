using Eumis.Domain.Procedures.Validation.Abstract;
using NCalc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class KidCodeOrganizationParameter : CustomNCalcExpressionParameter
    {
        private static readonly List<string> ParameterAliases = new List<string>
        {
            "Код на организацията по КИД 2008",
            "Kid code organization",
        };

        public KidCodeOrganizationParameter()
        {
            this.InitFields(
                string.Format(
                    "Параметър 'Код на организацията по КИД 2008'. Връща стойноста полето 'Код на организацията по КИД 2008'. (Формат: {0})",
                    GetParameterAliasDescription()),
                "Параметър 'Код на организацията по КИД 2008'.",
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

            if (evaluationContext.Candidate.KidCodeOrganization != null &&
                !string.IsNullOrWhiteSpace(evaluationContext.Candidate.KidCodeOrganization.Code))
            {
                value = evaluationContext.Candidate.KidCodeOrganization.Code;
            }

            args.Result = value;
        }
    }
}
