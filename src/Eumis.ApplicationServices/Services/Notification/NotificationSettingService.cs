using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Notifications;
using Eumis.Domain.Notifications.DataObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.Notification
{
    public class NotificationSettingService : INotificationSettingService
    {
        private IUnitOfWork unitOfWork;
        private INotificationSettingsRepository notificationSettingsRepository;
        private INotificationEventsNomsRepository notificationEventsNomsRepository;
        private IContractsRepository contractRepository;
        private IProceduresRepository proceduresRepository;
        private IPermissionsRepository permissionsRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;

        public NotificationSettingService(
            INotificationSettingsRepository notificationSettingsRepository,
            INotificationEventsNomsRepository notificationEventsNomsRepository,
            IContractsRepository contractRepository,
            IProceduresRepository proceduresRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IPermissionsRepository permissionsRepository,
            IUnitOfWork unitOfWork)
        {
            this.notificationSettingsRepository = notificationSettingsRepository;
            this.notificationEventsNomsRepository = notificationEventsNomsRepository;
            this.contractRepository = contractRepository;
            this.proceduresRepository = proceduresRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.permissionsRepository = permissionsRepository;
            this.unitOfWork = unitOfWork;
        }

        public NotificationSetting CreateNotificationSetting(NotificationSettingDO notificationSetting, int userId)
        {
            var notificationEvent = this.notificationEventsNomsRepository.Find(notificationSetting.NotificationEventId);
            var newNotificationSetting = new NotificationSetting(notificationEvent);

            newNotificationSetting.UpdateAttributes(
                notificationEvent,
                userId,
                notificationSetting.ProgrammeId,
                notificationSetting.Scope);

            this.notificationSettingsRepository.Add(newNotificationSetting);

            this.unitOfWork.Save();

            return newNotificationSetting;
        }

        public void UpdateNotificationSetting(NotificationSettingDO notificationSetting, int userId)
        {
            var notificationEvent = this.notificationEventsNomsRepository.Find(notificationSetting.NotificationEventId);
            var setting = this.notificationSettingsRepository.FindForUpdate(notificationSetting.NotificationSettingId, notificationSetting.Version);

            setting.AssertIsStoredUser(userId);

            setting.ClearObsoleteSets(notificationSetting.Scope);

            setting.UpdateAttributes(
                notificationEvent,
                userId,
                notificationSetting.ProgrammeId,
                notificationSetting.Scope);

            this.unitOfWork.Save();
        }

        public void ChangeStatus(int notificationSettingId, NotificationSettingStatus status, byte[] version, int userId)
        {
            var setting = this.notificationSettingsRepository.FindForUpdate(notificationSettingId, version);

            setting.AssertIsStoredUser(userId);

            setting.ChangeStatus(status);
            this.unitOfWork.Save();
        }

        public void DeleteSetting(int notificationSettingId, byte[] vers, int userId)
        {
            var notificationSetting = this.notificationSettingsRepository.Find(notificationSettingId);
            notificationSetting.AssertIsStoredUser(userId);
            notificationSetting.AssertIsDraft();
            this.notificationSettingsRepository.Remove(notificationSetting);

            this.unitOfWork.Save();
        }

        #region AttachedContracts
        public List<ContractVO> GetUnattachedContracts(int notificationSettingId)
        {
            var notificationSetting = this.notificationSettingsRepository.Find(notificationSettingId);

            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Contract);

            var attachedContracts = this.notificationSettingsRepository.GetAttachedContracts(notificationSettingId)
                .Select(x => x.ContractId)
                .ToList();

            var programmeContracts = this.contractRepository.GetContracts(new int[] { (int)notificationSetting.ProgrammeId }, null);

            return programmeContracts
                .Where(x => !attachedContracts.Contains(x.ContractId))
                .ToList();
        }

        public List<ContractVO> GetAttachedContracts(int notificationSettingId)
        {
            var notificationSetting = this.notificationSettingsRepository.Find(notificationSettingId);

            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Contract);

            return this.notificationSettingsRepository.GetAttachedContracts(notificationSettingId);
        }

        public void AddSelectedContracts(int notificationSettingId, byte[] vers, int[] attachedContractIds, int userId)
        {
            var notificationSetting = this.notificationSettingsRepository.FindForUpdate(notificationSettingId, vers);

            notificationSetting.AssertIsStoredUser(userId);
            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Contract);

            notificationSetting.AddContracts(attachedContractIds);

            this.unitOfWork.Save();
        }

        public void RemoveSelectedContract(int notificationSettingId, byte[] vers, int contractId, int userId)
        {
            var notificationSetting = this.notificationSettingsRepository.FindForUpdate(notificationSettingId, vers);

            notificationSetting.AssertIsStoredUser(userId);
            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Contract);

            var contractSetId = this.notificationSettingsRepository.GetAttachedContractSet(notificationSettingId, contractId);
            notificationSetting.RemoveNotificationSetItem(contractSetId);

            this.unitOfWork.Save();
        }

        #endregion

        #region AttachedProcedures

        public List<ProcedureVO> GetUnattachedProcedures(int notificationSettingId)
        {
            var notificationSetting = this.notificationSettingsRepository.Find(notificationSettingId);

            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Procedure);

            var attachedProcedures = this.notificationSettingsRepository.GetAttachedProcedures(notificationSettingId)
                .Select(x => x.ProcedureId)
                .ToList();

            var programmeProcedures = this.proceduresRepository.GetProcedures(new int[] { (int)notificationSetting.ProgrammeId });

            return programmeProcedures
                .Where(x => !attachedProcedures.Contains(x.ProcedureId))
                .ToList();
        }

        public List<ProcedureVO> GetAttachedProcedures(int notificationSettingId)
        {
            var notificationSetting = this.notificationSettingsRepository.Find(notificationSettingId);

            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Procedure);

            return this.notificationSettingsRepository.GetAttachedProcedures(notificationSettingId);
        }

        public void AddSelectedProcedures(int notificationSettingId, byte[] vers, int[] attachedProcedureIds, int userId)
        {
            var notificationSetting = this.notificationSettingsRepository.FindForUpdate(notificationSettingId, vers);

            notificationSetting.AssertIsStoredUser(userId);
            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Procedure);

            notificationSetting.AddProcedures(attachedProcedureIds);

            this.unitOfWork.Save();
        }

        public void RemoveSelectedProcedure(int notificationSettingId, byte[] vers, int procedureId, int userId)
        {
            var notificationSetting = this.notificationSettingsRepository.FindForUpdate(notificationSettingId, vers);

            notificationSetting.AssertIsStoredUser(userId);
            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Procedure);

            var procedureSetId = this.notificationSettingsRepository.GetAttachedProcedureSet(notificationSettingId, procedureId);
            notificationSetting.RemoveNotificationSetItem(procedureSetId);

            this.unitOfWork.Save();
        }

        #endregion

        #region AttachedProgrammePriorities

        public List<ProgrammePriorityItemVO> GetUnattachedProgrammePriorities(int notificationSettingId)
        {
            var notificationSetting = this.notificationSettingsRepository.Find(notificationSettingId);

            notificationSetting.AssertScopeIsNotChanged(NotificationScope.ProgrammePriority);

            var attachedProgrammePriorities = this.notificationSettingsRepository.GetAttachedProgrammePriorities(notificationSettingId)
                .Select(x => x.ItemId)
                .ToArray();

            var programmePriorities = this.programmePrioritiesRepository.GetProgrammePriorityItems((int)notificationSetting.ProgrammeId, attachedProgrammePriorities);

            return programmePriorities.ToList();
        }

        public List<ProgrammePriorityItemVO> GetAttachedProgrammePriorities(int notificationSettingId)
        {
            var notificationSetting = this.notificationSettingsRepository.Find(notificationSettingId);

            notificationSetting.AssertScopeIsNotChanged(NotificationScope.ProgrammePriority);

            return this.notificationSettingsRepository.GetAttachedProgrammePriorities(notificationSettingId);
        }

        public void AddSelectedProgrammePriorities(int notificationSettingId, byte[] vers, int[] attachedPrioritiesIds, int userId)
        {
            var notificationSetting = this.notificationSettingsRepository.FindForUpdate(notificationSettingId, vers);

            notificationSetting.AssertIsStoredUser(userId);
            notificationSetting.AssertScopeIsNotChanged(NotificationScope.ProgrammePriority);

            notificationSetting.AddProgrammePriorities(attachedPrioritiesIds);

            this.unitOfWork.Save();
        }

        public void RemoveSelectedProgrammePriority(int notificationSettingId, byte[] vers, int programePriorityId, int userId)
        {
            var notificationSetting = this.notificationSettingsRepository.FindForUpdate(notificationSettingId, vers);

            notificationSetting.AssertIsStoredUser(userId);
            notificationSetting.AssertScopeIsNotChanged(NotificationScope.ProgrammePriority);

            var programePrioritySetId = this.notificationSettingsRepository.GetAttachedProgrammePrioritySet(notificationSettingId, programePriorityId);
            notificationSetting.RemoveNotificationSetItem(programePrioritySetId);

            this.unitOfWork.Save();
        }

        #endregion

        #region AttachedProgrammes

        public List<EntityCodeNomVO> GetUnattachedProgrammes(int notificationSettingId, int userId)
        {
            var notificationSetting = this.notificationSettingsRepository.Find(notificationSettingId);

            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Programme);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(userId, OperationalMapPermissions.CanRead);

            return this.notificationSettingsRepository.GetUnattachedProgrammes(notificationSettingId, programmeIds);
        }

        public List<EntityCodeNomVO> GetAttachedProgrammes(int notificationSettingId)
        {
            var notificationSetting = this.notificationSettingsRepository.Find(notificationSettingId);

            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Programme);

            return this.notificationSettingsRepository.GetAttachedProgrammes(notificationSettingId);
        }

        public void AddSelectedProgrammes(int notificationSettingId, byte[] vers, int[] attachedProgrammesIds, int userId)
        {
            var notificationSetting = this.notificationSettingsRepository.FindForUpdate(notificationSettingId, vers);

            notificationSetting.AssertIsStoredUser(userId);
            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Programme);

            notificationSetting.AddProgrammes(attachedProgrammesIds);

            this.unitOfWork.Save();
        }

        public void RemoveSelectedProgramme(int notificationSettingId, byte[] vers, int programmeId, int userId)
        {
            var notificationSetting = this.notificationSettingsRepository.FindForUpdate(notificationSettingId, vers);

            notificationSetting.AssertIsStoredUser(userId);
            notificationSetting.AssertScopeIsNotChanged(NotificationScope.Programme);

            var attachedProgrammeSet = this.notificationSettingsRepository.GetAttachedPogrammeSet(notificationSettingId, programmeId);
            notificationSetting.RemoveNotificationSetItem(attachedProgrammeSet);

            this.unitOfWork.Save();
        }

        #endregion
    }
}
