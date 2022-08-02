using System;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;

namespace Eumis.Domain.Contracts
{
    public partial class ContractProcurementXml
    {
        public override void SetXml(string xml)
        {
            if (this.Status != ContractProcurementStatus.Draft)
            {
                throw new DomainValidationException("Cannot update non-draft procurement's xml");
            }

            this.ModifyDate = DateTime.Now;
            base.SetXml(xml);
        }

        public void SetAttributes(string createNote)
        {
            this.CreateNote = createNote;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDraft()
        {
            if (this.Status != ContractProcurementStatus.Entered)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = ContractProcurementStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered()
        {
            if (this.Status != ContractProcurementStatus.Draft)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = ContractProcurementStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToActive()
        {
            if (this.Status != ContractProcurementStatus.Entered)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = ContractProcurementStatus.Active;
            this.ModifyDate = DateTime.Now;

            ((IEventEmitter)this).Events.Add(new ContractProcurementXmlActivatedEvent()
            {
                ContractId = this.ContractId,
                ContractProcurementXmlId = this.ContractProcurementXmlId,
            });

            ((INotificationEventEmitter)this).NotificationEvents.Add(new ContractNotificationEvent(
                NotificationEventType.ContractProcurementActivated,
                this.ContractId,
                this.ContractProcurementXmlId,
                DispatchParameterIdentifier.Pid));
        }

        public void ChangeStatusToArchived()
        {
            if (this.Status != ContractProcurementStatus.Active)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = ContractProcurementStatus.Archived;
        }
    }
}
