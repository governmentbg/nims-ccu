using Eumis.Common.Extensions;
using Eumis.Common.Helpers;
using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models.Draft;
using Ionic.Zip;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Controllers
{
    public partial class DraftController : DraftBaseController
    {
        public DraftController(IDocumentSerializer documentSerializer, IDraftCommunicator draftCommunicator,
            IProcedureCommunicator procedureCommunicator, IProjectCommunicator projectCommunicator)
            : base(documentSerializer, draftCommunicator, procedureCommunicator, projectCommunicator)
        {
        }

        [HttpGet]
        public virtual ActionResult Index(int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var projects = _draftCommunicator.GetDrafts(CurrentUser.AccessToken, Constants.PAGE_ITEMS_COUNT, offset);

            var model = new StaticPagedList<RegProjectXmlPVO>(projects.results, page, Constants.PAGE_ITEMS_COUNT, projects.count);

            return View(model);
        }
    }
}