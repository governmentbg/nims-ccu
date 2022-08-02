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
using Eumis.Portal.Web.Models.TechnicalReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using R_10044;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class TechnicalReportController : WorkflowController<EditVM>
    {
        private IBFPContractCommunicator _bfpContractCommunicator;
        private ITechnicalReportCommunicator _technicalReportCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public TechnicalReportController(
            IBFPContractCommunicator bfpContractCommunicator,
            ITechnicalReportCommunicator technicalReportCommunicator,
            IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _bfpContractCommunicator = bfpContractCommunicator;
            _technicalReportCommunicator = technicalReportCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutTechnicalReport();

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
            return View(this.InitAppContext(gid, access_token, false));
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            PutTechnicalReport();
            _technicalReportCommunicator.PrivateSubmitTechnicalReport
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
            PutTechnicalReport();

            base.SaveDraft();
        }

        private void PutTechnicalReport()
        {
            AppContext.Current.WorkingDocument.version =
                _technicalReportCommunicator.PrivatePutTechnicalReport
                    (
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version
                    ).version;
        }

        private R_10044.TechnicalReport InitAppContext(Guid gid, string access_token, bool isEdit)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.TechnicalReportMetadata.Code);

            var contractTechnicalReport = _technicalReportCommunicator.PrivateGetTechnicalReport(gid, access_token, isEdit);

            var technicalReport = _documentSerializer.XmlDeserializeFromString<R_10044.TechnicalReport>(contractTechnicalReport.xml);
            technicalReport.CanEnterErrors = contractTechnicalReport.CanEnterErrors;

            var procedureApplicationSections = _bfpContractCommunicator.GetProcedureApplciationSections(Guid.Parse(technicalReport.contractGid), access_token);

            R_10044.TechnicalReport.LoadNomenclatures(ref technicalReport, contractTechnicalReport.ProcedureContractReportTechnicalDocuments, procedureApplicationSections);

            AppContext.Current.Document = technicalReport;
            AppContext.Current.Xml = contractTechnicalReport.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractTechnicalReport.version
            };

            return technicalReport;
        }
    }
}