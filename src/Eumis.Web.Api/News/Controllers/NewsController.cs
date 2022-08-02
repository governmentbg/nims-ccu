using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.News.Repositories;
using Eumis.Data.News.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Domain;
using Eumis.Domain.Emails;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.News.DataObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.News.Controllers
{
    [RoutePrefix("api/news")]
    public class NewsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private INewsRepository newsRepository;
        private IUsersRepository usersRepository;
        private IAuthorizer authorizer;
        private UserClaimsContextFactory userClaimsContextFactory;
        private IAccessContext accessContext;
        private IUserClaimsContext currentUserClaimsContext;

        public NewsController(
            IUnitOfWork unitOfWork,
            INewsRepository newsRepository,
            IUsersRepository usersRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            UserClaimsContextFactory userClaimsContextFactory)
        {
            this.unitOfWork = unitOfWork;
            this.newsRepository = newsRepository;
            this.usersRepository = usersRepository;
            this.authorizer = authorizer;
            this.userClaimsContextFactory = userClaimsContextFactory;
            this.accessContext = accessContext;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("")]
        public IList<NewsVO> GetNewsQuery(
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            NewsType? type = null,
            NewsStatus? status = null)
        {
            this.authorizer.AssertCanDo(NewsListActions.Search);

            return this.newsRepository.GetNews(dateFrom, dateTo, type, status);
        }

        [Route("~/api/newsFeed")]
        public IList<NewsFeedVO> GetNewsFeedQuery()
        {
            return this.newsRepository.GetNewsFeed();
        }

        [Route("~/api/allNews")]
        public AllNewsVO GetAllNewsQuery(int offset, int limit)
        {
            return this.newsRepository.GetAllNews(offset, limit);
        }

        [Route("{newsId:int}")]
        public NewsDO GetNews(int newsId)
        {
            this.authorizer.AssertCanDo(NewsActions.View, newsId);

            var news = this.newsRepository.Find(newsId);

            var userClaimsContext = this.userClaimsContextFactory(news.CreatedByUserId);
            var username = userClaimsContext.Fullname + "(" + userClaimsContext.Username + ")";

            return new NewsDO(news, username);
        }

        [Route("~/api/newsFeed/{newsId:int}")]
        public NewsFeedDO GetNewsFeed(int newsId)
        {
            var news = this.newsRepository.FindPublished(newsId);
            var userClaimsContext = this.userClaimsContextFactory(news.CreatedByUserId);

            return new NewsFeedDO(news, userClaimsContext.Fullname);
        }

        [HttpPut]
        [Route("{newsId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.News.Edit), IdParam = "newsId")]
        public void UpdateNews(int newsId, NewsDO news)
        {
            this.authorizer.AssertCanDo(NewsActions.Edit, newsId);

            var oldNews = this.newsRepository.FindForUpdate(newsId, news.Version);

            if (oldNews.Type == NewsType.Internal)
            {
                oldNews.UpdateAttributes(news.Title, news.Content, news.EmailNotification);
            }
            else
            {
                oldNews.UpdateAttributes(news.Title, news.TitleAlt, news.Content, news.ContentAlt, news.Author, news.AuthorAlt, news.DateFrom.Value, news.DateTo.Value);
            }

            var newFiles = news.Files
                .Where(f => f.Status == FileStatus.Added)
                .Select(f => new NewsFile { BlobKey = f.File.Key, Name = f.File.Name, Description = f.Description })
                .ToList();
            oldNews.AddFiles(newFiles);

            var updatedFiles = news.Files
                .Where(f => f.Status == FileStatus.Updated)
                .Select(f => Tuple.Create<int, Guid, string, string>(f.NewsFileId.Value, f.File.Key, f.File.Name, f.Description))
                .ToList();
            oldNews.UpdateFiles(updatedFiles);

            var removedFileIds = news.Files
                .Where(f => f.Status == FileStatus.Removed)
                .Select(f => f.NewsFileId.Value)
                .ToList();
            oldNews.RemoveFiles(removedFileIds);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public NewsDO NewNews()
        {
            this.authorizer.AssertCanDo(NewsListActions.Create);

            var username = this.currentUserClaimsContext.Fullname + "(" + this.currentUserClaimsContext.Username + ")";

            return new NewsDO(username);
        }

        [HttpGet]
        [Route("newFile")]
        public NewsFileDO NewFile()
        {
            this.authorizer.AssertCanDo(NewsListActions.Create);

            return new NewsFileDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.News.Create))]
        public object CreateNews(NewsDO news)
        {
            this.authorizer.AssertCanDo(NewsListActions.Create);

            Domain.News newNews = null;

            if (news.Type == NewsType.Internal)
            {
                newNews = new Domain.News(
                    news.Title,
                    news.Content,
                    news.EmailNotification,
                    news.Files.Select(f => new NewsFile
                    {
                        BlobKey = f.File.Key,
                        Name = f.File.Name,
                        Description = f.Description,
                    }).ToList(),
                    this.accessContext.UserId);

                this.newsRepository.Add(newNews);
            }
            else
            {
                newNews = new Domain.News(
                    news.Title,
                    news.TitleAlt,
                    news.Content,
                    news.ContentAlt,
                    news.Author,
                    news.AuthorAlt,
                    news.DateFrom.Value,
                    news.DateTo.Value,
                    news.Files.Select(f => new NewsFile
                    {
                        BlobKey = f.File.Key,
                        Name = f.File.Name,
                        Description = f.Description,
                    }).ToList(),
                    this.accessContext.UserId);

                this.newsRepository.Add(newNews);
            }

            this.unitOfWork.Save();

            return new { NewsId = newNews.NewsId };
        }

        [HttpGet]
        [Route("{newsId:int}/newPublication")]
        public NewsPublishDO NewPublication(int newsId)
        {
            this.authorizer.AssertCanDo(NewsActions.Edit, newsId);

            var news = this.newsRepository.Find(newsId);

            return new NewsPublishDO(news);
        }

        [HttpPost]
        [Route("{newsId:int}/publish")]
        [ActionLog(Action = typeof(ActionLogGroups.News.ChangeStatusToPublished), IdParam = "newsId")]
        public void PublishNews(int newsId, NewsPublishDO publishDO)
        {
            this.authorizer.AssertCanDo(NewsActions.Edit, newsId);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var news = this.newsRepository.FindForUpdate(newsId, publishDO.Version);

                if (news.Type == NewsType.Internal)
                {
                    news.Publish(
                        publishDO.DateFrom.Value,
                        publishDO.DateTo.Value);

                    this.unitOfWork.Save();

                    if (news.EmailNotification)
                    {
                        var publisher = this.usersRepository.Find(this.accessContext.UserId);
                        var recipients = this.usersRepository.GetActiveUsersEmails()
                            .Where(em => !string.IsNullOrWhiteSpace(em) && em != publisher.Email);
                        var context = JObject.FromObject(
                            new
                            {
                                title = news.Title,
                                content = news.Content.MakeHtml(),
                                sender = publisher.Fullname,
                                newsId = news.NewsId,
                            });

                        var emails = recipients.Select(re => new Email(re, "NewsPublishedMessage", context));

                        this.unitOfWork.BulkInsert<Email>(emails);

                        this.unitOfWork.Save();
                    }
                }
                else
                {
                    news.ChangeStatusToPublished();

                    this.unitOfWork.Save();
                }

                transaction.Commit();
            }
        }

        [HttpPost]
        [Route("{newsId:int}/archive")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.News.ChangeStatusToArchived), IdParam = "newsId")]
        public void ArchiveNews(int newsId, string version)
        {
            this.authorizer.AssertCanDo(NewsActions.Edit, newsId);

            byte[] vers = System.Convert.FromBase64String(version);
            var news = this.newsRepository.FindForUpdate(newsId, vers);

            news.Archive();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{newsId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.News.ChangeStatusToDraft), IdParam = "newsId")]
        public void ChangeStatusToDraft(int newsId, string version)
        {
            this.authorizer.AssertCanDo(NewsActions.Edit, newsId);

            byte[] vers = System.Convert.FromBase64String(version);
            var news = this.newsRepository.FindForUpdate(newsId, vers);

            news.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{newsId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.News.Delete), IdParam = "newsId")]
        public void DeleteNews(int newsId, string version)
        {
            this.authorizer.AssertCanDo(NewsActions.Edit, newsId);

            byte[] vers = System.Convert.FromBase64String(version);
            var news = this.newsRepository.FindForUpdate(newsId, vers);

            this.newsRepository.Remove(news);

            this.unitOfWork.Save();
        }
    }
}
