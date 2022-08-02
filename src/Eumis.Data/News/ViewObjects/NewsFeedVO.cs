using System;

namespace Eumis.Data.News.ViewObjects
{
    public class NewsFeedVO
    {
        public int NewsId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Creator { get; set; }
    }
}
