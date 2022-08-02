using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Directions;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;

namespace Eumis.Data.OperationalMap.ProgrammePriorities.Repositories
{
    internal class ProgrammePrioritiesRepository : AggregateRepository<ProgrammePriority, MapNode>, IProgrammePrioritiesRepository
    {
        public ProgrammePrioritiesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ProgrammePriority, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ProgrammePriority, object>>[]
                {
                    p => p.MapNodeRelation,
                    p => p.MapNodeDocuments.Select(e => e.File),
                    p => p.MapNodeDirections,
                    p => p.ProgrammePriorityBudgets,
                    p => p.CompanyData,
                    p => p.CompanyData.Company,
                };
            }
        }

        public IList<ProgrammePriorityBudgetsWrapperVO> GetProgrammePriorityBudgets(int programmePriorityId)
        {
            return new List<ProgrammePriorityBudgetsWrapperVO>();
        }

        public IList<ProgrammePriorityItemVO> GetProgrammePriorityItems(int programmeId, int[] exceptIds)
        {
            return (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    where !exceptIds.Contains(pp.MapNodeId) && p.MapNodeId == programmeId
                    select new ProgrammePriorityItemVO()
                    {
                        ItemId = pp.MapNodeId,
                        Code = pp.Code,
                        Name = pp.Name,
                        ProgrammeName = p.ShortName,
                    }).ToList();
        }

        public string GetProgrammePriorityCode(int programmePriorityId)
        {
            return this.FindWithoutIncludes(programmePriorityId).Code;
        }

        public IList<string> CanCreateProgrammePriority(int programmeId)
        {
            IList<string> errors = new List<string>();

            if (this.unitOfWork.DbContext.Set<MapNode>().Where(mn => mn.MapNodeId == programmeId).Select(mn => mn.Status).Single() != MapNodeStatus.Draft)
            {
                errors.Add("Не може да създавате приоритетна ос към програма, която не е в статус 'Чернова'.");
            }

            return errors;
        }

        public IList<string> CanModifyProgrammePriority(
            int programmeId,
            int? programmePriorityId,
            string code,
            string name)
        {
            IList<string> errors = new List<string>();

            var predicate = PredicateBuilder.True<ProgrammePriority>();
            if (programmePriorityId.HasValue)
            {
                predicate = predicate.And(p => p.MapNodeId != programmePriorityId);
            }

            var isCodeDuplicated =
                (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>().Where(predicate)
                 where pp.Code == code
                 select pp.MapNodeId).Any();
            if (isCodeDuplicated)
            {
                errors.Add("Дублиран код на приоритетна ос.");
            }

            var isNameDuplicated =
                (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>().Where(predicate)
                 join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                 where pp.Name == name && mnr.ParentMapNodeId == programmeId
                 select pp.MapNodeId).Any();
            if (isNameDuplicated)
            {
                errors.Add("Наименованието на приоритетната ос трябва да е уникално в рамките на програмата.");
            }

            return errors;
        }

        public IList<string> CanDeleteProgrammePriority(int programmePriorityId)
        {
            IList<string> errors = new List<string>();

            var hasAssociatedProcedures =
                (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                 where ps.ProgrammePriorityId == programmePriorityId
                 select ps.ProcedureShareId).Any();
            if (hasAssociatedProcedures)
            {
                errors.Add("Не може да се изтрие приоритетна ос, към която съществуват свързани процедури.");
            }

            var hasAssociatedInvestmentPriorities =
                (from mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>()
                 where mnr.ParentMapNodeId == programmePriorityId
                 select mnr.MapNodeId).Any();
            if (hasAssociatedInvestmentPriorities)
            {
                errors.Add("Не може да се изтрие приоритетна ос, която има въведени инвестиционни приоритети.");
            }

            return errors;
        }

        public int GetProgrammeId(int programmePriorityId)
        {
            return (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    where pp.MapNodeId == programmePriorityId
                    select p.MapNodeId).Single();
        }

        public new void Remove(ProgrammePriority programmePriority)
        {
            if (programmePriority.Status != MapNodeStatus.Draft || this.CanDeleteProgrammePriority(programmePriority.MapNodeId).Any())
            {
                throw new DomainValidationException("Cannot delete ProgrammePriority.");
            }

            base.Remove(programmePriority);
        }

        public IList<MapNodeDirectionVO> GetProgrammePriorityDirections(int mapNodeId)
        {
            var directions = (from ppd in this.unitOfWork.DbContext.Set<MapNodeDirection>().Where(t => t.MapNodeId == mapNodeId)
                              join d in this.unitOfWork.DbContext.Set<Direction>() on ppd.DirectionId equals d.DirectionId

                              join sd in this.unitOfWork.DbContext.Set<SubDirection>() on new { ppd.DirectionId, SubDirectionId = ppd.SubDirectionId.HasValue ? ppd.SubDirectionId.Value : 0 } equals new { sd.DirectionId, sd.SubDirectionId } into g1
                              from sd in g1.DefaultIfEmpty()

                              select new MapNodeDirectionVO
                              {
                                  MapNodeDirectionId = ppd.MapNodeDirectionId,
                                  MapNodeId = ppd.MapNodeId,
                                  DirectionName = d.Name,
                                  DirectionNameAlt = d.NameAlt,
                                  SubDirectionName = sd == null ? string.Empty : sd.Name,
                                  SubDirectionNameAlt = sd == null ? string.Empty : sd.NameAlt,
                              }).ToList();
            return directions;
        }
    }
}
