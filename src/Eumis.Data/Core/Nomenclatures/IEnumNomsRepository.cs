using System;
using System.Collections.Generic;

namespace Eumis.Data.Core.Nomenclatures
{
    public interface IEnumNomsRepository<TEnum>
        where TEnum : struct, IConvertible
    {
        EnumNomVO<TEnum> GetNom(TEnum e);

        IList<EnumNomVO<TEnum>> GetNoms(string term);
    }
}
