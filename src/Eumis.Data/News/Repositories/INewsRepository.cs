using System;
using System.Collections.Generic;
using Eumis.Data.Core;
using Eumis.Data.News.PortalViewObjects;
using Eumis.Data.News.ViewObjects;
using Eumis.Domain;

namespace Eumis.Data.News.Repositories
{
    public interface INewsRepository : IAggregateRepository<Eumis.Domain.News>
    {
        IList<NewsVO> GetNews(
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            NewsType? type = null,
            NewsStatus? status = null);

        PagePVO<NewsPVO> GetPortalNews(bool showAll, int offset = 0, int? limit = null);

        IList<NewsFeedVO> GetNewsFeed();

        AllNewsVO GetAllNews(int offset, int limit);

        Eumis.Domain.News FindPublished(int newsId);

        bool IsNewsFileVisible(int newsId, Guid fileKey);
    }
}
