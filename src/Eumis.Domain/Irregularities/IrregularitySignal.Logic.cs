using System;
using System.Linq;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularitySignal
    {
        #region IrregularitySignal

        public void UpdateSignalData(
            DateTime? regDate,
            string signalSource,
            DateTime? maSystemRegDate,
            string signalKind,
            string violationDesrc,
            string actions,
            string actRegNum,
            DateTime? actRegDate,
            string comment)
        {
            this.AssertIsDraft();

            this.RegDate = regDate;
            this.SignalSource = signalSource;
            this.MASystemRegDate = maSystemRegDate;
            this.SignalKind = signalKind;
            this.ViolationDesrc = violationDesrc;
            this.Actions = actions;
            this.ActRegNum = actRegNum;
            this.ActRegDate = actRegDate;
            this.Comment = comment;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateSignalBasicData(
            string regNumber)
        {
            this.AssertIsNotRemoved();

            this.RegNumber = regNumber;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToActive(int? signalNum = null)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                this.IsActivated = true;
                this.Number = signalNum;
                this.RegNumber = signalNum.ToString();
            }

            this.Status = IrregularitySignalStatus.Active;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEnded()
        {
            if (this.Status != IrregularitySignalStatus.Active)
            {
                throw new DomainValidationException("Irregularity signal status must be active!");
            }

            this.Status = IrregularitySignalStatus.Ended;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToRemoved(string deleteComment)
        {
            if (this.Status != IrregularitySignalStatus.Draft)
            {
                throw new DomainValidationException("Irregularity signal status must be draft!");
            }

            if (!this.IsActivated)
            {
                throw new DomainValidationException("Irregularity signal must not be activated!");
            }

            this.Status = IrregularitySignalStatus.Removed;
            this.DeleteNote = deleteComment;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDraft()
        {
            if (this.Status == IrregularitySignalStatus.Draft)
            {
                throw new DomainValidationException("Irregularity signal status must not be draft!");
            }

            this.Status = IrregularitySignalStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != IrregularitySignalStatus.Draft)
            {
                throw new DomainValidationException("Irregularity signal status must be draft!");
            }
        }

        private void AssertIsNotRemoved()
        {
            if (this.Status == IrregularitySignalStatus.Removed)
            {
                throw new DomainValidationException("Irregularity signal status must not be removed!");
            }
        }

        #endregion //IrregularitySignal

        #region IrregularitySignalInvolvedPerson

        public IrregularitySignalInvolvedPerson AddInvolvedPerson(
            string uin,
            UinType uinType,
            string firstName,
            string middleName,
            string lastName,
            int? countryId,
            int? settlementId,
            string postCode,
            string street,
            string address)
        {
            this.AssertIsDraft();

            var newPerson = new IrregularitySignalInvolvedPerson()
            {
                IrregularitySignalId = this.IrregularitySignalId,
                LegalType = InvolvedPersonLegalType.Person,
                Uin = uin,
                UinType = uinType,
                CompanyName = null,
                TradeName = null,
                HoldingName = null,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                CountryId = countryId,
                SettlementId = settlementId,
                PostCode = postCode,
                Street = street,
                Address = address,
            };

            this.InvolvedPersons.Add(newPerson);
            this.ModifyDate = DateTime.Now;

            return newPerson;
        }

        public IrregularitySignalInvolvedPerson AddInvolvedLegalPerson(
            string uin,
            UinType uinType,
            string companyName,
            string tradeName,
            string holdingName,
            int? countryId,
            int? settlementId,
            string postCode,
            string street,
            string address)
        {
            this.AssertIsDraft();

            var newPerson = new IrregularitySignalInvolvedPerson()
            {
                IrregularitySignalId = this.IrregularitySignalId,
                LegalType = InvolvedPersonLegalType.LegalPerson,
                Uin = uin,
                UinType = uinType,
                CompanyName = companyName,
                TradeName = tradeName,
                HoldingName = holdingName,
                FirstName = null,
                MiddleName = null,
                LastName = null,
                CountryId = countryId,
                SettlementId = settlementId,
                PostCode = postCode,
                Street = street,
                Address = address,
            };

            this.InvolvedPersons.Add(newPerson);
            this.ModifyDate = DateTime.Now;

            return newPerson;
        }

        public IrregularitySignalInvolvedPerson GetInvolvedPerson(int personId)
        {
            var person = this.InvolvedPersons.Single(ip => ip.IrregularitySignalInvolvedPersonId == personId);

            if (person == null)
            {
                throw new DomainObjectNotFoundException("Cannot find IrregularitySignalInvolvedPerson with id " + personId);
            }

            return person;
        }

        public void UpdateInvolvedPerson(
            int personId,
            string uin,
            UinType uinType,
            string firstName,
            string middleName,
            string lastName,
            int? countryId,
            int? settlementId,
            string postCode,
            string street,
            string address)
        {
            this.AssertIsDraft();

            var person = this.GetInvolvedPerson(personId);
            person.SetAttributes(
                InvolvedPersonLegalType.Person,
                uin,
                uinType,
                null,
                null,
                null,
                firstName,
                middleName,
                lastName,
                countryId,
                settlementId,
                postCode,
                street,
                address);

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateInvolvedLegalPerson(
            int personId,
            string uin,
            UinType uinType,
            string companyName,
            string tradeName,
            string holdingName,
            int? countryId,
            int? settlementId,
            string postCode,
            string street,
            string address)
        {
            this.AssertIsDraft();

            var person = this.GetInvolvedPerson(personId);
            person.SetAttributes(
                InvolvedPersonLegalType.LegalPerson,
                uin,
                uinType,
                companyName,
                tradeName,
                holdingName,
                null,
                null,
                null,
                countryId,
                settlementId,
                postCode,
                street,
                address);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveInvolvedPerson(int personId)
        {
            this.AssertIsDraft();

            var person = this.GetInvolvedPerson(personId);
            this.InvolvedPersons.Remove(person);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //IrregularitySignalInvolvedPerson

        #region IrregularitySignalDoc

        public void AddDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            this.Documents.Add(new IrregularitySignalDoc()
            {
                IrregularitySignalId = this.IrregularitySignalId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public IrregularitySignalDoc GetDocument(int documentId)
        {
            var document = this.Documents.Single(d => d.IrregularitySignalDocId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find IrregularitySignalDoc with id " + documentId);
            }

            return document;
        }

        public void UpdateDocument(
            int documentId,
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            var document = this.GetDocument(documentId);
            document.SetAttributes(description, fileName, fileKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveDocument(int documentId)
        {
            this.AssertIsDraft();

            var document = this.GetDocument(documentId);
            this.Documents.Remove(document);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //IrregularitySignalDoc
    }
}
