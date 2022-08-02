using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Linq;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Core;
using Eumis.Domain.Indicators;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Directions;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProcedureManuals;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.PermissionTemplates;
using Eumis.Domain.Procedures;
using Eumis.Domain.RequestPackages;
using Eumis.Domain.Users;
using Eumis.Domain.Users.PermissionAggregations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Eumis.Data.OperationalMap.Programmes.Repositories
{
    internal class ProgrammesRepository : AggregateRepository<Programme, MapNode>, IProgrammesRepository
    {
        public ProgrammesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Programme, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Programme, object>>[]
                {
                    p => p.MapNodeRelation,
                    p => p.MapNodeDocuments.Select(e => e.File),
                    p => p.MapNodeDirections,
                    p => p.ProgrammeProcedureManuals.Select(e => e.File),
                    p => p.ProgrammeApplicationDocuments,
                };
            }
        }

        public IList<ProgrammeBudgetsWrapperVO> GetProgrammeBudgets(int programmeId)
        {
            return new List<ProgrammeBudgetsWrapperVO>();
        }

        public IList<ProgrammeTreeVO> GetProgrammesTree()
        {
            var map = (from p in this.unitOfWork.DbContext.Set<Programme>()
                       join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on p.MapNodeId equals pp.MapNodeRelation.ParentMapNodeId into g1
                       from pp in g1.DefaultIfEmpty()
                       select new
                       {
                           p = new
                           {
                               MapNodeId = (int?)p.MapNodeId,
                               p.Name,
                               p.Code,
                           },
                           pp = new
                           {
                               MapNodeId = (int?)pp.MapNodeId,
                               pp.Name,
                               pp.Code,
                           },
                       })
                    .ToList();

            var invPriorityCodeRegex = new Regex(@"(\d+)(.*)");
            var specTargetCodeRegex = new Regex(@"\d+");
            return (from item in map
                    group item by new { item.p, item.pp } into byP_PP_IP
                    group byP_PP_IP by new { byP_PP_IP.Key.p, byP_PP_IP.Key.pp } into byP_PP
                    group byP_PP by byP_PP.Key.p into byP
                    select new ProgrammeTreeVO
                    {
                        ProgrammeId = byP.Key.MapNodeId.Value,
                        Name = byP.Key.Code + " " + byP.Key.Name,
                        ProgrammePriorities = byP.Where(byP_PP => byP_PP.Key.pp != null && byP_PP.Key.pp.MapNodeId != null).Select(byP_PP =>
                            new ProgrammePriorityTreeVO
                            {
                                ProgrammePriorityId = byP_PP.Key.pp.MapNodeId.Value,
                                Name = byP_PP.Key.pp.Code + " " + byP_PP.Key.pp.Name,
                            })
                            .OrderBy(p => p.Name)
                            .ToList(),
                    })
                    .OrderBy(p => p.Name)
                    .ToList();
        }

        public IList<ProgrammeProcedureManualsVO> GetProgrammeProcedureManuals(int programmeId)
        {
            return (from pm in this.unitOfWork.DbContext.Set<ProgrammeProcedureManual>()
                    where pm.MapNodeId == programmeId
                    select new ProgrammeProcedureManualsVO
                    {
                        ProgrammeProcedureManualId = pm.ProgrammeProcedureManualId,
                        ProgrammeId = pm.MapNodeId,
                        Name = pm.Name,
                        VersionNum = pm.OrderNum,
                        ActivationDate = pm.ActivationDate,
                        Status = pm.Status,
                        File = new FileVO()
                        {
                            Key = pm.File.Key,
                            Name = pm.File.FileName,
                        },
                    })
                    .OrderByDescending(pm => pm.VersionNum)
                    .ToList();
        }

        public IList<ProgrammeApplicationDocumentsVO> GetProgrammeApplicationDocuments(int programmeId)
        {
            return (from pad in this.unitOfWork.DbContext.Set<ProgrammeApplicationDocument>()
                    where pad.ProgrammeId == programmeId
                    select new ProgrammeApplicationDocumentsVO
                    {
                        ProgrammeApplicationDocumentId = pad.ProgrammeApplicationDocumentId,
                        ProgrammeId = pad.ProgrammeId,
                        Name = pad.Name,
                        Extension = pad.Extension,
                        IsSignatureRequired = pad.IsSignatureRequired,
                        IsActive = pad.IsActive,
                    })
                    .ToList();
        }

        public bool IsProgrammeApplicationDocumentAttachedToProcedure(int programmeApplicationDocumentId)
        {
            return (from pad in this.unitOfWork.DbContext.Set<ProcedureApplicationDoc>()
                    where pad.ProgrammeApplicationDocumentId == programmeApplicationDocumentId
                    select pad)
                    .Any();
        }

        public Dictionary<int, string> GetProgrammesIdAndShortName()
        {
            var programmes = this.unitOfWork.DbContext.Set<Programme>()
                .OrderBy(e => e.MapNodeId)
                .ToList();

            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            foreach (var programme in programmes)
            {
                dictionary.Add(programme.MapNodeId, programme.ShortName);
            }

            return dictionary;
        }

        public Dictionary<int, Tuple<string, string>> GetProgrammesIdCodeAndShortName()
        {
            var programmes = this.unitOfWork.DbContext.Set<Programme>()
                .OrderBy(e => e.MapNodeId)
                .ToList();

            Dictionary<int, Tuple<string, string>> dictionary = new Dictionary<int, Tuple<string, string>>();

            foreach (var programme in programmes)
            {
                dictionary.Add(programme.MapNodeId, new Tuple<string, string>(programme.Code, programme.ShortName));
            }

            return dictionary;
        }

        public int[] GetProgammeIds()
        {
            return (from p in this.unitOfWork.DbContext.Set<Programme>()
                    select p.MapNodeId).ToArray();
        }

        public IList<string> CanModifyProgramme(
            int? programmeId,
            string code,
            string name,
            string shortName)
        {
            IList<string> errors = new List<string>();

            var predicate = PredicateBuilder.True<Programme>();
            if (programmeId.HasValue)
            {
                predicate = predicate.And(p => p.MapNodeId != programmeId);
            }

            var isCodeDuplicated =
                (from p in this.unitOfWork.DbContext.Set<Programme>().Where(predicate)
                 where p.Code == code
                 select p.MapNodeId).Any();
            if (isCodeDuplicated)
            {
                errors.Add("Дублиран код на основна организация.");
            }

            var isNameDuplicated =
                (from p in this.unitOfWork.DbContext.Set<Programme>().Where(predicate)
                 where p.Name == name
                 select p.MapNodeId).Any();
            if (isNameDuplicated)
            {
                errors.Add("Дублирано наименование на основна организация.");
            }

            var isShortNameDuplicated =
                (from p in this.unitOfWork.DbContext.Set<Programme>().Where(predicate)
                 where p.ShortName == shortName
                 select p.MapNodeId).Any();
            if (isShortNameDuplicated)
            {
                errors.Add("Дублирано кратко име на основна организация.");
            }

            return errors;
        }

        public IList<string> CanEnterProgramme(int programmeId)
        {
            IList<string> errors = new List<string>();

            var programmePriorities =
                (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                 join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                 where mnr.ParentMapNodeId == programmeId
                 select new { pp.MapNodeId, pp.Status }).ToList();

            if (programmePriorities.Count == 0)
            {
                errors.Add("Не може да въведете основна организация, към която няма въведени разпоредители с бюджетни средства");
            }

            if (programmePriorities.Any(pp => pp.Status == MapNodeStatus.Draft))
            {
                errors.Add("Не може да въведете основна организация, към която има разпоредители с бюджетни средства със статус: 'Чернова'.");
            }

            return errors;
        }

        public IList<string> CanDeleteProgramme(int programmeId)
        {
            IList<string> errors = new List<string>();

            var hasAssociatedProcedures =
                (from pp in this.unitOfWork.DbContext.Set<ProcedureProgramme>()
                 where pp.ProgrammeId == programmeId
                 select new { pp.ProcedureId, pp.ProgrammeId }).Any();
            if (hasAssociatedProcedures)
            {
                errors.Add("Не може да се изтрие основна организация, към която съществуват свързани бюджети.");
            }

            var hasAssociatedProgrammePriorities =
                (from mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>()
                 where mnr.ParentMapNodeId == programmeId
                 select mnr.MapNodeId).Any();
            if (hasAssociatedProgrammePriorities)
            {
                errors.Add("Не може да се изтрие основна организация, за която има въведени разпоредители с бюджетни средства.");
            }

            var programmeIdPermissionStr = string.Format(
                "{0}{1}{2}",
                PermissionAggregation.PermissionSeparator,
                programmeId,
                PermissionAggregation.PermissionPartsSeparator);
            var firstProgrammeIdPermissionStr = string.Format(
                "{0}{1}{2}",
                PermissionAggregation.CommonPermissionsSeparator,
                programmeId,
                PermissionAggregation.PermissionPartsSeparator);

            var isIncludedInPermissionTemplate =
                (from pt in this.unitOfWork.DbContext.Set<PermissionTemplate>()
                 where pt.PermissionsString.Contains(programmeIdPermissionStr) || pt.PermissionsString.Contains(firstProgrammeIdPermissionStr)
                 select pt.PermissionTemplateId).Any();
            if (isIncludedInPermissionTemplate)
            {
                errors.Add("Не може да се изтрие основна организация, която е включена в шаблон за група потребители.");
            }

            var isIncludedInUserPermissions =
                (from pp in this.unitOfWork.DbContext.Set<ProgrammePermission>()
                 where pp.ProgrammeId == programmeId
                 select pp.UserPermissionId).Any();
            if (isIncludedInUserPermissions)
            {
                errors.Add("Не може да се изтрие основна организация, за която са дадени потребителски права.");
            }

            var isIncludedInPermissionRequest =
                (from pr in this.unitOfWork.DbContext.Set<PermissionRequest>()
                 join rp in this.unitOfWork.DbContext.Set<RequestPackage>() on pr.RequestPackageId equals rp.RequestPackageId
                 where RequestPackage.InProgressStatuses.Contains(rp.Status) &&
                       (pr.PermissionsString.Contains(programmeIdPermissionStr) || pr.PermissionsString.Contains(firstProgrammeIdPermissionStr))
                 select pr.RequestPackageId).Any();
            if (isIncludedInPermissionRequest)
            {
                errors.Add("Не може да се изтрие основна организация, за която са дадени потребителски права в заявка за промяна на права.");
            }

            return errors;
        }

        public string GetProgrammeCode(int programmeId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Programme>()
                    where p.MapNodeId == programmeId
                    select p.Code).Single();
        }

        public new void Remove(Programme programme)
        {
            if (programme.Status != MapNodeStatus.Draft || this.CanDeleteProgramme(programme.MapNodeId).Any())
            {
                throw new DomainValidationException("Cannot delete Programme.");
            }

            base.Remove(programme);
        }

        public string GetProgrammeApplicationDocumentExtension(int programmeApplicationDocumentId)
        {
            return (from p in this.unitOfWork.DbContext.Set<ProgrammeApplicationDocument>()
                    where p.ProgrammeApplicationDocumentId == programmeApplicationDocumentId
                    select p.Extension)
                    .First();
        }

        public IList<Guid> GetApplicationDocumentRelatedProcedures(int programmeApplicationDocumentId)
        {
            return (from pd in this.unitOfWork.DbContext.Set<ProcedureApplicationDoc>().Where(pd => pd.ProgrammeApplicationDocumentId == programmeApplicationDocumentId)
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on pd.ProcedureId equals p.ProcedureId
                    select p.Gid)
                    .ToList();
        }

        public IList<ProgrammeApplicationDocumentProcedureVO> GetApplicationDocumentRelatedProceduresData(int programmeApplicationDocumentId)
        {
            return (from pd in this.unitOfWork.DbContext.Set<ProcedureApplicationDoc>().Where(pd => pd.ProgrammeApplicationDocumentId == programmeApplicationDocumentId)
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on pd.ProcedureId equals p.ProcedureId
                    select new ProgrammeApplicationDocumentProcedureVO()
                    {
                        ProcedureId = p.ProcedureId,
                        ProcedureCode = p.Code,
                        ProcedureName = p.Name,
                        ActivationDate = p.ActivationDate,
                        Status = p.ProcedureStatus,
                    })
                    .ToList();
        }

        public IList<MapNodeDirectionVO> GetProgrammeDirections(int mapNodeId)
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
