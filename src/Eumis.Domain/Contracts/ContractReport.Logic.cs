using System;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReport
    {
        public static ContractReportStatus[] GetCreationStatuses()
        {
            return CreationStatuses;
        }

        public void UpdateAttributes(
            ContractReportType contractReportType,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline)
        {
            this.ReportType = contractReportType;
            this.OtherRegistration = otherRegistration;
            this.StoragePlace = storagePlace;
            this.SubmitDate = submitDate;
            this.SubmitDeadline = submitDeadline;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateCheckAttributes(DateTime? checkedDate)
        {
            this.CheckedDate = checkedDate;

            this.ModifyDate = DateTime.Now;
        }

        public void SetStatus(ContractReportStatus status, string statusNote = null)
        {
            if (this.Status != status)
            {
                this.Status = status;

                if (status == ContractReportStatus.Refused)
                {
                    this.StatusNote = statusNote;
                }

                var notificationEvent = this.GetNotificationEventType();

                if (notificationEvent.HasValue)
                {
                    ((INotificationEventEmitter)this).NotificationEvents.Add(new ContractNotificationEvent(
                        notificationEvent.Value,
                        this.ContractReportId,
                        this.ContractId));
                }
            }

            this.ModifyDate = DateTime.Now;
        }

        public void AssertIsNotBeneficiary()
        {
            if (this.Source == Source.Beneficiary)
            {
                throw new UnauthorizedAccessException("ContractReport has source 'Beneficiary'");
            }
        }

        public void AssertIsNotDraftFromBeneficiary()
        {
            if (this.Status == ContractReportStatus.Draft && this.Source == Source.Beneficiary)
            {
                throw new UnauthorizedAccessException("Cannot get/edit/delete ContractReport with status 'Draft' and source 'Beneficiary'");
            }
        }

        public void AssertIsDraftContractReport()
        {
            if (this.Status != ContractReportStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReport when not in 'Draft' status");
            }
        }

        public void AssertIsUncheckedContractReport()
        {
            if (this.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit ContractReport when not in 'Unchecked' status");
            }
        }

        public void AssertIsDraftOrUncheckedContractReport()
        {
            if (this.Status != ContractReportStatus.Draft && this.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit ContractReport when not in 'Draft' or 'Unchecked' status");
            }
        }

        public void AssertIsDraftOrSentCheckedOrUncheckedContractReport()
        {
            if (this.Status != ContractReportStatus.Draft && this.Status != ContractReportStatus.SentChecked && this.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit ContractReport when not in 'Draft', 'SentChecked' or 'Unchecked' status");
            }
        }

        public void AssertIsNotSentCheckedContractReport()
        {
            if (this.Status == ContractReportStatus.SentChecked)
            {
                throw new DomainException("Cannot edit ContractReport when in 'SentChecked' status");
            }
        }

        private NotificationEventType? GetNotificationEventType()
        {
            switch (this.Status)
            {
                case ContractReportStatus.SentChecked:
                    return NotificationEventType.ContractReportSentUnchecked;
                default:
                    break;
            }

            return null;
        }
    }
}
