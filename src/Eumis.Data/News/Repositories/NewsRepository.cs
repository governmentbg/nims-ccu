using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Data.News.PortalViewObjects;
using Eumis.Data.News.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Users;

namespace Eumis.Data.News.Repositories
{
    internal class NewsRepository : AggregateRepository<Eumis.Domain.News>, INewsRepository
    {
        public NewsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Eumis.Domain.News, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Eumis.Domain.News, object>>[]
                {
                    c => c.NewsFiles,
                };
            }
        }

        public IList<NewsVO> GetNews(
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            NewsType? type = null,
            NewsStatus? status = null)
        {
            var predicate = PredicateBuilder.True<Eumis.Domain.News>();

            if (dateTo.HasValue)
            {
                dateTo = dateTo.Value.AddDays(1).AddMilliseconds(-1);
            }

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(n => n.DateFrom, dateFrom)
                .AndDateTimeLessThanOrEqual(n => n.DateTo, dateTo)
                .AndEquals(n => n.Type, type)
                .AndEquals(n => n.Status, status);

            return (from n in this.unitOfWork.DbContext.Set<Eumis.Domain.News>().Where(predicate)
                    join u in this.unitOfWork.DbContext.Set<User>() on n.CreatedByUserId equals u.UserId
                    orderby n.CreateDate descending
                    select new
                    {
                        n.NewsId,
                        n.Type,
                        n.Status,
                        n.DateFrom,
                        n.DateTo,
                        n.Title,
                        u.Username,
                        u.Fullname,
                        n.CreateDate,
                    })
                    .ToList()
                    .Select(o => new NewsVO
                    {
                        NewsId = o.NewsId,
                        Type = o.Type,
                        Status = o.Status,
                        DateFrom = o.Type == NewsType.Internal ? (o.Status == NewsStatus.Draft ? null : o.DateFrom) : o.DateFrom,
                        DateTo = o.Type == NewsType.Internal ? (o.Status == NewsStatus.Draft ? null : o.DateTo) : o.DateTo,
                        Title = o.Title,
                        Creator = o.Fullname + "(" + o.Username + ")",
                        CreateDate = o.CreateDate,
                    })
                    .ToList();
        }

        public PagePVO<NewsPVO> GetPortalNews(bool showAll, int offset = 0, int? limit = null)
        {
            DateTime currentDate = DateTime.Now;

            var predicate = PredicateBuilder.True<Eumis.Domain.News>()
                .AndDateTimeLessThanOrEqual(n => n.DateFrom, currentDate)
                .AndEquals(n => n.Type, NewsType.Portal);

            if (!showAll)
            {
                predicate = predicate
                    .AndDateTimeGreaterThanOrEqual(n => n.DateTo, currentDate)
                    .AndEquals(n => n.Status, NewsStatus.Published);
            }
            else
            {
                predicate = predicate
                    .And(n => n.Status != NewsStatus.Draft);
            }

            var query = from n in this.unitOfWork.DbContext.Set<Eumis.Domain.News>().Where(predicate)
                        join u in this.unitOfWork.DbContext.Set<User>() on n.CreatedByUserId equals u.UserId
                        select new NewsPVO
                        {
                            NewsId = n.NewsId,
                            Status = n.Status,
                            StatusText = n.Status,
                            DateFrom = n.DateFrom,
                            DateTo = n.DateTo,
                            Title = n.Title,
                            Content = n.Content.Length > 300 ? n.Content.Substring(0, 300) + "..." : n.Content,
                            TitleAlt = n.TitleAlt,
                            ContentAlt = n.ContentAlt.Length > 300 ? n.ContentAlt.Substring(0, 300) + "..." : n.ContentAlt,
                            Author = n.Author,
                            AuthorAlt = n.AuthorAlt,
                            CreateDate = n.CreateDate,
                        };

            return new PagePVO<NewsPVO>
            {
                Results = query
                    .OrderByDescending(t => t.DateFrom)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList(),
                Count = query.Count(),
            };
        }

        public IList<NewsFeedVO> GetNewsFeed()
        {
            var currentDate = DateTime.Now;

            var predicate = PredicateBuilder.True<Eumis.Domain.News>()
                .And(n => n.Status == NewsStatus.Published)
                .And(n => n.DateFrom.HasValue && n.DateTo.HasValue && n.DateFrom <= currentDate && n.DateTo >= currentDate);

            return (from n in this.unitOfWork.DbContext.Set<Eumis.Domain.News>().Where(predicate)
                    join u in this.unitOfWork.DbContext.Set<User>() on n.CreatedByUserId equals u.UserId
                    where n.Type == NewsType.Internal
                    orderby n.DateFrom descending
                    select new NewsFeedVO
                    {
                        NewsId = n.NewsId,
                        DateFrom = n.DateFrom,
                        DateTo = n.DateTo,
                        Title = n.Title,
                        Content = n.Content,
                        Creator = u.Fullname,
                    })
                    .ToList();
        }

        public AllNewsVO GetAllNews(int offset, int limit)
        {
            var currentDate = DateTime.Now;

            var predicate = PredicateBuilder.True<Eumis.Domain.News>()
                .And(n => n.Status == NewsStatus.Published);

            var query = from n in this.unitOfWork.DbContext.Set<Eumis.Domain.News>().Where(predicate)
                        join u in this.unitOfWork.DbContext.Set<User>() on n.CreatedByUserId equals u.UserId
                        where n.Type == NewsType.Internal
                        orderby n.DateFrom descending
                        select new NewsFeedVO
                        {
                            NewsId = n.NewsId,
                            DateFrom = n.DateFrom,
                            DateTo = n.DateTo,
                            Title = n.Title,
                            Content = n.Content,
                            Creator = u.Fullname,
                        };

            return new AllNewsVO
            {
                Count = query.Count(),
                News = query.WithOffsetAndLimit(offset, limit).ToList(),
            };
        }

        public Eumis.Domain.News FindPublished(int newsId)
        {
            var result = this.Find(newsId);

            if (result.Status != NewsStatus.Published)
            {
                throw new InvalidOperationException("Invalid status");
            }

            return result;
        }

        public bool IsNewsFileVisible(int newsId, Guid fileKey)
        {
            return (from n in this.unitOfWork.DbContext.Set<Domain.News>()
                    join nf in this.unitOfWork.DbContext.Set<Domain.NewsFile>() on n.NewsId equals nf.NewsId
                    where n.NewsId == newsId && nf.BlobKey == fileKey && n.Status == NewsStatus.Published
                    select nf.NewsFileId).Any();
        }

        public new void Remove(Domain.News news)
        {
            if (news.Status != NewsStatus.Draft)
            {
                throw new InvalidOperationException("Cannot delete nondraft news.");
            }

            base.Remove(news);
        }
    }
}
