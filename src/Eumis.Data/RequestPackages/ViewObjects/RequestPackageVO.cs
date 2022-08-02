using Eumis.Common.Json;
using Eumis.Domain.RequestPackages;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.RequestPackages.ViewObjects
{
    public class RequestPackageVO
    {
        public int RequestPackageId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public RequestPackageType Type { get; set; }

        public string Code { get; set; }

        public DateTime CreateDate { get; set; }

        public string Organization { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public RequestPackageStatus Status { get; set; }

        public int RequestPackageUsersCount { get; set; }
    }
}
