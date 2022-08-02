using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.CertAuthorityChecks.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.CertAuthorityChecks;
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

namespace Eumis.Data.CertAuthorityChecks.Repositories
{
    internal class CertAuthorityChecksRepository : AggregateRepository<CertAuthorityCheck>, ICertAuthorityChecksRepository
    {
        public CertAuthorityChecksRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<CertAuthorityCheck, object>>[] Includes
        {
            get
            {
                return new Expression<Func<CertAuthorityCheck, object>>[]
                {
                    sc => sc.LevelItems,
                    sc => sc.Ascertainments,
                    sc => sc.CertAuthorityCheckDocuments.Select(t => t.File),
                    sc => sc.Projects,
                };
            }
        }

        public IList<CertAuthorityCheckVO> GetCertAuthorityCheks(
            CertAuthorityCheckStatus? status = null,
            CertAuthorityCheckType? type = null)
        {
            var predicate = PredicateBuilder.True<CertAuthorityCheck>();

            predicate = predicate
                .AndEquals(c => c.Status, status)
                .AndEquals(c => c.Type, type);

            var certAuthorityChecks =
                        (from sc in this.unitOfWork.DbContext.Set<CertAuthorityCheck>().Where(predicate)
                         orderby new { sc.CreateDate } descending
                         select new CertAuthorityCheckVO
                         {
                             CertAuthorityCheckId = sc.CertAuthorityCheckId,
                             CheckNumber = sc.CheckNum,
                             StatusDescr = sc.Status,
                             Status = sc.Status,
                             Type = sc.Type,
                             SubjectType = sc.SubjectType,
                             SubjectName = sc.SubjectName,
                             DateFrom = sc.DateFrom,
                             DateTo = sc.DateTo,
                         })
                        .ToList();

            var certAuthorityCheckIds = certAuthorityChecks.Select(c => c.CertAuthorityCheckId).ToArray();

            var programmes =
                        (from cac in certAuthorityChecks
                         join item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>() on cac.CertAuthorityCheckId equals item.CertAuthorityCheckId
                         join p in this.unitOfWork.DbContext.Set<Programme>() on item.ProgrammeId equals p.MapNodeId

                         where certAuthorityCheckIds.Contains(cac.CertAuthorityCheckId)

                         select new
                         {
                             CertAuthorityCheckId = item.CertAuthorityCheckId,
                             ProgrammeId = p.MapNodeId,
                             ShortName = p.ShortName,
                             Code = p.Code,
                         })
                        .ToList();

            var programmePriorities =
                        (from cac in certAuthorityChecks
                         join item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>() on cac.CertAuthorityCheckId equals item.CertAuthorityCheckId
                         join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on item.ProgrammePriorityId equals pp.MapNodeId
                         join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                         join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId

                         where certAuthorityCheckIds.Contains(cac.CertAuthorityCheckId)

                         select new
                         {
                             CertAuthorityCheckId = item.CertAuthorityCheckId,
                             ProgrammeId = p.MapNodeId,
                             ShortName = p.ShortName,
                             Code = pp.Code,
                         })
                        .ToList();

            var procedures =
                        (from cac in certAuthorityChecks
                         join item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>() on cac.CertAuthorityCheckId equals item.CertAuthorityCheckId
                         join proc in this.unitOfWork.DbContext.Set<Procedure>() on item.ProcedureId equals proc.ProcedureId
                         join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                         join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                         join p in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals p.MapNodeId
                         where certAuthorityCheckIds.Contains(cac.CertAuthorityCheckId)
                         select new
                         {
                             CertAuthorityCheckId = item.CertAuthorityCheckId,
                             ProgrammeId = p.MapNodeId,
                             ShortName = p.ShortName,
                             Code = proc.Code,
                         }).Distinct().ToList();

            var contracts =
                        (from cac in certAuthorityChecks
                         join item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>() on cac.CertAuthorityCheckId equals item.CertAuthorityCheckId
                         join c in this.unitOfWork.DbContext.Set<Contract>() on item.ContractId equals c.ContractId
                         join proc in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals proc.ProcedureId
                         join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                         join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                         join p in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals p.MapNodeId
                         where certAuthorityCheckIds.Contains(cac.CertAuthorityCheckId)
                         select new
                         {
                             CertAuthorityCheckId = item.CertAuthorityCheckId,
                             ProgrammeId = p.MapNodeId,
                             ShortName = p.ShortName,
                             Code = c.RegNumber,
                         }).Distinct().ToList();

            var dataItems = programmes.Union(programmePriorities).Union(procedures).Union(contracts);

            var projects =
                        (from cac in certAuthorityChecks
                         join cacp in this.unitOfWork.DbContext.Set<CertAuthorityCheckProject>() on cac.CertAuthorityCheckId equals cacp.CertAuthorityCheckId
                         join p in this.unitOfWork.DbContext.Set<Project>() on cacp.ProjectId equals p.ProjectId
                         where certAuthorityCheckIds.Contains(cac.CertAuthorityCheckId)
                         select new
                         {
                             CertAuthorityCheckId = cac.CertAuthorityCheckId,
                             ProjectName = p.RegNumber,
                         }).Distinct().ToList();

            var ascertainments =
                        (from cac in certAuthorityChecks
                         join a in this.unitOfWork.DbContext.Set<CertAuthorityCheckAscertainment>() on cac.CertAuthorityCheckId equals a.CertAuthorityCheckId
                         where certAuthorityCheckIds.Contains(cac.CertAuthorityCheckId)
                         select new
                         {
                             CertAuthorityCheckId = cac.CertAuthorityCheckId,
                             CertAuthorityCheckAscertainmentId = a.CertAuthorityCheckAscertainmentId,
                             OrderNum = a.OrderNum,
                             RecommendationExecutionStatus = a.RecommendationExecutionStatus,
                             Ascertainment = a.Ascertainment,
                             Recommendation = a.Recommendation,
                         }).Distinct().ToList();

            return (from cac in certAuthorityChecks
                    select new CertAuthorityCheckVO
                    {
                        CertAuthorityCheckId = cac.CertAuthorityCheckId,
                        CheckNumber = cac.CheckNumber,
                        StatusDescr = cac.Status,
                        Status = cac.Status,
                        Type = cac.Type,
                        SubjectType = cac.SubjectType,
                        SubjectName = cac.SubjectName,
                        DateFrom = cac.DateFrom,
                        DateTo = cac.DateTo,
                        ProgrammeShortNames = dataItems.Where(i => i.CertAuthorityCheckId == cac.CertAuthorityCheckId).Select(i => i.ShortName).Distinct().ToList(),
                        ItemCodes = dataItems.Where(i => i.CertAuthorityCheckId == cac.CertAuthorityCheckId).Select(i => i.Code).Distinct().ToList(),
                        ProjectCodes = projects.Where(p => p.CertAuthorityCheckId == cac.CertAuthorityCheckId).Select(p => p.ProjectName).Distinct().ToList(),
                        Ascertainments = ascertainments.Where(asc => asc.CertAuthorityCheckId == cac.CertAuthorityCheckId).Select(x => new CertAuthorityCheckAscertainmentContentVO() { Ascertainment = x.Ascertainment, Recommendation = x.Recommendation, Id = x.CertAuthorityCheckAscertainmentId, OrderNum = x.OrderNum }).ToList(),
                        RecommendationExecutionStatuses = ascertainments.Where(asc => asc.CertAuthorityCheckId == cac.CertAuthorityCheckId).Select(asc => new CertAuthorityCheckAscertainmentExecutionStatusVO { AscertainmentId = asc.CertAuthorityCheckAscertainmentId, RecommendationExecutionStatus = asc.RecommendationExecutionStatus }).Distinct().ToList(),
                    }).ToList();
        }

