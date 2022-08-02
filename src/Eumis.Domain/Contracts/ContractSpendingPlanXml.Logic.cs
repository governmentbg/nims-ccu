using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;
using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractSpendingPlanXml
    {
        public override void SetXml(string xml)
        {
            if (this.Status != ContractSpendingPlanStatus.Draft)
            {
                throw new DomainValidationException("Cannot update non-draft SpendingPlan's xml");
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
            if (this.Status != ContractSpendingPlanStatus.Entered)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = ContractSpendingPlanStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered()
        {
            if (this.Status != ContractSpendingPlanStatus.Draft)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = ContractSpendingPlanStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToActive()
        {
            if (this.Status != ContractSpendingPlanStatus.Entered)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            ((INotificationEventEmitter)this).NotificationEvents.Add(new ContractNotificationEvent(
                NotificationEventType.ContractSpendingPlanActivated,
                this.ContractId,
                this.ContractSpendingPlanXmlId,
                DispatchParameterIdentifier.Spid));

            this.Status = ContractSpendingPlanStatus.Active;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToArchived()
        {
            if (this.Status != ContractSpendingPlanStatus.Active)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = ContractSpendingPlanStatus.Archived;
            this.ModifyDate = DateTime.Now;
        }
    }
}
