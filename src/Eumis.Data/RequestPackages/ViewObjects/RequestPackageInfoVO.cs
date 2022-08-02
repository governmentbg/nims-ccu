using Eumis.Common.Json;
using Eumis.Domain.RequestPackages;
using Newtonsoft.Json;

namespace Eumis.Data.RequestPackages.ViewObjects
{
    public class RequestPackageInfoVO
    {
        public string Code { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public RequestPackageType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public RequestPackageStatus Status { get; set; }
    }
}
