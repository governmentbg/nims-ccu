using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.Guidances;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Guidances.ViewObjects
{
    public class GuidanceDataVO
    {
        public int GuidanceId { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public GuidanceModule Module { get; set; }

        public DateTime CreateDate { get; set; }

        public string Creator { get; set; }

        public FileVO File { get; set; }
    }
}