        public int GetNextOrderNum(int certAuthorityCheckId)
        {
            var lastOrderNumber = this.unitOfWork.DbContext.Set<CertAuthorityCheckAscertainment>()
                .Where(t => t.CertAuthorityCheckId == certAuthorityCheckId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public CertAuthorityCheckInfoVO GetInfo(int certAuthorityCheckId)
        {
            return (from sc in this.unitOfWork.DbContext.Set<CertAuthorityCheck>()
                    where sc.CertAuthorityCheckId == certAuthorityCheckId
                    select new CertAuthorityCheckInfoVO
                    {
                        Level = sc.Level,
                        Status = sc.Status,
                        StatusDescr = sc.Status,
                        Version = sc.Version,
                    }).Single();
        }

        public IList<CertAuthorityCheckAscertainmentVO> GetCertAuthorityCheckAscertainments(int certAuthorityCheckId)
        {
            return (from ca in this.unitOfWork.DbContext.Set<CertAuthorityCheckAscertainment>()
                    where ca.CertAuthorityCheckId == certAuthorityCheckId
                    orderby ca.OrderNum
                    select new CertAuthorityCheckAscertainmentVO
                    {
                        OrderNum = ca.OrderNum,
                        AscertainmentId = ca.CertAuthorityCheckAscertainmentId,
                        CertAuthorityCheckId = ca.CertAuthorityCheckId,
                        Status = ca.Status,
                        Type = ca.Type,
                        RecommendationExecutionStatus = ca.RecommendationExecutionStatus,
                    }).ToList();
        }

        public IList<CertAuthorityCheckProgrammeItemVO> GetProgrammeItems(int certAuthorityCheckId)
        {
            return (from item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on item.ProgrammeId equals p.MapNodeId

                    join c in this.unitOfWork.DbContext.Set<Company>() on p.CompanyId equals c.CompanyId into g1
                    from c in g1.DefaultIfEmpty()

                    where item.CertAuthorityCheckId == certAuthorityCheckId
                    select new
                    {
                        item.CertAuthorityCheckLevelItemId,
                        item.ProgrammeId,
                        p.Code,
                        p.ShortName,
                        CompanyUinType = (UinType?)c.UinType,
                        CompanyName = c.Name,
                        CompanyUin = c.Uin,
                    })
                .ToList()
                .Select(o => new CertAuthorityCheckProgrammeItemVO()
                {
                    CertAuthorityCheckItemId = o.CertAuthorityCheckLevelItemId,
                    ItemId = o.ProgrammeId.Value,
                    Code = o.Code,
                    ShortName = o.ShortName,
                    Company = o.CompanyUinType.HasValue ?
                        string.Format("{0} {1} {2}", EnumUtils.GetEnumDescription(o.CompanyUinType), o.CompanyUin, o.CompanyName) :
                        null,
                })
                .ToList();
        }

        public IList<CertAuthorityCheckProgrammeItemVO> GetNotIncludedProgrammes(int certAuthorityCheckId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>()
                           where item.CertAuthorityCheckId == certAuthorityCheckId
                           select item.ProgrammeId;

            return (from p in this.unitOfWork.DbContext.Set<Programme>()

                    join c in this.unitOfWork.DbContext.Set<Company>() on p.CompanyId equals c.CompanyId into g1
                    from c in g1.DefaultIfEmpty()

                    where !subquery.Contains(p.MapNodeId)
                    select new
                    {
                        p.MapNodeId,
                        p.Code,
                        p.ShortName,
                        CompanyUinType = (UinType?)c.UinType,
                        CompanyName = c.Name,
                        CompanyUin = c.Uin,
                    })
                .ToList()
                .Select(o => new CertAuthorityCheckProgrammeItemVO()
                {
                    ItemId = o.MapNodeId,
                    Code = o.Code,
                    ShortName = o.ShortName,
                    Company = o.CompanyUinType.HasValue ?
                        string.Format("{0} {1} {2}", EnumUtils.GetEnumDescription(o.CompanyUinType), o.CompanyUin, o.CompanyName) :
                        null,
                })
                .ToList();
        }

        public IList<CertAuthorityCheckProgrammePriorityItemVO> GetProgrammePriorityItems(int certAuthorityCheckId)
        {
            return (from item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on item.ProgrammePriorityId equals pp.MapNodeId
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    where item.CertAuthorityCheckId == certAuthorityCheckId
                    select new CertAuthorityCheckProgrammePriorityItemVO
                    {
                        CertAuthorityCheckItemId = item.CertAuthorityCheckLevelItemId,
                        ItemId = item.ProgrammePriorityId.Value,
                        Code = pp.Code,
                        Name = pp.Name,
                        ProgrammeName = p.ShortName,
                    }).ToList();
        }

        public IList<CertAuthorityCheckProgrammePriorityItemVO> GetNotIncludedProgrammePriorities(int certAuthorityCheckId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>()
                           where item.CertAuthorityCheckId == certAuthorityCheckId
                           select item.ProgrammePriorityId;

            return (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    where !subquery.Contains(pp.MapNodeId)
                    select new CertAuthorityCheckProgrammePriorityItemVO()
                    {
                        ItemId = pp.MapNodeId,
                        Code = pp.Code,
                        Name = pp.Name,
                        ProgrammeName = p.ShortName,
                    }).ToList();
        }

        public IList<CertAuthorityCheckProcedureItemVO> GetProcedureItems(int certAuthorityCheckId)
        {
            var proceduresData = (from item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>()
                                  join proc in this.unitOfWork.DbContext.Set<Procedure>() on item.ProcedureId equals proc.ProcedureId
                                  join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                                  join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                  join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                                  join ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>() on proc.ProcedureId equals ptl.ProcedureId into g0
                                  from ptl in g0.DefaultIfEmpty()
                                  where item.CertAuthorityCheckId == certAuthorityCheckId
                                  select new
                                  {
                                      CertAuthorityCheckLevelItemId = item.CertAuthorityCheckLevelItemId,
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
                        pd.CertAuthorityCheckLevelItemId,
                        pd.ProcedureId,
                        pd.Code,
                        pd.Name,
                        pd.ProcedureStatus,
                    }
                    into g
                    select new CertAuthorityCheckProcedureItemVO
                    {
                        CertAuthorityCheckItemId = g.Key.CertAuthorityCheckLevelItemId,
                        ItemId = g.Key.ProcedureId,
                        Name = g.Key.Name,
                        Code = g.Key.Code,
                        Status = g.Key.ProcedureStatus,
                        EndingDate = g.Select(pd => pd.EndDate).Max(),
                        ProgrammeNames = g.Select(pd => pd.ProgrammeName).Distinct().ToList(),
                    })
                    .ToList();
        }

        public IList<CertAuthorityCheckProcedureItemVO> GetNotIncludedProcedures(int certAuthorityCheckId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>()
                           where item.CertAuthorityCheckId == certAuthorityCheckId
                           select item.ProcedureId;

            var proceduresData = (from proc in this.unitOfWork.DbContext.Set<Procedure>()
                                  join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                                  join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                  join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                                  join ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>() on proc.ProcedureId equals ptl.ProcedureId into g0
                                  from ptl in g0.DefaultIfEmpty()
                                  where !subquery.Contains(proc.ProcedureId) && Procedure.EvalSessionOrProjectCreationStatuses.Contains(proc.ProcedureStatus)
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
                    select new CertAuthorityCheckProcedureItemVO
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

        public IList<CertAuthorityCheckContractItemVO> GetContractItems(int certAuthorityCheckId)
        {
            return (from item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on item.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where item.CertAuthorityCheckId == certAuthorityCheckId
                    orderby c.CreateDate descending
                    select new
                    {
                        item.CertAuthorityCheckLevelItemId,
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
                    .Select(o => new CertAuthorityCheckContractItemVO
                    {
                        CertAuthorityCheckItemId = o.CertAuthorityCheckLevelItemId,
                        ItemId = o.ContractId,
                        ProcedureName = o.ProcedureName,
                        Name = o.Name,
                        RegNumber = o.RegNumber,
                        ContractDate = o.ContractDate,
                        ExecutionStatus = o.ExecutionStatus,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public IList<CertAuthorityCheckContractItemVO> GetNotIncludedContracts(int certAuthorityCheckId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<CertAuthorityCheckLevelItem>()
                           where item.CertAuthorityCheckId == certAuthorityCheckId
                           select item.ContractId;

            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where !subquery.Contains(c.ContractId) && c.ContractStatus == ContractStatus.Entered
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
                    .Select(o => new CertAuthorityCheckContractItemVO
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

        public new void Remove(CertAuthorityCheck certAuthorityCheck)
        {
            if (certAuthorityCheck.IsActivated || certAuthorityCheck.Status != CertAuthorityCheckStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete certAuthority check  which is in draft status or is activated");
            }

            base.Remove(certAuthorityCheck);
        }

        public IList<CertAuthorityCheckDocumentVO> GetCertAuthorityCheckDocuments(int certAuthorityCheckId)
        {
            return (from crd in this.unitOfWork.DbContext.Set<CertAuthorityCheckDocument>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on crd.BlobKey equals b.Key into g1
                    from b in g1.DefaultIfEmpty()
                    where crd.CertAuthorityCheckId == certAuthorityCheckId
                    select new { crd, b })
                    .Select(p => new CertAuthorityCheckDocumentVO
                    {
                        CertAuthorityCheckId = p.crd.CertAuthorityCheckId,
                        CertAuthorityCheckDocumentId = p.crd.CertAuthorityCheckDocumentId,
                        Name = p.crd.Name,
                        Description = p.crd.Description,
                        File = (p.b.Key == null) ? null : new FileVO
                        {
                            Key = p.b.Key,
                            Name = p.b.FileName,
                        },
                    })
                   .ToList();
        }

        public IList<CertAuthorityCheckProjectVO> GetNotIncludedProjects(int certAuthorityCheckId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<CertAuthorityCheckProject>()
                           where item.CertAuthorityCheckId == certAuthorityCheckId
                           select item.ProjectId;

            return (from p in this.unitOfWork.DbContext.Set<Project>()
                    where !subquery.Contains(p.ProjectId)
                    select new CertAuthorityCheckProjectVO()
                    {
                        ProjectId = p.ProjectId,
                        Code = p.RegNumber,
                        Name = p.Name,
                    }).ToList();
        }

        public IList<CertAuthorityCheckProjectVO> GetCertAuthorityCheckProjects(int certAuthorityCheckId)
        {
            return (from cacp in this.unitOfWork.DbContext.Set<CertAuthorityCheckProject>()
                    join p in this.unitOfWork.DbContext.Set<Project>() on cacp.ProjectId equals p.ProjectId into g1
                    from p in g1.DefaultIfEmpty()
                    where cacp.CertAuthorityCheckId == certAuthorityCheckId
                    select new CertAuthorityCheckProjectVO
                    {
                        CertAuthorityCheckId = cacp.CertAuthorityCheckId,
                        ProjectId = cacp.ProjectId,
                        Code = p.RegNumber,
                        Name = p.Name,
                    }).ToList();
        }
    }
}
