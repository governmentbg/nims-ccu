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
using Eumis.Portal.Web.Models.FinanceReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using R_10043;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class FinanceReportController : WorkflowController<EditVM>
    {
        private IBFPContractCommunicator _bfpContractCommunicator;
        private IFinanceReportCommunicator _financeReportCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public FinanceReportController(
            IBFPContractCommunicator bfpContractCommunicator,
            IFinanceReportCommunicator financeReportCommunicator,
            IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _bfpContractCommunicator = bfpContractCommunicator;
            _financeReportCommunicator = financeReportCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutFinanceReport();

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
            PutFinanceReport();
            _financeReportCommunicator.PrivateSubmitFinanceReport
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
            PutFinanceReport();

            base.SaveDraft();
        }

        private void PutFinanceReport()
        {
            AppContext.Current.WorkingDocument.version =
                _financeReportCommunicator.PrivatePutFinanceReport
                    (
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version
                    ).version;
        }

        private FinanceReport InitAppContext(Guid gid, string access_token, bool isEdit)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.FinanceReportMetadata.Code);

            var contractFinanceReport = _financeReportCommunicator.PrivateGetFinanceReport(gid, access_token, isEdit);

            var financeReport = _documentSerializer.XmlDeserializeFromString<FinanceReport>(contractFinanceReport.xml);
            financeReport.CanEnterErrors = contractFinanceReport.CanEnterErrors;

            if (isEdit)
            {
                R_10043.FinanceReport.LoadNomenclatures(ref financeReport, contractFinanceReport.ProcedureContractReportFinancialDocuments);
            }

            AppContext.Current.Document = financeReport;
            AppContext.Current.Xml = contractFinanceReport.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractFinanceReport.version
            };

            return financeReport;
        }
    }
}