using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Components;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Components.Communicators;

namespace Eumis.Portal.Web.Controllers
{
    public partial class DefaultController : BaseController
    {
        private IGuidancesCommunicator _guidancesCommunicator;

        public DefaultController(IGuidancesCommunicator guidancesCommunicator)
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
                return View(_guidancesCommunicator.GetGuidances(Eumis.Documents.Enums.GuidanceModuleNomenclature.ApplicationsPortal.Id));
            }
            catch
            {
                return View();
            }
        }

        public virtual ActionResult ManualDownload(Guid id)
        {
            var applicationsPortalGuidances = _guidancesCommunicator.GetGuidances(Eumis.Documents.Enums.GuidanceModuleNomenclature.ApplicationsPortal.Id);

            if (!applicationsPortalGuidances.Where(g => g.FileKey == id).Any())
            {
                return new HttpNotFoundResult();
            }

            return this.DownloadFile(id);
        }

        public virtual ActionResult PublicManualDownload(Guid id)
        {
            var publicPortalGuidances = _guidancesCommunicator.GetGuidances(Eumis.Documents.Enums.GuidanceModuleNomenclature.PublicPortal.Id);

            if (!publicPortalGuidances.Where(g => g.FileKey == id).Any())
            {
                return new HttpNotFoundResult();
            }

            return this.DownloadFile(id);
        }
    }
}