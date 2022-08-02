using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.Prognoses.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;

namespace Eumis.Data.Prognoses.Repositories
{
    internal class PrognosesRepository : AggregateRepository<Prognosis>, IPrognosesRepository
    {
        private static decimal euroExchangeRates = 1.95583m;

        public PrognosesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<PrognosisVO> GetPrognoses(
            int[] programmeIds,
            PrognosisLevel level,
            Year? year,
            Month? month)
        {
            var predicate = PredicateBuilder.True<Prognosis>()
                .And(p => p.Level == level)
                .AndEquals(p => p.Year, year)
                .AndEquals(p => p.Month, month);

            switch (level)
            {
                case PrognosisLevel.Programme:
                    predicate = predicate.And(p => programmeIds.Contains(p.ProgrammeId.Value));
                    break;
                case PrognosisLevel.ProgrammePriority:
                    var visibleProgrammePriorities =
                        from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                        join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                        where programmeIds.Contains(mnr.ProgrammeId.Value)
                        select pp.MapNodeId;
                    predicate = predicate.And(p => visibleProgrammePriorities.Contains(p.ProgrammePriorityId.Value));
                    break;
                case PrognosisLevel.Procedure:
                    var visibleProcedures =
                        from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                        where ps.IsPrimary && programmeIds.Contains(ps.ProgrammeId)
                        select ps.ProcedureId;
                    predicate = predicate.And(p => visibleProcedures.Contains(p.ProcedureId.Value));
                    break;
            }

            return (from p in this.unitOfWork.DbContext.Set<Prognosis>().Where(predicate)

                    join prog in this.unitOfWork.DbContext.Set<Programme>() on p.ProgrammeId equals prog.MapNodeId into g0
                    from prog in g0.DefaultIfEmpty()

                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on p.ProgrammePriorityId equals pp.MapNodeId into g1
                    from pp in g1.DefaultIfEmpty()

                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId into g2
                    from proc in g2.DefaultIfEmpty()

                    orderby p.CreateDate descending
                    select new PrognosisVO
                    {
                        PrognosisId = p.PrognosisId,
                        Programme = prog.Name,
                        ProgrammePriority = pp.Name,
                        Procedure = proc.Name,
                        Year = p.Year,
                        Month = p.Month,
                        StatusDescr = p.Status,
                        Status = p.Status,
                        ContractedAmount = p.Contracted.TotalAmount,
                        PaymentAmount = p.Payment.TotalAmount,
                        AdvancePaymentAmount = p.AdvancePayment.TotalAmount,
                        AdvanceVerPaymentAmount = p.AdvanceVerPayment.TotalAmount,
                        IntermediatePaymentAmount = p.IntermediatePayment.TotalAmount,
                        FinalPaymentAmount = p.FinalPayment.TotalAmount,
                        ApprovedAmount = p.Approved.TotalAmount,
                        CertifiedAmount = p.Certified.TotalAmount,
                    }).ToList();
        }

        public IList<string> CanCreateProgrammePrognosis(
            int programmeId,
            Year year,
            Month month)
        {
            IList<string> errors = new List<string>();

            var prognosisExists =
                (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                 where p.Level == PrognosisLevel.Programme &&
                    p.ProgrammeId == programmeId &&
                    p.Year == year &&
                    p.Month == month
                 select p.PrognosisId).Any();
            if (prognosisExists)
            {
                errors.Add("Вече съществува прогноза за същите програма, година, месец и фонд.");
            }

            return errors;
        }

        public IList<string> CanCreateProgrammePriorityPrognosis(
            int programmePriorityId,
            Year year,
            Month month)
        {
            IList<string> errors = new List<string>();

            var prognosisExists =
                (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                 where p.Level == PrognosisLevel.ProgrammePriority &&
                     p.ProgrammePriorityId == programmePriorityId &&
                     p.Year == year &&
                     p.Month == month
                 select p.PrognosisId).Any();
            if (prognosisExists)
            {
                errors.Add("Вече съществува прогноза за същите приоритетна ос, година, месец и фонд.");
            }

            return errors;
        }

