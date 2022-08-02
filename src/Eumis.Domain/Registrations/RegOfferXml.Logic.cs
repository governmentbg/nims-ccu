using System;
using Eumis.Common;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Rio;

namespace Eumis.Domain.Registrations
{
    public partial class RegOfferXml : RioXmlDocumentWithFiles<Offer, RegOfferXmlFile>, IAggregateRoot
    {
        public void Update(string xml)
        {
            this.AssertIsDraft();
            this.SetXmlInt(xml);
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToSubmitted()
        {
            this.AssertIsDraft();

            if (this.Tenderer == null)
            {
                throw new DomainValidationException("Tenderer is null");
            }

            if (this.Email == null)
            {
                throw new DomainValidationException("Email is null");
            }

            if (!this.UinType.HasValue)
            {
                throw new DomainValidationException("UinType is null");
            }

            this.Status = RegOfferStatus.Submitted;

            var currentDate = DateTime.Now;
            this.SubmitDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public void ChangeStatusToWithdrawn()
        {
            this.AssertIsSubmitted();

            this.Status = RegOfferStatus.Withdrawn;

            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != RegOfferStatus.Draft)
            {
                throw new DomainValidationException("RegOfferXml status is not draft");
            }
        }

        private void AssertIsSubmitted()
        {
            if (this.Status != RegOfferStatus.Submitted)
            {
                throw new DomainValidationException("RegOfferXml status is not Submitted");
            }
        }

        private void SetXmlInt(string xml)
        {
            this.SetXml(xml);

            var regDocument = this.GetDocument();
            this.Tenderer = regDocument.Get(d => d.Candidate.Name).Truncate(200);
            this.Email = regDocument.Get(d => d.Candidate.Email).Truncate(200);
            this.Uin = regDocument.Get(d => d.Candidate.Uin).Truncate(200);
            var uinTypeId = regDocument.Get(d => d.Candidate.UinType)?.Id;
            if (uinTypeId != null)
            {
                this.UinType = (UinType)Enum.Parse(typeof(UinType), uinTypeId, true);
            }
        }
    }
}
