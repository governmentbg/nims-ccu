using Eumis.Components.Communicators;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers.Attributes;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    [Authenticated]
    public partial class ListController : BaseController
    {
        private IBFPContractCommunicator _bfpContractCommunicator;

        public ListController(IBFPContractCommunicator bfpContractCommunicator)
        {
            _bfpContractCommunicator = bfpContractCommunicator;
        }

        [HttpGet]
        public virtual ActionResult Index(int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var contracts = _bfpContractCommunicator.GetContracts(CurrentUser.AccessToken, offset, Constants.PAGE_ITEMS_COUNT);

            if (contracts.count == 1)
            {
                return RedirectToAction(MVC.Report.BFPContract.ActionNames.Index, MVC.Report.BFPContract.Name,
                    new {area = MVC.Report.Name, cgid = contracts.results.First().gid});
            }

            var model = new StaticPagedList<ContractPVO>(contracts.results, page, Constants.PAGE_ITEMS_COUNT, contracts.count);

            return View(model);
        }
    }
}