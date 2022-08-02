using Eumis.Common.Json;
using Eumis.Domain.SapInterfaces;
using Newtonsoft.Json;

namespace Eumis.Data.SapInterfaces.ViewObjects
{
    public class SapFileInfoVO
    {
        public int SapFileId { get; set; }

        public SapFileStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SapFileStatus StatusDescr { get; set; }

        public SapFileType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SapFileType TypeDescr { get; set; }

        public string FileName { get; set; }
    }
}
