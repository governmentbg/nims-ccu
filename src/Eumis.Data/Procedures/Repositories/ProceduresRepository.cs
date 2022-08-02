using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Eumis.Common.Db;
using Eumis.Common.Localization;
using Eumis.Data.Linq;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.Procedures.PortalViewObjects;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.ExpenseTypes;
using Eumis.Domain.Indicators;
using Eumis.Domain.Measures;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Directions;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.Json;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.Domain.Users;

namespace Eumis.Data.Procedures.Repositories
{
    [SuppressMessage("", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "The code is grouped into regions.")]
    internal class ProceduresRepository : AggregateRepository<Procedure>, IProceduresRepository
    {
        public ProceduresRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Procedure, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Procedure, object>>[]
                {
                    p => p.ProcedureIndicators,
                    p => p.ProcedureProgrammes.Select(e => e.ProcedureBudgetLevel1.Select(el1 => el1.ProcedureBudgetLevel2.Select(el2 => el2.ProcedureBudgetLevel3))),
                    p => p.ProcedureProgrammes.Select(e => e.ProcedureBudgetValidationRules),
                    p => p.ProcedureShares.Select(e => e.ProcedureBudgetLevel2),
                    p => p.ProcedureApplicationGuidelines,
                    p => p.ProcedureApplicationGuidelines.Select(e => e.File),
                    p => p.ProcedureSpecFields,
                    p => p.ProcedureDocuments.Select(e => e.File),
                    p => p.ProcedureApplicationDocs,
                    p => p.ProcedureEvalTables,
                    p => p.ProcedureQuestions.Select(e => e.File),
                    p => p.ProcedureContractReportDocuments,
                    p => p.ProcedureLocations,
                    p => p.ProcedureApplicationSections,
                    p => p.ProcedureApplicationSectionAdditionalSetting,
                    p => p.ProcedureDirections.Select(d => d.SubDirection),
                    p => p.ProcedureDirections.Select(d => d.Direction),
                    p => p.ProcedureTimeLimits,
                };
            }
        }

        public Procedure FindByCode(string code)
        {
            var procedure = this.Set()
                .Where(p => p.Code == code)
                .SingleOrDefault();

            if (procedure == null)
            {
                throw new DataObjectNotFoundException(typeof(Procedure).Name, "code: " + code);
            }

            return procedure;
        }

        public int FindProcedureIdByCode(string code)
        {
            var procedureId = this.Set()
                .Where(p => p.Code == code)
                .Select(p => p.ProcedureId)
                .SingleOrDefault();

            return procedureId;
        }

        public IList<ProcedureProgrammeTreeVO> GetProcedureProgrammesTree()
        {
            return this.GetProcedureProgrammesTreeInternal(ProcedureTypeForProgrammesTreeInternal.All)
                .Select(p => new ProcedureProgrammeTreeVO
                {
                    ProgrammeId = p.ProgrammeId,
                    Name = p.Code + " " + p.Name,
                    ProgrammePriorities = p.ProgrammePriorities
                        .Select(pp => new ProcedureProgrammePriorityTreeVO
                        {
                            ProgrammeId = pp.ProgrammeId,
                            ProgrammePriorityId = pp.ProgrammePriorityId,
                            Name = pp.Code + " " + pp.Name,
                            Procedures = pp.Procedures
                                .Select(pr => new ProcedureTreeVO
                                {
                                    ProgrammeId = pr.ProgrammeId,
                                    ProgrammePriorityId = pr.ProgrammePriorityId,
                                    ProcedureId = pr.ProcedureId,
                                    Name = pr.Code + " " + pr.Name,
                                })
                                .OrderBy(t => t.Name)
                                .ToList(),
                        })
                        .OrderBy(t => t.Name)
                        .ToList(),
                })
                .OrderBy(t => t.Name)
                .ToList();
        }

        public IList<ProcedureVO> GetProcedures(int[] programmeIds, int? programmeId = null, int? programmePriorityId = null)
        {
            var proceduresQuery =
                from proc in this.unitOfWork.DbContext.Set<Procedure>()
                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                where programmeIds.Contains(prog.MapNodeId)
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
                };

            if (programmeId.HasValue)
            {
                proceduresQuery = proceduresQuery.Where(p => p.ProgrammeId == programmeId.Value);
            }

            if (programmePriorityId.HasValue)
            {
                proceduresQuery = proceduresQuery.Where(p => p.ProgramPriorityId == programmePriorityId.Value);
            }

            var proceduresData = proceduresQuery.ToList();

            var procedureTimeLimits =
                (from ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                 group ptl by ptl.ProcedureId into g
                 select new
                 {
                     ProcedureId = g.Key,
                     EndDate = g.Max(ptl => ptl.EndDate),
                 })
                 .ToList();

            return (from pd in proceduresData
                    join ptl in procedureTimeLimits on pd.ProcedureId equals ptl.ProcedureId into g
                    from ptl in g.DefaultIfEmpty()
                    group pd by new
                    {
                        pd.ProcedureId,
                        pd.Code,
                        pd.Name,
                        pd.ActivationDate,
                        pd.ProcedureStatus,
                        EndDate = ptl == null ? (DateTime?)null : ptl.EndDate,
                    }
                    into g
                    select new ProcedureVO
                    {
                        ProcedureId = g.Key.ProcedureId,
                        Name = g.Key.Name,
                        Code = g.Key.Code,
                        ActivationDate = g.Key.ActivationDate,
                        Status = g.Key.ProcedureStatus,
                        EndingDate = g.Key.EndDate,
                        ProgrammeNames = g.Select(pd => pd.ProgrammeName).Distinct().ToList(),
                        BgAmount = g.Sum(t => t.BgAmount),
                    })
                    .ToList();
        }

        public IList<ProcedureIndicatorsVO> GetProcedureIndicators(int procedureId)
        {
            return (from pi in this.unitOfWork.DbContext.Set<ProcedureIndicator>()
                    join i in this.unitOfWork.DbContext.Set<Indicator>() on pi.IndicatorId equals i.IndicatorId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on i.ProgrammeId equals p.MapNodeId
                    join m in this.unitOfWork.DbContext.Set<Measure>() on i.MeasureId equals m.MeasureId
                    where pi.ProcedureId == procedureId
                    select new ProcedureIndicatorsVO
                    {
                        ProgrammeId = p.MapNodeId,
                        ProcedureId = pi.ProcedureId,
                        IndicatorId = pi.IndicatorId,
                        Gid = i.Gid,
                        ProgrammeName = p.ShortName,
                        Name = i.Name,
                        NameAlt = i.NameAlt,
                        HasGenderDivision = i.HasGenderDivision,
                        MeasureName = m.Name,
                        MeasureNameAlt = m.NameAlt,
                        BaseTotalValue = pi.BaseTotalValue,
                        BaseYear = pi.BaseYear,
                        TargetTotalValue = pi.TargetTotalValue,
                        MilestoneTargetTotalValue = pi.MilestoneTargetTotalValue,
                        DataSource = pi.DataSource,
                        IsActivated = pi.IsActivated,
                        IsActive = pi.IsActive,
                        ActiveStatus = !pi.IsActivated ? ActiveStatus.NotActivated : pi.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive,
                    })
                    .ToList();
        }

        public bool HasAvailableIndicatorsForAttach(int procedureId)
        {
            var indicators =
                from pr in this.unitOfWork.DbContext.Set<ProcedureProgramme>()
                join i in this.unitOfWork.DbContext.Set<Indicator>() on pr.ProgrammeId equals i.ProgrammeId
                where pr.ProcedureId == procedureId
                select i;

            var usedIndicators =
                from pi in this.unitOfWork.DbContext.Set<ProcedureIndicator>()
                join i in this.unitOfWork.DbContext.Set<Indicator>() on pi.IndicatorId equals i.IndicatorId
                where pi.ProcedureId == procedureId
                select i;

            return indicators.Except(usedIndicators).Any();
        }

        public bool HasProceduresWithIndicator(int indicatorId)
        {
            return this.unitOfWork.DbContext.Set<ProcedureIndicator>().Any(e => e.IndicatorId == indicatorId);
        }

        public IList<ProcedureSharesVO> GetProcedureShares(int procedureId)
        {
            return (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on pp.MapNodeRelation.ProgrammeId equals p.MapNodeId
                    where ps.ProcedureId == procedureId
                    select new ProcedureSharesVO()
                    {
                        ProcedureShareId = ps.ProcedureShareId,
                        ProgrammeId = ps.ProgrammeId,
                        ProgrammeName = p.Name,
                        ProgrammeNameAlt = p.NameAlt,
                        ProgrammePriorityId = ps.ProgrammePriorityId,
                        ProgrammePriorityGid = pp.Gid,
                        ProgrammePriorityName = pp.Name,
                        ProgrammePriorityNameAlt = pp.NameAlt,
                        ProgrammePriorityCode = pp.Code,
                        BgAmount = ps.BgAmount,
                        IsPrimary = ps.IsPrimary,
                    })
                    .ToList();
        }

        public IList<ProcedureTimeLimitsVO> GetProcedureTimeLimits(int procedureId)
        {
            return this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                .Where(e => e.ProcedureId == procedureId)
                .Select(e => new ProcedureTimeLimitsVO()
                {
                    ProcedureTimeLimitId = e.ProcedureTimeLimitId,
                    EndDate = e.EndDate,
                    Notes = e.Notes,
                    Version = e.Procedure.Version,
                })
                .OrderBy(t => t.EndDate)
                .ToList();
        }

        public DateTime GetProcedureCurrentEndDate(int procedureId)
        {
            var timeLimits = (from l in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                              where l.ProcedureId == procedureId
                              orderby l.EndDate
                              select l.EndDate).ToList();

            var currentEndDate = timeLimits.Where(t => t >= DateTime.Now).DefaultIfEmpty(timeLimits.Last()).First();

            return currentEndDate;
        }

        public bool IsValidProcedureTimeLimitEndTime(int procedureId, DateTime endDateTime, int? procedureTimeLimitId = null)
        {
            if (procedureTimeLimitId.HasValue)
            {
                return !this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                    .Where(e => e.ProcedureId == procedureId && e.EndDate > endDateTime && e.ProcedureTimeLimitId != procedureTimeLimitId).Any();
            }
            else
            {
                return !this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                    .Where(e => e.ProcedureId == procedureId && e.EndDate > endDateTime).Any();
            }
        }

        public bool IsProcedureInTimeLimit(int procedureId)
        {
            var lastLimitDate = this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                .Where(l => l.ProcedureId == procedureId)
                .Select(ptl => ptl.EndDate)
                .OrderByDescending(d => d)
                .First();

            return DateTime.Now < new DateTime(lastLimitDate.Year, lastLimitDate.Month, lastLimitDate.Day, lastLimitDate.Hour, lastLimitDate.Minute, 0).AddMinutes(1);
        }

        public IList<int> GetProceduresPassedTimeLimit()
        {
            var limitTime = DateTime.Now.AddMinutes(-1);

            return this.unitOfWork.DbContext.Set<Procedure>()
                .Where(p => p.ProcedureStatus == ProcedureStatus.Active)
                .Where(p => p.ProcedureTimeLimits.All(tl => tl.EndDate <= limitTime))
                .Select(p => p.ProcedureId).ToList();
        }

        public Guid GetGid(int procedureId)
        {
            return this.FindWithoutIncludes(procedureId).Gid;
        }

        public int GetId(Guid procedureGid)
        {
            return this.unitOfWork.DbContext.Set<Procedure>()
                .Where(p => p.Gid == procedureGid)
                .Select(p => p.ProcedureId)
                .Single();
        }

        public byte[] GetProcedureVersion(int procedureId)
        {
            return this.FindWithoutIncludes(procedureId).Version;
        }

        public ProcedureBudgetTreeVO GetExpenseBudgetTree(int procedureId)
        {
            var version = this.GetVersion(procedureId);

            var procedureBudgetProgrammes = this.GetExpenseBudgetProgrammes(procedureId);
            var procedureBudgetValidationRules = this.GetProcedureBudgetValidationRules(procedureId);

            var expenseBudgetProgrammes = procedureBudgetProgrammes.Select(l0 =>
                new ProcedureBudgetLevel0TreeVO
                {
                    ProgrammeId = l0.ProgrammeId,
                    DisplayName = l0.Name,
                    NameAlt = l0.NameAlt,
                    Code = l0.Code,
                    Level1Items = l0.ExpenseTypes.Select(l1 =>
                        new ProcedureBudgetLevel1TreeVO
                        {
                            ProcedureBudgetLevel1Id = l1.ProcedureBudgetLevel1Id,
                            Gid = l1.Gid,
                            DisplayName = l1.Name,
                            NameAlt = l1.NameAlt,
                            OrderNum = l1.OrderNum,
                            IsActivated = l1.IsActivated,
                            IsActive = l1.IsActive,
                            Level2Items = l1.Expenses.Select(l2 =>
                                new ProcedureBudgetLevel2TreeVO
                                {
                                    ProcedureBudgetLevel2Id = l2.ProcedureBudgetLevel2Id,
                                    Gid = l2.Gid,
                                    DisplayName = l2.Name,
                                    NameAlt = l2.NameAlt,
                                    ProgrammePriorityName = l2.ProgrammePriorityName,
                                    ProgrammePriorityCode = l2.ProgrammePriorityCode,
                                    AidMode = l2.AidMode,
                                    IsEligibleCost = l2.IsEligibleCost,
                                    IsStandardTablesExpense = l2.IsStandardTablesExpense,
                                    IsOneTimeExpense = l2.IsOneTimeExpense,
                                    IsFlatRateExpense = l2.IsFlatRateExpense,
                                    IsLandExpense = l2.IsLandExpense,
                                    IsEuApprovedStandardTablesExpense = l2.IsEuApprovedStandardTablesExpense,
                                    IsEuApprovedOneTimeExpense = l2.IsEuApprovedOneTimeExpense,
                                    OrderNum = l2.OrderNum,
                                    IsActivated = l2.IsActivated,
                                    IsActive = l2.IsActive,
                                    Level3Items = l2.Details.Select(l3 =>
                                        new ProcedureBudgetLevel3TreeVO
                                        {
                                            ProcedureBudgetLevel3Id = l3.ProcedureBudgetLevel3Id,
                                            Gid = l3.Gid,
                                            DisplayName = l3.Note,
                                            OrderNum = l3.OrderNum,
                                        })
                                        .ToList(),
                                })
                                .ToList(),
                        })
                         .ToList(),
                    ValidationRules = procedureBudgetValidationRules.Where(e => e.ProgrammeId == l0.ProgrammeId).Select(vr =>
                    new ProcedureBudgetValidationRuleVO
                    {
                        ProcedureBudgetValidationRuleId = vr.ProcedureBudgetValidationRuleId,
                        Message = vr.Message,
                        Condition = vr.Condition,
                        Rule = vr.Rule,
                    })
                        .ToList(),
                })

                // orderby only the programmes, the other levels should be sorted
                .OrderBy(e => e.DisplayName)
                .ToList();

            return new ProcedureBudgetTreeVO()
            {
                ProcedureId = procedureId,
                Version = version,
                Programmes = expenseBudgetProgrammes,
            };
        }

        public int GetLastProcedureNumber(int programmePriorityId, int year)
        {
            return this.unitOfWork.DbContext.Set<ProcedureNumber>()
                .Where(pn => pn.ProgrammePriorityId == programmePriorityId && pn.Year == year)
                .Max(pn => (int?)pn.Number) ?? 0;
        }

        public IList<ProcedureAppGuidelinesVO> GetProcedureAppGuidelines(int procedureId)
        {
            return this.GetProcedureAppGuidelinesInternal(procedureId)
                .Select(pag => new ProcedureAppGuidelinesVO
                {
                    ProcedureApplicationGuidelineId = pag.ProcedureApplicationGuidelineId,
                    ProcedureId = procedureId,
                    Name = pag.Name,
                    Description = pag.Description,
                    File = new FileVO
                    {
                        Key = pag.BlobKey,
                        Name = pag.Filename,
                    },
                })
                .ToList();
        }

        public IList<ProcedureSpecFieldVO> GetProcedureSpecFields(int procedureId)
        {
            return (from f in this.unitOfWork.DbContext.Set<ProcedureSpecField>()
                    where f.ProcedureId == procedureId
                    select new ProcedureSpecFieldVO
                    {
                        ProcedureSpecFieldId = f.ProcedureSpecFieldId,
                        Title = f.Title,
                        TitleAlt = f.TitleAlt,
                        Description = f.Description,
                        DescriptionAlt = f.DescriptionAlt,
                        IsRequired = f.IsRequired,
                        IsActive = f.IsActive,
                        IsActivated = f.IsActivated,
                        ActiveStatus = !f.IsActivated ? ActiveStatus.NotActivated : f.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive,
                    })
                    .ToList();
        }

        public IList<ProcedureDocumentsVO> GetProcedureDocuments(int procedureId)
        {
            return (from pd in this.unitOfWork.DbContext.Set<ProcedureDocument>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on pd.BlobKey equals b.Key into g1
                    from b in g1.DefaultIfEmpty()
                    where pd.ProcedureId == procedureId
                    select new { pd, b })
                    .Select(p => new ProcedureDocumentsVO
                    {
                        ProcedureId = p.pd.ProcedureId,
                        ProcedureDocumentId = p.pd.ProcedureDocumentId,
                        Name = p.pd.Name,
                        Description = p.pd.Description,
                        File = (p.b.Key == null) ? null : new FileVO
                        {
                            Key = p.b.Key,
                            Name = p.b.FileName,
                        },
                    })
                   .ToList();
        }

        public IList<ProcedureAppDocsVO> GetProcedureAppDocs(int procedureId)
        {
            return this.GetProcedureAppDocsInternal(procedureId)
                .Select(p => new ProcedureAppDocsVO
                {
                    ProcedureApplicationDocId = p.ProcedureApplicationDocId,
                    ProcedureId = procedureId,
                    Name = p.Name,
                    Extension = p.Extension,
                    IsRequired = p.IsRequired,
                    IsSignatureRequired = p.IsSignatureRequired,
                    IsActive = p.IsActive,
                    IsActivated = p.IsActivated,
                    ActiveStatus = !p.IsActivated ? ActiveStatus.NotActivated : p.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive,
                })
                .ToList();
        }

        public IList<ProcedureContractReportDocumentVO> GetProcedureContractReportDocuments(int procedureId, ProcedureContractReportDocumentType type)
        {
            Type documentType = ProcedureContractReportDocument.ProcedureReportDocuments[type];

            var procedureContractReportDocuments = (IQueryable<ProcedureContractReportDocument>)this.unitOfWork.DbContext.Set(documentType);

            return (from crd in procedureContractReportDocuments
                    where crd.ProcedureId == procedureId
                    select new ProcedureContractReportDocumentVO
                    {
                        ProcedureContractReportDocumentId = crd.ProcedureContractReportDocumentId,
                        ProcedureId = procedureId,
                        Name = crd.Name,
                        Extension = crd.Extension,
                        IsRequired = crd.IsRequired,
                        IsActive = crd.IsActive,
                        IsActivated = crd.IsActivated,
                        ActiveStatus = !crd.IsActivated ? ActiveStatus.NotActivated : (crd.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive),
                    }).ToList();
        }

        public IList<ProcedureEvalTablesVO> GetProcedureEvalTables(int procedureId)
        {
            return (from prt in this.unitOfWork.DbContext.Set<ProcedureEvalTable>()
                    join prtx in this.unitOfWork.DbContext.Set<ProcedureEvalTableXml>() on prt.ProcedureEvalTableId equals prtx.ProcedureEvalTableId
                    where prt.ProcedureId == procedureId
                    select new ProcedureEvalTablesVO
                    {
                        ProcedureEvalTableId = prt.ProcedureEvalTableId,
                        ProcedureId = prt.ProcedureId,
                        Name = prt.Name,
                        Type = prt.Type,
                        XmlGid = prtx.Gid,
                        IsActive = prt.IsActive,
                        IsActivated = prt.IsActivated,
                        ActiveStatus = !prt.IsActivated ? ActiveStatus.NotActivated : prt.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive,
                    })
                    .ToList();
        }

        public bool HasPreliminaryEvalTable(int procedureId)
        {
            return false;
        }

        public IList<ProcedureProgrammeTreePVO> GetPortalActiveProcedureProgrammesTree()
        {
            return this.GetProcedureProgrammesTreeInternal(ProcedureTypeForProgrammesTreeInternal.Active)
                .Select(p => new ProcedureProgrammeTreePVO
                {
                    Code = p.Code,
                    Name = p.Name,
                    NameAlt = p.NameAlt,
                    ProgrammePriorities = p.ProgrammePriorities
                        .Select(pp => new ProcedureProgrammePriorityTreePVO
                        {
                            Number = int.Parse(pp.Code.Substring(pp.Code.LastIndexOf('-') + 1)),
                            Code = pp.Code,
                            Name = pp.Name,
                            NameAlt = pp.NameAlt,
                            Procedures = pp.Procedures
                                .Select(pr => new ProcedureTreePVO
                                {
                                    Number = pr.Number,
                                    Gid = pr.Gid,
                                    Code = pr.Code,
                                    Name = pr.Name,
                                    NameAlt = pr.NameAlt,
                                    Status = pr.Status,
                                    StatusText = pr.Status,
                                    IsIntroducedByLAG = pr.IsIntroducedByLAG,
                                })
                                .OrderBy(pr => pr.Number)
                                .ToList(),
                        })
                        .OrderBy(pp => pp.Number)
                        .ToList(),
                })
                .OrderBy(p => p.Code)
                .ToList();
        }

        public IList<ProcedureProgrammeTreePVO> GetPortalEndedProcedureProgrammesTree()
        {
            return this.GetProcedureProgrammesTreeInternal(ProcedureTypeForProgrammesTreeInternal.Ended)
                .Select(p => new ProcedureProgrammeTreePVO
                {
                    Code = p.Code,
                    Name = p.Name,
                    NameAlt = p.NameAlt,
                    ProgrammePriorities = p.ProgrammePriorities
                        .Select(pp => new ProcedureProgrammePriorityTreePVO
                        {
                            Number = int.Parse(pp.Code.Substring(pp.Code.LastIndexOf('-') + 1)),
                            Code = pp.Code,
                            Name = pp.Name,
                            NameAlt = pp.NameAlt,
                            Procedures = pp.Procedures
                                .Select(pr => new ProcedureTreePVO
                                {
                                    Number = pr.Number,
                                    Gid = pr.Gid,
                                    Code = pr.Code,
                                    Name = pr.Name,
                                    NameAlt = pr.NameAlt,
                                    Status = pr.Status,
                                    StatusText = pr.Status,
                                    IsIntroducedByLAG = pr.IsIntroducedByLAG,
                                })
                                .OrderBy(pr => pr.Number)
                                .ToList(),
                        })
                        .OrderBy(pp => pp.Number)
                        .ToList(),
                })
                .OrderBy(p => p.Code)
                .ToList();
        }

        public IList<ProcedureProgrammeTreePVO> GetPortalPublicDiscussionProcedureProgrammesTree()
        {
            return this.GetProcedureProgrammesTreeInternal(ProcedureTypeForProgrammesTreeInternal.PublicDiscussion)
                .Select(p => new ProcedureProgrammeTreePVO
                {
                    Code = p.Code,
                    Name = p.Name,
                    NameAlt = p.NameAlt,
                    ProgrammePriorities = p.ProgrammePriorities
                        .Select(pp => new ProcedureProgrammePriorityTreePVO
                        {
                            Number = int.Parse(pp.Code.Substring(pp.Code.LastIndexOf('-') + 1)),
                            Code = pp.Code,
                            Name = pp.Name,
                            NameAlt = pp.NameAlt,
                            Procedures = pp.Procedures
                                .Select(pr => new ProcedureTreePVO
                                {
                                    Number = pr.Number,
                                    Gid = pr.Gid,
                                    Code = pr.Code,
                                    Name = pr.Name,
                                    NameAlt = pr.NameAlt,
                                    Status = pr.Status,
                                    StatusText = pr.Status,
                                    IsIntroducedByLAG = pr.IsIntroducedByLAG,
                                })
                                .Where(pr => pr.Status != ProcedureStatus.Canceled)
                                .OrderBy(pr => pr.Number)
                                .ToList(),
                        })
                        .OrderBy(pp => pp.Number)
                        .ToList(),
                })
                .OrderBy(p => p.Code)
                .ToList();
        }

        public IList<ProcedureProgrammeTreePVO> GetPortalArchivedPublicDiscussionProcedureProgrammesTree()
        {
            return this.GetProcedureProgrammesTreeInternal(ProcedureTypeForProgrammesTreeInternal.ArchivedPublicDiscussion)
                .Select(p => new ProcedureProgrammeTreePVO
                {
                    Code = p.Code,
                    Name = p.Name,
                    NameAlt = p.NameAlt,
                    ProgrammePriorities = p.ProgrammePriorities
                        .Select(pp => new ProcedureProgrammePriorityTreePVO
                        {
                            Number = int.Parse(pp.Code.Substring(pp.Code.LastIndexOf('-') + 1)),
                            Code = pp.Code,
                            Name = pp.Name,
                            NameAlt = pp.NameAlt,
                            Procedures = pp.Procedures
                                .Select(pr => new ProcedureTreePVO
                                {
                                    Number = pr.Number,
                                    Gid = pr.Gid,
                                    Code = pr.Code,
                                    Name = pr.Name,
                                    NameAlt = pr.NameAlt,
                                    Status = pr.Status,
                                    StatusText = pr.Status,
                                    IsIntroducedByLAG = pr.IsIntroducedByLAG,
                                })
                                .Where(pr => pr.Status != ProcedureStatus.Canceled)
                                .OrderBy(pr => pr.Number)
                                .ToList(),
                        })
                        .OrderBy(pp => pp.Number)
                        .ToList(),
                })
                .OrderBy(p => p.Code)
                .ToList();
        }

        public int GetPrimaryProcedureProgrammeId(int procedureId)
        {
            return (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                    where ps.ProcedureId == procedureId && ps.IsPrimary
                    select ps.ProgrammeId)
                   .Single();
        }

        public int GetPrimaryProcedureProgrammePriorityId(int procedureId)
        {
            return (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                    where ps.ProcedureId == procedureId && ps.IsPrimary
                    select ps.ProgrammePriorityId)
                   .Single();
        }

        public (int ProgrammeId, int ProgrammePriorityId) GetProcedureParentData(int procedureId)
        {
            return (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                    where ps.ProcedureId == procedureId && ps.IsPrimary
                    select new
                    {
                        programmeId = ps.ProgrammeId,
                        programmePriorityId = ps.ProgrammePriorityId,
                    })
                    .ToList()
                    .Select(x => (ProgrammeId: x.programmeId, ProgrammePriorityId: x.programmePriorityId))
                    .Single();
        }

        public int? GetRelatedProgrammePriority(int programmeId, int procedureId)
        {
            var programmePriorities = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                                       where ps.ProcedureId == procedureId && ps.ProgrammeId == programmeId
                                       select ps.ProgrammePriorityId)
                                       .ToList();

            if (programmePriorities.Count == 1)
            {
                return programmePriorities.Single();
            }

            return null;
        }

        public int[] GetProcedureProgrammeIds(int procedureId)
        {
            return (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                    where ps.ProcedureId == procedureId
                    select ps.ProgrammeId)
                   .ToArray();
        }

        public ProcedureStatus GetProcedureStatus(int procedureId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Procedure>()
                    where p.ProcedureId == procedureId
                    select p.ProcedureStatus).Single();
        }

        public ProcedureInfoVO GetProcedureInfo(int procedureId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Procedure>().Where(p => p.ProcedureId == procedureId)

                    join pi in this.unitOfWork.DbContext.Set<ProcedureApplicationSection>().Where(x => x.Section == ApplicationSectionType.Indicators) on p.ProcedureId equals pi.ProcedureId into g0
                    from pi in g0.DefaultIfEmpty()

                    join pai in this.unitOfWork.DbContext.Set<ProcedureApplicationSection>().Where(x => x.Section == ApplicationSectionType.AdditionalInformation) on p.ProcedureId equals pai.ProcedureId into g1
                    from pai in g1.DefaultIfEmpty()

                    join pad in this.unitOfWork.DbContext.Set<ProcedureApplicationSection>().Where(x => x.Section == ApplicationSectionType.AttachedDocuments) on p.ProcedureId equals pad.ProcedureId into g2
                    from pad in g2.DefaultIfEmpty()

                    where p.ProcedureId == procedureId
                    select new ProcedureInfoVO()
                    {
                        Name = p.Name,
                        Version = p.Version,
                        StatusName = p.ProcedureStatus,
                        Status = p.ProcedureStatus,
                        ActivationDate = p.ActivationDate,
                        ApplicationFormType = p.ApplicationFormType,
                        ProcedureKind = p.ProcedureKind,
                        ProcedureContractReportDocumentsSectionStatus = p.ProcedureContractReportDocumentsSectionStatus,
                        IsIndicatorVisible = pi == null ? false : pi.IsSelected,
                        IsTimeLimitsVisible = p.ProcedureKind == ProcedureKind.Schema,
                        IsAdditionalInformationVisible = pai == null ? false : pai.IsSelected,
                        IsAttachedDocumentsVisible = pad == null ? false : pad.IsSelected,
                    })
                    .SingleOrDefault();
        }

        public ApplicationFormType GetProcedureApplicationFormType(int procedureId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Procedure>()
                    where p.ProcedureId == procedureId
                    select p.ApplicationFormType).Single();
        }

        public ProcedureBasicDataVO GetProcedureBasicData(int procedureId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Procedure>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals pr.MapNodeId
                    where p.ProcedureId == procedureId && ps.IsPrimary
                    select new ProcedureBasicDataVO()
                    {
                        Name = p.Name,
                        NameAlt = p.NameAlt,
                        Code = p.Code,
                        PrimaryProgrammeName = pr.Name,
                    }).SingleOrDefault();
        }

        public IList<ProcedureQuestionsVO> GetProcedureQuestions(int procedureId)
        {
            return (from pq in this.unitOfWork.DbContext.Set<ProcedureQuestion>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on pq.BlobKey equals b.Key
                    join u in this.unitOfWork.DbContext.Set<User>() on pq.CreatedByUserId equals u.UserId
                    where pq.ProcedureId == procedureId
                    select new { pq, b, u })
                        .Select(p => new ProcedureQuestionsVO
                        {
                            ProcedureQuestionId = p.pq.ProcedureQuestionId,
                            ProcedureId = p.pq.ProcedureId,
                            CreatedByUser = p.u.Fullname + "(" + p.u.Username + ")",
                            CreateDate = p.pq.CreateDate,
                            IsActivated = p.pq.IsActivated,
                            ActiveStatus = p.pq.IsActivated ? ActiveStatus.Active : ActiveStatus.NotActivated,
                            File = new FileVO
                            {
                                Key = p.b.Key,
                                Name = p.b.FileName,
                            },
                        })
                        .OrderBy(t => t.CreateDate)
                        .ToList();
        }

        public ProcedureEvalTable GetProcedureEvalTable(int procedureId, ProcedureEvalTableType evalTableType)
        {
            return (from pet in this.unitOfWork.DbContext.Set<ProcedureEvalTable>()
                    where pet.ProcedureId == procedureId && pet.Type == evalTableType && pet.IsActivated && pet.IsActive
                    select pet)
                    .SingleOrDefault();
        }

        public IList<BudgetLevel2EuPercentPVO> GetBudgetLevel2EuPercent(Guid procedureGid, string programmeCode)
        {
            var q = (from p in this.unitOfWork.DbContext.Set<Procedure>()
                     join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                     join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                     join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on ps.ProcedureShareId equals l2.ProcedureShareId
                     where p.Gid == procedureGid && prog.Code == programmeCode
                     select new
                     {
                         l2.Gid,
                         ps.BgAmount,
                     })
                    .ToList();

            return (from l2 in q
                    let eu = l2.BgAmount != 0 ? l2.BgAmount : 0
                    select new BudgetLevel2EuPercentPVO
                    {
                        Gid = l2.Gid,
                        EuPercent = Math.Round((eu + 0.00001m) * 100) / 100,
                    })
                .ToList();
        }

        public IList<ProcedureItemVO> GetProcedureItems(int programmeId, int[] exceptIds)
        {
            var proceduresData = (from proc in this.unitOfWork.DbContext.Set<Procedure>()
                                  join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                                  join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                  join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                                  join ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>() on proc.ProcedureId equals ptl.ProcedureId into g0
                                  from ptl in g0.DefaultIfEmpty()
                                  where !exceptIds.Contains(proc.ProcedureId) && prog.MapNodeId == programmeId && Procedure.EvalSessionOrProjectCreationStatuses.Contains(proc.ProcedureStatus)
                                  select new
                                  {
                                      proc.ProcedureId,
                                      proc.Code,
                                      proc.Name,
                                      proc.ProcedureStatus,
                                      ProgramPriorityId = pp.MapNodeId,
                                      ProgrammePriorityName = pp.Name,
                                      ProgrammeId = prog.MapNodeId,
                                      ProgrammeName = prog.Name,
                                      EndDate = (DateTime?)ptl.EndDate,
                                  }).ToList();

            return (from pd in proceduresData
                    group pd by new
                    {
                        pd.ProcedureId,
                        pd.Code,
                        pd.Name,
                        pd.ProcedureStatus,
                    }
                    into g
                    select new ProcedureItemVO
                    {
                        ItemId = g.Key.ProcedureId,
                        Name = g.Key.Name,
                        Code = g.Key.Code,
                        Status = g.Key.ProcedureStatus,
                        EndingDate = g.Select(pd => pd.EndDate).Max(),
                        ProgrammeNames = g.Select(pd => pd.ProgrammeName).Distinct().ToList(),
                    })
                    .ToList();
        }

        public ProcedureInfoPVO GetPortalProcedureInfo(Guid procedureGid)
        {
            var procedure = (from p in this.Set()

                             where p.Gid == procedureGid
                             select new
                             {
                                 Procedure = p,
                                 ProcedureId = p.ProcedureId,
                                 Status = p.ProcedureStatus,
                             })
                             .FirstOrDefault();

            if (procedure == null)
            {
                throw new DataObjectNotFoundException("Procedures", procedureGid);
            }

            var appGuidelines = (from pag in this.unitOfWork.DbContext.Set<ProcedureApplicationGuideline>()
                                 join b in this.unitOfWork.DbContext.Set<Blob>() on pag.BlobKey equals b.Key
                                 where pag.ProcedureId == procedure.ProcedureId
                                 select pag)
                                 .Include(p => p.File)
                                 .ToList();

            ProcedureVersion version = new Eumis.Domain.Procedures.ProcedureVersion(
                1,
                procedure.Procedure.ProcedureId,
                procedure.Procedure.Gid,
                procedure.Procedure.Name,
                procedure.Procedure.NameAlt,
                procedure.Procedure.Code,
                procedure.Procedure.Description,
                procedure.Procedure.DescriptionAlt,
                procedure.Procedure.ApplicationFormType,
                procedure.Procedure.ProcedureKind,
                procedure.Procedure.Year,
                procedure.Procedure.ProjectDuration,
                (int?)null,
                (Guid?)null,
                null,
                (DateTime?)null,
                appGuidelines.Select(ag => new ProcedureAppGuidlineJson(ag)).ToList(),
                procedure.Procedure.ProcedureApplicationDocs.Select(ad => new ProcedureAppDocJson(ad)).ToList(),
                procedure.Procedure.ProcedureSpecFields.Select(sp => new ProcedureSpecFieldJson(sp)).ToList(),
                new List<ProcedureProgrammeJson>(),
                new List<ProcedureLocationJson>(),
                new List<ProcedureApplicationSectionJson>(),
                new List<DirectionPairJson>(),
                new List<ProcedureDeclarationJson>(),
                false);

            var timeLimits = (from l in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                              where l.ProcedureId == procedure.ProcedureId
                              orderby l.EndDate
                              select new { l.ProcedureTimeLimitId, l.EndDate, l.Notes }).ToList();

            var actualTimeLimits = timeLimits.Where(t => t.EndDate >= DateTime.Now).DefaultIfEmpty(timeLimits.Last()).First();

            return new ProcedureInfoPVO(version.ProcedureVersionJson, false, procedureGid, procedure.Status, actualTimeLimits.EndDate);
        }

        public IList<ProcedureLocationsVO> GetProcedureLocations(int procedureId)
        {
            return (from pl in this.unitOfWork.DbContext.Set<ProcedureLocation>().Where(x => x.ProcedureId == procedureId)

                    join c in this.unitOfWork.DbContext.Set<Country>() on pl.CountryId equals c.CountryId into g1
                    from c in g1.DefaultIfEmpty()
                    join n1 in this.unitOfWork.DbContext.Set<Nuts1>() on pl.Nuts1Id equals n1.Nuts1Id into g2
                    from n1 in g2.DefaultIfEmpty()
                    join n2 in this.unitOfWork.DbContext.Set<Nuts2>() on pl.Nuts2Id equals n2.Nuts2Id into g3
                    from n2 in g3.DefaultIfEmpty()
                    join d in this.unitOfWork.DbContext.Set<District>() on pl.DistrictId equals d.DistrictId into g4
                    from d in g4.DefaultIfEmpty()
                    join m in this.unitOfWork.DbContext.Set<Municipality>() on pl.MunicipalityId equals m.MunicipalityId into g5
                    from m in g5.DefaultIfEmpty()
                    join s in this.unitOfWork.DbContext.Set<Settlement>() on pl.SettlementId equals s.SettlementId into g6
                    from s in g6.DefaultIfEmpty()
                    join p in this.unitOfWork.DbContext.Set<ProtectedZone>() on pl.ProtectedZoneId equals p.ProtectedZoneId into g7
                    from p in g7.DefaultIfEmpty()

                    select new ProcedureLocationsVO
                    {
                        ProcedureLocationId = pl.ProcedureLocationId,
                        NutsLevel = pl.NutsLevel,
                        FullPath = c.Name ?? n1.Name ?? n2.Name ?? d.Name ?? m.Name ?? s.Name ?? p.Name,
                    }).ToList();
        }

        public List<ProcedureEvalTable> GetProcedureActiveEvalTables(int procedureId)
        {
            return this.unitOfWork.DbContext.Set<ProcedureEvalTable>()
                .Where(x => x.ProcedureId == procedureId && x.IsActivated && x.IsActive)
                .ToList();
        }

        public IList<ProcedureContractReportDocument> FindProcedureReportDocuments(int procedureId, ProcedureContractReportDocumentType documentType)
        {
            return this.unitOfWork.DbContext.Set<ProcedureContractReportDocument>()
                    .Where(x => x.ProcedureId == procedureId && x.IsActive && x.IsActivated)
                    .ToList()
                    .Where(x => x.Type == documentType)
                    .ToList();
        }

        public async Task<IList<ProcedureContractReportDocument>> FindProcedureReportDocumentsAsync(int procedureId, ProcedureContractReportDocumentType documentType, CancellationToken ct)
        {
            var resultTask = this.unitOfWork.DbContext.Set<ProcedureContractReportDocument>()
                .Where(x => x.ProcedureId == procedureId && x.IsActive && x.IsActivated)
                .ToListAsync(ct);

            var result = (await resultTask)
                .Where(x => x.Type == documentType)
                .ToList();

            return result;
        }

        public IList<ApplicationSectionVO> GetApplicationSections(int procedureId)
        {
            var procedure = this.FindWithoutIncludes(procedureId);
            var procedureTypeSections = procedure.GetApplicableSections().Select((x, t) => new { Section = x, Index = t + 1 });
            var procedureAdditionalSettings = this.unitOfWork.DbContext.Set<ProcedureApplicationSectionAdditionalSetting>()
                .Where(i => i.ProcedureId == procedureId)
                .FirstOrDefault();
            procedureAdditionalSettings = procedureAdditionalSettings ?? new ProcedureApplicationSectionAdditionalSetting(procedureId);

            var result = (from pts in procedureTypeSections

                          join pas in this.unitOfWork.DbContext.Set<ProcedureApplicationSection>().Where(t => t.ProcedureId == procedureId) on pts.Section equals pas.Section into g0
                          from pas in g0.DefaultIfEmpty()
                          select new ApplicationSectionVO
                          {
                              ApplicationSection = pts.Section,
                              IsSelected = pas == null ? (procedure.ProcedureKind == ProcedureKind.Budget ? true : (bool?)null) : pas.IsSelected,
                              ProcedureId = procedureId,
                              OrderNum = pas == null ? pts.Index : pas.OrderNum,
                              AdditionalSettings = new List<ApplicationSectionAdditionalSettingVO>(),
                          })
                          .OrderBy(t => t.OrderNum)
                          .ToList();

            result.ForEach(i =>
            {
                foreach (var property in procedureAdditionalSettings.GetType().GetProperties().Where(prop => prop.PropertyType == typeof(bool)))
                {
                    var propertyValue = (bool)property.GetValue(procedureAdditionalSettings);
                    var foundAdditionalSetting = ProcedureApplicationSectionAdditionalSettingType.GetType(i.ApplicationSection, property.Name);

                    if (foundAdditionalSetting.HasValue)
                    {
                        i.AdditionalSettings.Add(new ApplicationSectionAdditionalSettingVO()
                        {
                            Type = property.Name,
                            IsSelected = propertyValue,
                            Description = foundAdditionalSetting?.description,
                            Info = foundAdditionalSetting?.info,
                        });
                    }
                }
            });

            return result;
        }

        public IList<ProcedureDirectionVO> GetProcedureDirections(int procedureId)
        {
            var result = (from pd in this.unitOfWork.DbContext.Set<ProcedureDirection>().Where(p => p.ProcedureId == procedureId)
                          join d in this.unitOfWork.DbContext.Set<Direction>() on pd.DirectionId equals d.DirectionId

                          join sd in this.unitOfWork.DbContext.Set<SubDirection>() on new { SubDirectionId = pd.SubDirectionId.HasValue ? pd.SubDirectionId.Value : 0 } equals new { sd.SubDirectionId } into g0
                          from sd in g0.DefaultIfEmpty()
                          select new ProcedureDirectionVO
                          {
                              DirectionName = d.Name,
                              DirectionNameAlt = d.NameAlt,
                              SubDirectionName = sd.Name,
                              SubDirectionNameAlt = sd.NameAlt,
                              Amount = pd.Amount,
                              ProcedureDirectionId = pd.ProcedureDirectionId,
                              ProgrammePriorityId = pd.ProgrammePriorityId,
                          })
                          .ToList();

            return result;
        }

        public IList<MapNodeDirectionVO> GetProgrammePriorityDirections(int procedureId)
        {
            var attachedDirecions = this.unitOfWork.DbContext.Set<ProcedureDirection>().Where(x => x.ProcedureId == procedureId);

            var result = (from p in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(p => p.ProcedureId == procedureId && p.IsPrimary)
                          join mnd in this.unitOfWork.DbContext.Set<MapNodeDirection>() on p.ProgrammePriorityId equals mnd.MapNodeId
                          join d in this.unitOfWork.DbContext.Set<Direction>() on mnd.DirectionId equals d.DirectionId

                          join sd in this.unitOfWork.DbContext.Set<SubDirection>() on new { SubDirectionId = mnd.SubDirectionId.HasValue ? mnd.SubDirectionId.Value : 0 } equals new { sd.SubDirectionId } into g0
                          from sd in g0.DefaultIfEmpty()

                          where !attachedDirecions.Any(t => t.DirectionId == d.DirectionId && t.SubDirectionId == sd.SubDirectionId)
                          select new MapNodeDirectionVO
                          {
                              MapNodeId = p.ProgrammePriorityId,
                              MapNodeDirectionId = mnd.MapNodeDirectionId,
                              DirectionName = d.Name,
                              DirectionNameAlt = d.NameAlt,
                              SubDirectionName = sd.Name,
                              SubDirectionNameAlt = sd.NameAlt,
                          })
                          .ToList();

            return result;
        }

        public ProgrammePriorityCompany GetProgrammePriorityCompany(int procedureId)
        {
            var companyData = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(x => x.ProcedureId == procedureId)
                               join cd in this.unitOfWork.DbContext.Set<ProgrammePriorityCompany>() on ps.ProgrammePriorityId equals cd.ProgrammePriorityId
                               select cd)
                               .Single();
            return companyData;
        }

        public ProcedureApplicationDoc FindProcedureAppDoc(int procedureApplicationDocId)
        {
            return this.unitOfWork.DbContext.Set<ProcedureApplicationDoc>()
                .Where(pad => pad.ProcedureApplicationDocId == procedureApplicationDocId)
                .Single();
        }

        #region private

        #region Budget

        private class BudgetLevel0
        {
            public int ProgrammeId { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            public string NameAlt { get; set; }

            public List<BudgetLevel1> ExpenseTypes { get; set; }
        }

        private class BudgetLevel1
        {
            public int ProcedureBudgetLevel1Id { get; set; }

            public Guid Gid { get; set; }

            public string Name { get; set; }

            public string NameAlt { get; set; }

            public int OrderNum { get; set; }

            public bool IsActivated { get; set; }

            public bool IsActive { get; set; }

            public List<BudgetLevel2> Expenses { get; set; }
        }

        private class BudgetLevel2
        {
            public int ProcedureBudgetLevel2Id { get; set; }

            public Guid Gid { get; set; }

            public string Name { get; set; }

            public string NameAlt { get; set; }

            public ProcedureBudgetLevel2AidMode AidMode { get; set; }

            public bool IsEligibleCost { get; set; }

            public bool IsStandardTablesExpense { get; set; }

            public bool IsOneTimeExpense { get; set; }

            public bool IsFlatRateExpense { get; set; }

            public bool IsLandExpense { get; set; }

            public bool IsEuApprovedStandardTablesExpense { get; set; }

            public bool IsEuApprovedOneTimeExpense { get; set; }

            public int OrderNum { get; set; }

            public bool IsActivated { get; set; }

            public bool IsActive { get; set; }

            public string ProgrammePriorityName { get; set; }

            public string ProgrammePriorityCode { get; set; }

            public List<BudgetLevel3> Details { get; set; }
        }

        private class BudgetLevel3
        {
            public int ProcedureBudgetLevel3Id { get; set; }

            public Guid Gid { get; set; }

            public string Note { get; set; }

            public int OrderNum { get; set; }
        }

        private List<BudgetLevel0> GetExpenseBudgetProgrammes(int procedureId)
        {
            var lev0 = from prp in this.unitOfWork.DbContext.Set<ProcedureProgramme>()
                       join p in this.unitOfWork.DbContext.Set<Programme>() on prp.ProgrammeId equals p.MapNodeId
                       where prp.ProcedureId == procedureId
                       select new
                       {
                           ProgrammeId = p.MapNodeId,
                           Name = p.Name,
                           Code = p.Code,
                           NameAlt = p.NameAlt,
                       };

            var lev1 = from pbl1 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel1>()
                       join et in this.unitOfWork.DbContext.Set<ExpenseType>() on pbl1.ExpenseTypeId equals et.ExpenseTypeId
                       where pbl1.ProcedureId == procedureId
                       select new
                       {
                           ProcedureBudgetLevel1Id = pbl1.ProcedureBudgetLevel1Id,
                           ProgrammeId = pbl1.ProgrammeId,
                           Gid = pbl1.Gid,
                           ExpenseTypeName = et.Name,
                           OrderNum = pbl1.OrderNum,
                           IsActivated = pbl1.IsActivated,
                           IsActive = pbl1.IsActive,
                           NameAlt = et.NameAlt,
                       };

            var lev2 = from pbl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>()
                       join pbl1 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel1>() on pbl2.ProcedureBudgetLevel1Id equals pbl1.ProcedureBudgetLevel1Id
                       join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pbl2.ProcedureShareId equals ps.ProcedureShareId
                       join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                       where ps.ProcedureId == procedureId
                       select new
                       {
                           ProcedureBudgetLevel2Id = pbl2.ProcedureBudgetLevel2Id,
                           ProcedureBudgetLevel1Id = pbl2.ProcedureBudgetLevel1Id,
                           Gid = pbl2.Gid,
                           Name = pbl2.Name,
                           NameAlt = pbl2.NameAlt,
                           ProgrammePriorityName = pp.Name,
                           ProgrammePriorityCode = pp.Code,
                           AidMode = pbl2.AidMode,
                           OrderNum = pbl2.OrderNum,
                           IsActivated = pbl2.IsActivated,
                           IsActive = pbl2.IsActive,
                       };

            var lev3 = from pbl3 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel3>()
                       join pbl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on pbl3.ProcedureBudgetLevel2Id equals pbl2.ProcedureBudgetLevel2Id
                       join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pbl2.ProcedureShareId equals ps.ProcedureShareId
                       where ps.ProcedureId == procedureId
                       select new
                       {
                           ProcedureBudgetLevel3Id = pbl3.ProcedureBudgetLevel3Id,
                           ProcedureBudgetLevel2Id = pbl2.ProcedureBudgetLevel2Id,
                           Gid = pbl3.Gid,
                           Note = pbl3.Note,
                           OrderNum = pbl3.OrderNum,
                       };

            var joinedValues = (from l0 in lev0

                                join l1 in lev1 on l0.ProgrammeId equals l1.ProgrammeId into g1
                                from l1 in g1.DefaultIfEmpty()

                                join l2 in lev2 on l1.ProcedureBudgetLevel1Id equals l2.ProcedureBudgetLevel1Id into g2
                                from l2 in g2.DefaultIfEmpty()

                                join l3 in lev3 on l2.ProcedureBudgetLevel2Id equals l3.ProcedureBudgetLevel2Id into g3
                                from l3 in g3.DefaultIfEmpty()

                                select new { l0 = l0, l1 = l1, l2 = l2, l3 = l3 })
                               .ToList();

            var budget =
                (from item in joinedValues
                 group item by new { l0 = item.l0, l1 = item.l1, l2 = item.l2 } into byL0L1L2
                 group byL0L1L2 by new { l0 = byL0L1L2.Key.l0, l1 = byL0L1L2.Key.l1 } into byL0L1
                 group byL0L1 by byL0L1.Key.l0 into byL0
                 select new BudgetLevel0
                 {
                     ProgrammeId = byL0.Key.ProgrammeId,
                     Name = byL0.Key.Name,
                     Code = byL0.Key.Code,
                     NameAlt = byL0.Key.NameAlt,
                     ExpenseTypes = byL0.Where(byL0L1 => byL0L1.Key.l1 != null).Select(byL1L2 =>
                         new BudgetLevel1
                         {
                             ProcedureBudgetLevel1Id = byL1L2.Key.l1.ProcedureBudgetLevel1Id,
                             Gid = byL1L2.Key.l1.Gid,
                             Name = byL1L2.Key.l1.ExpenseTypeName,
                             NameAlt = byL1L2.Key.l1.NameAlt,
                             OrderNum = byL1L2.Key.l1.OrderNum,
                             IsActivated = byL1L2.Key.l1.IsActivated,
                             IsActive = byL1L2.Key.l1.IsActive,
                             Expenses = byL1L2.Where(byL0L1L2 => byL0L1L2.Key.l2 != null).Select(byL1L2L3 =>
                                 new BudgetLevel2
                                 {
                                     ProcedureBudgetLevel2Id = byL1L2L3.Key.l2.ProcedureBudgetLevel2Id,
                                     Gid = byL1L2L3.Key.l2.Gid,
                                     Name = byL1L2L3.Key.l2.Name,
                                     NameAlt = byL1L2L3.Key.l2.NameAlt,
                                     ProgrammePriorityName = byL1L2L3.Key.l2.ProgrammePriorityName,
                                     ProgrammePriorityCode = byL1L2L3.Key.l2.ProgrammePriorityCode,
                                     AidMode = byL1L2L3.Key.l2.AidMode,
                                     OrderNum = byL1L2L3.Key.l2.OrderNum,
                                     IsActivated = byL1L2L3.Key.l2.IsActivated,
                                     IsActive = byL1L2L3.Key.l2.IsActive,
                                     Details = byL1L2L3.Where(byL0L1L2L3 => byL0L1L2L3.l3 != null).Select(byL1L2L3L4 =>
                                         new BudgetLevel3
                                         {
                                             ProcedureBudgetLevel3Id = byL1L2L3L4.l3.ProcedureBudgetLevel3Id,
                                             Gid = byL1L2L3L4.l3.Gid,
                                             Note = byL1L2L3L4.l3.Note,
                                             OrderNum = byL1L2L3L4.l3.OrderNum,
                                         })
                                         .OrderBy(e => e.OrderNum)
                                         .ToList(),
                                 })
                                 .OrderBy(e => e.OrderNum)
                                 .ToList(),
                         })
                         .OrderBy(e => e.OrderNum)
                         .ToList(),
                 })
                 .OrderBy(e => e.Name)
                 .ToList();

            return budget;
        }

        private List<ProcedureBudgetValidationRule> GetProcedureBudgetValidationRules(int procedureId)
        {
            return (from vr in this.unitOfWork.DbContext.Set<ProcedureBudgetValidationRule>()
                        .Include(vr => vr.ProcedureProgramme.ProcedureBudgetLevel1.Select(l1 => l1.ProcedureBudgetLevel2)) // TODO do not include entities in queries just to make the Domain object work
                    where vr.ProcedureId == procedureId
                    select vr)
                    .ToList();
        }

        #endregion // Budget

        #region Programmes tree

        private class ProcedureProgrammeTree
        {
            public int ProgrammeId { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            public string NameAlt { get; set; }

            public IList<ProcedureProgrammePriorityTree> ProgrammePriorities { get; set; }
        }

        private class ProcedureProgrammePriorityTree
        {
            public int ProgrammeId { get; set; }

            public int ProgrammePriorityId { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            public string NameAlt { get; set; }

            public IList<ProcedureTree> Procedures { get; set; }
        }

        private class ProcedureTree
        {
            public int ProgrammeId { get; set; }

            public int ProgrammePriorityId { get; set; }

            public int ProcedureId { get; set; }

            public Guid Gid { get; set; }

            public int Number { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            public string NameAlt { get; set; }

            public ProcedureStatus Status { get; set; }

            public bool IsIntroducedByLAG { get; set; }
        }

        public enum ProcedureTypeForProgrammesTreeInternal
        {
            Active,
            Ended,
            PublicDiscussion,
            ArchivedPublicDiscussion,
            All,
        }

        private IList<ProcedureProgrammeTree> GetProcedureProgrammesTreeInternal(ProcedureTypeForProgrammesTreeInternal type)
        {
            var programmePredicate = PredicateBuilder.True<Programme>();
            var programmePriorityPredicate = PredicateBuilder.True<ProgrammePriority>();
            var procedurePredicate = PredicateBuilder.True<Procedure>();

            IQueryable<Procedure> procedures = this.unitOfWork.DbContext.Set<Procedure>().AsQueryable();
            var currentType = DateTime.Now;

            switch (type)
            {
                case ProcedureTypeForProgrammesTreeInternal.Active:
                    programmePredicate = programmePredicate.And(p => p.Status == MapNodeStatus.Entered);
                    programmePriorityPredicate = programmePriorityPredicate.And(pp => pp.Status == MapNodeStatus.Entered);
                    procedurePredicate = procedurePredicate.And(p => p.ProcedureStatus == ProcedureStatus.Active);
                    break;
                case ProcedureTypeForProgrammesTreeInternal.Ended:
                    programmePredicate = programmePredicate.And(p => p.Status == MapNodeStatus.Entered);
                    programmePriorityPredicate = programmePriorityPredicate.And(pp => pp.Status == MapNodeStatus.Entered);
                    procedurePredicate = procedurePredicate.And(p => p.ProcedureStatus == ProcedureStatus.Ended || p.ProcedureStatus == ProcedureStatus.Terminated);
                    break;
                default:
                    break;
            }

            if (type == ProcedureTypeForProgrammesTreeInternal.Active || type == ProcedureTypeForProgrammesTreeInternal.Ended)
            {
                // Portal procedures should not be of type "Budget"
                procedurePredicate = procedurePredicate.And(p => p.ProcedureKind == ProcedureKind.Schema);
            }

            var map = (from p in this.unitOfWork.DbContext.Set<Programme>().Where(programmePredicate)
                       join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>().Where(programmePriorityPredicate) on p.MapNodeId equals pp.MapNodeRelation.ProgrammeId into g1
                       from pp in g1.DefaultIfEmpty()
                       join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pp.MapNodeId equals ps.ProgrammePriorityId into g2
                       from ps in g2.DefaultIfEmpty()
                       join pr in procedures.Where(procedurePredicate) on ps.ProcedureId equals pr.ProcedureId into g3
                       from pr in g3.DefaultIfEmpty()
                       join prn in this.unitOfWork.DbContext.Set<ProcedureNumber>() on pr.ProcedureId equals prn.ProcedureId into g4
                       from prn in g4.DefaultIfEmpty()
                       select new
                       {
                           p = new
                           {
                               ProgrammeId = p.MapNodeId,
                               p.Name,
                               p.Code,
                               p.NameAlt,
                           },
                           pp = new
                           {
                               ProgrammeId = p.MapNodeId,
                               ProgrammePriorityId = (int?)pp.MapNodeId,
                               pp.Name,
                               pp.Code,
                               pp.NameAlt,
                           },
                           pr = new
                           {
                               Number = prn == null ? (int?)null : prn.Number,
                               Code = pr.Code,
                               ProgrammeId = p.MapNodeId,
                               ProgrammePriorityId = (int?)pp.MapNodeId,
                               ProcedureId = (int?)pr.ProcedureId,
                               Gid = (Guid?)pr.Gid,
                               Name = pr.Name,
                               Status = (ProcedureStatus?)pr.ProcedureStatus,
                               IsPrimary = (bool?)ps.IsPrimary,
                               pr.NameAlt,
                           },
                       })
                    .ToList();

            return (from p_pp_pr in map
                    group p_pp_pr by new { p_pp_pr.p, p_pp_pr.pp } into p_pp
                    group p_pp by p_pp.Key.p into p
                    select new ProcedureProgrammeTree
                    {
                        ProgrammeId = p.Key.ProgrammeId,
                        Name = p.Key.Name,
                        Code = p.Key.Code,
                        NameAlt = p.Key.NameAlt,
                        ProgrammePriorities = p
                            .Where(p_pp => p_pp.Key.pp != null && p_pp.Key.pp.ProgrammePriorityId != null)
                            .Select(p_pp => new ProcedureProgrammePriorityTree
                            {
                                ProgrammeId = p_pp.Key.pp.ProgrammeId,
                                ProgrammePriorityId = p_pp.Key.pp.ProgrammePriorityId.Value,
                                Name = p_pp.Key.pp.Name,
                                Code = p_pp.Key.pp.Code,
                                NameAlt = p_pp.Key.pp.NameAlt,
                                Procedures = p_pp
                                    .Where(p_pp_pr => p_pp_pr.pr != null && p_pp_pr.pr.ProcedureId != null && p_pp_pr.pr.IsPrimary.Value)
                                    .Select(p_pp_pr => p_pp_pr.pr)
                                    .Distinct()
                                    .Select(pr => new ProcedureTree
                                    {
                                        Number = pr.Number.Value,
                                        Code = pr.Code,
                                        ProgrammeId = pr.ProgrammeId,
                                        ProgrammePriorityId = pr.ProgrammePriorityId.Value,
                                        ProcedureId = pr.ProcedureId.Value,
                                        Gid = pr.Gid.Value,
                                        Name = pr.Name,
                                        Status = pr.Status.Value,
                                        NameAlt = pr.NameAlt,
                                    })
                                    .ToList(),
                            })
                            .ToList(),
                    })
                    .ToList();
        }

        public int GetProcedureIdByProcedureCode(string code)
        {
            return this.unitOfWork.DbContext.Set<Procedure>()
                .Where(p => p.Code == code)
                .Select(p => p.ProcedureId)
                .Single();
        }

        #endregion // Programmes tree

        #region ProcedureApplicationDocs

        private class ProcedureAppDoc
        {
            public int ProcedureApplicationDocId { get; set; }

            public Guid Gid { get; set; }

            public string Name { get; set; }

            public string Extension { get; set; }

            public bool IsRequired { get; set; }

            public bool IsSignatureRequired { get; set; }

            public bool IsActivated { get; set; }

            public bool IsActive { get; set; }
        }

        private IList<ProcedureAppDoc> GetProcedureAppDocsInternal(int procedureId)
        {
            return (from pad in this.unitOfWork.DbContext.Set<ProcedureApplicationDoc>()
                    join pcd in this.unitOfWork.DbContext.Set<ProgrammeApplicationDocument>() on pad.ProgrammeApplicationDocumentId equals pcd.ProgrammeApplicationDocumentId into j1
                    from pcd in j1.DefaultIfEmpty()
                    where pad.ProcedureId == procedureId
                    select new ProcedureAppDoc
                    {
                        ProcedureApplicationDocId = pad.ProcedureApplicationDocId,
                        Gid = pad.Gid,
                        Name = pad.Name,
                        Extension = pcd != null ? pcd.Extension : pad.Extension,
                        IsRequired = pad.IsRequired,
                        IsSignatureRequired = pad.IsSignatureRequired,
                        IsActivated = pad.IsActivated,
                        IsActive = pad.IsActive,
                    }).ToList();
        }

        #endregion //ProcedureApplicationDoc

        #region ProcedureApplicationGuidelines

        private class ProcedureAppGuidline
        {
            public int ProcedureApplicationGuidelineId { get; set; }

            public Guid Gid { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public Guid BlobKey { get; set; }

            public string Filename { get; set; }
        }

        private IList<ProcedureAppGuidline> GetProcedureAppGuidelinesInternal(int procedureId)
        {
            return (from pag in this.unitOfWork.DbContext.Set<ProcedureApplicationGuideline>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on pag.BlobKey equals b.Key
                    where pag.ProcedureId == procedureId
                    select new ProcedureAppGuidline
                    {
                        ProcedureApplicationGuidelineId = pag.ProcedureApplicationGuidelineId,
                        Gid = pag.Gid,
                        Name = pag.Name,
                        Description = pag.Decription,
                        BlobKey = pag.BlobKey,
                        Filename = b.FileName,
                    })
                    .ToList();
        }

        public IList<ProcedureDiscussionsInfoPVO> GetPortalProcedureDiscussion(Guid procedureGid)
        {
            string bgQaTitle = DataTexts.ResourceManager.GetString(nameof(DataTexts.ProcedureRepository_QandA_Name), new CultureInfo(SystemLocalization.Bg_BG));
            string enQaTitle = DataTexts.ResourceManager.GetString(nameof(DataTexts.ProcedureRepository_QandA_Name), new CultureInfo(SystemLocalization.En_GB));
            return (from pq in this.unitOfWork.DbContext.Set<ProcedureQuestion>()
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on pq.ProcedureId equals p.ProcedureId
                    join b in this.unitOfWork.DbContext.Set<Blob>() on pq.BlobKey equals b.Key
                    join u in this.unitOfWork.DbContext.Set<User>() on pq.CreatedByUserId equals u.UserId
                    where p.Gid == procedureGid && pq.IsActivated
                    select new { pq, b, u })
                       .Select(p => new ProcedureDiscussionsInfoPVO
                       {
                           Name = bgQaTitle,
                           NameAlt = enQaTitle,
                           QaBlobKey = p.b.Key,
                           QaFileName = p.b.FileName,
                           QaModifyDate = p.pq.CreateDate,
                       })
                       .OrderBy(t => t.QaModifyDate)
                       .ToList();
        }

        #endregion // ProcedureApplicationGuidelines

        #endregion //private
    }
}
