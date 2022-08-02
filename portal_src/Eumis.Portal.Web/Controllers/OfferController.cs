using System;
using System.Web.Mvc;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Models.Offer;
using Eumis.Components.Communicators;
using Eumis.Components;
using Eumis.Portal.Web.Helpers;
using Eumis.Documents;
using Eumis.Portal.Web.Helpers.Attributes;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    public partial class OfferController : WorkflowController<EditVM>
    {
        private IOffersCommunicator _offersCommunicator;

        public OfferController(IDocumentSerializer documentSerializer, IOffersCommunicator offersCommunicator)
            : base(documentSerializer)
        {
            _offersCommunicator = offersCommunicator;
        }

        [HttpGet]
        public virtual ActionResult New(Guid id)
        {
            var newOffer = _offersCommunicator.CreateRegistrationOffer(id, CurrentUser.AccessToken);

            return RedirectToAction(MVC.Offer.Edit(newOffer.gid.Value));
        }

        [HttpGet]
        public virtual ActionResult Edit(Guid gid)
        {
            var regOffer = _offersCommunicator.GetRegistrationOffer(gid, CurrentUser.AccessToken);

            AppContext.Current = new AppContext(DocumentMetadata.OfferMetadata.Code);
            AppContext.Current.Document = _documentSerializer.XmlDeserializeFromString<R_10080.Offer>(regOffer.xml);
            AppContext.Current.Xml = regOffer.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                version = regOffer.version,
            };

            return RedirectToAction(ActionNames.Prepare);
        }

        [HttpGet]
        public virtual ActionResult Preview(Guid id)
        {
            AppContext.Current = new AppContext(DocumentMetadata.OfferMetadata.Code);

            var offerXml = _offersCommunicator.GetRegistrationOffer(id, CurrentUser.AccessToken).xml;

            AppContext.Current.Document = _documentSerializer.XmlDeserializeFromString<R_10080.Offer>(offerXml);
            AppContext.Current.Xml = offerXml;

            return View(AppContext.Current.Document);
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM model)
        {
            base.Save(model);
            this.PutDraft();
            return base.Prepare(model);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            this.PutDraft();
            base.SaveDraft();
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            var offer = (R_10080.Offer)AppContext.Current.Document;

            AppContext.Current.Document = offer;
            AppContext.Current.Xml = _documentSerializer.XmlSerializeToString<R_10080.Offer>(offer);


            PutDraft();

            _offersCommunicator.SubmitRegistrationOffer(AppContext.Current.WorkingDocument.gid, AppContext.Current.WorkingDocument.version, CurrentUser.AccessToken);

            AppContext.Current.Clear();

            return RedirectToAction(MVC.Offers.ActionNames.Submitted, MVC.Offers.Name);
        }

        private void PutDraft()
        {
            byte[] version;

            if (string.IsNullOrEmpty(AppContext.Current.Xml))
            {
                AppContext.Current.Xml = _documentSerializer.XmlSerializeObjectToString(AppContext.Current.Document);
            }

            var updateDraft = _offersCommunicator.UpdateRegistrationOffer(
                AppContext.Current.WorkingDocument.gid,
                AppContext.Current.Xml,
                AppContext.Current.WorkingDocument.version,
                CurrentUser.AccessToken);

            version = updateDraft.version;

            AppContext.Current.WorkingDocument.version = version;
        }
    }
}