using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Notifications.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.Notifications;
using Eumis.Domain.Notifications.NotificationSets;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Notifications.Repositories
{
    internal class NotificationSettingsRepository : AggregateRepository<NotificationSetting>, INotificationSettingsRepository
    {
        public NotificationSettingsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<NotificationSetting, object>>[] Includes
        {
            get
            {
                return new Expression<Func<NotificationSetting, object>>[]
                {
                    c => c.NotificationEvent,
                    c => c.Set,
                };
            }
        }

        public List<int> GetSubscribedUsersForEntry(NotificationEntry ne, bool isProgrammeDependent)
        {
            List<int> result = new List<int>();
            if (!isProgrammeDependent)
            {
                return this.Set()
                    .Where(x => x.NotificationEventId == ne.NotificationEventId && x.Status == NotificationSettingStatus.Actual)
                    .Select(x => x.UserId)
                    .Distinct()
                    .ToList();
            }

            var pUsers = this.unitOfWork.DbContext.Set<NotificationProgrammeSet>()
                .Where(x => x.NotificationSetting.NotificationEventId == ne.NotificationEventId && x.NotificationSetting.Status == NotificationSettingStatus.Actual)
                .Where(x => x.ProgrammeId == ne.ProgrammeId)
                .Select(x => x.NotificationSetting.UserId);

            var ppUsers = this.unitOfWork.DbContext.Set<NotificationProgrammePrioritySet>()
                .Where(x => x.NotificationSetting.NotificationEventId == ne.NotificationEventId && x.NotificationSetting.Status == NotificationSettingStatus.Actual)
                .Where(x => x.ProgrammePriorityId == ne.ProgrammePriorityId)
                .Select(x => x.NotificationSetting.UserId);

            var prUsers = this.unitOfWork.DbContext.Set<NotificationProcedureSet>()
                .Where(x => x.NotificationSetting.NotificationEventId == ne.NotificationEventId && x.NotificationSetting.Status == NotificationSettingStatus.Actual)
                .Where(x => x.ProcedureId == ne.ProcedureId)
                .Select(x => x.NotificationSetting.UserId);

            var cUsers = this.unitOfWork.DbContext.Set<NotificationContractSet>()
                .Where(x => x.NotificationSetting.NotificationEventId == ne.NotificationEventId && x.NotificationSetting.Status == NotificationSettingStatus.Actual)
                .Where(x => x.ContractId == ne.ContractId)
                .Select(x => x.NotificationSetting.UserId);

            return pUsers.Concat(ppUsers).Concat(prUsers).Concat(cUsers).Distinct().ToList();
        }

        public List<NotificationSettingVO> GetUserNotificationSettings(int userId)
        {
            return (from ns in this.unitOfWork.DbContext.Set<NotificationSetting>()
                    where ns.UserId == userId
                    select new NotificationSettingVO
                    {
                        NotificationSettingId = ns.NotificationSettingId,
                        EventName = ns.NotificationEvent != null ? ns.NotificationEvent.Name : string.Empty,
                        Scope = ns.Scope,
                        Status = ns.Status,
                        CreateDate = ns.CreateDate,
                    }).ToList();
        }

        public List<NotificationSettingVO> GetActiveUserNotificationSettings(int userId)
        {
            return (from ns in this.unitOfWork.DbContext.Set<NotificationSetting>()
                    where ns.UserId == userId && ns.Status == NotificationSettingStatus.Actual
                    select new NotificationSettingVO
                    {
                        NotificationSettingId = ns.NotificationSettingId,
                        EventName = ns.NotificationEvent != null ? ns.NotificationEvent.Name : string.Empty,
                        Scope = ns.Scope,
                        Status = ns.Status,
                        CreateDate = ns.CreateDate,
                    }).ToList();
        }

        public NotificationSettingInfoVO GetInfo(int notificationSettingId)
        {
            return this.Set()
                .Where(x => x.NotificationSettingId == notificationSettingId)
                .Select(x => new NotificationSettingInfoVO()
                {
                    Scope = x.Scope,
                    Status = x.Status,
                    StatusDescr = x.Status,
                    Version = x.Version,
                })
                .Single();
        }

        public List<ContractVO> GetAttachedContracts(int notificationSettingId)
        {
            return (from nc in this.unitOfWork.DbContext.Set<NotificationContractSet>().Where(x => x.NotificationSettingId == notificationSettingId)
                    join c in this.unitOfWork.DbContext.Set<Contract>() on nc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    select new
                    {
                        c.ContractId,
                        p.ProcedureId,
                        c.ProgrammeId,
                        c.ContractStatus,
                        ProcedureName = p.Name,
                        c.Name,
                        c.RegNumber,
                        c.ContractDate,
                        c.ExecutionStatus,
                        c.CompanyName,
                        c.CompanyUinType,
                        c.CompanyUin,
                        c.TotalBfpAmount,
                        c.TotalSelfAmount,
                    }).ToList()
                   .Select(o => new ContractVO
                   {
                       ContractId = o.ContractId,
                       ProcedureId = o.ProcedureId,
                       ProgrammeId = o.ProgrammeId,
                       ContractStatus = o.ContractStatus,
                       ProcedureName = o.ProcedureName,
                       Name = o.Name,
                       RegNumber = o.RegNumber,
                       ContractDate = o.ContractDate,
                       ExecutionStatus = o.ExecutionStatus,
                       Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                       TotalBfpAmount = o.TotalBfpAmount,
                       TotalSelfAmount = o.TotalSelfAmount,
                   }).ToList();
        }

        public int GetAttachedContractSet(int notificationSettingId, int contractId)
        {
            return this.unitOfWork.DbContext.Set<NotificationContractSet>()
                .Where(x => x.ContractId == contractId && x.NotificationSettingId == notificationSettingId)
                .Select(x => x.NotificationSettingSetId)
                .Single();
        }

        public List<ProcedureVO> GetAttachedProcedures(int notificationSettingId)
        {
            return (from nc in this.unitOfWork.DbContext.Set<NotificationProcedureSet>().Where(x => x.NotificationSettingId == notificationSettingId)
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on nc.ProcedureId equals proc.ProcedureId
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                    join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                    select new
                    {
                        proc.ProcedureId,
                        proc.Code,
                        proc.Name,
                        proc.ActivationDate,
                        proc.ProcedureStatus,
                        ProgramPriorityId = pp.MapNodeId,
                        ProgrammePriorityName = pp.Name,
                        ProgrammeId = prog.MapNodeId,
                        ProgrammeName = prog.Name,
                        BgAmount = ps.BgAmount,
                    }
                    into g1
                    group g1 by new
                    {
                        g1.ProcedureId,
                        g1.Code,
                        g1.Name,
                        g1.ActivationDate,
                        g1.ProcedureStatus,
                    }
                    into g2
                    select new ProcedureVO
                    {
                        ProcedureId = g2.Key.ProcedureId,
                        Name = g2.Key.Name,
                        Code = g2.Key.Code,
                        ActivationDate = g2.Key.ActivationDate,
                        Status = g2.Key.ProcedureStatus,
                        BgAmount = g2.Sum(t => t.BgAmount),
                        ProgrammeNames = g2.Select(pd => pd.ProgrammeName).Distinct().ToList(),
                    }).ToList();
        }

        public int GetAttachedProcedureSet(int notificationSettingId, int procedureId)
        {
            return this.unitOfWork.DbContext.Set<NotificationProcedureSet>()
                .Where(x => x.ProcedureId == procedureId && x.NotificationSettingId == notificationSettingId)
                .Select(x => x.NotificationSettingSetId)
                .Single();
        }

        public List<ProgrammePriorityItemVO> GetAttachedProgrammePriorities(int notificationSettingId)
        {
            return (from npp in this.unitOfWork.DbContext.Set<NotificationProgrammePrioritySet>().Where(x => x.NotificationSettingId == notificationSettingId)
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on npp.ProgrammePriorityId equals pp.MapNodeId
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    select new ProgrammePriorityItemVO()
                    {
                        ItemId = pp.MapNodeId,
                        Code = pp.Code,
                        Name = pp.Name,
                        ProgrammeName = p.ShortName,
                    }).ToList();
        }

        public int GetAttachedProgrammePrioritySet(int settingId, int programmePriorityId)
        {
            return this.unitOfWork.DbContext.Set<NotificationProgrammePrioritySet>()
               .Where(x => x.ProgrammePriorityId == programmePriorityId && x.NotificationSettingId == settingId)
               .Select(x => x.NotificationSettingSetId)
               .Single();
        }

        public List<EntityCodeNomVO> GetAttachedProgrammes(int notificationSettingId)
        {
            return (from nps in this.unitOfWork.DbContext.Set<NotificationProgrammeSet>().Where(x => x.NotificationSettingId == notificationSettingId)
                    join mn in this.unitOfWork.DbContext.Set<MapNode>() on nps.ProgrammeId equals mn.MapNodeId
                    select new EntityCodeNomVO
                    {
                        Code = mn.Code,
                        Name = mn.Name,
                        NomValueId = mn.MapNodeId,
                        NameAlt = mn.NameAlt,
                    })
                    .ToList();
        }

        public List<EntityCodeNomVO> GetUnattachedProgrammes(int notificationSettingId, int[] programmeIds)
        {
            var attachedProgrammes = this.GetAttachedProgrammes(notificationSettingId).Select(x => x.NomValueId);

            return this.unitOfWork.DbContext.Set<MapNode>()
                .Where(x => programmeIds.Contains(x.MapNodeId))
                .Where(x => !attachedProgrammes.Contains(x.MapNodeId))
                .Select(x => new EntityCodeNomVO()
                {
                    Code = x.Code,
                    Name = x.Name,
                    NomValueId = x.MapNodeId,
                    NameAlt = x.NameAlt,
                })
                .ToList();
        }

        public int GetAttachedPogrammeSet(int settingId, int programmeId)
        {
            return this.unitOfWork.DbContext.Set<NotificationProgrammeSet>()
               .Where(x => x.ProgrammeId == programmeId && x.NotificationSettingId == settingId)
               .Select(x => x.NotificationSettingSetId)
               .Single();
        }

        public void CopyUserSettings(int fromUserId, int toUserId)
        {
            var settings = this.unitOfWork.DbContext.Set<NotificationSetting>()
                .AsNoTracking()
                .Where(x => x.UserId == fromUserId)
                .ToList();

            var settingIds = settings.Select(z => z.NotificationSettingId);

            var settingSets = this.unitOfWork.DbContext.Set<NotificationSet>()
                .AsNoTracking()
                .Where(x => settingIds.Contains(x.NotificationSettingId))
                .ToList();

            settings.ForEach(
                x =>
                {
                    x.UserId = toUserId;
                    var sets = settingSets.Where(z => z.NotificationSettingId == x.NotificationSettingId).ToList();

                    sets.ForEach(s => x.Set.Add(s));

                    this.Add(x);
                });

            this.unitOfWork.Save();
        }
    }
}
