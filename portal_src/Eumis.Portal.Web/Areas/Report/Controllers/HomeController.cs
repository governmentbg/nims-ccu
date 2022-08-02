using Eumis.Components.Communicators;
using Eumis.Portal.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class HomeController : BaseController
    {
        private IGuidancesCommunicator _guidancesCommunicator;

        public HomeController(IGuidancesCommunicator guidancesCommunicator)
        {
            _guidancesCommunicator = guidancesCommunicator;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult AccessibilityPolicy()
        {
            return View();
        }

        public virtual ActionResult Glossary()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            return View();
        }

        public virtual ActionResult Manual()
        {
            try
            {
                return View(_guidancesCommunicator.GetGuidances(Eumis.Documents.Enums.GuidanceModuleNomenclature.ReportsPortal.Id));
            }
            catch
            {
                return View();
            }
        }

        public virtual ActionResult ManualDownload(Guid id)
        {
            var reportsPortalGuidances = _guidancesCommunicator.GetGuidances(Eumis.Documents.Enums.GuidanceModuleNomenclature.ReportsPortal.Id);

            if (!reportsPortalGuidances.Where(g => g.FileKey == id).Any())
            {
                return new HttpNotFoundResult();
            }

            return this.DownloadFile(id);
        }
    }
}