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
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Eumis.Common.Resources;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;
using Eumis.Documents.Mappers;

namespace Eumis.Portal.Web.Controllers
{
    public partial class ProjectController : WorkflowController<EditVM>
    {
        private IProcedureCommunicator _procedureCommunicator;
        private IDraftCommunicator _draftCommunicator;

        private PublicSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PublicSignInManager>();
            }
        }

        public ProjectController(IProcedureCommunicator procedureCommunicator, IProjectCommunicator projectCommunicator,
            IDocumentSerializer documentSerializer, IDraftCommunicator draftCommunicator)
            : base(documentSerializer)
        {
            _procedureCommunicator = procedureCommunicator;
            _draftCommunicator = draftCommunicator;
        }

        [HttpGet]
        public virtual ActionResult New(Guid id)
        {
            AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);

            ContractProcedure procedure = _procedureCommunicator.GetProcedureAppData(id);

            if (!procedure.isActive)
            {
                return View(MVC.Shared.Views.Failure, (object)Global.ProcedureNewNotActive);
            }

            R_10019.Project project = R_10019.Project.Load(procedure, null, id);

            AppContext.Current.Document = project;

            return RedirectToAction(ActionNames.Prepare);
        }

        [HttpGet]
        [AllowAnonymous]
        [RequiresAppContext]
        public virtual ActionResult Preview()
        {
            var project = (R_10019.Project)AppContext.Current.Document;

            ContractProcedure procedure = _procedureCommunicator.GetProcedureAppData(Guid.Parse(project.ProjectBasicData.ProcedureIdentifier));
            project.ApplicationSections = procedure.applicationSections.Select(x => new ApplicationSection(x)).ToList();

            project.SetDeclarationsAttributes(procedure.declarations);

            return View(AppContext.Current.Document);
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutDraft();

            ContractProcedure procedure = _procedureCommunicator.GetProcedureAppData(Guid.Parse(((R_10019.Project)AppContext.Current.Document).ProjectBasicData.ProcedureIdentifier));

            if (procedure.applicationSections.SelectMany(x => x.additionalSettings).Any())
            {
                vm.ProjectBasicData.FillMainData = procedure
                    .applicationSections
                    .Where(x => x.section.value == "basicData")
                    .SelectMany(x => x.additionalSettings)
                    .Where(x => x.name == "FillMainData")
                    .Select(x => x.isSelected)
                    .First();
            }

            return base.Prepare(vm);
        }

        [HttpPost]
        [RequiresAppContext]
        public virtual ActionResult Finalize()
        {
            this.PutDraft();
            this.FinalizeDraft();

            AppContext.Current.Clear();

            TempData["ProjectFinalized"] = true;

            return RedirectToAction(MVC.Finalized.ActionNames.Index, MVC.Finalized.Name);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            var modifyDate = PutDraft();

            if (!modifyDate.HasValue)
            {
                base.SaveDraft();
            }
            else
            {
                AppContext.Current.LastSaveDate = modifyDate;
            }
        }

        private DateTime? PutDraft()
        {
            byte[] version;
            DateTime? modifyDate = null;

            if (string.IsNullOrEmpty(AppContext.Current.Xml))
            {
                AppContext.Current.Xml = _documentSerializer.XmlSerializeObjectToString(AppContext.Current.Document);
            }

            if (AppContext.Current.WorkingDocument.gid == null
                || AppContext.Current.WorkingDocument.gid == Guid.Empty)
            {
                var newDraft = _draftCommunicator.CreateDraft
                (
                    AppContext.Current.Xml,
                    null,
                    CurrentUser.AccessToken
                );

                version = newDraft.version;
                modifyDate = newDraft.modifyDate;
                AppContext.Current.WorkingDocument.gid = newDraft.gid.Value;
            }
            else
            {
                var updateDraft = _draftCommunicator.UpdateDraft
                (
                    AppContext.Current.WorkingDocument.gid,
                    AppContext.Current.Xml,
                    AppContext.Current.WorkingDocument.version,
                    CurrentUser.AccessToken
                );

                version = updateDraft.version;
                modifyDate = updateDraft.modifyDate;
            }

            AppContext.Current.WorkingDocument.version = version;
            return modifyDate;
        }

        private void FinalizeDraft()
        {
            _draftCommunicator.FinalizeDraft
            (
                AppContext.Current.WorkingDocument.gid,
                AppContext.Current.WorkingDocument.version,
                CurrentUser.AccessToken
            );
        }
    }
}