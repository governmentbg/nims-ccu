using System;
using Eumis.Common.Json;
using Eumis.Domain;
using Newtonsoft.Json;

namespace Eumis.Data.News.ViewObjects
{
    public class NewsVO
    {
        public int NewsId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public NewsType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public NewsStatus Status { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Title { get; set; }

        public string Creator { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
