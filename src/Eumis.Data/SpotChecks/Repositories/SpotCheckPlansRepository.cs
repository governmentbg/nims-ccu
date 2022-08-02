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
    internal class SpotCheckPlansRepository : AggregateRepository<SpotCheckPlan>, ISpotCheckPlansRepository
    {
        public SpotCheckPlansRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<SpotCheckPlan, object>>[] Includes
        {
            get
            {
                return new Expression<Func<SpotCheckPlan, object>>[]
                {
                    scp => scp.Targets,
                    scp => scp.Items,
                    scp => scp.Documents,
                };
            }
        }

        public IList<SpotCheckPlanVO> GetSpotCheckPlans(
            int[] programmeIds,
            int userId,
            Year? year = null,
            Month? month = null)
        {
            var basePredicate = PredicateBuilder.True<SpotCheckPlan>()
                .AndEquals(scp => scp.Year, year)
                .AndEquals(scp => scp.Month, month);

            var externalVerificatorSpotCheckPlans = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                    join sp in this.unitOfWork.DbContext.Set<SpotCheckPlan>().Where(basePredicate) on cu.ContractId equals sp.ContractId
                                                    select sp;

            var planPredicate = basePredicate
                .And(s => programmeIds.Contains(s.ProgrammeId));

            return (from scp in this.unitOfWork.DbContext.Set<SpotCheckPlan>().Where(planPredicate).Union(externalVerificatorSpotCheckPlans)
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on scp.ProgrammeId equals pr.MapNodeId
                    orderby new { scp.Year, scp.Month } descending
                    select new SpotCheckPlanVO
                    {
                        SpotCheckPlanId = scp.SpotCheckPlanId,
                        ProgrammeName = pr.Name,
                        Level = scp.Level,
                        Year = scp.Year,
                        Month = scp.Month,
                    })
                    .Distinct()
                    .ToList();
        }

        public SpotCheckPlanInfoVO GetInfo(int spotCheckPlanId)
        {
            return (from scp in this.unitOfWork.DbContext.Set<SpotCheckPlan>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on scp.ProgrammeId equals pr.MapNodeId
                    where scp.SpotCheckPlanId == spotCheckPlanId
                    select new SpotCheckPlanInfoVO
                    {
                        ProgrammeCode = pr.Code,
                        Month = scp.Month,
                        Year = scp.Year,
                        Level = scp.Level,
                        Version = scp.Version,
                    }).Single();
        }

        public SpotCheckPlanBasicDataVO GetBasicData(int spotCheckPlanId)
        {
            return (from scp in this.unitOfWork.DbContext.Set<SpotCheckPlan>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on scp.ProgrammeId equals p.MapNodeId
                    join c in this.unitOfWork.DbContext.Set<Company>() on p.CompanyId equals c.CompanyId into g1
                    from c in g1.DefaultIfEmpty()
                    where scp.SpotCheckPlanId == spotCheckPlanId
                    select new SpotCheckPlanBasicDataVO
                    {
                        SpotCheckPlanId = scp.SpotCheckPlanId,
                        Year = scp.Year,
                        Month = scp.Month,
                        Level = scp.Level,
                        ContractId = scp.ContractId,
                        ProgrammeCode = p.Code,
                        ProgrammeName = p.Name,
                        ProgrammeCompanyUinType = (UinType?)c.UinType,
                        ProgrammeCompanyUin = c.Uin,
                        ProgrammeCompanyName = c.Name,
                        Version = scp.Version,
                    }).Single();
        }

        public IList<SpotCheckDocVO> GetSpotCheckPlanDocs(int spotCheckPlanId)
        {
            return (from scpf in this.unitOfWork.DbContext.Set<SpotCheckPlanDoc>()
                    where scpf.SpotCheckPlanId == spotCheckPlanId
                    orderby scpf.SpotCheckPlanDocId descending
                    select new SpotCheckDocVO
                    {
                        DocumentId = scpf.SpotCheckPlanDocId,
                        Description = scpf.Description,
                        File = new FileVO
                        {
                            Key = scpf.FileKey,
                            Name = scpf.FileName,
                        },
                    }).ToList();
        }

        public IList<SpotCheckTargetVO> GetSpotCheckPlanTargets(int spotCheckPlanId)
        {
            return (from scpt in this.unitOfWork.DbContext.Set<SpotCheckPlanTarget>()
                    where scpt.SpotCheckPlanId == spotCheckPlanId
                    orderby scpt.SpotCheckPlanTargetId descending
                    select new SpotCheckTargetVO
                    {
                        TargetId = scpt.SpotCheckPlanTargetId,
                        Type = scpt.Type,
                        Name = scpt.Name,
                    }).ToList();
        }

        public IList<SpotCheckProgrammePriorityItemVO> GetProgrammePriorityItems(int spotCheckPlanId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckPlanItem>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on item.ProgrammePriorityId equals pp.MapNodeId
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    where item.SpotCheckPlanId == spotCheckPlanId
                    select new SpotCheckProgrammePriorityItemVO
                    {
                        SpotCheckItemId = item.SpotCheckPlanItemId,
                        ItemId = item.ProgrammePriorityId.Value,
                        Code = pp.Code,
                        Name = pp.Name,
                        ProgrammeName = p.ShortName,
                    }).ToList();
        }

        public int[] GetProgrammePriorityIds(int spotCheckPlanId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckPlanItem>()
                    where item.SpotCheckPlanId == spotCheckPlanId
                    select item.ProgrammePriorityId.Value).ToArray();
        }

        public IList<SpotCheckProcedureItemVO> GetProcedureItems(int spotCheckPlanId)
        {
            var proceduresData = (from item in this.unitOfWork.DbContext.Set<SpotCheckPlanItem>()
                                  join proc in this.unitOfWork.DbContext.Set<Procedure>() on item.ProcedureId equals proc.ProcedureId
                                  join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                                  join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                  join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                                  join ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>() on proc.ProcedureId equals ptl.ProcedureId into g0
                                  from ptl in g0.DefaultIfEmpty()
                                  where item.SpotCheckPlanId == spotCheckPlanId
                                  select new
                                  {
                                      item.SpotCheckPlanItemId,
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
                        pd.SpotCheckPlanItemId,
                        pd.ProcedureId,
                        pd.Code,
                        pd.Name,
                        pd.ProcedureStatus,
                    }
                    into g
                    select new SpotCheckProcedureItemVO
                    {
                        SpotCheckItemId = g.Key.SpotCheckPlanItemId,
                        ItemId = g.Key.ProcedureId,
                        Name = g.Key.Name,
                        Code = g.Key.Code,
                        Status = g.Key.ProcedureStatus,
                        EndingDate = g.Select(pd => pd.EndDate).Max(),
                        ProgrammeNames = g.Select(pd => pd.ProgrammeName).Distinct().ToList(),
                    })
                    .ToList();
        }

        public int[] GetProcedureIds(int spotCheckPlanId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckPlanItem>()
                    where item.SpotCheckPlanId == spotCheckPlanId
                    select item.ProcedureId.Value).ToArray();
        }

        public IList<SpotCheckContractItemVO> GetContractItems(int spotCheckPlanId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckPlanItem>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on item.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where item.SpotCheckPlanId == spotCheckPlanId
                    orderby c.CreateDate descending
                    select new
                    {
                        item.SpotCheckPlanItemId,
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
                        SpotCheckItemId = o.SpotCheckPlanItemId,
                        ItemId = o.ContractId,
                        ProcedureName = o.ProcedureName,
                        Name = o.Name,
                        RegNumber = o.RegNumber,
                        ContractDate = o.ContractDate,
                        ExecutionStatus = o.ExecutionStatus,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public int[] GetContractIds(int spotCheckPlanId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckPlanItem>()
                    where item.SpotCheckPlanId == spotCheckPlanId
                    select item.ContractId.Value).ToArray();
        }

        public IList<SpotCheckContractContractItemVO> GetContractContractItems(int spotCheckPlanId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckPlanItem>()
                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on item.ContractContractId equals cc.ContractContractId
                    join ccr in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals ccr.ContractContractorId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join country in this.unitOfWork.DbContext.Set<Country>() on ccr.SeatCountryId equals country.CountryId into g2
                    from country in g2.DefaultIfEmpty()

                    join set in this.unitOfWork.DbContext.Set<Settlement>() on ccr.SeatSettlementId equals set.SettlementId into g3
                    from set in g3.DefaultIfEmpty()

                    where item.SpotCheckPlanId == spotCheckPlanId
                    select new
                    {
                        item.SpotCheckPlanItemId,
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
                    SpotCheckItemId = t.SpotCheckPlanItemId,
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

        public int[] GetContractContractIds(int spotCheckPlanId)
        {
            return (from item in this.unitOfWork.DbContext.Set<SpotCheckPlanItem>()
                    where item.SpotCheckPlanId == spotCheckPlanId
                    select item.ContractContractId.Value).ToArray();
        }

        public bool IsCheckPlanUnique(int programmeId, Year year, Month month)
        {
            return !(from scp in this.unitOfWork.DbContext.Set<SpotCheckPlan>()
                     where scp.ProgrammeId == programmeId && scp.Year == year && scp.Month == month
                     select scp.SpotCheckPlanId).Any();
        }

        public bool HasAssociatedCheck(int spotCheckPlanId)
        {
            return (from sc in this.unitOfWork.DbContext.Set<SpotCheck>()
                    where sc.SpotCheckPlanId == spotCheckPlanId
                    select sc.SpotCheckId).Any();
        }

        public IList<string> CanDeletePlan(int spotCheckPlanId)
        {
            var errors = new List<string>();

            if (this.HasAssociatedCheck(spotCheckPlanId))
            {
                errors.Add("Не може да се изтрие план, към който има асоциирана проверка на място.");
            }

            return errors;
        }

        public new void Remove(SpotCheckPlan plan)
        {
            if (this.CanDeletePlan(plan.SpotCheckPlanId).Any())
            {
                throw new DomainValidationException("Cannot delete SpotCheckPlan");
            }

            base.Remove(plan);
        }

        public int GetProgrammeId(int spotCheckPlanId)
        {
            return (from scp in this.unitOfWork.DbContext.Set<SpotCheckPlan>()
                    where scp.SpotCheckPlanId == spotCheckPlanId
                    select scp.ProgrammeId).Single();
        }

        public int? GetContractId(int spotCheckPlanId)
        {
            return (from scp in this.unitOfWork.DbContext.Set<SpotCheckPlan>()
                    where scp.SpotCheckPlanId == spotCheckPlanId
                    select scp.ContractId).Single();
        }
    }
}
