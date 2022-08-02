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
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    public partial class HelpController : BaseController
    {
        protected IDocumentSerializer _documentSerializer;

        public HelpController() { }

        public HelpController(IDocumentSerializer documentSerializer)
        {
            _documentSerializer = documentSerializer;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Preview()
        {
            var isun = System.IO.File.ReadAllBytes(Server.MapPath(@"~/docs/help.suni"));

            string xml = ZipManager.UnzipProject(new MemoryStream(isun));

            R_10019.Project project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(xml);

            AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);
            AppContext.Current.Document = project;
            AppContext.Current.Xml = xml;

            return RedirectToAction(MVC.Project.ActionNames.Preview, MVC.Project.Name);
        }
    }
}