        public IList<string> CanCreateProcedurePrognosis(
            int procedureId,
            Year year,
            Month month)
        {
            IList<string> errors = new List<string>();

            var prognosisExists =
                (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                 where p.Level == PrognosisLevel.Procedure &&
                    p.ProcedureId == procedureId &&
                    p.Year == year &&
                    p.Month == month
                 select p.PrognosisId).Any();
            if (prognosisExists)
            {
                errors.Add("Вече съществува прогноза за същите процедура, година, месец и фонд.");
            }

            return errors;
        }

        public IList<PrognosisYearlyReportVO> GetYearlyPrognosisReport(int programmeId, Year[] years)
        {
            var programmePriorities =
                (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                 join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                 where mnr.ProgrammeId == programmeId && pp.Status == MapNodeStatus.Entered
                 select new
                 {
                     ProgrammePriorityId = pp.MapNodeId,
                     Code = pp.Code,
                     Name = pp.Name,
                 }).ToList();

            var prognosis =
                (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                 join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on p.ProgrammePriorityId equals mnr.MapNodeId
                 where p.Level == PrognosisLevel.ProgrammePriority && p.Status == PrognosisStatus.Entered && mnr.ProgrammeId == programmeId && years.Contains(p.Year)
                 select new
                 {
                     ProgrammePriorityId = p.ProgrammePriorityId.Value,
                     Year = p.Year,
                     Month = p.Month,
                     AdvancePaymentAmount = p.AdvancePayment.TotalAmount,
                     AdvanceVerPaymentAmount = p.AdvanceVerPayment.TotalAmount,
                     IntermediatePaymentAmount = p.IntermediatePayment.TotalAmount,
                     FinalPaymentAmount = p.FinalPayment.TotalAmount,
                 }).ToList();

            return programmePriorities
                .Select(pp =>
                    {
                        var items = prognosis.Where(p => p.ProgrammePriorityId == pp.ProgrammePriorityId)
                            .Select(p => new PrognosisYearlyReportItemVO
                            {
                                Year = p.Year,
                                Quarter = (Quarter)((((int)p.Month - 1) / 3) + 1),
                                AdvancePaymentAmount = p.AdvancePaymentAmount,
                                AdvanceVerPaymentAmount = p.AdvanceVerPaymentAmount,
                                IntermediatePaymentAmount = p.IntermediatePaymentAmount,
                                FinalPaymentAmount = p.FinalPaymentAmount,
                            }).ToList();

                        return new PrognosisYearlyReportVO
                        {
                            ProgrammePriorityId = pp.ProgrammePriorityId,
                            ProgrammePriorityCode = pp.Code,
                            ProgrammePriorityName = pp.Name,
                            ReportItems = items,
                        };
                    }).ToList();
        }

        public IList<PrognosisMonthlyReportVO> GetMonthlyPrognosisReport(int programmeId, Year year, Month[] months)
        {
            var programmePriorities =
                (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                 join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                 where mnr.ProgrammeId == programmeId && pp.Status == MapNodeStatus.Entered
                 select new
                 {
                     ProgrammePriorityId = pp.MapNodeId,
                     Code = pp.Code,
                     Name = pp.Name,
                 }).ToList();

            var prognosis =
                (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                 join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on p.ProgrammePriorityId equals mnr.MapNodeId
                 where p.Level == PrognosisLevel.ProgrammePriority &&
                    p.Status == PrognosisStatus.Entered &&
                    mnr.ProgrammeId == programmeId &&
                    p.Year == year &&
                    months.Contains(p.Month)
                 select new
                 {
                     ProgrammePriorityId = p.ProgrammePriorityId.Value,
                     Year = p.Year,
                     Month = p.Month,
                     AdvancePaymentAmount = p.AdvancePayment.TotalAmount,
                     AdvanceVerPaymentAmount = p.AdvanceVerPayment.TotalAmount,
                     IntermediatePaymentAmount = p.IntermediatePayment.TotalAmount,
                     FinalPaymentAmount = p.FinalPayment.TotalAmount,
                 }).ToList();

            return programmePriorities
                .Select(pp =>
                {
                    var items = prognosis.Where(p => p.ProgrammePriorityId == pp.ProgrammePriorityId)
                        .Select(p => new PrognosisMonthlyReportItemVO
                        {
                            Month = p.Month,
                            AdvancePaymentAmount = p.AdvancePaymentAmount,
                            AdvanceVerPaymentAmount = p.AdvanceVerPaymentAmount,
                            IntermediatePaymentAmount = p.IntermediatePaymentAmount,
                            FinalPaymentAmount = p.FinalPaymentAmount,
                        }).ToList();

                    return new PrognosisMonthlyReportVO
                    {
                        ProgrammePriorityId = pp.ProgrammePriorityId,
                        ProgrammePriorityCode = pp.Code,
                        ProgrammePriorityName = pp.Name,
                        ReportItems = items,
                    };
                }).ToList();
        }

