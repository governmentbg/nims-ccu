using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Autofac.Extras.Attributed;
using Eumis.Public.Common;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Common.Localization;
using Eumis.Public.Data.Core;
using Eumis.Public.Data.Linq;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities;
using Eumis.Public.Domain.Entities.Umis.Companies;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.Debts;
using Eumis.Public.Domain.Entities.Umis.EuReimbursedAmounts;
using Eumis.Public.Domain.Entities.Umis.EvalSessions;
using Eumis.Public.Domain.Entities.Umis.HistoricContracts;
using Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes;
using Eumis.Public.Domain.Entities.Umis.Indicators;
using Eumis.Public.Domain.Entities.Umis.Measures;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators.Views;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammePriorities;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Domain.Entities.Umis.Procedures;
using Eumis.Public.Domain.Entities.Umis.Projects;
using Eumis.Public.Domain.Entities.Umis.Registrations;
using Eumis.Public.Domain.Entities.Umis.Users;

namespace Eumis.Public.Model.Repositories
{
    internal class UmisRepository : Repository, IUmisRepository
    {
        private const int Projects_IndicativePageNumber = 2000;
        private const int ContractReportsAP_IndicativePageNumber = 10;
        private const int ContractReportsPF_IndicativePageNumber = 500;
        private const int ContractReportsT_IndicativePageNumber = 500;
        private const int ContractReportsPTF_IndicativePageNumber = 1000;
        private const int ProjectCommunications_IndicativePageNumber = 20;
        private const int ContractProcurementPlans_IndicativePageNumber = 1000;

        private const int IndicativePageNumberPerTree = 8000;

        private UnitOfWork mainUow;

        public static readonly Reimbursement[] ReportsReimbursements = new Reimbursement[]
        {
                Reimbursement.Bank,
                Reimbursement.WrittenОff,
                Reimbursement.DistributedLimitDeduction,
        };

        public UmisRepository([WithKey(DbKey.Umis)]IUnitOfWork uow, [WithKey(DbKey.Main)]IUnitOfWork mainUow)
            : base(uow)
        {
            this.mainUow = (UnitOfWork)mainUow;
        }

        public int GetSavedTrees()
        {
            var projectsCount = this.unitOfWork.DbContext.Set<Project>().Count();
            var contractReportsAPCount = this.unitOfWork.DbContext.Set<ContractReport>().Where(x => x.ReportType == ContractReportType.AdvancePayment).Count();
            var contractReportsPFCount = this.unitOfWork.DbContext.Set<ContractReport>().Where(x => x.ReportType == ContractReportType.PaymentFinancial).Count();
            var contractReportsTCount = this.unitOfWork.DbContext.Set<ContractReport>().Where(x => x.ReportType == ContractReportType.Technical).Count();
            var contractReportsPTFCount = this.unitOfWork.DbContext.Set<ContractReport>().Where(x => x.ReportType == ContractReportType.PaymentTechnicalFinancial).Count();
            var projectCommunicationsCount = this.unitOfWork.DbContext.Set<ProjectCommunication>().Count() + this.unitOfWork.DbContext.Set<ContractCommunicationXml>().Count();
            var contractProcurementPlansCount = this.unitOfWork.DbContext.Set<ContractProcurementPlan>().Count();

            var projectsTotalPagesCount = projectsCount * Projects_IndicativePageNumber;
            var contractReportsAPTotalPagesCount = contractReportsAPCount * ContractReportsAP_IndicativePageNumber;
            var contractReportsPFTotalPagesCount = contractReportsPFCount * ContractReportsPF_IndicativePageNumber;
            var contractReportsTTotalPagesCount = contractReportsTCount * ContractReportsT_IndicativePageNumber;
            var contractReportsPTFTotalPagesCount = contractReportsPTFCount * ContractReportsPTF_IndicativePageNumber;
            var projectCommunicationsTotalPagesCount = projectCommunicationsCount * ProjectCommunications_IndicativePageNumber;
            var contractProcurementPlansTotalPagesCount = contractProcurementPlansCount * ContractProcurementPlans_IndicativePageNumber;

            var totalPagesCount = projectsTotalPagesCount +
                contractReportsAPTotalPagesCount +
                contractReportsPFTotalPagesCount +
                contractReportsTTotalPagesCount +
                contractReportsPTFTotalPagesCount +
                projectCommunicationsTotalPagesCount +
                contractProcurementPlansTotalPagesCount;

            return (int)(totalPagesCount / IndicativePageNumberPerTree);
        }

        public IList<ProgrammeBudgetDetailedVO> GetProgrammeBudgetDetailed(DateTime? from = null, DateTime? to = null, bool includeSelfAmount = false)
        {
            var budgets =
                (from p in this.unitOfWork.DbContext.Set<Programme>()
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
                }
                into g
                 select new
                 {
                     ProgrammeShortName = g.Key.ProgrammeShortName,
                     ProgrammeShortNameAlt = g.Key.ProgrammeShortNameAlt,
                     ProgrammeId = g.Key.ProgrammeId,
                     Code = g.Key.Code,
                     PortalOrderNum = g.Key.PortalOrderNum,
                     EuAmount = g.Sum(i => i.EuAmount),
                     EuReservedAmount = g.Sum(i => i.EuReservedAmount),
                     BgAmount = g.Sum(i => i.BgAmount),
                     BgReservedAmount = g.Sum(i => i.BgReservedAmount),
                 })
                    .OrderBy(e => e.PortalOrderNum)
                    .ToList();

            var contractPredicate = PredicateBuilder.True<Contract>();
            var historicContractPredicate = PredicateBuilder.True<HistoricContract>();

            if (from.HasValue)
            {
                contractPredicate = contractPredicate.And(c => c.ContractDate >= from);
                historicContractPredicate = historicContractPredicate.And(c => c.ContractDate >= from);
            }

            if (to.HasValue)
            {
                contractPredicate = contractPredicate.And(c => c.ContractDate <= to);
                historicContractPredicate = historicContractPredicate.And(c => c.ContractDate <= to);
            }

            var filteredAmounts =
                from cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(cba1 => cba1.IsActive)
                join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on cba.ContractId equals c.ContractId
                select cba;

            var contracted =
                (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
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
                 join c in this.unitOfWork.DbContext.Set<HistoricContract>().Where(historicContractPredicate) on ca.HistoricContractId equals c.HistoricContractId into g
                 from hcca in g.DefaultIfEmpty()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hcca.ProcedureId equals ps.ProcedureId
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

            var reimbursedAmountPredicate = PredicateBuilder.True<ReimbursedAmount>();
            var historicReimbursedAmountPredicate = PredicateBuilder.True<HistoricContractReimbursedAmount>();
            if (from.HasValue)
            {
                reimbursedAmountPredicate = reimbursedAmountPredicate.And(c => c.ReimbursementDate >= from);
                historicReimbursedAmountPredicate = historicReimbursedAmountPredicate.And(c => c.ReimbursementDate >= from);
            }

            if (to.HasValue)
            {
                reimbursedAmountPredicate = reimbursedAmountPredicate.And(c => c.ReimbursementDate <= to);
                historicReimbursedAmountPredicate = historicReimbursedAmountPredicate.And(c => c.ReimbursementDate <= to);
            }

            var reimbursedAmounts =
                (from ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>().Where(reimbursedAmountPredicate)
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
                (from ra in this.unitOfWork.DbContext.Set<HistoricContractReimbursedAmount>().Where(historicReimbursedAmountPredicate)
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

            var actuallyPaidAmountPredicate = PredicateBuilder.True<ActuallyPaidAmount>();
            var historicActuallyPaidAmountPredicate = PredicateBuilder.True<HistoricContractActuallyPaidAmount>();
            if (from.HasValue)
            {
                actuallyPaidAmountPredicate = actuallyPaidAmountPredicate.And(c => c.PaymentDate >= from);
                historicActuallyPaidAmountPredicate = historicActuallyPaidAmountPredicate.And(c => c.PaymentDate >= from);
            }

            if (to.HasValue)
            {
                actuallyPaidAmountPredicate = actuallyPaidAmountPredicate.And(c => c.PaymentDate <= to);
                historicActuallyPaidAmountPredicate = historicActuallyPaidAmountPredicate.And(c => c.PaymentDate <= to);
            }

            var payed =
                (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(actuallyPaidAmountPredicate)
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
                (from apa in this.unitOfWork.DbContext.Set<HistoricContractActuallyPaidAmount>().Where(historicActuallyPaidAmountPredicate)
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

            var euReimbursedAmountPredicate = PredicateBuilder.True<EuReimbursedAmount>();
            if (from.HasValue)
            {
                euReimbursedAmountPredicate = euReimbursedAmountPredicate.And(c => c.Date >= from);
            }

            if (to.HasValue)
            {
                euReimbursedAmountPredicate = euReimbursedAmountPredicate.And(c => c.Date <= to);
            }

            var euReimbursedAmount =
                (from era in this.unitOfWork.DbContext.Set<EuReimbursedAmount>().Where(euReimbursedAmountPredicate)
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
                     IncludeSelfAmount = includeSelfAmount,
                     ContractEU = c?.ContractedEuAmount ?? 0m,
                     ContractNational = c?.ContractedBgAmount ?? 0m,
                     ContractSelf = c?.ContractedSelfAmount ?? 0m,
                     PaidEU = (p?.PayedEuAmount ?? 0m) - (ra?.EuAmount ?? 0m),
                     PaidNational = (p?.PayedBgAmount ?? 0m) - (ra?.BgAmount ?? 0m),
                     ReceivedTotal = era?.EuTranche ?? 0m,
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
                        }).ToList();

            return programs;
        }

        public IList<PPFundsWithProcedureFundsVO> GetPPFundsWithProcedureFunds(int programmeId)
        {
            var contracted =
                (from mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on mnr.MapNodeId equals ps.ProgrammePriorityId
                 join p in this.unitOfWork.DbContext.Set<Procedure>() on ps.ProcedureId equals p.ProcedureId
                 join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on ps.ProcedureShareId equals l2.ProcedureShareId

                 join cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(cba1 => cba1.IsActive) on l2.ProcedureBudgetLevel2Id equals cba.ProcedureBudgetLevel2Id into j1
                 from cba in j1.DefaultIfEmpty()

                 where mnr.ProgrammeId == programmeId
                    && p.ProcedureStatus != ProcedureStatus.Canceled
                 group new
                 {
                     CurrentEuAmount = (decimal?)cba.CurrentEuAmount,
                     CurrentBgAmount = (decimal?)cba.CurrentBgAmount,
                     CurrentSelfAmount = (decimal?)cba.CurrentSelfAmount,
                 }
                 by new
                 {
                     MapNodeId = mnr.MapNodeId,
                     ProcedureId = ps.ProcedureId,
                     ProcedureName = p.Name,
                     ProcedureNameAlt = p.NameAlt,
                     ProcedureCode = p.Code,
                 }
                 into g
                 select new
                 {
                     MapNodeId = g.Key.MapNodeId,
                     ProcedureId = g.Key.ProcedureId,
                     ProcedureName = g.Key.ProcedureName,
                     ProcedureNameAlt = g.Key.ProcedureNameAlt,
                     ProcedureCode = g.Key.ProcedureCode,
                     ContractedEuAmount = g.Sum(i => i.CurrentEuAmount),
                     ContractedBgAmount = g.Sum(i => i.CurrentBgAmount),
                     ContractedSelfAmount = g.Sum(i => i.CurrentSelfAmount),
                 })
                 .ToList();

            var historicContracted =
                 (from ca in this.unitOfWork.DbContext.Set<HistoricContractContractedAmount>()
                  join c in this.unitOfWork.DbContext.Set<HistoricContract>() on ca.HistoricContractId equals c.HistoricContractId into j1
                  from hcca in j1.DefaultIfEmpty()

                  join p in this.unitOfWork.DbContext.Set<Procedure>() on hcca.ProcedureId equals p.ProcedureId into j2
                  from p in j2.DefaultIfEmpty()

                  join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                  where ca.IsLast == true && ps.ProgrammeId == programmeId
                 group new
                 {
                     CurrentEuAmount = (decimal?)ca.ContractedEuAmount,
                     CurrentBgAmount = (decimal?)ca.ContractedBgAmount,
                     CurrentSelfAmount = (decimal?)ca.ContractedSeftAmount,
                 }
                 by new
                 {
                     MapNodeId = ps.ProgrammePriorityId,
                     ProcedureId = hcca.ProcedureId,
                     ProcedureName = p.Name,
                     ProcedureNameAlt = p.NameAlt,
                     ProcedureCode = p.Code,
                 }
                 into g
                 select new
                 {
                     MapNodeId = g.Key.MapNodeId,
                     ProcedureId = g.Key.ProcedureId,
                     ProcedureName = g.Key.ProcedureName,
                     ProcedureNameAlt = g.Key.ProcedureNameAlt,
                     ProcedureCode = g.Key.ProcedureCode,
                     ContractedEuAmount = g.Sum(i => i.CurrentEuAmount),
                     ContractedBgAmount = g.Sum(i => i.CurrentBgAmount),
                     ContractedSelfAmount = g.Sum(i => i.CurrentSelfAmount),
                 })
                 .ToList();

            var allContracted = contracted
                .Union(historicContracted)
                .GroupBy(g => new { g.MapNodeId, g.ProcedureId, g.ProcedureName, g.ProcedureNameAlt, g.ProcedureCode })
                .Select(g => new
                {
                    MapNodeId = g.Key.MapNodeId,
                    ProcedureId = g.Key.ProcedureId,
                    ProcedureName = g.Key.ProcedureName,
                    ProcedureNameAlt = g.Key.ProcedureNameAlt,
                    ProcedureCode = g.Key.ProcedureCode,
                    ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                    ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                    ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                })
                .ToList();

            var debtReimbursedAmounts = (from ra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                                        join d in this.unitOfWork.DbContext.Set<ContractDebt>() on ra.ContractDebtId equals d.ContractDebtId
                                        join c in this.unitOfWork.DbContext.Set<Contract>() on ra.ContractId equals c.ContractId
                                        join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                                        where ra.Status == ReimbursedAmountStatus.Entered &&
                                            p.ProcedureStatus != ProcedureStatus.Canceled &&
                                            !ra.ProgrammePriorityId.HasValue &&
                                            ReportsReimbursements.Contains(ra.Reimbursement)
                                        group new
                                        {
                                            EuAmount = ra.PrincipalBfp.EuAmount,
                                            BgAmount = ra.PrincipalBfp.BgAmount,
                                        }
                                        by new
                                        {
                                            MapNodeId = d.ProgrammePriorityId,
                                            ProcedureId = c.ProcedureId,
                                            ProcedureName = p.Name,
                                            ProcedureNameAlt = p.NameAlt,
                                            ProcedureCode = p.Code,
                                        }
                                        into g
                                        select new
                                        {
                                            g.Key.MapNodeId,
                                            g.Key.ProcedureId,
                                            g.Key.ProcedureName,
                                            g.Key.ProcedureNameAlt,
                                            g.Key.ProcedureCode,
                                            EuAmount = g.Sum(i => i.EuAmount),
                                            BgAmount = g.Sum(i => i.BgAmount),
                                        }).ToList();

            var reimbursedAmounts =
                (from ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>()
                 join c in this.unitOfWork.DbContext.Set<Contract>() on ra.ContractId equals c.ContractId
                 join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                 where ra.Status == ReimbursedAmountStatus.Entered &&
                    p.ProcedureStatus != ProcedureStatus.Canceled &&
                    ra.ProgrammePriorityId.HasValue &&
                    ReportsReimbursements.Contains(ra.Reimbursement)
                 group new
                 {
                     EuAmount = ra.PrincipalBfp.EuAmount,
                     BgAmount = ra.PrincipalBfp.BgAmount,
                 }
                 by new
                 {
                    MapNodeId = ra.ProgrammePriorityId.Value,
                    ProcedureId = c.ProcedureId,
                    ProcedureName = p.Name,
                    ProcedureNameAlt = p.NameAlt,
                    ProcedureCode = p.Code,
                 }
                 into g
                 select new
                 {
                    g.Key.MapNodeId,
                    g.Key.ProcedureId,
                    g.Key.ProcedureName,
                    g.Key.ProcedureNameAlt,
                    g.Key.ProcedureCode,
                    EuAmount = g.Sum(i => i.EuAmount),
                    BgAmount = g.Sum(i => i.BgAmount),
                 }).ToList();

            var historicReimbursedAmounts =
                (from ra in this.unitOfWork.DbContext.Set<HistoricContractReimbursedAmount>()
                 join c in this.unitOfWork.DbContext.Set<HistoricContract>() on ra.HistoricContractId equals c.HistoricContractId into j1
                 from hcra in j1.DefaultIfEmpty()

                 join p in this.unitOfWork.DbContext.Set<Procedure>() on hcra.ProcedureId equals p.ProcedureId into j2
                 from p in j2.DefaultIfEmpty()

                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                 group new
                 {
                     EuAmount = ra.ReimbursedPrincipalEuAmount,
                     BgAmount = ra.ReimbursedPrincipalBgAmount,
                 }
                 by new
                 {
                     MapNodeId = ps.ProgrammePriorityId,
                     ProcedureId = hcra.ProcedureId,
                     ProcedureName = p.Name,
                     ProcedureNameAlt = p.NameAlt,
                     ProcedureCode = p.Code,
                 }
                 into g
                 select new
                 {
                     g.Key.MapNodeId,
                     g.Key.ProcedureId,
                     g.Key.ProcedureName,
                     g.Key.ProcedureNameAlt,
                     g.Key.ProcedureCode,
                     EuAmount = g.Sum(i => i.EuAmount),
                     BgAmount = g.Sum(i => i.BgAmount),
                 })
                 .ToList();

            var allReimbursedAmounts = reimbursedAmounts
                .Union(debtReimbursedAmounts)
                .Union(historicReimbursedAmounts)
                .GroupBy(g => new { g.MapNodeId, g.ProcedureId, g.ProcedureName, g.ProcedureNameAlt, g.ProcedureCode })
                .Select(g => new
                {
                    g.Key.MapNodeId,
                    g.Key.ProcedureId,
                    g.Key.ProcedureName,
                    g.Key.ProcedureNameAlt,
                    g.Key.ProcedureCode,
                    EuAmount = g.Sum(i => i.EuAmount),
                    BgAmount = g.Sum(i => i.BgAmount),
                })
                .ToList();

            var payed =
                (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                 join c in this.unitOfWork.DbContext.Set<Contract>() on pa.ContractId equals c.ContractId
                 join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                 where pa.Status == ActuallyPaidAmountStatus.Entered && p.ProcedureStatus != ProcedureStatus.Canceled
                 group new
                 {
                     EuAmount = (decimal?)pa.PaidBfpEuAmount,
                     BgAmount = (decimal?)pa.PaidBfpBgAmount,
                 }
                 by new
                 {
                     MapNodeId = pa.ProgrammePriorityId,
                     ProcedureId = c.ProcedureId,
                     ProcedureName = p.Name,
                     ProcedureNameAlt = p.NameAlt,
                     ProcedureCode = p.Code,
                 }
                 into g
                 select new
                 {
                     g.Key.MapNodeId,
                     g.Key.ProcedureId,
                     g.Key.ProcedureName,
                     g.Key.ProcedureNameAlt,
                     g.Key.ProcedureCode,
                     PayedEuAmount = g.Sum(i => i.EuAmount),
                     PayedBgAmount = g.Sum(i => i.BgAmount),
                 })
                 .ToList();

            var historicPayed =
                (from apa in this.unitOfWork.DbContext.Set<HistoricContractActuallyPaidAmount>()
                 join c in this.unitOfWork.DbContext.Set<HistoricContract>() on apa.HistoricContractId equals c.HistoricContractId into j1
                 from hcapa in j1.DefaultIfEmpty()

                 join p in this.unitOfWork.DbContext.Set<Procedure>() on hcapa.ProcedureId equals p.ProcedureId into j2
                 from p in j2.DefaultIfEmpty()

                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                 group new
                 {
                     EuAmount = (decimal?)apa.PaidEuAmount,
                     BgAmount = (decimal?)apa.PaidBgAmount,
                 }
                 by new
                 {
                     MapNodeId = ps.ProgrammePriorityId,
                     ProcedureId = hcapa.ProcedureId,
                     ProcedureName = p.Name,
                     ProcedureNameAlt = p.NameAlt,
                     ProcedureCode = p.Code,
                 }
                 into g
                 select new
                 {
                     g.Key.MapNodeId,
                     g.Key.ProcedureId,
                     g.Key.ProcedureName,
                     g.Key.ProcedureNameAlt,
                     g.Key.ProcedureCode,
                     PayedEuAmount = g.Sum(i => i.EuAmount),
                     PayedBgAmount = g.Sum(i => i.BgAmount),
                 })
                 .ToList();

            var allPayed = payed
                .Union(historicPayed)
                .GroupBy(g => new { g.MapNodeId, g.ProcedureId, g.ProcedureName, g.ProcedureNameAlt, g.ProcedureCode })
                .Select(g => new
                {
                    g.Key.MapNodeId,
                    g.Key.ProcedureId,
                    g.Key.ProcedureName,
                    g.Key.ProcedureNameAlt,
                    g.Key.ProcedureCode,
                    PayedEuAmount = g.Sum(i => i.PayedEuAmount),
                    PayedBgAmount = g.Sum(i => i.PayedBgAmount),
                })
                .ToList();

            var procedures1 =
                from c in allContracted
                join p in allPayed on new { c.ProcedureId, c.MapNodeId } equals new { p.ProcedureId, p.MapNodeId } into j1
                from p in j1.DefaultIfEmpty()

                join ra in allReimbursedAmounts on new { c.ProcedureId, c.MapNodeId } equals new { ra.ProcedureId, ra.MapNodeId } into j2
                from ra in j2.DefaultIfEmpty()
                select new { c, p, ra };

            var procedures2 =
                from p in allPayed
                join c in allContracted on new { p.ProcedureId, p.MapNodeId } equals new { c.ProcedureId, c.MapNodeId } into j1
                from c in j1.DefaultIfEmpty()

                join ra in allReimbursedAmounts on new { p.ProcedureId, p.MapNodeId } equals new { ra.ProcedureId, ra.MapNodeId } into j2
                from ra in j2.DefaultIfEmpty()
                select new { c, p, ra };

            // Do a full outer join of allContracted and allPayed
            var procedures = procedures1
                .Union(procedures2)
                .Select(p => new
                {
                    MapNodeId = p.c != null ? p.c.MapNodeId : p.p.MapNodeId,
                    ProcedureId = p.c != null ? p.c.ProcedureId : p.p.ProcedureId,
                    ProcedureName = p.c != null ? p.c.ProcedureName : p.p.ProcedureName,
                    ProcedureNameAlt = p.c != null ? p.c.ProcedureNameAlt : p.p.ProcedureNameAlt,
                    ProcedureCode = p.c != null ? p.c.ProcedureCode : p.p.ProcedureCode,
                    ContractedEuAmount = p.c != null ? p.c.ContractedEuAmount : null,
                    ContractedBgAmount = p.c != null ? p.c.ContractedBgAmount : null,
                    ContractedSelfAmount = p.c != null ? p.c.ContractedSelfAmount : null,
                    PayedEuAmount = p.p != null ? p.p.PayedEuAmount - (p.ra != null ? p.ra.EuAmount : 0m) : null,
                    PayedBgAmount = p.p != null ? p.p.PayedBgAmount - (p.ra != null ? p.ra.BgAmount : 0m) : null,
                });

            var budget = (from p in procedures
                          join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                          group new
                          {
                              EuAmount = (decimal?)ps.EuAmount,
                              BgAmount = (decimal?)ps.BgAmount,
                          }
                               by new
                               {
                                   ProcedureId = p.ProcedureId,
                               }
                                into g
                          select new
                          {
                              g.Key.ProcedureId,
                              BfpEuAmount = g.Sum(i => i.EuAmount),
                              BfPBgAmount = g.Sum(i => i.BgAmount),
                          })
                     .ToList();

            var programmePriorities =
                (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                 join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId

                 join mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>() on pp.MapNodeId equals mnb.MapNodeId into j1
                 from mnb in j1.DefaultIfEmpty()

                 where mnr.ProgrammeId == programmeId
                 group new
                 {
                     EuAmount = (decimal?)mnb.EuAmount,
                     EuReservedAmount = (decimal?)mnb.EuReservedAmount,
                     BgAmount = (decimal?)mnb.BgAmount,
                     BgReservedAmount = (decimal?)mnb.BgReservedAmount,
                 }
                 by new
                 {
                     pp.MapNodeId,
                     pp.Name,
                     pp.NameAlt,
                 }
                into g
                 select new
                 {
                     g.Key.MapNodeId,
                     g.Key.Name,
                     g.Key.NameAlt,
                     EuAmount = g.Sum(i => i.EuAmount),
                     EuReservedAmount = g.Sum(i => i.EuReservedAmount),
                     BgAmount = g.Sum(i => i.BgAmount),
                     BgReservedAmount = g.Sum(i => i.BgReservedAmount),
                 })
                 .ToList()
                 .Select(pp => new PPFundsWithProcedureFundsVO
                 {
                     ProgrammePriorityId = pp.MapNodeId,
                     ProgrammePriorityName = pp.Name,
                     ProgrammePriorityNameAlt = pp.NameAlt,
                     EuAmount = (pp.EuAmount ?? 0m) + (pp.EuReservedAmount ?? 0m),
                     BgAmount = (pp.BgAmount ?? 0m) + (pp.BgReservedAmount ?? 0m),
                     TotalAmount = (pp.EuAmount ?? 0m) + (pp.EuReservedAmount ?? 0m) + (pp.BgAmount ?? 0m) + (pp.BgReservedAmount ?? 0m),
                     Procedures =
                        (from p in procedures.Where(p1 => p1.MapNodeId == pp.MapNodeId)
                         join b in budget on p.ProcedureId equals b.ProcedureId
                         select new PPByProcedureChildVO
                         {
                             ProcedureId = (int)p.ProcedureId,
                             ProcedureName = p.ProcedureName,
                             ProcedureNameAlt = p.ProcedureNameAlt,
                             ProcedureCode = p.ProcedureCode,
                             ContractedEuAmount = p.ContractedEuAmount ?? 0m,
                             ContractedBgAmount = p.ContractedBgAmount ?? 0m,
                             ContractedSelfAmount = p.ContractedSelfAmount ?? 0m,
                             ContractedTotalAmount = (p.ContractedEuAmount ?? 0m) + (p.ContractedBgAmount ?? 0m) + (p.ContractedSelfAmount ?? 0m),
                             PayedEuAmount = p.PayedEuAmount ?? 0m,
                             PayedBgAmount = p.PayedBgAmount ?? 0m,
                             PayedTotalAmount = (p.PayedEuAmount ?? 0m) + (p.PayedBgAmount ?? 0m),
                             BfpEuAmount = b.BfpEuAmount ?? 0m,
                             BfpBgAmount = b.BfPBgAmount ?? 0m,
                         }).ToList(),
                 })
                 .ToList();

            return programmePriorities;
        }

        public ProgrammeBudgetBySourceVO GetProgrammeBudgetBySource(int programmeId)
        {
            var sources = (from fs in this.unitOfWork.DbContext.Set<MapNodeFinanceSource>()
                           where fs.MapNodeId == programmeId
                           select fs.FinanceSource)
                           .ToList();

            var budgets =
                from mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>()
                join mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>() on mnr.MapNodeId equals mnb.MapNodeId

                where mnr.ProgrammeId == programmeId
                select mnb;

            var periods =
                (from bp in this.unitOfWork.DbContext.Set<BudgetPeriod>()

                 join b in budgets on bp.BudgetPeriodId equals b.BudgetPeriodId into j1
                 from b in j1.DefaultIfEmpty()

                 select new
                 {
                     Year = bp.Name,
                     FinanceSource = (FinanceSource?)b.FinanceSource,
                     EuAmount = (decimal?)b.EuAmount,
                     EuReservedAmount = (decimal?)b.EuReservedAmount,
                     BgAmount = (decimal?)b.BgAmount,
                     BgReservedAmount = (decimal?)b.BgReservedAmount,
                 })
                .ToList()
                .GroupBy(b => b.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    Budgets =
                        (from s in sources
                         let funds = (from gi in g
                                      where gi.FinanceSource == s
                                      group gi by gi.FinanceSource into g1
                                      select new
                                      {
                                          EuAmount = g1.Sum(b => b.EuAmount),
                                          EuReservedAmount = g1.Sum(b => b.EuReservedAmount),
                                          BgAmount = g1.Sum(b => b.BgAmount),
                                          BgReservedAmount = g1.Sum(b => b.BgReservedAmount),
                                      }).SingleOrDefault()

                         // if there are no budget items for this source the default decimal 0 will be the value
                         select new
                         {
                             FinanceSource = s,
                             EuAmount = funds != null ? (funds.EuAmount ?? 0m) + (funds.EuReservedAmount ?? 0m) : (decimal?)null,
                             BgAmount = funds != null ? (funds.BgAmount ?? 0m) + (funds.BgReservedAmount ?? 0m) : (decimal?)null,
                         })
                        .ToDictionary(b => b.FinanceSource, b => b),
                })
                .ToList()
                .Select(p => new ProgrammeBudgetBySourceChildVO
                {
                    Year = p.Year,
                    CohesionFund = p.Budgets.ContainsKey(FinanceSource.CohesionFund) ? p.Budgets[FinanceSource.CohesionFund].EuAmount ?? 0m : 0m,
                    EuropeanRegionalDevelopmentFund = p.Budgets.ContainsKey(FinanceSource.EuropeanRegionalDevelopmentFund) ? p.Budgets[FinanceSource.EuropeanRegionalDevelopmentFund].EuAmount ?? 0m : 0m,
                    EuropeanSocialFund = p.Budgets.ContainsKey(FinanceSource.EuropeanSocialFund) ? p.Budgets[FinanceSource.EuropeanSocialFund].EuAmount ?? 0m : 0m,
                    FundForEuropeanAidToTheMostDeprived = p.Budgets.ContainsKey(FinanceSource.FundForEuropeanAidToTheMostDeprived) ? p.Budgets[FinanceSource.FundForEuropeanAidToTheMostDeprived].EuAmount ?? 0m : 0m,
                    YouthEmploymentInitiative = p.Budgets.ContainsKey(FinanceSource.YouthEmploymentInitiative) ? p.Budgets[FinanceSource.YouthEmploymentInitiative].EuAmount ?? 0m : 0m,

                    EFMDR = p.Budgets.ContainsKey(FinanceSource.EFMDR) ? p.Budgets[FinanceSource.EFMDR].EuAmount ?? 0m : 0m,
                    EZFRSR = p.Budgets.ContainsKey(FinanceSource.EZFRSR) ? p.Budgets[FinanceSource.EZFRSR].EuAmount ?? 0m : 0m,
                    FVS = p.Budgets.ContainsKey(FinanceSource.FVS) ? p.Budgets[FinanceSource.FVS].EuAmount ?? 0m : 0m,
                    FUMI = p.Budgets.ContainsKey(FinanceSource.FUMI) ? p.Budgets[FinanceSource.FUMI].EuAmount ?? 0m : 0m,
                    Other = p.Budgets.ContainsKey(FinanceSource.Other) ? p.Budgets[FinanceSource.Other].EuAmount ?? 0m : 0m,
                    EEAFM = p.Budgets.ContainsKey(FinanceSource.EEAFM) ? p.Budgets[FinanceSource.EEAFM].EuAmount ?? 0m : 0m,
                    NFM = p.Budgets.ContainsKey(FinanceSource.NFM) ? p.Budgets[FinanceSource.NFM].EuAmount ?? 0m : 0m,

                    BgAmount = p.Budgets.Sum(b => b.Value.BgAmount ?? 0m),
                })
                .ToList();

            return new ProgrammeBudgetBySourceVO
            {
                CohesionFundTotal = periods.Sum(p => p.CohesionFund),
                EuropeanRegionalDevelopmentFundTotal = periods.Sum(p => p.EuropeanRegionalDevelopmentFund),
                EuropeanSocialFundTotal = periods.Sum(p => p.EuropeanSocialFund),
                FundForEuropeanAidToTheMostDeprivedTotal = periods.Sum(p => p.FundForEuropeanAidToTheMostDeprived),
                YouthEmploymentInitiativeTotal = periods.Sum(p => p.YouthEmploymentInitiative),

                EFMDRTotal = periods.Sum(p => p.EFMDR),
                EZFRSRTotal = periods.Sum(p => p.EZFRSR),
                FVSTotal = periods.Sum(p => p.FVS),
                FUMITotal = periods.Sum(p => p.FUMI),
                OtherTotal = periods.Sum(p => p.Other),
                EEAFMTotal = periods.Sum(p => p.EEAFM),
                NFMTotal = periods.Sum(p => p.NFM),

                BgAmountTotal = periods.Sum(p => p.BgAmount),
                Items = periods,

                Sources = sources,
            };
        }

        public ProgrammeBudgetWithContractedAndPayedVO GetProgrammeBudgetWithContractedAndPayed(int programmeId)
        {
            var budgets =
               from mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>()
               join mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>() on mnr.MapNodeId equals mnb.MapNodeId

               where mnr.ProgrammeId == programmeId
               select mnb;

            var periods =
                (from bp in this.unitOfWork.DbContext.Set<BudgetPeriod>()

                 join b in budgets on bp.BudgetPeriodId equals b.BudgetPeriodId into j1
                 from b in j1.DefaultIfEmpty()

                 group new
                 {
                     EuAmount = (decimal?)b.EuAmount,
                     EuReservedAmount = (decimal?)b.EuReservedAmount,
                     BgAmount = (decimal?)b.BgAmount,
                     BgReservedAmount = (decimal?)b.BgReservedAmount,
                 }
                 by bp.Name into g
                 select new
                 {
                     Year = g.Key,
                     BudgetEuAmount = g.Sum(b => b.EuAmount),
                     BudgetEuReservedAmount = g.Sum(b => b.EuReservedAmount),
                     BudgetBgAmount = g.Sum(b => b.BgAmount),
                     BudgetBgReservedAmount = g.Sum(b => b.BgReservedAmount),
                 })
                .ToList();

            var contracted =
                (from mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(p => p.IsPrimary) on mnr.MapNodeId equals ps.ProgrammePriorityId
                 join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on ps.ProcedureShareId equals l2.ProcedureShareId
                 join cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(cba1 => cba1.IsActive) on l2.ProcedureBudgetLevel2Id equals cba.ProcedureBudgetLevel2Id
                 join c in this.unitOfWork.DbContext.Set<Contract>() on cba.ContractId equals c.ContractId
                 where mnr.ProgrammeId == programmeId && c.ContractDate != null
                 group new
                 {
                     cba.CurrentEuAmount,
                     cba.CurrentBgAmount,
                     cba.CurrentSelfAmount,
                 }
                by c.ContractDate.Value.Year into g
                 select new
                 {
                     Year = g.Key.ToString(),
                     ContractedEuAmount = g.Sum(i => i.CurrentEuAmount),
                     ContractedBgAmount = g.Sum(i => i.CurrentBgAmount),
                     ContractedTotalAmount = g.Sum(i => i.CurrentEuAmount + i.CurrentBgAmount),
                 })
                 .ToList();

            var historicContracted =
                (from ca in this.unitOfWork.DbContext.Set<HistoricContractContractedAmount>()
                 join c in this.unitOfWork.DbContext.Set<HistoricContract>() on ca.HistoricContractId equals c.HistoricContractId into g
                 from hcca in g.DefaultIfEmpty()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(p => p.IsPrimary) on hcca.ProcedureId equals ps.ProcedureId
                 where ps.ProgrammeId == programmeId && ca.IsLast == true && hcca.ContractDate != null
                 group new
                 {
                     ContractedEuAmount = ca.ContractedEuAmount ?? 0,
                     ContractedBgAmount = ca.ContractedBgAmount ?? 0,
                     ContractedSelfAmount = ca.ContractedSeftAmount ?? 0,
                 }
                by hcca.ContractDate.Value.Year into g
                 select new
                 {
                     Year = g.Key.ToString(),
                     ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                     ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                     ContractedTotalAmount = g.Sum(i => i.ContractedEuAmount + i.ContractedBgAmount),
                 })
                 .ToList();

            var allContracted = contracted
                .Union(historicContracted)
                .GroupBy(g => g.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                    ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                    ContractedTotalAmount = g.Sum(i => i.ContractedTotalAmount),
                })
                .ToList();

            var reimbursedAmounts =
                (from ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>()
                 where ra.Status == ReimbursedAmountStatus.Entered && ra.ProgrammeId == programmeId && ReportsReimbursements.Contains(ra.Reimbursement)
                 group new
                 {
                     EuAmount = ra.PrincipalBfp.EuAmount ?? 0,
                     BgAmount = ra.PrincipalBfp.BgAmount ?? 0,
                 }
                by ra.ReimbursementDate.Year into g
                 select new
                 {
                     Year = g.Key.ToString(),
                     EuAmount = g.Sum(i => i.EuAmount),
                     BgAmount = g.Sum(i => i.BgAmount),
                     TotalAmount = g.Sum(i => i.EuAmount + i.BgAmount),
                 }).ToList();

            var historicReimbursedAmounts =
                (from ra in this.unitOfWork.DbContext.Set<HistoricContractReimbursedAmount>()
                 join c in this.unitOfWork.DbContext.Set<HistoricContract>() on ra.HistoricContractId equals c.HistoricContractId into g
                 from hcra in g.DefaultIfEmpty()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hcra.ProcedureId equals ps.ProcedureId
                 where ps.ProgrammeId == programmeId
                 group new
                 {
                     EuAmount = ra.ReimbursedPrincipalEuAmount,
                     BgAmount = ra.ReimbursedPrincipalBgAmount,
                 }
                by ra.ReimbursementDate.Year into g
                 select new
                 {
                     Year = g.Key.ToString(),
                     EuAmount = g.Sum(i => i.EuAmount.Value),
                     BgAmount = g.Sum(i => i.BgAmount.Value),
                     TotalAmount = g.Sum(i => i.EuAmount.Value + i.BgAmount.Value),
                 })
                 .ToList();

            var allReimbursedAmounts = reimbursedAmounts
                .Union(historicReimbursedAmounts)
                .GroupBy(g => g.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    EuAmount = g.Sum(i => i.EuAmount),
                    BgAmount = g.Sum(i => i.BgAmount),
                    TotalAmount = g.Sum(i => i.EuAmount + i.BgAmount),
                })
                .ToList();

            var payed =
                (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                 where pa.Status == ActuallyPaidAmountStatus.Entered && pa.ProgrammeId == programmeId && pa.PaymentDate != null
                 group new
                 {
                     pa.PaidBfpEuAmount,
                     pa.PaidBfpBgAmount,
                 }
                by pa.PaymentDate.Value.Year into g
                 select new
                 {
                     Year = g.Key.ToString(),
                     PayedEuAmount = g.Sum(i => i.PaidBfpEuAmount.HasValue ? i.PaidBfpEuAmount.Value : 0m),
                     PayedBgAmount = g.Sum(i => i.PaidBfpBgAmount.HasValue ? i.PaidBfpBgAmount.Value : 0m),
                     PayedTotalAmount = g.Sum(i => (i.PaidBfpEuAmount.HasValue ? i.PaidBfpEuAmount.Value : 0m) + (i.PaidBfpBgAmount.HasValue ? i.PaidBfpBgAmount.Value : 0m)),
                 })
                 .ToList();

            var historicPayed =
                (from apa in this.unitOfWork.DbContext.Set<HistoricContractActuallyPaidAmount>()
                 join c in this.unitOfWork.DbContext.Set<HistoricContract>() on apa.HistoricContractId equals c.HistoricContractId into g
                 from hcapa in g.DefaultIfEmpty()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hcapa.ProcedureId equals ps.ProcedureId
                 where ps.ProgrammeId == programmeId && apa.PaymentDate != null
                 group new
                 {
                     apa.PaidEuAmount,
                     apa.PaidBgAmount,
                 }
                 by apa.PaymentDate.Year into g
                 select new
                 {
                     Year = g.Key.ToString(),
                     PayedEuAmount = g.Sum(i => i.PaidEuAmount ?? 0m),
                     PayedBgAmount = g.Sum(i => i.PaidBgAmount ?? 0m),
                     PayedTotalAmount = g.Sum(i => (i.PaidEuAmount ?? 0m) + (i.PaidBgAmount ?? 0m)),
                 })
                 .ToList();

            var allPayed = payed
                .Union(historicPayed)
                .GroupBy(g => g.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    PayedEuAmount = g.Sum(i => i.PayedEuAmount),
                    PayedBgAmount = g.Sum(i => i.PayedBgAmount),
                    PayedTotalAmount = g.Sum(i => i.PayedTotalAmount),
                })
                .ToList();

