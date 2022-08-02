using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Documents;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Models.ProjectCommunicationAnswer;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using R_10020;
using Eumis.Portal.Web.Helpers.Attributes;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class ProjectCommunicationAnswerController : WorkflowController<EditVM>
    {
        private IProjectCommunicationCommunicator _projectCommunicationCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public ProjectCommunicationAnswerController(
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

            this.PutProjectCommunicationAnswer();

            return base.Prepare(vm);
        }

        [HttpGet]
        [Route("edit")]
        [AllowAnonymous]
        public virtual ActionResult Edit(Guid communicationGid, Guid childGid, string access_token)
        {
            this.InitAppContext(communicationGid, childGid, access_token);

            return RedirectToAction("Prepare");
        }

        [HttpGet]
        [Route("preview")]
        [AllowAnonymous]
        public virtual ActionResult Preview(Guid communicationGid, Guid childGid, string access_token)
        {
            return View(this.InitAppContext(communicationGid, childGid, access_token));
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            _projectCommunicationCommunicator.PrivateSubmitProjectCommunicationAnswer
                (
                    AppContext.Current.WorkingDocument.parentGid,
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
            PutProjectCommunicationAnswer();
        
            base.SaveDraft();
        }

        private void PutProjectCommunicationAnswer()
        {
            AppContext.Current.WorkingDocument.version =
                _projectCommunicationCommunicator.PrivatePutProjectCommunicationAnswer
                    (
                        AppContext.Current.WorkingDocument.parentGid,
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version
                    ).version;
        }

        private Message InitAppContext(Guid communicationGid, Guid answerGid, string access_token)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            var contractMessage = _projectCommunicationCommunicator.PrivateGetProjectCommunicationAnswer(communicationGid, answerGid, access_token);

            var message = _documentSerializer.XmlDeserializeFromString<Message>(contractMessage.xml);
            message.IsManagingAuthority = true;

            AppContext.Current.Document = message;
            AppContext.Current.Xml = contractMessage.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                parentGid = communicationGid,
                gid = answerGid,
                token = access_token,
                version = contractMessage.version
            };

            return message;
        }
    }
}
