using System;
using Eumis.Common;
using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Data.Core.Nomenclatures
{
    public class EnumNomVO<TEnum> : IEquatable<EnumNomVO<TEnum>>
        where TEnum : struct, IConvertible
    {
        private TEnum e;

        public EnumNomVO(TEnum e)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            this.e = e;
        }

        public TEnum NomValueId
        {
            get { return this.e; }
        }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public TEnum Name
        {
            get { return this.e; }
        }

        public int OrderNum
        {
            get
            {
                Enum enumVal = this.e as Enum;
                return enumVal.GetOrderNum();
            }
        }

        public override int GetHashCode()
        {
            return this.e.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((EnumNomVO<TEnum>)obj);
        }

        public bool Equals(EnumNomVO<TEnum> other)
        {
            return other.e.Equals(this.e);
        }
    }
}
