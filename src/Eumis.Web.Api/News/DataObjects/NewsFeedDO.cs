using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common;
using Eumis.Domain;

namespace Eumis.Web.Api.News.DataObjects
{
    public class NewsFeedDO
    {
        public NewsFeedDO(Eumis.Domain.News news, string createdByUser)
        {
            this.NewsId = news.NewsId;
            this.Type = news.Type;
            this.Status = news.Status;
            this.DateFrom = news.DateFrom;
            this.DateTo = news.DateTo;
            this.Title = news.Title;
            this.TitleAlt = news.TitleAlt;
            this.Content = news.Content.MakeHtml();
            this.ContentAlt = news.ContentAlt.MakeHtml();
            this.CreatedByUser = createdByUser;
            this.CreateDate = news.CreateDate;
            this.Files = news.NewsFiles.Select(f => new NewsFileDO(f)).ToList();
        }

        public int? NewsId { get; set; }

        public NewsType? Type { get; set; }

        public NewsStatus? Status { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Title { get; set; }

        public string TitleAlt { get; set; }

        public string Content { get; set; }

        public string ContentAlt { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreateDate { get; set; }

        public IList<NewsFileDO> Files { get; set; }
    }
}