        public IList<PrognosisProgrammePriorityReportVO> GetProgrammePriorityPrognosisReport(int programmePriorityId)
        {
            var ppContractedAmounts =
                (from cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>()
                 join c in this.unitOfWork.DbContext.Set<Contract>() on cba.ContractId equals c.ContractId
                 join pb in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cba.ProcedureBudgetLevel2Id equals pb.ProcedureBudgetLevel2Id
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pb.ProcedureShareId equals ps.ProcedureShareId
                 join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                 where cba.IsActive && ps.ProgrammePriorityId == programmePriorityId && c.ContractDate.HasValue
                 group cba by new { Year = c.ContractDate.Value.Year, Month = c.ContractDate.Value.Month, pp.NameAlt } into g
                 select new
                 {
                     Year = g.Key.Year,
                     Month = g.Key.Month,
                     ProgrammePriority = g.Key.NameAlt,
                     TotalBfpAmount = g.Sum(o => o.CurrentEuAmount + o.CurrentBgAmount),
                 }).ToList();

            var prognoses =
                (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                 join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on p.ProgrammePriorityId equals pp.MapNodeId
                 where p.Level == PrognosisLevel.ProgrammePriority && p.Status == PrognosisStatus.Entered && p.ProgrammePriorityId == programmePriorityId
                 group p by new { p.Year, p.Month, pp.NameAlt } into g
                 select new
                 {
                     Year = g.Key.Year,
                     Month = g.Key.Month,
                     ProgrammePriority = g.Key.NameAlt,
                     PrognosedContractedBfpAmount = g.Sum(o => o.Contracted.TotalAmount),
                     PrognosedApprovedBfpAmount = g.Sum(o => o.Approved.TotalAmount),
                     PrognosedCertifiedBfpAmount = g.Sum(o => o.Certified.TotalAmount),
                 }).ToList();

            var nextThreeAmounts =
                (from mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>()
                 join bp in this.unitOfWork.DbContext.Set<BudgetPeriod>() on mnb.BudgetPeriodId equals bp.BudgetPeriodId
                 where mnb.MapNodeId == programmePriorityId
                 group mnb by bp.Year into g
                 select new
                 {
                     Year = g.Key,
                     NextThreeWithAdvances = g.Sum(o => o.NextThreeWithAdvances),
                     NextThreeWithoutAdvances = g.Sum(o => o.NextThreeWithoutAdvances),
                 }).ToList();

            var query1 =
                from ca in ppContractedAmounts
                join p in prognoses on new { ca.Year, ca.Month } equals new { Year = (int)p.Year, Month = (int)p.Month } into g0
                from p in g0.DefaultIfEmpty()
                select new
                {
                    Year = (Year)ca.Year,
                    Month = ca.Month,
                    ProgrammePriority = ca.ProgrammePriority,
                    ContractedTotalBfpAmount = ca.TotalBfpAmount,
                    PrognosedContractedBfpAmount = p == null ? 0 : p.PrognosedContractedBfpAmount,
                    PrognosedApprovedBfpAmount = p == null ? 0 : p.PrognosedApprovedBfpAmount,
                    PrognosedCertifiedBfpAmount = p == null ? 0 : p.PrognosedCertifiedBfpAmount,
                };

            var query2 =
                from p in prognoses
                join ca in ppContractedAmounts on new { Year = (int)p.Year, Month = (int)p.Month } equals new { ca.Year, ca.Month } into g0
                from ca in g0.DefaultIfEmpty()
                select new
                {
                    Year = p.Year,
                    Month = (int)p.Month,
                    ProgrammePriority = p.ProgrammePriority,
                    ContractedTotalBfpAmount = ca == null ? 0 : ca.TotalBfpAmount,
                    PrognosedContractedBfpAmount = p.PrognosedContractedBfpAmount,
                    PrognosedApprovedBfpAmount = p.PrognosedApprovedBfpAmount,
                    PrognosedCertifiedBfpAmount = p.PrognosedCertifiedBfpAmount,
                };

            return (from i in query1.Union(query2)
                    join nta in nextThreeAmounts on (int)i.Year equals nta.Year into g0
                    from nta in g0.DefaultIfEmpty()
                    group new { i, nta } by new { Year = i.Year, Quarter = ((i.Month - 1) / 3) + 1, i.ProgrammePriority } into g
                    select new PrognosisProgrammePriorityReportVO(
                        programmePriority: g.Key.ProgrammePriority,
                        year: g.Key.Year,
                        quarter: (Quarter)g.Key.Quarter,
                        nextThreeWithAdvances: g.Sum(o => o.nta == null ? 0 : o.nta.NextThreeWithAdvances),
                        nextThreeWithoutAdvances: g.Sum(o => o.nta == null ? 0 : o.nta.NextThreeWithoutAdvances),
                        prognosedContractedBfpAmount: g.Sum(o => (o.i.PrognosedContractedBfpAmount ?? 0) / euroExchangeRates),
                        contractsBfpAmount: g.Sum(o => o.i.ContractedTotalBfpAmount / euroExchangeRates),
                        prognosedApprovedBfpAmount: g.Sum(o => (o.i.PrognosedApprovedBfpAmount ?? 0) / euroExchangeRates),
                        approvedBfpAmount: 0, // TODO
                        prognosedCertifiedBfpAmount: g.Sum(o => (o.i.PrognosedCertifiedBfpAmount ?? 0) / euroExchangeRates),
                        certifiedBfpAmount: 0)) // TODO
                    .ToList();
        }

