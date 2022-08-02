using System.Web.Http;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.News.PortalViewObjects;
using Eumis.Data.News.Repositories;
using Eumis.Data.Users.Repositories;

namespace Eumis.PortalIntegration.Api.Portal.News.Controllers
{
    [RoutePrefix("api/news")]
    public class NewsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private INewsRepository newsRepository;
        private IUsersRepository usersRepository;

        public NewsController(
            IUnitOfWork unitOfWork,
            INewsRepository newsRepository,
            IUsersRepository usersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.newsRepository = newsRepository;
            this.usersRepository = usersRepository;
        }

        [AllowAnonymous]
        [Route("active")]
        public PagePVO<NewsPVO> GetPortalNews(int offset = 0, int? limit = null)
        {
            return this.newsRepository.GetPortalNews(false, offset, limit);
        }

        [AllowAnonymous]
        [Route("all")]
        public PagePVO<NewsPVO> GetAllPortalNews(int offset = 0, int? limit = null)
        {
            return this.newsRepository.GetPortalNews(true, offset, limit);
        }

        [AllowAnonymous]
        [Route("{id:int}/info")]
        public NewsPVO GetPortalNewsInfo(int id)
        {
            var news = this.newsRepository.Find(id);

            return new NewsPVO(news);
        }
    }
}
