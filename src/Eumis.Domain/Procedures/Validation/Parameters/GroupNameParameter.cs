using Eumis.Domain.Procedures.Validation.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Procedures.Validation
{
    internal class GroupNameParameter : CustomNCalcExpressionParameter
    {
        private static readonly List<string> ParameterAliases = new List<string>
        {
            "Име на група",
            "Group name",
        };

        public GroupNameParameter()
        {
            this.InitFields(
                string.Format(
                    "Параметър име на група. Връща името на текущата група. (Формат: '{0}')",
                    GetParameterAliasDescription()),
                "Параметър име на група",
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

        public static Dictionary<string, string> GetContextValue(string groupName)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (var alias in ParameterAliases)
            {
                dictionary.Add(alias, groupName);
            }

            return dictionary;
        }

        public override bool MatchName(string parameterName)
        {
            return MatchParameterAlias(parameterName);
        }
    }
}
