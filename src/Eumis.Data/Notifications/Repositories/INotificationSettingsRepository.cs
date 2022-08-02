using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Notifications.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Notifications;
using System.Collections.Generic;

namespace Eumis.Data.Notifications.Repositories
{
    public interface INotificationSettingsRepository : IAggregateRepository<NotificationSetting>
    {
        List<int> GetSubscribedUsersForEntry(NotificationEntry notificationEntry, bool isProgrammeDependent);

        List<NotificationSettingVO> GetUserNotificationSettings(int userId);

        List<NotificationSettingVO> GetActiveUserNotificationSettings(int userId);

        NotificationSettingInfoVO GetInfo(int notificationSettingId);

        List<Contracts.ViewObjects.ContractVO> GetAttachedContracts(int notificationSettingId);

        int GetAttachedContractSet(int settingId, int contractId);

        List<ProcedureVO> GetAttachedProcedures(int notificationSettingId);

        int GetAttachedProcedureSet(int settingId, int procedureId);

        List<ProgrammePriorityItemVO> GetAttachedProgrammePriorities(int notificationSettingId);

        int GetAttachedProgrammePrioritySet(int settingId, int programmePriorityId);

        List<EntityCodeNomVO> GetAttachedProgrammes(int notificationSettingId);

        List<EntityCodeNomVO> GetUnattachedProgrammes(int notificationSettingId, int[] programmeIds);

        int GetAttachedPogrammeSet(int settingId, int programmeId);

        void CopyUserSettings(int fromUserId, int toUserId);
    }
}
