using Eumis.Common.Json;
using System.Globalization;

namespace Eumis.Common.Localization
{
    public class SpecificEnumDescriptionConverterEn : EnumDescriptionConverter
    {
        public SpecificEnumDescriptionConverterEn()
            : base(new CultureInfo(SystemLocalization.En_GB))
        {
        }
    }
}
