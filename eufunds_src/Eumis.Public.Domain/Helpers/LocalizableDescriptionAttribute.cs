using System;
using System.ComponentModel;

namespace Eumis.Public.Domain.Helpers
{
    [Serializable]
    public class LocalizableDescriptionAttribute : DescriptionAttribute
    {
        public LocalizableDescriptionAttribute(string resourceKey)
            : base(Resources.Texts.ResourceManager.GetString(Constants.ENUM_RESOURCES_PREFIX + resourceKey))
        { }

    }
}
