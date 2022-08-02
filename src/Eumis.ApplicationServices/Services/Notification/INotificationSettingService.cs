using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Notifications;
using Eumis.Domain.Notifications.DataObjects;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.Notification
{
    public interface INotificationSettingService
    {
        NotificationSetting CreateNotificationSetting(NotificationSettingDO notificationSetting, int userId);

        void UpdateNotificationSetting(NotificationSettingDO notificationSetting, int userId);

        void ChangeStatus(int notificationSettingId, NotificationSettingStatus status, byte[] version, int userId);

        void DeleteSetting(int notificationSettingId, byte[] vers, int userId);

        List<ContractVO> GetUnattachedContracts(int notificationSettingId);

        List<ContractVO> GetAttachedContracts(int notificationSettingId);

        void AddSelectedContracts(int notificationSettingId, byte[] vers, int[] attachedContractIds, int userId);

        void RemoveSelectedContract(int notificationSettingId, byte[] vers, int contractId, int userId);

        List<ProcedureVO> GetUnattachedProcedures(int notificationSettingId);

        List<ProcedureVO> GetAttachedProcedures(int notificationSettingId);

        void AddSelectedProcedures(int notificationSettingId, byte[] vers, int[] attachedProcedureIds, int userId);

        void RemoveSelectedProcedure(int notificationSettingId, byte[] vers, int procedureId, int userId);

        List<ProgrammePriorityItemVO> GetUnattachedProgrammePriorities(int notificationSettingId);

        List<ProgrammePriorityItemVO> GetAttachedProgrammePriorities(int notificationSettingId);

        void AddSelectedProgrammePriorities(int notificationSettingId, byte[] vers, int[] attachedPrioritiesIds, int userId);

        void RemoveSelectedProgrammePriority(int notificationSettingId, byte[] vers, int programmePriorityId, int userId);

        List<EntityCodeNomVO> GetUnattachedProgrammes(int notificationSettingId, int userId);

        List<EntityCodeNomVO> GetAttachedProgrammes(int notificationSettingId);

        void AddSelectedProgrammes(int notificationSettingId, byte[] vers, int[] attachedProgrammesIds, int userId);

        void RemoveSelectedProgramme(int notificationSettingId, byte[] vers, int programmeId, int userId);
    }
}
