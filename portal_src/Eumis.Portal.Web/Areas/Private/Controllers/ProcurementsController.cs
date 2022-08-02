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
using R_10041;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class ProcurementsController : WorkflowController<EditVM>
    {
        private IProcurementsCommunicator _procurementsCommunicator;
        private IBFPContractCommunicator _bfpContractCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public ProcurementsController(IProcurementsCommunicator procurementsCommunicator, IBFPContractCommunicator bfpContractCommunicator, IDocumentSerializer documentSerializer, IOffersCommunicator offersCommunicator)
            : base(documentSerializer)
        {
            _procurementsCommunicator = procurementsCommunicator;
            _bfpContractCommunicator = bfpContractCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutProcurements();

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
            var procurements = (R_10041.Procurements)AppContext.Current.Document;

            AppContext.Current.Document = procurements;
            AppContext.Current.Xml = _documentSerializer.XmlSerializeToString<R_10041.Procurements>(procurements);

            PutProcurements();
            _procurementsCommunicator.SubmitProcurements
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
            PutProcurements();

            base.SaveDraft();
        }

        private void PutProcurements()
        {
            AppContext.Current.WorkingDocument.version =
                _procurementsCommunicator.PutProcurements
                    (
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version
                    ).version;
        }

        private R_10041.Procurements InitAppContext(Guid gid, string access_token, bool isEdit)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.ProcurementsMetadata.Code);

            var contractProcurements = _procurementsCommunicator.GetProcurements(gid, access_token);

            var procurements = _documentSerializer.XmlDeserializeFromString<R_10041.Procurements>(contractProcurements.xml);

            if (isEdit)
            {
                var centralProcurements = _procurementsCommunicator.GetCentralProcurement(access_token);

                R_10041.Procurements.LoadNomenclatures(ref procurements, contractProcurements.ContractProcurementDocuments, centralProcurements);
            }

            AppContext.Current.Document = procurements;
            AppContext.Current.Xml = contractProcurements.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractProcurements.version
            };

            return procurements;
        }
    }
}