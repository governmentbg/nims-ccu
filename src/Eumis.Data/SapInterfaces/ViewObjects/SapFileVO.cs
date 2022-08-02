using System;
using Eumis.Common.Json;
using Eumis.Domain.SapInterfaces;
using Newtonsoft.Json;

namespace Eumis.Data.SapInterfaces.ViewObjects
{
    public class SapFileVO
    {
        public int SapFileId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SapFileStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SapFileType Type { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
