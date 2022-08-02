using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Documents;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models.ProjectCommunicationAnswer;
using System;
using System.Web.Mvc;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    [Authorize]
    public partial class ProjectCommunicationAnswerController : WorkflowController<EditVM>
    {
        private IProjectCommunicationCommunicator _projectCommunicationCommunicator;

        public ProjectCommunicationAnswerController(
            IDocumentSerializer documentSerializer,
            IProjectCommunicationCommunicator projectCommunicationCommunicator)
            : base(documentSerializer)
        {
            _projectCommunicationCommunicator = projectCommunicationCommunicator;
        }


        [HttpGet]
        public virtual ActionResult New(Guid registeredGid, Guid communicationGid)
        {
            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            var contractMessage = _projectCommunicationCommunicator.GetNewProjectCommunicationAnswer(communicationGid, CurrentUser.AccessToken);

            AppContext.Current.Document = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(contractMessage.xml);
            AppContext.Current.Xml = contractMessage.xml;

            return RedirectToAction(ActionNames.Edit,
                new
                {
                    registeredGid = registeredGid,
                    communicationGid = contractMessage.gid,
                    answerGid = contractMessage.answerGid
                });
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid registeredGid, Guid communicationGid, Guid answerGid, string version)
        {
            _projectCommunicationCommunicator.DeleteProjectCommunicationAnswer(
                communicationGid,
                answerGid,
                Convert.FromBase64String(version),
                CurrentUser.AccessToken);

            TempData["SuccessAction"] = "Отговорът беше изтрит успешно.";

            return RedirectToAction(MVC.ProjectCommunication.ActionNames.Index, MVC.ProjectCommunication.Name, new { registeredGid = registeredGid });
        }

        [HttpGet]
        public virtual ActionResult Preview(Guid registeredGid, Guid communicationGid, Guid answerGid)
        {
            var contractMessage = _projectCommunicationCommunicator.GetProjectCommunicationAnswer(communicationGid, answerGid, CurrentUser.AccessToken);

            var message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(contractMessage.xml);
            message.RegisteredGid = registeredGid;
            message.ProjectRegNumber = contractMessage.projectRegNumber;

            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);
            AppContext.Current.Document = message;
            AppContext.Current.Xml = contractMessage.xml;

            return View(AppContext.Current.Document);
        }

        [HttpGet]
        public virtual ActionResult Edit(Guid registeredGid, Guid communicationGid, Guid answerGid)
        {
            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            var contractMessage = _projectCommunicationCommunicator.GetProjectCommunicationAnswer(communicationGid, answerGid, CurrentUser.AccessToken);
            var message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(contractMessage.xml);

            message = R_10020.Message.LoadReply(message, registeredGid, contractMessage.projectRegNumber);

            AppContext.Current.Document = message;
            AppContext.Current.Xml = contractMessage.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = answerGid,
                parentGid = communicationGid,
                version = contractMessage.version
            };

            return RedirectToAction(ActionNames.Prepare);
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutProjectCommunicationAnswer();

            return base.Prepare(vm);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            PutProjectCommunicationAnswer();

            base.SaveDraft();
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            PutProjectCommunicationAnswer();
            _projectCommunicationCommunicator.SubmitProjectCommunicationAnswer
                (
                    AppContext.Current.WorkingDocument.parentGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    AppContext.Current.WorkingDocument.version
                );

            return RedirectToAction(ActionNames.Finish, new { answerGid = AppContext.Current.WorkingDocument.gid });

        }

        [HttpGet]
        public virtual ActionResult Finish(Guid answerGid)
        {
            var finishModel = _projectCommunicationCommunicator.GetSentProjectCommunicationAnswerInfo(answerGid, CurrentUser.AccessToken);

            return View(finishModel);
        }

        private void PutProjectCommunicationAnswer()
        {
            if (string.IsNullOrEmpty(AppContext.Current.Xml))
            {
                AppContext.Current.Xml = _documentSerializer.XmlSerializeObjectToString(AppContext.Current.Document);
            }

            AppContext.Current.WorkingDocument.version =
                _projectCommunicationCommunicator.PutProjectCommunicationAnswer
                   (
                       AppContext.Current.WorkingDocument.parentGid,
                       AppContext.Current.WorkingDocument.gid,
                       AppContext.Current.Xml,
                       AppContext.Current.WorkingDocument.version,
                       CurrentUser.AccessToken
                   ).version;
        }
    }
}
