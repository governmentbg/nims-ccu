using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Regix.Helpers.Bulstat
{
    public static class CountryMap
    {
        private static readonly Dictionary<Country, int> Map = new Dictionary<Country, int>
        {
            { Country.Bulgaria, Domain.NonAggregates.Country.ID_BG },
        };

        public static int? GetCountry(Country country)
        {
            if (Map.ContainsKey(country))
            {
                return Map[country];
            }

            return null;
        }

        public static int GetCountryIdFromCode(string code)
        {
            try
            {
                int elementId = int.Parse(code);
                var element = (Country)elementId;
                return CountryMap.GetCountry(element) ?? Domain.NonAggregates.Country.ID_BG;
            }
            catch
            {
            }

            return Domain.NonAggregates.Country.ID_BG;
        }
    }
}
