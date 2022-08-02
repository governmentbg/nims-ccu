using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Components;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Common.Resources;
using Eumis.Documents;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class HelpController : BaseController
    {
        public HelpController() { }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}