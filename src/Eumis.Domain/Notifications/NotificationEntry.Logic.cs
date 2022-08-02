using System;

namespace Eumis.Domain.Notifications
{
    public partial class NotificationEntry
    {
        public void ChangeStatus(NotificationEntryStatus status)
        {
            this.AssertCanChangeStatus(status);

            this.Status = status;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertCanChangeStatus(NotificationEntryStatus status)
        {
            switch (status)
            {
                case NotificationEntryStatus.Pending:
                    throw new DomainException("Transition to status \"Pending\" is not allowed.");
                case NotificationEntryStatus.UnknownError:
                    if (this.Status == NotificationEntryStatus.Done)
                    {
                        throw new DomainException("Transition to status \"UnknownError\" is not allowed.");
                    }

                    break;
                case NotificationEntryStatus.Done:
                    if (this.Status == NotificationEntryStatus.Done)
                    {
                        throw new DomainException("Transition to status \"Done\" is not allowed.");
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
