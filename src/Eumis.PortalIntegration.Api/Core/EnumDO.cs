using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.PortalIntegration.Api.Core
{
    public class EnumDO<TEnum>
        where TEnum : struct
    {
        [JsonConverter(typeof(EnumDescriptionConverter))]
        public TEnum? Description { get; set; }

        public TEnum Value { get; set; }
    }
}
