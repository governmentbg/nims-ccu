using System.Collections.Generic;
using System.Linq;

namespace Eumis.Database.Configurator
{
    internal static class DictionaryExtensions
    {
        public static int? GetNomId(this Dictionary<int, string> nomDictionary, string code)
        {
            int? returnValue = null;

            if (!string.IsNullOrWhiteSpace(code))
            {
                returnValue = nomDictionary.Single(e => e.Value.ToLower() == code.ToLower()).Key;
            }

            return returnValue;
        }
    }
}
