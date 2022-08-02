using Eumis.Common.Db;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.DataObjects;
using Eumis.Domain.Procedures.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Procedures.Repositories
{
    internal class ProcedureAppFormDeclarationsRepository : AggregateRepository<ProcedureAppFormDeclaration, ProcedureDeclaration>, IProcedureAppFormDeclarationsRepository
    {
        public ProcedureAppFormDeclarationsRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }

        public void ActivateProcedureDeclarations(int procedureId)
        {
            var declarations = this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>()
                .Where(x => x.ProcedureId == procedureId && x.IsActivated == false);

            var currentDate = DateTime.Now;
            foreach (var declaration in declarations)
            {
                declaration.IsActivated = true;
                declaration.ModifyDate = currentDate;
            }
        }

        public ProcedureDeclarationDO GetDeclaration(int declarationId)
        {
            return (from pd in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>().Where(x => x.ProcedureDeclarationId == declarationId)
                    join progd in this.unitOfWork.DbContext.Set<ProgrammeDeclaration>() on pd.ProgrammeDeclarationId equals progd.ProgrammeDeclarationId
                    orderby pd.CreateDate descending
                    select new ProcedureDeclarationDO
                    {
                        ProcedureDeclarationId = pd.ProcedureDeclarationId,
                        ProgrammeDeclarationId = pd.ProgrammeDeclarationId,
                        ProgrammeId = progd.ProgrammeId,
                        ProcedureId = pd.ProcedureId,
                        OrderNum = progd.OrderNum,
                        ContentBg = progd.Content,
                        ContentEn = progd.ContentAlt,
                        Version = pd.Version,
                        IsRequired = pd.IsRequired,
                        Status = !pd.IsActivated ?
                            ActiveStatus.NotActivated :
                            pd.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive,
                    })
                    .Single();
        }

        public IList<ProcedureDeclarationVO> GetDeclarations(int procedureId)
        {
            var declarations =
                (from pd in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>().Where(x => x.ProcedureId == procedureId)
                 join progd in this.unitOfWork.DbContext.Set<ProgrammeDeclaration>() on pd.ProgrammeDeclarationId equals progd.ProgrammeDeclarationId
                 select new ProcedureDeclarationVO
                 {
                     ProcedureDeclarationId = pd.ProcedureDeclarationId,
                     OrderNum = progd.OrderNum,
                     Name = progd.Name,
                     NameAlt = progd.NameAlt,
                     Status = !pd.IsActivated ?
                        ActiveStatus.NotActivated :
                        pd.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive,
                 })
                 .OrderBy(d => d.OrderNum)
                 .ToList();

            return declarations;
        }

        public IList<ProcedureDeclarationJson> GetDeclarationsForProcedureVersion(int procedureId)
        {
            var declarations =
                (from pd in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>().Where(x => x.ProcedureId == procedureId)
                 join progd in this.unitOfWork.DbContext.Set<ProgrammeDeclaration>() on pd.ProgrammeDeclarationId equals progd.ProgrammeDeclarationId

                 join di in this.unitOfWork.DbContext.Set<ProgrammeDeclarationItem>() on progd.ProgrammeDeclarationId equals di.ProgrammeDeclarationId into g1
                 from di in g1.DefaultIfEmpty()

                 group di
                 by new
                 {
                     pd.ProcedureDeclarationId,
                     pd.Gid,
                     progd.OrderNum,
                     progd.Name,
                     progd.NameAlt,
                     progd.Content,
                     progd.ContentAlt,
                     progd.FieldType,
                     pd.IsRequired,
                     pd.IsActive,
                     pd.CreateDate,
                 }
                 into g
                 orderby g.Key.OrderNum
                 select new ProcedureDeclarationJson
                 {
                     ProcedureDeclarationId = g.Key.ProcedureDeclarationId,
                     Gid = g.Key.Gid,
                     OrderNum = g.Key.OrderNum,
                     Name = g.Key.Name,
                     NameAlt = g.Key.NameAlt,
                     Content = g.Key.Content,
                     ContentAlt = g.Key.ContentAlt,
                     FieldType = g.Key.FieldType,
                     IsRequired = g.Key.IsRequired,
                     IsActive = g.Key.IsActive,
                     Items = g.Where(di => di != null)
                     .Select(di => new ProcedureDeclarationItemJson()
                     {
                         ProgrammeDeclarationItemId = di.ProgrammeDeclarationItemId,
                         Gid = di.Gid,
                         OrderNum = di.OrderNum,
                         Content = di.Content,
                         IsActive = di.IsActive,
                     }).ToList(),
                 })
                 .ToList();

            return declarations;
        }

        public Guid GetProcedureDeclarationGid(int programmeDeclarationId, int procedureId)
        {
            return (from pd in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>()
                    where pd.ProgrammeDeclarationId == programmeDeclarationId && pd.ProcedureId == procedureId && pd.IsActivated == true
                    select pd.Gid)
                    .Single();
        }

        public bool IsDeclarationReadonly(int programmeDeclarationId)
        {
            return (from pd in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>()
                    where pd.ProgrammeDeclarationId == programmeDeclarationId && pd.IsActivated == true
                    select pd)
                    .Any();
        }

        public List<string> ValidateProcedureDeclarations(int procedureId)
        {
            var errors = new List<string>();

            var isDeclarationsSectionSelected =
                (from pd in this.unitOfWork.DbContext.Set<ProcedureApplicationSection>()
                 where pd.ProcedureId == procedureId && pd.Section == ApplicationSectionType.ElectronicDeclarations && pd.IsSelected
                 select pd)
                 .Any();

            if (isDeclarationsSectionSelected)
            {
                var procedureHasActiveDeclarations =
                    (from pd in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>()
                     where pd.ProcedureId == procedureId && pd.IsActive
                     select pd.ProcedureDeclarationId)
                    .Any();

                if (!procedureHasActiveDeclarations)
                {
                    errors.Add("Секцията \"Е - Декларации\" е включена във формуляра, но липсват настройки за нея");
                }
            }

            return errors;
        }
    }
}
