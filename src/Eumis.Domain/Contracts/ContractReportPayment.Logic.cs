using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;
using System;
using System.Linq;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportPayment
    {
        public override void SetXml(string xml)
        {
            base.SetXml(xml);

            var paymentDoc = this.GetDocument();

            if (paymentDoc.BasicData != null && paymentDoc.BasicData.Type != null && paymentDoc.BasicData.Type.Value != null)
            {
                var paymentType = paymentDoc.BasicData.Type.Value;

                paymentType = paymentType.First().ToString().ToUpper() + paymentType.Substring(1);

                this.PaymentType = (ContractReportPaymentType)System.Enum.Parse(typeof(ContractReportPaymentType), paymentType);
            }

            this.OtherRegistration = paymentDoc.BasicData.OtherRegistration;
            this.DateFrom = paymentDoc.BasicData.StartDate;
            this.DateTo = paymentDoc.BasicData.EndDate;

            this.AdditionalIncome = paymentDoc.BasicData.AdditionalIncome;
            this.TotalAmount = paymentDoc.BasicData.TotalAmount;
            this.RequestedAmount = paymentDoc.BasicData.FinanceReportAmountWithoutIncome;

            this.ModifyDate = DateTime.Now;
        }

        public void SetAttachedDocumentsActivationDate()
        {
            var payment = this.GetDocument();

            var activationDate = DateTime.Now;

            foreach (var document in payment.AttachedDocuments.AttachedDocumentCollection.Where(d => d.VersionNum == payment.docNumber && d.VersionSubNum == payment.docSubNumber))
            {
                document.ActivationDate = activationDate;
            }

            this.SetXml(payment);
        }

        public void ChangeStatus(ContractReportPaymentStatus status, string note = null)
        {
            this.Status = status;
            this.StatusNote = note;

            this.ModifyDate = DateTime.Now;

            var eventType = this.GetNotificationEventType();

            if (eventType.HasValue)
            {
                ((INotificationEventEmitter)this).NotificationEvents.Add(new ContractNotificationEvent(
                    eventType.Value,
                    this.ContractReportPaymentId,
                    this.ContractReportId,
                    this.ContractId));
            }

            if (this.Status == ContractReportPaymentStatus.Returned)
            {
                ((IEventEmitter)this).Events.Add(new ContractReportReturnedDocumentEvent()
                {
                    ContractReportDocumentType = ContractReportDocumentType.ContractReportPayment,
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
                case ContractReportPaymentStatus.Returned:
                    return NotificationEventType.ContractReportPaymentToReturned;
                case ContractReportPaymentStatus.Actual:
                    if (this.VersionSubNum > 1)
                    {
                        return NotificationEventType.ContractReportPaymentToResent;
                    }

                    break;
                default:
                    break;
            }

            return null;
        }
    }
}
