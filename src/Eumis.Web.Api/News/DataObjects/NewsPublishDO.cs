using System;

namespace Eumis.Web.Api.News.DataObjects
{
    public class NewsPublishDO
    {
        public NewsPublishDO()
        {
        }

        public NewsPublishDO(Domain.News news)
        {
            this.DateFrom = news.DateFrom ?? DateTime.Now.Date;
            this.DateTo = news.DateTo;
            this.Version = news.Version;
        }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public byte[] Version { get; set; }
    }
}
