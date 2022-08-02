using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Audits.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain.Audits;
using Eumis.Domain.Companies;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Audits.Repositories
{
    internal class AuditsRepository : AggregateRepository<Audit>, IAuditsRepository
    {
        public AuditsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Audit, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Audit, object>>[]
                {
                    a => a.Documents,
                    a => a.Ascertainments.Select(aa => aa.Items),
                    a => a.LevelItems,
                    a => a.Projects,
                };
            }
        }

        public IList<AuditVO> GetAudits(int[] programmeIds, AuditLevel? level = null)
        {
            var predicate = PredicateBuilder.True<Audit>()
                .AndEquals(a => a.Level, level);

            var audits = (from a in this.unitOfWork.DbContext.Set<Audit>().Where(predicate)
                          join pr in this.unitOfWork.DbContext.Set<Programme>() on a.ProgrammeId equals pr.MapNodeId
                          where programmeIds.Contains(a.ProgrammeId)
                          orderby a.CreateDate descending
                          select new AuditVO
                          {
                              AuditId = a.AuditId,
                              ProgrammeName = pr.Name,
                              AuditInstitution = a.AuditInstitution,
                              AuditType = a.AuditType,
                              AuditKind = a.AuditKind,
                              Level = a.Level,
                          }).ToList();

            var auditIds = audits.Select(a => a.AuditId).ToArray();

            var programmePriorities =
                        (from a in audits
                         join item in this.unitOfWork.DbContext.Set<AuditLevelItem>() on a.AuditId equals item.AuditId
                         join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on item.ProgrammePriorityId equals pp.MapNodeId

                         where auditIds.Contains(a.AuditId)

                         select new
                         {
                             AuditId = item.AuditId,
                             Code = pp.Code,
                         })
                         .ToList();

            var procedures =
                        (from a in audits
                         join item in this.unitOfWork.DbContext.Set<AuditLevelItem>() on a.AuditId equals item.AuditId
                         join proc in this.unitOfWork.DbContext.Set<Procedure>() on item.ProcedureId equals proc.ProcedureId

                         where auditIds.Contains(a.AuditId)

                         select new
                         {
                             AuditId = item.AuditId,
                             Code = proc.Code,
                         })
                         .Distinct()
                         .ToList();

            var contracts =
                        (from a in audits
                         join item in this.unitOfWork.DbContext.Set<AuditLevelItem>() on a.AuditId equals item.AuditId
                         join c in this.unitOfWork.DbContext.Set<Contract>() on item.ContractId equals c.ContractId

                         where auditIds.Contains(a.AuditId)

                         select new
                         {
                             AuditId = item.AuditId,
                             Code = c.RegNumber,
                         })
                         .Distinct()
                         .ToList();

            var contractContracts =
                        (from a in audits
                         join item in this.unitOfWork.DbContext.Set<AuditLevelItem>() on a.AuditId equals item.AuditId
                         join cc in this.unitOfWork.DbContext.Set<ContractContract>() on item.ContractContractId equals cc.ContractContractId
                         join c in this.unitOfWork.DbContext.Set<Contract>() on cc.ContractId equals c.ContractId

                         where auditIds.Contains(a.AuditId)

                         select new
                         {
                             AuditId = item.AuditId,
                             Code = c.RegNumber,
                         })
                         .Distinct()
                         .ToList();

            var dataItems = programmePriorities.Union(procedures).Union(contracts).Union(contractContracts);

            var projects =
                        (from a in audits
                         join ap in this.unitOfWork.DbContext.Set<AuditProject>() on a.AuditId equals ap.AuditId
                         join p in this.unitOfWork.DbContext.Set<Project>() on ap.ProjectId equals p.ProjectId
                         where auditIds.Contains(a.AuditId)
                         select new
                         {
                             AuditId = a.AuditId,
                             ProjectCode = p.RegNumber,
                         })
                         .Distinct()
                         .ToList();

            var ascertainments =
                        (from a in audits
                         join aa in this.unitOfWork.DbContext.Set<AuditAscertainment>() on a.AuditId equals aa.AuditId
                         where auditIds.Contains(a.AuditId)
                         select new
                         {
                             AscertainmentId = aa.AuditAscertainmentId,
                             AuditId = a.AuditId,
                             OrderNum = aa.OrderNum,
                             RecommendationsFulfilledStatus = aa.RecommendationsFulfilled != null ? (((bool)aa.RecommendationsFulfilled) ? "изпълнена" : "неизпълнена") : null,
                             Ascertainment = aa.Ascertainment,
                             Recommendation = aa.Recommendation,
                         })
                         .Distinct()
                         .ToList();

            return (from a in audits
                    select new AuditVO
                    {
                        AuditId = a.AuditId,
                        ProgrammeName = a.ProgrammeName,
                        AuditInstitution = a.AuditInstitution,
                        AuditType = a.AuditType,
                        AuditKind = a.AuditKind,
                        Level = a.Level,
                        ItemCodes =
                                dataItems.Where(i => i.AuditId == a.AuditId)
                                .Select(i => i.Code)
                                .Distinct()
                                .ToList(),
                        ProjectCodes =
                                projects.Where(p => p.AuditId == a.AuditId)
                                .Select(p => p.ProjectCode)
                                .Distinct()
                                .ToList(),
                        Ascertainments =
                                ascertainments.Where(i => i.AuditId == a.AuditId)
                                .Select(x => new AuditAscertainmentContentVO() { Id = x.AscertainmentId, OrderNum = x.OrderNum, AscertainmentContent = x.Ascertainment, RecommendationContent = x.Recommendation })
                                .Distinct()
                                .ToList(),
                        RecommendationsFulfilledStatuses =
                                ascertainments.Where(i => i.AuditId == a.AuditId)
                                .Select(i => new AuditAscertainmentFulfilledStatusVO { Id = i.AscertainmentId, RecommendationsFulfilledStatus = i.RecommendationsFulfilledStatus })
                                .Distinct()
                                .ToList(),
                    }).ToList();
        }

        public int GetNextOrderNum(int auditId)
        {
            var lastOrderNumber = this.unitOfWork.DbContext.Set<AuditAscertainment>()
                .Where(t => t.AuditId == auditId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public AuditInfoVO GetInfo(int auditId)
        {
            return (from a in this.unitOfWork.DbContext.Set<Audit>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on a.ProgrammeId equals pr.MapNodeId
                    where a.AuditId == auditId
                    select new AuditInfoVO
                    {
                        Level = a.Level,
                        LevelDescr = a.Level,
                        ProgrammeCode = pr.Code,
                        Version = a.Version,
                    }).Single();
        }

        public AuditBasicDataVO GetBasicData(int auditId)
        {
            return (from a in this.unitOfWork.DbContext.Set<Audit>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on a.ProgrammeId equals p.MapNodeId
                    join c in this.unitOfWork.DbContext.Set<Company>() on p.CompanyId equals c.CompanyId into g1
                    from c in g1.DefaultIfEmpty()
                    where a.AuditId == auditId
                    select new AuditBasicDataVO
                    {
                        AuditId = a.AuditId,
                        Level = a.Level,
                        ContractId = a.ContractId,
                        ProgrammeCode = p.Code,
                        ProgrammeName = p.Name,
                        ProgrammeCompanyUinType = (UinType?)c.UinType,
                        ProgrammeCompanyUin = c.Uin,
                        ProgrammeCompanyName = c.Name,
                        Version = a.Version,
                    }).Single();
        }

        public IList<AuditAscertainmentVO> GetAscertainments(int auditId)
        {
            return (from aa in this.unitOfWork.DbContext.Set<AuditAscertainment>()
                    where aa.AuditId == auditId
                    orderby aa.OrderNum
                    select new AuditAscertainmentVO
                    {
                        AscertainmentId = aa.AuditAscertainmentId,
                        OrderNum = aa.OrderNum,
                        AuditId = aa.AuditId,
                        Ascertainment = aa.Ascertainment,
                        Recommendation = aa.Recommendation,
                        IsFinancial = aa.IsFinancial,
                        FinancialSum = aa.FinancialSum,
                    }).ToList();
        }

        public IList<AuditDocVO> GetDocuments(int auditId)
        {
            return (from ad in this.unitOfWork.DbContext.Set<AuditDoc>()
                    where ad.AuditId == auditId
                    orderby ad.AuditDocId descending
                    select new AuditDocVO
                    {
                        DocumentId = ad.AuditDocId,
                        Description = ad.Description,
                        File = new FileVO
                        {
                            Key = ad.FileKey,
                            Name = ad.FileName,
                        },
                    }).ToList();
        }

        public IList<AuditProgrammePriorityItemVO> GetProgrammePriorityItems(int auditId)
        {
            return (from item in this.unitOfWork.DbContext.Set<AuditLevelItem>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on item.ProgrammePriorityId equals pp.MapNodeId
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    where item.AuditId == auditId
                    select new AuditProgrammePriorityItemVO
                    {
                        AuditItemId = item.AuditLevelItemId,
                        ItemId = item.ProgrammePriorityId.Value,
                        Code = pp.Code,
                        Name = pp.Name,
                        ProgrammeName = p.ShortName,
                    }).ToList();
        }

        public IList<AuditProgrammePriorityItemVO> GetNotIncludedProgrammePriorities(int auditId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<AuditLevelItem>()
                           where item.AuditId == auditId
                           select item.ProgrammePriorityId;

            return (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    join a in this.unitOfWork.DbContext.Set<Audit>() on p.MapNodeId equals a.ProgrammeId
                    where !subquery.Contains(pp.MapNodeId) && a.AuditId == auditId
                    select new AuditProgrammePriorityItemVO
                    {
                        ItemId = pp.MapNodeId,
                        Code = pp.Code,
                        Name = pp.Name,
                        ProgrammeName = p.ShortName,
                    }).ToList();
        }

        public IList<AuditProcedureItemVO> GetProcedureItems(int auditId)
        {
            var proceduresData = (from item in this.unitOfWork.DbContext.Set<AuditLevelItem>()
                                  join proc in this.unitOfWork.DbContext.Set<Procedure>() on item.ProcedureId equals proc.ProcedureId
                                  join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                                  join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                  join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                                  join ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>() on proc.ProcedureId equals ptl.ProcedureId into g0
                                  from ptl in g0.DefaultIfEmpty()
                                  where item.AuditId == auditId
                                  select new
                                  {
                                      AuditLevelItemId = item.AuditLevelItemId,
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
                        pd.AuditLevelItemId,
                        pd.ProcedureId,
                        pd.Code,
                        pd.Name,
                        pd.ProcedureStatus,
                    }
                    into g
                    select new AuditProcedureItemVO
                    {
                        AuditItemId = g.Key.AuditLevelItemId,
                        ItemId = g.Key.ProcedureId,
                        Name = g.Key.Name,
                        Code = g.Key.Code,
                        Status = g.Key.ProcedureStatus,
                        EndingDate = g.Select(pd => pd.EndDate).Max(),
                        ProgrammeNames = g.Select(pd => pd.ProgrammeName).Distinct().ToList(),
                    })
                    .ToList();
        }

        public IList<AuditProcedureItemVO> GetNotIncludedProcedures(int auditId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<AuditLevelItem>()
                           where item.AuditId == auditId
                           select item.ProcedureId;

            var proceduresData = (from proc in this.unitOfWork.DbContext.Set<Procedure>()
                                  join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                                  join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                  join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                                  join a in this.unitOfWork.DbContext.Set<Audit>() on prog.MapNodeId equals a.ProgrammeId
                                  join ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>() on proc.ProcedureId equals ptl.ProcedureId into g0
                                  from ptl in g0.DefaultIfEmpty()
                                  where !subquery.Contains(proc.ProcedureId) && a.AuditId == auditId && Procedure.EvalSessionOrProjectCreationStatuses.Contains(proc.ProcedureStatus)
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
                    select new AuditProcedureItemVO
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

        public IList<AuditContractItemVO> GetContractItems(int auditId)
        {
            return (from item in this.unitOfWork.DbContext.Set<AuditLevelItem>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on item.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where item.AuditId == auditId
                    orderby c.CreateDate descending
                    select new
                    {
                        item.AuditLevelItemId,
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
                    .Select(o => new AuditContractItemVO
                    {
                        AuditItemId = o.AuditLevelItemId,
                        ItemId = o.ContractId,
                        ProcedureName = o.ProcedureName,
                        Name = o.Name,
                        RegNumber = o.RegNumber,
                        ContractDate = o.ContractDate,
                        ExecutionStatus = o.ExecutionStatus,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public IList<AuditContractItemVO> GetNotIncludedContracts(int auditId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<AuditLevelItem>()
                           where item.AuditId == auditId
                           select item.ContractId;

            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    join a in this.unitOfWork.DbContext.Set<Audit>() on c.ProgrammeId equals a.ProgrammeId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where !subquery.Contains(c.ContractId) && a.AuditId == auditId && c.ContractStatus == ContractStatus.Entered
                    orderby c.CreateDate descending
                    select new
                    {
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
                    .Select(o => new AuditContractItemVO
                    {
                        ItemId = o.ContractId,
                        ProcedureName = o.ProcedureName,
                        Name = o.Name,
                        RegNumber = o.RegNumber,
                        ContractDate = o.ContractDate,
                        ExecutionStatus = o.ExecutionStatus,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public IList<AuditContractContractItemVO> GetContractContractItems(int auditId)
        {
            return (from item in this.unitOfWork.DbContext.Set<AuditLevelItem>()
                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on item.ContractContractId equals cc.ContractContractId
                    join ccr in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals ccr.ContractContractorId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join country in this.unitOfWork.DbContext.Set<Country>() on ccr.SeatCountryId equals country.CountryId into g2
                    from country in g2.DefaultIfEmpty()

                    join set in this.unitOfWork.DbContext.Set<Settlement>() on ccr.SeatSettlementId equals set.SettlementId into g3
                    from set in g3.DefaultIfEmpty()

                    where item.AuditId == auditId
                    select new
                    {
                        item.AuditLevelItemId,
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
                .Select(t => new AuditContractContractItemVO
                {
                    AuditItemId = t.AuditLevelItemId,
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

        public IList<AuditContractContractItemVO> GetNotIncludedContractContracts(int auditId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<AuditLevelItem>()
                           where item.AuditId == auditId
                           select item.ContractContractId;

            return (from cc in this.unitOfWork.DbContext.Set<ContractContract>()
                    join ccr in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals ccr.ContractContractorId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cc.ContractId equals c.ContractId
                    join a in this.unitOfWork.DbContext.Set<Audit>() on c.ProgrammeId equals a.ProgrammeId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join country in this.unitOfWork.DbContext.Set<Country>() on ccr.SeatCountryId equals country.CountryId into g2
                    from country in g2.DefaultIfEmpty()

                    join set in this.unitOfWork.DbContext.Set<Settlement>() on ccr.SeatSettlementId equals set.SettlementId into g3
                    from set in g3.DefaultIfEmpty()

                    where !subquery.Contains(cc.ContractContractId) && a.AuditId == auditId
                    select new
                    {
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
                .Select(t => new AuditContractContractItemVO
                {
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

        public int GetProgrammeId(int auditId)
        {
            return (from a in this.unitOfWork.DbContext.Set<Audit>()
                    where a.AuditId == auditId
                    select a.ProgrammeId).Single();
        }

        public IList<InternalЕnvironmentAuditVO> GetInternalЕnvironmentAuditsForProjectDossier(int contractId)
        {
             var audits = (
                from a in this.unitOfWork.DbContext.Set<Audit>()
                join pr in this.unitOfWork.DbContext.Set<Programme>() on a.ProgrammeId equals pr.MapNodeId
                join ali in this.unitOfWork.DbContext.Set<AuditLevelItem>() on a.AuditId equals ali.AuditId
                join c in this.unitOfWork.DbContext.Set<Contract>() on ali.ContractId equals c.ContractId
                where c.ContractId == contractId
                select new
                {
                    a.AuditId,
                    ProgrammeName = pr.Name,
                    ContractRegNum = c.RegNumber,
                    a.AuditInstitution,
                    a.AuditType,
                    a.AuditKind,
                    a.Level,
                })
                .Distinct()
                .ToArray();

             var auditIds = audits.Select(a => a.AuditId).ToArray();

             var ascertainments = (
                from a in audits
                join aa in this.unitOfWork.DbContext.Set<AuditAscertainment>() on a.AuditId equals aa.AuditId
                where auditIds.Contains(a.AuditId)
                select new
                {
                    AscertainmentId = aa.AuditAscertainmentId,
                    aa.AuditId,
                    aa.OrderNum,
                    RecommendationsFulfilledStatus = aa.RecommendationsFulfilled != null ? (((bool)aa.RecommendationsFulfilled) ? "изпълнена" : "неизпълнена") : null,
                    IsFinancial = aa.IsFinancial ? "Да" : "Не",
                    aa.Ascertainment,
                    aa.Recommendation,
                })
                .Distinct()
                .ToArray();

             return (
                from a in audits
                select new InternalЕnvironmentAuditVO
                {
                    AuditId = a.AuditId,
                    ProgrammeName = a.ProgrammeName,
                    ContractRegNum = a.ContractRegNum,
                    AuditInstitution = a.AuditInstitution,
                    AuditType = a.AuditType,
                    AuditKind = a.AuditKind,
                    Level = a.Level,
                    Ascertainments =
                        ascertainments.Where(i => i.AuditId == a.AuditId)
                        .Select(x => new AuditAscertainmentContentVO() { Id = x.AscertainmentId, OrderNum = x.OrderNum, AscertainmentContent = x.Ascertainment, RecommendationContent = x.Recommendation })
                        .Distinct()
                        .ToList(),
                    RecommendationsFulfilledStatuses =
                        ascertainments.Where(i => i.AuditId == a.AuditId)
                        .Select(i => new AuditAscertainmentFulfilledStatusVO { Id = i.AscertainmentId, RecommendationsFulfilledStatus = i.RecommendationsFulfilledStatus, IsFinancial = i.IsFinancial })
                        .Distinct()
                        .ToList(),
                })
                .ToList();
        }

        public IList<AuditProjectVO> GetNotIncludedProjects(int auditId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<AuditProject>()
                           where item.AuditId == auditId
                           select item.ProjectId;

            return (from p in this.unitOfWork.DbContext.Set<Project>()
                    where !subquery.Contains(p.ProjectId)
                    select new AuditProjectVO()
                    {
                        ProjectId = p.ProjectId,
                        Code = p.RegNumber,
                        Name = p.Name,
                    }).ToList();
        }

        public int? GetContractId(int auditId)
        {
            return (from a in this.unitOfWork.DbContext.Set<Audit>()
                    where a.AuditId == auditId
                    select a.ContractId).Single();
        }
    }
}
