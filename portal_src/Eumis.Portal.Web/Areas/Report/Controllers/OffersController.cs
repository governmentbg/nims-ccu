using Eumis.Components.Communicators;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    [Authenticated]
    public partial class OffersController : BaseController
    {
        private Guid _contractGid;
        private IOffersCommunicator _offersCommunicator;

        public OffersController(IOffersCommunicator offersCommunicator)
        {
            _contractGid = new Guid(System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData.Values["cgid"].ToString());
            _offersCommunicator = offersCommunicator;
        }

        [HttpGet]
        public virtual ActionResult SubmittedDetails(Guid id)
        {
            var offer = _offersCommunicator.GetRegistrationOfferInfo(CurrentUser.AccessToken, _contractGid, id);

            return View(offer);
        }
    }
}