        public IList<PrognosisProgrammeReportVO> GetProgrammePrognosisReport(int programmeId)
        {
            var ppContractedAmounts =
                (from c in this.unitOfWork.DbContext.Set<Contract>()
                 join p in this.unitOfWork.DbContext.Set<Programme>() on c.ProgrammeId equals p.MapNodeId
                 where c.ProgrammeId == programmeId && c.ContractDate.HasValue && c.ContractStatus == ContractStatus.Entered
                 group c by new { Year = c.ContractDate.Value.Year, Month = c.ContractDate.Value.Month, p.NameAlt } into g
                 select new
                 {
                     Year = g.Key.Year,
                     Month = g.Key.Month,
                     Programme = g.Key.NameAlt,
                     TotalBfpAmount = g.Sum(o => o.TotalBfpAmount),
                 })
                 .ToList();

            var prognoses =
                (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                 join prog in this.unitOfWork.DbContext.Set<Programme>() on p.ProgrammeId equals prog.MapNodeId
                 where p.Level == PrognosisLevel.Programme && p.Status == PrognosisStatus.Entered && p.ProgrammeId == programmeId
                 group p by new { p.Year, p.Month, prog.NameAlt } into g
                 select new
                 {
                     Year = g.Key.Year,
                     Month = g.Key.Month,
                     Programme = g.Key.NameAlt,
                     PrognosedContractedBfpAmount = g.Sum(o => o.Contracted.TotalAmount),
                     PrognosedApprovedBfpAmount = g.Sum(o => o.Approved.TotalAmount),
                     PrognosedCertifiedBfpAmount = g.Sum(o => o.Certified.TotalAmount),
                 })
                 .ToList();

            var nextThreeAmounts =
                (from mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>()
                 join bp in this.unitOfWork.DbContext.Set<BudgetPeriod>() on mnb.BudgetPeriodId equals bp.BudgetPeriodId
                 where mnb.ProgrammeId == programmeId
                 group mnb by bp.Year into g
                 select new
                 {
                     Year = g.Key,
                     NextThreeWithAdvances = g.Sum(o => o.NextThreeWithAdvances),
                     NextThreeWithoutAdvances = g.Sum(o => o.NextThreeWithoutAdvances),
                 })
                 .ToList();

            var query1 =
                from ca in ppContractedAmounts
                join p in prognoses on new { ca.Year, ca.Month } equals new { Year = (int)p.Year, Month = (int)p.Month } into g0
                from p in g0.DefaultIfEmpty()
                select new
                {
                    Year = (Year)ca.Year,
                    Month = ca.Month,
                    Programme = ca.Programme,
                    ContractedTotalBfpAmount = ca.TotalBfpAmount,
                    PrognosedContractedBfpAmount = p == null ? 0 : p.PrognosedContractedBfpAmount,
                    PrognosedApprovedBfpAmount = p == null ? 0 : p.PrognosedApprovedBfpAmount,
                    PrognosedCertifiedBfpAmount = p == null ? 0 : p.PrognosedCertifiedBfpAmount,
                };

            var query2 =
                from p in prognoses
                join ca in ppContractedAmounts on new { Year = (int)p.Year, Month = (int)p.Month } equals new { ca.Year, ca.Month } into g0
                from ca in g0.DefaultIfEmpty()
                select new
                {
                    Year = p.Year,
                    Month = (int)p.Month,
                    Programme = p.Programme,
                    ContractedTotalBfpAmount = ca == null ? 0 : ca.TotalBfpAmount,
                    PrognosedContractedBfpAmount = p.PrognosedContractedBfpAmount,
                    PrognosedApprovedBfpAmount = p.PrognosedApprovedBfpAmount,
                    PrognosedCertifiedBfpAmount = p.PrognosedCertifiedBfpAmount,
                };

            return (from i in query1.Union(query2)
                    join nta in nextThreeAmounts on (int)i.Year equals nta.Year into g0
                    from nta in g0.DefaultIfEmpty()
                    group new { i, nta } by new { Year = i.Year, Quarter = ((i.Month - 1) / 3) + 1, i.Programme } into g
                    select new PrognosisProgrammeReportVO(
                        programme: g.Key.Programme,
                        year: g.Key.Year,
                        quarter: (Quarter)g.Key.Quarter,
                        nextThreeWithAdvances: g.Sum(o => o.nta == null ? 0 : o.nta.NextThreeWithAdvances),
                        nextThreeWithoutAdvances: g.Sum(o => o.nta == null ? 0 : o.nta.NextThreeWithoutAdvances),
                        prognosedContractedBfpAmount: g.Sum(o => (o.i.PrognosedContractedBfpAmount ?? 0) / euroExchangeRates),
                        contractsBfpAmount: g.Sum(o => (o.i.ContractedTotalBfpAmount ?? 0) / euroExchangeRates),
                        prognosedApprovedBfpAmount: g.Sum(o => (o.i.PrognosedApprovedBfpAmount ?? 0) / euroExchangeRates),
                        approvedBfpAmount: 0, // TODO
                        prognosedCertifiedBfpAmount: g.Sum(o => (o.i.PrognosedCertifiedBfpAmount ?? 0) / euroExchangeRates),
                        certifiedBfpAmount: 0)) // TODO
                    .ToList();
        }

