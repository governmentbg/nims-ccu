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
using PagedList;
using Eumis.Portal.Web.Areas.Report.Models.MicroData;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class MicroDataController : BaseController
    {
        private IMicroDataCommunicator _microDataCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public MicroDataController( IMicroDataCommunicator microDataCommunicator)
        {
            _microDataCommunicator = microDataCommunicator;
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Type1(Guid gid, string access_token, int page = 1)
        {
            int offset = (page - 1) * Constants.MICRO_PAGE_ITEMS_COUNT;
            var items = _microDataCommunicator.PrivateGetType1(gid, access_token, Constants.MICRO_PAGE_ITEMS_COUNT, offset);

            var model = new MicroStaticPagedList<ContractReportMicroType1ItemPVO>(items.items.results, page, Constants.MICRO_PAGE_ITEMS_COUNT, items.items.count, items.versionNum, items.contractNumber);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Type2(Guid gid, string access_token, int page = 1)
        {
            int offset = (page - 1) * Constants.MICRO_PAGE_ITEMS_COUNT;
            var items = _microDataCommunicator.PrivateGetType2(gid, access_token, Constants.MICRO_PAGE_ITEMS_COUNT, offset);

            var model = new MicroStaticPagedList<ContractReportMicroType2ItemPVO>(items.items.results, page, Constants.MICRO_PAGE_ITEMS_COUNT, items.items.count, items.versionNum, items.contractNumber);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Type3(Guid gid, string access_token, int page = 1)
        {
            int offset = (page - 1) * Constants.MICRO_PAGE_ITEMS_COUNT;
            var items = _microDataCommunicator.PrivateGetType3(gid, access_token, Constants.MICRO_PAGE_ITEMS_COUNT, offset);

            var model = new MicroStaticPagedList<ContractReportMicroType3ItemPVO>(items.items.results, page, Constants.MICRO_PAGE_ITEMS_COUNT, items.items.count, items.versionNum, items.contractNumber);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Type4(Guid gid, string access_token, int page = 1)
        {
            int offset = (page - 1) * Constants.MICRO_PAGE_ITEMS_COUNT;
            var items = _microDataCommunicator.PrivateGetType4(gid, access_token, Constants.MICRO_PAGE_ITEMS_COUNT, offset);

            var model = new MicroStaticPagedList<ContractReportMicroType4ItemPVO>(items.items.results, page, Constants.MICRO_PAGE_ITEMS_COUNT, items.items.count, items.versionNum, items.contractNumber);

            return View(model);
        }
    }
}