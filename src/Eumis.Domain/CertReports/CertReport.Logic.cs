using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Linq;

namespace Eumis.Domain.CertReports
{
    public partial class CertReport : IAggregateRoot
    {
        public void UpdateAttributes(DateTime regDate, DateTime dateFrom, DateTime dateTo, string reportNumber)
        {
            this.RegDate = regDate;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            this.CertReportNumber = reportNumber;
            this.ModifyDate = DateTime.Now;
        }

        #region CertReportDocument

        public CertReportDocument FindCertReportDocument(int documentId)
        {
            var document = this.CertReportDocuments.Single(d => d.CertReportDocumentId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find CertReportDocument with id " + documentId);
            }

            return document;
        }

        public CertReportDocument CreateCertReportDocument(
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsDraft();

            var newCertReportDocument = new CertReportDocument()
            {
                CertReportId = this.CertReportId,
                Name = name,
                Description = description,
                BlobKey = blobKey,
            };

            this.CertReportDocuments.Add(newCertReportDocument);
            this.ModifyDate = DateTime.Now;

            return newCertReportDocument;
        }

        public void UpdateCertReportDocument(
            int documentId,
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsDraft();

            var certReportDocument = this.FindCertReportDocument(documentId);

            certReportDocument.SetAttributes(
                name,
                description,
                blobKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveCertReportDocument(int documentId)
        {
            this.AssertIsDraft();

            var certReportDocument = this.FindCertReportDocument(documentId);

            this.CertReportDocuments.Remove(certReportDocument);
            this.ModifyDate = DateTime.Now;
        }

        #endregion CertReportDocument

        #region CertReportCertificationDocument

        public CertReportCertificationDocument FindCertReportCertificationDocument(int documentId)
        {
            var document = this.CertReportCertificationDocuments.Single(d => d.CertReportCertificationDocumentId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find CertReportCertificationDocument with id " + documentId);
            }

            return document;
        }

        public CertReportCertificationDocument CreateCertReportCertificationDocument(
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsInCertificationPhase();

            var newCertReportCertificationDocument = new CertReportCertificationDocument()
            {
                CertReportId = this.CertReportId,
                Name = name,
                Description = description,
                BlobKey = blobKey,
            };

            this.CertReportCertificationDocuments.Add(newCertReportCertificationDocument);
            this.ModifyDate = DateTime.Now;

            return newCertReportCertificationDocument;
        }

        public void UpdateCertReportCertificationDocument(
            int documentId,
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsInCertificationPhase();

            var certReportCertificationDocument = this.FindCertReportCertificationDocument(documentId);

            certReportCertificationDocument.SetAttributes(
                name,
                description,
                blobKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveCertReportCertificationDocument(int documentId)
        {
            this.AssertIsInCertificationPhase();

            var certReportCertificationDocument = this.FindCertReportCertificationDocument(documentId);

            this.CertReportCertificationDocuments.Remove(certReportCertificationDocument);
            this.ModifyDate = DateTime.Now;
        }

        #endregion CertReportCertificationDocument

        #region CertReportAttachedCertReport

        public CertReportAttachedCertReport FindCertReportAttachedCertReport(int attachedCertReportId)
        {
            var attachedCertReport = this.CertReportAttachedCertReports.Single(d => d.AttachedCertReportId == attachedCertReportId);

            if (attachedCertReport == null)
            {
                throw new DomainObjectNotFoundException("Cannot find CertReportAttachedCertReport with id " + attachedCertReportId);
            }

            return attachedCertReport;
        }

        public void AttachCertReport(int attachedCertReportId)
        {
            this.AssertIsDraft();

            var newAttachedCertReport = new CertReportAttachedCertReport()
            {
                CertReportId = this.CertReportId,
                AttachedCertReportId = attachedCertReportId,
            };

            this.CertReportAttachedCertReports.Add(newAttachedCertReport);
            this.ModifyDate = DateTime.Now;
        }

        public void RemoveAttachedCertReport(int attachedCertReportId)
        {
            this.AssertIsDraft();

            var attachedCertReport = this.FindCertReportAttachedCertReport(attachedCertReportId);

            this.CertReportAttachedCertReports.Remove(attachedCertReport);
            this.ModifyDate = DateTime.Now;
        }

        #endregion CertReportAttachedCertReport

        public void AssertIsDraft()
        {
            if (this.Status != CertReportStatus.Draft)
            {
                throw new DomainException("Cannot edit CertReport when not in 'Draft' status");
            }
        }

        public void AssertIsInCertificationPhase()
        {
            if (this.Status != CertReportStatus.Unchecked &&
                this.Status != CertReportStatus.Approved &&
                this.Status != CertReportStatus.PartialyApproved &&
                this.Status != CertReportStatus.Unapproved)
            {
                throw new DomainException("CertReport should be in certification phase");
            }
        }

        public void ChangeStatus(CertReportStatus status)
        {
            this.Status = status;
            this.ModifyDate = DateTime.Now;

            Func<CertReportStatus, NotificationEventType?> getNotificationEventType = (s) =>
            {
                switch (s)
                {
                    case CertReportStatus.Ended:
                        return NotificationEventType.CertReportStatusToEnded;
                    case CertReportStatus.Returned:
                        return NotificationEventType.CertReportStatusToReturned;
                    default:
                        break;
                }

                return null;
            };

            var notificationEventType = getNotificationEventType(this.Status);
            if (notificationEventType.HasValue)
            {
                ((INotificationEventEmitter)this).NotificationEvents.Add(new ProgrammeNotificationEvent(
                    notificationEventType.Value,
                    this.CertReportId,
                    this.ProgrammeId));
            }
        }
    }
}
