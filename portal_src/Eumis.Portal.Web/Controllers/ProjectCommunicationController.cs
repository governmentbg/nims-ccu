using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models.ProjectCommunication;
using PagedList;
using System;
using System.Web.Mvc;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    [Authorize]
    public partial class ProjectCommunicationController : WorkflowController<EditVM>
    {
        private IProjectCommunicationCommunicator _projectCommunicationCommunicator;

        public ProjectCommunicationController(
            IDocumentSerializer documentSerializer,
            IProjectCommunicationCommunicator projectCommunicationCommunicator)
            : base(documentSerializer)
        {
            _projectCommunicationCommunicator = projectCommunicationCommunicator;
        }

        public virtual ActionResult Index(Guid registeredGid, int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var result = _projectCommunicationCommunicator.GetCommunications(registeredGid, CurrentUser.AccessToken, Constants.PAGE_ITEMS_COUNT, offset);

            IndexVM model = new IndexVM();

            model.registeredGid = registeredGid;
            model.ProjectRegNumber = result.projectRegNumber;
            model.Communications = new StaticPagedList<MessagePVO>(result.communications.results, page, Constants.PAGE_ITEMS_COUNT, result.communications.count);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult New(Guid registeredGid)
        {
            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            var contractMessage = _projectCommunicationCommunicator.GetNewProjectCommunication(registeredGid, CurrentUser.AccessToken);

            AppContext.Current.Document = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(contractMessage.xml);
            AppContext.Current.Xml = contractMessage.xml;

            return RedirectToAction(ActionNames.Edit, new { registeredGid = registeredGid, gid = contractMessage.gid });
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid registeredGid, Guid gid)
        {
            _projectCommunicationCommunicator.DeleteProjectCommunication(gid, CurrentUser.AccessToken);

            TempData["SuccessAction"] = "Комуникацията беше изтрита успешно.";

            var projectHasCommunications = _projectCommunicationCommunicator.AssertProjectHasCommunications(registeredGid, CurrentUser.AccessToken);
            if (projectHasCommunications)
            {
                return RedirectToAction(ActionNames.Index, new { registeredGid = registeredGid });
            }

            return RedirectToAction(MVC.Registered.ActionNames.Communication, MVC.Registered.Name);
        }

        [HttpGet]
        public virtual ActionResult Cancel(Guid registeredGid, Guid gid, string version)
        {
            _projectCommunicationCommunicator.CancelProjectCommunication(gid, Convert.FromBase64String(version), CurrentUser.AccessToken);

            TempData["SuccessAction"] = "Комуникацията беше Анулирана успешно.";

            return RedirectToAction(ActionNames.Index, new { registeredGid = registeredGid });
        }

        [HttpGet]
        public virtual ActionResult Preview(Guid registeredGid, Guid gid)
        {
            var contractMessage = _projectCommunicationCommunicator.GetProjectCommunication(gid, CurrentUser.AccessToken);

            var message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(contractMessage.xml);
            message.RegisteredGid = registeredGid;
            message.ProjectRegNumber = contractMessage.projectRegNumber;


            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);
            AppContext.Current.Document = message;
            AppContext.Current.Xml = contractMessage.xml;

            return View(AppContext.Current.Document);
        }


        [HttpGet]
        public virtual ActionResult Edit(Guid registeredGid, Guid gid)
        {
            AppContext.Current = new AppContext(DocumentMetadata.MessageMetadata.Code);

            var contractMessage = _projectCommunicationCommunicator.GetProjectCommunication(gid, CurrentUser.AccessToken);
            var message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(contractMessage.xml);

            message = R_10020.Message.LoadQuestion(message, registeredGid, contractMessage.projectRegNumber);

            AppContext.Current.Document = message;
            AppContext.Current.Xml = contractMessage.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                version = contractMessage.version
            };

            return RedirectToAction(ActionNames.Prepare);
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutProjectCommunication();

            return base.Prepare(vm);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            PutProjectCommunication();

            base.SaveDraft();
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            PutProjectCommunication();
            _projectCommunicationCommunicator.SubmitProjectCommunication
                (
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    AppContext.Current.WorkingDocument.version
                );

            return RedirectToAction(ActionNames.Finish, new { gid = AppContext.Current.WorkingDocument.gid });

        }

        [HttpGet]
        public virtual ActionResult Finish(Guid gid)
        {
            var finishModel = _projectCommunicationCommunicator.GetSentProjectCommunicationInfo(gid, CurrentUser.AccessToken);

            return View(finishModel);
        }

        private void PutProjectCommunication()
        {
            if (string.IsNullOrEmpty(AppContext.Current.Xml))
            {
                AppContext.Current.Xml = _documentSerializer.XmlSerializeObjectToString(AppContext.Current.Document);
            }

            var projectCommunication = ((R_10020.Message)AppContext.Current.Document);

            AppContext.Current.WorkingDocument.version =
                _projectCommunicationCommunicator.PutProjectCommunication
                   (
                       AppContext.Current.WorkingDocument.gid,
                       AppContext.Current.Xml,
                       AppContext.Current.WorkingDocument.version,
                       projectCommunication?.Subject.id,
                       CurrentUser.AccessToken
                   ).version;
        }
    }
}