        public IList<PrognosisSummaryReportVO> GetPrognosisSummaryReport()
        {
            var proceduresBudget =
                (from p in this.unitOfWork.DbContext.Set<Procedure>()
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId into g0
                 from ps in g0.DefaultIfEmpty()
                 where p.ProcedureStatus != ProcedureStatus.Canceled
                 group ps by p.ProcedureId into g
                 select new
                 {
                     ProcedureId = g.Key,
                     BgAmount = g.Sum(o => o.BgAmount),
                 }).ToList();
            var proceduresTotalBudget = proceduresBudget.Sum(b => b.BgAmount);

            var procedureProjects =
                (from p in this.unitOfWork.DbContext.Set<Project>()
                 join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>() on p.ProjectId equals esps.ProjectId into g0
                 from esps in g0.DefaultIfEmpty()
                 join es in this.unitOfWork.DbContext.Set<EvalSession>() on esps.EvalSessionId equals es.EvalSessionId into g1
                 from es in g1.DefaultIfEmpty()
                 where p.RegistrationStatus != ProjectRegistrationStatus.Withdrawn && es.EvalSessionStatus != EvalSessionStatus.Canceled && !esps.IsDeleted
                 group new { p, esps } by p.ProcedureId into g
                 select new
                 {
                     ProcedureId = g.Key,
                     ProjectsCount = g.Count(),
                     ApprovedProjectsCount = g.Count(o => o.esps != null && o.esps.Status == EvalSessionProjectStandingStatus.Approved),
                     ApprovedProjectsBudget = g.Sum(o => o.esps == null || o.esps.Status != EvalSessionProjectStandingStatus.Approved ? 0 : o.p.TotalBfpAmount),
                 }).ToList();

            var procedureContracts =
                (from c in this.unitOfWork.DbContext.Set<Contract>()
                 where c.ContractStatus == ContractStatus.Entered
                 group c by c.ProcedureId into g
                 select new
                 {
                     ProcedureId = g.Key,
                     ContractsCount = g.Count(),
                     TotalBfpAmount = g.Sum(o => o.TotalBfpAmount),
                     TotalEuAmount = g.Sum(o => o.TotalEuAmount),
                 }).ToList();

            var procedurePrognoses =
                (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                 where p.Status == PrognosisStatus.Entered
                 group p by p.ProcedureId into g
                 select new
                 {
                     ProcedureId = g.Key,
                     ContractedBfpAmount = g.Sum(o => o.Contracted.TotalAmount),
                     ApprovedBfpAmount = g.Sum(o => o.Approved.TotalAmount),
                     CertifiedBfpAmount = g.Sum(o => o.Certified.TotalAmount),
                 }).ToList();

            var procedurePayments =
                (from p in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                 join pc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>() on p.ContractReportPaymentId equals pc.ContractReportPaymentId
                 join pca in this.unitOfWork.DbContext.Set<ContractReportPaymentCheckAmount>() on pc.ContractReportPaymentCheckId equals pca.ContractReportPaymentCheckId
                 join cr in this.unitOfWork.DbContext.Set<ContractReport>() on p.ContractReportId equals cr.ContractReportId
                 join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                 where (p.PaymentType == ContractReportPaymentType.Final || p.PaymentType == ContractReportPaymentType.Intermediate) &&
                    p.Status == ContractReportPaymentStatus.Actual &&
                    pc.Status == ContractReportPaymentCheckStatus.Active &&
                    cr.Status == ContractReportStatus.Accepted
                 group new { p.TotalAmount, pca.ApprovedBfpTotalAmount } by c.ProcedureId into g
                 select new
                 {
                     ProcedureId = g.Key,
                     BfpTotalAmount = g.Sum(o => o.TotalAmount),
                     ApprovedBfpTotalAmount = g.Sum(o => o.ApprovedBfpTotalAmount),
                 }).ToList();

            var procedureActuallyPaidAmounts =
                (from apa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                 join c in this.unitOfWork.DbContext.Set<Contract>() on apa.ContractId equals c.ContractId
                 where apa.Status == ActuallyPaidAmountStatus.Entered
                 group new { apa.PaidBfpTotalAmount } by c.ProcedureId into g
                 select new
                 {
                     ProcedureId = g.Key,
                     PaidBfpTotalAmount = g.Sum(o => o.PaidBfpTotalAmount),
                 }).ToList();

            var procedureVersionsSubquery =
                from pv in this.unitOfWork.DbContext.Set<ProcedureVersion>()
                group pv by pv.ProcedureId into g
                select g.Key;

            return (from p in this.unitOfWork.DbContext.Set<Procedure>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                    where ps.IsPrimary && procedureVersionsSubquery.Contains(p.ProcedureId) && p.ProcedureStatus != ProcedureStatus.Canceled
                    select new
                    {
                        p.ProcedureId,
                        ProcedureName = p.Name,
                        ProcedureNameAlt = p.NameAlt,
                        Programme = prog.NameAlt,
                        ProgrammePriority = pp.NameAlt,
                    })
                .ToList()
                .Select(o =>
                {
                    var budget = proceduresBudget.SingleOrDefault(b => b.ProcedureId == o.ProcedureId);
                    var projects = procedureProjects.SingleOrDefault(p => p.ProcedureId == o.ProcedureId);
                    var contracts = procedureContracts.SingleOrDefault(c => c.ProcedureId == o.ProcedureId);
                    var prognoses = procedurePrognoses.SingleOrDefault(p => p.ProcedureId == o.ProcedureId);
                    var payments = procedurePayments.SingleOrDefault(p => p.ProcedureId == o.ProcedureId);
                    var actuallyPaidAmounts = procedureActuallyPaidAmounts.SingleOrDefault(p => p.ProcedureId == o.ProcedureId);

                    return new PrognosisSummaryReportVO(
                        procedureId: o.ProcedureId,
                        procedureName: o.ProcedureName,
                        procedureNameAlt: o.ProcedureNameAlt,
                        programme: o.Programme,
                        programmePriority: o.ProgrammePriority,
                        procedureBudget: budget == null ? 0 : budget.BgAmount / euroExchangeRates,
                        proceduresTotalBudget: proceduresTotalBudget / euroExchangeRates,
                        projectsCount: projects == null ? 0 : projects.ProjectsCount,
                        approvedProjectsCount: projects == null ? 0 : projects.ApprovedProjectsCount,
                        approvedProjectsBudget: projects == null ? 0 : (projects.ApprovedProjectsBudget ?? 0) / euroExchangeRates,
                        contractsCount: contracts == null ? 0 : contracts.ContractsCount,
                        contractsBfpBudget: contracts == null ? 0 : (contracts.TotalBfpAmount ?? 0) / euroExchangeRates,
                        prognosedContractedBfpAmount: prognoses == null ? 0 : (prognoses.ContractedBfpAmount ?? 0) / euroExchangeRates,
                        paymentsBfpAmount: payments == null ? 0 : (payments.BfpTotalAmount ?? 0) / euroExchangeRates,
                        approvedPaymentsBfpAmount: payments == null ? 0 : (payments.ApprovedBfpTotalAmount ?? 0) / euroExchangeRates,
                        approvedBfpAmount: 0, // TODO
                        actuallyPaidAmount: actuallyPaidAmounts == null ? 0 : (actuallyPaidAmounts.PaidBfpTotalAmount ?? 0) / euroExchangeRates,
                        prognosedApprovedBfpAmount: prognoses == null ? 0 : (prognoses.ApprovedBfpAmount ?? 0) / euroExchangeRates,
                        requestedBfpAmount: 0, // TODO
                        certifiedBfpAmount: 0, // TODO
                        prognosedCertifiedBfpAmount: prognoses == null ? 0 : (prognoses.CertifiedBfpAmount ?? 0) / euroExchangeRates);
                }).ToList();
        }

        public int GetProgrammePrognosisProgrammeId(int prognosisId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                    where p.PrognosisId == prognosisId
                    select p.ProgrammeId.Value).Single();
        }

        public int GetProgrammePriorityPrognosisProgrammeId(int prognosisId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on p.ProgrammePriorityId equals mnr.MapNodeId
                    where p.PrognosisId == prognosisId
                    select mnr.ProgrammeId.Value).Single();
        }

        public int GetProcedurePrognosisProgrammeId(int prognosisId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where p.PrognosisId == prognosisId && ps.IsPrimary
                    select ps.ProgrammeId).Single();
        }

        public new void Remove(Prognosis prognosis)
        {
            if (prognosis.IsActivated || prognosis.Status != PrognosisStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete Prognosis which is in draft status or is activated.");
            }

            base.Remove(prognosis);
        }
    }
}
