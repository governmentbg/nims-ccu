using Autofac.Extras.Attributed;
using Eumis.Public.Data.Core;
using Eumis.Public.Data.Linq;
using Eumis.Public.Data.ProgrammeGroups.ViewObjects;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.EuReimbursedAmounts;
using Eumis.Public.Domain.Entities.Umis.EvalSessions;
using Eumis.Public.Domain.Entities.Umis.HistoricContracts;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammeGroups;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Domain.Entities.Umis.Procedures;
using Eumis.Public.Domain.Entities.Umis.Projects;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Data.ProgrammeGroups.Repositories
{
    internal class ProgrammeGroupsRepository : Repository, IProgrammeGroupsRepository
    {
        private UnitOfWork mainUow;

        public static readonly Reimbursement[] ReportsReimbursements = new Reimbursement[]
        {
                Reimbursement.Bank,
                Reimbursement.WrittenОff,
                Reimbursement.DistributedLimitDeduction,
        };

        public ProgrammeGroupsRepository([WithKey(DbKey.Umis)]IUnitOfWork uow, [WithKey(DbKey.Main)]IUnitOfWork mainUow)
            : base(uow)
        {
            this.mainUow = (UnitOfWork)mainUow;
        }

        public IEnumerable<ProgrammeGroup> GetAllProgrammeGroups()
        {
            return (from p in this.unitOfWork.DbContext.Set<Programme>()
                    join pg in this.unitOfWork.DbContext.Set<ProgrammeGroup>() on p.ProgrammeGroupId equals pg.ProgrammeGroupId
                    select pg)
             .Distinct();
        }

        public ProgrammeGroup GetProgrammeGroup(int programmeGroupId)
        {
            return (from g in this.unitOfWork.DbContext.Set<ProgrammeGroup>().Where(g => g.ProgrammeGroupId == programmeGroupId)
                    select g)
                    .Single();
        }

        public IList<ProgrammeBudgetDetailedVO> GetProgrammeBudgetDetailed(bool getProgrammeGroups, int? programmeGroupId = null)
        {
            var programmePredicate = PredicateBuilder.True<Programme>();

            if (getProgrammeGroups == false)
            {
                programmePredicate = programmePredicate.And(p => p.ProgrammeGroupId == null);
            }
            else if (programmeGroupId != null)
            {
                programmePredicate = programmePredicate.AndEquals(p => p.ProgrammeGroupId, programmeGroupId);
            }

            var budgets =
                (from p in this.unitOfWork.DbContext.Set<Programme>().Where(programmePredicate)
                 join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on p.MapNodeId equals mnr.ProgrammeId
                 join mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>() on mnr.MapNodeId equals mnb.MapNodeId into j1
                 from b in j1.DefaultIfEmpty()

                 group new
                 {
                     EuAmount = (decimal?)b.EuAmount,
                     EuReservedAmount = (decimal?)b.EuReservedAmount,
                     BgAmount = (decimal?)b.BgAmount,
                     BgReservedAmount = (decimal?)b.BgReservedAmount,
                 }
                 by new
                 {
                     ProgrammeShortName = p.ShortName,
                     ProgrammeShortNameAlt = p.PortalShortNameAlt,
                     Code = p.Code,
                     PortalOrderNum = p.PortalOrderNum,
                     ProgrammeId = (int?)p.MapNodeId,
                     ProgrammeGroupId = (int?)p.ProgrammeGroupId,
                 }
                 into g
                 select new
                 {
                     ProgrammeShortName = g.Key.ProgrammeShortName,
                     ProgrammeShortNameAlt = g.Key.ProgrammeShortNameAlt,
                     ProgrammeId = g.Key.ProgrammeId,
                     ProgrammeGroupId = g.Key.ProgrammeGroupId,
                     Code = g.Key.Code,
                     PortalOrderNum = g.Key.PortalOrderNum,
                     EuAmount = g.Sum(i => i.EuAmount),
                     EuReservedAmount = g.Sum(i => i.EuReservedAmount),
                     BgAmount = g.Sum(i => i.BgAmount),
                     BgReservedAmount = g.Sum(i => i.BgReservedAmount),
                 })
                    .OrderBy(e => e.PortalOrderNum)
                    .ToList();

            var filteredAmounts =
                from cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(cba1 => cba1.IsActive)
                join c in this.unitOfWork.DbContext.Set<Contract>() on cba.ContractId equals c.ContractId
                select cba;

            var contracted =
                (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(p => p.IsPrimary)
                 join p in this.unitOfWork.DbContext.Set<Procedure>() on ps.ProcedureId equals p.ProcedureId
                 join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on ps.ProcedureShareId equals l2.ProcedureShareId

                 join cba in filteredAmounts on l2.ProcedureBudgetLevel2Id equals cba.ProcedureBudgetLevel2Id into j1
                 from cba in j1.DefaultIfEmpty()

                 group new
                 {
                     CurrentEuAmount = (decimal?)cba.CurrentEuAmount,
                     CurrentBgAmount = (decimal?)cba.CurrentBgAmount,
                     CurrentSelfAmount = (decimal?)cba.CurrentSelfAmount,
                 }
                 by new
                 {
                     ProgrammeId = ps.ProgrammeId,
                 }
                 into g
                 select new
                 {
                     ProgrammeId = g.Key.ProgrammeId,
                     ContractedEuAmount = g.Sum(i => i.CurrentEuAmount),
                     ContractedBgAmount = g.Sum(i => i.CurrentBgAmount),
                     ContractedSelfAmount = g.Sum(i => i.CurrentSelfAmount),
                 })
                 .ToList();

            var historicContracted =
                (from ca in this.unitOfWork.DbContext.Set<HistoricContractContractedAmount>()
                 join c in this.unitOfWork.DbContext.Set<HistoricContract>() on ca.HistoricContractId equals c.HistoricContractId into g
                 from hcca in g.DefaultIfEmpty()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(p => p.IsPrimary) on hcca.ProcedureId equals ps.ProcedureId
                 where ca.IsLast == true
                 group new
                 {
                     ContractedEuAmount = (decimal?)ca.ContractedEuAmount,
                     ContractedBgAmount = (decimal?)ca.ContractedBgAmount,
                     ContractedSelfAmount = (decimal?)ca.ContractedSeftAmount,
                 }
                 by ps.ProgrammeId into g
                 select new
                 {
                     ProgrammeId = g.Key,
                     ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                     ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                     ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                 })
                 .ToList();

            var allContracted = contracted
                .Union(historicContracted)
                .GroupBy(g => g.ProgrammeId)
                .Select(g => new
                {
                    ProgrammeId = g.Key,
                    ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                    ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                    ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                })
                .ToList();

            var reimbursedAmounts =
                (from ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>()
                 where ra.Status == ReimbursedAmountStatus.Entered && ReportsReimbursements.Contains(ra.Reimbursement)
                 group new
                 {
                     EuAmount = ra.PrincipalBfp.EuAmount,
                     BgAmount = ra.PrincipalBfp.BgAmount,
                 }
                 by ra.ProgrammeId into g
                 select new
                 {
                     ProgrammeId = g.Key,
                     EuAmount = g.Sum(i => i.EuAmount),
                     BgAmount = g.Sum(i => i.BgAmount),
                 })
                 .ToList();

            var historicReimbursedAmounts =
                (from ra in this.unitOfWork.DbContext.Set<HistoricContractReimbursedAmount>()
                 join c in this.unitOfWork.DbContext.Set<HistoricContract>() on ra.HistoricContractId equals c.HistoricContractId into g
                 from hcra in g.DefaultIfEmpty()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hcra.ProcedureId equals ps.ProcedureId
                 group new
                 {
                     EuAmount = ra.ReimbursedPrincipalEuAmount,
                     BgAmount = ra.ReimbursedPrincipalBgAmount,
                 }
                 by ps.ProgrammeId into g
                 select new
                 {
                     ProgrammeId = g.Key,
                     EuAmount = g.Sum(i => i.EuAmount),
                     BgAmount = g.Sum(i => i.BgAmount),
                 })
                 .ToList();

            var allReimbursedAmounts = reimbursedAmounts
                .Union(historicReimbursedAmounts)
                .GroupBy(g => g.ProgrammeId)
                .Select(g => new
                {
                    ProgrammeId = g.Key,
                    EuAmount = g.Sum(i => i.EuAmount),
                    BgAmount = g.Sum(i => i.BgAmount),
                })
                .ToList();

            var payed =
                (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                 where pa.Status == ActuallyPaidAmountStatus.Entered
                 group new
                 {
                     EuAmount = (decimal?)pa.PaidBfpEuAmount,
                     BgAmount = (decimal?)pa.PaidBfpBgAmount,
                 }
                 by pa.ProgrammeId into g
                 select new
                 {
                     ProgrammeId = g.Key,
                     PayedEuAmount = g.Sum(i => i.EuAmount),
                     PayedBgAmount = g.Sum(i => i.BgAmount),
                 })
                 .ToList();

            var historicPayed =
                (from apa in this.unitOfWork.DbContext.Set<HistoricContractActuallyPaidAmount>()
                 join c in this.unitOfWork.DbContext.Set<HistoricContract>() on apa.HistoricContractId equals c.HistoricContractId into g
                 from hcapa in g.DefaultIfEmpty()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hcapa.ProcedureId equals ps.ProcedureId
                 group new
                 {
                     EuAmount = (decimal?)apa.PaidEuAmount,
                     BgAmount = (decimal?)apa.PaidBgAmount,
                 }
                 by ps.ProgrammeId into g
                 select new
                 {
                     ProgrammeId = g.Key,
                     PayedEuAmount = g.Sum(i => i.EuAmount),
                     PayedBgAmount = g.Sum(i => i.BgAmount),
                 })
                 .ToList();

            var allPayed = payed
                .Union(historicPayed)
                .GroupBy(g => g.ProgrammeId)
                .Select(g => new
                {
                    ProgrammeId = g.Key,
                    PayedEuAmount = g.Sum(i => i.PayedEuAmount),
                    PayedBgAmount = g.Sum(i => i.PayedBgAmount),
                })
                .ToList();

            var euReimbursedAmount =
                (from era in this.unitOfWork.DbContext.Set<EuReimbursedAmount>()
                 where era.Status == EuReimbursedAmountStatus.Entered
                 group era.EuTranche by era.ProgrammeId into g
                 select new
                 {
                     ProgrammeId = g.Key,
                     EuTranche = g.Sum(i => i),
                 }).ToList();

            var projects =
                (from p in this.unitOfWork.DbContext.Set<Project>()
                 join procs in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals procs.ProcedureId
                 join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on p.ProjectId equals esp.ProjectId into g0
                 from esp in g0.DefaultIfEmpty()
                 join es in this.unitOfWork.DbContext.Set<EvalSession>() on esp.EvalSessionId equals es.EvalSessionId into g1
                 from es in g1.DefaultIfEmpty()
                 where p.RegistrationStatus != ProjectRegistrationStatus.Withdrawn && (esp == null || !esp.IsDeleted) && (es == null || es.EvalSessionStatus != EvalSessionStatus.Canceled)
                 select new { p.ProjectId, procs.ProgrammeId }).Distinct()
                 .GroupBy(o => o.ProgrammeId)
                 .Select(g => new { ProgrammeId = g.Key, ProjectsCount = g.Count() })
                 .ToList();

            var contracts =
                (from c in this.unitOfWork.DbContext.Set<Contract>()
                 where c.ContractStatus == ContractStatus.Entered
                 group c by c.ProgrammeId into g
                 select new { ProgrammeId = g.Key, ContractsCount = g.Count() }).ToList();

            var historicContracts =
                (from c in this.unitOfWork.DbContext.Set<HistoricContract>()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                 group c by ps.ProgrammeId into g
                 select new { ProgrammeId = g.Key, ContractsCount = g.Count() }).ToList();

            var allContracts = contracts
                .Union(historicContracts)
                .GroupBy(g => g.ProgrammeId)
                .Select(g => new
                {
                    ProgrammeId = g.Key,
                    ContractsCount = g.Sum(i => i.ContractsCount),
                })
                .ToList();

            var programs =
                (from b in budgets
                 join c in allContracted on b.ProgrammeId equals c.ProgrammeId into j1
                 from c in j1.DefaultIfEmpty()

                 join p in allPayed on b.ProgrammeId equals p.ProgrammeId into j2
                 from p in j2.DefaultIfEmpty()

                 join era in euReimbursedAmount on b.ProgrammeId equals era.ProgrammeId into j3
                 from era in j3.DefaultIfEmpty()

                 join proj in projects on b.ProgrammeId equals proj.ProgrammeId into j4
                 from proj in j4.DefaultIfEmpty()

                 join contr in allContracts on b.ProgrammeId equals contr.ProgrammeId into j5
                 from contr in j5.DefaultIfEmpty()

                 join ra in allReimbursedAmounts on b.ProgrammeId equals ra.ProgrammeId into j6
                 from ra in j6.DefaultIfEmpty()

                 select new ProgrammeBudgetDetailedVO
                 {
                     Id = b.ProgrammeId.Value,
                     Code = b.Code,
                     Name = b.ProgrammeShortName,
                     NameAlt = b.ProgrammeShortNameAlt,
                     PortalOrderNum = b.PortalOrderNum,
                     BudgetTotal = (b.BgAmount ?? 0m) + (b.BgReservedAmount ?? 0m) + (b.EuAmount ?? 0m) + (b.EuReservedAmount ?? 0m),
                     BudgetEU = (b.EuAmount ?? 0m) + (b.EuReservedAmount ?? 0m),
                     BudgetNational = (b.BgAmount ?? 0m) + (b.BgReservedAmount ?? 0m),
                     ProjectsCount = proj?.ProjectsCount ?? 0,
                     ContractsCount = contr?.ContractsCount ?? 0,
                     IncludeSelfAmount = true,
                     ContractEU = c?.ContractedEuAmount ?? 0m,
                     ContractNational = c?.ContractedBgAmount ?? 0m,
                     ContractSelf = c?.ContractedSelfAmount ?? 0m,
                     PaidEU = (p?.PayedEuAmount ?? 0m) - (ra?.EuAmount ?? 0m),
                     PaidNational = (p?.PayedBgAmount ?? 0m) - (ra?.BgAmount ?? 0m),
                     ReceivedTotal = era?.EuTranche ?? 0m,
                     ProgrammeGroupId = b.ProgrammeGroupId,
                 }).ToList();

            var opStatOverrides = this.mainUow.DbContext.Set<OpStatOverride>().ToList();

            programs = (from p in programs
                        let o = opStatOverrides.Where(o => o.ProgrammeCode == p.Code).FirstOrDefault()
                        select new ProgrammeBudgetDetailedVO
                        {
                            Id = p.Id,
                            Code = p.Code,
                            Name = p.Name,
                            NameAlt = p.NameAlt,
                            PortalOrderNum = p.PortalOrderNum,
                            BudgetTotal = p.BudgetTotal,
                            BudgetEU = p.BudgetEU,
                            BudgetNational = p.BudgetNational,
                            IncludeSelfAmount = p.IncludeSelfAmount,
                            ProjectsCount = p.ProjectsCount + (o?.ProjectsCount ?? 0),
                            ContractsCount = p.ContractsCount + (o?.ContractsCount ?? 0),
                            ContractEU = p.ContractEU + (o?.ContractedEuAmount ?? 0),
                            ContractNational = p.ContractNational + (o?.ContractedBgAmount ?? 0),
                            ContractSelf = p.ContractSelf + (o?.ContractedSelfAmount ?? 0),
                            PaidEU = p.PaidEU + (o?.PaidEuAmount ?? 0),
                            PaidNational = p.PaidNational + (o?.PaidBgAmount ?? 0),
                            ReceivedTotal = p.ReceivedTotal,
                            ProgrammeGroupId = p.ProgrammeGroupId,
                        }).ToList();

            return programs;
        }

        public IList<ProgrammeBudgetTotalsVO> GetProgrammeBudgetTotals(bool getProgrammeGroups, int? programmeGroupId = null)
        {
            var programmePredicate = PredicateBuilder.True<Programme>();

            if (getProgrammeGroups == false)
            {
                programmePredicate = programmePredicate.And(p => p.ProgrammeGroupId == null);
            }
            else if (programmeGroupId != null)
            {
                programmePredicate = programmePredicate.AndEquals(p => p.ProgrammeGroupId, programmeGroupId);
            }

            return (from p in this.unitOfWork.DbContext.Set<Programme>().Where(programmePredicate)
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on p.MapNodeId equals mnr.ProgrammeId
                    join mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>() on mnr.MapNodeId equals mnb.MapNodeId into j1
                    from b in j1.DefaultIfEmpty()

                    group new
                    {
                        EuAmount = (decimal?)b.EuAmount,
                        EuReservedAmount = (decimal?)b.EuReservedAmount,
                        BgAmount = (decimal?)b.BgAmount,
                        BgReservedAmount = (decimal?)b.BgReservedAmount,
                    }
                    by new
                    {
                        p.MapNodeId,
                        ProgrammeName = p.ShortName,
                        ProgrammeNameAlt = p.PortalShortNameAlt,
                    }
                    into g
                    select new ProgrammeBudgetTotalsVO
                    {
                        ProgrammeName = g.Key.ProgrammeName,
                        ProgrammeNameAlt = g.Key.ProgrammeNameAlt,
                        TotalAmount =
                        g.Sum(b => b.BgAmount ?? 0m) +
                        g.Sum(b => b.BgReservedAmount ?? 0m) +
                        g.Sum(b => b.EuAmount ?? 0m) +
                        g.Sum(b => b.EuReservedAmount ?? 0m),
                    })
                    .ToList();
        }

        public IList<FinanceSourceBudgetTotalsVO> GetFinanceSourceTotals(bool getProgrammeGroups, int? programmeGroupId = null)
        {
            var programmePredicate = PredicateBuilder.True<Programme>();

            if (getProgrammeGroups == false)
            {
                programmePredicate = programmePredicate.And(p => p.ProgrammeGroupId == null);
            }
            else if (programmeGroupId != null)
            {
                programmePredicate = programmePredicate.AndEquals(p => p.ProgrammeGroupId, programmeGroupId);
            }

            return (from p in this.unitOfWork.DbContext.Set<Programme>().Where(programmePredicate)
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on p.MapNodeId equals mnr.ProgrammeId
                    join mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>() on mnr.MapNodeId equals mnb.MapNodeId
                    group new
                    {
                        EuAmount = (decimal?)mnb.EuAmount,
                        EuReserverAmount = (decimal?)mnb.EuReservedAmount,
                    }
                    by mnb.FinanceSource into g
                    select new FinanceSourceBudgetTotalsVO
                    {
                        FinanceSource = g.Key,
                        TotalAmount = g.Sum(b => (b.EuAmount ?? 0m) + (b.EuReserverAmount ?? 0m)),
                    })
                    .ToList();
        }

        public IList<ProgrammeFinanceSourceBudgetsVO> GetFinanceSourceTotalsByProgramme(bool getProgrammeGroups, int? programmeGroupId = null)
        {
            var programmePredicate = PredicateBuilder.True<Programme>();

            if (getProgrammeGroups == false)
            {
                programmePredicate = programmePredicate.And(p => p.ProgrammeGroupId == null);
            }
            else if (programmeGroupId != null)
            {
                programmePredicate = programmePredicate.AndEquals(p => p.ProgrammeGroupId, programmeGroupId);
            }

            return (from p in this.unitOfWork.DbContext.Set<Programme>().Where(programmePredicate)
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on p.MapNodeId equals mnr.ProgrammeId
                    join mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>() on mnr.MapNodeId equals mnb.MapNodeId into j1
                    from b in j1.DefaultIfEmpty()
                    group new
                    {
                        FinanceSource = b.FinanceSource,
                        EuAmount = (decimal?)b.EuAmount,
                        EuReservedAmount = (decimal?)b.EuReservedAmount,
                    }
                    by new
                    {
                        p.MapNodeId,
                        p.ShortName,
                        p.PortalShortNameAlt,
                    }
                 into g
                    select new
                    {
                        ProgrammeId = g.Key.MapNodeId,
                        Name = g.Key.ShortName,
                        NameAlt = g.Key.PortalShortNameAlt,
                        FinanceSources = g.Select(t => new
                        {
                            FinanceSource = t.FinanceSource,
                            TotalAmount = (t.EuAmount ?? 0) + (t.EuReservedAmount ?? 0),
                        }),
                    }
                 into g1
                    select new ProgrammeFinanceSourceBudgetsVO()
                    {
                        Name = g1.Name,
                        NameAlt = g1.NameAlt,
                        ESFAmount = g1.FinanceSources.Where(fs => fs.FinanceSource == FinanceSource.EuropeanSocialFund).Sum(fs => fs.TotalAmount),
                        ERDFAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.EuropeanRegionalDevelopmentFund).Sum(a => a.TotalAmount),
                        CFAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.CohesionFund).Sum(a => a.TotalAmount),
                        YEIAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.YouthEmploymentInitiative).Sum(a => a.TotalAmount),
                        FEAMDAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.FundForEuropeanAidToTheMostDeprived).Sum(a => a.TotalAmount),
                        EFMDRAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.EFMDR).Sum(a => a.TotalAmount),
                        EZFRSRAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.EZFRSR).Sum(a => a.TotalAmount),
                        FVSAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.FVS).Sum(a => a.TotalAmount),
                        FUMIAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.FUMI).Sum(a => a.TotalAmount),
                        OtherAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.Other).Sum(a => a.TotalAmount),
                        EEAFMAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.EEAFM).Sum(a => a.TotalAmount),
                        NFMAmount = g1.FinanceSources.Where(a => a.FinanceSource == FinanceSource.NFM).Sum(a => a.TotalAmount),
                    })
                 .ToList();
        }
    }
}
