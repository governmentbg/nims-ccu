using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models;
using Eumis.Portal.Web.Models.EvalSheet;
using R_10023;
using R_10026;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class EvalSheetController : WorkflowController<EditVM>
    {
        private IEvalCommunicator _evalCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public EvalSheetController(IEvalCommunicator evalCommunicator, IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _evalCommunicator = evalCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutEvalSheet();

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
            PutEvalSheet();
            _evalCommunicator.SubmitEvalSheet
                (
                    AppContext.Current.WorkingDocument.gid,
                    AppContext.Current.WorkingDocument.token,
                    AppContext.Current.WorkingDocument.version
                );

            return View(MVC.Private.Shared.Views.Success, MVC.Private.Shared.Views._Layout, (object)Global.SuccessSave);
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Pause()
        {
            PutEvalSheet();
            _evalCommunicator.PauseEvalSheet
                (
                    AppContext.Current.WorkingDocument.gid,
                    AppContext.Current.WorkingDocument.token,
                    AppContext.Current.WorkingDocument.version
                );

            return View(MVC.Private.Shared.Views.Success, MVC.Private.Shared.Views._Layout, (object)Global.SuccessSave);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            PutEvalSheet();
            
            base.SaveDraft();
        }

        private void PutEvalSheet()
        {
            AppContext.Current.WorkingDocument.version =
                _evalCommunicator.PutEvalSheet
                    (
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version
                    ).version;
        }

        private R_10026.EvalSheet InitAppContext(Guid gid, string access_token)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.EvalSheetMetadata.Code);

            var contractEvalSheet = _evalCommunicator.GetEvalSheet(gid, access_token);

            var sheet = _documentSerializer.XmlDeserializeFromString<R_10026.EvalSheet>(contractEvalSheet.xml);

            sheet = R_10026.EvalSheet.Load(sheet);

            sheet.ProjectName = contractEvalSheet.displayProjectName;
            sheet.ProjectRegNumber = contractEvalSheet.projectRegNumber;
            sheet.AssessorName = contractEvalSheet.assessorName;

            sheet.ProcedureName = contractEvalSheet.displayProcedureName;
            sheet.CompanyName = contractEvalSheet.displayCompanyName;
            sheet.EvalTableTypeText = contractEvalSheet.displayEvalTableTypeText;

            AppContext.Current.Document = sheet;
            AppContext.Current.Xml = contractEvalSheet.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractEvalSheet.version
            };

            return sheet;
        }
    }
}