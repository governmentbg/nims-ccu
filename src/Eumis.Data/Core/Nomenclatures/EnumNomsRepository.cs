using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Core.Nomenclatures
{
    internal class EnumNomsRepository<TEnum> : IEnumNomsRepository<TEnum>
        where TEnum : struct, IConvertible
    {
        public EnumNomsRepository()
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
        }

        public EnumNomVO<TEnum> GetNom(TEnum e)
        {
            return new EnumNomVO<TEnum>(e);
        }

        public IList<EnumNomVO<TEnum>> GetNoms(string term)
        {
            // TODO: Implement search by term and order by description
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new EnumNomVO<TEnum>(e))
                .OrderBy(e => e.OrderNum)
                .ToList();
        }
    }
}
