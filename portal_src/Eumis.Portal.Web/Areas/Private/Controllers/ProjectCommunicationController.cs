using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Documents;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Models.ProjectCommunication;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using R_10020;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Documents.Enums;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class ProjectCommunicationController : WorkflowController<EditVM>
    {
        private IProjectCommunicationCommunicator _projectCommunicationCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public ProjectCommunicationController(
            IProjectCommunicationCommunicator projectCommunicationCommunicator,
            IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _projectCommunicationCommunicator = projectCommunicationCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutProjectCommunication();

            return base.Prepare(vm);
        }

        [HttpGet]
        [Route("edit")]
        [AllowAnonymous]
        public virtual ActionResult Edit(Guid gid, string access_token)
        {
            this.InitAppContext(gid, access_token, true);

            return RedirectToAction("Prepare");
        }

        [HttpGet]
        [Route("preview")]
        [AllowAnonymous]
        public virtual ActionResult Preview(Guid gid, string access_token)
        {
            return View(this.InitAppContext(gid, access_token, false));
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            _projectCommunicationCommunicator.PrivateSubmitProjectCommunication
                (
                    AppContext.Current.WorkingDocument.gid,
                    AppContext.Current.WorkingDocument.token,
                    AppContext.Current.Xml,
                    AppContext.Current.WorkingDocument.version
                );

            return View(MVC.Private.Shared.Views.Success, MVC.Private.Shared.Views._Layout, (object)Web.Views.Shared.App_LocalResources.Message.Sent);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            PutProjectCommunication();

            base.SaveDraft();
        }

        private void PutProjectCommunication()
        {
            var projectCommunication = ((R_10020.Message)AppContext.Current.Document);

            AppContext.Current.WorkingDocument.version =
                _projectCommunicationCommunicator.PrivatePutProjectCommunication
                    (
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version,
                        projectCommunication?.Subject?.id
                    ).version;
        }

        private Message InitAppContext(Guid gid, string access_token, bool isEdit)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            var contractMessage = _projectCommunicationCommunicator.PrivateGetProjectCommunication(gid, access_token);

            var message = _documentSerializer.XmlDeserializeFromString<Message>(contractMessage.xml);

            message.IsManagingAuthority = true;

            message.MessageHeader = contractMessage.regData;
            message.CompanyName = contractMessage.companyDisplayName;

            AppContext.Current.Document = message;
            AppContext.Current.Xml = contractMessage.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractMessage.version
            };

            return message;
        }
    }
}
