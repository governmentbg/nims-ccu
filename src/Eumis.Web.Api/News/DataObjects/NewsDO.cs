using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain;

namespace Eumis.Web.Api.News.DataObjects
{
    public class NewsDO
    {
        public NewsDO()
        {
            this.Files = new List<NewsFileDO>();
        }

        public NewsDO(string createdByUser)
        {
            var currentDate = DateTime.Now;

            this.Type = NewsType.Internal;
            this.Status = NewsStatus.Draft;
            this.CreatedByUser = createdByUser;
            this.CreateDate = currentDate;
            this.EmailNotification = false;

            this.Files = new List<NewsFileDO>();
        }

        public NewsDO(Eumis.Domain.News news, string createdByUser)
        {
            this.NewsId = news.NewsId;
            this.Type = news.Type;
            this.Status = news.Status;
            this.DateFrom = news.DateFrom;
            this.DateTo = news.DateTo;
            this.Title = news.Title;
            this.TitleAlt = news.TitleAlt;
            this.Content = news.Content;
            this.ContentAlt = news.ContentAlt;
            this.EmailNotification = news.EmailNotification;
            this.Author = news.Author;
            this.AuthorAlt = news.AuthorAlt;
            this.CreatedByUser = createdByUser;
            this.CreateDate = news.CreateDate;
            this.Version = news.Version;
            this.Files = news.NewsFiles.Select(f => new NewsFileDO(f)).ToList();
        }

        public int? NewsId { get; set; }

        public NewsType Type { get; set; }

        public NewsStatus? Status { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Title { get; set; }

        public string TitleAlt { get; set; }

        public string Content { get; set; }

        public string ContentAlt { get; set; }

        public bool EmailNotification { get; set; }

        public string Author { get; set; }

        public string AuthorAlt { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreateDate { get; set; }

        public byte[] Version { get; set; }

        public IList<NewsFileDO> Files { get; set; }
    }
}