            var periodFunds =
                (from p in periods

                 join c in allContracted on p.Year equals c.Year into j1
                 from c in j1.DefaultIfEmpty()

                 join pa in allPayed on p.Year equals pa.Year into j2
                 from pa in j2.DefaultIfEmpty()

                 join ra in allReimbursedAmounts on p.Year equals ra.Year into j3
                 from ra in j3.DefaultIfEmpty()

                 select new ProgrammeBudgetWithContractedAndPayedChildVO
                 {
                     Year = p.Year,
                     BudgetBgAmount = (p.BudgetBgAmount ?? 0m) + (p.BudgetBgReservedAmount ?? 0m),
                     BudgetEuAmount = (p.BudgetEuAmount ?? 0m) + (p.BudgetEuReservedAmount ?? 0m),
                     Budget = (p.BudgetEuAmount ?? 0m) + (p.BudgetEuReservedAmount ?? 0m) + (p.BudgetBgAmount ?? 0m) + (p.BudgetBgReservedAmount ?? 0m),
                     Contracted = c != null ? c.ContractedTotalAmount : 0m,
                     ContractedBgAmount = c != null ? c.ContractedBgAmount : 0m,
                     ContractedEuAmount = c != null ? c.ContractedEuAmount : 0m,
                     Payed = (pa != null ? pa.PayedTotalAmount : 0m) - (ra != null ? ra.TotalAmount : 0m),
                     PayedBgAmount = (pa != null ? pa.PayedBgAmount : 0m) - (ra != null ? ra.BgAmount : 0m),
                     PayedEuAmount = (pa != null ? pa.PayedEuAmount : 0m) - (ra != null ? ra.EuAmount : 0m),
                 })
                .OrderBy(t => int.Parse(t.Year))
                .ToList();

            decimal budgetBgSum = 0;
            decimal budgetEuSum = 0;

            decimal contractedBgSum = 0;
            decimal contractedEuSum = 0;
            decimal contractedTotalSum = 0;

            decimal payedBgSum = 0;
            decimal payedEuSum = 0;
            decimal payedTotalSum = 0;

            foreach (var periodFund in periodFunds)
            {
                budgetBgSum += periodFund.BudgetBgAmount;
                budgetEuSum += periodFund.BudgetEuAmount;
                periodFund.BudgetBgAmount = budgetBgSum;
                periodFund.BudgetEuAmount = budgetEuSum;
                periodFund.Budget = periodFund.BudgetBgAmount + periodFund.BudgetEuAmount;

                contractedBgSum += periodFund.ContractedBgAmount;
                contractedEuSum += periodFund.ContractedEuAmount;
                contractedTotalSum += periodFund.Contracted;
                periodFund.ContractedBgAmount = contractedBgSum;
                periodFund.ContractedEuAmount = contractedEuSum;
                periodFund.Contracted = contractedTotalSum;

                payedBgSum += periodFund.PayedBgAmount;
                payedEuSum += periodFund.PayedEuAmount;
                payedTotalSum += periodFund.Payed;
                periodFund.PayedBgAmount = payedBgSum;
                periodFund.PayedEuAmount = payedEuSum;
                periodFund.Payed = payedTotalSum;
            }

            return new ProgrammeBudgetWithContractedAndPayedVO
            {
                BudgetTotal = budgetBgSum + budgetEuSum,
                ContractedTotal = contractedTotalSum,
                PayedTotal = payedTotalSum,
                Items = periodFunds,
            };
        }

        public ContractedFundsByYearAndSourceWrapperVO GetContractedFundsByYearAndSource(int contractId, bool isHistoric)
        {
            if (isHistoric)
            {
                var sources = (from hc in this.unitOfWork.DbContext.Set<HistoricContract>()
                               join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hc.ProcedureId equals ps.ProcedureId
                               where hc.HistoricContractId == contractId
                               select ps.FinanceSource)
                        .ToList();

                var periods = Enum.GetValues(typeof(Year)).Cast<int>();

                var query =
                    (from hc in this.unitOfWork.DbContext.Set<HistoricContract>()
                     join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hc.ProcedureId equals ps.ProcedureId
                     join hca in this.unitOfWork.DbContext.Set<HistoricContractContractedAmount>().Where(cba => cba.IsLast) on hc.HistoricContractId equals hca.HistoricContractId

                     // „Общо РИС“ по съответен проект да се разделят по години (2014 до 2023),
                     // по видове плащания(авансово, авансово по чл. 131 от Регламент 1303 / 2013, междинно, окончателно)
                     // join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on c.ContractId equals crp.ContractId
                     where hc.HistoricContractId == contractId
                     group new
                     {
                         ContractedEuAmount = (decimal?)hca.ContractedEuAmount,
                         ContractedBgAmount = (decimal?)hca.ContractedBgAmount,
                         ContractedSelfAmount = (decimal?)hca.ContractedSeftAmount,
                     }
                     by new
                     {
                         FinanceSource = ps.FinanceSource,
                         Year = hc.ContractDate.Value.Year,
                     }
                    into g
                     select new
                     {
                         Year = g.Key.Year,
                         FinanceSource = g.Key.FinanceSource,
                         ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                         ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                         ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                     })
                    .ToList();

                var fundsByYearAndSource =
                    (from bp in periods

                     join cd in query on bp equals cd.Year into j1
                     from cd in j1.DefaultIfEmpty()

                     group cd by bp into g
                     select new
                     {
                         Year = g.Key,
                         Contracted =
                             (from s in sources
                              let funds = (from gi in g
                                           where gi != null && gi.FinanceSource == s
                                           group gi by gi.FinanceSource into g1
                                           select new
                                           {
                                               ContractedBgAmount = g1.Sum(c => c.ContractedBgAmount),
                                               ContractedEuAmount = g1.Sum(c => c.ContractedEuAmount),
                                               ContractedSelfAmount = g1.Sum(c => c.ContractedSelfAmount),
                                           }).SingleOrDefault()

                              // if there are no budget items for this source the default decimal 0 will be the value
                              select new
                              {
                                  FinanceSource = s,
                                  ContractedBgAmount = funds != null ? funds.ContractedBgAmount : null,
                                  ContractedEuAmount = funds != null ? funds.ContractedEuAmount : null,
                                  ContractedSelfAmount = funds != null ? funds.ContractedSelfAmount : null,
                              })
                             .ToDictionary(b => b.FinanceSource, b => b),
                     })
                    .Select(p => new ContractedFundsByYearAndSourceVO
                    {
                        Year = p.Year.ToString(),

                        CohesionFund = p.Contracted.ContainsKey(FinanceSource.CohesionFund) ? p.Contracted[FinanceSource.CohesionFund].ContractedEuAmount ?? 0m : 0m,
                        EuropeanRegionalDevelopmentFund = p.Contracted.ContainsKey(FinanceSource.EuropeanRegionalDevelopmentFund) ? p.Contracted[FinanceSource.EuropeanRegionalDevelopmentFund].ContractedEuAmount ?? 0m : 0m,
                        EuropeanSocialFund = p.Contracted.ContainsKey(FinanceSource.EuropeanSocialFund) ? p.Contracted[FinanceSource.EuropeanSocialFund].ContractedEuAmount ?? 0m : 0m,
                        FundForEuropeanAidToTheMostDeprived = p.Contracted.ContainsKey(FinanceSource.FundForEuropeanAidToTheMostDeprived) ? p.Contracted[FinanceSource.FundForEuropeanAidToTheMostDeprived].ContractedEuAmount ?? 0m : 0m,
                        YouthEmploymentInitiative = p.Contracted.ContainsKey(FinanceSource.YouthEmploymentInitiative) ? p.Contracted[FinanceSource.YouthEmploymentInitiative].ContractedEuAmount ?? 0m : 0m,

                        EFMDR = p.Contracted.ContainsKey(FinanceSource.EFMDR) ? p.Contracted[FinanceSource.EFMDR].ContractedEuAmount ?? 0m : 0m,
                        EZFRSR = p.Contracted.ContainsKey(FinanceSource.EZFRSR) ? p.Contracted[FinanceSource.EZFRSR].ContractedEuAmount ?? 0m : 0m,
                        FVS = p.Contracted.ContainsKey(FinanceSource.FVS) ? p.Contracted[FinanceSource.FVS].ContractedEuAmount ?? 0m : 0m,
                        FUMI = p.Contracted.ContainsKey(FinanceSource.FUMI) ? p.Contracted[FinanceSource.FUMI].ContractedEuAmount ?? 0m : 0m,
                        Other = p.Contracted.ContainsKey(FinanceSource.Other) ? p.Contracted[FinanceSource.Other].ContractedEuAmount ?? 0m : 0m,

                        BgAmount = p.Contracted.Sum(b => b.Value.ContractedBgAmount ?? 0m),
                        SelfAmount = p.Contracted.Sum(b => b.Value.ContractedSelfAmount ?? 0m),
                    })
                    .ToList();

                var res = new ContractedFundsByYearAndSourceWrapperVO();
                res.ContractedFundsByYearAndSource = fundsByYearAndSource;
                res.Sources = sources;

                return res;
            }
            else
            {
                var sources = (from cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(cba1 => cba1.IsActive)
                               join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cba.ProcedureBudgetLevel2Id equals l2.ProcedureBudgetLevel2Id
                               join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on l2.ProcedureShareId equals ps.ProcedureShareId

                               where cba.ContractId == contractId
                               select ps.FinanceSource)
                        .Distinct()
                        .ToList();

                var periods = Enum.GetValues(typeof(Year)).Cast<int>();

                var query =
                    (from c in this.unitOfWork.DbContext.Set<Contract>()
                     join cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(cba1 => cba1.IsActive) on c.ContractId equals cba.ContractId
                     join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cba.ProcedureBudgetLevel2Id equals l2.ProcedureBudgetLevel2Id
                     join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on l2.ProcedureShareId equals ps.ProcedureShareId

                     // „Общо РИС“ по съответен проект да се разделят по години (2014 до 2023),
                     // по видове плащания(авансово, авансово по чл. 131 от Регламент 1303 / 2013, междинно, окончателно)
                     // join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on c.ContractId equals crp.ContractId
                     where c.ContractId == contractId && c.ContractStatus == ContractStatus.Entered
                     group new
                     {
                         ContractedEuAmount = (decimal?)cba.CurrentEuAmount,
                         ContractedBgAmount = (decimal?)cba.CurrentBgAmount,
                         ContractedSelfAmount = (decimal?)cba.CurrentSelfAmount,
                     }
                     by new
                     {
                         FinanceSource = ps.FinanceSource,
                         Year = c.ContractDate.Value.Year,
                     }
                    into g
                     select new
                     {
                         Year = g.Key.Year,
                         FinanceSource = g.Key.FinanceSource,
                         ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                         ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                         ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                     })
                    .ToList();

                var fundsByYearAndSource =
                    (from bp in periods

                     join cd in query on bp equals cd.Year into j1
                     from cd in j1.DefaultIfEmpty()

                     group cd by bp into g
                     select new
                     {
                         Year = g.Key,
                         Contracted =
                             (from s in sources
                              let funds = (from gi in g
                                           where gi != null && gi.FinanceSource == s
                                           group gi by gi.FinanceSource into g1
                                           select new
                                           {
                                               ContractedBgAmount = g1.Sum(c => c.ContractedBgAmount),
                                               ContractedEuAmount = g1.Sum(c => c.ContractedEuAmount),
                                               ContractedSelfAmount = g1.Sum(c => c.ContractedSelfAmount),
                                           }).SingleOrDefault()

                              // if there are no budget items for this source the default decimal 0 will be the value
                              select new
                              {
                                  FinanceSource = s,
                                  ContractedBgAmount = funds != null ? funds.ContractedBgAmount : null,
                                  ContractedEuAmount = funds != null ? funds.ContractedEuAmount : null,
                                  ContractedSelfAmount = funds != null ? funds.ContractedSelfAmount : null,
                              })
                             .ToDictionary(b => b.FinanceSource, b => b),
                     })
                    .Select(p => new ContractedFundsByYearAndSourceVO
                    {
                        Year = p.Year.ToString(),

                        CohesionFund = p.Contracted.ContainsKey(FinanceSource.CohesionFund) ? p.Contracted[FinanceSource.CohesionFund].ContractedEuAmount ?? 0m : 0m,
                        EuropeanRegionalDevelopmentFund = p.Contracted.ContainsKey(FinanceSource.EuropeanRegionalDevelopmentFund) ? p.Contracted[FinanceSource.EuropeanRegionalDevelopmentFund].ContractedEuAmount ?? 0m : 0m,
                        EuropeanSocialFund = p.Contracted.ContainsKey(FinanceSource.EuropeanSocialFund) ? p.Contracted[FinanceSource.EuropeanSocialFund].ContractedEuAmount ?? 0m : 0m,
                        FundForEuropeanAidToTheMostDeprived = p.Contracted.ContainsKey(FinanceSource.FundForEuropeanAidToTheMostDeprived) ? p.Contracted[FinanceSource.FundForEuropeanAidToTheMostDeprived].ContractedEuAmount ?? 0m : 0m,
                        YouthEmploymentInitiative = p.Contracted.ContainsKey(FinanceSource.YouthEmploymentInitiative) ? p.Contracted[FinanceSource.YouthEmploymentInitiative].ContractedEuAmount ?? 0m : 0m,

                        EFMDR = p.Contracted.ContainsKey(FinanceSource.EFMDR) ? p.Contracted[FinanceSource.EFMDR].ContractedEuAmount ?? 0m : 0m,
                        EZFRSR = p.Contracted.ContainsKey(FinanceSource.EZFRSR) ? p.Contracted[FinanceSource.EZFRSR].ContractedEuAmount ?? 0m : 0m,
                        FVS = p.Contracted.ContainsKey(FinanceSource.FVS) ? p.Contracted[FinanceSource.FVS].ContractedEuAmount ?? 0m : 0m,
                        FUMI = p.Contracted.ContainsKey(FinanceSource.FUMI) ? p.Contracted[FinanceSource.FUMI].ContractedEuAmount ?? 0m : 0m,
                        Other = p.Contracted.ContainsKey(FinanceSource.Other) ? p.Contracted[FinanceSource.Other].ContractedEuAmount ?? 0m : 0m,

                        BgAmount = p.Contracted.Sum(b => b.Value.ContractedBgAmount ?? 0m),
                        SelfAmount = p.Contracted.Sum(b => b.Value.ContractedSelfAmount ?? 0m),
                    })
                    .ToList();

                var res = new ContractedFundsByYearAndSourceWrapperVO();
                res.ContractedFundsByYearAndSource = fundsByYearAndSource;
                res.Sources = sources;

                return res;
            }
        }

        public ContractedFundsByAidModeVO GetContractedFundsByAidMode(int contractId)
        {
            var query =
                (from c in this.unitOfWork.DbContext.Set<Contract>()
                 join cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(cba1 => cba1.IsActive) on c.ContractId equals cba.ContractId
                 join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cba.ProcedureBudgetLevel2Id equals l2.ProcedureBudgetLevel2Id

                 where c.ContractId == contractId && c.ContractStatus == ContractStatus.Entered

                 group new
                 {
                     ContractedEuAmount = (decimal?)cba.CurrentEuAmount,
                     ContractedBgAmount = (decimal?)cba.CurrentBgAmount,
                     ContractedSelfAmount = (decimal?)cba.CurrentSelfAmount,
                 }
                 by new
                 {
                     l2.AidMode,
                 }
                 into g
                 select new
                 {
                     DeminimisAmount = g.Key.AidMode == ProcedureBudgetLevel2AidMode.Deminimis ? g.Sum(p => p.ContractedBgAmount) + g.Sum(p => p.ContractedEuAmount) : 0,
                     StateAidAmount = g.Key.AidMode == ProcedureBudgetLevel2AidMode.StateAid ? g.Sum(p => p.ContractedBgAmount) + g.Sum(p => p.ContractedEuAmount) : 0,
                     OtherAmount = g.Key.AidMode == ProcedureBudgetLevel2AidMode.NotApplicable ? g.Sum(p => p.ContractedBgAmount) + g.Sum(p => p.ContractedEuAmount) : 0,
                     SelfAmount = g.Sum(p => p.ContractedSelfAmount),
                 }).ToList();

            var result = new ContractedFundsByAidModeVO
            {
                DeminimisAmount = query.Sum(p => p.DeminimisAmount) ?? 0,
                StateAidAmount = query.Sum(p => p.StateAidAmount) ?? 0,
                OtherAmount = query.Sum(p => p.OtherAmount) ?? 0,
                SelfAmount = query.Sum(p => p.SelfAmount) ?? 0,
            };

            return result;
        }

        public ICollection<PaidAmountsByYearVO> GetPaidAmountsByYear(int contractId)
        {
            var contract = this.unitOfWork.DbContext.Set<Contract>().Where(c => c.ContractId == contractId);

            var reimbursedAmounts =
                from c in contract
                join ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>().Where(cra => cra.Status == ReimbursedAmountStatus.Entered && ReportsReimbursements.Contains(cra.Reimbursement)) on c.ContractId equals ra.ContractId
                group new
                {
                    EuAmount = ra.PrincipalBfp.EuAmount,
                    BgAmount = ra.PrincipalBfp.BgAmount,
                }
                by new
                {
                    c.ContractId,
                    ra.ReimbursementDate.Year,
                }
                into g
                select new
                {
                    ContractId = g.Key.ContractId,
                    Year = g.Key.Year,
                    EuAmount = g.Sum(i => i.EuAmount),
                    BgAmount = g.Sum(i => i.BgAmount),
                };

            var paidAmounts =
                from c in contract
                join pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(pa => pa.Status == ActuallyPaidAmountStatus.Entered) on c.ContractId equals pa.ContractId
                group new
                {
                    PaidEuAmount = pa.PaidBfpEuAmount,
                    PaidBgAmount = pa.PaidBfpBgAmount,
                }
                by new
                {
                    c.ContractId,
                    pa.PaymentDate.Value.Year,
                }
                into g
                select new
                {
                    ContractId = g.Key.ContractId,
                    Year = g.Key.Year,
                    PaidEuAmount = g.Sum(i => i.PaidEuAmount),
                    PaidBgAmount = g.Sum(i => i.PaidBgAmount),
                };

            var lastContractVersionsByYear =
                from c in contract
                join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals cv.ContractId
                where cv.Status == ContractVersionStatus.Archived || cv.Status == ContractVersionStatus.Active
                group new
                {
                    cv.OrderNum,
                }
                by new
                {
                    cv.ContractId,
                    cv.CreateDate.Year,
                }
                into g
                select new
                {
                    g.Key.ContractId,
                    g.Key.Year,
                    OrderNum = g.Max(e => e.OrderNum),
                };

            var contractedAmounts =
                from c in contract
                join lcv in lastContractVersionsByYear on c.ContractId equals lcv.ContractId
                join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on new { lcv.ContractId, lcv.OrderNum } equals new { cv.ContractId, cv.OrderNum }
                join xmla in this.unitOfWork.DbContext.Set<ContractVersionXmlAmount>() on cv.ContractVersionXmlId equals xmla.ContractVersionXmlId
                group new
                {
                    CurrentBfpAmount = xmla.CurrentEuAmount + xmla.CurrentBgAmount,
                }
                by new
                {
                    lcv.Year,
                }
                into g
                select new
                {
                    g.Key.Year,
                    TotalCurrentBfpAmount = g.Sum(e => e.CurrentBfpAmount),
                };

            var periods = Enum.GetValues(typeof(Year)).Cast<int>();

            var query =
                from p in periods

                join pa in paidAmounts on p equals pa.Year into j1
                from pa in j1.DefaultIfEmpty()

                join ra in reimbursedAmounts on p equals ra.Year into j2
                from ra in j2.DefaultIfEmpty()

                join ca in contractedAmounts on p equals ca.Year into j3
                from ca in j3.DefaultIfEmpty()

                select new
                {
                    Year = p,
                    PaidEuAmount = (pa != null ? pa.PaidEuAmount ?? 0 : 0) - (ra != null ? ra.EuAmount ?? 0 : 0),
                    PaidBgAmount = (pa != null ? pa.PaidBgAmount ?? 0 : 0) - (ra != null ? ra.BgAmount ?? 0 : 0),
                    ContractedAmount = ca != null ? ca.TotalCurrentBfpAmount : 0,
                };

            var result = query.OrderBy(e => e.Year).Select(q => new PaidAmountsByYearVO
            {
                Year = q.Year,
                PaidEuAmount = q.PaidEuAmount,
                PaidBgAmount = q.PaidBgAmount,
                ContractedAmount = q.ContractedAmount,
            }).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                if (i == 0)
                {
                    continue;
                }

                result[i].PaidBgAmount += result[i - 1].PaidBgAmount;
                result[i].PaidEuAmount += result[i - 1].PaidEuAmount;

                if (result[i].ContractedAmount == 0)
                {
                    result[i].ContractedAmount = result[i - 1].ContractedAmount;
                }
            }

            var currentContract = contract.Single();

            var startYear = currentContract.StartDate.HasValue ? currentContract.StartDate.Value.Year : periods.First();
            var endYear = currentContract.CompletionDate.HasValue ? currentContract.CompletionDate.Value.Year : periods.Last();

