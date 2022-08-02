using Eumis.Common.Json;
using Eumis.Common.Localization;
using Newtonsoft.Json;

namespace Eumis.Data.Core.Nomenclatures
{
    public class EnumPVO<TEnum>
    {
        [JsonConverter(typeof(EnumDescriptionConverter))]
        public TEnum Description { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public TEnum DescriptionAlt
        {
            get
            {
                return this.Description;
            }
        }

        public TEnum Value { get; set; }
    }
}
