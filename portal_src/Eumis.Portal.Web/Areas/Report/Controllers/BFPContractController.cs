using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Areas.Report.Models.BFPContract;
using Eumis.Portal.Web.Controllers.Base;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Portal.Web.Models.BFPContract;
using Eumis.Documents;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;
using Eumis.Documents.Mappers;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class BFPContractController : WorkflowController<EditVM>
    {
        private Guid _contractGid;
        private IBFPContractCommunicator _bfpContractCommunicator;
        private IProcurementsCommunicator _procurementsCommunicator;
        private ISpendingPlanCommunicator _spendingPlanCommunicator;
        private IOffersCommunicator _offersCommunicator;
        private IProcedureCommunicator _procedureCommunicator;

        public BFPContractController(IProcedureCommunicator procedureCommunicator
            , IBFPContractCommunicator bfpContractCommunicator
            , IProcurementsCommunicator procurementsCommunicator
            , ISpendingPlanCommunicator spendingPlanCommunicator
            , IOffersCommunicator offersCommunicator
            , IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _bfpContractCommunicator = bfpContractCommunicator;
            _procurementsCommunicator = procurementsCommunicator;
            _spendingPlanCommunicator = spendingPlanCommunicator;
            _contractGid = new Guid(System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData.Values["cgid"].ToString());
            _offersCommunicator = offersCommunicator;
            _procedureCommunicator = procedureCommunicator;
        }
        public virtual ActionResult Index(int cpage = 1, int ppage = 1, int spage = 1, int opage = 1)
        {
            int coffset = (cpage - 1) * Constants.PAGE_ITEMS_COUNT;
            var contracts = _bfpContractCommunicator.GetContractVersions(CurrentUser.AccessToken, _contractGid, coffset, Constants.PAGE_ITEMS_COUNT);

            int poffset = (ppage - 1) * Constants.PAGE_ITEMS_COUNT;
            var procurements = _procurementsCommunicator.GetContractProcurements(CurrentUser.AccessToken, _contractGid, poffset, Constants.PAGE_ITEMS_COUNT);

            int soffset = (spage - 1) * Constants.PAGE_ITEMS_COUNT;
            var spendingPlans = _spendingPlanCommunicator.GetContractSpendingPlans(CurrentUser.AccessToken, _contractGid, soffset, Constants.PAGE_ITEMS_COUNT);

            var model = new IndexVM
            {
                ContractVersions = new StaticPagedList<ContractVersionPVO>(contracts.results, cpage, Constants.PAGE_ITEMS_COUNT, contracts.count),

                ProcurementVersions = new StaticPagedList<ContractProcurementPVO>(procurements.results, ppage, Constants.PAGE_ITEMS_COUNT, procurements.count),
                CanCreateProcurement = procurements.canCreate,

                SpendingPlanVersions = new StaticPagedList<ContractSpendingPlanPVO>(spendingPlans.results, ppage, Constants.PAGE_ITEMS_COUNT, spendingPlans.count),
                CanCreateSpendingPlan = spendingPlans.canCreate,
            };

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Preview(Guid gid)
        {
            var version = _bfpContractCommunicator.GetContractVersion(CurrentUser.AccessToken, _contractGid, gid);

            var contract = _documentSerializer.XmlDeserializeFromString<R_10040.BFPContract>(version.xml);

            ContractProcedure procedure = _procedureCommunicator.GetProcedureAppData(new Guid(contract.BFPContractBasicData.ProcedureIdentifier));
            
            contract = R_10040.BFPContract.AddMissingProperties(contract, procedure);

            contract.SetDeclarationsAttributes(procedure.declarations);

            AppContext.Current = new AppContext(DocumentMetadata.BFPContractMetadata.Code);
            AppContext.Current.Document = contract;

            return View(contract);
        }

    }
}