using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models;
using Eumis.Portal.Web.Models.EvalTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class EvalTableController : WorkflowController<EditVM>
    {
        private IEvalCommunicator _evalCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public EvalTableController(IEvalCommunicator evalCommunicator, IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _evalCommunicator = evalCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutEvalTable();

            return base.Prepare(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Edit(Guid gid, string access_token)
        {
            this.InitAppContext(gid, access_token);

            return RedirectToAction("Prepare");
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Preview(Guid gid, string access_token)
        {
            return View(this.InitAppContext(gid, access_token));
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            PutEvalTable();
            _evalCommunicator.SubmitEvalTable
                (
                    AppContext.Current.WorkingDocument.gid,
                    AppContext.Current.WorkingDocument.token,
                    AppContext.Current.WorkingDocument.version
                );

            return View(MVC.Private.Shared.Views.Success, MVC.Private.Shared.Views._Layout, (object)Global.SuccessSave);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            PutEvalTable();

            base.SaveDraft();
        }

        private void PutEvalTable()
        {
            AppContext.Current.WorkingDocument.version =
                _evalCommunicator.PutEvalTable
                    (
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version
                    ).version;
        }

        private R_10023.EvalTable InitAppContext(Guid gid, string access_token)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.EvalTableMetadata.Code);

            var contractEvalRating = _evalCommunicator.GetEvalTable(gid, access_token);

            var table = _documentSerializer.XmlDeserializeFromString<R_10023.EvalTable>(contractEvalRating.xml);

            table = R_10023.EvalTable.Load(table);

            AppContext.Current.Document = table;
            AppContext.Current.Xml = contractEvalRating.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractEvalRating.version
            };

            return table;
        }
    }
}