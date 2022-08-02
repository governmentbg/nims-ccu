using Eumis.Common.Json;
using System.Globalization;

namespace Eumis.Common.Localization
{
    public class SpecificEnumDescriptionConverterBg : EnumDescriptionConverter
    {
        public SpecificEnumDescriptionConverterBg()
            : base(new CultureInfo(SystemLocalization.Bg_BG))
        {
        }
    }
}
