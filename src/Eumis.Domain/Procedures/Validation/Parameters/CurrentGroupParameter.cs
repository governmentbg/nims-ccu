using Eumis.Domain.Procedures.Validation.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class CurrentGroupParameter : CustomNCalcExpressionParameter
    {
        private static readonly List<string> ParameterAliases = new List<string>
        {
            "Група",
            "Group",
        };

        public CurrentGroupParameter()
        {
            this.InitFields(
                string.Format(
                    "Параметър група. Връща детайлите на бюджета, които принадлежат на текущата група. (Формат: '[{0}]')",
                    GetParameterAliasDescription()),
                "Параметър група",
                NCalcType.NCalcExpressionParameter,
                false,
                null,
                null,
                null,
                null);
        }

        public static bool MatchParameterAlias(string parameterName)
        {
            return ParameterAliases.Any(e => e.ToLower().Trim().Replace(" ", string.Empty) == parameterName.ToLower().Trim().Replace(" ", string.Empty));
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

        public static Dictionary<string, List<bool>> GetContextValue(string levelName, ProcedureProgramme procedureProgrammeContext, NCalcEvaluationContext evaluationContext)
        {
            Dictionary<string, List<bool>> dictionary = new Dictionary<string, List<bool>>();

            List<bool> value = SubTreeParameter.GetContextValue(levelName, procedureProgrammeContext, evaluationContext);

            foreach (var alias in ParameterAliases)
            {
                dictionary.Add(alias, value);
            }

            return dictionary;
        }

        public override bool MatchName(string parameterName)
        {
            return MatchParameterAlias(parameterName);
        }
    }
}