            return result.Where(r => startYear <= r.Year && r.Year <= endYear).ToList();
        }

        public ProjectPageVO<ContractVO> GetContracts(
            int? startDateYearFrom = null,
            int? startDateYearTo = null,
            int? completionDateYearFrom = null,
            int? completionDateYearTo = null,
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            string companyUin = null,
            UinType? companyUinType = null,
            string contractorUin = null,
            UinType? contractorUinType = null,
            string subcontractorUin = null,
            UinType? subcontractorUinType = null,
            string memberUin = null,
            UinType? memberUinType = null,
            string partnerUin = null,
            UinType? partnerUinType = null,
            string searchUin = null,
            string searchName = null,
            NutsLevel? regionNutsLevel = null,
            int? regionId = null,
            int offset = 0,
            int? limit = null)
        {
            IQueryable<Contract> contracts;
            IQueryable<HistoricContract> historicContracts;

            if (procedureId != null)
            {
                if (programmeId != null)
                {
                    contracts = from c in this.unitOfWork.DbContext.Set<Contract>()
                                where c.ProcedureId == procedureId && c.ProgrammeId == programmeId
                                select c;

                    historicContracts = from hc in this.unitOfWork.DbContext.Set<HistoricContract>()
                                        join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hc.ProcedureId equals ps.ProcedureId
                                        where hc.ProcedureId == procedureId && ps.ProgrammeId == programmeId
                                        select hc;
                }
                else if (programmePriorityId != null)
                {
                    contracts = from c in this.unitOfWork.DbContext.Set<Contract>()
                                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                                join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on ps.ProgrammePriorityId equals mnr.MapNodeId
                                where c.ProgrammeId == mnr.ProgrammeId &&
                                    ps.ProgrammePriorityId == programmePriorityId &&
                                    ps.ProcedureId == procedureId
                                select c;

                    historicContracts = from hc in this.unitOfWork.DbContext.Set<HistoricContract>()
                                        join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hc.ProcedureId equals ps.ProcedureId
                                        where ps.ProgrammePriorityId == programmePriorityId &&
                                            hc.ProcedureId == procedureId
                                        select hc;
                }
                else
                {
                    contracts = from c in this.unitOfWork.DbContext.Set<Contract>()
                                where c.ProcedureId == procedureId
                                select c;

                    historicContracts = from hc in this.unitOfWork.DbContext.Set<HistoricContract>()
                                        where hc.ProcedureId == procedureId
                                        select hc;
                }
            }
            else if (programmePriorityId != null)
            {
                if (programmeId != null)
                {
                    contracts = from c in this.unitOfWork.DbContext.Set<Contract>()
                                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                                where c.ProgrammeId == programmeId &&
                                    ps.ProgrammePriorityId == programmePriorityId
                                select c;

                    historicContracts = from hc in this.unitOfWork.DbContext.Set<HistoricContract>()
                                        join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hc.ProcedureId equals ps.ProcedureId
                                        where ps.ProgrammeId == programmeId &&
                                            ps.ProgrammePriorityId == programmePriorityId
                                        select hc;
                }
                else
                {
                    contracts = from c in this.unitOfWork.DbContext.Set<Contract>()
                                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                                join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on ps.ProgrammePriorityId equals mnr.MapNodeId
                                where c.ProgrammeId == mnr.ProgrammeId &&
                                    ps.ProgrammePriorityId == programmePriorityId
                                select c;

                    historicContracts = from hc in this.unitOfWork.DbContext.Set<HistoricContract>()
                                        join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hc.ProcedureId equals ps.ProcedureId
                                        where ps.ProgrammePriorityId == programmePriorityId
                                        select hc;
                }
            }
            else if (programmeId != null)
            {
                contracts = from c in this.unitOfWork.DbContext.Set<Contract>()
                            where c.ProgrammeId == programmeId
                            select c;

                historicContracts = from hc in this.unitOfWork.DbContext.Set<HistoricContract>()
                                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hc.ProcedureId equals ps.ProcedureId
                                    where ps.ProgrammeId == programmeId
                                    select hc;
            }
            else
            {
                contracts = this.unitOfWork.DbContext.Set<Contract>();
                historicContracts = this.unitOfWork.DbContext.Set<HistoricContract>();
            }

            if (contractorUin != null)
            {
                var predicateCompany = PredicateBuilder.True<ContractContractor>()
                    .AndEquals(x => x.Uin, contractorUin)
                    .AndEquals(x => x.UinType, contractorUinType);

                contracts = from c in contracts
                            join cc in this.unitOfWork.DbContext.Set<ContractContractor>().Where(predicateCompany) on c.ContractId equals cc.ContractId
                            select c;

                var historicPredicateCompany = PredicateBuilder.True<HistoricContractPartner>()
                    .AndEquals(x => x.PartnerUin, contractorUin)
                    .AndEquals(x => x.PartnerUinType, contractorUinType)
                    .And(x => x.PartnerType == HistoricContractPartnerType.Contractor);

                historicContracts = from hc in historicContracts
                                    join hcp in this.unitOfWork.DbContext.Set<HistoricContractPartner>().Where(historicPredicateCompany) on hc.HistoricContractId equals hcp.HistoricContractId
                                    select hc;
            }

            if (subcontractorUin != null)
            {
                var predicateCompany = PredicateBuilder.True<ContractContractor>()
                    .AndEquals(x => x.Uin, subcontractorUin)
                    .AndEquals(x => x.UinType, subcontractorUinType);

                contracts = from c in contracts
                            join cc in this.unitOfWork.DbContext.Set<ContractContractor>().Where(predicateCompany) on c.ContractId equals cc.ContractId
                            join cs in this.unitOfWork.DbContext.Set<ContractSubcontract>().Where(x => x.Type == ContractSubcontractType.Subcontractor) on cc.ContractContractorId equals cs.ContractContractorId
                            select c;

                var historicPredicateCompany = PredicateBuilder.True<HistoricContractPartner>()
                    .AndEquals(x => x.PartnerUin, subcontractorUin)
                    .AndEquals(x => x.PartnerUinType, subcontractorUinType)
                    .And(x => x.PartnerType == HistoricContractPartnerType.Subcontractor);

                historicContracts = from hc in historicContracts
                                    join hcp in this.unitOfWork.DbContext.Set<HistoricContractPartner>().Where(historicPredicateCompany) on hc.HistoricContractId equals hcp.HistoricContractId
                                    select hc;
            }

            if (memberUin != null)
            {
                var predicateCompany = PredicateBuilder.True<ContractContractor>()
                    .AndEquals(x => x.Uin, memberUin)
                    .AndEquals(x => x.UinType, memberUinType);

                contracts = from c in contracts
                            join cc in this.unitOfWork.DbContext.Set<ContractContractor>().Where(predicateCompany) on c.ContractId equals cc.ContractId
                            join cs in this.unitOfWork.DbContext.Set<ContractSubcontract>().Where(x => x.Type == ContractSubcontractType.Member) on cc.ContractContractorId equals cs.ContractContractorId
                            select c;

                var historicPredicateCompany = PredicateBuilder.True<HistoricContractPartner>()
                    .AndEquals(x => x.PartnerUin, memberUin)
                    .AndEquals(x => x.PartnerUinType, memberUinType)
                    .And(x => x.PartnerType == HistoricContractPartnerType.Member);

                historicContracts = from hc in historicContracts
                                    join hcp in this.unitOfWork.DbContext.Set<HistoricContractPartner>().Where(historicPredicateCompany) on hc.HistoricContractId equals hcp.HistoricContractId
                                    select hc;
            }

            if (partnerUin != null)
            {
                var predicateCompany = PredicateBuilder.True<ContractPartner>()
                    .AndEquals(x => x.Uin, partnerUin)
                    .AndEquals(x => x.UinType, partnerUinType);

                contracts = from c in contracts
                            join cp in this.unitOfWork.DbContext.Set<ContractPartner>().Where(predicateCompany) on c.ContractId equals cp.ContractId
                            select c;

                var historicPredicateCompany = PredicateBuilder.True<HistoricContractPartner>()
                    .AndEquals(x => x.PartnerUin, partnerUin)
                    .AndEquals(x => x.PartnerUinType, partnerUinType)
                    .And(x => x.PartnerType == HistoricContractPartnerType.Partner);

                historicContracts = from hc in historicContracts
                                    join hcp in this.unitOfWork.DbContext.Set<HistoricContractPartner>().Where(historicPredicateCompany) on hc.HistoricContractId equals hcp.HistoricContractId
                                    select hc;
            }

            if (searchUin != null)
            {
                var contractUinPredicate = PredicateBuilder.False<Contract>()
                    .Or(c => c.CompanyUin.Contains(searchUin) && c.CompanyUinType != UinType.PersonalBulstat)
                    .Or(c => c.ContractContractors.Any(cc => cc.Uin.Contains(searchUin) && cc.UinType != UinType.PersonalBulstat))
                    .Or(c => c.ContractPartners.Any(cp => cp.Uin.Contains(searchUin) && cp.UinType != UinType.PersonalBulstat));

                var historicContractUinPredicate = PredicateBuilder.False<HistoricContract>()
                    .Or(c => c.CompanyUin.Contains(searchUin) && c.CompanyUinType != UinType.PersonalBulstat)
                    .Or(c => c.HistoricContractPartners.Any(cp => cp.PartnerUin.Contains(searchUin) && cp.PartnerUinType != UinType.PersonalBulstat));

                contracts = contracts.Where(contractUinPredicate);

                historicContracts = historicContracts.Where(historicContractUinPredicate);
            }

            if (searchName != null)
            {
                var contractNamePredicate = PredicateBuilder.True<Contract>();
                var historicContractNamePredicate = PredicateBuilder.True<HistoricContract>();

                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.Bulgarian)
                {
                    contractNamePredicate = contractNamePredicate.And(c => c.Name.Contains(searchName));
                    historicContractNamePredicate = historicContractNamePredicate.And(c => c.Name.Contains(searchName));
                }
                else
                {
                    contractNamePredicate = contractNamePredicate.And(c => c.NameEN.Contains(searchName));
                    historicContractNamePredicate = historicContractNamePredicate.And(c => c.NameEN.Contains(searchName));
                }

                contracts = contracts.Where(contractNamePredicate);

                historicContracts = historicContracts.Where(historicContractNamePredicate);
            }

            var predicate = PredicateBuilder.True<Contract>();
            var historicPredicate = PredicateBuilder.True<HistoricContract>();

            if (startDateYearFrom != null)
            {
                predicate = predicate.And(c => c.StartDate.Value.Year >= startDateYearFrom);
                historicPredicate = historicPredicate.And(c => c.StartDate.Value.Year >= startDateYearFrom);
            }

            if (startDateYearTo != null)
            {
                predicate = predicate.And(c => c.StartDate.Value.Year <= startDateYearTo);
                historicPredicate = historicPredicate.And(c => c.StartDate.Value.Year <= startDateYearTo);
            }

            if (completionDateYearFrom != null)
            {
                predicate = predicate.And(c => c.CompletionDate.Value.Year >= completionDateYearFrom);
                historicPredicate = historicPredicate.And(c => c.CompletionDate.Value.Year >= completionDateYearFrom);
            }

            if (completionDateYearTo != null)
            {
                predicate = predicate.And(c => c.CompletionDate.Value.Year <= completionDateYearTo);
                historicPredicate = historicPredicate.And(c => c.CompletionDate.Value.Year <= completionDateYearTo);
            }

            if (companyUin != null)
            {
                predicate = predicate.And(c => c.CompanyUin == companyUin);
                historicPredicate = historicPredicate.And(c => c.CompanyUin == companyUin);
            }

            if (companyUinType != null)
            {
                predicate = predicate.And(c => c.CompanyUinType == companyUinType);
                historicPredicate = historicPredicate.And(c => c.CompanyUinType == companyUinType);
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contracts = contracts.Where(predicate);
                historicContracts = historicContracts.Where(historicPredicate);
            }

            string fullPath = null;

            if (regionId != null)
            {
                if (regionId == Configuration.PR_INTERNATIONAL_ID)
                {
                    contracts = from c in contracts
                                join cl in this.unitOfWork.DbContext.Set<ContractLocation>() on c.ContractId equals cl.ContractId
                                where c.NutsLevel == NutsLevel.Country && cl.FullPath != Configuration.BULGARIA_COUNTRY_CODE
                                select c;

                    historicContracts = from hc in historicContracts
                                        join hcl in this.unitOfWork.DbContext.Set<HistoricContractLocation>() on hc.HistoricContractId equals hcl.HistoricContractId
                                        where hc.NutsLevel == NutsLevel.Country && hcl.CountryCode != Configuration.BULGARIA_COUNTRY_CODE
                                        select hc;
                }
                else if (regionNutsLevel != null)
                {
                    if (regionNutsLevel.Value == NutsLevel.Country)
                    {
                        fullPath = this.unitOfWork.DbContext.Set<Country>().First(e => e.CountryId.Equals(regionId.Value)).NutsCode;
                    }
                    else if (regionNutsLevel.Value == NutsLevel.RegionNUTS1)
                    {
                        fullPath = this.unitOfWork.DbContext.Set<Nuts1>().First(e => e.Nuts1Id.Equals(regionId.Value)).FullPath;
                    }
                    else if (regionNutsLevel.Value == NutsLevel.RegionNUTS2)
                    {
                        fullPath = this.unitOfWork.DbContext.Set<Nuts2>().First(e => e.Nuts2Id.Equals(regionId.Value)).FullPath;
                    }
                    else if (regionNutsLevel.Value == NutsLevel.District)
                    {
                        fullPath = this.unitOfWork.DbContext.Set<District>().First(e => e.DistrictId.Equals(regionId.Value)).FullPath;
                    }
                    else if (regionNutsLevel.Value == NutsLevel.Municipality)
                    {
                        fullPath = this.unitOfWork.DbContext.Set<Municipality>().First(e => e.MunicipalityId.Equals(regionId.Value)).FullPath;
                    }
                    else if (regionNutsLevel.Value == NutsLevel.Settlement)
                    {
                        fullPath = this.unitOfWork.DbContext.Set<Settlement>().First(e => e.SettlementId.Equals(regionId.Value)).FullPath;
                    }
                    else if (regionNutsLevel.Value == NutsLevel.ProtectedZone)
                    {
                        fullPath = this.unitOfWork.DbContext.Set<ProtectedZone>().First(e => e.ProtectedZoneId.Equals(regionId.Value)).FullPath;
                    }

                    if (!string.IsNullOrWhiteSpace(fullPath))
                    {
                        contracts = from c in contracts
                                    join cl in this.unitOfWork.DbContext.Set<ContractLocation>() on c.ContractId equals cl.ContractId
                                    where cl.FullPath.Contains(fullPath)
                                    select c;

                        historicContracts = from hc in historicContracts
                                            join hcl in this.unitOfWork.DbContext.Set<HistoricContractLocation>() on hc.HistoricContractId equals hcl.HistoricContractId
                                            where hcl.FullPath.Contains(fullPath)
                                            select hc;
                    }
                }
            }

            var reimbursedAmounts =
                from c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate)
                 join ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>().Where(cra => cra.Status == ReimbursedAmountStatus.Entered && ReportsReimbursements.Contains(cra.Reimbursement)) on c.ContractId equals ra.ContractId into j1
                 from ra in j1.DefaultIfEmpty()
                 group new
                 {
                     EuAmount = ra.PrincipalBfp.EuAmount,
                     BgAmount = ra.PrincipalBfp.BgAmount,
                 }
                by c.ContractId into g
                 select new
                 {
                     ContractId = g.Key,
                     EuAmount = g.Sum(i => i.EuAmount),
                     BgAmount = g.Sum(i => i.BgAmount),
                 };

            var paidAmounts =
                from c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate)
                 join pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(pa => pa.Status == ActuallyPaidAmountStatus.Entered) on c.ContractId equals pa.ContractId into j1
                 from pa in j1.DefaultIfEmpty()
                 group new
                 {
                     PaidEuAmount = pa.PaidBfpEuAmount,
                     PaidBgAmount = pa.PaidBfpBgAmount,
                 }
                by c.ContractId into g
                 select new
                 {
                     ContractId = g.Key,
                     PaidEuAmount = g.Sum(i => i.PaidEuAmount),
                     PaidBgAmount = g.Sum(i => i.PaidBgAmount),
                 };

            var budgetAmountsPredicate = PredicateBuilder.True<ContractBudgetLevel3Amount>()
                .And(b => b.IsActive);

            if (fullPath != null)
            {
                budgetAmountsPredicate = budgetAmountsPredicate.And(b => b.NutsFullPath.Contains(fullPath));
            }

            var budgetAmounts =
                from c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate)
                 join cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(budgetAmountsPredicate) on c.ContractId equals cba.ContractId into j1
                 from cba in j1.DefaultIfEmpty()
                 group new
                 {
                     ContractedEuAmount = (decimal?)cba.CurrentEuAmount,
                     ContractedBgAmount = (decimal?)cba.CurrentBgAmount,
                     ContractedSelfAmount = (decimal?)cba.CurrentSelfAmount,
                 }
                by c.ContractId into g
                 select new
                 {
                     ContractId = g.Key,
                     ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                     ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                     ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                 };

            var contractLocations =
                from c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate)
                 join cl in this.unitOfWork.DbContext.Set<ContractLocation>() on c.ContractId equals cl.ContractId into j1
                 from cl in j1.DefaultIfEmpty()
                 group new
                 {
                     NutsFullPathName = cl.Name,
                     NutsFullPathNameEN = cl.NameAlt,
                 }
                by c.ContractId into g
                 select new
                 {
                     ContractId = g.Key,
                     NutsFullPathNames = g.Select(e => e.NutsFullPathName),
                     NutsFullPathNamesEn = g.Select(e => e.NutsFullPathNameEN),
                 };

            var oldTimeout = this.unitOfWork.DbContext.Database.CommandTimeout;
            this.unitOfWork.DbContext.Database.CommandTimeout = 60 * 10;

            var query =
                (from c in contracts.GroupBy(x => x.ContractId).Select(y => y.FirstOrDefault())

                 join cba in budgetAmounts on c.ContractId equals cba.ContractId into j1
                 from cba in j1.DefaultIfEmpty()

                 join pa in paidAmounts on c.ContractId equals pa.ContractId into j2
                 from pa in j2.DefaultIfEmpty()

                 join ra in reimbursedAmounts on c.ContractId equals ra.ContractId into j6
                 from ra in j6.DefaultIfEmpty()

                 join cl in contractLocations on c.ContractId equals cl.ContractId into j3
                 from cl in j3.DefaultIfEmpty()

                 join country in this.unitOfWork.DbContext.Set<Country>() on c.BeneficiarySeatCountryId equals country.CountryId into j4
                 from country in j4.DefaultIfEmpty()

                 join settlement in this.unitOfWork.DbContext.Set<Settlement>() on c.BeneficiarySeatSettlementId equals settlement.SettlementId into j5
                 from settlement in j5.DefaultIfEmpty()
                 where c.ContractStatus == ContractStatus.Entered
                 select new
                 {
                     ContractId = c.ContractId,
                     Name = c.Name,
                     NameEN = c.NameEN,
                     CompanyName = c.CompanyName,
                     CompanyNameAlt = c.CompanyNameAlt,
                     CompanyUin = c.CompanyUin,
                     CompanyUinType = c.CompanyUinType,
                     StartDate = c.StartDate,
                     CompletionDate = c.CompletionDate,
                     BeneficiarySeatCountryId = c.BeneficiarySeatCountryId ?? 0,
                     BeneficiarySeatCountry = country.Name,
                     BeneficiarySeatCountryAlt = country.NameAlt,
                     BeneficiarySeatSettlementId = c.BeneficiarySeatSettlementId ?? 0,
                     BeneficiarySeatSettlement = settlement.Name,
                     BeneficiarySeatSettlementAlt = settlement.NameAlt,
                     BeneficiarySeatAddress = c.BeneficiarySeatAddress,
                     BeneficiarySeatPostCode = c.BeneficiarySeatPostCode,
                     BeneficiarySeatStreet = c.BeneficiarySeatStreet,
                     NutsLevel = c.NutsLevel,
                     RegNumber = c.RegNumber,
                     ExecutionStatus = c.ExecutionStatus,
                     NutsFullPathNames = cl.NutsFullPathNames.Distinct(),
                     NutsFullPathNamesEN = cl.NutsFullPathNamesEn.Distinct(),
                     ContractedEuAmount = cba.ContractedEuAmount,
                     ContractedBgAmount = cba.ContractedBgAmount,
                     ContractedSelfAmount = cba.ContractedSelfAmount,
                     PaidEuAmount = (pa.PaidEuAmount ?? 0m) - (ra.EuAmount ?? 0m),
                     PaidBgAmount = (pa.PaidBgAmount ?? 0m) - (ra.BgAmount ?? 0m),
                     IsHistoric = false,
                 }).ToList();

            this.unitOfWork.DbContext.Database.CommandTimeout = oldTimeout;

            var historicReimbursedAmounts =
                from hc in this.unitOfWork.DbContext.Set<HistoricContract>().Where(historicPredicate)
                 join ra in this.unitOfWork.DbContext.Set<HistoricContractReimbursedAmount>() on hc.HistoricContractId equals ra.HistoricContractId into g
                 from hcra in g.DefaultIfEmpty()
                 group new
                 {
                     EuAmount = hcra.ReimbursedPrincipalEuAmount,
                     BgAmount = hcra.ReimbursedPrincipalBgAmount,
                 }
                by hcra.HistoricContractId into g
                 select new
                 {
                     HistoricContractId = g.Key,
                     EuAmount = g.Sum(i => i.EuAmount),
                     BgAmount = g.Sum(i => i.BgAmount),
                 };

            var historicPaidAmounts =
                from hc in this.unitOfWork.DbContext.Set<HistoricContract>().Where(historicPredicate)
                 join pa in this.unitOfWork.DbContext.Set<HistoricContractActuallyPaidAmount>() on hc.HistoricContractId equals pa.HistoricContractId into g
                 from hcpa in g.DefaultIfEmpty()
                 group new
                 {
                     hcpa.PaidEuAmount,
                     hcpa.PaidBgAmount,
                 }
                 by hc.HistoricContractId into g
                 select new
                 {
                     HistoricContractId = g.Key,
                     PaidEuAmount = g.Sum(i => i.PaidEuAmount),
                     PaidBgAmount = g.Sum(i => i.PaidBgAmount),
                 };

            var historicBudgetAmounts =
                from hc in this.unitOfWork.DbContext.Set<HistoricContract>().Where(historicPredicate)
                 join ca in this.unitOfWork.DbContext.Set<HistoricContractContractedAmount>() on hc.HistoricContractId equals ca.HistoricContractId into g
                 from hcca in g.DefaultIfEmpty()
                 where hcca.IsLast == true
                 group new
                 {
                     ContractedEuAmount = (decimal?)hcca.ContractedEuAmount,
                     ContractedBgAmount = (decimal?)hcca.ContractedBgAmount,
                     ContractedSelfAmount = (decimal?)hcca.ContractedSeftAmount,
                 }
                by hcca.HistoricContractId into g
                 select new
                 {
                     HistoricContractId = g.Key,
                     ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                     ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                     ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                 };

            var historicContractLocations = from n in from hc in this.unitOfWork.DbContext.Set<HistoricContract>().Where(historicPredicate)
                                                       join hcl in this.unitOfWork.DbContext.Set<HistoricContractLocation>() on hc.HistoricContractId equals hcl.HistoricContractId into j1
                                                       from hcl in j1.DefaultIfEmpty()

                                                       join c in this.unitOfWork.DbContext.Set<Country>() on hcl.CountryCode equals c.NutsCode into j2
                                                       from c in j2.DefaultIfEmpty()

                                                       join pz in this.unitOfWork.DbContext.Set<ProtectedZone>() on hcl.ProtectedZoneCode equals pz.NutsCode into j3
                                                       from pz in j3.DefaultIfEmpty()

                                                       join n1 in this.unitOfWork.DbContext.Set<Nuts1>() on hcl.Nuts1Code equals n1.NutsCode into j4
                                                       from n1 in j4.DefaultIfEmpty()

                                                       join n2 in this.unitOfWork.DbContext.Set<Nuts2>() on hcl.Nuts2Code equals n2.NutsCode into j5
                                                       from n2 in j5.DefaultIfEmpty()

                                                       join d in this.unitOfWork.DbContext.Set<District>() on hcl.DistrictCode equals d.NutsCode into j6
                                                       from d in j6.DefaultIfEmpty()

                                                       join m in this.unitOfWork.DbContext.Set<Municipality>() on hcl.MunicipalityCode equals m.LauCode into j7
                                                       from m in j7.DefaultIfEmpty()

                                                       join s in this.unitOfWork.DbContext.Set<Settlement>() on hcl.SettlementCode equals s.LauCode into j8
                                                       from s in j8.DefaultIfEmpty()
                                                       select new
                                                       {
                                                           HistoricContractId = hc.HistoricContractId,
                                                           Name = s.Name ?? m.Name ?? d.Name ?? n2.Name ?? n1.Name ?? pz.Name ?? c.Name,
                                                           NameEN = s.NameAlt ?? m.NameAlt ?? d.NameAlt ?? n2.NameAlt ?? n1.NameAlt ?? pz.NameAlt ?? c.NameAlt,
                                                       }
                                            group new
                                            {
                                                NutsFullPathName = n.Name,
                                                NutsFullPathNameEN = n.NameEN,
                                            }
                                            by n.HistoricContractId into g
                                            select new
                                            {
                                                HistoricContractId = g.Key,
                                                NutsFullPathNames = g.Select(e => e.NutsFullPathName),
                                                NutsFullPathNamesEN = g.Select(e => e.NutsFullPathNameEN),
                                            };

            var historicQuery =
                (from hc in historicContracts.GroupBy(x => x.HistoricContractId).Select(y => y.FirstOrDefault())

                 join hba in historicBudgetAmounts on hc.HistoricContractId equals hba.HistoricContractId into j1
                 from hba in j1.DefaultIfEmpty()

                 join hpa in historicPaidAmounts on hc.HistoricContractId equals hpa.HistoricContractId into j2
                 from hpa in j2.DefaultIfEmpty()

                 join hra in historicReimbursedAmounts on hc.HistoricContractId equals hra.HistoricContractId into j3
                 from hra in j3.DefaultIfEmpty()

                 join hcl in historicContractLocations.AsEnumerable() on hc.HistoricContractId equals hcl.HistoricContractId into j4
                 from hcl in j4.DefaultIfEmpty()

                 join country in this.unitOfWork.DbContext.Set<Country>() on hc.SeatCountryCode equals country.NutsCode into j5
                 from country in j5.DefaultIfEmpty()

                 join settlement in this.unitOfWork.DbContext.Set<Settlement>() on hc.SeatSettlementCode equals settlement.LauCode into j6
                 from settlement in j6.DefaultIfEmpty()
                 select new
                 {
                     ContractId = hc.HistoricContractId,
                     Name = hc.Name,
                     NameEN = hc.NameEN,
                     CompanyName = hc.CompanyName,
                     CompanyNameAlt = hc.CompanyNameEn,
                     CompanyUin = hc.CompanyUin,
                     CompanyUinType = hc.CompanyUinType,
                     StartDate = hc.StartDate,
                     CompletionDate = hc.CompletionDate,
                     BeneficiarySeatCountryId = country.CountryId,
                     BeneficiarySeatCountry = country.Name,
                     BeneficiarySeatCountryAlt = country.NameAlt,
                     BeneficiarySeatSettlementId = settlement.SettlementId,
                     BeneficiarySeatSettlement = settlement.Name,
                     BeneficiarySeatSettlementAlt = settlement.NameAlt,
                     BeneficiarySeatAddress = hc.SeatAddress,
                     BeneficiarySeatPostCode = hc.SeatPostCode,
                     BeneficiarySeatStreet = hc.SeatStreet,
                     NutsLevel = hc.NutsLevel,
                     RegNumber = hc.RegNumber,
                     ExecutionStatus = hc.ExecutionStatus,
                     NutsFullPathNames = hcl.NutsFullPathNames.Distinct(),
                     NutsFullPathNamesEN = hcl.NutsFullPathNamesEN.Distinct(),
                     ContractedEuAmount = hba.ContractedEuAmount,
                     ContractedBgAmount = hba.ContractedBgAmount,
                     ContractedSelfAmount = hba.ContractedSelfAmount,
                     PaidEuAmount = (hpa.PaidEuAmount ?? 0m) - (hra.EuAmount ?? 0m),
                     PaidBgAmount = (hpa.PaidBgAmount ?? 0m) - (hra.BgAmount ?? 0m),
                     IsHistoric = true,
                 }).ToList();

            var queryWithOffsetAndLimit = query
                .Union(historicQuery)
                .OrderBy(c => c.CompanyName)
                .WithOffsetAndLimit(offset, limit);

            var results = queryWithOffsetAndLimit.AsEnumerable();

            return new ProjectPageVO<ContractVO>()
            {
                Results =
                    results
                    .Select(c => new ContractVO
                    {
                        ContractId = c.ContractId,
                        Name = c.Name,
                        NameEN = c.NameEN,
                        CompanyName = c.CompanyName,
                        CompanyNameAlt = c.CompanyNameAlt,
                        CompanyUin = c.CompanyUin,
                        CompanyUinType = c.CompanyUinType,
                        StartDate = c.StartDate,
                        CompletionDate = c.CompletionDate,
                        BeneficiarySeatCountryId = c.BeneficiarySeatCountryId,
                        BeneficiarySeatCountry = c.BeneficiarySeatCountry,
                        BeneficiarySeatCountryAlt = c.BeneficiarySeatCountryAlt,
                        BeneficiarySeatSettlementId = c.BeneficiarySeatSettlementId,
                        BeneficiarySeatSettlement = c.BeneficiarySeatSettlement,
                        BeneficiarySeatSettlementAlt = c.BeneficiarySeatSettlementAlt,
                        BeneficiarySeatAddress = c.BeneficiarySeatAddress,
                        BeneficiarySeatPostCode = c.BeneficiarySeatPostCode,
                        BeneficiarySeatStreet = c.BeneficiarySeatStreet,
                        NutsLevel = c.NutsLevel,
                        NutsFullPathNames = c.NutsFullPathNames.Where(e => !string.IsNullOrWhiteSpace(e)),
                        NutsFullPathNamesEN = c.NutsFullPathNamesEN.Where(e => !string.IsNullOrWhiteSpace(e)),
                        RegNumber = c.RegNumber,
                        ExecutionStatus = c.ExecutionStatus,
                        ContractedEuAmount = c.ContractedEuAmount ?? 0m,
                        ContractedBgAmount = c.ContractedBgAmount ?? 0m,
                        ContractedSelfAmount = c.ContractedSelfAmount ?? 0m,
                        PaidEuAmount = c.PaidEuAmount,
                        PaidBgAmount = c.PaidBgAmount,
                        IsHistoric = c.IsHistoric,
                    })
                    .ToList(),
                ProjectsSummarizedData = new ProjectsSummarizedDataVO(query
                .Union(historicQuery)
                .OrderBy(c => c.CompanyName)
                .Select(c => new ContractVO
                {
                    CompanyUin = c.CompanyUin,
                    ContractedEuAmount = c.ContractedEuAmount ?? 0m,
                    ContractedBgAmount = c.ContractedBgAmount ?? 0m,
                    ContractedSelfAmount = c.ContractedSelfAmount ?? 0m,
                }).ToList()),
                Count = query.Count(),
            };
        }

        public PageVO<StatisticContractVO> GetStatisticContracts(
            DateTime? startDateFrom = null,
            DateTime? completionDateTo = null,
            int? programmeId = null,
            int offset = 0,
            int? limit = null)
        {
            IQueryable<Contract> contracts;

            if (programmeId != null)
            {
                contracts = from c in this.unitOfWork.DbContext.Set<Contract>()
                            where c.ProgrammeId == programmeId
                            select c;
            }
            else
            {
                contracts = from c in this.unitOfWork.DbContext.Set<Contract>()
                            select c;
            }

            var predicate = PredicateBuilder.True<Contract>();

            if (startDateFrom != null)
            {
                predicate = predicate.And(c => c.StartDate.Value >= startDateFrom);
            }

            if (completionDateTo != null)
            {
                predicate = predicate.And(c => c.CompletionDate.HasValue && c.CompletionDate.Value <= completionDateTo);
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contracts = contracts.Where(predicate);
            }

            var budgets =
                from cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>()
                 join c in contracts on cba.ContractId equals c.ContractId
                 join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cba.ProcedureBudgetLevel2Id equals l2.ProcedureBudgetLevel2Id

                 where l2.IsEligibleCost && cba.IsActive && c.ContractStatus == ContractStatus.Entered

                 group new
                 {
                     ContractedEuAmount = cba.CurrentEuAmount,
                     ContractedBgAmount = cba.CurrentBgAmount,
                     ContractedSelfAmount = cba.CurrentSelfAmount,
                 }
                by cba.ContractId into g
                 select new
                 {
                     ContractId = g.Key,

                     ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                     ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                     ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                 };

            var query =
                from c in contracts

                join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals cv.ContractId

                join cba in budgets on c.ContractId equals cba.ContractId into j1
                from cba in j1.DefaultIfEmpty()

                where c.ContractStatus == ContractStatus.Entered && cv.Status == ContractVersionStatus.Active
                select new
                {
                    ContractId = c.ContractId,
                    Name = c.Name,
                    NameEN = c.NameEN,
                    CompanyName = c.CompanyName,
                    CompanyNameAlt = c.CompanyNameAlt,
                    CompanyUin = c.CompanyUin,
                    CompanyUinType = c.CompanyUinType,
                    StartDate = c.StartDate,
                    CompletionDate = c.CompletionDate,
                    Description = c.Description,
                    DescriptionEN = c.DescriptionEN,

                    ModifyDate = cv.ModifyDate,

                    ContractedEuAmount = (decimal?)cba.ContractedEuAmount,
                    ContractedBgAmount = (decimal?)cba.ContractedBgAmount,
                    ContractedSelfAmount = (decimal?)cba.ContractedSelfAmount,
                };

            var results =
                query
                .OrderBy(c => c.CompanyName)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
            var contractIds = results.Select(e => e.ContractId);

            var locationDict = this.unitOfWork.DbContext.Set<ContractLocation>()
                .Where(e => contractIds.Contains(e.ContractId))
                .AsEnumerable()
                .GroupBy(e => e.ContractId)
                .ToDictionary(g => g.Key, g => g.ToList());
            var contractIcsDict = (from cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>()
                                   join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cba.ProcedureBudgetLevel2Id equals l2.ProcedureBudgetLevel2Id

                                   where l2.IsEligibleCost && contractIds.Contains(cba.ContractId)
                                   select new
                                   {
                                       cba.ContractId,

                                       cba.InterventionFieldId,
                                       cba.FormOfFinanceId,
                                       cba.TerritorialDimensionId,
                                       cba.TerritorialDeliveryMechanismId,
                                       cba.ThematicObjectiveId,
                                       cba.ESFSecondaryThemeId,
                                       cba.EconomicDimensionId,
                                   })
                .ToList()
                .GroupBy(e => e.ContractId)
                .ToDictionary(g => g.Key, g =>
                {
                    var interventionFieldIds = g.Select(e => e.InterventionFieldId);
                    var formOfFinanceIds = g.Select(e => e.FormOfFinanceId);
                    var territorialDimensionIds = g.Select(e => e.TerritorialDimensionId);
                    var territorialDeliveryMechanismIds = g.Select(e => e.TerritorialDeliveryMechanismId);
                    var thematicObjectiveIds = g.Select(e => e.ThematicObjectiveId);
                    var eSFSecondaryThemeIds = g.Select(e => e.ESFSecondaryThemeId);
                    var economicDimensionIds = g.Select(e => e.EconomicDimensionId);

                    return interventionFieldIds
                        .Concat(formOfFinanceIds)
                        .Concat(territorialDimensionIds)
                        .Concat(territorialDeliveryMechanismIds)
                        .Concat(thematicObjectiveIds)
                        .Concat(eSFSecondaryThemeIds)
                        .Concat(economicDimensionIds).Distinct();
                });

            var ics = this.unitOfWork.DbContext.Set<InterventionCategory>().ToList();

            return new PageVO<StatisticContractVO>()
            {
                Results =
                    results
                    .Select(c =>
                    {
                        var icsIds = contractIcsDict[c.ContractId];

                        var categories = ics.Where(t => icsIds.Contains(t.InterventionCategoryId));

                        return new StatisticContractVO
                        {
                            ContractId = c.ContractId,
                            Name = c.Name,
                            NameEN = c.NameEN,
                            CompanyName = c.CompanyName,
                            CompanyNameAlt = c.CompanyNameAlt,
                            CompanyUin = c.CompanyUin,
                            CompanyUinType = c.CompanyUinType,
                            StartDate = c.StartDate,
                            CompletionDate = c.CompletionDate,
                            ModifyDate = c.ModifyDate,
                            ContractedEuAmount = c.ContractedEuAmount ?? 0m,
                            ContractedBgAmount = c.ContractedBgAmount ?? 0m,
                            ContractedSelfAmount = c.ContractedSelfAmount ?? 0m,

                            Description = c.Description,
                            DescriptionEN = c.DescriptionEN,

                            NutsFullPathNames = locationDict.ContainsKey(c.ContractId) ? locationDict[c.ContractId].Select(e => e.Name) : null,
                            NutsFullPathNamesAlt = locationDict.ContainsKey(c.ContractId) ? locationDict[c.ContractId].Select(e => e.NameAlt) : null,

                            InterventionCategories = categories,
                        };
                    })
                    .ToList(),
                Count = query.Count(),
            };
        }

        public PageVO<Operations508ReportVO> GetOperations508Report(
            DateTime? startDateFrom = null,
            DateTime? completionDateTo = null,
            int? programmeId = null,
            int offset = 0,
            int? limit = null)
        {
            IQueryable<Contract> contracts;

            if (programmeId != null)
            {
                contracts = from c in this.unitOfWork.DbContext.Set<Contract>()
                            where c.ProgrammeId == programmeId
                            select c;
            }
            else
            {
                contracts = from c in this.unitOfWork.DbContext.Set<Contract>()
                            select c;
            }

            var predicate = PredicateBuilder.True<Contract>();

            if (startDateFrom != null)
            {
                predicate = predicate.And(c => c.StartDate.Value >= startDateFrom);
            }

            if (completionDateTo != null)
            {
                predicate = predicate.And(c => c.CompletionDate.HasValue && c.CompletionDate.Value <= completionDateTo);
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contracts = contracts.Where(predicate);
            }

            var budgets =
                from cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>()
                join c in contracts on cba.ContractId equals c.ContractId
                join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cba.ProcedureBudgetLevel2Id equals l2.ProcedureBudgetLevel2Id

                where l2.IsEligibleCost && cba.IsActive && c.ContractStatus == ContractStatus.Entered

                group new
                {
                    ContractedEuAmount = cba.CurrentEuAmount,
                    ContractedBgAmount = cba.CurrentBgAmount,
                    ContractedSelfAmount = cba.CurrentSelfAmount,
                }
               by cba.ContractId into g
                select new
                {
                    ContractId = g.Key,

                    ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                    ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                    ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                };

            var query =
                from c in contracts

                join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals cv.ContractId
                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(e => e.IsPrimary) on c.ProcedureId equals ps.ProcedureId
                join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId

                join cba in budgets on c.ContractId equals cba.ContractId into j1
                from cba in j1.DefaultIfEmpty()

                join cntr in this.unitOfWork.DbContext.Set<Country>() on c.BeneficiarySeatCountryId equals cntr.CountryId into j2
                from cntr in j2.DefaultIfEmpty()

                where c.ContractStatus == ContractStatus.Entered && cv.Status == ContractVersionStatus.Active
                select new
                {
                    ContractId = c.ContractId,
                    Name = c.Name,
                    NameEN = c.NameEN,
                    CompanyName = c.CompanyName,
                    CompanyNameAlt = c.CompanyNameAlt,
                    CompanyUin = c.CompanyUin,
                    CompanyUinType = c.CompanyUinType,
                    StartDate = c.StartDate,
                    CompletionDate = c.CompletionDate,
                    Description = c.Description,
                    DescriptionEN = c.DescriptionEN,
                    BeneficiarySeatPostCode = c.BeneficiarySeatPostCode,
                    BeneficiarySeatCountry = cntr.Name,
                    BeneficiarySeatCountryAlt = cntr.NameAlt,
                    ProgrammePriorityName = pp.Name,
                    ProgrammePriorityNameAlt = pp.NameAlt,

                    ModifyDate = cv.ModifyDate,

                    ContractedEuAmount = (decimal?)cba.ContractedEuAmount,
                    ContractedBgAmount = (decimal?)cba.ContractedBgAmount,
                    ContractedSelfAmount = (decimal?)cba.ContractedSelfAmount,
                };

            var results =
                query
                .OrderBy(c => c.CompanyName)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            var contractIds = results.Select(e => e.ContractId);

            var vesselIdentifiers = this.unitOfWork.DbContext.Set<ContractExtensionVesselIdentifier>()
                .Where(e => contractIds.Contains(e.ContractId))
                .GroupBy(e => e.ContractId)
                .ToDictionary(g => g.Key, g => g.Select(e => e.Value));

            return new PageVO<Operations508ReportVO>()
            {
                Results =
                    results
                    .Select(c =>
                    {
                        vesselIdentifiers.TryGetValue(c.ContractId, out var vesselIds);

                        return new Operations508ReportVO
                        {
                            ContractId = c.ContractId,
                            Name = c.Name,
                            NameEN = c.NameEN,
                            CompanyName = c.CompanyName,
                            CompanyNameAlt = c.CompanyNameAlt,
                            CompanyUin = c.CompanyUin,
                            CompanyUinType = c.CompanyUinType,
                            StartDate = c.StartDate,
                            CompletionDate = c.CompletionDate,
                            ModifyDate = c.ModifyDate,
                            ContractedEuAmount = c.ContractedEuAmount ?? 0m,
                            ContractedBgAmount = c.ContractedBgAmount ?? 0m,
                            ContractedSelfAmount = c.ContractedSelfAmount ?? 0m,

                            Description = c.Description,
                            DescriptionEN = c.DescriptionEN,
                            PostCode = c.BeneficiarySeatPostCode,
                            CountryName = c.BeneficiarySeatCountry,
                            CountryNameAlt = c.BeneficiarySeatCountryAlt,
                            ProgrammePriorityName = c.ProgrammePriorityName,
                            ProgrammePriorityNameAlt = c.ProgrammePriorityNameAlt,

                            VesselIdentifiers = vesselIds ?? Enumerable.Empty<string>(),
                        };
                    })
                    .ToList(),
                Count = query.Count(),
            };
        }

        public PageVO<StatisticIndicatorVO> GetStatisticIndicators(
            int? programmeId = null,
            int offset = 0,
            int? limit = null,
            bool isEn = false)
        {
            var indPredicate = PredicateBuilder.True<VwMonitoringMapNodeIndicator>()
                .AndEquals(i => i.ProgrammeId, programmeId);

            var query =
                from mnri in this.unitOfWork.DbContext.Set<VwMonitoringMapNodeIndicator>().Where(indPredicate)
                 join ind in this.unitOfWork.DbContext.Set<Indicator>() on mnri.IndicatorId equals ind.IndicatorId
                 join m in this.unitOfWork.DbContext.Set<Measure>() on ind.MeasureId equals m.MeasureId
                 join p in this.unitOfWork.DbContext.Set<Programme>() on mnri.ProgrammeId equals p.MapNodeId
                 orderby ind.Name
                 select new
                 {
                     IndicatorId = mnri.IndicatorId,
                     Name = ind.Name,
                     NameAlt = ind.NameAlt,

                     ProgrammeId = p.MapNodeId,
                     ProgrammeShortName = p.ShortName,
                     ProgrammeShortNameAlt = p.PortalShortNameAlt,

                     IndicatorType = ind.Type,
                     IndicatorKind = ind.Kind,
                     IndicatorTrend = ind.Trend,
                     MeasuerName = m.Name,
                     MeasuerNameAlt = m.NameAlt,
                     AggregatedReport = ind.AggregatedReport,
                     AggregatedTarget = ind.AggregatedTarget,

                     BaseTotalValue = mnri.BaseTotalValue,
                     TargetTotalValue = mnri.TargetTotalValue,
                 };

            var sortedQuery = query;

            if (isEn)
            {
                sortedQuery = sortedQuery
                    .OrderBy(e => e.ProgrammeShortNameAlt)
                    .ThenBy(e => e.NameAlt);
            }
            else
            {
                sortedQuery = sortedQuery
                    .OrderBy(e => e.ProgrammeShortName)
                    .ThenBy(e => e.Name);
            }

            var count = sortedQuery.Count();

            var indicators = sortedQuery
                .WithOffsetAndLimit(offset, limit).ToList();

            var indicatorIds = indicators.Select(ind => ind.IndicatorId);
            var programmeIds = indicators.Select(ind => ind.ProgrammeId);

            // TODO: add the reported amounts to the VwMonitoringMapNodeIndicator view
            var reportedAmounts =
            (from cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>()
             join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on cri.ContractIndicatorId equals ci.ContractIndicatorId
             join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>() on cri.ContractReportTechnicalId equals crt.ContractReportTechnicalId
             join cr in this.unitOfWork.DbContext.Set<ContractReport>() on cri.ContractReportId equals cr.ContractReportId
             join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
             where indicatorIds.Contains(ci.IndicatorId) && programmeIds.Contains(c.ProgrammeId) && cri.Status == ContractReportIndicatorStatus.Ended && crt.Status == ContractReportTechnicalStatus.Actual && cr.Status == ContractReportStatus.Accepted
             group new
             {
                 ci.IndicatorId,
                 c.ProgrammeId,
                 cr.DateTo,
                 cri.ApprovedCumulativeAmountTotal,
             }
             by new
             {
                 ci.IndicatorId,
                 c.ContractId,
                 c.ProgrammeId,
                 c.ProcedureId,
             }
             into g0
             select g0.OrderByDescending(x => x.DateTo).FirstOrDefault() into r
             select new
             {
                 r.ProgrammeId,
                 r.IndicatorId,
                 r.ApprovedCumulativeAmountTotal,
             }).ToList();

            return new PageVO<StatisticIndicatorVO>()
            {
                Results =
                    indicators
                    .Select(i =>
                        {
                            var reportedAmount = reportedAmounts.Where(a => a.IndicatorId == i.IndicatorId && a.ProgrammeId == i.ProgrammeId)
                                .Select(a => a.ApprovedCumulativeAmountTotal);
                            return new StatisticIndicatorVO
                            {
                                IndicatorId = i.IndicatorId,
                                Name = i.Name,
                                NameAlt = i.NameAlt,

                                ProgrammeShortName = i.ProgrammeShortName,
                                ProgrammeShortNameAlt = i.ProgrammeShortNameAlt,

                                IndicatorType = i.IndicatorType,
                                IndicatorKind = i.IndicatorKind,
                                IndicatorTrend = i.IndicatorTrend,
                                MeasuerName = i.MeasuerName,
                                MeasuerNameAlt = i.MeasuerNameAlt,
                                AggregatedReport = i.AggregatedReport,
                                AggregatedTarget = i.AggregatedTarget,

                                BaseTotalValue = i.BaseTotalValue ?? 0m,
                                TargetTotalValue = i.TargetTotalValue ?? 0m,

                                ApprovedPeriodAmountTotal = reportedAmount.Sum() ?? 0m,
                            };
                        })
                    .ToList(),
                Count = count,
            };
        }

        public ProjectProposalWrapperVO GetStatisticProjects(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            int offset = 0,
            int? limit = null)
        {
            // Procedure
            // join Project where ProjectRegistrationStatus == Registered || RegisteredLate and ProjectEvalStatus == Contracted
            //     get: RequestedFundingAmount + (CoFinancingPublicAmount + CoFinancingPrivateAmount)
            // left join EvalSessionProjectStandings where not IsDeleted and Status == N'Статус: 1 - Одобрено, 2 - Резерва, 3 - Отхвърлено, 4 - Отхвърлено на ОАСД, 5 - Отхвърлено на ТФО'
            var procedureSharePredicate = PredicateBuilder.True<ProcedureShare>()
                .AndEquals(ps => ps.ProgrammeId, programmeId)
                .AndEquals(ps => ps.ProgrammePriorityId, programmePriorityId)
                .AndEquals(ps => ps.ProcedureId, procedureId);

            var procedureSharesSubquery = from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(procedureSharePredicate)
                                          select ps.ProcedureId;

            var procedureVersionsSubquery =
                from pv in this.unitOfWork.DbContext.Set<ProcedureVersion>()
                 group pv by pv.ProcedureId into g
                 select g.Key;

            var evalSessionProjectStandings = from esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>()
                                               join es in this.unitOfWork.DbContext.Set<EvalSession>() on esps.EvalSessionId equals es.EvalSessionId
                                               where esps.IsDeleted == false && esps.IsPreliminary == false && es.EvalSessionStatus == EvalSessionStatus.Ended
                                               select esps;

            var projects = (from p in this.unitOfWork.DbContext.Set<Procedure>()

                            join pj in this.unitOfWork.DbContext.Set<Project>().Where(t => t.RegistrationStatus != ProjectRegistrationStatus.Withdrawn) on p.ProcedureId equals pj.ProcedureId into j0
                            from pj in j0.DefaultIfEmpty()

                            join esps in evalSessionProjectStandings on pj.ProjectId equals esps.ProjectId into j1
                            from esps in j1.DefaultIfEmpty()

                            where procedureSharesSubquery.Contains(p.ProcedureId) && procedureVersionsSubquery.Contains(p.ProcedureId) && p.ProcedureStatus != ProcedureStatus.Canceled

                            group new
                            {
                                esps.Status,
                                pj.TotalBfpAmount,
                                pj.CoFinancingAmount,
                                ProjectId = (int?)pj.ProjectId,
                            }
                            by new
                            {
                                p.ProcedureId,
                                p.Code,
                                p.Name,
                                p.NameAlt,
                            }
                                into g
                            select new ProjectProposalVO
                            {
                                ProcedureId = g.Key.ProcedureId,
                                Code = g.Key.Code,
                                Name = g.Key.Name,
                                NameAlt = g.Key.NameAlt,
                                ProjectCount = g.Count(t => t.ProjectId.HasValue),
                                BfpAmount = g.Sum(i => i.TotalBfpAmount ?? 0),
                                SFAmount = g.Sum(i => i.CoFinancingAmount ?? 0),
                                ApprovedCount = g.Count(e => e.Status == EvalSessionProjectStandingStatus.Approved),
                                ReserveCount = g.Count(e => e.Status == EvalSessionProjectStandingStatus.Reserve),
                                RejectedCount = g.Count(e =>
                                e.Status == EvalSessionProjectStandingStatus.Rejected ||
                                e.Status == EvalSessionProjectStandingStatus.RejectedAtASD ||
                                e.Status == EvalSessionProjectStandingStatus.RejectedAtTFO ||
                                e.Status == EvalSessionProjectStandingStatus.RejectedAtPO),
                            })
                    .OrderBy(e => e.Name)
                    .ToList();

            ProjectProposalWrapperVO result = new ProjectProposalWrapperVO();

            result.Totals = new ProjectProposalVO()
            {
                ProjectCount = projects.Sum(e => e.ProjectCount),
                BfpAmount = projects.Sum(e => e.BfpAmount),
                SFAmount = projects.Sum(e => e.SFAmount),
                ApprovedCount = projects.Sum(e => e.ApprovedCount),
                ReserveCount = projects.Sum(e => e.ReserveCount),
                RejectedCount = projects.Sum(e => e.RejectedCount),
            };

            var projectsWithOffsetAndLimit = projects
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            result.PageResults = new PageVO<ProjectProposalVO>() { Count = projects.Count(), Results = projectsWithOffsetAndLimit };

            return result;
        }

        public ContractDetailsVO GetContract(
            int contractId,
            bool isHistoric)
        {
            if (isHistoric)
            {
                IQueryable<HistoricContract> historicContracts =
                    from hc in this.unitOfWork.DbContext.Set<HistoricContract>()
                    where hc.HistoricContractId == contractId
                    select hc;

                var reimbursedAmounts = from hc in historicContracts
                                         join hra in this.unitOfWork.DbContext.Set<HistoricContractReimbursedAmount>() on hc.HistoricContractId equals hra.HistoricContractId into j1
                                         from hra in j1.DefaultIfEmpty()
                                         group new
                                         {
                                             EuAmount = hra.ReimbursedPrincipalEuAmount,
                                             BgAmount = hra.ReimbursedPrincipalBgAmount,
                                         }
                                        by hc.HistoricContractId into g
                                         select new
                                         {
                                             HistoricContractId = g.Key,
                                             EuAmount = g.Sum(i => i.EuAmount),
                                             BgAmount = g.Sum(i => i.BgAmount),
                                         };

                var paidAmounts = from hc in historicContracts
                                   join hpa in this.unitOfWork.DbContext.Set<HistoricContractActuallyPaidAmount>() on hc.HistoricContractId equals hpa.HistoricContractId into j1
                                   from hpa in j1.DefaultIfEmpty()
                                   group new
                                   {
                                       PaidEuAmount = hpa.PaidEuAmount,
                                       PaidBgAmount = hpa.PaidBgAmount,
                                   }
                                    by hc.HistoricContractId into g
                                   select new
                                   {
                                       HistoricContractId = g.Key,
                                       PaidEuAmount = g.Sum(i => i.PaidEuAmount),
                                       PaidBgAmount = g.Sum(i => i.PaidBgAmount),
                                   };

                var query =
                    from hc in historicContracts

                    join hcba in this.unitOfWork.DbContext.Set<HistoricContractContractedAmount>().Where(cba => cba.IsLast) on hc.HistoricContractId equals hcba.HistoricContractId into j1
                    from hcba in j1.DefaultIfEmpty()

                    join hpa in paidAmounts on hc.HistoricContractId equals hpa.HistoricContractId into j2
                    from hpa in j2.DefaultIfEmpty()

                    join hra in reimbursedAmounts on hc.HistoricContractId equals hra.HistoricContractId into j3
                    from hra in j3.DefaultIfEmpty()

                    join country in this.unitOfWork.DbContext.Set<Country>() on hc.SeatCountryCode equals country.NutsCode into j4
                    from country in j4.DefaultIfEmpty()

                    join settlement in this.unitOfWork.DbContext.Set<Settlement>() on hc.SeatSettlementCode equals settlement.LauCode into j5
                    from settlement in j5.DefaultIfEmpty()

                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on hc.ProcedureId equals ps.ProcedureId

                    join p in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals p.MapNodeId

                    where ps.IsPrimary
                    group new
                    {
                        ContractedEuAmount = (decimal?)hcba.ContractedEuAmount,
                        ContractedBgAmount = (decimal?)hcba.ContractedBgAmount,
                        ContractedSelfAmount = (decimal?)hcba.ContractedSeftAmount,
                    }
                    by new
                    {
                        hc.HistoricContractId,
                        hc.CompanyName,
                        hc.CompanyNameEn,
                        hc.CompanyUin,
                        hc.CompanyUinType,
                        hc.Name,
                        hc.NameEN,
                        hc.Description,
                        hc.DescriptionEN,
                        hc.ContractDate,
                        hc.StartDate,
                        hc.CompletionDate,
                        country.CountryId,
                        settlement.SettlementId,
                        hc.SeatAddress,
                        hc.SeatPostCode,
                        hc.SeatStreet,
                        hc.NutsLevel,
                        hc.RegNumber,
                        hc.ExecutionStatus,
                        hc.ProcedureId,
                        ps.ProgrammeId,
                        ProgrammeName = p.PortalName,
                        ProgrammeNameEN = p.PortalNameAlt,
                        PaidEuAmount = hpa.PaidEuAmount,
                        PaidBgAmount = hpa.PaidBgAmount,
                        ReimbursedEuAmount = hra.EuAmount,
                        ReimbursedBgAmount = hra.BgAmount,
                        EuAmount = ps.EuAmount,
                        BgAmount = ps.BgAmount,
                    }
                    into g
                    select new
                    {
                        ContractId = g.Key.HistoricContractId,
                        Name = g.Key.Name,
                        NameEN = g.Key.NameEN,
                        Description = g.Key.Description,
                        DescriptionEN = g.Key.DescriptionEN,
                        CompanyName = g.Key.CompanyName,
                        CompanyNameAlt = g.Key.CompanyNameEn,
                        CompanyUin = g.Key.CompanyUin,
                        CompanyUinType = g.Key.CompanyUinType,
                        ContractDate = g.Key.ContractDate,
                        StartDate = g.Key.StartDate,
                        CompletionDate = g.Key.CompletionDate,
                        BeneficiarySeatCountryId = g.Key.CountryId,
                        BeneficiarySeatSettlementId = g.Key.SettlementId,
                        BeneficiarySeatAddress = g.Key.SeatAddress,
                        BeneficiarySeatPostCode = g.Key.SeatPostCode,
                        BeneficiarySeatStreet = g.Key.SeatStreet,
                        NutsLevel = g.Key.NutsLevel,
                        RegNumber = g.Key.RegNumber,
                        ExecutionStatus = g.Key.ExecutionStatus,
                        ProcedureId = g.Key.ProcedureId,

                        ProgrammeId = g.Key.ProgrammeId,
                        ProgrammeName = g.Key.ProgrammeName,
                        ProgrammeNameEN = g.Key.ProgrammeNameEN,

                        ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                        ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                        ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                        PaidEuAmount = (g.Key.PaidEuAmount ?? 0m) - (g.Key.ReimbursedEuAmount ?? 0m),
                        PaidBgAmount = (g.Key.PaidBgAmount ?? 0m) - (g.Key.ReimbursedBgAmount ?? 0m),

                        EuAmount = g.Key.EuAmount,
                        BgAmount = g.Key.BgAmount,
                    };

                var contract = query.Single();
                var settlements = from s in this.unitOfWork.DbContext.Set<Settlement>()
                                  select new { FullPathName = s.FullPathNameAlt, Code = s.FullPath };
                var municipalities = from m in this.unitOfWork.DbContext.Set<Municipality>()
                                     select new { FullPathName = m.FullPathNameAlt, Code = m.FullPath };
                var districts = from d in this.unitOfWork.DbContext.Set<District>()
                                select new { FullPathName = d.FullPathNameAlt, Code = d.FullPath };
                var nuts2s = from s in this.unitOfWork.DbContext.Set<Nuts2>()
                             select new { FullPathName = s.FullPathNameAlt, Code = s.FullPath };
                var nuts1s = from m in this.unitOfWork.DbContext.Set<Nuts1>()
                             select new { FullPathName = m.FullPathNameAlt, Code = m.FullPath };
                var protectedZones = from d in this.unitOfWork.DbContext.Set<ProtectedZone>()
                                     select new { FullPathName = d.FullPathNameAlt, Code = d.FullPath };
                var countries = from d in this.unitOfWork.DbContext.Set<Country>()
                                select new { FullPathName = d.NameAlt, Code = d.NutsCode };

                var fullPathNamesAlt = settlements
                    .Union(municipalities)
                    .Union(districts)
                    .Union(nuts2s)
                    .Union(nuts1s)
                    .Union(protectedZones)
                    .Union(countries);

                var locations = from l in this.unitOfWork.DbContext.Set<HistoricContractLocation>().Where(e => e.HistoricContractId == contractId)
                                 join fpna in fullPathNamesAlt on l.FullPath equals fpna.Code
                                 select new
                                 {
                                     FullPathName = l.FullPathName,
                                     FullPathNameAlt = fpna.FullPathName,
                                 };

                var activities = this.unitOfWork.DbContext.Set<HistoricContractActivity>().Where(e => e.HistoricContractId == contractId).AsEnumerable();

                var partners = this.unitOfWork.DbContext.Set<HistoricContractPartner>().Where(e => e.PartnerType == HistoricContractPartnerType.Partner && e.HistoricContractId == contractId).AsEnumerable();

                var contractors = this.unitOfWork.DbContext.Set<HistoricContractPartner>().Where(e => e.PartnerType == HistoricContractPartnerType.Contractor && e.HistoricContractId == contractId).AsEnumerable();

                var subcontractors = this.unitOfWork.DbContext.Set<HistoricContractPartner>().Where(e => e.HistoricContractId == contractId).AsEnumerable();

                var offers =
                    (from hcppp in this.unitOfWork.DbContext.Set<HistoricContractProcurementPlan>()

                     where hcppp.HistoricContractId == contractId

                     select new
                     {
                         PositionNames = hcppp.HistoricContractProcurementPlanPositions.Select(hp => hp.PositionName),
                         ProcurementPlanName = hcppp.ProcurementPlanName,
                         Amount = hcppp.Amount,
                     }).AsEnumerable();
                IEnumerable<string> funds = this.unitOfWork.DbContext.Set<ProcedureShare>()
                    .Where(e => e.ProcedureId == contract.ProcedureId && e.ProgrammeId == contract.ProgrammeId)
                    .Select(e => e.FinanceSource).Distinct().AsEnumerable().Select(e => e.GetEnumDescription());
                return new ContractDetailsVO()
                {
                    ContractId = contract.ContractId,
                    RegNumber = contract.RegNumber,
                    Name = contract.Name,
                    NameEN = contract.NameEN,
                    Description = contract.Description,
                    DescriptionEN = contract.DescriptionEN,
                    CompanyName = contract.CompanyName,
                    CompanyNameAlt = contract.CompanyNameAlt,
                    CompanyUin = contract.CompanyUin,
                    CompanyUinType = contract.CompanyUinType,

                    Funds = funds,

                    ProgrammeId = contract.ProgrammeId,
                    ProgrammeName = contract.ProgrammeName,
                    ProgrammeNameEN = contract.ProgrammeNameEN,
                    ContractDate = contract.ContractDate,
                    StartDate = contract.StartDate,
                    CompletionDate = contract.CompletionDate,
                    ExecutionStatus = contract.ExecutionStatus,

                    NutsFullPathNames = locations.Where(e => e.FullPathName != null && e.FullPathName != string.Empty).Select(e => e.FullPathName).Distinct(),
                    NutsFullPathNamesEN = locations.Where(e => e.FullPathNameAlt != null && e.FullPathNameAlt != string.Empty).Select(e => e.FullPathNameAlt).Distinct(),
                    Activities = activities.Select(e => new ContractActivityVO { Title = e.Activity }),

                    Partners = partners.Select(e => new ContractPartnerVO { Name = e.PartnerName, NameAlt = e.PartnerNameEn, Uin = e.PartnerUin, UinType = e.PartnerUinType }),
                    Contractors = contractors.Select(e => new ContractContractorVO() { Name = e.PartnerName, NameAlt = e.PartnerNameEn, Uin = e.PartnerUin, UinType = e.PartnerUinType }),
                    Subcontractors = subcontractors.Where(e => e.PartnerType == HistoricContractPartnerType.Subcontractor).Select(e => new ContractSubcontractorVO() { Name = e.PartnerName, NameAlt = e.PartnerNameEn, Uin = e.PartnerUin, UinType = e.PartnerUinType }),
                    Members = subcontractors.Where(e => e.PartnerType == HistoricContractPartnerType.Member).Select(e => new ContractSubcontractorVO() { Name = e.PartnerName, NameAlt = e.PartnerNameEn, Uin = e.PartnerUin, UinType = e.PartnerUinType }),

                    ContractedEuAmount = contract.ContractedEuAmount ?? 0m,
                    ContractedBgAmount = contract.ContractedBgAmount ?? 0m,
                    ContractedSelfAmount = contract.ContractedSelfAmount ?? 0m,
                    PaidEuAmount = contract.PaidEuAmount,
                    PaidBgAmount = contract.PaidBgAmount,

                    Indicators = Enumerable.Empty<ContractIndicatorVO>(),

                    Offers = offers.Select(e => new OfferVO()
                    {
                        ContractDifferentiatedPositions = e.PositionNames.Select(n => new ContractDifferentiatedPositionVO
                        {
                            Name = n,
                        }).ToList(),
                        ProcurementPlanName = e.ProcurementPlanName,
                        Amount = e.Amount,
                    }),

                    ProcedureShareBgAmount = contract.BgAmount,
                    ProcedureShareEuAmount = contract.EuAmount,

                    IsHistoric = true,
                };
            }
            else
            {
                IQueryable<Contract> contracts =
                    from c in this.unitOfWork.DbContext.Set<Contract>()
                    where c.ContractId == contractId
                    select c;

                var reimbursedAmounts = from c in contracts
                                         join ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>().Where(cra => cra.Status == ReimbursedAmountStatus.Entered && ReportsReimbursements.Contains(cra.Reimbursement)) on c.ContractId equals ra.ContractId into j1
                                         from ra in j1.DefaultIfEmpty()
                                         group new
                                         {
                                             EuAmount = ra.PrincipalBfp.EuAmount,
                                             BgAmount = ra.PrincipalBfp.BgAmount,
                                         }
                                        by c.ContractId into g
                                         select new
                                         {
                                             ContractId = g.Key,
                                             EuAmount = g.Sum(i => i.EuAmount),
                                             BgAmount = g.Sum(i => i.BgAmount),
                                         };

                var paidAmounts = from c in contracts
                                   join pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(pa => pa.Status == ActuallyPaidAmountStatus.Entered) on c.ContractId equals pa.ContractId into j1
                                   from pa in j1.DefaultIfEmpty()
                                   group new
                                   {
                                       PaidEuAmount = pa.PaidBfpEuAmount,
                                       PaidBgAmount = pa.PaidBfpBgAmount,
                                   }
                                    by c.ContractId into g
                                   select new
                                   {
                                       ContractId = g.Key,
                                       PaidEuAmount = g.Sum(i => i.PaidEuAmount),
                                       PaidBgAmount = g.Sum(i => i.PaidBgAmount),
                                   };

                var query =
                    from c in contracts

                    join cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(cba1 => cba1.IsActive) on c.ContractId equals cba.ContractId into j1
                    from cba in j1.DefaultIfEmpty()

                    join pa in paidAmounts on c.ContractId equals pa.ContractId into j2
                    from pa in j2.DefaultIfEmpty()

                    join ra in reimbursedAmounts on c.ContractId equals ra.ContractId into j3
                    from ra in j3.DefaultIfEmpty()

                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId

                    join p in this.unitOfWork.DbContext.Set<Programme>() on c.ProgrammeId equals p.MapNodeId

                    where c.ContractStatus == ContractStatus.Entered && ps.IsPrimary
                    group new
                    {
                        ContractedEuAmount = (decimal?)cba.CurrentEuAmount,
                        ContractedBgAmount = (decimal?)cba.CurrentBgAmount,
                        ContractedSelfAmount = (decimal?)cba.CurrentSelfAmount,
                    }
                    by new
                    {
                        c.ContractId,
                        c.CompanyName,
                        c.CompanyNameAlt,
                        c.CompanyUin,
                        c.CompanyUinType,
                        c.Name,
                        c.NameEN,
                        c.Description,
                        c.DescriptionEN,
                        c.ContractDate,
                        c.StartDate,
                        c.CompletionDate,
                        c.BeneficiarySeatCountryId,
                        c.BeneficiarySeatSettlementId,
                        c.BeneficiarySeatAddress,
                        c.BeneficiarySeatPostCode,
                        c.BeneficiarySeatStreet,
                        c.NutsLevel,
                        c.RegNumber,
                        c.ExecutionStatus,
                        c.ProcedureId,
                        c.ProgrammeId,
                        ProgrammeName = p.PortalName,
                        ProgrammeNameEN = p.PortalNameAlt,
                        PaidEuAmount = pa.PaidEuAmount,
                        PaidBgAmount = pa.PaidBgAmount,
                        ReimbursedEuAmount = ra.EuAmount,
                        ReimbursedBgAmount = ra.BgAmount,
                        EuAmount = ps.EuAmount,
                        BgAmount = ps.BgAmount,
                    }
                    into g
                    select new
                    {
                        ContractId = g.Key.ContractId,
                        Name = g.Key.Name,
                        NameEN = g.Key.NameEN,
                        Description = g.Key.Description,
                        DescriptionEN = g.Key.DescriptionEN,
                        CompanyName = g.Key.CompanyName,
                        CompanyNameAlt = g.Key.CompanyNameAlt,
                        CompanyUin = g.Key.CompanyUin,
                        CompanyUinType = g.Key.CompanyUinType,
                        ContractDate = g.Key.ContractDate,
                        StartDate = g.Key.StartDate,
                        CompletionDate = g.Key.CompletionDate,
                        BeneficiarySeatCountryId = g.Key.BeneficiarySeatCountryId,
                        BeneficiarySeatSettlementId = g.Key.BeneficiarySeatSettlementId,
                        BeneficiarySeatAddress = g.Key.BeneficiarySeatAddress,
                        BeneficiarySeatPostCode = g.Key.BeneficiarySeatPostCode,
                        BeneficiarySeatStreet = g.Key.BeneficiarySeatStreet,
                        NutsLevel = g.Key.NutsLevel,
                        RegNumber = g.Key.RegNumber,
                        ExecutionStatus = g.Key.ExecutionStatus,
                        ProcedureId = g.Key.ProcedureId,

                        ProgrammeId = g.Key.ProgrammeId,
                        ProgrammeName = g.Key.ProgrammeName,
                        ProgrammeNameEN = g.Key.ProgrammeNameEN,

                        ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                        ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                        ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                        PaidEuAmount = (g.Key.PaidEuAmount ?? 0m) - (g.Key.ReimbursedEuAmount ?? 0m),
                        PaidBgAmount = (g.Key.PaidBgAmount ?? 0m) - (g.Key.ReimbursedBgAmount ?? 0m),

                        EuAmount = g.Key.EuAmount,
                        BgAmount = g.Key.BgAmount,
                    };

                var contract = query.Single();
                var locations = this.unitOfWork.DbContext.Set<ContractLocation>().Where(e => e.ContractId == contractId).AsEnumerable();

                var acceptedcsdbi =
                    from csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on csdbi.ContractReportId equals cr.ContractReportId
                    where cr.Status == ContractReportStatus.Accepted
                    select csdbi;

                var activities =
                    from act in this.unitOfWork.DbContext.Set<ContractActivity>()

                    join csdbi in acceptedcsdbi.Where(e => e.ContractId == contractId) on act.Gid equals csdbi.ContractActivityGid into gcsdbi
                    from csdbi in gcsdbi.DefaultIfEmpty()

                    where act.ContractId == contractId

                    group new
                    {
                        CSDTotalAmount = (decimal?)csdbi.TotalAmount,
                    }
                    by new
                    {
                        act.ContractActivityId,
                        act.Code,
                        act.Name,
                        act.Amount,
                    }
                    into g
                    select new
                    {
                        g.Key.Code,
                        g.Key.Name,
                        g.Key.Amount,
                        TotalReportedAmount = g.Sum(p => p.CSDTotalAmount),
                    };

                var partners =
                    from p in this.unitOfWork.DbContext.Set<ContractPartner>().Where(e => e.ContractId == contractId)

                    join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>()
                    .Where(e => e.ContractId == contractId && e.CompanyType == CostSupportingDocumentCompanyType.Partner) on p.Gid equals csd.CompanyGid into gcsd
                    from csd in gcsd.DefaultIfEmpty()

                    join csdbi in acceptedcsdbi on csd.ContractReportFinancialCSDId equals csdbi.ContractReportFinancialCSDId into gcsdbi
                    from csdbi in gcsdbi.DefaultIfEmpty()

                    group new
                    {
                        p.FinancialContribution,
                        CsdbiTotalAmount = (decimal?)csdbi.TotalAmount,
                    }
                    by new
                    {
                        p.Gid,
                        p.Name,
                        p.NameAlt,
                        p.Uin,
                        p.UinType,
                    }
                    into g
                    select new
                    {
                        g.Key.Gid,
                        g.Key.Name,
                        g.Key.NameAlt,
                        g.Key.Uin,
                        g.Key.UinType,
                        FinancialContribution = g.Sum(p => p.FinancialContribution),
                        CsdbiTotalAmount = g.Sum(p => p.CsdbiTotalAmount),
                    };

                var contractors1 =
                    from cctor in this.unitOfWork.DbContext.Set<ContractContractor>()
                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on cctor.ContractContractorId equals cc.ContractContractorId
                    where cctor.ContractId == contractId
                    select new
                    {
                        cctor.Gid,
                        cctor.Name,
                        cctor.NameAlt,
                        cctor.Uin,
                        cctor.UinType,
                        ContractTotalFundedValue = cc.TotalFundedValue,
                        CsdbiTotalAmount = 0m,
                    };

                var contractors2 =
                    from cctor in this.unitOfWork.DbContext.Set<ContractContractor>()
                    join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on cctor.Gid equals csd.CompanyGid
                    join csdbi in acceptedcsdbi on csd.ContractReportFinancialCSDId equals csdbi.ContractReportFinancialCSDId
                    where csd.ContractId == contractId && csd.CompanyType == CostSupportingDocumentCompanyType.Contractor
                    select new
                    {
                        cctor.Gid,
                        cctor.Name,
                        cctor.NameAlt,
                        cctor.Uin,
                        cctor.UinType,
                        ContractTotalFundedValue = 0m,
                        CsdbiTotalAmount = csdbi.TotalAmount,
                    };

                var contractors =
                    from c in contractors1.Concat(contractors2)
                    group new
                    {
                        c.ContractTotalFundedValue,
                        c.CsdbiTotalAmount,
                    }
                    by new
                    {
                        c.Gid,
                        c.Name,
                        c.NameAlt,
                        c.Uin,
                        c.UinType,
                    }
                    into g
                    select new
                    {
                        g.Key.Gid,
                        g.Key.Name,
                        g.Key.NameAlt,
                        g.Key.Uin,
                        g.Key.UinType,
                        ContractTotalFundedValue = g.Sum(p => p.ContractTotalFundedValue),
                        CsdbiTotalAmount = g.Sum(p => p.CsdbiTotalAmount),
                    };

                var subcontractors =
                    from cs in this.unitOfWork.DbContext.Set<ContractSubcontract>()
                    join ccr in this.unitOfWork.DbContext.Set<ContractContractor>() on cs.ContractContractorId equals ccr.ContractContractorId

                    where ccr.ContractId == contractId

                    group new
                    {
                        cs.Amount,
                    }

                    by new
                    {
                        cs.Type,
                        ccr.Name,
                        ccr.NameAlt,
                        ccr.Uin,
                        ccr.UinType,
                    }

                    into g

                    select new
                    {
                        g.Key.Type,
                        g.Key.Name,
                        g.Key.NameAlt,
                        g.Key.Uin,
                        g.Key.UinType,
                        TotalAmount = g.Sum(p => p.Amount),
                    };

                var lastContractReportTechnicalId =
                    (from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(p => p.ContractId == contractId && p.Status == ContractReportStatus.Accepted)
                     join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>().Where(p => p.Status == ContractReportTechnicalStatus.Actual) on cr.ContractReportId equals crt.ContractReportId
                     orderby crt.VersionNum descending
                     select (int?)crt.ContractReportTechnicalId).FirstOrDefault();

                var actualContractReportTechnicalCorrectionIndicators =
                    from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                    join tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>() on tci.ContractReportTechnicalCorrectionId equals tc.ContractReportTechnicalCorrectionId
                    where tc.Status == ContractReportTechnicalCorrectionStatus.Ended
                    select tci;

                var indicators =
                    (from i in this.unitOfWork.DbContext.Set<Indicator>()
                     join m in this.unitOfWork.DbContext.Set<Measure>() on i.MeasureId equals m.MeasureId
                     join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on i.IndicatorId equals ci.IndicatorId
                     join cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>().Where(p => p.ContractReportTechnicalId == lastContractReportTechnicalId) on ci.ContractIndicatorId equals cri.ContractIndicatorId into j1
                     from cri in j1.DefaultIfEmpty()

                     join tci in actualContractReportTechnicalCorrectionIndicators on cri.ContractReportIndicatorId equals tci.ContractReportIndicatorId into g0
                     from tci in g0.DefaultIfEmpty()

                     where ci.ContractId == contractId

                     select new
                     {
                         Name = i.Name,
                         NameAlt = i.NameAlt,
                         MeasureShortName = m.ShortName,
                         MeasureNameAlt = m.NameAlt,
                         ci.BaseTotalValue,
                         TargetAmount = (decimal?)ci.TargetTotalValue,
                         ApprovedCumulativeAmountTotal = tci == null ? cri.ApprovedCumulativeAmountTotal : tci.CorrectedApprovedCumulativeAmountTotal,
                     }).AsEnumerable();

                var offers =
                    from cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>()

                    join p in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on cpp.ContractProcurementPlanId equals p.ContractProcurementPlanId into gp
                    from p in gp.DefaultIfEmpty()

                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on p.ContractContractId equals cc.ContractContractId into gcc
                    from cc in gcc.DefaultIfEmpty()

                    join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into gcctor
                    from cctor in gcctor.DefaultIfEmpty()

                    where cpp.ContractId == contractId
                    group new
                    {
                        HasContractDifferentiatedPosition = p != null,
                        PositionName = p.Name,
                        ContractorName = cctor.Name,
                        TotalFundedValue = (decimal?)cc.TotalFundedValue,
                    }
                    by new
                    {
                        ProcurementPlanName = cpp.Name,
                        cpp.Amount,
                    }
                    into g
                    select new
                    {
                        Positions = g.Where(p => p.HasContractDifferentiatedPosition).Select(p => new
                        {
                            p.PositionName,
                            p.ContractorName,
                            p.TotalFundedValue,
                        }),
                        g.Key.ProcurementPlanName,
                        g.Key.Amount,
                    };

                var financialCorrections =
                    from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                    join fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>() on fc.FinancialCorrectionId equals fcv.FinancialCorrectionId

                    join ir in this.unitOfWork.DbContext.Set<FinancialCorrectionImposingReason>() on fcv.FinancialCorrectionImposingReasonId equals ir.FinancialCorrectionImposingReasonId into gir
                    from ir in gir.DefaultIfEmpty()

                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on fc.ContractContractId equals cc.ContractContractId into gcc
                    from cc in gcc.DefaultIfEmpty()

                    join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into gcctor
                    from cctor in gcctor.DefaultIfEmpty()

                    where fc.ContractId == contractId && fc.Status == FinancialCorrectionStatus.Entered && fcv.Status == FinancialCorrectionVersionStatus.Actual
                    select new
                    {
                        ImposingReasonName = ir.Name,
                        fcv.Percent,
                        fcv.BfpAmount,
                        fcv.SelfAmount,
                        fcv.TotalAmount,
                        ContractorName = cctor.Name,
                    };

                IEnumerable<string> funds = this.unitOfWork.DbContext.Set<ProcedureShare>()
                    .Where(e => e.ProcedureId == contract.ProcedureId && e.ProgrammeId == contract.ProgrammeId)
                    .Select(e => e.FinanceSource).Distinct().AsEnumerable().Select(e => e.GetEnumDescription());
                return new ContractDetailsVO()
                {
                    ContractId = contract.ContractId,
                    RegNumber = contract.RegNumber,
                    Name = contract.Name,
                    NameEN = contract.NameEN,
                    Description = contract.Description,
                    DescriptionEN = contract.DescriptionEN,
                    CompanyName = contract.CompanyName,
                    CompanyNameAlt = contract.CompanyNameAlt,
                    CompanyUin = contract.CompanyUin,
                    CompanyUinType = contract.CompanyUinType,

                    Funds = funds,

                    ProgrammeId = contract.ProgrammeId,
                    ProgrammeName = contract.ProgrammeName,
                    ProgrammeNameEN = contract.ProgrammeNameEN,
                    ContractDate = contract.ContractDate,
                    StartDate = contract.StartDate,
                    CompletionDate = contract.CompletionDate,
                    ExecutionStatus = contract.ExecutionStatus,

                    NutsFullPathNames = locations.Where(e => e.FullPathName != null && e.FullPathName != string.Empty).Select(e => e.FullPathName).Distinct(),
                    NutsFullPathNamesEN = locations.Where(e => e.FullPathNameAlt != null && e.FullPathNameAlt != string.Empty).Select(e => e.FullPathNameAlt).Distinct(),

                    Activities = activities.Select(e => new ContractActivityVO
                    {
                        Title = e.Code + ": " + e.Name,
                        TotalAmount = e.Amount,
                        TotalReportedAmount = e.TotalReportedAmount ?? 0,
                    }).ToList(),

                    Partners = partners.Select(e => new ContractPartnerVO
                    {
                        Name = e.Name,
                        NameAlt = e.NameAlt,
                        Uin = e.Uin,
                        UinType = e.UinType,
                        TotalFinancialContribution = e.FinancialContribution,
                        TotalReportedAmount = e.CsdbiTotalAmount ?? 0,
                    }).ToList(),

                    Contractors = contractors.Select(e => new ContractContractorVO()
                    {
                        Name = e.Name,
                        NameAlt = e.NameAlt,
                        Uin = e.Uin,
                        UinType = e.UinType,
                        TotalContractedAmount = e.ContractTotalFundedValue,
                        TotalReportedAmount = e.CsdbiTotalAmount,
                    }).ToList(),

                    Subcontractors = subcontractors.Where(e => e.Type == ContractSubcontractType.Subcontractor).Select(e => new ContractSubcontractorVO()
                    {
                        Name = e.Name,
                        NameAlt = e.NameAlt,
                        Uin = e.Uin,
                        UinType = e.UinType,
                        TotalContractedAmount = e.TotalAmount,
                    }).ToList(),

                    Members = subcontractors.Where(e => e.Type == ContractSubcontractType.Member).Select(e => new ContractSubcontractorVO()
                    {
                        Name = e.Name,
                        NameAlt = e.NameAlt,
                        Uin = e.Uin,
                        UinType = e.UinType,
                        TotalContractedAmount = e.TotalAmount,
                    }).ToList(),

                    ContractedEuAmount = contract.ContractedEuAmount ?? 0m,
                    ContractedBgAmount = contract.ContractedBgAmount ?? 0m,
                    ContractedSelfAmount = contract.ContractedSelfAmount ?? 0m,
                    PaidEuAmount = contract.PaidEuAmount,
                    PaidBgAmount = contract.PaidBgAmount,

                    Indicators = indicators.Select(e => new ContractIndicatorVO()
                    {
                        Name = e.Name,
                        NameAlt = e.NameAlt,
                        MeasureShortName = e.MeasureShortName,
                        MeasureNameAlt = e.MeasureNameAlt,
                        BaseTotalValue = e.BaseTotalValue ?? 0,
                        TargetAmount = e.TargetAmount ?? 0m,
                        CumulativeAmountTotal = e.ApprovedCumulativeAmountTotal ?? 0m,
                    }),

                    FinancialCorrections = financialCorrections.Select(fc => new FinancialCorrectionVO
                    {
                        ImposingReason = fc.ImposingReasonName,
                        Percent = fc.Percent,
                        BfpAmount = fc.BfpAmount,
                        SelfAmount = fc.SelfAmount,
                        TotalAmount = fc.TotalAmount,
                        ContractorName = fc.ContractorName,
                    }),

                    Offers = offers.Select(e => new OfferVO()
                    {
                        ProcurementPlanName = e.ProcurementPlanName,
                        Amount = e.Amount,
                        ContractDifferentiatedPositions = e.Positions.Select(p => new ContractDifferentiatedPositionVO
                        {
                            Name = p.PositionName,
                            ContractorName = p.ContractorName,
                            TotalFundedValue = p.TotalFundedValue,
                        }).ToList(),
                    }).ToList(),

                    ProcedureShareBgAmount = contract.BgAmount,
                    ProcedureShareEuAmount = contract.EuAmount,

                    IsHistoric = false,
                };
            }
        }

        public PageVO<ContractBeneficiaryVO> GetContractBeneficiaries(
            string name = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null,
            string uin = null,
            int offset = 0,
            int? limit = null)
        {
            IQueryable<Contract> contracts = this.unitOfWork.DbContext.Set<Contract>();
            IQueryable<HistoricContract> historicContracts = this.unitOfWork.DbContext.Set<HistoricContract>();

            var predicate = PredicateBuilder.True<Contract>();
            var historicPredicate = PredicateBuilder.True<HistoricContract>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.Bulgarian)
                {
                    predicate = predicate.And(c => c.CompanyName.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.CompanyName.Contains(name));
                }
                else
                {
                    predicate = predicate.And(c => c.CompanyNameAlt.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.CompanyNameEn.Contains(name));
                }
            }

            if (companyTypeId != null)
            {
                predicate = predicate.And(c => c.CompanyTypeId == companyTypeId);
                historicPredicate = historicPredicate.And(c => c.CompanyTypeId == companyTypeId);
            }

            if (companyLegalTypeId != null)
            {
                predicate = predicate.And(c => c.CompanyLegalTypeId == companyLegalTypeId);
                historicPredicate = historicPredicate.And(c => c.CompanyLegalTypeId == companyLegalTypeId);
            }

            if (!string.IsNullOrEmpty(uin))
            {
                predicate = predicate.And(c => c.CompanyUin.Contains(uin));
                historicPredicate = historicPredicate.And(c => c.CompanyUin.Contains(uin));
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contracts = contracts.Where(predicate);
                historicContracts = historicContracts.Where(historicPredicate);
            }

            var reimbursedAmounts = from c in contracts
                                     join ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>().Where(cra => cra.Status == ReimbursedAmountStatus.Entered && ReportsReimbursements.Contains(cra.Reimbursement)) on c.ContractId equals ra.ContractId into j1
                                     from ra in j1.DefaultIfEmpty()
                                     group new
                                     {
                                         EuAmount = ra.PrincipalBfp.EuAmount,
                                         BgAmount = ra.PrincipalBfp.BgAmount,
                                     }
                                    by c.ContractId into g
                                     select new
                                     {
                                         ContractId = g.Key,
                                         EuAmount = g.Sum(i => i.EuAmount),
                                         BgAmount = g.Sum(i => i.BgAmount),
                                     };

            var paidAmounts = from c in contracts
                               join pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(pa => pa.Status == ActuallyPaidAmountStatus.Entered) on c.ContractId equals pa.ContractId into j1
                               from pa in j1.DefaultIfEmpty()
                               group new
                               {
                                   PaidEuAmount = pa.PaidBfpEuAmount,
                                   PaidBgAmount = pa.PaidBfpBgAmount,
                               }
                                by c.ContractId into g
                               select new
                               {
                                   ContractId = g.Key,
                                   PaidEuAmount = g.Sum(i => i.PaidEuAmount),
                                   PaidBgAmount = g.Sum(i => i.PaidBgAmount),
                               };

            var contractedAmounts = from c in contracts
                                     join cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(cba1 => cba1.IsActive) on c.ContractId equals cba.ContractId into j1
                                     from cba in j1.DefaultIfEmpty()
                                     group new
                                     {
                                         CurrentEuAmount = (decimal?)cba.CurrentEuAmount,
                                         CurrentBgAmount = (decimal?)cba.CurrentBgAmount,
                                         CurrentSelfAmount = (decimal?)cba.CurrentSelfAmount,
                                     }
                                    by c.ContractId into g
                                     select new
                                     {
                                         ContractId = g.Key,
                                         ContractedEuAmount = g.Sum(i => i.CurrentEuAmount),
                                         ContractedBgAmount = g.Sum(i => i.CurrentBgAmount),
                                         ContractedSelfAmount = g.Sum(i => i.CurrentSelfAmount),
                                     };

            var query =
                from c in contracts

                join ca in contractedAmounts on c.ContractId equals ca.ContractId into j1
                from ca in j1.DefaultIfEmpty()

                join pa in paidAmounts on c.ContractId equals pa.ContractId into j2
                from pa in j2.DefaultIfEmpty()

                join ra in reimbursedAmounts on c.ContractId equals ra.ContractId into j3
                from ra in j3.DefaultIfEmpty()

                where c.ContractStatus == ContractStatus.Entered

                group new
                {
                    Id = c.ContractId,
                    Name = c.CompanyName,
                    NameAlt = c.CompanyNameAlt,
                    ContractId = c.ContractId,
                    ContractedEuAmount = ca.ContractedEuAmount ?? 0m,
                    ContractedBgAmount = ca.ContractedBgAmount ?? 0m,
                    ContractedSelfAmount = ca.ContractedSelfAmount ?? 0m,
                    PaidEuAmount = pa.PaidEuAmount ?? 0m,
                    PaidBgAmount = pa.PaidBgAmount ?? 0m,
                    ReimbursedEuAmount = ra.EuAmount ?? 0m,
                    ReimbursedBgAmount = ra.BgAmount ?? 0m,
                    UinType = c.CompanyUinType,
                }
                by new
                {
                    Uin = c.CompanyUin,
                }
                into g

                select new
                {
                    Id = g.Select(i => i.Id).Min(),

                    Uin = g.Key.Uin,
                    UinType = g.Select(i => i.UinType).Min(),
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    ContractsCount = g.Select(e => e.ContractId).Distinct().Count(),

                    ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                    ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                    ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                    PaidEuAmount = g.Sum(i => i.PaidEuAmount) - g.Sum(i => i.ReimbursedEuAmount),
                    PaidBgAmount = g.Sum(i => i.PaidBgAmount) - g.Sum(i => i.ReimbursedBgAmount),
                    IsHistoric = false,
                };

            var historicReimbursedAmounts =
                from hc in historicContracts
                 join ra in this.unitOfWork.DbContext.Set<HistoricContractReimbursedAmount>() on hc.HistoricContractId equals ra.HistoricContractId into g
                 from hcra in g.DefaultIfEmpty()
                 group new
                 {
                     EuAmount = hcra.ReimbursedPrincipalEuAmount,
                     BgAmount = hcra.ReimbursedPrincipalBgAmount,
                 }
                by hcra.HistoricContractId into g
                 select new
                 {
                     HistoricContractId = g.Key,
                     EuAmount = g.Sum(i => i.EuAmount),
                     BgAmount = g.Sum(i => i.BgAmount),
                 };

            var historicPaidAmounts =
                from hc in historicContracts
                 join pa in this.unitOfWork.DbContext.Set<HistoricContractActuallyPaidAmount>() on hc.HistoricContractId equals pa.HistoricContractId into g
                 from hcpa in g.DefaultIfEmpty()
                 group new
                 {
                     hcpa.PaidEuAmount,
                     hcpa.PaidBgAmount,
                 }
                 by hc.HistoricContractId into g
                 select new
                 {
                     HistoricContractId = g.Key,
                     PaidEuAmount = g.Sum(i => i.PaidEuAmount),
                     PaidBgAmount = g.Sum(i => i.PaidBgAmount),
                 };

            var historicBudgetAmounts =
                from hc in historicContracts
                 join ca in this.unitOfWork.DbContext.Set<HistoricContractContractedAmount>() on hc.HistoricContractId equals ca.HistoricContractId into g
                 from hcca in g.DefaultIfEmpty()
                 where hcca.IsLast == true
                 group new
                 {
                     ContractedEuAmount = (decimal?)hcca.ContractedEuAmount,
                     ContractedBgAmount = (decimal?)hcca.ContractedBgAmount,
                     ContractedSelfAmount = (decimal?)hcca.ContractedSeftAmount,
                 }
                by hcca.HistoricContractId into g
                 select new
                 {
                     HistoricContractId = g.Key,
                     ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                     ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                     ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                 };

            var historicQuery =
                from hc in historicContracts

                join hba in historicBudgetAmounts on hc.HistoricContractId equals hba.HistoricContractId into j1
                from hba in j1.DefaultIfEmpty()

                join hpa in historicPaidAmounts on hc.HistoricContractId equals hpa.HistoricContractId into j2
                from hpa in j2.DefaultIfEmpty()

                join hra in historicReimbursedAmounts on hc.HistoricContractId equals hra.HistoricContractId into j3
                from hra in j3.DefaultIfEmpty()

                group new
                {
                    Id = hc.HistoricContractId,
                    Name = hc.CompanyName,
                    NameAlt = hc.CompanyNameEn,
                    ContractId = hc.HistoricContractId,
                    ContractedEuAmount = hba.ContractedEuAmount ?? 0m,
                    ContractedBgAmount = hba.ContractedBgAmount ?? 0m,
                    ContractedSelfAmount = hba.ContractedSelfAmount ?? 0m,
                    PaidEuAmount = hpa.PaidEuAmount ?? 0m,
                    PaidBgAmount = hpa.PaidBgAmount ?? 0m,
                    ReimbursedEuAmount = hra.EuAmount ?? 0m,
                    ReimbursedBgAmount = hra.BgAmount ?? 0m,
                    UinType = hc.CompanyUinType,
                }
                by new
                {
                    Uin = hc.CompanyUin,
                }
                into g
                select new
                {
                    Id = g.Select(i => i.Id).Min(),

                    Uin = g.Key.Uin,
                    UinType = g.Select(i => i.UinType).Min(),
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    ContractsCount = g.Select(e => e.ContractId).Distinct().Count(),

                    ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                    ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                    ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                    PaidEuAmount = g.Sum(i => i.PaidEuAmount) - g.Sum(i => i.ReimbursedEuAmount),
                    PaidBgAmount = g.Sum(i => i.PaidBgAmount) - g.Sum(i => i.ReimbursedBgAmount),
                    IsHistoric = true,
                };

            var queryWithOffsetAndLimit = query
                .Union(historicQuery)
                .OrderBy(c => c.Name)
                .WithOffsetAndLimit(offset, limit);

            return new PageVO<ContractBeneficiaryVO>()
            {
                Results =
                    queryWithOffsetAndLimit
                    .ToList()
                    .Select(cp => new ContractBeneficiaryVO
                    {
                        Id = cp.Id,

                        Name = cp.Name,
                        NameAlt = cp.NameAlt,
                        Uin = cp.Uin,
                        UinType = cp.UinType,
                        ContractsCount = cp.ContractsCount,

                        ContractedAmount = cp.ContractedEuAmount + cp.ContractedBgAmount + cp.ContractedSelfAmount,
                        PaidAmount = cp.PaidEuAmount + cp.PaidBgAmount,
                        IsHistoric = cp.IsHistoric,
                    })
                    .ToList(),
                Count = query.Count(),
            };
        }

        public IEnumerable<ContractBeneficiaryVO> GetSimpleContractBeneficiaries(
            string name = null)
        {
            IQueryable<Contract> contracts = this.unitOfWork.DbContext.Set<Contract>();
            IQueryable<HistoricContract> historicContracts = this.unitOfWork.DbContext.Set<HistoricContract>();

            var predicate = PredicateBuilder.True<Contract>();
            var historitPredicate = PredicateBuilder.True<HistoricContract>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.Bulgarian)
                {
                    predicate = predicate.And(c => c.CompanyName.Contains(name));
                    historitPredicate = historitPredicate.And(c => c.CompanyName.Contains(name));
                }
                else
                {
                    predicate = predicate.And(c => c.CompanyNameAlt.Contains(name));
                    historitPredicate = historitPredicate.And(c => c.CompanyNameEn.Contains(name));
                }
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contracts = contracts.Where(predicate);
                historicContracts = historicContracts.Where(historitPredicate);
            }

            var query =
                from c in contracts

                where c.ContractStatus == ContractStatus.Entered

                group new
                {
                    Id = c.ContractId,
                    Name = c.CompanyName,
                    NameAlt = c.CompanyNameAlt,
                }
                by new
                {
                    Uin = c.CompanyUin,
                    UinType = c.CompanyUinType,
                }
                into g

                select new
                {
                    Id = g.Select(i => i.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Key.UinType,
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = false,
                };

            var historicQuery =
                from c in historicContracts
                group new
                {
                    Id = c.HistoricContractId,
                    Name = c.CompanyName,
                    NameAlt = c.CompanyNameEn,
                }
                by new
                {
                    Uin = c.CompanyUin,
                    UinType = c.CompanyUinType,
                }
                into g

                select new
                {
                    Id = g.Select(i => i.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Key.UinType,
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = true,
                };

            var allPayed = query
                .Union(historicQuery)
                .ToList();

            return allPayed.OrderBy(c => c.Name)
                    .ToList()
                    .Select(cp => new ContractBeneficiaryVO
                    {
                        Id = cp.Id,
                        Name = cp.Name,
                        NameAlt = cp.NameAlt,
                        Uin = cp.Uin,
                        UinType = cp.UinType,
                        IsHistoric = cp.IsHistoric,
                    })
                    .ToList();
        }

        public PageVO<ContractBeneficiaryWithoutFinancialCorrectionsVO> GetContractBeneficiariesWithoutFinancialCorrections(
            string name = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null,
            string uin = null,
            string seat = null,
            int offset = 0,
            int? limit = null)
        {
            IQueryable<Contract> contracts = this.unitOfWork.DbContext.Set<Contract>();

            var predicate = PredicateBuilder.True<Contract>()
                .AndEquals(c => c.ExecutionStatus, ContractExecutionStatus.Ended);

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.Bulgarian)
                {
                    predicate = predicate.And(c => c.CompanyName.Contains(name));
                }
                else
                {
                    predicate = predicate.And(c => c.CompanyNameAlt.Contains(name));
                }
            }

            if (companyTypeId.HasValue)
            {
                predicate = predicate.And(c => c.CompanyTypeId == companyTypeId);
            }

            if (companyLegalTypeId.HasValue)
            {
                predicate = predicate.And(c => c.CompanyLegalTypeId == companyLegalTypeId);
            }

            if (!string.IsNullOrEmpty(uin))
            {
                predicate = predicate.And(c => c.CompanyUin.Contains(uin));
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contracts = contracts.Where(predicate);
            }

            var financialCorrectionCompanyIds =
                from c in this.unitOfWork.DbContext.Set<Contract>()
                join fc in this.unitOfWork.DbContext.Set<FinancialCorrection>() on c.ContractId equals fc.ContractId
                join fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>() on fc.FinancialCorrectionId equals fcv.FinancialCorrectionId
                where fc.Status == FinancialCorrectionStatus.Entered && fcv.Status == FinancialCorrectionVersionStatus.Actual && fcv.TotalAmount > 0
                select c.CompanyUin;

            var extendedCompanyIds =
                from c in this.unitOfWork.DbContext.Set<Contract>()
                join p in this.unitOfWork.DbContext.Set<Project>() on c.ProjectId equals p.ProjectId
                where c.ContractStatus == ContractStatus.Entered && p.Duration < c.Duration
                select c.CompanyUin;

            var excludedCompanyIds = financialCorrectionCompanyIds
                .Union(extendedCompanyIds);

            var query =
                from c in contracts
                join ct in this.unitOfWork.DbContext.Set<CompanyType>() on c.CompanyTypeId equals ct.CompanyTypeId
                join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on c.CompanyLegalTypeId equals clt.CompanyLegalTypeId
                join country in this.unitOfWork.DbContext.Set<Country>() on c.BeneficiarySeatCountryId equals country.CountryId
                join settlement in this.unitOfWork.DbContext.Set<Settlement>() on c.BeneficiarySeatSettlementId equals settlement.SettlementId

                where !excludedCompanyIds.Contains(c.CompanyUin)

                 group new
                {
                    Id = c.ContractId,
                    Name = c.CompanyName,
                    NameAlt = c.CompanyNameAlt,
                    ContractId = c.ContractId,
                    UinType = c.CompanyUinType,
                    CompanyUin = c.CompanyUin,
                    CompanyTypeName = ct.Name,
                    CompanyLegalTypeName = clt.Name,
                    SeatCountry = country.Name,
                    SeatSettlement = settlement.DisplayName,
                    SeatPostCode = c.BeneficiarySeatPostCode,
                    SeatStreet = c.BeneficiarySeatStreet,
                    SeatAddress = c.BeneficiarySeatAddress,
                }
                by new
                {
                    Uin = c.CompanyUin,
                }
                into g

                select new
                {
                    Id = g.Select(i => i.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Select(i => i.UinType).Min(),
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    CompanyTypeName = g.Select(i => i.CompanyTypeName).Min(),
                    CompanyLegalTypeName = g.Select(i => i.CompanyLegalTypeName).Min(),
                    ContractsCount = g.Select(e => e.ContractId).Distinct().Count(),
                    SeatCountry = g.Select(i => i.SeatCountry).Min(),
                    SeatSettlement = g.Select(i => i.SeatSettlement).Min(),
                    SeatPostCode = g.Select(i => i.SeatPostCode).Min(),
                    SeatStreet = g.Select(i => i.SeatStreet).Min(),
                    SeatAddress = g.Select(i => i.SeatAddress).Min(),
                };

            var queryWithOffsetAndLimit = query
                .OrderBy(c => c.Name)
                .WithOffsetAndLimit(offset, limit);

            return new PageVO<ContractBeneficiaryWithoutFinancialCorrectionsVO>()
            {
                Results =
                    queryWithOffsetAndLimit
                    .ToList()
                    .Select(cp => new ContractBeneficiaryWithoutFinancialCorrectionsVO
                    {
                        Id = cp.Id,

                        Name = cp.Name,
                        NameAlt = cp.NameAlt,
                        Uin = cp.Uin,
                        UinType = cp.UinType,
                        CompanyTypeName = cp.CompanyTypeName,
                        CompanyLegalTypeName = cp.CompanyLegalTypeName,
                        ContractsCount = cp.ContractsCount,
                        SeatCountry = cp.SeatCountry,
                        SeatSettlement = cp.SeatSettlement,
                        SeatPostCode = cp.SeatPostCode,
                        SeatStreet = cp.SeatStreet,
                        SeatAddress = cp.SeatAddress,
                    })
                    .Where(x => string.IsNullOrEmpty(seat) || x.Seat.Contains(seat))
                    .ToList(),
                Count = query.Count(),
            };
        }

        public ContractBeneficiaryVO GetContractBeneficiary(
            string uin,
            UinType uinType,
            bool isHistoric)
        {
            var query = Enumerable.Empty<ContractBeneficiaryVO>().AsQueryable();

            if (isHistoric)
            {
                IQueryable<HistoricContract> historicContracts = this.unitOfWork.DbContext.Set<HistoricContract>();

                query =
                        from hc in historicContracts
                        join country in this.unitOfWork.DbContext.Set<Country>() on hc.SeatCountryCode equals country.NutsCode into j1
                        from country in j1.DefaultIfEmpty()
                        join settlement in this.unitOfWork.DbContext.Set<Settlement>() on hc.SeatSettlementCode equals settlement.LauCode into j2
                        from settlement in j2.DefaultIfEmpty()
                        where hc.CompanyUin == uin && hc.CompanyUinType == uinType

                        group new
                        {
                            Id = hc.HistoricContractId,
                            Name = hc.CompanyName,
                            NameAlt = hc.CompanyNameEn,
                            SeatCountry = country.Name,
                            SeatSettlement = settlement.DisplayName,
                            SeatPostCode = hc.SeatPostCode,
                            SeatStreet = hc.SeatStreet,
                            SeatAddress = hc.SeatAddress,
                        }
                        by new
                        {
                            Uin = hc.CompanyUin,
                            UinType = hc.CompanyUinType,
                        }
                        into g

                        select new ContractBeneficiaryVO
                        {
                            Id = g.Select(e => e.Id).Min(),
                            Uin = g.Key.Uin,
                            UinType = g.Key.UinType,
                            Name = g.Select(i => i.Name).Min(),
                            NameAlt = g.Select(i => i.NameAlt).Min(),
                            SeatCountry = g.Select(i => i.SeatCountry).Min(),
                            SeatSettlement = g.Select(i => i.SeatSettlement).Min(),
                            SeatPostCode = g.Select(i => i.SeatPostCode).Min(),
                            SeatStreet = g.Select(i => i.SeatStreet).Min(),
                            SeatAddress = g.Select(i => i.SeatAddress).Min(),
                        };
            }
            else
            {
                IQueryable<Contract> contracts = this.unitOfWork.DbContext.Set<Contract>();

                query =
                    from c in contracts
                    join country in this.unitOfWork.DbContext.Set<Country>() on c.BeneficiarySeatCountryId equals country.CountryId into j1
                    from country in j1.DefaultIfEmpty()
                    join settlement in this.unitOfWork.DbContext.Set<Settlement>() on c.BeneficiarySeatSettlementId equals settlement.SettlementId into j2
                    from settlement in j2.DefaultIfEmpty()
                    where c.CompanyUin == uin && c.CompanyUinType == uinType && c.ContractStatus == ContractStatus.Entered

                    group new
                    {
                        Id = c.ContractId,
                        Name = c.CompanyName,
                        NameAlt = c.CompanyNameAlt,
                        SeatCountry = country.Name,
                        SeatSettlement = settlement.DisplayName,
                        SeatPostCode = c.BeneficiarySeatPostCode,
                        SeatStreet = c.BeneficiarySeatStreet,
                        SeatAddress = c.BeneficiarySeatAddress,
                    }
                    by new
                    {
                        Uin = c.CompanyUin,
                        UinType = c.CompanyUinType,
                    }
                    into g

                    select new ContractBeneficiaryVO
                    {
                        Id = g.Select(e => e.Id).Min(),
                        Uin = g.Key.Uin,
                        UinType = g.Key.UinType,
                        Name = g.Select(i => i.Name).Min(),
                        NameAlt = g.Select(i => i.NameAlt).Min(),
                        SeatCountry = g.Select(i => i.SeatCountry).Min(),
                        SeatSettlement = g.Select(i => i.SeatSettlement).Min(),
                        SeatPostCode = g.Select(i => i.SeatPostCode).Min(),
                        SeatStreet = g.Select(i => i.SeatStreet).Min(),
                        SeatAddress = g.Select(i => i.SeatAddress).Min(),
                    };
            }

            var foundBeneficiary = query.FirstOrDefault();

            var result = new ContractBeneficiaryVO();

            if (foundBeneficiary != null)
            {
                result.Id = foundBeneficiary.Id;
                result.Uin = foundBeneficiary.Uin;
                result.UinType = foundBeneficiary.UinType;
                result.Name = foundBeneficiary.Name;
                result.NameAlt = foundBeneficiary.NameAlt;
                result.SeatCountry = foundBeneficiary.SeatCountry;
                result.SeatSettlement = foundBeneficiary.SeatSettlement;
                result.SeatPostCode = foundBeneficiary.SeatPostCode;
                result.SeatStreet = foundBeneficiary.SeatStreet;
                result.SeatAddress = foundBeneficiary.SeatAddress;
            }

            return result;
        }

        public ContractBeneficiaryVO GetContractBeneficiary(int contractId, bool isHistoric)
        {
            if (isHistoric)
            {
                HistoricContract contract = this.unitOfWork.DbContext.Set<HistoricContract>().Single(e => e.HistoricContractId == contractId);

                var result = new ContractBeneficiaryVO();

                result.Id = contract.HistoricContractId;
                result.Uin = contract.CompanyUin;
                result.UinType = contract.CompanyUinType;
                result.Name = contract.CompanyName;
                result.NameAlt = contract.CompanyNameEn;

                return result;
            }
            else
            {
                Contract contract = this.unitOfWork.DbContext.Set<Contract>().Single(e => e.ContractId == contractId);

                var result = new ContractBeneficiaryVO();

                result.Id = contract.ContractId;
                result.Uin = contract.CompanyUin;
                result.UinType = contract.CompanyUinType;
                result.Name = contract.CompanyName;
                result.NameAlt = contract.CompanyNameAlt;

                return result;
            }
        }

        public PageVO<ContractPartnerVO> GetContractPartners(
            string name = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null,
            string companyUin = null,
            int offset = 0,
            int? limit = null)
        {
            IQueryable<ContractPartner> contractPartners = this.unitOfWork.DbContext.Set<ContractPartner>();
            IQueryable<HistoricContractPartner> historicContractPartners = this.unitOfWork.DbContext.Set<HistoricContractPartner>();

            var predicate = PredicateBuilder.True<ContractPartner>();
            var historicPredicate = PredicateBuilder.True<HistoricContractPartner>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.Bulgarian)
                {
                    predicate = predicate.And(c => c.Name.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.PartnerName.Contains(name));
                }
                else
                {
                    predicate = predicate.And(c => c.NameAlt.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.PartnerNameEn.Contains(name));
                }
            }

            if (companyTypeId != null)
            {
                predicate = predicate.And(c => c.CompanyTypeId == companyTypeId);
                historicPredicate = historicPredicate.And(c => c.PartnerTypeId == companyTypeId);
            }

            if (companyLegalTypeId != null)
            {
                predicate = predicate.And(c => c.CompanyLegalTypeId == companyLegalTypeId);
                historicPredicate = historicPredicate.And(c => c.PartnerLegalTypeId == companyLegalTypeId);
            }

            if (!string.IsNullOrWhiteSpace(companyUin))
            {
                predicate = predicate.And(c => c.Uin.Contains(companyUin));
                historicPredicate = historicPredicate.And(c => c.PartnerUin.Contains(companyUin));
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contractPartners = contractPartners.Where(predicate);
                historicContractPartners = historicContractPartners.Where(historicPredicate);
            }

            var query =
                from cp in contractPartners

                group new
                {
                    Id = cp.ContractPartnerId,
                    Name = cp.Name,
                    NameAlt = cp.NameAlt,
                    ContractId = cp.ContractId,
                    UinType = cp.UinType,
                }
                by new
                {
                    Uin = cp.Uin,
                }
                into g

                select new
                {
                    Id = g.Select(e => e.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Select(e => e.UinType).Min(),
                    ContractsCount = g.Count(),
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = false,
                };

            var historicQuery =
                from cp in historicContractPartners
                where cp.PartnerType == HistoricContractPartnerType.Partner

                group new
                {
                    Id = cp.HistoricContractPartnerId,
                    Name = cp.PartnerName,
                    NameAlt = cp.PartnerNameEn,
                    ContractId = cp.HistoricContractId,
                    UinType = cp.PartnerUinType,
                }
                by new
                {
                    Uin = cp.PartnerUin,
                }
                into g

                select new
                {
                    Id = g.Select(e => e.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Select(e => e.UinType).Min(),
                    ContractsCount = g.Count(),
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = true,
                };

            var queryWithOffsetAndLimit =
                query
                .Union(historicQuery)
                .OrderBy(c => c.Name)
                .WithOffsetAndLimit(offset, limit);

            return new PageVO<ContractPartnerVO>()
            {
                Results =
                    queryWithOffsetAndLimit
                    .ToList()
                    .Select(cp => new ContractPartnerVO
                    {
                        Id = cp.Id,
                        Name = cp.Name,
                        NameAlt = cp.NameAlt,
                        Uin = cp.Uin,
                        UinType = cp.UinType,
                        ContractsCount = cp.ContractsCount,
                        IsHistoric = cp.IsHistoric,
                    })
                    .ToList(),
                Count = query.Count(),
            };
        }

        public IEnumerable<ContractPartnerVO> GetSimpleContractPartners(
            string name = null)
        {
            IQueryable<ContractPartner> contractPartners = this.unitOfWork.DbContext.Set<ContractPartner>();
            IQueryable<HistoricContractPartner> historicContractPartners = this.unitOfWork.DbContext.Set<HistoricContractPartner>();

            var predicate = PredicateBuilder.True<ContractPartner>();
            var historicPredicate = PredicateBuilder.True<HistoricContractPartner>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.Bulgarian)
                {
                    predicate = predicate.And(c => c.Name.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.PartnerName.Contains(name));
                }
                else
                {
                    predicate = predicate.And(c => c.NameAlt.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.PartnerNameEn.Contains(name));
                }
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contractPartners = contractPartners.Where(predicate);
                historicContractPartners = historicContractPartners.Where(historicPredicate);
            }

            var query =
                from cp in contractPartners

                group new
                {
                    Id = cp.ContractPartnerId,
                    Name = cp.Name,
                    NameAlt = cp.NameAlt,
                }
                by new
                {
                    Uin = cp.Uin,
                    UinType = cp.UinType,
                }
                into g

                select new
                {
                    Id = g.Select(e => e.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Key.UinType,
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = false,
                };

            var historicQuery =
                from cp in historicContractPartners
                where cp.PartnerType == HistoricContractPartnerType.Partner

                group new
                {
                    Id = cp.HistoricContractPartnerId,
                    Name = cp.PartnerName,
                    NameAlt = cp.PartnerNameEn,
                }
                by new
                {
                    Uin = cp.PartnerUin,
                    UinType = cp.PartnerUinType,
                }
                into g

                select new
                {
                    Id = g.Select(e => e.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Key.UinType,
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = true,
                };

            return query
                    .Union(historicQuery)
                    .OrderBy(c => c.Name)
                    .ToList()
                    .Select(cp => new ContractPartnerVO
                    {
                        Id = cp.Id,
                        Name = cp.Name,
                        NameAlt = cp.NameAlt,
                        Uin = cp.Uin,
                        UinType = cp.UinType,
                        IsHistoric = cp.IsHistoric,
                    })
                    .ToList();
        }

        public ContractPartnerVO GetContractPartner(
            string uin,
            UinType uinType,
            bool isHistoric)
        {
            var query = Enumerable.Empty<ContractPartnerVO>().AsQueryable();

            if (isHistoric)
            {
                IQueryable<HistoricContractPartner> partners = this.unitOfWork.DbContext.Set<HistoricContractPartner>();

                query =
                    from p in partners
                    join country in this.unitOfWork.DbContext.Set<Country>() on p.SeatCountryCode equals country.NutsCode into j1
                    from country in j1.DefaultIfEmpty()
                    join settlement in this.unitOfWork.DbContext.Set<Settlement>() on p.SeatSettlementCode equals settlement.LauCode into j2
                    from settlement in j2.DefaultIfEmpty()
                    where p.PartnerType == HistoricContractPartnerType.Partner && p.PartnerUin == uin && p.PartnerUinType == uinType

                    group new
                    {
                        Id = p.HistoricContractPartnerId,
                        Name = p.PartnerName,
                        NameAlt = p.PartnerNameEn,
                        SeatCountry = country.Name,
                        SeatSettlement = settlement.DisplayName,
                        SeatPostCode = p.SeatPostCode,
                        SeatStreet = p.SeatStreet,
                        SeatAddress = p.SeatAddress,
                    }
                    by new
                    {
                        Uin = p.PartnerUin,
                        UinType = p.PartnerUinType,
                    }
                    into g

                    select new ContractPartnerVO
                    {
                        Id = g.Select(e => e.Id).Min(),
                        Uin = g.Key.Uin,
                        UinType = g.Key.UinType,
                        Name = g.Select(i => i.Name).Min(),
                        NameAlt = g.Select(i => i.NameAlt).Min(),
                        SeatCountry = g.Select(i => i.SeatCountry).Min(),
                        SeatSettlement = g.Select(i => i.SeatSettlement).Min(),
                        SeatPostCode = g.Select(i => i.SeatPostCode).Min(),
                        SeatStreet = g.Select(i => i.SeatStreet).Min(),
                        SeatAddress = g.Select(i => i.SeatAddress).Min(),
                    };
            }
            else
            {
                IQueryable<ContractPartner> partners = this.unitOfWork.DbContext.Set<ContractPartner>();

                query =
                    from p in partners
                    join country in this.unitOfWork.DbContext.Set<Country>() on p.SeatCountryId equals country.CountryId into j1
                    from country in j1.DefaultIfEmpty()
                    join settlement in this.unitOfWork.DbContext.Set<Settlement>() on p.SeatSettlementId equals settlement.SettlementId into j2
                    from settlement in j2.DefaultIfEmpty()
                    where p.Uin == uin

                    group new
                    {
                        Id = p.ContractPartnerId,
                        Name = p.Name,
                        NameAlt = p.NameAlt,
                        SeatCountry = country.Name,
                        SeatSettlement = settlement.DisplayName,
                        SeatPostCode = p.SeatPostCode,
                        SeatStreet = p.SeatStreet,
                        SeatAddress = p.SeatAddress,
                        UinType = p.UinType,
                    }
                    by new
                    {
                        Uin = p.Uin,
                    }
                    into g

                    select new ContractPartnerVO
                    {
                        Id = g.Select(e => e.Id).Min(),
                        Uin = g.Key.Uin,
                        UinType = g.Select(i => i.UinType).Min(),
                        Name = g.Select(i => i.Name).Min(),
                        NameAlt = g.Select(i => i.NameAlt).Min(),
                        SeatCountry = g.Select(i => i.SeatCountry).Min(),
                        SeatSettlement = g.Select(i => i.SeatSettlement).Min(),
                        SeatPostCode = g.Select(i => i.SeatPostCode).Min(),
                        SeatStreet = g.Select(i => i.SeatStreet).Min(),
                        SeatAddress = g.Select(i => i.SeatAddress).Min(),
                    };
            }

            var foundPartner = query.FirstOrDefault();

            var result = new ContractPartnerVO();

            if (foundPartner != null)
            {
                result.Id = foundPartner.Id;
                result.Uin = foundPartner.Uin;
                result.UinType = foundPartner.UinType;
                result.Name = foundPartner.Name;
                result.NameAlt = foundPartner.NameAlt;
                result.SeatCountry = foundPartner.SeatCountry;
                result.SeatSettlement = foundPartner.SeatSettlement;
                result.SeatPostCode = foundPartner.SeatPostCode;
                result.SeatStreet = foundPartner.SeatStreet;
                result.SeatAddress = foundPartner.SeatAddress;
            }

            return result;
        }

        public ContractPartnerVO GetContractPartner(int partnerId, bool isHistoric)
        {
            if (isHistoric)
            {
                HistoricContractPartner partner = this.unitOfWork.DbContext.Set<HistoricContractPartner>().Single(e => e.HistoricContractPartnerId == partnerId);

                var result = new ContractPartnerVO();

                result.Id = partner.HistoricContractPartnerId;
                result.Uin = partner.PartnerUin;
                result.UinType = partner.PartnerUinType;
                result.Name = partner.PartnerName;
                result.NameAlt = partner.PartnerNameEn;

                return result;
            }
            else
            {
                ContractPartner partner = this.unitOfWork.DbContext.Set<ContractPartner>().Single(e => e.ContractPartnerId == partnerId);

                var result = new ContractPartnerVO();

                result.Id = partner.ContractPartnerId;
                result.Uin = partner.Uin;
                result.UinType = partner.UinType;
                result.Name = partner.Name;
                result.NameAlt = partner.NameAlt;

                return result;
            }
        }

        public PageVO<ContractContractorVO> GetContractContractors(
            string name = null,
            string companyUin = null,
            int offset = 0,
            int? limit = null)
        {
            IQueryable<ContractContractor> contractContractors = this.unitOfWork.DbContext.Set<ContractContractor>();
            IQueryable<HistoricContractPartner> historicContractContractors = this.unitOfWork.DbContext.Set<HistoricContractPartner>();

            var predicate = PredicateBuilder.True<ContractContractor>();
            var historicPredicate = PredicateBuilder.True<HistoricContractPartner>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.Bulgarian)
                {
                    predicate = predicate.And(c => c.Name.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.PartnerName.Contains(name));
                }
                else
                {
                    predicate = predicate.And(c => c.NameAlt.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.PartnerNameEn.Contains(name));
                }
            }

            if (!string.IsNullOrWhiteSpace(companyUin))
            {
                predicate = predicate.And(c => c.Uin.Contains(companyUin));
                historicPredicate = historicPredicate.And(c => c.PartnerUin.Contains(companyUin));
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contractContractors = contractContractors.Where(predicate);
                historicContractContractors = historicContractContractors.Where(historicPredicate);
            }

            var query =
                from contractor in contractContractors

                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on contractor.ContractContractorId equals cc.ContractContractorId into j1
                from cc in j1.DefaultIfEmpty()

                group new
                {
                    Id = contractor.ContractContractorId,
                    Name = contractor.Name,
                    NameAlt = contractor.NameAlt,
                    ContractContractId = (int?)cc.ContractContractId,
                    UinType = contractor.UinType,
                }
                by new
                {
                    Uin = contractor.Uin,
                }
                into g

                select new
                {
                    Id = g.Select(e => e.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Select(c => c.UinType).Min(),
                    ContractsCount = g.Count(c => c.ContractContractId != null),
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = false,
                };

            var historicQuery =
                from contractor in historicContractContractors

                join cc in this.unitOfWork.DbContext.Set<HistoricContractPartner>() on contractor.HistoricContractPartnerId equals cc.HistoricContractPartnerId into j1
                from cc in j1.DefaultIfEmpty()

                where cc.PartnerType == HistoricContractPartnerType.Contractor

                group new
                {
                    Id = contractor.HistoricContractPartnerId,
                    Name = contractor.PartnerName,
                    NameAlt = contractor.PartnerNameEn,
                    ContractContractId = (int?)cc.HistoricContractPartnerId,
                    UinType = contractor.PartnerUinType,
                }
                by new
                {
                    Uin = contractor.PartnerUin,
                }
                into g

                select new
                {
                    Id = g.Select(e => e.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Select(c => c.UinType).Min(),
                    ContractsCount = g.Count(c => c.ContractContractId != null),
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = true,
                };

            var queryWithOffsetAndLimit =
                query
                .Union(historicQuery)
                .OrderBy(c => c.Name)
                .WithOffsetAndLimit(offset, limit);

            return new PageVO<ContractContractorVO>()
            {
                Results =
                    queryWithOffsetAndLimit
                    .ToList()
                    .Select(cp => new ContractContractorVO
                    {
                        Id = cp.Id,
                        Name = cp.Name,
                        NameAlt = cp.NameAlt,
                        Uin = cp.Uin,
                        UinType = cp.UinType,
                        ContractsCount = cp.ContractsCount,
                        IsHistoric = cp.IsHistoric,
                    })
                    .ToList(),
                Count = query.Count(),
            };
        }

        public IEnumerable<ContractContractorVO> GetSimpleContractContractors(
            string name = null)
        {
            IQueryable<ContractContractor> contractContractors = this.unitOfWork.DbContext.Set<ContractContractor>();
            IQueryable<HistoricContractPartner> historicContractContractors = this.unitOfWork.DbContext.Set<HistoricContractPartner>();

            var predicate = PredicateBuilder.True<ContractContractor>();
            var historicPredicate = PredicateBuilder.True<HistoricContractPartner>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.Bulgarian)
                {
                    predicate = predicate.And(c => c.Name.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.PartnerName.Contains(name));
                }
                else
                {
                    predicate = predicate.And(c => c.NameAlt.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.PartnerNameEn.Contains(name));
                }
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contractContractors = contractContractors.Where(predicate);
                historicContractContractors = historicContractContractors.Where(historicPredicate);
            }

            var query =
                from contractor in contractContractors

                group new
                {
                    Id = contractor.ContractContractorId,
                    Name = contractor.Name,
                    NameAlt = contractor.NameAlt,
                }
                by new
                {
                    Uin = contractor.Uin,
                    UinType = contractor.UinType,
                }
                into g

                select new
                {
                    Id = g.Select(e => e.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Key.UinType,
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = false,
                };

            var historicQuery =
                from contractor in historicContractContractors
                where contractor.PartnerType == HistoricContractPartnerType.Contractor

                group new
                {
                    Id = contractor.HistoricContractPartnerId,
                    Name = contractor.PartnerName,
                    NameAlt = contractor.PartnerNameEn,
                }
                by new
                {
                    Uin = contractor.PartnerUin,
                    UinType = contractor.PartnerUinType,
                }
                into g

                select new
                {
                    Id = g.Select(e => e.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Key.UinType,
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = true,
                };

            return query
                    .Union(historicQuery)
                    .OrderBy(c => c.Name)
                    .ToList()
                    .Select(cp => new ContractContractorVO
                    {
                        Id = cp.Id,
                        Name = cp.Name,
                        NameAlt = cp.NameAlt,
                        Uin = cp.Uin,
                        UinType = cp.UinType,
                        IsHistoric = cp.IsHistoric,
                    })
                    .ToList();
        }

        public ContractContractorVO GetContractContractor(
            string uin,
            UinType uinType,
            bool isHistoric)
        {
            var query = Enumerable.Empty<ContractContractorVO>().AsQueryable();

            if (isHistoric)
            {
                IQueryable<HistoricContractPartner> contractors = this.unitOfWork.DbContext.Set<HistoricContractPartner>();

                query =
                    from c in contractors
                    join country in this.unitOfWork.DbContext.Set<Country>() on c.SeatCountryCode equals country.NutsCode into j1
                    from country in j1.DefaultIfEmpty()
                    join settlement in this.unitOfWork.DbContext.Set<Settlement>() on c.SeatSettlementCode equals settlement.LauCode into j2
                    from settlement in j2.DefaultIfEmpty()
                    where c.PartnerType == HistoricContractPartnerType.Contractor && c.PartnerUin == uin && c.PartnerUinType == uinType

                    group new
                    {
                        Id = c.HistoricContractPartnerId,
                        Name = c.PartnerName,
                        NameAlt = c.PartnerNameEn,
                        SeatCountry = country.Name,
                        SeatSettlement = settlement.DisplayName,
                        SeatPostCode = c.SeatPostCode,
                        SeatStreet = c.SeatStreet,
                        SeatAddress = c.SeatAddress,
                    }
                    by new
                    {
                        Uin = c.PartnerUin,
                        UinType = c.PartnerUinType,
                    }
                    into g

                    select new ContractContractorVO
                    {
                        Id = g.Select(e => e.Id).Min(),
                        Uin = g.Key.Uin,
                        UinType = g.Key.UinType,
                        Name = g.Select(i => i.Name).Min(),
                        NameAlt = g.Select(i => i.NameAlt).Min(),
                        SeatCountry = g.Select(i => i.SeatCountry).Min(),
                        SeatSettlement = g.Select(i => i.SeatSettlement).Min(),
                        SeatPostCode = g.Select(i => i.SeatPostCode).Min(),
                        SeatStreet = g.Select(i => i.SeatStreet).Min(),
                        SeatAddress = g.Select(i => i.SeatAddress).Min(),
                    };
            }
            else
            {
                IQueryable<ContractContractor> contractors = this.unitOfWork.DbContext.Set<ContractContractor>();

                query =
                    from c in contractors
                    join country in this.unitOfWork.DbContext.Set<Country>() on c.SeatCountryId equals country.CountryId into j1
                    from country in j1.DefaultIfEmpty()
                    join settlement in this.unitOfWork.DbContext.Set<Settlement>() on c.SeatSettlementId equals settlement.SettlementId into j2
                    from settlement in j2.DefaultIfEmpty()
                    where c.Uin == uin && c.UinType == uinType

                    group new
                    {
                        Id = c.ContractContractorId,
                        Name = c.Name,
                        NameAlt = c.NameAlt,
                        SeatCountry = country.Name,
                        SeatSettlement = settlement.DisplayName,
                        SeatPostCode = c.SeatPostCode,
                        SeatStreet = c.SeatStreet,
                        SeatAddress = c.SeatAddress,
                    }
                    by new
                    {
                        Uin = c.Uin,
                        UinType = c.UinType,
                    }
                    into g

                    select new ContractContractorVO
                    {
                        Id = g.Select(e => e.Id).Min(),
                        Uin = g.Key.Uin,
                        UinType = g.Key.UinType,
                        Name = g.Select(i => i.Name).Min(),
                        NameAlt = g.Select(i => i.NameAlt).Min(),
                        SeatCountry = g.Select(i => i.SeatCountry).Min(),
                        SeatSettlement = g.Select(i => i.SeatSettlement).Min(),
                        SeatPostCode = g.Select(i => i.SeatPostCode).Min(),
                        SeatStreet = g.Select(i => i.SeatStreet).Min(),
                        SeatAddress = g.Select(i => i.SeatAddress).Min(),
                    };
            }

            var foundContractor = query.FirstOrDefault();

            var result = new ContractContractorVO();

            if (foundContractor != null)
            {
                result.Id = foundContractor.Id;
                result.Uin = foundContractor.Uin;
                result.UinType = foundContractor.UinType;
                result.Name = foundContractor.Name;
                result.NameAlt = foundContractor.NameAlt;
                result.SeatCountry = foundContractor.SeatCountry;
                result.SeatSettlement = foundContractor.SeatSettlement;
                result.SeatPostCode = foundContractor.SeatPostCode;
                result.SeatStreet = foundContractor.SeatStreet;
                result.SeatAddress = foundContractor.SeatAddress;
            }

            return result;
        }

        public ContractContractorVO GetContractContractor(int contractorId, bool isHistoric)
        {
            if (isHistoric)
            {
                HistoricContractPartner contractor = this.unitOfWork.DbContext.Set<HistoricContractPartner>().Single(e => e.HistoricContractPartnerId == contractorId);

                var result = new ContractContractorVO();

                result.Id = contractor.HistoricContractPartnerId;
                result.Uin = contractor.PartnerUin;
                result.UinType = contractor.PartnerUinType;
                result.Name = contractor.PartnerName;
                result.NameAlt = contractor.PartnerNameEn;

                return result;
            }
            else
            {
                ContractContractor contractor = this.unitOfWork.DbContext.Set<ContractContractor>().Single(e => e.ContractContractorId == contractorId);

                var result = new ContractContractorVO();

                result.Id = contractor.ContractContractorId;
                result.Uin = contractor.Uin;
                result.UinType = contractor.UinType;
                result.Name = contractor.Name;
                result.NameAlt = contractor.NameAlt;

                return result;
            }
        }

        public PageVO<ContractSubcontractorVO> GetContractSubcontractors(
            ContractSubcontractType type,
            string name = null,
            string companyUin = null,
            int offset = 0,
            int? limit = null)
        {
            IQueryable<ContractSubcontract> contractSubcontractors = this.unitOfWork.DbContext.Set<ContractSubcontract>().Include("ContractContractor");
            IQueryable<HistoricContractPartner> historicContractSubcontractors = this.unitOfWork.DbContext.Set<HistoricContractPartner>();

            var predicate = PredicateBuilder.True<ContractSubcontract>();
            var historicPredicate = PredicateBuilder.True<HistoricContractPartner>();
            var contractorPredicate = PredicateBuilder.True<ContractContractor>();

            var partnerType = HistoricContractPartnerType.Subcontractor;

            if (type == ContractSubcontractType.Member)
            {
                partnerType = HistoricContractPartnerType.Member;
            }

            predicate = predicate.And(c => c.Type == type);
            historicPredicate = historicPredicate.And(c => c.PartnerType == partnerType);

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.Bulgarian)
                {
                    predicate = predicate.And(c => c.ContractContractor.Name.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.PartnerName.Contains(name));
                }
                else
                {
                    predicate = predicate.And(c => c.ContractContractor.NameAlt.Contains(name));
                    historicPredicate = historicPredicate.And(c => c.PartnerNameEn.Contains(name));
                }
            }

            if (!string.IsNullOrWhiteSpace(companyUin))
            {
                contractorPredicate = contractorPredicate.And(x => x.Uin.Contains(companyUin));
            }

            if (!predicate.IsTrueLambdaExpr())
            {
                contractSubcontractors = contractSubcontractors.Where(predicate);
                historicContractSubcontractors = historicContractSubcontractors.Where(historicPredicate);
            }

            var query =
                from subcontractor in contractSubcontractors

                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on subcontractor.ContractContractId equals cc.ContractContractId

                join cc1 in this.unitOfWork.DbContext.Set<ContractContractor>().Where(contractorPredicate) on subcontractor.ContractContractorId equals cc1.ContractContractorId

                group new
                {
                    Id = subcontractor.ContractContractorId,
                    Name = cc1.Name,
                    NameAlt = cc1.NameAlt,
                    ContractContractId = subcontractor.ContractContractId,
                    UinType = cc1.UinType,
                }
                by new
                {
                    Uin = cc1.Uin,
                }
                into g

                select new
                {
                    Id = g.Select(e => e.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Select(e => e.UinType).Min(),
                    ContractsCount = g.Count(),
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = false,
                };

            var historicQuery =
                from subcontractor in historicContractSubcontractors

                join cc in this.unitOfWork.DbContext.Set<HistoricContractPartner>() on subcontractor.HistoricContractPartnerId equals cc.HistoricContractPartnerId

                join cc1 in this.unitOfWork.DbContext.Set<HistoricContractPartner>() on subcontractor.HistoricContractPartnerId equals cc1.HistoricContractPartnerId

                group new
                {
                    Id = subcontractor.HistoricContractPartnerId,
                    Name = cc1.PartnerName,
                    NameAlt = cc1.PartnerNameEn,
                    ContractContractId = subcontractor.HistoricContractPartnerId,
                    UinType = cc1.PartnerUinType,
                }
                by new
                {
                    Uin = cc1.PartnerUin,
                }
                into g

                select new
                {
                    Id = g.Select(e => e.Id).Min(),
                    Uin = g.Key.Uin,
                    UinType = g.Select(e => e.UinType).Min(),
                    ContractsCount = g.Count(),
                    Name = g.Select(i => i.Name).Min(),
                    NameAlt = g.Select(i => i.NameAlt).Min(),
                    IsHistoric = true,
                };

            var queryWithOffsetAndLimit =
                query

                // .Union(historicQuery)
                .OrderBy(c => c.Name)
                .WithOffsetAndLimit(offset, limit);

            return new PageVO<ContractSubcontractorVO>()
            {
                Results =
                    queryWithOffsetAndLimit
                    .ToList()
                    .Select(cp => new ContractSubcontractorVO
                    {
                        Id = cp.Id,
                        Name = cp.Name,
                        NameAlt = cp.NameAlt,
                        Uin = cp.Uin,
                        UinType = cp.UinType,
                        ContractsCount = cp.ContractsCount,
                        IsHistoric = cp.IsHistoric,
                    })
                    .ToList(),
                Count = query.Count(),
            };
        }

        public ContractSubcontractorVO GetContractSubcontractor(
            ContractSubcontractType type,
            string uin,
            UinType uinType,
            bool isHistoric)
        {
            var query = Enumerable.Empty<ContractSubcontractorVO>().AsQueryable();

            if (isHistoric)
            {
                IQueryable<HistoricContractPartner> subcontractors = this.unitOfWork.DbContext.Set<HistoricContractPartner>();

                var partnerType = HistoricContractPartnerType.Subcontractor;

                if (type == ContractSubcontractType.Member)
                {
                    partnerType = HistoricContractPartnerType.Member;
                }

                query =
                    from s in subcontractors
                    join country in this.unitOfWork.DbContext.Set<Country>() on s.SeatCountryCode equals country.NutsCode into j1
                    from country in j1.DefaultIfEmpty()
                    join settlement in this.unitOfWork.DbContext.Set<Settlement>() on s.SeatSettlementCode equals settlement.LauCode into j2
                    from settlement in j2.DefaultIfEmpty()
                    where s.PartnerType == partnerType && s.PartnerUin == uin && s.PartnerUinType == uinType

                    group new
                    {
                        Id = s.HistoricContractPartnerId,
                        Name = s.PartnerName,
                        NameAlt = s.PartnerNameEn,
                        SeatCountry = country.Name,
                        SeatSettlement = settlement.DisplayName,
                        SeatPostCode = s.SeatPostCode,
                        SeatStreet = s.SeatStreet,
                        SeatAddress = s.SeatAddress,
                    }
                    by new
                    {
                        Uin = s.PartnerUin,
                        UinType = s.PartnerUinType,
                    }
                    into g

                    select new ContractSubcontractorVO
                    {
                        Id = g.Select(e => e.Id).Min(),
                        Uin = g.Key.Uin,
                        UinType = g.Key.UinType,
                        Name = g.Select(i => i.Name).Min(),
                        NameAlt = g.Select(i => i.NameAlt).Min(),
                        SeatCountry = g.Select(i => i.SeatCountry).Min(),
                        SeatSettlement = g.Select(i => i.SeatSettlement).Min(),
                        SeatPostCode = g.Select(i => i.SeatPostCode).Min(),
                        SeatStreet = g.Select(i => i.SeatStreet).Min(),
                        SeatAddress = g.Select(i => i.SeatAddress).Min(),
                    };
            }
            else
            {
                IQueryable<ContractSubcontract> subcontractors = this.unitOfWork.DbContext.Set<ContractSubcontract>();

                query =
                    from s in subcontractors
                    join c in this.unitOfWork.DbContext.Set<ContractContractor>() on s.ContractContractorId equals c.ContractContractorId
                    join country in this.unitOfWork.DbContext.Set<Country>() on c.SeatCountryId equals country.CountryId into j1
                    from country in j1.DefaultIfEmpty()
                    join settlement in this.unitOfWork.DbContext.Set<Settlement>() on c.SeatSettlementId equals settlement.SettlementId into j2
                    from settlement in j2.DefaultIfEmpty()
                    where s.Type == type && c.Uin == uin && c.UinType == uinType

                    group new
                    {
                        Id = c.ContractContractorId,
                        Name = c.Name,
                        NameAlt = c.NameAlt,
                        SeatCountry = country.Name,
                        SeatSettlement = settlement.DisplayName,
                        SeatPostCode = c.SeatPostCode,
                        SeatStreet = c.SeatStreet,
                        SeatAddress = c.SeatAddress,
                    }
                    by new
                    {
                        Uin = c.Uin,
                        UinType = c.UinType,
                    }
                    into g

                    select new ContractSubcontractorVO
                    {
                        Id = g.Select(e => e.Id).Min(),
                        Uin = g.Key.Uin,
                        UinType = g.Key.UinType,
                        Name = g.Select(i => i.Name).Min(),
                        NameAlt = g.Select(i => i.NameAlt).Min(),
                        SeatCountry = g.Select(i => i.SeatCountry).Min(),
                        SeatSettlement = g.Select(i => i.SeatSettlement).Min(),
                        SeatPostCode = g.Select(i => i.SeatPostCode).Min(),
                        SeatStreet = g.Select(i => i.SeatStreet).Min(),
                        SeatAddress = g.Select(i => i.SeatAddress).Min(),
                    };
            }

            var foundSubcontractor = query.FirstOrDefault();

            var result = new ContractSubcontractorVO();

            if (foundSubcontractor != null)
            {
                result.Id = foundSubcontractor.Id;
                result.Uin = foundSubcontractor.Uin;
                result.UinType = foundSubcontractor.UinType;
                result.Name = foundSubcontractor.Name;
                result.NameAlt = foundSubcontractor.NameAlt;
                result.SeatCountry = foundSubcontractor.SeatCountry;
                result.SeatSettlement = foundSubcontractor.SeatSettlement;
                result.SeatPostCode = foundSubcontractor.SeatPostCode;
                result.SeatStreet = foundSubcontractor.SeatStreet;
                result.SeatAddress = foundSubcontractor.SeatAddress;
            }

            return result;
        }

        public UsersCountVO GetUsersCount()
        {
            UsersCountVO result = new UsersCountVO();

            result.PortalUsersCount = this.unitOfWork.DbContext.Set<Registration>().Count();
            result.ReportUsersCount = this.unitOfWork.DbContext.Set<ContractRegistration>().Count();
            result.PrivateUsersCount = this.unitOfWork.DbContext.Set<User>().Count();

            return result;
        }

        public PageVO<UserStatisticsVO> GetUsersStatistics(int offset = 0, int? limit = null, bool isEn = false)
        {
            var procedureProgrammes = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                                       join pr in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals pr.MapNodeId
                                       select new
                                       {
                                           ps.ProcedureId,
                                           pr.ShortName,
                                           pr.PortalShortNameAlt,
                                       })
                                       .ToList()
                                       .GroupBy(g => g.ProcedureId)
                                       .ToDictionary(g => g.Key, g => g.Select(t => new
                                       {
                                           ShortName = t.ShortName,
                                           ShortNameAlt = t.PortalShortNameAlt,
                                       }));

            var query =
                from reg in this.unitOfWork.DbContext.Set<Registration>()

                join r in this.unitOfWork.DbContext.Set<RegProjectXml>() on reg.RegistrationId equals r.RegistrationId into j0
                from r in j0.DefaultIfEmpty()

                group new
                {
                    r.Status,
                    r.ProcedureId,
                }
                by new
                {
                    reg.RegistrationId,
                    reg.FirstName,
                    reg.LastName,
                    reg.Email,
                }
                into g

                select new
                {
                    g.Key.RegistrationId,
                    g.Key.FirstName,
                    g.Key.LastName,
                    g.Key.Email,
                    DraftProjectsCount = g.Count(e => e.Status == RegProjectXmlStatus.Draft || e.Status == RegProjectXmlStatus.Finalized),
                    DraftProjectsProcedureIds = g.Where(e => e.Status == RegProjectXmlStatus.Draft || e.Status == RegProjectXmlStatus.Finalized).Select(t => t.ProcedureId).Distinct(),

                    RegisteredProjectsCount = g.Count(e => e.Status == RegProjectXmlStatus.Registered || e.Status == RegProjectXmlStatus.Submitted),
                    RegisteredProjectsProcedureIds = g.Where(e => e.Status == RegProjectXmlStatus.Registered || e.Status == RegProjectXmlStatus.Submitted).Select(e => e.ProcedureId).Distinct(),
                };

            var queryWithOffsetAndLimit =
                query
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .WithOffsetAndLimit(offset, limit);

            return new PageVO<UserStatisticsVO>()
            {
                Results =
                    queryWithOffsetAndLimit
                    .ToList()
                    .Select(u => new UserStatisticsVO
                    {
                        UserId = u.RegistrationId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Username = u.Email,
                        DraftProjectsCount = u.DraftProjectsCount,
                        DraftOperationalProgrammes = u.DraftProjectsProcedureIds.SelectMany(e => isEn ? procedureProgrammes[e].Select(t => t.ShortNameAlt) : procedureProgrammes[e].Select(t => t.ShortName)).Distinct().OrderBy(e => e).ToList(),
                        RegisteredProjectsCount = u.RegisteredProjectsCount,
                        RegisteredOperationalProgrammes = u.RegisteredProjectsProcedureIds.SelectMany(e => isEn ? procedureProgrammes[e].Select(t => t.ShortNameAlt) : procedureProgrammes[e].Select(t => t.ShortName)).Distinct().OrderBy(e => e).ToList(),
                    })
                    .ToList(),
                Count = query.Count(),
            };
        }

        public OPStatisticsVO GetOPStatistics(int? programmeId = null)
        {
            OPStatisticsVO result = new OPStatisticsVO();

            var procedureSharePredicate = PredicateBuilder.True<ProcedureShare>()
                .AndEquals(ps => ps.ProgrammeId, programmeId);

            var procedureSharesSubquery = from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(procedureSharePredicate)
                                          select ps.ProcedureId;

            var procedureVersionsSubquery =
                from pv in this.unitOfWork.DbContext.Set<ProcedureVersion>()
                 group pv by pv.ProcedureId into g
                 select g.Key;

            var projects = (from p in this.unitOfWork.DbContext.Set<Procedure>()

                            join pj in this.unitOfWork.DbContext.Set<Project>().Where(t => t.RegistrationStatus != ProjectRegistrationStatus.Withdrawn) on p.ProcedureId equals pj.ProcedureId into j0
                            from pj in j0.DefaultIfEmpty()

                            join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(t => t.IsDeleted == false) on pj.ProjectId equals esps.ProjectId into j1
                            from esps in j1.DefaultIfEmpty()

                            where procedureSharesSubquery.Contains(p.ProcedureId) && procedureVersionsSubquery.Contains(p.ProcedureId) && p.ProcedureStatus != ProcedureStatus.Canceled

                            select new
                            {
                                ProjectId = (int?)pj.ProjectId,
                                BfpAmount = pj.TotalBfpAmount,
                                CoFinancingAmount = pj.CoFinancingAmount,
                                Status = (EvalSessionProjectStandingStatus?)esps.Status,
                            }).ToList();

            var contracts = (from pId in procedureSharesSubquery
                             join con in this.unitOfWork.DbContext.Set<Contract>() on pId equals con.ProcedureId

                             select new
                             {
                                 Status = con.ExecutionStatus,
                             }).ToList();

            var historicContracts = (from pId in procedureSharesSubquery
                                     join con in this.unitOfWork.DbContext.Set<HistoricContract>() on pId equals con.ProcedureId

                                     select new
                                     {
                                         Status = con.ExecutionStatus,
                                     }).ToList();

            var evalSessions = (from pId in procedureSharesSubquery
                                join eval in this.unitOfWork.DbContext.Set<EvalSession>() on pId equals eval.ProcedureId

                                select eval).AsEnumerable();

            result.ProceduresCount = (from p in this.unitOfWork.DbContext.Set<Procedure>()
                                      where procedureSharesSubquery.Contains(p.ProcedureId) && procedureVersionsSubquery.Contains(p.ProcedureId) && p.ProcedureStatus != ProcedureStatus.Canceled
                                      select p.ProcedureId)
                                      .Count();

            result.SubmittedProposalsCount = projects.Count(e => e.ProjectId.HasValue);
            result.ProposalsBFPAmount = projects.Sum(e => e.BfpAmount).Value;
            result.ProposalsSelfAmount = projects.Sum(e => e.CoFinancingAmount).Value;
            result.ProposalsTotalAmount = result.ProposalsBFPAmount + result.ProposalsSelfAmount;
            result.ApprovedProposalsCount = projects.Count(e => e.Status == EvalSessionProjectStandingStatus.Approved);
            result.ReserveProposalsCount = projects.Count(e => e.Status == EvalSessionProjectStandingStatus.Reserve);
            result.RejectedProposalsCount = projects.Count(e => e.Status == EvalSessionProjectStandingStatus.Rejected);

            result.ActiveContractsCount = contracts.Count(e => e.Status == ContractExecutionStatus.Active) + historicContracts.Count(e => e.Status == ContractExecutionStatus.Active);
            result.PausedContractsCount = contracts.Count(e => e.Status == ContractExecutionStatus.Paused) + historicContracts.Count(e => e.Status == ContractExecutionStatus.Paused);
            result.MonitoredContractsCount = contracts.Count(e => e.Status == ContractExecutionStatus.Monitored) + historicContracts.Count(e => e.Status == ContractExecutionStatus.Monitored);
            result.CanceledContractsCount = contracts.Count(e => e.Status == ContractExecutionStatus.Canceled) + historicContracts.Count(e => e.Status == ContractExecutionStatus.Canceled);
            result.EndedContractsCount = contracts.Count(e => e.Status == ContractExecutionStatus.Ended) + historicContracts.Count(e => e.Status == ContractExecutionStatus.Ended);
            result.ConcludedContractsCount = contracts.Count(e => e.Status == ContractExecutionStatus.Concluded) + historicContracts.Count(e => e.Status == ContractExecutionStatus.Concluded);
            result.SuspendedContractsCount = contracts.Count(e => e.Status == ContractExecutionStatus.Suspended) + historicContracts.Count(e => e.Status == ContractExecutionStatus.Suspended);
            result.TotalContractsCount = contracts.Count() + historicContracts.Count();

            result.EvalSessionsCount = evalSessions.Count();

            return result;
        }

        public ProgrammesProceduresStatisticsVO GetProgrammesProceduresStatistics(
            int? programmeId = null,
            int offset = 0,
            int? limit = null)
        {
            var procedureSharePredicate = PredicateBuilder.True<ProcedureShare>()
                .AndEquals(ps => ps.ProgrammeId, programmeId);

            var procedureSharesSubquery = from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(procedureSharePredicate)
                                          select ps.ProcedureId;

            var procedureVersionsSubquery =
                from pv in this.unitOfWork.DbContext.Set<ProcedureVersion>()
                 group pv by pv.ProcedureId into g
                 select g.Key;

            var projects = from p in this.unitOfWork.DbContext.Set<Procedure>()

                            join pj in this.unitOfWork.DbContext.Set<Project>() on p.ProcedureId equals pj.ProcedureId into g0
                            from pj in g0.DefaultIfEmpty()

                            where procedureSharesSubquery.Contains(p.ProcedureId) && procedureVersionsSubquery.Contains(p.ProcedureId)

                            group new
                            {
                                ProjectId = (int?)pj.ProjectId,
                            }
                            by new
                            {
                                p.ProcedureId,
                                p.Code,
                                p.Name,
                                p.NameAlt,
                            }
                                into g
                            select new ProjectProposalVO
                            {
                                ProcedureId = g.Key.ProcedureId,
                                Code = g.Key.Code,
                                Name = g.Key.Name,
                                NameAlt = g.Key.NameAlt,
                                ProjectCount = g.Count(t => t.ProjectId.HasValue),
                            };

            ProgrammesProceduresStatisticsVO result = new ProgrammesProceduresStatisticsVO();

            if (programmeId.HasValue)
            {
                var programme = (from p in this.unitOfWork.DbContext.Set<Programme>()
                                 where p.MapNodeId == programmeId.Value
                                 select p).Single();

                result.ProgrammeId = programme.MapNodeId;
                result.ProgrammeName = programme.PortalName;
                result.ProgrammeNameAlt = programme.PortalNameAlt;
            }

            var projectsWithOffsetAndLimit =
                projects
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
            result.PageProcedures = new PageVO<ProjectProposalVO>() { Count = projects.Count(), Results = projectsWithOffsetAndLimit };

            return result;
        }

        public ProjectStatisticsWrapperVO GetProcedureProjectsStatistics(
            int procedureId,
            int offset = 0,
            int? limit = null)
        {
            var projectFiles = from pvx in this.unitOfWork.DbContext.Set<ProjectVersionXml>()
                                join pf in this.unitOfWork.DbContext.Set<ProjectFile>() on pvx.ProjectVersionXmlId equals pf.ProjectVersionXmlId

                                select new { pvx.ProjectId, pf.ProjectFileId };

            var projects = from pj in this.unitOfWork.DbContext.Set<Project>()

                            join rpx in this.unitOfWork.DbContext.Set<RegProjectXml>() on pj.ProjectId equals rpx.ProjectId into g0
                            from rpx in g0.DefaultIfEmpty()

                            join pjf in projectFiles on pj.ProjectId equals pjf.ProjectId into g1
                            from pjf in g1.DefaultIfEmpty()

                            where pj.ProcedureId == procedureId

                            select new ProjectStatisticsVO
                            {
                                Name = pj.Name,
                                BeneficiaryName = pj.CompanyName,
                                UserId = (int?)rpx.RegistrationId,
                                ProjectFileId = (int?)pjf.ProjectFileId,
                            };

            ProjectStatisticsWrapperVO result = new ProjectStatisticsWrapperVO();

            var procedure = (from p in this.unitOfWork.DbContext.Set<Procedure>()
                             where p.ProcedureId == procedureId
                             select p).Single();

            result.ProcedureId = procedure.ProcedureId;
            result.ProcedureName = procedure.Name;
            result.ProcedureNameAlt = procedure.NameAlt;

            var projectsWithOffsetAndLimit =
                projects
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            result.PageProjects = new PageVO<ProjectStatisticsVO>() { Count = projects.Count(), Results = projectsWithOffsetAndLimit };

            return result;
        }

        public PageVO<EvalSessionAdminAdmissProjectVO> GetEvaluatedProjectsADS(int resultId, int offset = 0, int? limit = null)
        {
            var projectsResults = from esaap in this.unitOfWork.DbContext.Set<EvalSessionResultProject>()
                                   join esaar in this.unitOfWork.DbContext.Set<EvalSessionResult>().Where(x => x.EvalSessionResultId == resultId) on esaap.EvalSessionResultId equals esaar.EvalSessionResultId
                                   where esaar.Status == EvalSessionResultStatus.Archived || esaar.Status == EvalSessionResultStatus.Published
                                   select new EvalSessionAdminAdmissProjectVO
                                   {
                                       CompanyName = esaap.CompanyName,
                                       CompanyNameAlt = esaap.CompanyNameAlt,
                                       CompanyUin = esaap.CompanyUin,
                                       CompanyUinType = esaap.CompanyUinType,
                                       EvalSessioResultProjectId = esaap.EvalSessionResultProjectId,
                                       Name = esaap.ProjectName,
                                       NameAlt = esaap.ProjectNameAlt,
                                       NonAdmissionReason = esaap.NonAdmissionReason,
                                       AdminAdmissResult = esaap.EvaluationAdminAdmissResult != null && esaap.EvaluationAdminAdmissResult.Value ? EvalSessionAdminAdmissEvaluation.Passed : EvalSessionAdminAdmissEvaluation.NotPassed,
                                       RegNumber = esaap.ProjectRegNumber,
                                       ProjectId = esaap.ProjectId,
                                       RegDate = esaap.ProjectRegDate,
                                   };

            var projectsWithOffsetAndLimit = projectsResults
                .OrderByDescending(x => x.AdminAdmissResult)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return new PageVO<EvalSessionAdminAdmissProjectVO>() { Count = projectsResults.Count(), Results = projectsWithOffsetAndLimit };
        }

        public PageVO<EvalSessionPreliminaryProjectVO> GetPreliminaryProjects(int resultId, int offset = 0, int? limit = null)
        {
            var projectsResults = from esaap in this.unitOfWork.DbContext.Set<EvalSessionResultProject>()
                                   join esaar in this.unitOfWork.DbContext.Set<EvalSessionResult>().Where(x => x.EvalSessionResultId == resultId) on esaap.EvalSessionResultId equals esaar.EvalSessionResultId
                                   where esaar.Status == EvalSessionResultStatus.Archived || esaar.Status == EvalSessionResultStatus.Published
                                   select new EvalSessionPreliminaryProjectVO
                                   {
                                       CompanyName = esaap.CompanyName,
                                       CompanyNameAlt = esaap.CompanyNameAlt,
                                       CompanyUin = esaap.CompanyUin,
                                       CompanyUinType = esaap.CompanyUinType,
                                       EvalSessioResultProjectId = esaap.EvalSessionResultProjectId,
                                       Name = esaap.ProjectName,
                                       NameAlt = esaap.ProjectNameAlt,
                                       RegNumber = esaap.ProjectRegNumber,
                                       ProjectId = esaap.ProjectId,
                                       RegDate = esaap.ProjectRegDate,

                                       PreliminaryResult = esaap.StandingPreliminaryResult.HasValue && esaap.StandingPreliminaryResult.Value ? EvalSessionEvaluationResult.Passed : EvalSessionEvaluationResult.NotPassed,
                                       Points = esaap.StandingPreliminaryPoints,
                                       OrderNum = esaap.ProjectStandingNumber,
                                       Status = esaap.ProjectStandingStatus.Value,
                                       GrantAmount = esaap.GrantAmount ?? 0,
                                       SelfAmount = esaap.SelfAmount ?? 0,
                                       Note = esaap.Note,
                                   };

            var projectsWithOffsetAndLimit = projectsResults
                .OrderByDescending(x => x.PreliminaryResult)
                .ThenBy(x => x.Points)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return new PageVO<EvalSessionPreliminaryProjectVO>() { Count = projectsResults.Count(), Results = projectsWithOffsetAndLimit };
        }

        public PageVO<EvalSessionStandingProjectVO> GetStandingProjects(int resultId, int offset = 0, int? limit = null)
        {
            var projectsResults = from esaap in this.unitOfWork.DbContext.Set<EvalSessionResultProject>()
                                   join esaar in this.unitOfWork.DbContext.Set<EvalSessionResult>().Where(x => x.EvalSessionResultId == resultId) on esaap.EvalSessionResultId equals esaar.EvalSessionResultId
                                   where esaar.Status == EvalSessionResultStatus.Archived || esaar.Status == EvalSessionResultStatus.Published
                                   select new EvalSessionStandingProjectVO
                                   {
                                       CompanyName = esaap.CompanyName,
                                       CompanyNameAlt = esaap.CompanyNameAlt,
                                       CompanyUin = esaap.CompanyUin,
                                       CompanyUinType = esaap.CompanyUinType,
                                       EvalSessioResultProjectId = esaap.EvalSessionResultProjectId,
                                       Name = esaap.ProjectName,
                                       NameAlt = esaap.ProjectNameAlt,
                                       RegNumber = esaap.ProjectRegNumber,
                                       ProjectId = esaap.ProjectId,
                                       RegDate = esaap.ProjectRegDate,

                                       IsPassedPreliminary = esaap.StandingPreliminaryResult,
                                       PointsPreliminary = esaap.StandingPreliminaryPoints,
                                       IsPassedASD = esaap.EvaluationAdminAdmissResult,
                                       IsPassedTFO = esaap.StandingTechFinanceResult,
                                       PointsTFO = esaap.StandingTechFinancePoints,
                                       IsPassedComplex = esaap.StandingComplexResult,
                                       PointsComplex = esaap.StandingComplexPoints,
                                       OrderNum = esaap.ProjectStandingNumber,
                                       Status = esaap.ProjectStandingStatus.Value,
                                       SelfAmount = esaap.SelfAmount ?? 0,
                                       GrantAmount = esaap.GrantAmount ?? 0,
                                       CorrectedGrantAmount = esaap.GrantAmountCorrected ?? 0,
                                       CorrectedSelfAmount = esaap.SelfAmountCorrected ?? 0,
                                       Note = esaap.Note,
                                   };

            var projectsWithOffsetAndLimit = projectsResults
                .OrderByDescending(x => x.Status)
                .ThenBy(x => x.PointsTFO)
                .ThenBy(x => x.PointsComplex)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return new PageVO<EvalSessionStandingProjectVO>() { Count = projectsResults.Count(), Results = projectsWithOffsetAndLimit };
        }

        public List<EvalSessionResultVO> GetEvalSessionResults(int procedureId, EvalSessionResultType resultType)
        {
            return (from es in this.unitOfWork.DbContext.Set<EvalSession>().Where(x => x.ProcedureId == procedureId)
                    join esr in this.unitOfWork.DbContext.Set<EvalSessionResult>().Where(s => s.Status == EvalSessionResultStatus.Archived || s.Status == EvalSessionResultStatus.Published) on es.EvalSessionId equals esr.EvalSessionId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on es.ProcedureId equals p.ProcedureId
                    where esr.Type == resultType
                    select new EvalSessionResultVO
                    {
                        EvalSessionNum = es.SessionNum,
                        EvalSessionResultId = esr.EvalSessionResultId,
                        PublicationDate = esr.PublicationDate.Value,
                        Status = esr.Status,
                        Type = esr.Type,
                        ProcedureName = p.Name,
                        ProcedureNameAlt = p.NameAlt,
                    })
                    .OrderBy(x => x.Status)
                    .ThenByDescending(x => x.PublicationDate)
                    .ToList();
        }

        public EvalSessionResultVO GetEvalSessionResult(int evalSessionResultId)
        {
            return (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                    join esr in this.unitOfWork.DbContext.Set<EvalSessionResult>().Where(s => s.EvalSessionResultId == evalSessionResultId && (s.Status == EvalSessionResultStatus.Archived || s.Status == EvalSessionResultStatus.Published)) on es.EvalSessionId equals esr.EvalSessionId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on es.ProcedureId equals p.ProcedureId
                    select new EvalSessionResultVO
                    {
                        EvalSessionNum = es.SessionNum,
                        EvalSessionResultId = esr.EvalSessionResultId,
                        PublicationDate = esr.PublicationDate.Value,
                        Status = esr.Status,
                        Type = esr.Type,
                        ProcedureName = p.Name,
                        ProcedureNameAlt = p.NameAlt,
                    }).Single();
        }

        public int GetProgrammePriorityTotalContractsCount(List<int> procedureIds)
        {
            return this.unitOfWork.DbContext.Set<Contract>()
                .Where(c => procedureIds.Contains(c.ProcedureId) && c.ContractStatus == ContractStatus.Entered)
                .Count();
        }

        public int GetProgrammePriorityTotalCompaniesCount(List<int> procedureIds)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    .Where(c => procedureIds.Contains(c.ProcedureId) && c.ContractStatus == ContractStatus.Entered)
                    join b in this.unitOfWork.DbContext.Set<Company>() on c.CompanyId equals b.CompanyId
                    select b.Uin)
                    .Distinct()
                    .Count();
        }

        public OpenDataVO GetOpenDataResult(int programmeId)
        {
            var contracts = this.GetContracts(programmeId).ToDictionary(cb => cb.ContractId);

            var contractBeneficiariesByContract = this.GetContractBeneficiaries(programmeId).ToDictionary(cb => cb.ContractId);

            var contractPartnersByContract = this.GetContractPartners(programmeId).GroupBy(p => p.ContractId).ToDictionary(g => g.Key, g => g.AsEnumerable());

            var contractContracts = this.GetContractContracts(programmeId).ToDictionary(cc => cc.ContractContractId);

            var subcontracts = this.GetSubcontracts(programmeId);

            var subcontractsByContractContract = subcontracts.GroupBy(sc => sc.ContractContractId).ToDictionary(g => g.Key, g => g.AsEnumerable());

            var contractContractors = this.GetContractContractors(programmeId).ToDictionary(cc => cc.ContractContractorId);

            List<ExportProject> exportProjects = new List<ExportProject>();
            List<ExportContract> exportContracts = new List<ExportContract>();
            Dictionary<string, ExportEntity> exportEntities = new Dictionary<string, ExportEntity>();

            Action<EntityDO> tryAddEntity = (entity) =>
            {
                var id = entity.GetId();

                if (!exportEntities.ContainsKey(id))
                {
                    exportEntities.Add(id, entity.GetArachneEntity());
                }
            };

            foreach (var c in contracts.Values)
            {
                var beneficiary = contractBeneficiariesByContract[c.ContractId];

                tryAddEntity(beneficiary);

                var partners = contractPartnersByContract.ContainsKey(c.ContractId) ? contractPartnersByContract[c.ContractId] : Enumerable.Empty<EntityDO>();

                foreach (var p in partners)
                {
                    tryAddEntity(p);
                }

                var funds = string.Join(",", this.unitOfWork.DbContext.Set<ProcedureShare>()
                    .Where(e => e.ProcedureId == c.ProcedureId && e.ProgrammeId == programmeId)
                    .Select(e => e.FinanceSource)
                    .Distinct()
                    .AsEnumerable()
                    .Select(e => e.GetEnumDescription()));

                var actuallyParidAmounts = this.GetActuallyPaidAmounts(programmeId, c.ContractId);

                exportProjects.Add(
                    new ExportProject
                    {
                        Id = c.GetId(),
                        Status = c.ExecutionStatus.GetEnumDescription(),
                        SourceofFunding = funds,
                        Name = c.Name.TruncateWithEllipsis(300),
                        ProjectBeneficiaryEntityId = beneficiary.GetId(),
                        PlaceOfExecution = c.PlaceOfExecution,
                        InitialDate = c.StartDate ?? DateTime.MinValue,
                        EndDate = c.EndDate ?? DateTime.MinValue,
                        Description = c.Description,
                        DurationInMonths = c.Duration ?? 0,
                        ActuallyPaid = actuallyParidAmounts,
                        TotalValue = decimal.Round(c.TotalAmount ?? 0m, 2),
                        PartnerEntityIds = partners.Any() ? partners.Select(p => p.GetId()).ToArray() : null,
                    });
            }

            foreach (var cc in contractContracts.Values)
            {
                var contract = contracts[cc.ContractId];

                var contractContractor = contractContractors[cc.ContractContractorId];

                tryAddEntity(contractContractor);

                var subcontractsForContract =
                    subcontractsByContractContract.ContainsKey(cc.ContractContractId) ?
                        subcontractsByContractContract[cc.ContractContractId] :
                        Enumerable.Empty<ContractSubcontractDO>();

                var consortiumMembers = subcontractsForContract
                    .Where(sc => sc.Type == ContractSubcontractType.Member)
                    .Select(sc => contractContractors[sc.ContractContractorId]);

                foreach (var cm in consortiumMembers)
                {
                    tryAddEntity(cm);
                }

                exportContracts.Add(new ExportContract
                {
                    ContractId = contract.GetId() + $"({cc.Number})",
                    EntityId = contractContractor.GetId(),
                    SignatureDate = cc.SignDate,
                    InitialDate = cc.InitialDate,
                    EndDate = cc.EndDate,
                    Description = contract.Description,
                    Amount = decimal.Round(cc.TotalFundedValue, 2),
                    SubcontractEntityIds = consortiumMembers.Any() ? consortiumMembers.Select(cm => cm.GetId()).ToArray() : null,
                });
            }

            return new OpenDataVO
            {
                Projects = exportProjects.ToList(),
                Contracts = exportContracts.ToList(),
                Entities = exportEntities.Values.ToList(),
            };
        }

        private decimal[] GetActuallyPaidAmounts(int programmeId, int contractId)
        {
            return
                (from c in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                where c.ProgrammeId == programmeId && c.ContractId == contractId && c.PaidTotalAmount.HasValue
                select c.PaidTotalAmount.Value).ToArray();
        }

        private IQueryable<Contract> GetContractsQuery(int programmeId)
        {
            return
                from c in this.unitOfWork.DbContext.Set<Contract>()
                where c.ProgrammeId == programmeId && c.ContractStatus == ContractStatus.Entered
                select c;
        }

        private List<ContractDO> GetContracts(int programmeId)
        {
            return (
                from c in this.GetContractsQuery(programmeId)

                join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals cv.ContractId

                where cv.Status == ContractVersionStatus.Active

                select new ContractDO
                {
                    ContractId = c.ContractId,
                    ProcedureId = c.ProcedureId,
                    RegNumber = c.RegNumber,
                    ExecutionStatus = c.ExecutionStatus,
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.CompletionDate,
                    Duration = c.Duration,
                    Description = c.Description,
                    TotalAmount = c.TotalAmount,
                }).ToList();
        }

        private List<EntityDO> GetContractPartners(int programmeId)
        {
            return (
                from c in this.GetContractsQuery(programmeId)
                join cp in this.unitOfWork.DbContext.Set<ContractPartner>() on c.ContractId equals cp.ContractId

                join co in this.unitOfWork.DbContext.Set<Country>() on cp.SeatCountryId equals co.CountryId into g0
                from co in g0.DefaultIfEmpty()

                join s in this.unitOfWork.DbContext.Set<Settlement>() on cp.SeatSettlementId equals s.SettlementId into g1
                from s in g1.DefaultIfEmpty()

                select new EntityDO
                {
                    ContractId = c.ContractId,
                    Uin = cp.Uin,
                    UinType = cp.UinType,
                    Name = cp.Name,
                    SeatCountryCode = co.NutsCode,
                    SeatSettlement = s.Name,
                    SeatPostCode = cp.SeatPostCode,
                    SeatStreet = cp.SeatStreet,
                    SeatAddress = cp.SeatAddress,
                    MunicipalityName = s.Municipality.DisplayName,
                    EntityDistrictName = s.Municipality.District.Name,
                }).ToList();
        }

        private List<EntityDO> GetContractBeneficiaries(int programmeId)
        {
            return (
                from c in this.GetContractsQuery(programmeId)

                join co in this.unitOfWork.DbContext.Set<Country>() on c.BeneficiarySeatCountryId equals co.CountryId into g0
                from co in g0.DefaultIfEmpty()

                join s in this.unitOfWork.DbContext.Set<Settlement>() on c.BeneficiarySeatSettlementId equals s.SettlementId into g1
                from s in g1.DefaultIfEmpty()

                select new EntityDO
                {
                    ContractId = c.ContractId,
                    Uin = c.CompanyUin,
                    UinType = c.CompanyUinType,
                    Name = c.CompanyName,
                    SeatCountryCode = co.NutsCode,
                    SeatSettlement = s.Name,
                    SeatPostCode = c.BeneficiarySeatPostCode,
                    SeatStreet = c.BeneficiarySeatStreet,
                    SeatAddress = c.BeneficiarySeatAddress,
                    MunicipalityName = s.Municipality.DisplayName,
                    EntityDistrictName = s.Municipality.District.Name,
                }).ToList();
        }

        private List<ContractContractDO> GetContractContracts(int programmeId)
        {
            return (
                from c in this.GetContractsQuery(programmeId)

                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on c.ContractId equals cc.ContractId

                select new ContractContractDO
                {
                    ContractId = c.ContractId,
                    ContractContractId = cc.ContractContractId,
                    ContractContractorId = cc.ContractContractorId,

                    Number = cc.Number,
                    SignDate = cc.SignDate,
                    InitialDate = cc.StartDate,
                    EndDate = cc.EndDate,
                    TotalFundedValue = cc.TotalFundedValue,
                }).ToList();
        }

        private List<ContractSubcontractDO> GetSubcontracts(int programmeId)
        {
            var firstDifferentiatedPositions =
                from cdp in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>()

                group new
                {
                    cdp.ContractContractId,
                }
                by cdp.ContractContractId into g

                select g.FirstOrDefault();

            return (
                from c in this.GetContractsQuery(programmeId)

                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on c.ContractId equals cc.ContractId

                join cdp in firstDifferentiatedPositions on cc.ContractContractId equals cdp.ContractContractId

                join csc in this.unitOfWork.DbContext.Set<ContractSubcontract>() on cc.ContractContractId equals csc.ContractContractId

                select new ContractSubcontractDO
                {
                    ContractId = c.ContractId,
                    ContractContractId = cc.ContractContractId,
                    ContractContractorId = cc.ContractContractorId,
                    Number = csc.Number,
                    Amount = csc.Amount,
                    Type = csc.Type,
                }).ToList();
        }

        private List<EntityDO> GetContractContractors(int programmeId)
        {
            return (
                from c in this.GetContractsQuery(programmeId)

                join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on c.ContractId equals cctor.ContractId

                join co in this.unitOfWork.DbContext.Set<Country>() on cctor.SeatCountryId equals co.CountryId into g0
                from co in g0.DefaultIfEmpty()

                join s in this.unitOfWork.DbContext.Set<Settlement>() on cctor.SeatSettlementId equals s.SettlementId into g1
                from s in g1.DefaultIfEmpty()

                select new EntityDO
                {
                    ContractId = c.ContractId,
                    ContractContractorId = cctor.ContractContractorId,
                    Uin = cctor.Uin,
                    UinType = cctor.UinType,
                    Name = cctor.Name,
                    SeatCountryCode = co.NutsCode,
                    SeatSettlement = s.Name,
                    SeatPostCode = cctor.SeatPostCode,
                    SeatStreet = cctor.SeatStreet,
                    SeatAddress = cctor.SeatAddress,
                    MunicipalityName = s.Municipality.DisplayName,
                    EntityDistrictName = s.Municipality.District.Name,
                }).ToList();
        }

        public PageVO<ActuallyPaidAmountsVO> GetActuallyPaidAmounts(
            GroupingLevel groupingLevel,
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? dateTo = null,
            int offset = 0,
            int? limit = null)
        {
            if (dateTo.HasValue)
            {
                dateTo = dateTo.Value.AddDays(1).AddMilliseconds(-1);
            }
            else
            {
                dateTo = DateTime.Now.AddDays(1).AddMilliseconds(-1);
            }

            var predicate = PredicateBuilder.True<Contract>();
            predicate = predicate
                .AndEquals(t => t.ProgrammeId, programmeId)
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndDateTimeLessThanOrEqual(t => t.ContractDate, dateTo);

            var procedureSharePredicate = PredicateBuilder.True<ProcedureShare>();
            procedureSharePredicate = procedureSharePredicate
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndEquals(t => t.ProgrammeId, programmeId)
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var actuallyPaidAmountsPredicate = PredicateBuilder.True<ActuallyPaidAmount>();
            actuallyPaidAmountsPredicate = actuallyPaidAmountsPredicate
                .And(a => a.Status == ActuallyPaidAmountStatus.Entered)
                .And(a => a.PaymentDate != null)
                .AndDateTimeLessThanOrEqual(t => t.PaymentDate, dateTo);

            var subqueryPredicate = PredicateBuilder.True<ProcedureShare>();

            if (programmePriorityId.HasValue)
            {
                subqueryPredicate = subqueryPredicate.And(ps => ps.ProgrammePriorityId == programmePriorityId);
            }

            var subquery = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(subqueryPredicate)
                            select ps.ProcedureId).Distinct();

            var contracts =
                from c in this.unitOfWork.DbContext.Set<Contract>()
                join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals cv.ContractId
                join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                where cv.Status == ContractVersionStatus.Active && subquery.Contains(p.ProcedureId)
                select new
                {
                    ContractId = c.ContractId,
                    RegNumber = c.RegNumber,
                    ProcedureId = c.ProcedureId,
                    IsHistoric = false,
                };

            var amountPredicate = PredicateBuilder.True<ContractAmountDO>();
            amountPredicate = amountPredicate
                .AndEquals(t => t.ProgrammeId, programmeId)
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId)
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndDateTimeLessThanOrEqual(t => t.ContractDate, dateTo);

            IQueryable<ContractAmountDO> amounts = this.GetActualContractContractedAmounts().Where(amountPredicate);

            var contractAmounts = this.GetActualContractContractedAmounts()
                .Where(amountPredicate)
                .Where(t => t.ContractId.HasValue && contracts.Select(p => p.ContractId).Contains(t.ContractId.Value));

            Func<IQueryable<IGrouping<ContractedAmountGroupingItem, ContractAmountDO>>, IQueryable<ContractedAmountItem>> amountsMaker = (ca) =>
            {
                return ca
                .Select(t => new ContractedAmountItem
                {
                    ProgrammeId = t.Key.ProgrammeId,
                    ProgrammePriorityId = t.Key.ProgrammePriorityId,
                    ProcedureId = t.Key.ProcedureId,
                    ContractId = t.Key.ContractId,
                    IsHistoric = t.Key.IsHistoric,

                    ContractedSelfAmount = t.Sum(p => p.ContractedSelfAmount),
                    ContractedEuAmount = t.Sum(p => p.ContractedEuAmount),
                    ContractedBgAmount = t.Sum(p => p.ContractedBgAmount),
                    ContractedTotalAmount = t.Sum(p => p.ContractedTotalAmount),
                });
            };

            var allPaidAmounts = from apa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(actuallyPaidAmountsPredicate)
                                      join c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate) on apa.ContractId equals c.ContractId
                                      join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                                      where p.ProcedureStatus != ProcedureStatus.Canceled
                                      select new
                                      {
                                          ProgrammeId = apa.ProgrammeId,
                                          ProgrammePriorityId = apa.ProgrammePriorityId,
                                          ProcedureId = c.ProcedureId,
                                          ContractId = c.ContractId,
                                          ActuallyPaidEuAmount = apa.PaidBfpEuAmount ?? 0m,
                                          ActuallyPaidBgAmount = apa.PaidBfpBgAmount ?? 0m,
                                          ActuallyPaidTotalAmount = (apa.PaidBfpEuAmount ?? 0m) + (apa.PaidBfpBgAmount ?? 0m),
                                      };

            var debtReimbursedAmounts = from ra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                                        join d in this.unitOfWork.DbContext.Set<ContractDebt>() on ra.ContractDebtId equals d.ContractDebtId
                                        join c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate) on ra.ContractId equals c.ContractId
                                        join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                                        where p.ProcedureStatus != ProcedureStatus.Canceled && ra.Status == ReimbursedAmountStatus.Entered && ReportsReimbursements.Contains(ra.Reimbursement) && ra.ReimbursementDate <= dateTo && !ra.ProgrammePriorityId.HasValue
                                        select new
                                        {
                                            ProgrammeId = ra.ProgrammeId,
                                            ProgrammePriorityId = d.ProgrammePriorityId,
                                            ProcedureId = c.ProcedureId,
                                            ContractId = c.ContractId,
                                            TotalAmount = (ra.PrincipalBfp.EuAmount ?? 0m) + (ra.PrincipalBfp.BgAmount ?? 0m),
                                            EuAmount = ra.PrincipalBfp.EuAmount ?? 0m,
                                            BgAmount = ra.PrincipalBfp.BgAmount ?? 0m,
                                        };

            var contractReimbursedAmounts = from ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>()
                                            join c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate) on ra.ContractId equals c.ContractId
                                            join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                                            where p.ProcedureStatus != ProcedureStatus.Canceled && ra.Status == ReimbursedAmountStatus.Entered && ReportsReimbursements.Contains(ra.Reimbursement) && ra.ReimbursementDate <= dateTo && ra.ProgrammePriorityId.HasValue
                                            select new
                                            {
                                                ProgrammeId = ra.ProgrammeId,
                                                ProgrammePriorityId = ra.ProgrammePriorityId.Value,
                                                ProcedureId = c.ProcedureId,
                                                ContractId = c.ContractId,
                                                TotalAmount = (ra.PrincipalBfp.EuAmount ?? 0m) + (ra.PrincipalBfp.BgAmount ?? 0m),
                                                EuAmount = ra.PrincipalBfp.EuAmount ?? 0m,
                                                BgAmount = ra.PrincipalBfp.BgAmount ?? 0m,
                                            };

            var allReimbursedAmounts = contractReimbursedAmounts
            .Concat(debtReimbursedAmounts);

            IQueryable<ActuallyPaidAmountsVO> results = null;
            if (groupingLevel == GroupingLevel.Contract)
            {
                var groupedAmounts = amountsMaker(contractAmounts
                .GroupBy(t => new ContractedAmountGroupingItem
                {
                    ProgrammeId = t.ProgrammeId,
                    ProcedureId = t.ProcedureId,
                    ContractId = t.ContractId,
                    ProgrammePriorityId = t.ProgrammePriorityId,
                    IsHistoric = null,
                }));

                var currentReimbursedAmounts = allReimbursedAmounts
                .GroupBy(g => new
                {
                    g.ProgrammeId,
                    g.ProcedureId,
                    g.ContractId,
                    g.ProgrammePriorityId,
                })
                .Select(g => new
                {
                    ProgrammeId = g.Key.ProgrammeId,
                    ProcedureId = g.Key.ProcedureId,
                    ContractId = g.Key.ContractId,
                    ProgrammePriorityId = g.Key.ProgrammePriorityId,
                    EuAmount = g.Sum(i => i.EuAmount),
                    BgAmount = g.Sum(i => i.BgAmount),
                    TotalAmount = g.Sum(i => i.TotalAmount),
                });

                var currentPaidAmounts = allPaidAmounts
                .GroupBy(g => new
                {
                    g.ProgrammeId,
                    g.ProcedureId,
                    g.ContractId,
                    g.ProgrammePriorityId,
                })
                .Select(g => new
                {
                    ProgrammeId = g.Key.ProgrammeId,
                    ProcedureId = g.Key.ProcedureId,
                    ContractId = g.Key.ContractId,
                    ProgrammePriorityId = g.Key.ProgrammePriorityId,
                    ActuallyPaidEuAmount = g.Sum(i => i.ActuallyPaidEuAmount),
                    ActuallyPaidBgAmount = g.Sum(i => i.ActuallyPaidBgAmount),
                    ActuallyPaidTotalAmount = g.Sum(i => i.ActuallyPaidTotalAmount),
                });

                results = from a in groupedAmounts
                          join p in this.unitOfWork.DbContext.Set<Programme>() on a.ProgrammeId equals p.MapNodeId
                          join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on a.ProgrammePriorityId equals pp.MapNodeId
                          join pr in this.unitOfWork.DbContext.Set<Procedure>() on a.ProcedureId equals pr.ProcedureId
                          join c in contracts on a.ContractId equals c.ContractId

                          join apa in currentPaidAmounts on new { ProgrammeId = a.ProgrammeId.Value, ProcedureId = a.ProcedureId.Value, ContractId = a.ContractId.Value, ProgrammePriorityId = a.ProgrammePriorityId.Value } equals
                                                            new { apa.ProgrammeId, apa.ProcedureId, apa.ContractId, apa.ProgrammePriorityId } into g2
                          from apa in g2.DefaultIfEmpty()

                          join ra in currentReimbursedAmounts on new { ProgrammeId = a.ProgrammeId.Value, ProcedureId = a.ProcedureId.Value, ContractId = a.ContractId.Value, ProgrammePriorityId = a.ProgrammePriorityId.Value } equals
                                                                 new { ra.ProgrammeId, ra.ProcedureId, ra.ContractId, ra.ProgrammePriorityId } into g3
                          from ra in g3.DefaultIfEmpty()

                          orderby c.RegNumber
                          select new ActuallyPaidAmountsVO
                          {
                              ContractRegNumber = c.RegNumber,
                              ProgrammeName = p.Name,
                              ProgrammePriorityName = pp.Name,
                              ProcedureNumber = pr != null ? pr.Code : null,
                              ProcedureName = pr != null ? pr.Name : null,
                              ContractedSelfAmount = a.ContractedSelfAmount ?? 0m,
                              ContractedEuAmount = a.ContractedEuAmount ?? 0m,
                              ContractedBgAmount = a.ContractedBgAmount ?? 0m,
                              ContractedTotalAmount = a.ContractedTotalAmount ?? 0m,
                              ActuallyPaidEuAmount = (apa != null ? apa.ActuallyPaidEuAmount : 0m) - (ra != null ? ra.EuAmount : 0m),
                              ActuallyPaidBgAmount = (apa != null ? apa.ActuallyPaidBgAmount : 0m) - (ra != null ? ra.BgAmount : 0m),
                              ActuallyPaidTotalAmount = (apa != null ? apa.ActuallyPaidTotalAmount : 0m) - (ra != null ? ra.TotalAmount : 0m),
                          };
            }
            else if (groupingLevel == GroupingLevel.Procedure)
            {
                var groupedAmounts = amountsMaker(amounts
                .GroupBy(t => new ContractedAmountGroupingItem
                {
                    ProgrammeId = t.ProgrammeId,
                    ProcedureId = t.ProcedureId,
                    ContractId = null,
                    ProgrammePriorityId = t.ProgrammePriorityId,
                    IsHistoric = null,
                }));

                var currentReimbursedAmounts = allReimbursedAmounts
                .GroupBy(g => new
                {
                    g.ProgrammeId,
                    g.ProcedureId,
                    g.ProgrammePriorityId,
                })
                .Select(g => new
                {
                    ProgrammeId = g.Key.ProgrammeId,
                    ProcedureId = g.Key.ProcedureId,
                    ProgrammePriorityId = g.Key.ProgrammePriorityId,
                    EuAmount = g.Sum(i => i.EuAmount),
                    BgAmount = g.Sum(i => i.BgAmount),
                    TotalAmount = g.Sum(i => i.TotalAmount),
                });

                var currentPaidAmounts = allPaidAmounts
                .GroupBy(g => new
                {
                    g.ProgrammeId,
                    g.ProcedureId,
                    g.ProgrammePriorityId,
                })
                .Select(g => new
                {
                    ProgrammeId = g.Key.ProgrammeId,
                    ProcedureId = g.Key.ProcedureId,
                    ProgrammePriorityId = g.Key.ProgrammePriorityId,
                    ActuallyPaidEuAmount = g.Sum(i => i.ActuallyPaidEuAmount),
                    ActuallyPaidBgAmount = g.Sum(i => i.ActuallyPaidBgAmount),
                    ActuallyPaidTotalAmount = g.Sum(i => i.ActuallyPaidTotalAmount),
                });

                results =
                    from p in this.unitOfWork.DbContext.Set<Procedure>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(procedureSharePredicate) on p.ProcedureId equals ps.ProcedureId
                    join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                    where p.ActivationDate.HasValue

                    join a in groupedAmounts on new { ps.ProgrammeId, ps.ProcedureId, ps.ProgrammePriorityId } equals new { ProgrammeId = a.ProgrammeId.Value, ProcedureId = a.ProcedureId.Value, ProgrammePriorityId = a.ProgrammePriorityId.Value } into g0
                    from a in g0.DefaultIfEmpty()

                    join apa in currentPaidAmounts on new { ps.ProgrammeId, ps.ProcedureId, ps.ProgrammePriorityId } equals
                                                      new { apa.ProgrammeId, apa.ProcedureId, apa.ProgrammePriorityId } into g1
                    from apa in g1.DefaultIfEmpty()

                    join ra in currentReimbursedAmounts on new { ps.ProgrammeId, ps.ProcedureId, ps.ProgrammePriorityId } equals
                                                           new { ra.ProgrammeId, ra.ProcedureId, ra.ProgrammePriorityId } into g2
                    from ra in g2.DefaultIfEmpty()

                    orderby p.Name
                    select new ActuallyPaidAmountsVO
                    {
                        ContractRegNumber = null,
                        ProgrammeName = prog.Name,
                        ProcedureNumber = p != null ? p.Code : null,
                        ProcedureName = p != null ? p.Name : null,
                        ProgrammePriorityName = pp.Name,
                        ContractedSelfAmount = a.ContractedSelfAmount ?? 0m,
                        ContractedEuAmount = a.ContractedEuAmount ?? 0m,
                        ContractedBgAmount = a.ContractedBgAmount ?? 0m,
                        ContractedTotalAmount = a.ContractedTotalAmount ?? 0m,
                        ActuallyPaidEuAmount = (apa != null ? apa.ActuallyPaidEuAmount : 0m) - (ra != null ? ra.EuAmount : 0m),
                        ActuallyPaidBgAmount = (apa != null ? apa.ActuallyPaidBgAmount : 0m) - (ra != null ? ra.BgAmount : 0m),
                        ActuallyPaidTotalAmount = (apa != null ? apa.ActuallyPaidTotalAmount : 0m) - (ra != null ? ra.TotalAmount : 0m),
                    };
            }
            else if (groupingLevel == GroupingLevel.ProgrammePriority)
            {
                var programmePredicate = PredicateBuilder.True<Programme>();
                programmePredicate = programmePredicate
                    .AndEquals(t => t.MapNodeId, programmeId);

                var programmePriorityPredicate = PredicateBuilder.True<ProgrammePriority>();
                programmePriorityPredicate = programmePriorityPredicate
                    .AndEquals(t => t.MapNodeId, programmePriorityId);

                var groupedAmounts = amountsMaker(amounts
                .GroupBy(t => new ContractedAmountGroupingItem
                {
                    ProgrammeId = t.ProgrammeId,
                    ProcedureId = null,
                    ContractId = null,
                    ProgrammePriorityId = t.ProgrammePriorityId,
                    IsHistoric = null,
                }));

                var currentReimbursedAmounts = allReimbursedAmounts
                .GroupBy(g => new
                {
                    g.ProgrammeId,
                    g.ProgrammePriorityId,
                })
                .Select(g => new
                {
                    ProgrammeId = g.Key.ProgrammeId,
                    ProgrammePriorityId = g.Key.ProgrammePriorityId,
                    EuAmount = g.Sum(i => i.EuAmount),
                    BgAmount = g.Sum(i => i.BgAmount),
                    TotalAmount = g.Sum(i => i.TotalAmount),
                });

                var currentPaidAmounts = allPaidAmounts
                .GroupBy(g => new
                {
                    g.ProgrammeId,
                    g.ProgrammePriorityId,
                })
                .Select(g => new
                {
                    ProgrammeId = g.Key.ProgrammeId,
                    ProgrammePriorityId = g.Key.ProgrammePriorityId,
                    ActuallyPaidEuAmount = g.Sum(i => i.ActuallyPaidEuAmount),
                    ActuallyPaidBgAmount = g.Sum(i => i.ActuallyPaidBgAmount),
                    ActuallyPaidTotalAmount = g.Sum(i => i.ActuallyPaidTotalAmount),
                });

                results =
                    from p in this.unitOfWork.DbContext.Set<Programme>().Where(programmePredicate)
                    join mnb in this.unitOfWork.DbContext.Set<MapNodeRelation>() on p.MapNodeId equals mnb.ProgrammeId
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>().Where(programmePriorityPredicate) on mnb.ProgrammePriorityId equals pp.MapNodeId

                    join a in groupedAmounts on new { mnb.ProgrammeId, mnb.ProgrammePriorityId } equals new { a.ProgrammeId, a.ProgrammePriorityId } into g0
                    from a in g0.DefaultIfEmpty()

                    join apa in currentPaidAmounts on new { ProgrammeId = mnb.ProgrammeId.Value, ProgrammePriorityId = mnb.ProgrammePriorityId.Value } equals new { apa.ProgrammeId, apa.ProgrammePriorityId } into g1
                    from apa in g1.DefaultIfEmpty()

                    join ra in currentReimbursedAmounts on new { ProgrammeId = mnb.ProgrammeId.Value, ProgrammePriorityId = mnb.ProgrammePriorityId.Value } equals new { ra.ProgrammeId, ra.ProgrammePriorityId } into g2
                    from ra in g2.DefaultIfEmpty()

                    orderby p.Name, pp.Name
                    select new ActuallyPaidAmountsVO
                    {
                        ContractRegNumber = null,
                        ProgrammeName = p.Name,
                        ProcedureNumber = null,
                        ProcedureName = null,
                        ProgrammePriorityName = pp.Name,
                        ContractedSelfAmount = a.ContractedSelfAmount,
                        ContractedEuAmount = a.ContractedEuAmount ?? 0m,
                        ContractedBgAmount = a.ContractedBgAmount ?? 0m,
                        ContractedTotalAmount = a.ContractedTotalAmount ?? 0m,
                        ActuallyPaidEuAmount = (apa != null ? apa.ActuallyPaidEuAmount : 0m) - (ra != null ? ra.EuAmount : 0m),
                        ActuallyPaidBgAmount = (apa != null ? apa.ActuallyPaidBgAmount : 0m) - (ra != null ? ra.BgAmount : 0m),
                        ActuallyPaidTotalAmount = (apa != null ? apa.ActuallyPaidTotalAmount : 0m) - (ra != null ? ra.TotalAmount : 0m),
                    };
            }
            else if (groupingLevel == GroupingLevel.Programme)
            {
                var programmePredicate = PredicateBuilder.True<Programme>();
                programmePredicate = programmePredicate
                    .AndEquals(t => t.MapNodeId, programmeId);

                var groupedAmounts = amountsMaker(amounts
                .GroupBy(t => new ContractedAmountGroupingItem
                {
                    ProgrammeId = t.ProgrammeId,
                    ProcedureId = null,
                    ContractId = null,
                    ProgrammePriorityId = null,
                    IsHistoric = null,
                }));

                var currentReimbursedAmounts = allReimbursedAmounts
                .GroupBy(g => g.ProgrammeId)
                .Select(g => new
                {
                    ProgrammeId = g.Key,
                    EuAmount = g.Sum(i => i.EuAmount),
                    BgAmount = g.Sum(i => i.BgAmount),
                    TotalAmount = g.Sum(i => i.TotalAmount),
                });

                var currentPaidAmounts = allPaidAmounts
                .GroupBy(g => g.ProgrammeId)
                .Select(g => new
                {
                    ProgrammeId = g.Key,
                    ActuallyPaidEuAmount = g.Sum(i => i.ActuallyPaidEuAmount),
                    ActuallyPaidBgAmount = g.Sum(i => i.ActuallyPaidBgAmount),
                    ActuallyPaidTotalAmount = g.Sum(i => i.ActuallyPaidTotalAmount),
                });

                results =
                    from prog in this.unitOfWork.DbContext.Set<Programme>().Where(programmePredicate)

                    join a in groupedAmounts on prog.MapNodeId equals a.ProgrammeId.Value into g0
                    from a in g0.DefaultIfEmpty()

                    join apa in currentPaidAmounts on new { ProgrammeId = prog.MapNodeId } equals new { apa.ProgrammeId } into g1
                    from apa in g1.DefaultIfEmpty()

                    join ra in currentReimbursedAmounts on new { ProgrammeId = prog.MapNodeId } equals new { ra.ProgrammeId } into g2
                    from ra in g2.DefaultIfEmpty()
                    orderby prog.Name
                    select new ActuallyPaidAmountsVO
                    {
                        ContractRegNumber = null,
                        ProgrammeName = prog.Name,
                        ProgrammePriorityName = null,
                        ProcedureNumber = null,
                        ProcedureName = null,
                        ContractedSelfAmount = a.ContractedSelfAmount ?? 0m,
                        ContractedEuAmount = a.ContractedEuAmount ?? 0m,
                        ContractedBgAmount = a.ContractedBgAmount ?? 0m,
                        ContractedTotalAmount = a.ContractedTotalAmount ?? 0m,
                        ActuallyPaidEuAmount = (apa != null ? apa.ActuallyPaidEuAmount : 0m) - (ra != null ? ra.EuAmount : 0m),
                        ActuallyPaidBgAmount = (apa != null ? apa.ActuallyPaidBgAmount : 0m) - (ra != null ? ra.BgAmount : 0m),
                        ActuallyPaidTotalAmount = (apa != null ? apa.ActuallyPaidTotalAmount : 0m) - (ra != null ? ra.TotalAmount : 0m),
                    };
            }
            else
            {
                throw new DataException("GroupingLevel not recognized");
            }

            var actuallyPaidAmountsWithOffsetAndLimit = results
                .Distinct()
                .OrderBy(x => x.ProgrammeName)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return new PageVO<ActuallyPaidAmountsVO>() { Count = results.Count(), Results = actuallyPaidAmountsWithOffsetAndLimit };
        }

        private IQueryable<ContractAmountDO> GetActualContractContractedAmounts()
        {
            return from cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>()
                   join pbl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pbl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pbl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join c in this.unitOfWork.DbContext.Set<Contract>() on cbl3a.ContractId equals c.ContractId
                   where cbl3a.IsActive
                   select new ContractAmountDO
                   {
                       Id = cbl3a.ContractBudgetLevel3AmountId,
                       ContractId = cbl3a.ContractId,
                       ContractDate = c.ContractDate,
                       ProgrammeId = c.ProgrammeId,
                       ProcedureId = c.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = cbl3a.ContractBudgetLevel3AmountId,
                       ContractedSelfAmount = cbl3a.CurrentSelfAmount,
                       ContractedEuAmount = cbl3a.CurrentEuAmount,
                       ContractedBgAmount = cbl3a.CurrentBgAmount,
                       ContractedTotalAmount = cbl3a.CurrentEuAmount + cbl3a.CurrentBgAmount + cbl3a.CurrentSelfAmount,
                       IsHistoric = false,
                   };
        }

        public List<IndicativeAnnualWorkingProgrammeVO> GetIndicativeAnnualWorkingProgrammes(
            int? programmeId = null,
            IndicativeAnnualWorkingProgrammeYear? year = null,
            IndicativeAnnualWorkingProgrammeType? type = null)
        {
            var predicate = PredicateBuilder.True<IndicativeAnnualWorkingProgramme>();

            predicate = predicate
                .AndEquals(i => i.ProgrammeId, programmeId)
                .And(i => i.Status == IndicativeAnnualWorkingProgrammeStatus.Published || i.Status == IndicativeAnnualWorkingProgrammeStatus.Archived);

            if (year != null)
            {
                predicate = predicate.And(i => i.Year == year);
            }

            if (type != null)
            {
                predicate = predicate.And(i => i.Type == type);
            }

            return (from iawp in this.unitOfWork.DbContext.Set<IndicativeAnnualWorkingProgramme>().Where(predicate)
                    join p in this.unitOfWork.DbContext.Set<Programme>() on iawp.ProgrammeId equals p.MapNodeId

                    orderby iawp.ProgrammeId, iawp.OrderVersionNum descending
                    select new IndicativeAnnualWorkingProgrammeVO
                    {
                        IndicativeAnnualWorkingProgrammeId = iawp.IndicativeAnnualWorkingProgrammeId,
                        ProgrammeId = iawp.ProgrammeId,
                        ProgrammeName = p.Name,
                        ProgrammeNameAlt = p.NameAlt,
                        OrderVersionNum = iawp.OrderVersionNum,
                        Year = iawp.Year,
                        PublicationDate = iawp.PublicationDate.Value,
                        Type = iawp.Type,
                        Status = iawp.Status,
                    })
                .ToList();
        }

        public IndicativeAnnualWorkingProgrammeVO GetIndicativeAnnualWorkingProgramme(int indicativeAnnualWorkingProgrammeId)
        {
            return (from iawp in this.unitOfWork.DbContext.Set<IndicativeAnnualWorkingProgramme>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on iawp.ProgrammeId equals pr.MapNodeId
                    where iawp.IndicativeAnnualWorkingProgrammeId == indicativeAnnualWorkingProgrammeId
                    select new IndicativeAnnualWorkingProgrammeVO
                    {
                        IndicativeAnnualWorkingProgrammeId = iawp.IndicativeAnnualWorkingProgrammeId,
                        ProgrammeId = iawp.ProgrammeId,
                        ProgrammeName = pr.Name,
                        ProgrammeNameAlt = pr.NameAlt,
                        OrderVersionNum = iawp.OrderVersionNum,
                        Year = iawp.Year,
                        PublicationDate = iawp.PublicationDate.Value,
                        Type = iawp.Type,
                        Status = iawp.Status,
                    }).Single();
        }

        public PageVO<IndicativeAnnualWorkingProgrammeTableVO> GetIndicativeAnnualWorkingProgrammeTable(
            int iawpId,
            int offset = 0,
            int? limit = null)
        {
            var indicativeAnnualWorkingProgrammeTables =
                (from iawp in this.unitOfWork.DbContext.Set<IndicativeAnnualWorkingProgramme>()
                 join iawpt in this.unitOfWork.DbContext.Set<IndicativeAnnualWorkingProgrammeTable>() on iawp.IndicativeAnnualWorkingProgrammeId equals iawpt.IndicativeAnnualWorkingProgrammeId
                 join p in this.unitOfWork.DbContext.Set<Procedure>() on iawpt.ProcedureId equals p.ProcedureId
                 join pr in this.unitOfWork.DbContext.Set<Programme>() on iawp.ProgrammeId equals pr.MapNodeId
                 join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on iawpt.ProgrammePriorityId equals pp.MapNodeId
                 where iawp.IndicativeAnnualWorkingProgrammeId == iawpId
                 select new IndicativeAnnualWorkingProgrammeTableVO
                 {
                     IndicativeAnnualWorkingProgrammeTableId = iawpt.IndicativeAnnualWorkingProgrammeTableId,
                     ProcedureId = p.ProcedureId,
                     ProgrammePriorityName = pp.Name,
                     ProgrammePriorityNameAlt = pp.NameAlt,
                     OrderNum = iawpt.OrderNum,
                     ProcedureCode = p.Code,
                     ProcedureName = iawpt.ProcedureName,
                     ProcedureNameAlt = iawpt.ProcedureNameAlt,
                     ProcedureDescription = iawpt.ProcedureDescription,
                     ProcedureDescriptionAlt = iawpt.ProcedureDescriptionAlt,
                     IndicativeAnnualWorkingProgrammeTypeConducting = iawpt.IndicativeAnnualWorkingProgrammeTypeConducting,
                     WithPreSelection = iawpt.WithPreSelection,
                     ProcedureTotalAmount = iawpt.ProcedureTotalAmount,
                     EligibleActivities = iawpt.EligibleActivities,
                     EligibleActivitiesAlt = iawpt.EligibleActivitiesAlt,
                     EligibleCosts = iawpt.EligibleCosts,
                     EligibleCostsAlt = iawpt.EligibleCostsAlt,
                     MaxPercentCoFinancing = iawpt.MaxPercentCoFinancing,
                     MaxPercentCoFinancingInfo = iawpt.MaxPercentCoFinancingInfo,
                     MaxPercentCoFinancingInfoAlt = iawpt.MaxPercentCoFinancingInfoAlt,
                     ListingDate = iawpt.ListingDate,
                     IsStateAssistance = iawpt.IsStateAssistance,
                     IsMinimalAssistance = iawpt.IsMinimalAssistance,
                     ProjectMinAmount = iawpt.ProjectMinAmount,
                     ProjectMinAmountInfo = iawpt.ProjectMinAmountInfo,
                     ProjectMinAmountInfoAlt = iawpt.ProjectMinAmountInfoAlt,
                     ProjectMaxAmount = iawpt.ProjectMaxAmount,
                     ProjectMaxAmountInfo = iawpt.ProjectMaxAmountInfo,
                     ProjectMaxAmountInfoAlt = iawpt.ProjectMaxAmountInfoAlt,
                 })
                 .Distinct()
                 .ToList();

            var indicativeAnnualWorkingProgrammeTableIds = indicativeAnnualWorkingProgrammeTables.Select(i => i.IndicativeAnnualWorkingProgrammeTableId).ToArray();

            var programmes =
                        (from iawpt in indicativeAnnualWorkingProgrammeTables
                         join iawptp in this.unitOfWork.DbContext.Set<IndicativeAnnualWorkingProgrammeTableProgramme>() on iawpt.IndicativeAnnualWorkingProgrammeTableId equals iawptp.IndicativeAnnualWorkingProgrammeTableId
                         join p in this.unitOfWork.DbContext.Set<Programme>() on iawptp.ProgrammeId equals p.MapNodeId

                         where indicativeAnnualWorkingProgrammeTableIds.Contains(iawpt.IndicativeAnnualWorkingProgrammeTableId)
                         select new
                         {
                             iawptp.IndicativeAnnualWorkingProgrammeTableId,
                             p.ShortName,
                             ShortNameAlt = p.PortalShortNameAlt,
                         })
                         .ToList();

            var candidates =
                        (from iawpt in indicativeAnnualWorkingProgrammeTables
                         join iawptc in this.unitOfWork.DbContext.Set<IndicativeAnnualWorkingProgrammeTableCandidate>() on iawpt.IndicativeAnnualWorkingProgrammeTableId equals iawptc.IndicativeAnnualWorkingProgrammeTableId

                         join ct in this.unitOfWork.DbContext.Set<CompanyType>() on iawptc.CompanyTypeId equals ct.CompanyTypeId into g1
                         from ct in g1.DefaultIfEmpty()

                         join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on iawptc.CompanyLegalTypeId equals clt.CompanyLegalTypeId into g2
                         from clt in g2.DefaultIfEmpty()

                         where indicativeAnnualWorkingProgrammeTableIds.Contains(iawpt.IndicativeAnnualWorkingProgrammeTableId)
                         select new
                         {
                             IndicativeAnnualWorkingProgrammeTableId = iawptc.IndicativeAnnualWorkingProgrammeTableId,
                             IsCompany = ct == null,
                             CandidateName = ct != null ?
                                    (clt != null ?
                                        ct.Name + ", " + clt.Name + (iawptc.Info != null ?
                                                                                ", " + iawptc.Info :
                                                                                string.Empty)
                                        : ct.Name + (iawptc.Info != null ?
                                                                ", " + iawptc.Info : string.Empty)) :
                                                                iawptc.Info != null ?
                                                                    iawptc.Info :
                                                                    string.Empty,

                             CandidateNameAlt = ct != null ?
                                    (clt != null ?
                                        ct.NameAlt + ", " + clt.NameAlt + (iawptc.InfoAlt != null ?
                                                                                ", " + iawptc.InfoAlt :
                                                                                string.Empty)
                                        : ct.NameAlt + (iawptc.InfoAlt != null ?
                                                                ", " + iawptc.InfoAlt : string.Empty)) :
                                                                iawptc.InfoAlt != null ?
                                                                    iawptc.InfoAlt :
                                                                    string.Empty,
                         })
                         .OrderBy(c => c.IndicativeAnnualWorkingProgrammeTableId)
                         .ThenBy(c => c.IsCompany)
                         .ToList();

            var timeLimits =
                (from iawpt in indicativeAnnualWorkingProgrammeTables

                 join iawpttl in this.unitOfWork.DbContext.Set<IndicativeAnnualWorkingProgrammeTableTimeLimit>() on iawpt.IndicativeAnnualWorkingProgrammeTableId equals iawpttl.IndicativeAnnualWorkingProgrammeTableId

                 where indicativeAnnualWorkingProgrammeTableIds.Contains(iawpt.IndicativeAnnualWorkingProgrammeTableId)
                 select new
                 {
                     iawpttl.IndicativeAnnualWorkingProgrammeTableId,
                     iawpttl.EndDate,
                 })
                 .ToList();

            var primaryProcedureShares = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on pp.MapNodeRelation.ProgrammeId equals p.MapNodeId
                    where ps.IsPrimary
                    select new
                    {
                        ProcedureId = ps.ProcedureId,
                        ProgrammeShortName = p.ShortName,
                        ProgrammeShortNameAlt = p.PortalShortNameAlt,
                    })
                    .ToList();

            var results = (from iawpt in indicativeAnnualWorkingProgrammeTables
                    select new IndicativeAnnualWorkingProgrammeTableVO
                    {
                        IndicativeAnnualWorkingProgrammeTableId = iawpt.IndicativeAnnualWorkingProgrammeTableId,
                        ProgrammePriorityName = iawpt.ProgrammePriorityName,
                        ProgrammePriorityNameAlt = iawpt.ProgrammePriorityNameAlt,
                        OrderNum = iawpt.OrderNum,
                        ProcedureCode = iawpt.ProcedureCode,
                        ProcedureName = iawpt.ProcedureName,
                        ProcedureNameAlt = iawpt.ProcedureNameAlt,
                        ProcedureDescription = iawpt.ProcedureDescription,
                        ProcedureDescriptionAlt = iawpt.ProcedureDescriptionAlt,
                        IndicativeAnnualWorkingProgrammeTypeConducting = iawpt.IndicativeAnnualWorkingProgrammeTypeConducting,
                        WithPreSelection = iawpt.WithPreSelection,
                        IndicativeAnnualWorkingProgrammeTableProgrammes =
                            programmes.Where(c => c.IndicativeAnnualWorkingProgrammeTableId == iawpt.IndicativeAnnualWorkingProgrammeTableId)
                                .Select(p => p.ShortName)
                                .Distinct()
                                .ToList(),
                        IndicativeAnnualWorkingProgrammeTableProgrammesAlt =
                            programmes.Where(c => c.IndicativeAnnualWorkingProgrammeTableId == iawpt.IndicativeAnnualWorkingProgrammeTableId)
                                .Select(p => p.ShortNameAlt)
                                .Distinct()
                                .ToList(),
                        LeadingProgram = primaryProcedureShares.Where(ps => ps.ProcedureId == iawpt.ProcedureId).SingleOrDefault()?.ProgrammeShortName,
                        LeadingProgramAlt = primaryProcedureShares.Where(ps => ps.ProcedureId == iawpt.ProcedureId).SingleOrDefault()?.ProgrammeShortNameAlt,
                        ProcedureTotalAmount = iawpt.ProcedureTotalAmount,
                        IndicativeAnnualWorkingProgrammeTableCandidates =
                            candidates.Where(c => c.IndicativeAnnualWorkingProgrammeTableId == iawpt.IndicativeAnnualWorkingProgrammeTableId)
                                .Select(c => c.CandidateName)
                                .Distinct()
                                .ToList(),
                        IndicativeAnnualWorkingProgrammeTableCandidatesAlt =
                            candidates.Where(c => c.IndicativeAnnualWorkingProgrammeTableId == iawpt.IndicativeAnnualWorkingProgrammeTableId)
                                .Select(c => c.CandidateNameAlt)
                                .Distinct()
                                .ToList(),
                        EligibleActivities = iawpt.EligibleActivities,
                        EligibleActivitiesAlt = iawpt.EligibleActivitiesAlt,
                        EligibleCosts = iawpt.EligibleCosts,
                        EligibleCostsAlt = iawpt.EligibleCostsAlt,
                        MaxPercentCoFinancing = iawpt.MaxPercentCoFinancing,
                        MaxPercentCoFinancingInfo = iawpt.MaxPercentCoFinancingInfo,
                        MaxPercentCoFinancingInfoAlt = iawpt.MaxPercentCoFinancingInfoAlt,
                        ListingDate = iawpt.ListingDate,
                        IndicativeAnnualWorkingProgrammeTableTimeLimits =
                            timeLimits.Where(c => c.IndicativeAnnualWorkingProgrammeTableId == iawpt.IndicativeAnnualWorkingProgrammeTableId)
                                .Select(tl => tl.EndDate)
                                .Distinct()
                                .ToList(),
                        IsStateAssistance = iawpt.IsStateAssistance,
                        IsMinimalAssistance = iawpt.IsMinimalAssistance,
                        ProjectMinAmount = iawpt.ProjectMinAmount,
                        ProjectMinAmountInfo = iawpt.ProjectMinAmountInfo,
                        ProjectMinAmountInfoAlt = iawpt.ProjectMinAmountInfoAlt,
                        ProjectMaxAmount = iawpt.ProjectMaxAmount,
                        ProjectMaxAmountInfo = iawpt.ProjectMaxAmountInfo,
                        ProjectMaxAmountInfoAlt = iawpt.ProjectMaxAmountInfoAlt,
                    })
                    .OrderBy(x => x.OrderNum)
                    .ToList();

            var iawpTableWithOffsetAndLimit = results
                .Distinct()
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return new PageVO<IndicativeAnnualWorkingProgrammeTableVO>() { Count = results.Count(), Results = iawpTableWithOffsetAndLimit };
        }

        private class ContractDO
        {
            public int ContractId { get; set; }

            public int ProcedureId { get; set; }

            public string RegNumber { get; set; }

            public ContractExecutionStatus? ExecutionStatus { get; set; }

            public string Name { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? EndDate { get; set; }

            public decimal? TotalAmount { get; set; }

            public string[] PlaceOfExecution { get; set; }

            public string Description { get; set; }

            public int? Duration { get; set; }

            public string GetId()
            {
                return Regex.Replace(this.RegNumber, @"-C\d+$", string.Empty);
            }
        }

        private class EntityDO
        {
            private const string EntitySalt = "0831afa2c2a765608bc502fcf03a756c";

            public int ContractId { get; set; }

            public int ContractContractorId { get; set; }

            public string Uin { get; set; }

            public UinType? UinType { get; set; }

            public string Name { get; set; }

            public string SeatCountryCode { get; set; }

            public string SeatSettlement { get; set; }

            public string SeatPostCode { get; set; }

            public string SeatStreet { get; set; }

            public string SeatAddress { get; set; }

            public string MunicipalityName { get; set; }

            public string EntityDistrictName { get; set; }

            public string GetId()
            {
                var id = this.Uin;

                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new Exception("Uin is required");
                }

                return CryptoUtils.GetSha1Hash(string.Concat(id, EntitySalt));
            }

            public ExportEntity GetArachneEntity()
            {
                var name = this.UinType == Domain.Entities.Umis.NonAggregates.UinType.PersonalBulstat ? Helper.AnonymizeName(this.Name) : this.Name;

                return new ExportEntity(this.GetId())
                {
                    EntityUin = this.UinType == Domain.Entities.Umis.NonAggregates.UinType.PersonalBulstat ? null : this.Uin,
                    EntityName = name.TruncateWithEllipsis(200),
                    EntityAddress = this.UinType == Domain.Entities.Umis.NonAggregates.UinType.PersonalBulstat ? null : this.SeatAddress.TruncateWithEllipsis(255),
                    EntityZipCode = this.SeatPostCode.TruncateWithEllipsis(16),
                    EntityCity = this.SeatSettlement.TruncateWithEllipsis(150),
                    EntityMunicipality = this.MunicipalityName,
                    EntityDistrict = this.EntityDistrictName,
                };
            }
        }

        private class ContractContractDO
        {
            public int ContractId { get; set; }

            public int ContractContractId { get; set; }

            public int ContractContractorId { get; set; }

            public string Number { get; set; }

            public DateTime SignDate { get; set; }

            public DateTime InitialDate { get; set; }

            public DateTime EndDate { get; set; }

            public decimal TotalFundedValue { get; set; }
        }

        private class ContractSubcontractDO
        {
            public int ContractId { get; set; }

            public int ContractContractId { get; set; }

            public int ContractContractorId { get; set; }

            public string Number { get; set; }

            public decimal Amount { get; set; }

            public ContractSubcontractType Type { get; set; }
        }

        private class ContractAmountDO
        {
            public int Id { get; set; }

            public int? ContractId { get; set; }

            public DateTime? ContractDate { get; set; }

            public int? ProgrammeId { get; set; }

            public int? ProgrammePriorityId { get; set; }

            public string ProgrammePriorityName { get; set; }

            public int? ProcedureId { get; set; }

            public int? ContractBudgetLevel3AmountId { get; set; }

            public decimal? ContractedSelfAmount { get; set; }

            public decimal? ContractedEuAmount { get; set; }

            public decimal? ContractedBgAmount { get; set; }

            public decimal? ContractedTotalAmount { get; set; }

            public bool? IsHistoric { get; set; }
        }

        private class ContractedAmountItem
        {
            public int? ProgrammeId { get; set; }

            public int? ProgrammePriorityId { get; set; }

            public int? ProcedureId { get; set; }

            public int? ContractId { get; set; }

            public decimal? ContractedSelfAmount { get; set; }

            public decimal? ContractedEuAmount { get; set; }

            public decimal? ContractedBgAmount { get; set; }

            public decimal? ContractedTotalAmount { get; set; }

            public bool? IsHistoric { get; set; }
        }

        private class ContractedAmountGroupingItem
        {
            public int? ProgrammeId { get; set; }

            public int? ProgrammePriorityId { get; set; }

            public int? ProcedureId { get; set; }

            public int? ContractId { get; set; }

            public bool? IsHistoric { get; set; }
        }
    }
}
