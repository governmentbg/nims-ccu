using Eumis.Domain.NonAggregates;
using Eumis.Domain.Notifications.NotificationSets;
using System;
using System.Linq;

namespace Eumis.Domain.Notifications
{
    public partial class NotificationSetting
    {
        public void UpdateAttributes(
            NotificationEvent notificationEvent,
            int userId,
            int? programmeId,
            NotificationScope? scope)
        {
            this.NotificationEventId = notificationEvent.NotificationEventId;
            this.UserId = userId;
            if (notificationEvent.IsProgrammeDependent)
            {
                this.ProgrammeId = programmeId;
                this.Scope = scope;
            }
            else
            {
                this.ProgrammeId = null;
                this.Scope = null;
                this.Set.Clear();
            }

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatus(NotificationSettingStatus status)
        {
            this.Status = status;
            this.ModifyDate = DateTime.Now;
        }

        public void AssertIsDraft()
        {
            if (this.Status != NotificationSettingStatus.Draft)
            {
                throw new DomainException("Cannot edit NotificationSetting when not in 'Draft' status");
            }
        }

        public void AssertIsStoredUser(int userId)
        {
            if (this.UserId != userId)
            {
                throw new UnauthorizedAccessException("The user can't change notification setting");
            }
        }

        public void AssertScopeIsNotChanged(NotificationScope scope)
        {
            if (this.Scope != scope)
            {
                throw new DomainException($"The operation with scope \"{scope}\" is forbidden");
            }
        }

        public void AddContracts(int[] contractIds)
        {
            this.AssertIsDraft();

            foreach (int contractId in contractIds)
            {
                this.Set.Add(new NotificationContractSet() { ContractId = contractId, ModifyDate = DateTime.Now });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void AddProcedures(int[] procedureIds)
        {
            this.AssertIsDraft();

            foreach (int procedureId in procedureIds)
            {
                this.Set.Add(new NotificationProcedureSet() { ProcedureId = procedureId, ModifyDate = DateTime.Now });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void AddProgrammePriorities(int[] attachedPrioritiesIds)
        {
            this.AssertIsDraft();

            foreach (int priorityId in attachedPrioritiesIds)
            {
                this.Set.Add(new NotificationProgrammePrioritySet() { ProgrammePriorityId = priorityId, ModifyDate = DateTime.Now });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void AddProgrammes(int[] attachedProgrammesIds)
        {
            this.AssertIsDraft();

            foreach (int programmeId in attachedProgrammesIds)
            {
                this.Set.Add(new NotificationProgrammeSet() { ProgrammeId = programmeId, ModifyDate = DateTime.Now });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveNotificationSetItem(int setId)
        {
            this.AssertIsDraft();
            var setItem = this.Set.Where(x => x.NotificationSettingSetId == setId).Single();

            this.Set.Remove(setItem);

            this.ModifyDate = DateTime.Now;
        }

        public void ClearObsoleteSets(NotificationScope? newScope)
        {
            if (newScope == null)
            {
                this.Set.Clear();
            }

            var obsoleteSets = this.Set.Where(s => s.Scope != newScope.Value).ToList();
            obsoleteSets.ForEach(x => this.Set.Remove(x));
        }
    }
}
