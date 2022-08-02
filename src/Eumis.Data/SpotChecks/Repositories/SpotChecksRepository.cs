using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Linq;
using Eumis.Data.SpotChecks.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Companies;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.SpotChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.SpotChecks.Repositories
{
    internal class SpotChecksRepository : AggregateRepository<SpotCheck>, ISpotChecksRepository
    {
        public SpotChecksRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<SpotCheck, object>>[] Includes
        {
            get
            {
                return new Expression<Func<SpotCheck, object>>[]
                {
                    sc => sc.Items,
                    sc => sc.Targets,
                    sc => sc.Documents,
                    sc => sc.Ascertainments.Select(a => a.Items),
                    sc => sc.Recommendations.Select(r => r.Ascertainments),
                };
            }
        }

        public IList<SpotCheckVO> GetSpotCheks(
            int[] programmeIds,
            int userId,
            int? programmeId,
            SpotCheckStatus? status = null,
            SpotCheckType? type = null)
        {
            var predicate = PredicateBuilder.True<SpotCheck>();

            predicate = predicate
                .AndEquals(c => c.ProgrammeId, programmeId)
                .AndEquals(c => c.Status, status)
                .AndEquals(c => c.Type, type);

            var externalVerificatorSpotChecks = from ev in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                join sc in this.unitOfWork.DbContext.Set<SpotCheck>().Where(predicate) on ev.ContractId equals sc.ContractId
                                                select sc;

            predicate = predicate
                .And(c => programmeIds.Contains(c.ProgrammeId));

            var spotChecks =
                (from sc in this.unitOfWork.DbContext.Set<SpotCheck>().Where(predicate).Union(externalVerificatorSpotChecks)
                 join pr in this.unitOfWork.DbContext.Set<Programme>() on sc.ProgrammeId equals pr.MapNodeId

                 join scp in this.unitOfWork.DbContext.Set<SpotCheckPlan>() on sc.SpotCheckPlanId equals scp.SpotCheckPlanId into g1
                 from scp in g1.DefaultIfEmpty()

                 join sci in this.unitOfWork.DbContext.Set<SpotCheckItem>() on sc.SpotCheckId equals sci.SpotCheckId into g2
                 from sci in g2.DefaultIfEmpty()

                 join scic in this.unitOfWork.DbContext.Set<Contract>() on sci.ContractId equals scic.ContractId into g3
                 from scic in g3.DefaultIfEmpty()

                 join scc in this.unitOfWork.DbContext.Set<Contract>() on sc.ContractId equals scc.ContractId into g4
                 from scc in g4.DefaultIfEmpty()

                 select new
                 {
                     // spot check
                     SpotCheckId = sc.SpotCheckId,
                     CheckNum = sc.CheckNum,
                     CreateDate = sc.CreateDate,
                     RegNumber = sc.RegNumber,
                     ProgrammeName = pr.Name,
                     Level = sc.Level,
                     Status = sc.Status,
                     Type = sc.Type,
                     DateFrom = sc.DateFrom,
                     DateTo = sc.DateTo,
                     ProgrammeCode = pr.Code,

                     // spot check contract
                     SpotCheckContractId = (int?)scc.ContractId,
                     SpotCheckContractRegNumber = scc.RegNumber,
                     SpotCheckContractCompanyName = scc.CompanyName,
                     SpotCheckContractCompanyUinType = (UinType?)scc.CompanyUinType,
                     SpotCheckContractCompanyUin = scc.CompanyUin,

                     // spot check item contract
                     SpotCheckItemContractId = (int?)scic.ContractId,
                     SpotCheckItemContractRegNumber = scic.RegNumber,
                     SpotCheckItemContractCompanyName = scic.CompanyName,
                     SpotCheckItemContractCompanyUinType = (UinType?)scic.CompanyUinType,
                     SpotCheckItemContractCompanyUin = scic.CompanyUin,

                     // spot check plan
                     SpotCheckPlanId = (int?)scp.SpotCheckPlanId,
                     SpotCheckPlanMonth = (Month?)scp.Month,
                     SpotCheckPlanYear = (Year?)scp.Year,
                 })
                 .Distinct()
                 .ToList();

            var externalVerificatorSpotCheckIds = externalVerificatorSpotChecks.Select(x => x.SpotCheckId).Distinct();

            var spotCheckIds = spotChecks.Select(sc => sc.SpotCheckId).Union(externalVerificatorSpotCheckIds).Distinct().ToArray();

            var programmePriorities =
                        (from sc in spotChecks
                         join item in this.unitOfWork.DbContext.Set<SpotCheckItem>() on sc.SpotCheckId equals item.SpotCheckId
                         join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on item.ProgrammePriorityId equals pp.MapNodeId

                         where spotCheckIds.Contains(sc.SpotCheckId)

                         select new
                         {
                             SpotCheckId = item.SpotCheckId,
                             Code = pp.Code,
                         })
                        .ToList();

            var procedures =
                        (from sc in spotChecks
                         join item in this.unitOfWork.DbContext.Set<SpotCheckItem>() on sc.SpotCheckId equals item.SpotCheckId
                         join proc in this.unitOfWork.DbContext.Set<Procedure>() on item.ProcedureId equals proc.ProcedureId

                         where spotCheckIds.Contains(sc.SpotCheckId)

                         select new
                         {
                             SpotCheckId = item.SpotCheckId,
                             Code = proc.Code,
                         })
                         .Distinct()
                         .ToList();

            var contracts =
                        (from sc in spotChecks
                         join item in this.unitOfWork.DbContext.Set<SpotCheckItem>() on sc.SpotCheckId equals item.SpotCheckId
                         join c in this.unitOfWork.DbContext.Set<Contract>() on item.ContractId equals c.ContractId

                         where spotCheckIds.Contains(sc.SpotCheckId)

                         select new
                         {
                             SpotCheckId = item.SpotCheckId,
                             Code = c.RegNumber,
                         })
                         .Distinct()
                         .ToList();

            var contractContracts =
                        (from sc in spotChecks
                         join item in this.unitOfWork.DbContext.Set<SpotCheckItem>() on sc.SpotCheckId equals item.SpotCheckId
                         join cc in this.unitOfWork.DbContext.Set<ContractContract>() on item.ContractContractId equals cc.ContractContractId
                         join c in this.unitOfWork.DbContext.Set<Contract>() on cc.ContractId equals c.ContractId

                         where spotCheckIds.Contains(sc.SpotCheckId)

                         select new
                         {
                             SpotCheckId = item.SpotCheckId,
                             Code = c.RegNumber,
                         })
                         .Distinct()
                         .ToList();

            var dataItems = programmePriorities.Union(procedures).Union(contracts).Union(contractContracts);

            var ascertainments =
                (from sc in spotChecks.Distinct()
                 join sca in this.unitOfWork.DbContext.Set<SpotCheckAscertainment>() on sc.SpotCheckId equals sca.SpotCheckId
                 where spotCheckIds.Contains(sc.SpotCheckId)
                 select new
                 {
                     AscertainmentId = sca.SpotCheckAscertainmentId,
                     SpotCheckId = sca.SpotCheckId,
                     OrderNumber = sca.OrderNumber,
                     Ascertainment = sca.Ascertainment,
                 }).Distinct().ToList();

            var recommendations =
                (from sc in spotChecks.Distinct()
                 join scr in this.unitOfWork.DbContext.Set<SpotCheckRecommendation>() on sc.SpotCheckId equals scr.SpotCheckId
                 where spotCheckIds.Contains(sc.SpotCheckId)
                 select new
                 {
                     RecommendationId = scr.SpotCheckRecommendationId,
                     SpotCheckId = scr.SpotCheckId,
                     OrderNumber = scr.OrderNumber,
                     ExecutionStatus = scr.ExecutionStatus,
                     Recommendation = scr.Recommendation,
                 }).Distinct().ToList();

            return (from sc in spotChecks
                    group new
                    {
                        // spot check item contract
                        ContractId = (int?)sc.SpotCheckItemContractId,
                        RegNumber = sc.SpotCheckItemContractRegNumber,
                        CompanyName = sc.SpotCheckItemContractCompanyName,
                        CompanyUinType = (UinType?)sc.SpotCheckItemContractCompanyUinType,
                        CompanyUin = sc.SpotCheckItemContractCompanyUin,
                    }
                    by new
                    {
                        // spot check
                        SpotCheckId = sc.SpotCheckId,
                        CheckNum = sc.CheckNum,
                        CreateDate = sc.CreateDate,
                        RegNumber = sc.RegNumber,
                        ProgrammeName = sc.ProgrammeName,
                        Level = sc.Level,
                        Status = sc.Status,
                        Type = sc.Type,
                        ProgrammeCode = sc.ProgrammeCode,
                        DateFrom = sc.DateFrom,
                        DateTo = sc.DateTo,

                        // spot check contract
                        ContractId = (int?)sc.SpotCheckContractId,
                        ContractRegNumber = sc.SpotCheckContractRegNumber,
                        ContractCompanyName = sc.SpotCheckContractCompanyName,
                        ContractCompanyUinType = (UinType?)sc.SpotCheckContractCompanyUinType,
                        ContractCompanyUin = sc.SpotCheckContractCompanyUin,

                        // spot check plan
                        PlanId = (int?)sc.SpotCheckPlanId,
                        PlanMonth = (Month?)sc.SpotCheckPlanMonth,
                        PlanYear = (Year?)sc.SpotCheckPlanYear,
                    }
                    into g
                    orderby g.Key.CheckNum descending, g.Key.CreateDate descending
                    select g)
                .ToList()
                .Select(t => new SpotCheckVO
                {
                    SpotCheckId = t.Key.SpotCheckId,
                    RegNumber = t.Key.RegNumber,
                    ProgrammeName = t.Key.ProgrammeName,
                    Level = t.Key.Level,
                    Status = t.Key.Status,
                    Type = t.Key.Type,
                    SpotCheckPlan = t.Key.PlanId != null ? string.Format("{0} ({1} {2})", t.Key.ProgrammeCode, t.Key.PlanMonth.GetEnumDescription(), t.Key.PlanYear.GetEnumDescription()) : null,
                    DateFrom = t.Key.DateFrom,
                    DateTo = t.Key.DateTo,
                    ContractRegNumsCompanies =
                            t.Where(p => p.ContractId.HasValue)
                            .Select(p => string.Format("{0} ({1}, {2}: {3})", p.RegNumber, p.CompanyName, p.CompanyUinType.GetEnumDescription(), p.CompanyUin))
                            .Concat(t.Key.ContractId.HasValue ? new string[] { string.Format("{0} ({1}, {2}: {3})", t.Key.ContractRegNumber, t.Key.ContractCompanyName, t.Key.ContractCompanyUinType.GetEnumDescription(), t.Key.ContractCompanyUin) } : Enumerable.Empty<string>())
                            .ToList(),
                    ItemCodes =
                            dataItems.Where(i => i.SpotCheckId == t.Key.SpotCheckId)
                            .Select(i => i.Code)
                            .Distinct()
                            .ToList(),
                    Ascertainments =
                            ascertainments.Where(i => i.SpotCheckId == t.Key.SpotCheckId)
                            .Select(i => new SpotCheckAscertainmentContentVO { Id = i.AscertainmentId, OrderNum = i.OrderNumber, AscertainmentContent = i.Ascertainment })
                            .Distinct()
                            .ToList(),
                    Recommendations =
                            recommendations.Where(i => i.SpotCheckId == t.Key.SpotCheckId)
                            .Select(i => new SpotCheckRecommendationContentVO { Id = i.RecommendationId, OrderNum = i.OrderNumber, RecommendationContent = i.Recommendation })
                            .Distinct()
                            .ToList(),
                    RecommendationExecutionStatuses =
                            recommendations.Where(i => i.SpotCheckId == t.Key.SpotCheckId)
                            .Select(i => new SpotCheckRecommendationExecutionStatusVO { Id = i.RecommendationId, ExecutionStatus = i.ExecutionStatus })
                            .Distinct()
                            .ToList(),
                }).ToList();
        }

        public int GetProgrammeId(int spotCheckId)
        {
            return (from sc in this.unitOfWork.DbContext.Set<SpotCheck>()
                    where sc.SpotCheckId == spotCheckId
                    select sc.ProgrammeId).Single();
        }

        public int? GetContractId(int spotCheckId)
        {
            return (from sc in this.unitOfWork.DbContext.Set<SpotCheck>()
                    where sc.SpotCheckId == spotCheckId
                    select sc.ContractId).Single();
        }

        public SpotCheckInfoVO GetInfo(int spotCheckId)
        {
            return (from sc in this.unitOfWork.DbContext.Set<SpotCheck>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on sc.ProgrammeId equals p.MapNodeId
                    where sc.SpotCheckId == spotCheckId
                    select new SpotCheckInfoVO
                    {
                        ProgrammeCode = p.Code,
                        Status = sc.Status,
                        StatusDescr = sc.Status,
                        Level = sc.Level,
                        Version = sc.Version,
                    }).Single();
        }

        public SpotCheckBasicDataVO GetBasicData(int spotCheckId)
        {
            return (from sc in this.unitOfWork.DbContext.Set<SpotCheck>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on sc.ProgrammeId equals p.MapNodeId
                    join c in this.unitOfWork.DbContext.Set<Company>() on p.CompanyId equals c.CompanyId into g1
                    from c in g1.DefaultIfEmpty()
                    where sc.SpotCheckId == spotCheckId
                    select new SpotCheckBasicDataVO
                    {
                        SpotCheckId = sc.SpotCheckId,
                        SpotCheckPlanId = sc.SpotCheckPlanId,
                        Type = sc.Type,
                        Status = sc.Status,
                        RegNumber = sc.RegNumber,
                        Level = sc.Level,
                        ContractId = sc.ContractId,
                        ProgrammeCode = p.Code,
                        ProgrammeName = p.Name,
                        ProgrammeCompanyUinType = (UinType?)c.UinType,
                        ProgrammeCompanyUin = c.Uin,
                        ProgrammeCompanyName = c.Name,
                        Version = sc.Version,
                    }).Single();
        }

        public IList<SpotCheckAscertainmentVO> GetSpotCheckAscertainments(int spotCheckId)
        {
            return (from sca in this.unitOfWork.DbContext.Set<SpotCheckAscertainment>()
                    where sca.SpotCheckId == spotCheckId
                    orderby sca.SpotCheckAscertainmentId descending
                    select new SpotCheckAscertainmentVO
                    {
                        AscertainmentId = sca.SpotCheckAscertainmentId,
                        SpotCheckId = sca.SpotCheckId,
                        OrderNumber = sca.OrderNumber,
                        Status = sca.Status,
                        Type = sca.Type,
                        Ascertainment = sca.Ascertainment,
                    }).ToList();
        }

        public IList<SpotCheckDocVO> GetSpotCheckDocs(int spotCheckId)
        {
            return (from scf in this.unitOfWork.DbContext.Set<SpotCheckDoc>()
                    where scf.SpotCheckId == spotCheckId
                    orderby scf.SpotCheckDocId descending
                    select new SpotCheckDocVO
                    {
                        DocumentId = scf.SpotCheckDocId,
                        Description = scf.Description,
                        File = new FileVO
                        {
                            Key = scf.FileKey,
                            Name = scf.FileName,
                        },
                    }).ToList();
        }

        public IList<SpotCheckTargetVO> GetSpotCheckTargets(int spotCheckId)
        {
            return (from sct in this.unitOfWork.DbContext.Set<SpotCheckTarget>()
                    where sct.SpotCheckId == spotCheckId
                    orderby sct.SpotCheckTargetId descending
                    select new SpotCheckTargetVO
                    {
                        TargetId = sct.SpotCheckTargetId,
                        Type = sct.Type,
                        Name = sct.Name,
                    }).ToList();
        }

        public IList<SpotCheckRecommendationVO> GetSpotCheckRecommendations(int spotCheckId)
        {
            return (from scr in this.unitOfWork.DbContext.Set<SpotCheckRecommendation>()
                    where scr.SpotCheckId == spotCheckId
                    orderby scr.OrderNumber descending
                    select new SpotCheckRecommendationVO
                    {
                        RecommendationId = scr.SpotCheckRecommendationId,
                        OrderNumber = scr.OrderNumber,
                        Recommendation = scr.Recommendation,
                        Deadline = scr.Deadline,
                        ExecutionStatus = scr.ExecutionStatus,
                        StatusDate = scr.StatusDate,
                        ExecutionDate = scr.ExecutionDate,
                        ExecutionProofDate = scr.ExecutionProofDate,
                    }).ToList();
        }

        public IList<SpotCheckAscertainmentItemVO> GetNotIncludedAscertainments(int spotCheckId, int recommendationId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<SpotCheckRecommendationAscertainment>()
                           where item.SpotCheckRecommendationId == recommendationId
                           select item.SpotCheckAscertainmentId;

            return (from sca in this.unitOfWork.DbContext.Set<SpotCheckAscertainment>()
                    where !subquery.Contains(sca.SpotCheckAscertainmentId) && sca.SpotCheckId == spotCheckId
                    orderby sca.OrderNumber descending
                    select new SpotCheckAscertainmentItemVO
                    {
                        AscertainmentId = sca.SpotCheckAscertainmentId,
                        OrderNumber = sca.OrderNumber,
                        Type = sca.Type,
                        Ascertainment = sca.Ascertainment,
                        Status = sca.Status,
                        CheckSubjectComment = sca.CheckSubjectComment,
                        ManagingAuthorityComment = sca.ManagingAuthorityComment,
                    }).ToList();
        }

        public IList<SpotCheckAscertainmentItemVO> GetAscertainments(int spotCheckId, int recommendationId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckRecommendationAscertainment>()
                    join sca in this.unitOfWork.DbContext.Set<SpotCheckAscertainment>() on item.SpotCheckAscertainmentId equals sca.SpotCheckAscertainmentId
                    where sca.SpotCheckId == spotCheckId && item.SpotCheckRecommendationId == recommendationId
                    orderby sca.OrderNumber descending
                    select new SpotCheckAscertainmentItemVO
                    {
                        ItemId = item.SpotCheckRecommendationAscertainmentId,
                        AscertainmentId = sca.SpotCheckAscertainmentId,
                        OrderNumber = sca.OrderNumber,
                        Type = sca.Type,
                        Ascertainment = sca.Ascertainment,
                        Status = sca.Status,
                        CheckSubjectComment = sca.CheckSubjectComment,
                        ManagingAuthorityComment = sca.ManagingAuthorityComment,
                    }).ToList();
        }

        public IList<SpotCheckRecommendationItemVO> GetRecommendations(int spotCheckId, int ascertainmentId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckRecommendationAscertainment>()
                    join scr in this.unitOfWork.DbContext.Set<SpotCheckRecommendation>() on item.SpotCheckRecommendationId equals scr.SpotCheckRecommendationId
                    where scr.SpotCheckId == spotCheckId && item.SpotCheckAscertainmentId == ascertainmentId
                    orderby scr.OrderNumber descending
                    select new SpotCheckRecommendationItemVO
                    {
                        ItemId = item.SpotCheckRecommendationAscertainmentId,
                        RecommendationId = scr.SpotCheckRecommendationId,
                        OrderNumber = scr.OrderNumber,
                        Recommendation = scr.Recommendation,
                        Deadline = scr.Deadline,
                        ExecutionStatus = scr.ExecutionStatus,
                        StatusDate = scr.StatusDate,
                        ExecutionDate = scr.ExecutionDate,
                        ExecutionProofDate = scr.ExecutionProofDate,
                    }).ToList();
        }

        public IList<SpotCheckProgrammePriorityItemVO> GetProgrammePriorityItems(int spotCheckId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckItem>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on item.ProgrammePriorityId equals pp.MapNodeId
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    where item.SpotCheckId == spotCheckId
                    select new SpotCheckProgrammePriorityItemVO
                    {
                        SpotCheckItemId = item.SpotCheckItemId,
                        ItemId = item.ProgrammePriorityId.Value,
                        Code = pp.Code,
                        Name = pp.Name,
                        ProgrammeName = p.ShortName,
                    }).ToList();
        }

        public int[] GetProgrammePriorityIds(int spotCheckId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckItem>()
                    where item.SpotCheckId == spotCheckId
                    select item.ProgrammePriorityId.Value).ToArray();
        }

        public IList<SpotCheckProcedureItemVO> GetProcedureItems(int spotCheckId)
        {
            var proceduresData = (from item in this.unitOfWork.DbContext.Set<SpotCheckItem>()
                                  join proc in this.unitOfWork.DbContext.Set<Procedure>() on item.ProcedureId equals proc.ProcedureId
                                  join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                                  join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                  join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                                  join ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>() on proc.ProcedureId equals ptl.ProcedureId into g0
                                  from ptl in g0.DefaultIfEmpty()
                                  where item.SpotCheckId == spotCheckId
                                  select new
                                  {
                                      item.SpotCheckItemId,
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
                        pd.SpotCheckItemId,
                        pd.ProcedureId,
                        pd.Code,
                        pd.Name,
                        pd.ProcedureStatus,
                    }
                    into g
                    select new SpotCheckProcedureItemVO
                    {
                        SpotCheckItemId = g.Key.SpotCheckItemId,
                        ItemId = g.Key.ProcedureId,
                        Name = g.Key.Name,
                        Code = g.Key.Code,
                        Status = g.Key.ProcedureStatus,
                        EndingDate = g.Select(pd => pd.EndDate).Max(),
                        ProgrammeNames = g.Select(pd => pd.ProgrammeName).Distinct().ToList(),
                    })
                    .ToList();
        }

        public int[] GetProcedureIds(int spotCheckId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckItem>()
                    where item.SpotCheckId == spotCheckId
                    select item.ProcedureId.Value).ToArray();
        }

        public IList<SpotCheckContractItemVO> GetContractItems(int spotCheckId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckItem>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on item.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where item.SpotCheckId == spotCheckId
                    orderby c.CreateDate descending
                    select new
                    {
                        item.SpotCheckItemId,
                        c.ContractId,
                        ProcedureName = p.Name,
                        c.Name,
                        c.RegNumber,
                        c.ContractDate,
                        c.ExecutionStatus,
                        c.CompanyName,
                        c.CompanyUinType,
                        c.CompanyUin,
                    }).ToList()
                    .Select(o => new SpotCheckContractItemVO
                    {
                        SpotCheckItemId = o.SpotCheckItemId,
                        ItemId = o.ContractId,
                        ProcedureName = o.ProcedureName,
                        Name = o.Name,
                        RegNumber = o.RegNumber,
                        ContractDate = o.ContractDate,
                        ExecutionStatus = o.ExecutionStatus,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public int[] GetContractIds(int spotCheckId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckItem>()
                    where item.SpotCheckId == spotCheckId
                    select item.ContractId.Value).ToArray();
        }

        public IList<SpotCheckContractContractItemVO> GetContractContractItems(int spotCheckId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckItem>()
                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on item.ContractContractId equals cc.ContractContractId
                    join ccr in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals ccr.ContractContractorId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join country in this.unitOfWork.DbContext.Set<Country>() on ccr.SeatCountryId equals country.CountryId into g2
                    from country in g2.DefaultIfEmpty()

                    join set in this.unitOfWork.DbContext.Set<Settlement>() on ccr.SeatSettlementId equals set.SettlementId into g3
                    from set in g3.DefaultIfEmpty()

                    where item.SpotCheckId == spotCheckId
                    select new
                    {
                        item.SpotCheckItemId,
                        cc.ContractContractId,
                        cc.SignDate,
                        cc.Number,
                        ccr.Uin,
                        ccr.UinType,
                        ccr.Name,
                        ccr.SeatPostCode,
                        ccr.SeatStreet,
                        ccr.SeatAddress,
                        CountryName = country.Name,
                        CountryNutsCode = country.NutsCode,
                        SettlementName = set.Name,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNumber = c.RegNumber,
                        c.CompanyName,
                        c.CompanyUinType,
                        c.CompanyUin,
                    })
                .ToList()
                .Select(t => new SpotCheckContractContractItemVO()
                {
                    SpotCheckItemId = t.SpotCheckItemId,
                    ItemId = t.ContractContractId,
                    SignDate = t.SignDate,
                    Number = t.Number,
                    ContractContractorCompany = string.Format("{0} ({1}: {2})", t.Name, t.UinType.GetEnumDescription(), t.Uin),
                    Seat = t.CountryNutsCode == "BG" ? t.SettlementName + " " + t.SeatPostCode + " " + t.SeatStreet : t.CountryName + " " + t.SeatAddress,
                    ProcedureName = t.ProcedureName,
                    ContractName = t.ContractName,
                    ContractRegNumber = t.ContractRegNumber,
                    ContractCompany = string.Format("{0} ({1}: {2})", t.CompanyName, t.CompanyUinType.GetEnumDescription(), t.CompanyUin),
                }).ToList();
        }

        public int[] GetContractContractIds(int spotCheckId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckItem>()
                    where item.SpotCheckId == spotCheckId
                    select item.ContractContractId.Value).ToArray();
        }

        public bool IsSpotCheckNumUnique(int checkNum, int programmeId, int? spotCheckId)
        {
            if (spotCheckId.HasValue)
            {
                return !(from sc in this.unitOfWork.DbContext.Set<SpotCheck>()
                         where sc.CheckNum == checkNum && sc.ProgrammeId == programmeId && sc.SpotCheckId != spotCheckId.Value
                         select sc.SpotCheckId).Any();
            }
            else
            {
                return !(from sc in this.unitOfWork.DbContext.Set<SpotCheck>()
                         where sc.CheckNum == checkNum
                         select sc.SpotCheckId).Any();
            }
        }

        public bool HasDocuments(int spotCheckId)
        {
            return (from d in this.unitOfWork.DbContext.Set<SpotCheckDoc>()
                    where d.SpotCheckId == spotCheckId
                    select d.SpotCheckDocId).Any();
        }

        public new void Remove(SpotCheck spotCheck)
        {
            if (spotCheck.Status != SpotCheckStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete nondraft spot check");
            }

            base.Remove(spotCheck);
        }

        public IList<InternalЕnvironmentSpotCheckVO> GetInternalЕnvironmentSpotChecksForProjectDossier(int contractId)
        {
            var spotChecks = (
                from sc in this.unitOfWork.DbContext.Set<SpotCheck>()
                join pr in this.unitOfWork.DbContext.Set<Programme>() on sc.ProgrammeId equals pr.MapNodeId
                join sci in this.unitOfWork.DbContext.Set<SpotCheckItem>() on sc.SpotCheckId equals sci.SpotCheckId
                join c in this.unitOfWork.DbContext.Set<Contract>() on sci.ContractId equals c.ContractId
                where c.ContractId == contractId && sc.Status != SpotCheckStatus.Draft
                select new
                {
                    sc.SpotCheckId,
                    ProgrammeName = pr.Name,
                    ContractRegNum = c.RegNumber,
                    sc.RegNumber,
                    sc.Status,
                    sc.DateFrom,
                    sc.DateTo,
                    sc.Type,
                })
                .Distinct()
                .ToArray();

            var spotCheckIds = spotChecks.Select(sc => sc.SpotCheckId).ToArray();

            var ascertainments =
                (from sc in spotChecks
                 join sca in this.unitOfWork.DbContext.Set<SpotCheckAscertainment>() on sc.SpotCheckId equals sca.SpotCheckId
                 where spotCheckIds.Contains(sc.SpotCheckId)
                 select new
                 {
                     AscertainmentId = sca.SpotCheckAscertainmentId,
                     sca.SpotCheckId,
                     sca.OrderNumber,
                     sca.Ascertainment,
                 })
                 .Distinct()
                 .ToArray();

            var recommendations =
                (from sc in spotChecks
                 join scr in this.unitOfWork.DbContext.Set<SpotCheckRecommendation>() on sc.SpotCheckId equals scr.SpotCheckId
                 where spotCheckIds.Contains(sc.SpotCheckId)
                 select new
                 {
                     RecommendationId = scr.SpotCheckRecommendationId,
                     scr.SpotCheckId,
                     scr.OrderNumber,
                     scr.ExecutionStatus,
                     scr.Recommendation,
                 })
                 .Distinct()
                 .ToArray();

            return (from sc in spotChecks
                    select new InternalЕnvironmentSpotCheckVO
                    {
                        SpotCheckId = sc.SpotCheckId,
                        ProgrammeName = sc.ProgrammeName,
                        ContractRegNum = sc.ContractRegNum,
                        RegNumber = sc.RegNumber,
                        Status = sc.Status,
                        DateFrom = sc.DateFrom,
                        DateTo = sc.DateTo,
                        Type = sc.Type,
                        Ascertainments =
                            ascertainments.Where(i => i.SpotCheckId == sc.SpotCheckId)
                            .Select(i => new SpotCheckAscertainmentContentVO { Id = i.AscertainmentId, OrderNum = i.OrderNumber, AscertainmentContent = i.Ascertainment })
                            .Distinct()
                            .ToList(),
                        Recommendations =
                            recommendations.Where(i => i.SpotCheckId == sc.SpotCheckId)
                            .Select(i => new SpotCheckRecommendationContentVO { Id = i.RecommendationId, OrderNum = i.OrderNumber, RecommendationContent = i.Recommendation })
                            .Distinct()
                            .ToList(),
                        RecommendationExecutionStatuses =
                            recommendations.Where(i => i.SpotCheckId == sc.SpotCheckId)
                            .Select(i => new SpotCheckRecommendationExecutionStatusVO { Id = i.RecommendationId, ExecutionStatus = i.ExecutionStatus })
                            .Distinct()
                            .ToList(),
                    }).ToList();
        }
    }
}
