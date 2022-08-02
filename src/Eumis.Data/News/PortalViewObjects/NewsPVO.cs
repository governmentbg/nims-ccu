using Eumis.Common;
using Eumis.Common.Localization;
using Eumis.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.News.PortalViewObjects
{
    public class NewsPVO
    {
        public NewsPVO()
        {
            this.Files = new List<NewsFilePVO>();
        }

        public NewsPVO(Eumis.Domain.News news)
        {
            this.NewsId = news.NewsId;
            this.Status = news.Status;
            this.StatusText = news.Status;
            this.DateFrom = news.DateFrom;
            this.DateTo = news.DateTo;
            this.Title = news.Title;
            this.TitleAlt = news.TitleAlt;
            this.Content = news.Content.MakeHtml();
            this.ContentAlt = news.ContentAlt.MakeHtml();
            this.Author = news.Author;
            this.AuthorAlt = news.AuthorAlt;
            this.CreateDate = news.CreateDate;
            this.Files = news.NewsFiles.Select(f => new NewsFilePVO(f)).ToList();
        }

        public int NewsId { get; set; }

        public NewsStatus Status { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public NewsStatus StatusText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public NewsStatus StatusTextAlt
        {
            get
            {
                return this.StatusText;
            }
        }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string TitleAlt { get; set; }

        public string ContentAlt { get; set; }

        public string Author { get; set; }

        public string AuthorAlt { get; set; }

        public DateTime CreateDate { get; set; }

        public IList<NewsFilePVO> Files { get; set; }
    }
}
