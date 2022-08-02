using Eumis.Common.Crypto;
using Eumis.Common.Extensions;
using Eumis.Common.Helpers;
using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Helplers;
using Eumis.Portal.Web.Models.Draft;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers.Base
{
    [Authenticated]
    public abstract partial class DraftBaseController : BaseController
    {
        protected IDocumentSerializer _documentSerializer;
        protected IDraftCommunicator _draftCommunicator;
        protected IProcedureCommunicator _procedureCommunicator;
        protected IProjectCommunicator _projectCommunicator;

        public DraftBaseController() { }

        public DraftBaseController(IDocumentSerializer documentSerializer, IDraftCommunicator draftCommunicator,
            IProcedureCommunicator procedureCommunicator, IProjectCommunicator projectCommunicator)
        {
            _documentSerializer = documentSerializer;
            _draftCommunicator = draftCommunicator;
            _procedureCommunicator = procedureCommunicator;
            _projectCommunicator = projectCommunicator;
        }

        [HttpPost]
        public virtual ActionResult LoadForEdit(FormCollection form)
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
                TempData["file_error"] = Global.PleaseAttachProject;

                return RedirectToAction("Index");
            }

            if (project != null)
            {
                var procedureGid = new Guid(project.ProjectBasicData.ProcedureIdentifier);

                AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);

                ContractProcedure procedure = null;

                try
                {
                    procedure = _procedureCommunicator.GetProcedureAppData(procedureGid);

                    if (!procedure.isActive)
                    {
                        return View(MVC.Shared.Views.Failure, (object)Global.ProcedureEditNotActive);
                    }
                }
                catch
                {
                    string fileName = Path.GetFileName(hpfb.FileName);
                    TempData["file_error"] = String.Format(Global.NotValidProcedure, fileName);
                    return RedirectToAction("Index");
                }

                _projectCommunicator.ResurrectFiles(xml);

                project = R_10019.Project.Load(procedure, project);

                project.id = Guid.NewGuid().ToString();
                project.createDate = DateTime.Now;
                project.modificationDate = DateTime.Now;

                AppContext.Current.Document = project;
                AppContext.Current.Xml = _documentSerializer.XmlSerializeToString<R_10019.Project>(project);

                return RedirectToAction(MVC.Project.ActionNames.Prepare, MVC.Project.Name);
            }

            TempData["file_error"] = String.Format(Global.NotSameFormat, hpfb.FileName);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public virtual ActionResult LoadForPreview(FormCollection form)
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
                TempData["file_error"] = Global.PleaseAttachProject;
                return RedirectToAction("Index");
            }

            if (project != null)
            {
                _projectCommunicator.ResurrectFiles(xml);

                AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);
                AppContext.Current.Document = project;
                AppContext.Current.Xml = xml;

                return RedirectToAction(MVC.Project.ActionNames.Preview, MVC.Project.Name);
            }

            TempData["file_error"] = String.Format(Global.NotSameFormat, hpfb.FileName);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Edit(Guid gid)
        {
            AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);

            var draft = _draftCommunicator.GetDraft(gid, CurrentUser.AccessToken);
            R_10019.Project project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(draft.xml);
            var procedure = _procedureCommunicator.GetProcedureAppData(new Guid(project.ProjectBasicData.ProcedureIdentifier));
            if (!procedure.isActive)
            {
                return View(MVC.Shared.Views.Failure, (object)Global.ProcedureEditNotActive);
            }
            project = R_10019.Project.Load(procedure, project);

            AppContext.Current.Document = project;
            AppContext.Current.Xml = draft.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                version = draft.version
            };

            return RedirectToAction(MVC.Project.ActionNames.Prepare, MVC.Project.Name);
        }

        [HttpGet]
        public virtual ActionResult Preview(Guid gid)
        {
            var xml = _draftCommunicator.GetDraft(gid, CurrentUser.AccessToken).xml;

            AppContext.Current = new AppContext(DocumentMetadata.ProjectMetadata.Code);
            AppContext.Current.Document = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(xml);
            AppContext.Current.Xml = xml;

            return RedirectToAction(MVC.Project.ActionNames.Preview, MVC.Project.Name);
        }

        [HttpGet]
        public virtual FileResult SaveAsFile(Guid gid, string hash)
        {
            var draft = _draftCommunicator.GetDraft(gid, CurrentUser.AccessToken);

            hash = hash ?? CryptoUtils.GetSha256XMLHash(draft.xml).Truncate(10);

            var isunFile = IsunFileManager.Create(draft.xml, hash);

            return File(isunFile.Content, isunFile.MimeType, isunFile.Filename);
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid gid)
        {
            var draft = _draftCommunicator.GetDraft(gid, CurrentUser.AccessToken);

            _draftCommunicator.DeleteDraft(gid,
                string.Empty,
                draft.version,
                CurrentUser.AccessToken);

            return RedirectToAction("Index");
        }
    }
}