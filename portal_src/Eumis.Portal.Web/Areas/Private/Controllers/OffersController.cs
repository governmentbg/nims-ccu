using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Documents;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Models.Procurements;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class OffersController : WorkflowController<EditVM>
    {
        private IOffersCommunicator _offersCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public OffersController( IDocumentSerializer documentSerializer, IOffersCommunicator offersCommunicator)
            : base(documentSerializer)
        {
            _offersCommunicator = offersCommunicator;
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Preview(Guid gid, string access_token)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);
            
            AppContext.Current = new AppContext(DocumentMetadata.OfferMetadata.Code);

            var offerXml = _offersCommunicator.PrivateGetRegistrationOffer(access_token, gid).xml;

            AppContext.Current.Document = _documentSerializer.XmlDeserializeFromString<R_10080.Offer>(offerXml);
            AppContext.Current.Xml = offerXml;
            
            return View(AppContext.Current.Document);
        }
    }
}
