using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Models.MessageSend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using R_10019;
using R_10020;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Common.Resources;
using Eumis.Common.Extensions;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    [Authorize]
    [RoutePrefix("message")]
    public partial class MessageSendController : WorkflowController<EditVM>
    {
        private IMessageCommunicator _messageCommunicator;
        private IProcedureCommunicator _procedureCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public MessageSendController(IMessageCommunicator messageCommunicator,
            IProcedureCommunicator procedureCommunicator, IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _messageCommunicator = messageCommunicator;
            _procedureCommunicator = procedureCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutMessage();

            return base.Prepare(vm);
        }

        [HttpGet]
        [Route("edit")]
        [AllowAnonymous]
        public virtual ActionResult Edit(Guid gid, string access_token, string type)
        {
            this.InitAppContext(gid, access_token, type);

            return RedirectToAction("Prepare");
        }

        [HttpGet]
        [Route("preview")]
        [AllowAnonymous]
        public virtual ActionResult Preview(Guid gid, string access_token, string type)
        {
            return View(this.InitAppContext(gid, access_token, type));
        }

        [HttpGet]
        [Route("answerPreview")]
        [AllowAnonymous]
        public virtual ActionResult AnswerPreview(Guid communicationGid, Guid childGid, string access_token, string type)
        {
            return View(Views.Preview, this.InitAnswerAppContext(communicationGid, childGid, access_token, type));
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            _messageCommunicator.SubmitProjectMessage
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
            PutMessage();

            base.SaveDraft();
        }

        private void PutMessage()
        {
            var endingDate = ((R_10020.Message)AppContext.Current.Document).EndingDate;

            AppContext.Current.WorkingDocument.version =
                _messageCommunicator.PutProjectMessage
                    (
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version,
                        endingDate
                    ).version;
        }

        private Message InitAppContext(Guid gid, string access_token, string type = "")
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            var contractMessage = _messageCommunicator.GetProjectMessage(gid, access_token, type);

            var message = _documentSerializer.XmlDeserializeFromString<Message>(contractMessage.xml);

            ContractProcedure procedure = _procedureCommunicator.GetProcedureAppData(new Guid(message.Project.ProjectBasicData.ProcedureIdentifier));
            message.Project.LoadNomenclature(procedure.applicationSections);

            message.Project.SetDeclarationsAttributes(procedure.declarations);
            
            message.MessageHeader = contractMessage.regData;
            message.ProjectCommunicationType = type;
            message.ProjectCommunicationGid = gid;
            message.EndingDate = contractMessage.messageEndingDate;
            message.Project.IsSubmitted = true;

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

        private Message InitAnswerAppContext(Guid communicationGid, Guid answerGid, string access_token, string type = "")
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            var contractMessage = _messageCommunicator.GetProjectMessageAnswer(communicationGid, answerGid, access_token);

            var message = _documentSerializer.XmlDeserializeFromString<Message>(contractMessage.xml);

            ContractProcedure procedure = _procedureCommunicator.GetProcedureAppData(new Guid(message.Project.ProjectBasicData.ProcedureIdentifier));
            message.Project.LoadNomenclature(procedure.applicationSections);

            message.Project.SetDeclarationsAttributes(procedure.declarations);

            message.MessageHeader = contractMessage.regData;
            message.ProjectCommunicationType = type;
            message.ProjectCommunicationGid = communicationGid;
            message.Project.IsSubmitted = true;

            AppContext.Current.Document = message;
            AppContext.Current.Xml = contractMessage.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = answerGid,
                token = access_token,
                version = contractMessage.version
            };

            return message;
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual FileResult DownloadFiles(Guid messageGid, string hash)
        {
            var message = (R_10020.Message)AppContext.Current.Document;

            var zipFile = this._messageCommunicator.GetMessageProjectFilesZip(
                messageGid.ToString(),
                AppContext.Current.WorkingDocument.gid.ToString(),
                message.ProjectCommunicationType,
                CurrentUser.AccessToken);

            var contentType = MimeTypeFileExtension.MIME_APPLICATION_ZIP;
            var fileDownaloadName = $"{message.Project.ProjectBasicData.Procedure.Code}-{hash}.zip";

            return File(zipFile, contentType, fileDownaloadName);
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual FileResult DownloadCommunicationFiles(Guid messageGid, string hash, bool isQuestion)
        {
            byte[] zipFile = null;
            if (isQuestion)
            {
                zipFile = this._messageCommunicator.GetQuestionFilesZip(messageGid.ToString(), CurrentUser.AccessToken);
            }
            else
            {
                zipFile = this._messageCommunicator.GetAnswerFilesZip(
                    messageGid.ToString(),
                    AppContext.Current.WorkingDocument.gid.ToString(),
                    CurrentUser.AccessToken);
            }

            var contentType = MimeTypeFileExtension.MIME_APPLICATION_ZIP;

            var message = (R_10020.Message)AppContext.Current.Document;
            var fileDownaloadName = $"{message.Project.ProjectBasicData.Procedure.Code}-{hash}.zip";

            return File(zipFile, contentType, fileDownaloadName);
        }
    }
}
