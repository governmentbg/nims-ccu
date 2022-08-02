using Eumis.Common.Localization;
using Eumis.Documents.News;
using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class NewsPVO
    {
        public IList<News> results { get; set; }

        public int count { get; set; }
    }

    public class News
    {
        public int newsId { get; set; }

        public string title { get; set; }

        public string titleAlt { get; set; }

        public string displayTitle
        {
            get
            {
                return SystemLocalization.GetDisplayName(title, titleAlt);
            }
        }

        public string content { get; set; }

        public string contentAlt { get; set; }

        public string displayContent
        {
            get
            {
                return SystemLocalization.GetDisplayName(content, contentAlt);
            }
        }

        public string statusText { get; set; }

        public string statusTextAlt { get; set; }

        public string displayStatusText
        {
            get
            {
                if (dateTo.HasValue && dateTo.Value < DateTime.Now)
                {
                    return SystemLocalization.GetDisplayName("изтекла", "expired");
                }
                else
                {
                    return SystemLocalization.GetDisplayName(statusText, statusTextAlt);
                }
            }
        }

        public bool isPublished
        {
            get
            {
                return statusTextAlt == "Published";
            }
        }

        public bool isExpired
        {
            get
            {
                return dateTo.HasValue && dateTo.Value < DateTime.Now;
            }
        }

        public DateTime? dateFrom { get; set; }

        public DateTime? dateTo { get; set; }

        public string author { get; set; }

        public string authorAlt { get; set; }

        public string displayAuthor
        {
            get
            {
                return SystemLocalization.GetDisplayName(author, authorAlt);
            }
        }

        public DateTime createDate { get; set; }

        public IList<NewsFilePVO> Files { get; set; }
    }
}
