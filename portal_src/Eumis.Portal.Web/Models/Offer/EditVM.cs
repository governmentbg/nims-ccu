using System.Reflection;
using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Documents.Contracts;
using Eumis.Documents.Validation;
using Eumis.Portal.Web.Helpers;
using R_10040;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Models.Offer
{
    public class EditVM : BaseVM, IEditVM<R_10080.Offer>, IEngineValidatable, IRemoteValidatable
    {
        public string PositionGid { get; set; }

        public R_10079.OfferBasicData BasicData { get; set; }

        public R_10004.Company Candidate { get; set; }

        public R_10080.OfferAttachedDocuments AttachedDocuments { get; set; }

        public List<string> RemoteValidationErrors { get; set; }
        public List<string> RemoteValidationWarnings { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10080.Offer offer)
        {
            this.PositionGid = offer.positionGid;

            this.BasicData = offer.BasicData;

            this.Candidate = offer.Candidate;

            this.AttachedDocuments = offer.AttachedDocuments;
        }

        public R_10080.Offer Set(R_10080.Offer offer)
        {
            return offer;
        }

        public R_10080.Offer SetAsync()
        {
            var offer = (R_10080.Offer)AppContext.Current.Document;

            if (!(offer.BasicData != null && offer.BasicData.isLocked))
            {
                offer.BasicData.BeneficiaryRegistrationVAT = this.BasicData.BeneficiaryRegistrationVAT;
            }

            if (!(offer.Candidate != null && offer.Candidate.isLocked))
            {
                if (this.Candidate != null && offer.Candidate != null)
                {
                    this.Candidate.id = offer.Candidate.id;
                    this.Candidate.isLocked = offer.Candidate.isLocked;
                }

                offer.Candidate = this.Candidate;
            }

            if (!(offer.AttachedDocuments != null && offer.AttachedDocuments.isLocked))
            {
                if (this.AttachedDocuments == null)
                    offer.AttachedDocuments.AttachedDocumentCollection = new R_10080.AttachedDocumentCollection();
                else
                    offer.AttachedDocuments.AttachedDocumentCollection = this.AttachedDocuments.AttachedDocumentCollection;
            }

            return offer;
        }

        #endregion
    }
}