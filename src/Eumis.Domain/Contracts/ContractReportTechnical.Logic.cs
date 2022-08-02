using System;
using System.Linq;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;
using Eumis.Rio;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportTechnical
    {
        public override void SetXml(string xml)
        {
            base.SetXml(xml);

            var technicalDoc = this.GetDocument();
            this.DateFrom = technicalDoc.BasicData.StartDate;
            this.DateTo = technicalDoc.BasicData.EndDate;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeIndicatorPortalAccessibility(Guid contractIndicatorGid, Domain.Contracts.ContractReportIndicatorStatus status)
        {
            bool isLocked = false;

            switch (status)
            {
                case ContractReportIndicatorStatus.Draft:
                    isLocked = false;
                    break;
                case ContractReportIndicatorStatus.Ended:
                    isLocked = true;
                    break;
                default:
                    break;
            }

            this.SetDocumentAttributes(this.GetDocument(), contractIndicatorGid, isLocked);
        }

        private void SetDocumentAttributes(TechnicalReport technicalReport, Guid contractIndicatorGid, bool isLocked)
        {
            technicalReport.modificationDate = DateTime.Now;
            technicalReport.Indicators.IndicatorCollection.Single(i => i.BFPContractIndicator.gid == contractIndicatorGid.ToString()).BFPContractIndicator.IsLocked = isLocked;
            this.SetXml(technicalReport);
        }

        public bool IsTechnicalIndicatorToDelete(Guid contractIndicatorGid)
        {
            var technicalReport = this.GetDocument();

            return technicalReport
                .Indicators.IndicatorCollection.Any(i =>
                i.BFPContractIndicator.gid == contractIndicatorGid.ToString() &&
                i.BFPContractIndicator.IsLocked == false);
        }

        public void SetAttachedDocumentsActivationDate()
        {
            var technicalReport = this.GetDocument();

            var activationDate = DateTime.Now;

            foreach (var document in technicalReport.AttachedDocuments.AttachedDocumentCollection.Where(d => d.VersionNum == technicalReport.docNumber && d.VersionSubNum == technicalReport.docSubNumber))
            {
                document.ActivationDate = activationDate;
            }

            this.SetXml(technicalReport);
        }

        public void ChangeStatus(ContractReportTechnicalStatus status, string note = null)
        {
            this.Status = status;
            this.StatusNote = note;
            this.ModifyDate = DateTime.Now;

            var eventType = this.GetNotificationEventType();
            if (eventType.HasValue)
            {
                ((INotificationEventEmitter)this).NotificationEvents.Add(new ContractNotificationEvent(
                    eventType.Value,
                    this.ContractReportTechnicalId,
                    this.ContractReportId,
                    this.ContractId));
            }

            if (this.Status == ContractReportTechnicalStatus.Returned)
            {
                ((IEventEmitter)this).Events.Add(new ContractReportReturnedDocumentEvent()
                {
                    ContractReportDocumentType = ContractReportDocumentType.ContractReportTechnical,
                    ContractReportId = this.ContractReportId,
                    VersionNum = this.VersionNum,
                    VersionSubNum = this.VersionSubNum,
                });
            }
        }

        private NotificationEventType? GetNotificationEventType()
        {
            switch (this.Status)
            {
                case ContractReportTechnicalStatus.Returned:
                    return NotificationEventType.ContractReportTechnicalToReturned;
                case ContractReportTechnicalStatus.Actual:
                    if (this.VersionSubNum > 1)
                    {
                        return NotificationEventType.ContractReportTechnicalToResent;
                    }

                    break;
                default:
                    break;
            }

            return null;
        }
    }
}
