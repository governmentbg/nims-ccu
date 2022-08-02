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
using Eumis.Portal.Web.Models.SpendingPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using R_10077;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class SpendingPlanController : WorkflowController<EditVM>
    {
        private ISpendingPlanCommunicator _spendingPlanCommunicator;
        private IBFPContractCommunicator _bfpContractCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public SpendingPlanController(ISpendingPlanCommunicator spendingPlanCommunicator, IBFPContractCommunicator bfpContractCommunicator, IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _spendingPlanCommunicator = spendingPlanCommunicator;
            _bfpContractCommunicator = bfpContractCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutSpendingPlan();

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
            var spendingPlan = (R_10077.SpendingPlan)AppContext.Current.Document;

            AppContext.Current.Document = spendingPlan;
            AppContext.Current.Xml = _documentSerializer.XmlSerializeToString<R_10077.SpendingPlan>(spendingPlan);

            PutSpendingPlan();
            _spendingPlanCommunicator.SubmitSpendingPlan
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
            PutSpendingPlan();

            base.SaveDraft();
        }

        private void PutSpendingPlan()
        {
            AppContext.Current.WorkingDocument.version =
                _spendingPlanCommunicator.PutSpendingPlan
                    (
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version
                    ).version;
        }

        private R_10077.SpendingPlan InitAppContext(Guid gid, string access_token, bool isEdit)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.SpendingPlanMetadata.Code);

            var contractSpendingPlan = _spendingPlanCommunicator.GetSpendingPlan(gid, access_token);

            var spendingPlan = _documentSerializer.XmlDeserializeFromString<R_10077.SpendingPlan>(contractSpendingPlan.xml);

            AppContext.Current.Document = spendingPlan;
            AppContext.Current.Xml = contractSpendingPlan.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractSpendingPlan.version
            };

            return spendingPlan;
        }
    }
}