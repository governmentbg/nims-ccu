using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Eumis.Common.Localization;
using Eumis.Common.Resources;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using System.Security.Claims;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Areas.Private;
using Eumis.Portal.Web.Helpers;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System;
using System.IO;

namespace Eumis.Portal.Web.Controllers.Base
{
    public abstract partial class BaseController : Controller
    {
        public BaseController() { }

        protected override void ExecuteCore()
        {
            SetLocalizationCulture();

            base.ExecuteCore();
        }

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetLocalizationCulture();

            // prevent browser caching
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnActionExecuting(filterContext);
        }

        protected void SetLocalizationCulture()
        {
            var bgLangHeader = SystemLocalization.GetDefaultCulture();

            if (SystemLocalization.IsLangaugePage(RouteData))
            {
                if (RouteData.Values["lang"] != null && !string.IsNullOrWhiteSpace(RouteData.Values["lang"].ToString()))
                {
                    var lang = RouteData.Values["lang"].ToString();
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(bgLangHeader);
                    RouteData.Values["lang"] = bgLangHeader;
                }
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(bgLangHeader);
            }

            SystemLocalization.Culture = Thread.CurrentThread.CurrentUICulture;
        }

        protected ActionResult DownloadFile(Guid fileKey)
        {
            string accessToken = BlobApi.GetAccessToken(fileKey);

            return Redirect(BlobApi.CreateRedirectUri(fileKey, accessToken).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="name">File name without extension</param>
        /// <returns></returns>
        protected ActionResult DownloadXlsxFile(Stream fileStream, string name)
        {
            fileStream.Position = 0;
            var fileDownloadName = name + ".xlsx";
            var contentType = MimeMapping.GetMimeMapping(fileDownloadName);
            return File(fileStream, contentType, fileDownloadName);
        }

        private EumisUser _currentUser;
        protected EumisUser CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    ClaimsIdentity ci = this.User.Identity as ClaimsIdentity;
                    _currentUser = EumisUserManager.LoadUser(ci);
                }
                return _currentUser;
            }
        }

        protected Eumis.Documents.Contracts.ContractBFPContractMetadata ContractMetadata
        {
            get
            {
                return DependencyResolver.Current.GetService<IBFPContractCommunicator>()
                            .GetContractMetadata(CurrentUser.AccessToken,
                                new Guid(this.RouteData.Values["cgid"].ToString()));
            }
        }
    }
}