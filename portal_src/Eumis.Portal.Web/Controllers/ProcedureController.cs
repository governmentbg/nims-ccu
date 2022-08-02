using Eumis.Common.ReCaptcha;
using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Helplers.Attributes;
using Eumis.Portal.Web.Models.ProcedureComment;
using Eumis.Portal.Web.Models.ProcedureDiscussion;
using Eumis.Portal.Web.Models.ProcedureQuestion;
using PagedList;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    [Authenticated]
    public partial class ProcedureController : BaseController
    {
        private IDocumentSerializer _documentSerializer;
        private IProcedureCommunicator _procedureCommunicator;
        private IProjectCommunicator _projectCommunicator;

        public ProcedureController(IDocumentSerializer documentSerializer, IProcedureCommunicator procedureCommunicator, IProjectCommunicator projectCommunicator)
        {
            _documentSerializer = documentSerializer;
            _procedureCommunicator = procedureCommunicator;
            _projectCommunicator = projectCommunicator;
        }

        [AllowAnonymous]
        public virtual ActionResult Active()
        {
            var tree = _procedureCommunicator.GetActiveProcedureProgrammesTree();

            return View(tree);
        }

        [AllowAnonymous]
        public virtual ActionResult Info(Guid id)
        {
            var info = _procedureCommunicator.GetProcedureInfo(id);

            if (!info.isActive)
            {
                return View(MVC.Shared.Views.Failure, (object)Global.ProcedureNotActive);
            }

            return View(info);
        }

        [AllowAnonymous]
        public virtual ActionResult InfoQA(Guid id, string searchTerm = "", int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var model = _procedureCommunicator.GetProcedureDiscussionsInfo(id, searchTerm, Constants.PAGE_ITEMS_COUNT, offset);

            ProcedureDiscussionsInfoVM vm = new ProcedureDiscussionsInfoVM()
            {
                Id = id,
                Questions = model,
            };

            return View(vm);
        }

        [HttpGet]
        public virtual ActionResult Question(Guid id)
        {
            var info = _procedureCommunicator.GetProcedureInfo(id);

            ProcedureDiscussionQuestionVM model = new ProcedureDiscussionQuestionVM()
            {
                Id = id,
                ProcedureName = info.displayName
            };

            return View(model);
        }

        [HttpPost]
        [ReCaptchaValidation]
        public virtual ActionResult Question(ProcedureDiscussionQuestionVM vm, bool? captchaValid)
        {
            if (captchaValid.HasValue && !captchaValid.Value)
            {
                ModelState.AddModelError(Constants.CaptchaModelName, Global.ValidationCaptcha);
            }

            if (!ModelState.IsValid)
            {
                var info = _procedureCommunicator.GetProcedureInfo(vm.Id);
                vm.ProcedureName = info.displayName;

                return View(vm);
            }

            _procedureCommunicator.SubmitProcedureDiscussionQuestion(vm.Id, CurrentUser.AccessToken, vm.Question);

            return View(MVC.Shared.Views.Success, (object)Eumis.Portal.Web.Views.Shared.App_LocalResources.Question.QuestionSuccessNotification);
        }

        [AllowAnonymous]
        public virtual ActionResult Ended()
        {
            var tree = _procedureCommunicator.GetEndedProcedureProgrammesTree();

            return View(tree);
        }

        [AllowAnonymous]
        public virtual ActionResult InfoEnded(Guid id)
        {
            var info = _procedureCommunicator.GetProcedureInfo(id);

            return View(info);
        }

        [AllowAnonymous]
        public virtual ActionResult InfoEndedQA(Guid id, string searchTerm = "", int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var model = _procedureCommunicator.GetProcedureDiscussionsInfo(id, searchTerm, Constants.PAGE_ITEMS_COUNT, offset);
            ProcedureDiscussionsInfoVM vm = new ProcedureDiscussionsInfoVM()
            {
                Id = id,
                Questions = model,
            };

            return View(vm);
        }

        [AllowAnonymous]
        public virtual ActionResult PublicDiscussion()
        {
            var tree = _procedureCommunicator.GetPublicDiscussionProcedureProgrammesTree();

            return View(tree);
        }

        [AllowAnonymous]
        public virtual ActionResult InfoPublicDiscussion(Guid id)
        {
            var info = _procedureCommunicator.GetProcedurePublicDiscussionInfo(id);

            return View(info);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Comment(Guid id)
        {
            var info = _procedureCommunicator.GetProcedurePublicDiscussionInfo(id);

            ProcedureCommentVM model = new ProcedureCommentVM()
            {
                Id = id,
                ProcedureName = info.displayName
            };

            return View(model);
        }

        [HttpPost]
        [ReCaptchaValidation]
        public virtual ActionResult Comment(ProcedureCommentVM vm, bool? captchaValid)
        {
            if (captchaValid.HasValue && !captchaValid.Value)
            {
                ModelState.AddModelError(Constants.CaptchaModelName, Global.ValidationCaptcha);
            }

            if (!ModelState.IsValid)
            {
                var info = _procedureCommunicator.GetProcedurePublicDiscussionInfo(vm.Id);
                vm.ProcedureName = info.displayName;

                return View(vm);
            }

            _procedureCommunicator.SubmitProcedurePublicDiscussionComment(vm.Id, CurrentUser.AccessToken, vm.Email, vm.Name, vm.Message);

            return View(MVC.Shared.Views.Success, (object)Eumis.Portal.Web.Views.Shared.App_LocalResources.Comment.CommentSuccessNotification);
        }

        [AllowAnonymous]
        public virtual ActionResult ArchivedPublicDiscussion()
        {
            var tree = _procedureCommunicator.GetArchivedPublicDiscussionProcedureProgrammesTree();

            return View(tree);
        }

        [AllowAnonymous]
        public virtual ActionResult InfoArchivedPublicDiscussion(Guid id)
        {
            var info = _procedureCommunicator.GetProcedurePublicDiscussionInfo(id);

            return View(info);
        }

        [AllowAnonymous]
        public virtual ActionResult InfoDownload(Guid id, Guid fileKey)
        {
            var procedureInfo = _procedureCommunicator.GetProcedureInfo(id);

            if (!procedureInfo.applicationGuidelines.Where(a => a.blobKey == fileKey).Any() &&
                procedureInfo.qaBlobKey != fileKey)
            {
                return new HttpNotFoundResult();
            }

            return this.DownloadFile(fileKey);
        }

        [AllowAnonymous]
        public virtual ActionResult InfoPublicDiscussionDownload(Guid id, Guid fileKey)
        {
            var procedureInfo = _procedureCommunicator.GetProcedurePublicDiscussionInfo(id);

            return this.DownloadFile(fileKey);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult PublicPreview()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult PublicPreview(FormCollection form)
        {
            HttpPostedFileBase hpfb = Request.Files["applied_isun_file"];

            string xml = null;
            R_10019.Project project = null;

            if (hpfb != null && hpfb.ContentLength != 0)
            {
                try
                {
                    xml = ZipManager.UnzipProject(hpfb.InputStream);

                    project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(xml);

                }
                catch { }
            }
            else
            {
                ModelState.AddModelError("project_file", Global.PleaseAttachProject);
                return View();
            }

            if (project != null)
            {
                _projectCommunicator.ResurrectFiles(xml);

                AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);
                AppContext.Current.Document = project;
                AppContext.Current.Xml = xml;

                return RedirectToAction(MVC.Project.ActionNames.Preview, MVC.Project.Name);
            }

            ModelState.AddModelError("_invalidProject", String.Format(Global.NotSameFormat, hpfb.FileName));

            return View();
        }
    }
}