using Eumis.Components.Communicators;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Controllers
{
    public partial class PublicController : BaseController
    {
        private INewsCommunicator _newsCommunicator;

        public PublicController(INewsCommunicator newsCommunicator)
        {
            _newsCommunicator = newsCommunicator;
        }

        [AllowAnonymous]
        public virtual ActionResult CurrentNews(int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;

            var news = _newsCommunicator.GetNews(offset, Constants.PAGE_ITEMS_COUNT);

            var model = new StaticPagedList<News>(news.results, page, Constants.PAGE_ITEMS_COUNT, news.count);

            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult AllNews(int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;

            var news = _newsCommunicator.GetAllNews(offset, Constants.PAGE_ITEMS_COUNT);

            var model = new StaticPagedList<News>(news.results, page, Constants.PAGE_ITEMS_COUNT, news.count);

            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult Preview(int id)
        {
            var info = _newsCommunicator.GetNewsInfo(id);

            return View(info);
        }

        [AllowAnonymous]
        public virtual ActionResult PreviewDownload(int id, Guid fileKey)
        {
            var info = _newsCommunicator.GetNewsInfo(id);

            if (!info.Files.Where(f => f.File.Key == fileKey).Any())
            {
                return new HttpNotFoundResult();
            }

            return this.DownloadFile(fileKey);
        }

        [AllowAnonymous]
        public virtual ActionResult AccessibilityPolicy()
        {
            return View();
        }

        [AllowAnonymous]
        public virtual ActionResult Glossary()
        {
            return View();
        }

        [AllowAnonymous]
        public virtual ActionResult About()
        {
            return View();
        }
    }
}
