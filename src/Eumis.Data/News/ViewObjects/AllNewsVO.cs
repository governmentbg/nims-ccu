using System.Collections.Generic;

namespace Eumis.Data.News.ViewObjects
{
    public class AllNewsVO
    {
        public int Count { get; set; }

        public IList<NewsFeedVO> News { get; set; }
    }
}
