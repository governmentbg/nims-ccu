using System;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;

namespace Eumis.Domain.Contracts
{
    public partial class ContractCommunicationXml
    {
        public override void SetXml(string xml)
        {
            base.SetXml(xml);
            var communicationDoc = this.GetDocument();

            this.Subject = communicationDoc.Subject;
            this.Content = communicationDoc.Content;
            this.ModifyDate = DateTime.Now;
        }

        public void SetReadDate()
        {
            var currentDate = DateTime.Now;
            this.ReadDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public void Activate(string regNumber)
        {
            if (this.Status != ContractCommunicationStatus.Draft)
            {
                throw new DomainValidationException("ContractCommunication can be activated only if it is in status Draft.");
            }

            var currentDate = DateTime.Now;

            this.RegNumber = regNumber;
            this.Status = ContractCommunicationStatus.Sent;

            this.SendDate = currentDate;
            this.ModifyDate = currentDate;

            if (this.Source == Contracts.Source.Beneficiary)
            {
                Func<ContractCommunicationType, NotificationEventType?> getNotificationEventType = (s) =>
                {
                    switch (s)
                    {
                        case ContractCommunicationType.Administrative:
                            return NotificationEventType.ContractCommunicationReceived;
                        case ContractCommunicationType.Cert:
                            return NotificationEventType.CertAuthorityCommunicationReceived;
                        case ContractCommunicationType.Audit:
                            return NotificationEventType.AuditAuthorityCommunicationReceived;
                        default:
                            return null;
                    }
                };

                var notificationType = getNotificationEventType(this.Type);

                if (notificationType.HasValue)
                {
                    ((INotificationEventEmitter)this).NotificationEvents.Add(new ContractNotificationEvent(
                        notificationType.Value,
                        this.ContractId,
                        this.ContractCommunicationXmlId,
                        DispatchParameterIdentifier.Ind));
                }
            }
            else if (this.Source == Contracts.Source.AdministrativeAuthority)
            {
                ((IEventEmitter)this).Events.Add(new ContractAuthorityCommunicationSentEvent() { ContractId = this.ContractId, ContractCommunicationXmlId = this.ContractCommunicationXmlId });
            }
            else
            {
                throw new DomainException("Unknown source");
            }
        }
    }
}
