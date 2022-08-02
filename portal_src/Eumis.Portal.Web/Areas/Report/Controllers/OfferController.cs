using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Documents;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Portal.Web.Models.Offer;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class OfferController : WorkflowController<EditVM>
    {
        private Guid _contractGid;
        private IOffersCommunicator _offersCommunicator;

        public OfferController(IOffersCommunicator offersCommunicator, IDocumentSerializer documentSerializer) : base(documentSerializer)
        {
            _contractGid = new Guid(System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData.Values["cgid"].ToString());
            _offersCommunicator = offersCommunicator;
        }

        [HttpGet]
        public virtual ActionResult Preview(Guid id)
        {
            AppContext.Current = new AppContext(DocumentMetadata.OfferMetadata.Code);

            var offerXml = _offersCommunicator.GetRegistrationOffer(CurrentUser.AccessToken, _contractGid, id).xml;

            AppContext.Current.Document = _documentSerializer.XmlDeserializeFromString<R_10080.Offer>(offerXml);
            AppContext.Current.Xml = offerXml;

            return View(AppContext.Current.Document);
        }
    }
}