using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Eumis.Common.Extensions;
using Eumis.Portal.Web.Models;
using Eumis.Documents.Validation;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class ProjectController : WorkflowController<EditVM>
    {
        private IProcedureCommunicator _procedureCommunicator;
        private IProjectCommunicator _projectCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public ProjectController(IProcedureCommunicator procedureCommunicator,
            IProjectCommunicator projectCommunicator, IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _procedureCommunicator = procedureCommunicator;
            _projectCommunicator = projectCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutProject();

            return base.Prepare(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Edit(Guid gid, string access_token)
        {
            this.InitAppContext(gid, access_token, true);

            return RedirectToAction("Prepare");
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Preview(Guid gid, string access_token)
        {
            this.InitAppContext(gid, access_token, false);

            this.Validate();

            return View(AppContext.Current.Document);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Draft(Guid gid)
        {
            return View(new EditVM(this.InitDraftAppContext(gid)));
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult ValidateDraft()
        {
            EditVM vm = (EditVM)Activator.CreateInstance(typeof(EditVM), AppContext.Current.Document);

            this.Validate(AppStep.Prepare, vm);

            return View(Views.Draft, vm);
        }

        [HttpPost]
        [RequiresAppContext]
        public virtual ActionResult Draft(EditVM model)
        {
            var document = typeof(EditVM).GetMethod("Set")
                .Invoke(model, new object[] { AppContext.Current.Document });

            AppContext.Current.Document = document;
            AppContext.Current.Xml = _documentSerializer.XmlSerializeObjectToString(AppContext.Current.Document);

            AppContext.Current.ValidatedSteps.Add(AppStep.Prepare);

            return RedirectToAction(ActionNames.ValidateDraft);
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            _projectCommunicator.SubmitProject
                (
                    AppContext.Current.WorkingDocument.gid,
                    AppContext.Current.WorkingDocument.token,
                    AppContext.Current.Xml,
                    AppContext.Current.WorkingDocument.version
                );

            return View(MVC.Private.Shared.Views.Success, MVC.Private.Shared.Views._Layout, (object)Global.SaveProjectDraftSuccess);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            PutProject();

            base.SaveDraft();
        }

        public void PutProject()
        {
            AppContext.Current.WorkingDocument.version =
                _projectCommunicator.PutProject
                (
                    AppContext.Current.WorkingDocument.gid,
                    AppContext.Current.WorkingDocument.token,
                    AppContext.Current.Xml,
                    AppContext.Current.WorkingDocument.version
                ).version;
        }

        private R_10019.Project InitAppContext(Guid gid, string access_token, bool isEdit)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);

            var contractProject = _projectCommunicator.GetProject(gid, access_token);

            R_10019.Project project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(contractProject.xml);

            var procedure = _procedureCommunicator.GetProcedureAppData(new Guid(project.ProjectBasicData.ProcedureIdentifier));

            project = R_10019.Project.Load(procedure, project);
            project.VersionCreateDate = contractProject.createDate;
            
            project.ProjectHeader = contractProject.regData;
            project.ProjectGid = gid;
            project.IsSubmitted = true;

            AppContext.Current.Document = project;
            AppContext.Current.Xml = contractProject.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractProject.version
            };

            return project;
        }

        private R_10019.Project InitDraftAppContext(Guid gid)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(string.Empty), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);

            ContractProcedure procedure = _procedureCommunicator.GetProcedureActualAppData(gid);

            R_10019.Project project = R_10019.Project.Load(procedure, null, gid);

            AppContext.Current.Document = project;

            return project;
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual FileResult DownloadFiles(Guid projectGid, string hash)
        {
            var project = (R_10019.Project)AppContext.Current.Document;

            var zipFile = this._projectCommunicator.GetProjectFilesZip(projectGid.ToString(), CurrentUser.AccessToken);

            var contentType = MimeTypeFileExtension.MIME_APPLICATION_ZIP;
            var fileDownaloadName = $"{project.ProjectBasicData.Procedure.Code}-{hash}.zip";

            return File(zipFile, contentType, fileDownaloadName);
        }

        private void Validate()
        {
            AppContext.Current.ValidatedSteps.Add(AppStep.Display);

            var validationVM = new ValidationVM()
            {
                RemoteValidationErrors = new List<string>(),
                RemoteValidationWarnings = new List<string>()
            };

            base.Validate(AppStep.Display, validationVM);

            if (AppContext.Current.Document is ILocalValidatable)
            {
                ((ILocalValidatable)AppContext.Current.Document).LocalValidationErrors =
                    validationVM.LocalValidationErrors;
            }

            if (AppContext.Current.Document is IRemoteValidatable)
            {
                ((IRemoteValidatable)AppContext.Current.Document).RemoteValidationErrors =
                    validationVM.RemoteValidationErrors;

                ((IRemoteValidatable)AppContext.Current.Document).RemoteValidationWarnings =
                    validationVM.RemoteValidationWarnings;
            }
        }
    }
}
