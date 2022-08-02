using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.OperationalMap.Programmes.Repositories
{
    internal class ProgrammeAppFormDeclarationsRepository : AggregateRepository<ProgrammeAppFormDeclaration, ProgrammeDeclaration>, IProgrammeAppFormDeclarationsRepository
    {
        public ProgrammeAppFormDeclarationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ProgrammeAppFormDeclaration, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ProgrammeAppFormDeclaration, object>>[]
                {
                    p => p.ProgrammeDeclarationItems,
                };
            }
        }

        public IList<ProgrammeDeclarationVO> GetProgrammeDeclarations(int programmeId)
        {
            return (from pd in this.unitOfWork.DbContext.Set<ProgrammeDeclaration>()
                    where pd.ProgrammeId == programmeId
                    select new ProgrammeDeclarationVO
                    {
                        ProgrammeDeclarationId = pd.ProgrammeDeclarationId,
                        OrderNum = pd.OrderNum,
                        Name = pd.Name,
                        NameAlt = pd.NameAlt,
                        IsActive = pd.IsActive,
                    })
                    .ToList();
        }

        public IList<string> CanDeleteProgrammeDeclaration(int programmeDeclarationId)
        {
            IList<string> errors = new List<string>();

            var isDeclarationAttached =
                (from pd in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>()
                 where pd.ProgrammeDeclarationId == programmeDeclarationId
                 select pd)
                 .Any();

            if (isDeclarationAttached)
            {
                errors.Add("Декларацията не може да бъде изтрита, защото вече е добавена в процедура.");
            }

            return errors;
        }

        public int GetNextProgrammeDeclarationOrderNum(int programmeId)
        {
            var lastOrderNumber = this.unitOfWork.DbContext.Set<ProgrammeDeclaration>()
                .Where(t => t.ProgrammeId == programmeId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public IList<ProgrammeProcedureVO> GetDeclarationRelatedProceduresData(int programmeDeclarationId)
        {
            var procedures =
                (from pd in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>().Where(pd => pd.ProgrammeDeclarationId == programmeDeclarationId)
                 join p in this.unitOfWork.DbContext.Set<Procedure>() on pd.ProcedureId equals p.ProcedureId
                 select new ProgrammeProcedureVO()
                 {
                     ProcedureId = p.ProcedureId,
                     ProcedureCode = p.Code,
                     ProcedureName = p.Name,
                     ActivationDate = p.ActivationDate,
                     Status = p.ProcedureStatus,
                 })
                 .ToList();

            return procedures;
        }

        public IList<ProgrammeDeclarationItemVO> GetProgrammeDeclarationItems(int programmeDeclarationId)
        {
            var programmeDeclarationItems =
                (from di in this.unitOfWork.DbContext.Set<ProgrammeDeclarationItem>().Where(di => di.ProgrammeDeclarationId == programmeDeclarationId)
                 select new ProgrammeDeclarationItemVO()
                 {
                     ProgrammeDeclarationItemId = di.ProgrammeDeclarationItemId,
                     OrderNum = di.OrderNum,
                     Content = di.Content,
                     IsActive = di.IsActive,
                 })
                 .OrderBy(di => di.OrderNum)
                 .ToList();

            return programmeDeclarationItems;
        }

        public IList<string> CanAddProgrammeDeclarationItem(int programmeDeclarationId, int orderNum)
        {
            var errors = new List<string>();

            if (this.unitOfWork.DbContext.Set<ProgrammeDeclarationItem>().Where(di => di.ProgrammeDeclarationId == programmeDeclarationId && di.OrderNum == orderNum && di.IsActive).Any())
            {
                errors.Add($"Вече съществува активен ред с пореден номер {orderNum}.");
            }

            return errors;
        }

        public IList<string> CanUpdateProgrammeDeclarationItem(int programmeDeclarationId, int programmeDeclarationItemId, int orderNum)
        {
            var errors = new List<string>();

            if (this.unitOfWork.DbContext.Set<ProgrammeDeclarationItem>()
                .Where(di =>
                di.ProgrammeDeclarationId == programmeDeclarationId &&
                di.ProgrammeDeclarationItemId != programmeDeclarationItemId &&
                di.OrderNum == orderNum &&
                di.IsActive).Any())
            {
                errors.Add($"Вече съществува активен ред с пореден номер {orderNum}.");
            }

            if (this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>().Where(pd => pd.ProgrammeDeclarationId == programmeDeclarationId && pd.IsActivated).Any())
            {
                errors.Add($"Декларацията е присъединена към процедура и активирана.");
            }

            return errors;
        }

        public IList<string> CanDeleteProgrammeDeclarationItem(int programmeDeclarationId)
        {
            var errors = new List<string>();

            if (this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>().Where(pd => pd.ProgrammeDeclarationId == programmeDeclarationId && pd.IsActivated).Any())
            {
                errors.Add($"Декларацията е присъединена към процедура и активирана.");
            }

            return errors;
        }

        public IList<string> CanCreateProgrammeDeclaration(int programmeId, string name, string nameAlt)
        {
            var errors = new List<string>();

            if (this.unitOfWork.DbContext.Set<ProgrammeAppFormDeclaration>().Where(pd => pd.ProgrammeId == programmeId && pd.Name == name).Any())
            {
                errors.Add($"Не можете да създадете декларация \"{name}\" защото вече съществува декларация с това наименование.");
            }

            if (!string.IsNullOrWhiteSpace(nameAlt) && this.unitOfWork.DbContext.Set<ProgrammeAppFormDeclaration>().Where(pd => pd.ProgrammeId == programmeId && pd.NameAlt == nameAlt).Any())
            {
                errors.Add($"Не можете да създадете декларация \"{nameAlt}\" защото вече съществува декларация с това наименование на английски.");
            }

            return errors;
        }

        public ProgrammeDeclaration FindProgrammeDeclaration(int programmeDeclarationId)
        {
            return this.unitOfWork.DbContext.Set<ProgrammeDeclaration>()
                .Where(pd => pd.ProgrammeDeclarationId == programmeDeclarationId)
                .Single();
        }

        public ProgrammeDeclaration GetProgrammeDeclaration(int programmeDeclarationId)
        {
            return this.unitOfWork.DbContext.Set<ProgrammeDeclaration>().Where(d => d.ProgrammeDeclarationId == programmeDeclarationId).Single();
        }
    }
}
