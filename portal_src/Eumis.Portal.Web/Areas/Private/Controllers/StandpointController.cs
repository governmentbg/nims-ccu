using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Models;
using Eumis.Portal.Web.Models.Standpoint;
using R_10027;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Eumis.Portal.Web.Helpers.Attributes;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class StandpointController : WorkflowController<EditVM>
    {
        private IStandpointCommunicator _standpointCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public StandpointController(IStandpointCommunicator standpointCommunicator, IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _standpointCommunicator = standpointCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutCommunication();

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
            PutCommunication();
            _standpointCommunicator.SubmitEvalSessionStandpointXml
                (
                    AppContext.Current.WorkingDocument.token,
                    AppContext.Current.WorkingDocument.gid,
                    AppContext.Current.WorkingDocument.version
                );

            return View(MVC.Private.Shared.Views.Success, MVC.Private.Shared.Views._Layout, (object)Global.SuccessSave);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            PutCommunication();

            base.SaveDraft();
        }

        private void PutCommunication()
        {
            AppContext.Current.WorkingDocument.version =
                _standpointCommunicator.UpdateEvalSessionStandpointXml
                    (
                        AppContext.Current.WorkingDocument.token,    
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version
                    ).version;
        }

        private R_10027.Standpoint InitAppContext(Guid gid, string access_token)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.StandpointMetadata.Code);

            var contractStandpoint = _standpointCommunicator.GetEvalSessionStandpointXml(access_token, gid);

            var standpoint = _documentSerializer.XmlDeserializeFromString<R_10027.Standpoint>(contractStandpoint.xml);

            AppContext.Current.Document = standpoint;
            AppContext.Current.Xml = contractStandpoint.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractStandpoint.version
            };

            return standpoint;
        }
    }
}