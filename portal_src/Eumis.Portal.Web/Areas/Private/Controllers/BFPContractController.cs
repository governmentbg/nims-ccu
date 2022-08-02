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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using R_10040;
using Eumis.Portal.Web.Models.BFPContract;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class BFPContractController : WorkflowController<EditVM>
    {

        private IProcedureCommunicator _procedureCommunicator;
        private IBFPContractCommunicator _bfpContractCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public BFPContractController(IProcedureCommunicator procedureCommunicator
            , IBFPContractCommunicator bfpContractCommunicator
            , IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _procedureCommunicator = procedureCommunicator;
            _bfpContractCommunicator = bfpContractCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutBFPContract();

            return base.Prepare(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Edit(Guid gid, string access_token)
        {
            this.InitAppContext(gid, access_token, true, false);

            return RedirectToAction("Prepare");
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult EditPartial(Guid gid, string access_token)
        {
            this.InitAppContext(gid, access_token, true, true);

            return RedirectToAction("Prepare");
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Preview(Guid gid, string access_token)
        {
            return View(this.InitAppContext(gid, access_token, true, false));
        }


        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            var contract = (R_10040.BFPContract)AppContext.Current.Document;
            contract.SetActivated();
            AppContext.Current.Document = contract;
            AppContext.Current.Xml = _documentSerializer.XmlSerializeToString<R_10040.BFPContract>(contract);

            PutBFPContract();
            _bfpContractCommunicator.SubmitBFPContract
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
            PutBFPContract();

            base.SaveDraft();
        }

        private void PutBFPContract()
        {
            AppContext.Current.WorkingDocument.version =
                _bfpContractCommunicator.PutBFPContract
                    (
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version
                    ).version;
        }

        private R_10040.BFPContract InitAppContext(Guid gid, string access_token, bool isEdit, bool isPartialReadOnly)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.BFPContractMetadata.Code);

            var contractBFPContract = _bfpContractCommunicator.GetBFPContract(gid, access_token);

            R_10040.BFPContract contract = _documentSerializer.XmlDeserializeFromString<R_10040.BFPContract>(contractBFPContract.xml);
            ContractProcedure procedure = _procedureCommunicator.GetProcedureAppData(new Guid(contract.BFPContractBasicData.ProcedureIdentifier));

            contract = BFPContract.AddMissingProperties(contract, procedure);

            if (isEdit)
            {
                ContractBFPContractData contractData = _bfpContractCommunicator.GetContractData(new Guid(contract.BFPContractBasicData.ProcedureIdentifier), contract.BFPContractBasicData.ProgrammeBasicData.Programme.Code);

                contract = BFPContract.Load(procedure, contractData, contract, isPartialReadOnly);
            }

            contract.SetDeclarationsAttributes(procedure.declarations);

            AppContext.Current.Document = contract;
            AppContext.Current.Xml = contractBFPContract.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractBFPContract.version
            };

            return contract;
        }
    }
}