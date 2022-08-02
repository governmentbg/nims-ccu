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
using Eumis.Portal.Web.Models.Procurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class ProcurementsController : WorkflowController<EditVM>
    {
        private Guid _contractGid;
        private IProcurementsCommunicator _procurementsCommunicator;
        private IBFPContractCommunicator _bfpContractCommunicator;
        private IOffersCommunicator _offersCommunicator;

        public ProcurementsController(IProcurementsCommunicator procurementsCommunicator,
            IBFPContractCommunicator bfpContractCommunicator,
            IDocumentSerializer documentSerializer,
            IOffersCommunicator offersCommunicator)
            : base(documentSerializer)
        {
            _procurementsCommunicator = procurementsCommunicator;
            _bfpContractCommunicator = bfpContractCommunicator;
            _contractGid = new Guid(System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData.Values["cgid"].ToString());
            _offersCommunicator = offersCommunicator;
        }

        [HttpGet]
        public virtual ActionResult New()
        {
            var newProcurements = _procurementsCommunicator.CreateContractProcurement(CurrentUser.AccessToken, _contractGid);

            return RedirectToAction(ActionNames.Edit, new { gid = newProcurements.gid.Value });
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
            var version = _procurementsCommunicator.GetContractProcurementForEdit(CurrentUser.AccessToken, _contractGid, gid);

            var centralProcurements = _procurementsCommunicator.GetCentralProcurement(CurrentUser.AccessToken);

            var procurements = _documentSerializer.XmlDeserializeFromString<R_10041.Procurements>(version.xml);

            R_10041.Procurements.LoadNomenclatures(ref procurements, version.ProcedureProcurementDocuments, centralProcurements);

            AppContext.Current = new AppContext(DocumentMetadata.ProcurementsMetadata.Code);
            AppContext.Current.Document = procurements;
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
            var version = _procurementsCommunicator.GetContractProcurement(CurrentUser.AccessToken, _contractGid, gid);

            var procurements = _documentSerializer.XmlDeserializeFromString<R_10041.Procurements>(version.xml);

            AppContext.Current = new AppContext(DocumentMetadata.ProcurementsMetadata.Code);
            AppContext.Current.Document = procurements;

            return View(procurements);
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid gid)
        {
            _procurementsCommunicator.DeleteContractProcurement(CurrentUser.AccessToken, _contractGid, gid);

            TempData["SuccessAction"] = "Процедури за избор на изпълнител и сключени договори е изтрит успешно.";

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

            var updateDraft = _procurementsCommunicator.UpdateContractProcurement
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
            var procurements = (R_10041.Procurements)AppContext.Current.Document;

            AppContext.Current.Document = procurements;
            AppContext.Current.Xml = _documentSerializer.XmlSerializeToString<R_10041.Procurements>(procurements);

            PutDraft();

            _procurementsCommunicator.SubmitContractProcurement
                (
                    CurrentUser.AccessToken,
                    _contractGid,
                    AppContext.Current.WorkingDocument.gid
                );

            TempData["SuccessAction"] = "Процедури за избор на изпълнител и сключени договори е приключен успешно.";

            return RedirectToAction(MVC.Report.BFPContract.ActionNames.Index, MVC.Report.BFPContract.Name);
        }
    }
}