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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Controllers
{
    [Authorize]
    public partial class ArchivedController : DraftBaseController
    {
        public ArchivedController(IDocumentSerializer documentSerializer, IDraftCommunicator draftCommunicator,
            IProcedureCommunicator procedureCommunicator, IProjectCommunicator projectCommunicator)
            : base(documentSerializer, draftCommunicator, procedureCommunicator, projectCommunicator)
        {
        }

        public virtual ActionResult Index()
        {
            return RedirectToAction(MVC.Finalized.ActionNames.Index, MVC.Finalized.Name);
        }
    }
}