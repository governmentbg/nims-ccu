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
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class SpendingPlanController : WorkflowController<EditVM>
    {
        private Guid _contractGid;
        private ISpendingPlanCommunicator _spendingPlanCommunicator;
        private IBFPContractCommunicator _bfpContractCommunicator;

        public SpendingPlanController(ISpendingPlanCommunicator spendingPlanCommunicator,
            IBFPContractCommunicator bfpContractCommunicator,
            IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _spendingPlanCommunicator = spendingPlanCommunicator;
            _bfpContractCommunicator = bfpContractCommunicator;
            _contractGid = new Guid(System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData.Values["cgid"].ToString());
        }

        [HttpGet]
        public virtual ActionResult New()
        {
            var newSpendingPlan = _spendingPlanCommunicator.CreateContractSpendingPlan(CurrentUser.AccessToken, _contractGid);

            return RedirectToAction(ActionNames.Edit, new { gid = newSpendingPlan.gid.Value });
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutDraft();

            return base.Prepare(vm);
        }

        [HttpGet]
        public virtual ActionResult Edit(Guid gid)
        {
            var version = _spendingPlanCommunicator.GetContractSpendingPlan(CurrentUser.AccessToken, _contractGid, gid);

            var spendingPlan = _documentSerializer.XmlDeserializeFromString<R_10077.SpendingPlan>(version.xml);

            AppContext.Current = new AppContext(DocumentMetadata.SpendingPlanMetadata.Code);
            AppContext.Current.Document = spendingPlan;
            AppContext.Current.WorkingDocument = new WorkingDocumentData()
            {
                gid = gid,
                version = version.version
            };

            return RedirectToAction(ActionNames.Prepare);
        }

        [HttpGet]
        public virtual ActionResult Preview(Guid gid)
        {
            var version = _spendingPlanCommunicator.GetContractSpendingPlan(CurrentUser.AccessToken, _contractGid, gid);

            var spendingPlan = _documentSerializer.XmlDeserializeFromString<R_10077.SpendingPlan>(version.xml);

            AppContext.Current = new AppContext(DocumentMetadata.SpendingPlanMetadata.Code);
            AppContext.Current.Document = spendingPlan;

            return View(spendingPlan);
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid gid)
        {
            _spendingPlanCommunicator.DeleteContractSpendingPlan(CurrentUser.AccessToken, _contractGid, gid);

            TempData["SuccessAction"] = "План за разходване на средствата е изтрит успешно.";

            return RedirectToAction(MVC.Report.BFPContract.ActionNames.Index, MVC.Report.BFPContract.Name);
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

            var updateDraft = _spendingPlanCommunicator.UpdateContractSpendingPlan
                (
                    CurrentUser.AccessToken,
                    _contractGid,
                    AppContext.Current.WorkingDocument.gid,
                    AppContext.Current.Xml,
                    AppContext.Current.WorkingDocument.version
                );

            version = updateDraft.version;
            modifyDate = updateDraft.modifyDate;

            AppContext.Current.WorkingDocument.version = version;
            return modifyDate;
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            var spendingPlan = (R_10077.SpendingPlan)AppContext.Current.Document;

            AppContext.Current.Document = spendingPlan;
            AppContext.Current.Xml = _documentSerializer.XmlSerializeToString<R_10077.SpendingPlan>(spendingPlan);

            PutDraft();

            _spendingPlanCommunicator.SubmitContractSpendingPlan
                (
                    CurrentUser.AccessToken,
                    _contractGid,
                    AppContext.Current.WorkingDocument.gid
                );

            TempData["SuccessAction"] = "План за разходване на средствата е приключен успешно.";

            return RedirectToAction(MVC.Report.BFPContract.ActionNames.Index, MVC.Report.BFPContract.Name);
        }
    }
}