using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Data.Monitoring.ViewObjects;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.Audits;
using Eumis.Domain.CertReports;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Core;
using Eumis.Domain.Debts;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.ExpenseTypes;
using Eumis.Domain.Indicators;
using Eumis.Domain.Irregularities;
using Eumis.Domain.Measures;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.PhysicalExecution;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.SpotChecks;
using Eumis.Rio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Monitoring.Repositories
{
    internal class MonitoringReportsRepository : Repository, IMonitoringReportsRepository
    {
        private static decimal euroExchangeRates = 1.95583m;

        private static ContractReportStatus[] allowedContractReportStatuses = new ContractReportStatus[]
        {
            ContractReportStatus.Unchecked,
            ContractReportStatus.Accepted,
        };

        private static CertReportStatus[] allowedCertReportStatuses = new CertReportStatus[]
        {
            CertReportStatus.PartialyApproved,
            CertReportStatus.Approved,
        };

        private static ContractReportPaymentType[] advancePayments = new ContractReportPaymentType[]
        {
            ContractReportPaymentType.Advance,
            ContractReportPaymentType.AdvanceVerification,
        };

        public MonitoringReportsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<AdvancePaymentsReportItem> GetAdvancePaymentsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }
            else
            {
                toDate = DateTime.Now.AddDays(1).AddMilliseconds(-1);
            }

            if (!fromDate.HasValue)
            {
                fromDate = DateTime.MinValue;
            }

            var predicate = PredicateBuilder.True<Domain.Contracts.Contract>()
                .AndEquals(c => c.ProgrammeId, programmeId)
                .AndEquals(c => c.ProcedureId, procedureId)
                .And(c => c.ContractStatus == ContractStatus.Entered);

            var certReportsPredicate = PredicateBuilder.True<CertReport>();
            certReportsPredicate = certReportsPredicate
                .And(x => allowedCertReportStatuses.Contains(x.Status));

            var csdBudgetItemsPredicate = PredicateBuilder.True<ContractReportFinancialCSDBudgetItem>();
            csdBudgetItemsPredicate = csdBudgetItemsPredicate
                .And(x => x.AdvancePayment == YesNoNonApplicable.Yes)
                .AndDateTimeGreaterThanOrEqual(x => x.CheckedDate, fromDate)
                .AndDateTimeLessThanOrEqual(x => x.CheckedDate, toDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value >= fromDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value <= toDate);

            var advancePaymentAmountsPredicate = PredicateBuilder.True<ContractReportAdvancePaymentAmount>();
            advancePaymentAmountsPredicate = advancePaymentAmountsPredicate
                .AndDateTimeGreaterThanOrEqual(x => x.CheckedDate, fromDate)
                .AndDateTimeLessThanOrEqual(x => x.CheckedDate, toDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value >= fromDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value <= toDate);

            var financialCorrectionCsdsPredicate = PredicateBuilder.True<ContractReportFinancialCorrectionCSD>();
            financialCorrectionCsdsPredicate = financialCorrectionCsdsPredicate
                .And(x => x.Status == ContractReportFinancialCorrectionCSDStatus.Ended)
                .AndDateTimeGreaterThanOrEqual(x => x.CheckedDate, fromDate)
                .AndDateTimeLessThanOrEqual(x => x.CheckedDate, toDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value >= fromDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value <= toDate);

            var contractReportCorrectionsPredicate = PredicateBuilder.True<ContractReportCorrection>();
            contractReportCorrectionsPredicate = contractReportCorrectionsPredicate
                .And(x => x.Status == ContractReportCorrectionStatus.Entered)
                .AndDateTimeGreaterThanOrEqual(x => x.CheckedDate, fromDate)
                .AndDateTimeLessThanOrEqual(x => x.CheckedDate, toDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value >= fromDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value <= toDate);

            var contractReportCertCorrectionsPredicate = PredicateBuilder.True<ContractReportCertCorrection>();
            contractReportCertCorrectionsPredicate = contractReportCertCorrectionsPredicate
                .AndDateTimeGreaterThanOrEqual(x => x.CheckedDate, fromDate)
                .AndDateTimeLessThanOrEqual(x => x.CheckedDate, toDate);

            var contractReportCertAuthorityFinancialCorrectionCSDsPredicate = PredicateBuilder.True<ContractReportCertAuthorityFinancialCorrectionCSD>();
            contractReportCertAuthorityFinancialCorrectionCSDsPredicate = contractReportCertAuthorityFinancialCorrectionCSDsPredicate
                .AndDateTimeGreaterThanOrEqual(x => x.CheckedDate, fromDate)
                .AndDateTimeLessThanOrEqual(x => x.CheckedDate, toDate);

            var contractReportCertAuthorityCorrectionsPredicate = PredicateBuilder.True<ContractReportCertAuthorityCorrection>();
            contractReportCertAuthorityCorrectionsPredicate = contractReportCertAuthorityCorrectionsPredicate
                .And(x => x.Status == ContractReportCertAuthorityCorrectionStatus.Entered)
                .AndDateTimeGreaterThanOrEqual(x => x.CheckedDate, fromDate)
                .AndDateTimeLessThanOrEqual(x => x.CheckedDate, toDate);

            var financialRevalidationCsdsPredicate = PredicateBuilder.True<ContractReportFinancialRevalidationCSD>();
            financialRevalidationCsdsPredicate = financialRevalidationCsdsPredicate
                .And(x => x.Status == ContractReportFinancialRevalidationCSDStatus.Ended)
                .AndDateTimeGreaterThanOrEqual(x => x.CheckedDate, fromDate)
                .AndDateTimeLessThanOrEqual(x => x.CheckedDate, toDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value >= fromDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value <= toDate);

            var contractReportRevalidationsPredicate = PredicateBuilder.True<ContractReportRevalidation>();
            contractReportRevalidationsPredicate = contractReportRevalidationsPredicate
                .And(x => x.Status == ContractReportRevalidationStatus.Entered)
                .And(x => x.Type == ContractReportRevalidationType.PaymentRevalidated)
                .AndDateTimeGreaterThanOrEqual(x => x.CheckedDate, fromDate)
                .AndDateTimeLessThanOrEqual(x => x.CheckedDate, toDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value >= fromDate)
                .And(x => !x.CertCheckedDate.HasValue || x.CertCheckedDate.Value <= toDate);

            var financialCertCorrectionCsdsPredicate = PredicateBuilder.True<ContractReportFinancialCertCorrectionCSD>();
            financialCertCorrectionCsdsPredicate = financialCertCorrectionCsdsPredicate
                .AndDateTimeGreaterThanOrEqual(x => x.CheckedDate, fromDate)
                .AndDateTimeLessThanOrEqual(x => x.CheckedDate, toDate);

            var subqueryPredicate = PredicateBuilder.True<ProcedureShare>();

            if (programmePriorityId.HasValue)
            {
                subqueryPredicate = subqueryPredicate.And(ps => ps.ProgrammePriorityId == programmePriorityId);
            }

            var subquery = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(subqueryPredicate)
                            select ps.ProcedureId).Distinct();

            var contracts =
                from c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(predicate)
                join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals cv.ContractId
                join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                where cv.Status == ContractVersionStatus.Active && subquery.Contains(p.ProcedureId)
                select new
                {
                    Id = c.ContractId,
                    ContractRegNum = c.RegNumber,
                    BeneficiaryName = c.CompanyName,
                };

            var advancePayments =
                from c in contracts
                join crapa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>().Where(advancePaymentAmountsPredicate) on c.Id equals crapa.ContractId
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crapa.ContractReportPaymentId equals crp.ContractReportPaymentId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId

                join crep in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crapa.CertReportId equals crep.CertReportId into g1
                from crep in g1.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted && crp.PaymentType == ContractReportPaymentType.AdvanceVerification

                select new
                {
                    Id = crp.ContractReportPaymentId,
                    ContractReportPaymentId = crp.ContractReportPaymentId,
                    ContractId = crapa.ContractId,
                    ContractRegNum = c.ContractRegNum,
                    BeneficiaryName = c.BeneficiaryName,
                    VerifiedValue = crapa.ApprovedBfpTotalAmount ?? 0,
                    VerifiedCosts = 0M,
                    CertifiedPayment = crep != null ? crapa.CertifiedApprovedBfpTotalAmount ?? 0M : 0M,
                    CertAdvancePaymentExpenses = 0M,
                    CertReportId = crep != null ? crep.CertReportId : (int?)null,
                    CertReportNumber = crep != null ? crep.CertReportNumber : null,
                };

            var advancePaymentsCSDs =
                from c in contracts
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on c.Id equals crp.ContractId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                join crfcsdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(csdBudgetItemsPredicate) on cr.ContractReportId equals crfcsdbi.ContractReportId

                where cr.Status == ContractReportStatus.Accepted && crp.PaymentType == ContractReportPaymentType.Intermediate

                select crp.ContractReportPaymentId;

            var advancePaymentCosts =
                from c in contracts
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>().Where(x => x.Status == ContractReportPaymentStatus.Actual) on c.Id equals crp.ContractId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                join crfcsdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(csdBudgetItemsPredicate) on cr.ContractReportId equals crfcsdbi.ContractReportId

                join crep in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crfcsdbi.CertReportId equals crep.CertReportId into g1
                from crep in g1.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted
                select new
                {
                    Id = crfcsdbi.ContractReportFinancialCSDBudgetItemId,
                    ContractReportPaymentId = crp.ContractReportPaymentId,
                    ContractId = crfcsdbi.ContractId,
                    ContractRegNum = c.ContractRegNum,
                    BeneficiaryName = c.BeneficiaryName,
                    VerifiedValue = 0M,
                    VerifiedCosts = crfcsdbi.ApprovedBfpTotalAmount ?? 0,
                    CertifiedPayment = 0M,
                    CertAdvancePaymentExpenses = crep != null ? crfcsdbi.CertifiedApprovedBfpTotalAmount ?? 0M : 0M,
                    CertReportId = crep != null ? crep.CertReportId : (int?)null,
                    CertReportNumber = crep != null ? crep.CertReportNumber : null,
                };

            var advancePaymentCostsCSDCorrections =
                 from crfcc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>().Where(financialCorrectionCsdsPredicate)
                 join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crfcc.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                 join p in advancePaymentCosts.Select(x => x.Id).Distinct() on crfcc.ContractReportFinancialCSDBudgetItemId equals p

                 join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crfcc.CertReportId equals cr.CertReportId into g0
                 from cr in g0.DefaultIfEmpty()

                 where crfc.Status == ContractReportFinancialCorrectionStatus.Ended
                 select new
                 {
                     ContractId = crfcc.ContractId,
                     CorrectedAmount = -1 * (int)crfcc.Sign * crfcc.CorrectedApprovedBfpTotalAmount,
                     CertAdvancePaymentExpenses = cr != null ? -1 * (int)crfcc.Sign * crfcc.CertifiedCorrectedApprovedBfpTotalAmount ?? 0M : 0M,
                 }
                 into g1
                 group new { g1.CertAdvancePaymentExpenses, g1.CorrectedAmount } by g1.ContractId into g2
                 select new
                 {
                     ContractId = g2.Key,
                     CorrectedAmount = g2.Sum(x => x.CorrectedAmount),
                     CertAdvancePaymentExpenses = g2.Sum(x => x.CertAdvancePaymentExpenses),
                 };

            var advancePaymentCSDCorrections =
                 from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>().Where(contractReportCorrectionsPredicate)
                 join p in advancePaymentsCSDs.Distinct() on crc.ContractReportPaymentId equals p

                 join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crc.CertReportId equals cr.CertReportId into g0
                 from cr in g0.DefaultIfEmpty()

                 select new
                 {
                     ContractId = crc.ContractId,
                     CorrectedAmount = (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount,
                     CertAdvancePaymentExpenses = cr != null ? (int)crc.Sign * crc.CertifiedCorrectedApprovedBfpTotalAmount ?? 0M : 0M,
                 }
                 into g1
                 group new { g1.CertAdvancePaymentExpenses, g1.CorrectedAmount } by g1.ContractId into g2
                 select new
                 {
                     ContractId = g2.Key,
                     CorrectedAmount = g2.Sum(x => x.CorrectedAmount),
                     CertAdvancePaymentExpenses = g2.Sum(x => x.CertAdvancePaymentExpenses),
                 };

            var advancePaymentOtherCorrections =
                 from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>().Where(contractReportCorrectionsPredicate)
                 join p in advancePayments.Select(x => x.ContractReportPaymentId).Distinct() on crc.ContractReportPaymentId equals p

                 join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crc.CertReportId equals cr.CertReportId into g0
                 from cr in g0.DefaultIfEmpty()

                 select new
                 {
                     ContractId = crc.ContractId,
                     CorrectedAmount = (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount,
                     CertifiedPayment = cr != null ? (int)crc.Sign * crc.CertifiedCorrectedApprovedBfpTotalAmount ?? 0M : 0M,
                 }
                 into g1
                 group new { g1.CertifiedPayment, g1.CorrectedAmount } by g1.ContractId into g2
                 select new
                 {
                     ContractId = g2.Key,
                     CorrectedAmount = g2.Sum(x => x.CorrectedAmount),
                     CertifiedPayment = g2.Sum(x => x.CertifiedPayment),
                 };

            var certAuthorityCSDCorrections =
                from cac in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>().Where(contractReportCertAuthorityFinancialCorrectionCSDsPredicate)
                join aarcfc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>() on cac.ContractReportCertAuthorityFinancialCorrectionCSDId equals aarcfc.ContractReportCertAuthorityFinancialCorrectionCSDId
                join aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>() on aarcfc.AnnualAccountReportId equals aar.AnnualAccountReportId
                join crfcbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on cac.ContractReportFinancialCSDBudgetItemId equals crfcbi.ContractReportFinancialCSDBudgetItemId
                join c in contracts on cac.ContractId equals c.Id
                where aar.Status == AnnualAccountReportStatus.Ended && crfcbi.AdvancePayment == YesNoNonApplicable.Yes
                group cac by cac.ContractId into g

                select new
                {
                    ContractId = g.Key,
                    CorrectedAmount = g.Sum(x => (int)x.Sign * x.CertifiedBfpTotalAmount ?? (decimal?)0),
                };

            var certAuthorityOtherCorrections =
                from cac in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>().Where(contractReportCertAuthorityCorrectionsPredicate)
                join p in advancePayments.Select(x => x.ContractReportPaymentId).Distinct() on cac.ContractReportPaymentId equals p
                join aarcfc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertCorrection>() on cac.ContractReportCertAuthorityCorrectionId equals aarcfc.ContractReportCertAuthorityCorrectionId
                join aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>() on aarcfc.AnnualAccountReportId equals aar.AnnualAccountReportId
                where aar.Status == AnnualAccountReportStatus.Ended
                group cac by cac.ContractId into g
                select new
                {
                    ContractId = g.Key,
                    CorrectedAmount = g.Sum(x => (int)x.Sign * x.CertifiedBfpTotalAmount ?? (decimal?)0),
                };

            var contractReportCorrectionCertReports =
                from crcc in this.unitOfWork.DbContext.Set<ContractReportCorrection>().Where(contractReportCorrectionsPredicate)
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crcc.ContractReportPaymentId equals crp.ContractReportPaymentId
                join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crcc.CertReportId equals cr.CertReportId

                where crp.PaymentType == ContractReportPaymentType.AdvanceVerification
                select new
                {
                    ContractId = crp.ContractId,
                    CertReportNumber = cr.CertReportNumber,
                };

            var contractReportRevalidationCertReports =
                from crr in this.unitOfWork.DbContext.Set<ContractReportRevalidation>().Where(contractReportRevalidationsPredicate)
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crr.ContractReportPaymentId equals crp.ContractReportPaymentId
                join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crr.CertReportId equals cr.CertReportId
                where crp.PaymentType == ContractReportPaymentType.AdvanceVerification
                select new
                {
                    ContractId = crr.ContractId.Value,
                    CertReportNumber = cr.CertReportNumber,
                };

            var revalidations =
                from ap in advancePayments
                join crr in this.unitOfWork.DbContext.Set<ContractReportRevalidation>().Where(contractReportRevalidationsPredicate) on ap.ContractReportPaymentId equals crr.ContractReportPaymentId
                join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crr.CertReportId equals cr.CertReportId
                group crr by crr.ContractId into g
                select new
                {
                    ContractId = g.Key,
                    CorrectedAmount = g.Sum(x => (int)x.Sign * x.CertifiedRevalidatedBfpTotalAmount ?? (decimal?)0),
                };

            var revalidationCSDs =
                from apc in advancePaymentCosts
                join crfrcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>().Where(financialRevalidationCsdsPredicate) on apc.Id equals crfrcsd.ContractReportFinancialCSDBudgetItemId
                join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crfrcsd.CertReportId equals cr.CertReportId
                group crfrcsd by crfrcsd.ContractId into g
                select new
                {
                    ContractId = g.Key,
                    CorrectedAmount = g.Sum(x => (int)x.Sign * x.CertifiedRevalidatedBfpTotalAmount ?? (decimal?)0),
                };

            var contractReportCertCorrectionCertReports =
                from crc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>().Where(contractReportCertCorrectionsPredicate)
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crc.ContractReportPaymentId equals crp.ContractReportPaymentId
                join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crc.CertReportId equals cr.CertReportId
                where crp.PaymentType == ContractReportPaymentType.AdvanceVerification
                select new
                {
                    ContractId = crc.ContractId.Value,
                    CertReportNumber = cr.CertReportNumber,
                };

            var contractReportFinancialCorrectionCSDsCertReports =
                from crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>().Where(financialCorrectionCsdsPredicate)
                join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crfccsd.CertReportId equals fbi.CertReportId
                join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crfccsd.CertReportId equals cr.CertReportId
                where fbi.AdvancePayment == YesNoNonApplicable.Yes
                select new
                {
                    ContractId = crfccsd.ContractId,
                    CertReportNumber = cr.CertReportNumber,
                };

            var contractReportFinancialCertCorrectionCSDsCertReports =
                from crfrcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>().Where(financialCertCorrectionCsdsPredicate)
                join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crfrcsd.CertReportId equals fbi.CertReportId
                join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crfrcsd.CertReportId equals cr.CertReportId
                where fbi.AdvancePayment == YesNoNonApplicable.Yes
                select new
                {
                    ContractId = crfrcsd.ContractId,
                    CertReportNumber = cr.CertReportNumber,
                };

            var contractReportFinancialRevalidationCSDsCertReports =
                from crfrcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>().Where(financialRevalidationCsdsPredicate)
                join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crfrcsd.CertReportId equals fbi.CertReportId
                join cr in this.unitOfWork.DbContext.Set<CertReport>().Where(certReportsPredicate) on crfrcsd.CertReportId equals cr.CertReportId
                where fbi.AdvancePayment == YesNoNonApplicable.Yes
                select new
                {
                    ContractId = crfrcsd.ContractId,
                    CertReportNumber = cr.CertReportNumber,
                };

            var certReportsWithAdvancePayments =
                advancePayments
                .Select(r => new { ContractId = r.ContractId, CertReportNumber = r.CertReportNumber })
                .Concat(contractReportCorrectionCertReports)
                .Concat(contractReportRevalidationCertReports)
                .Concat(contractReportCertCorrectionCertReports)
                .Where(x => !string.IsNullOrEmpty(x.CertReportNumber));

            var certReportsWithAdvancePaymentCSDs =
                advancePaymentCosts
                .Select(r => new { ContractId = r.ContractId, CertReportNumber = r.CertReportNumber })
                .Concat(contractReportFinancialCorrectionCSDsCertReports)
                .Concat(contractReportFinancialCertCorrectionCSDsCertReports)
                .Concat(contractReportFinancialRevalidationCSDsCertReports)
                .Where(x => !string.IsNullOrEmpty(x.CertReportNumber));

            return advancePayments
                .Concat(advancePaymentCosts)
                .ToList()
                .GroupBy(g => new
                {
                    g.ContractId,
                    g.ContractRegNum,
                    g.BeneficiaryName,
                })
                .Select(g =>
                    {
                        var correctedVerifiedValue = g.Sum(t => t.VerifiedValue) +
                            (advancePaymentOtherCorrections.SingleOrDefault(ca => ca.ContractId == g.Key.ContractId)?.CorrectedAmount ?? 0);
                        var correctedVerifiedCosts = g.Sum(t => t.VerifiedCosts) +
                            (advancePaymentCSDCorrections.SingleOrDefault(ca => ca.ContractId == g.Key.ContractId)?.CorrectedAmount ?? 0) +
                            (advancePaymentCostsCSDCorrections.SingleOrDefault(ca => ca.ContractId == g.Key.ContractId)?.CorrectedAmount ?? 0);
                        var correctedCertifiedPayment = g.Sum(t => t.CertifiedPayment) +
                            (certAuthorityOtherCorrections.SingleOrDefault(ca => ca.ContractId == g.Key.ContractId)?.CorrectedAmount ?? 0) +
                            (advancePaymentOtherCorrections.SingleOrDefault(ap => ap.ContractId == g.Key.ContractId)?.CertifiedPayment ?? 0) +
                            (revalidations.SingleOrDefault(ca => ca.ContractId == g.Key.ContractId)?.CorrectedAmount ?? 0);
                        var correctedCertAdvancePaymentExpenses = g.Sum(t => t.CertAdvancePaymentExpenses) +
                            (advancePaymentCostsCSDCorrections.SingleOrDefault(ca => ca.ContractId == g.Key.ContractId)?.CertAdvancePaymentExpenses ?? 0) +
                            (certAuthorityCSDCorrections.SingleOrDefault(ca => ca.ContractId == g.Key.ContractId)?.CorrectedAmount ?? 0) +
                            (revalidationCSDs.SingleOrDefault(ca => ca.ContractId == g.Key.ContractId)?.CorrectedAmount ?? 0);
                        return new AdvancePaymentsReportItem
                        {
                            ContractRegNum = g.Key.ContractRegNum,
                            BeneficiaryName = g.Key.BeneficiaryName,
                            VerifiedValue = correctedVerifiedValue,
                            VerifiedCosts = correctedVerifiedCosts,
                            VerifiedNonCoveredValue = correctedVerifiedValue - correctedVerifiedCosts,
                            CertifiedPayment = correctedCertifiedPayment,
                            CertAdvancePaymentExpenses = correctedCertAdvancePaymentExpenses,
                            CertifiedNonCoveredValue = correctedCertifiedPayment - correctedCertAdvancePaymentExpenses,
                            CertReportsWithAdvencePayments = string.Join(",", certReportsWithAdvancePayments.Where(x => x.ContractId == g.Key.ContractId).Select(x => x.CertReportNumber).Distinct()),
                            CertReportsWithAdvancePaymentExpenses = string.Join(",", certReportsWithAdvancePaymentCSDs.Where(x => x.ContractId == g.Key.ContractId).Select(x => x.CertReportNumber).Distinct()),
                        };
                    }).ToList();
        }

        public Anex3ReportVO GetAnex3Report(int contractId)
        {
            var contractData =
                (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                 join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Include(con => con.ContractLocations) on cv.ContractId equals c.ContractId
                 join prog in this.unitOfWork.DbContext.Set<Programme>() on c.ProgrammeId equals prog.MapNodeId
                 where cv.ContractId == contractId && cv.Status == ContractVersionStatus.Active && c.ContractStatus == ContractStatus.Entered
                 select new
                 {
                     c.CompanyUin,
                     c.CompanyName,
                     c.Name,
                     c.RegNumber,
                     c.Description,
                     c.ContractDate,
                     c.StartDate,
                     c.CompletionDate,
                     c.ExecutionStatus,
                     c.ProcedureId,
                     c.NutsLevel,
                     c.ContractLocations,
                     ProgrammeId = prog.MapNodeId,
                     ProgrammeCode = prog.Code,
                     Version = cv,
                 }).SingleOrDefault();

            if (contractData == null)
            {
                return null;
            }

            var procedureShares = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                   where ps.ProcedureId == contractData.ProcedureId && ps.IsActivated && ps.ProgrammeId == contractData.ProgrammeId
                                   select new
                                   {
                                       ProgrammePriorityName = pp.ShortName,
                                       ProgrammePriorityCode = pp.Code,
                                   }).ToList();

            var indicatorPeriods = (from cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>()
                                    join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on cri.ContractIndicatorId equals ci.ContractIndicatorId
                                    join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>() on cri.ContractReportTechnicalId equals crt.ContractReportTechnicalId
                                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on cri.ContractReportId equals cr.ContractReportId
                                    where cri.ContractId == contractId && cri.Status == ContractReportIndicatorStatus.Ended && crt.Status == ContractReportTechnicalStatus.Actual && cr.Status == ContractReportStatus.Accepted
                                    group new
                                    {
                                        cri.ContractIndicatorId,
                                        DateTo = crt.DateTo.Value,
                                        cri.CumulativeAmountTotal,
                                        cri.CumulativeAmountMen,
                                        cri.CumulativeAmountWomen,
                                        cri.PeriodAmountTotal,
                                        cri.PeriodAmountMen,
                                        cri.PeriodAmountWomen,
                                    }
                                    by new { crt.DateTo.Value.Year, ci.IndicatorId } into g0
                                    select g0.OrderByDescending(x => x.DateTo).FirstOrDefault() into r
                                    select new
                                    {
                                        r.DateTo,
                                        r.ContractIndicatorId,
                                        r.CumulativeAmountTotal,
                                        r.CumulativeAmountMen,
                                        r.CumulativeAmountWomen,
                                        r.PeriodAmountTotal,
                                        r.PeriodAmountMen,
                                        r.PeriodAmountWomen,
                                    }).ToList();

            var indicators = (from ci in this.unitOfWork.DbContext.Set<ContractIndicator>()
                              join i in this.unitOfWork.DbContext.Set<Domain.Indicators.Indicator>() on ci.IndicatorId equals i.IndicatorId
                              join m in this.unitOfWork.DbContext.Set<Measure>() on i.MeasureId equals m.MeasureId
                              where ci.ContractId == contractId && ci.IsActive
                              select new
                              {
                                  ci.ContractIndicatorId,
                                  i.Name,
                                  Measure = m.Name,
                                  i.HasGenderDivision,
                                  ci.BaseTotalValue,
                                  ci.BaseMenValue,
                                  ci.BaseWomenValue,
                                  ci.TargetTotalValue,
                                  ci.TargetMenValue,
                                  ci.TargetWomenValue,
                              }).ToList()
                                .Select(o => new Anex3IndicatorVO
                                {
                                    Name = o.Name,
                                    Measure = o.Measure,
                                    HasGenderDivision = o.HasGenderDivision,
                                    BaseTotalValue = o.BaseTotalValue,
                                    BaseMenValue = o.BaseMenValue,
                                    BaseWomenValue = o.BaseWomenValue,
                                    TargetTotalValue = o.TargetTotalValue,
                                    TargetMenValue = o.TargetMenValue,
                                    TargetWomenValue = o.TargetWomenValue,
                                    PeriodAmounts = indicatorPeriods
                                            .Where(ip => ip.ContractIndicatorId == o.ContractIndicatorId)
                                            .ToDictionary(p => p.DateTo.Year, p => new MonitoringIndicatorPeriodAmountVO(p.CumulativeAmountTotal, p.CumulativeAmountMen, p.CumulativeAmountWomen, p.PeriodAmountTotal, p.PeriodAmountMen, p.PeriodAmountWomen)),
                                }).ToList();

            var firstContractSpotCheck =
                (from sc in this.unitOfWork.DbContext.Set<SpotCheck>()
                 join sci in this.unitOfWork.DbContext.Set<SpotCheckItem>() on sc.SpotCheckId equals sci.SpotCheckId into g0
                 from sci in g0.DefaultIfEmpty()
                 where sc.Level == SpotCheckLevel.Contract && sci.ContractId == contractId && sc.Status == SpotCheckStatus.Entered
                 orderby sc.DateFrom
                 select new { sc.DateFrom, sc.Team }).FirstOrDefault();

            var firstContractAudit =
                (from a in this.unitOfWork.DbContext.Set<Audit>()
                 join ai in this.unitOfWork.DbContext.Set<AuditLevelItem>() on a.AuditId equals ai.AuditId into g0
                 from ai in g0.DefaultIfEmpty()
                 where a.Level == AuditLevel.Contract && ai.ContractId == contractId
                 orderby a.CheckStartDate
                 select new { a.CheckStartDate, a.AuditInstitution }).FirstOrDefault();

            var payments =
                (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                 join crpc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>() on crp.ContractReportPaymentId equals crpc.ContractReportPaymentId
                 join crpca in this.unitOfWork.DbContext.Set<ContractReportPaymentCheckAmount>() on crpc.ContractReportPaymentCheckId equals crpca.ContractReportPaymentCheckId
                 join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                 where crp.ContractId == contractId && crp.Status == ContractReportPaymentStatus.Actual && crpc.Status == ContractReportPaymentCheckStatus.Active && crpc.Approval == ContractReportPaymentCheckApproval.Approved && cr.Status == ContractReportStatus.Accepted

                 group new
                 {
                     crpca.ApprovedTotalAmount,
                     crpca.ApprovedBfpTotalAmount,
                     crpca.PaidBfpTotalAmount,
                 }
                 by new
                 {
                     cr.SubmitDate,
                     cr.CheckedDate,
                     crp.AdditionalIncome,
                 }
                 into g
                 select new
                 {
                     g.Key.SubmitDate,
                     g.Key.CheckedDate,
                     ApprovedTotalAmount = g.Sum(t => t.ApprovedTotalAmount),
                     ApprovedBfpTotalAmount = g.Sum(t => t.ApprovedBfpTotalAmount),
                     PaidBfpTotalAmount = g.Sum(t => t.PaidBfpTotalAmount),
                     g.Key.AdditionalIncome,
                 })
                 .ToList()
                 .Select(o => new Anex3PaymentVO
                 {
                     SubmitDate = o.SubmitDate,
                     CheckedDate = o.CheckedDate,
                     ApprovedTotalAmount = o.ApprovedTotalAmount,
                     ApprovedBfpTotalAmount = o.ApprovedBfpTotalAmount,
                     PaidBfpTotalAmount = o.PaidBfpTotalAmount,
                     AdditionalIncome = o.AdditionalIncome,
                     SpotCheckDateFrom = firstContractSpotCheck == null ? null : firstContractSpotCheck.DateFrom,
                     SpotCheckTeam = firstContractSpotCheck == null ? null : firstContractSpotCheck.Team,
                     AuditStartDate = firstContractAudit == null ? null : firstContractAudit.CheckStartDate,
                     AuditInstitution = firstContractAudit == null ? (AuditInstitution?)null : firstContractAudit.AuditInstitution,
                 }).ToList();

            var financialCSDBudgetItems =
                (from fcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                 join cr in this.unitOfWork.DbContext.Set<ContractReport>() on fcsd.ContractReportId equals cr.ContractReportId
                 join bi3 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel3>() on fcsd.BudgetDetailGid equals bi3.Gid
                 join bi2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on bi3.ProcedureBudgetLevel2Id equals bi2.ProcedureBudgetLevel2Id
                 where cr.ContractId == contractId && fcsd.CostSupportingDocumentApproved.Value && cr.Status == ContractReportStatus.Accepted
                 select new
                 {
                     fcsd.ApprovedTotalAmount,
                     fcsd.ApprovedBfpTotalAmount,
                     fcsd.UnitDefinition,
                     fcsd.ProducedUnitsCount,
                     fcsd.UnitCost,
                 }).ToList();

            var reimbursedAmounts =
                (from dra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                 join cd in this.unitOfWork.DbContext.Set<ContractDebt>() on dra.ContractDebtId equals cd.ContractDebtId
                 join cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>() on cd.ContractDebtId equals cdv.ContractDebtId
                 where dra.ContractId == contractId && dra.Status == ReimbursedAmountStatus.Entered && cdv.Status == ContractDebtVersionStatus.Actual && cd.Status == ContractDebtStatus.Entered
                 select new
                 {
                     cd.DebtStartDate,
                     DebtTotalAmount = cdv.TotalAmount,
                     dra.Reimbursement,
                     dra.ReimbursementDate,
                     ReimbursementTotalAmount = dra.PrincipalBfp.TotalAmount,
                 }).ToList()
                 .Select(o => new Anex3ReimbursedAmountVO
                 {
                     DebtStartDate = o.DebtStartDate,
                     DebtBfpTotalAmount = o.DebtTotalAmount,
                     DebtTotalAmount = o.DebtTotalAmount,
                     ReimbursementDate = o.ReimbursementDate,
                     ReimbursementBfpTotalAmount = o.Reimbursement == Reimbursement.WrittenOff ? null : o.ReimbursementTotalAmount,
                     ReimbursementTotalAmount = o.Reimbursement == Reimbursement.WrittenOff ? null : o.ReimbursementTotalAmount,
                     WrittenOffBfpTotalAmount = o.Reimbursement == Reimbursement.WrittenOff ? o.ReimbursementTotalAmount : null,
                     WrittenOffTotalAmount = o.Reimbursement == Reimbursement.WrittenOff ? o.ReimbursementTotalAmount : null,
                 }).ToList();

            var versionData = contractData.Version.GetDocument();

            return new Anex3ReportVO
            {
                CompanyUin = contractData.CompanyUin,
                CompanyName = contractData.CompanyName,
                RegNumber = contractData.RegNumber,
                Name = contractData.Name,
                Description = contractData.Description,
                IsVatEligible = versionData.BFPContractBasicData.GetEnum<Rio.BFPContractBasicData, YesNoOther>(c => c.IsVatEligible.Id),
                CorrespondenceAddress = this.GetContractAddress(versionData.Beneficiary.Correspondence),
                CorrespondenceEmail = versionData.Beneficiary.Email,
                CorrespondencePhone1 = versionData.Beneficiary.Phone1,
                CorrespondencePhone2 = versionData.Beneficiary.Phone2,
                CorrespondenceFax = versionData.Beneficiary.Fax,
                CompanyContactPerson = versionData.Beneficiary.CompanyContactPerson,
                CompanyContactPersonPhone = versionData.Beneficiary.CompanyContactPersonPhone,
                CompanyContactPersonEmail = versionData.Beneficiary.CompanyContactPersonEmail,
                RegDate = contractData.ContractDate,
                StartDate = contractData.StartDate,
                EndDate = contractData.CompletionDate,
                CompletionDate = contractData.ExecutionStatus == ContractExecutionStatus.Ended ? contractData.CompletionDate : (DateTime?)null,
                DocDate = contractData.ContractDate,
                AmountType = versionData.BFPContractBasicData.GetEnum<Rio.BFPContractBasicData, ContractAmountType>(c => c.AmountType.Id),
                IsJointActionPlan = versionData.BFPContractBasicData.IsJointActionPlan,
                IncludesSupportFromIYE = versionData.BFPContractBasicData.IsIncludesSupportFromIYE,
                IsSubjectToStateAidRegime = versionData.BFPContractBasicData.IsSubjectToStateAidRegime,
                IncludesPublicPrivatePartnership = versionData.BFPContractBasicData.IsIncludesPublicPrivatePartnership,
                Currency = "лева",
                ProgrammeCode = contractData.ProgrammeCode,
                ProgrammePriorities = procedureShares.Select(ps => string.Format("{0} {1}", ps.ProgrammePriorityCode, ps.ProgrammePriorityName)).Distinct().ToList(),
                NutsLevel = contractData.NutsLevel,
                Locations = contractData.ContractLocations.Select(cl => string.Format("{0} {1}", cl.NutsCode, cl.Name)).ToList(),
                Indicators = indicators,
                ApprovedExpenses = versionData.BFPContractDirectionsBudgetContract.Contract.TotalEligibleCosts,
                PublicExpenses = versionData.BFPContractDirectionsBudgetContract.Contract.TotalEligibleCostsPublicFunding,
                PublicAmounts = versionData.BFPContractDirectionsBudgetContract.Contract.RequestedFundingAmount,
                ReportPayments = payments,
                StandardTablesExpenses = financialCSDBudgetItems
                    .Select(fcsd => new Anex3StandardTablesExpenseVO
                    {
                        ApprovedTotalAmount = fcsd.ApprovedTotalAmount,
                        ApprovedBfpTotalAmount = fcsd.ApprovedBfpTotalAmount,
                        UnitDefinition = fcsd.UnitDefinition,
                        ProducedUnitsCount = fcsd.ProducedUnitsCount,
                        UnitCost = fcsd.UnitCost,
                    }).ToList(),
                FlatRateExpenses = financialCSDBudgetItems
                    .Select(fcsd => new Anex3FlatRateExpenseVO
                    {
                        ApprovedTotalAmount = fcsd.ApprovedTotalAmount,
                        ApprovedBfpTotalAmount = fcsd.ApprovedBfpTotalAmount,
                    }).ToList(),
                ReimbursedAmounts = reimbursedAmounts,
            };
        }

        public IList<DoubleFundingReportItem> GetDoubleFundingReport(string uin)
        {
            var companyAsBeneficiary = (from c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>()
                                        where c.CompanyUin == uin && c.ContractStatus == ContractStatus.Entered
                                        orderby c.ContractId
                                        select new DoubleFundingReportItem
                                        {
                                            ContractId = c.ContractId,
                                            BeneficiaryName = c.CompanyName,
                                            BeneficiaryUin = c.CompanyUin,
                                            ContractRegNum = c.RegNumber,
                                            ContractTotalAmount = c.TotalAmount,
                                            ContractBfpAmount = c.TotalBfpAmount,
                                        }).ToList();

            var companyAsPartner = (from cp in this.unitOfWork.DbContext.Set<Domain.Contracts.ContractPartner>()
                                    join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on cp.ContractId equals c.ContractId
                                    where cp.Uin == uin && c.ContractStatus == ContractStatus.Entered
                                    orderby c.ContractId
                                    select new DoubleFundingReportItem
                                    {
                                        ContractId = c.ContractId,
                                        BeneficiaryName = c.CompanyName,
                                        BeneficiaryUin = c.CompanyUin,
                                        ContractRegNum = c.RegNumber,
                                        ContractPartnerName = cp.Name,
                                        ContractPartnerUin = cp.Uin,
                                        ContractTotalAmount = c.TotalAmount,
                                        ContractBfpAmount = c.TotalBfpAmount,
                                    }).ToList();

            return companyAsBeneficiary.Concat(companyAsPartner).ToList();
        }

        private IEnumerable<IGrouping<(int year, int iId, int? ppId, int? ipId, int? stId), (decimal? total, decimal? men, decimal? women)>> GetContractReportIndicators(int[] indicatorIds)
        {
            var actualContractReportTechnicalCorrectionIndicators =
                from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                join tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>() on tci.ContractReportTechnicalCorrectionId equals tc.ContractReportTechnicalCorrectionId
                where tc.Status == ContractReportTechnicalCorrectionStatus.Ended
                select tci;

            return (from cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>()
                    join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on cri.ContractIndicatorId equals ci.ContractIndicatorId
                    join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>() on cri.ContractReportTechnicalId equals crt.ContractReportTechnicalId
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on cri.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on cr.ContractId equals c.ContractId

                    join tci in actualContractReportTechnicalCorrectionIndicators on cri.ContractReportIndicatorId equals tci.ContractReportIndicatorId into g0
                    from tci in g0.DefaultIfEmpty()

                    where cr.Status == ContractReportStatus.Accepted &&
                        cri.Status == ContractReportIndicatorStatus.Ended &&
                        crt.Status == ContractReportTechnicalStatus.Actual &&
                        indicatorIds.Contains(ci.IndicatorId)
                    group new
                    {
                        c.ContractId,
                        ci.ProgrammePriorityId,
                        ci.InvestmentPriorityId,
                        ci.SpecificTargetId,
                        ci.IndicatorId,
                        DateTo = crt.DateTo.Value,
                        ApprovedCumulativeAmountTotal = tci == null ? cri.ApprovedCumulativeAmountTotal : tci.CorrectedApprovedCumulativeAmountTotal,
                        ApprovedCumulativeAmountMen = tci == null ? cri.ApprovedCumulativeAmountMen : tci.CorrectedApprovedCumulativeAmountMen,
                        ApprovedCumulativeAmountWomen = tci == null ? cri.ApprovedCumulativeAmountWomen : tci.CorrectedApprovedCumulativeAmountWomen,
                    }
                    by new
                    {
                        crt.DateTo.Value.Year,
                        ci.IndicatorId,
                        c.ContractId,
                        ci.ProgrammePriorityId,
                        ci.InvestmentPriorityId,
                        ci.SpecificTargetId,
                    }
                    into g0
                    select g0.OrderByDescending(x => x.DateTo).FirstOrDefault() into r
                    select new
                    {
                        r.ContractId,
                        r.ProgrammePriorityId,
                        r.InvestmentPriorityId,
                        r.SpecificTargetId,
                        r.DateTo,
                        r.IndicatorId,
                        r.ApprovedCumulativeAmountTotal,
                        r.ApprovedCumulativeAmountMen,
                        r.ApprovedCumulativeAmountWomen,
                    }).ToList()
                    .GroupBy(
                        o =>
#pragma warning disable SA1101 // Prefix local calls with this // value tuples break this rule
                        (
                            year: o.DateTo.Year,
                            iId: o.IndicatorId,
                            ppId: o.ProgrammePriorityId,
                            ipId: o.InvestmentPriorityId,
                            stId: o.SpecificTargetId),
                        o =>
                        (
                            total: o.ApprovedCumulativeAmountTotal,
                            men: o.ApprovedCumulativeAmountMen,
                            women: o.ApprovedCumulativeAmountWomen));
#pragma warning restore SA1101 // Prefix local calls with this
        }

        public IList<FinancialExecutionTable1ReportItem> GetFinancialExecutionTable1Report(int programmeId, DateTime date, int? programmePriorityId = null)
        {
            var ppPredicate = PredicateBuilder.True<MapNodeBudget>()
                .And(mnb => mnb.ProgrammeId == programmeId)
                .AndEquals(mnb => mnb.MapNodeId, programmePriorityId);
            var psPredicate = PredicateBuilder.True<ProcedureShare>()
                .And(ps => ps.ProgrammeId == programmeId)
                .AndEquals(ps => ps.ProgrammePriorityId, programmePriorityId);

            List<ContractExecutionStatus?> correctionStatuses = new List<ContractExecutionStatus?>()
            {
                ContractExecutionStatus.Suspended,
                ContractExecutionStatus.Canceled,
                ContractExecutionStatus.Ended,
            };

            var oldTimeout = this.unitOfWork.DbContext.Database.CommandTimeout;
            this.unitOfWork.DbContext.Database.CommandTimeout = 10 * 60;

            var lastContractsVersions =
                from cdv in this.unitOfWork.DbContext.Set<ContractVersionXml>().Where(x => x.ContractDate <= date && (x.Status == ContractVersionStatus.Archived || x.Status == ContractVersionStatus.Active))
                join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(x => x.ProgrammeId == programmeId) on cdv.ContractId equals c.ContractId
                group cdv by cdv.ContractId into g1
                let maxVersionNumber = g1.Max(x => x.OrderNum)
                from cdv in g1
                where cdv.OrderNum == maxVersionNumber
                select new
                {
                    cdv.ContractId,
                    cdv.ExecutionStatus,
                    cdv.ContractVersionXmlId,
                };

            var contractedAmounts =
                (from cdv in lastContractsVersions
                 join cva in this.unitOfWork.DbContext.Set<ContractVersionXmlAmount>() on cdv.ContractVersionXmlId equals cva.ContractVersionXmlId
                 join pbl in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cva.ProcedureBudgetLevel2Id equals pbl.ProcedureBudgetLevel2Id
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(psPredicate) on pbl.ProcedureShareId equals ps.ProcedureShareId
                 select new
                 {
                     ps.ProgrammeId,
                     ps.ProgrammePriorityId,
                     cdv.ContractId,
                     ContractedBfpAmount = correctionStatuses.Contains(cdv.ExecutionStatus) ? 0 : cva.CurrentBgAmount + cva.CurrentEuAmount,
                     ContractedSelfAmount = correctionStatuses.Contains(cdv.ExecutionStatus) ? 0 : cva.CurrentSelfAmount,
                     ContractCanceled = cdv.ExecutionStatus == ContractExecutionStatus.Canceled,
                 }
                 into g2
                 group g2 by new { g2.ProgrammeId, g2.ProgrammePriorityId } into g3
                 select new
                 {
                     ProgrammeId = g3.Key.ProgrammeId,
                     ProgrammePriorityId = g3.Key.ProgrammePriorityId,
                     ContractedBfpAmount = g3.Sum(o => o.ContractedBfpAmount),
                     ContractedSelfAmount = g3.Sum(o => o.ContractedSelfAmount),
                     ContractsCount = g3.Where(o => !o.ContractCanceled).Select(o => o.ContractId).Distinct().Count(),
                 }).ToList();

            var correctedCsdAmounts =
                (from cbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                 join c in lastContractsVersions on cbi.ContractId equals c.ContractId
                 join cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on cbi.ContractBudgetLevel3AmountId equals cba.ContractBudgetLevel3AmountId
                 join pbl in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cba.ProcedureBudgetLevel2Id equals pbl.ProcedureBudgetLevel2Id
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(psPredicate) on pbl.ProcedureShareId equals ps.ProcedureShareId
                 join crrcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on cbi.ContractReportFinancialCSDBudgetItemId equals crrcsd.ContractReportFinancialCSDBudgetItemId
                 join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crrcsd.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                 where correctionStatuses.Contains(c.ExecutionStatus) && crrcsd.Status == ContractReportFinancialCorrectionCSDStatus.Ended && crfc.CorrectionDate <= date
                 group crrcsd by new { crrcsd.Sign, ps.ProgrammePriorityId, ps.ProgrammeId } into g3
                 select new
                 {
                     g3.Key.ProgrammeId,
                     g3.Key.ProgrammePriorityId,
                     CorrectedBfpAmount = (decimal?)((g3.Key.Sign == Sign.Positive ? -1 : 1) * ((g3.Sum(o => o.CorrectedApprovedBgAmount) ?? 0) + g3.Sum(o => o.CorrectedApprovedEuAmount) ?? 0)),
                     CorrectedSelfAmount = (decimal?)((g3.Key.Sign == Sign.Positive ? -1 : 1) * g3.Sum(o => o.CorrectedApprovedSelfAmount) ?? 0),
                 }
                 into g4
                 group g4 by new { g4.ProgrammeId, g4.ProgrammePriorityId } into g5
                 select new
                 {
                     g5.Key.ProgrammeId,
                     g5.Key.ProgrammePriorityId,
                     CorrectedBfpAmount = g5.Sum(o => o.CorrectedBfpAmount),
                     CorrectedSelfAmount = g5.Sum(o => o.CorrectedSelfAmount),
                 }).ToList();

            var reportedAmounts =
                (from csdb in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                 join cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on csdb.ContractBudgetLevel3AmountId equals cba.ContractBudgetLevel3AmountId
                 join pbl in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cba.ProcedureBudgetLevel2Id equals pbl.ProcedureBudgetLevel2Id
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(psPredicate) on pbl.ProcedureShareId equals ps.ProcedureShareId
                 join cr in this.unitOfWork.DbContext.Set<ContractReport>() on csdb.ContractReportId equals cr.ContractReportId
                 join lcv in lastContractsVersions on cr.ContractId equals lcv.ContractId
                 where cr.Status != ContractReportStatus.Refused && cr.SubmitDate <= date
                 group new { csdb.BfpTotalAmount, csdb.SelfAmount, csdb.ApprovedBfpTotalAmount, csdb.ApprovedSelfAmount } by new { ps.ProgrammeId, ps.ProgrammePriorityId, lcv.ExecutionStatus } into g
                 select new
                 {
                     ProgrammeId = g.Key.ProgrammeId,
                     ProgrammePriorityId = g.Key.ProgrammePriorityId,
                     ReportedBfpAmount = g.Sum(o => o.BfpTotalAmount),
                     ReportedSelfAmount = g.Sum(o => o.SelfAmount),
                     ApprovedBfpTotalAmount = correctionStatuses.Contains(g.Key.ExecutionStatus) ? g.Sum(o => o.ApprovedBfpTotalAmount) : 0,
                     ApprovedSelfAmount = correctionStatuses.Contains(g.Key.ExecutionStatus) ? g.Sum(o => o.ApprovedSelfAmount) : 0,
                 }
                 into g1
                 group new { g1.ReportedBfpAmount, g1.ReportedSelfAmount, g1.ApprovedBfpTotalAmount, g1.ApprovedSelfAmount } by new { g1.ProgrammeId, g1.ProgrammePriorityId } into g2
                 select new
                 {
                     ProgrammeId = g2.Key.ProgrammeId,
                     ProgrammePriorityId = g2.Key.ProgrammePriorityId,
                     ReportedBfpAmount = g2.Sum(o => o.ReportedBfpAmount),
                     ReportedSelfAmount = g2.Sum(o => o.ReportedSelfAmount),
                     ApprovedBfpTotalAmount = g2.Sum(o => o.ApprovedBfpTotalAmount),
                     ApprovedSelfAmount = g2.Sum(o => o.ApprovedSelfAmount),
                 }).ToList();

            var result = (from mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>().Where(ppPredicate)
                          join p in this.unitOfWork.DbContext.Set<Programme>() on mnb.ProgrammeId equals p.MapNodeId
                          join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on mnb.MapNodeId equals pp.MapNodeId
                          group mnb by new { mnb.ProgrammeId, mnb.MapNodeId, Programme = p.Name, ProgrammePriority = pp.Name } into g
                          select new
                          {
                              ProgrammeId = g.Key.ProgrammeId,
                              Programme = g.Key.Programme,
                              ProgrammePriorityId = g.Key.MapNodeId,
                              ProgrammePriority = g.Key.ProgrammePriority,
                              BudgetEuAmount = g.Sum(o => o.EuAmount) + g.Sum(o => o.EuReservedAmount),
                              BudgetBgAmount = g.Sum(o => o.BgAmount) + g.Sum(o => o.BgReservedAmount),
                          }).ToList()
                        .Select(o =>
                        {
                            var currContractedAmounts = contractedAmounts.SingleOrDefault(ca => ca.ProgrammeId == o.ProgrammeId && ca.ProgrammePriorityId == o.ProgrammePriorityId);
                            var currCorrectedCsdAmounts = correctedCsdAmounts.SingleOrDefault(ca => ca.ProgrammeId == o.ProgrammeId && ca.ProgrammePriorityId == o.ProgrammePriorityId);
                            var currReportedAmounts = reportedAmounts.SingleOrDefault(ra => ra.ProgrammeId == o.ProgrammeId && ra.ProgrammePriorityId == o.ProgrammePriorityId);
                            return new FinancialExecutionTable1ReportItem(
                                programme: o.Programme,
                                programmePriority: o.ProgrammePriority,
                                budgetBfpAmount: (o.BudgetBgAmount + o.BudgetEuAmount) / euroExchangeRates,
                                contractedTotalAmount: (decimal)(
                                    (currContractedAmounts?.ContractedBfpAmount ?? 0) + (currContractedAmounts?.ContractedSelfAmount ?? 0) +
                                    (currReportedAmounts?.ApprovedBfpTotalAmount ?? 0) + (currReportedAmounts?.ApprovedSelfAmount ?? 0) +
                                    (currCorrectedCsdAmounts?.CorrectedBfpAmount ?? 0) + (currCorrectedCsdAmounts?.CorrectedSelfAmount ?? 0)) / euroExchangeRates,
                                reportedTotalAmount: currReportedAmounts == null ? 0 : (currReportedAmounts.ReportedBfpAmount + currReportedAmounts.ReportedSelfAmount) / euroExchangeRates,
                                contractedBfpAmount: (
                                    (currContractedAmounts?.ContractedBfpAmount ?? 0) + (currReportedAmounts?.ApprovedBfpTotalAmount ?? 0) +
                                    (currCorrectedCsdAmounts?.CorrectedBfpAmount ?? 0)) / euroExchangeRates,
                                contractsCount: currContractedAmounts == null ? 0 : currContractedAmounts.ContractsCount);
                        }).ToList();

            this.unitOfWork.DbContext.Database.CommandTimeout = oldTimeout;
            return result;
        }

        public IList<FinancialExecutionTable2ReportItem> GetFinancialExecutionTable2Report(int programmeId, DateTime date, int? programmePriorityId = null)
        {
            throw new NotImplementedException();
        }

        public IList<FinancialExecutionTable3ReportItem> GetFinancialExecutionTable3Report(int programmeId, Year year)
        {
            var years = new Year[] { year, (Year)(year + 1) };

            return (from p in this.unitOfWork.DbContext.Set<Prognosis>()
                    join prog in this.unitOfWork.DbContext.Set<Programme>() on p.ProgrammeId equals prog.MapNodeId
                    where p.ProgrammeId == programmeId &&
                        p.Level == PrognosisLevel.Programme &&
                        p.Status == PrognosisStatus.Entered &&
                        years.Contains(p.Year)
                    select new
                    {
                        Programme = prog.Name,
                        Year = p.Year,
                        Month = p.Month,
                        PaymentEuAmount = p.IntermediatePayment.EuAmount,
                    })
                    .ToList()
                    .Select(g =>
                        {
                            return new FinancialExecutionTable3ReportItem(
                                programme: g.Programme,
                                currYearGroup1PaymentsEuAmount: 0,
                                currYearGroup2PaymentsEuAmount: 0,
                                nextYearPaymentsEuAmount: 0);
                        })
                    .ToList();
        }

        public IList<ProjectsReportItem> GetProjectsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            var divisor = 1m;

            if (currency == Currency.Euro)
            {
                divisor = euroExchangeRates;
            }

            IQueryable<Domain.Projects.Project> projects = this.unitOfWork.DbContext.Set<Domain.Projects.Project>();
            string fullPath = this.GetNutsFullPath(
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);

            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                projects = from p in projects
                           where p.NutsAddressFullPath.Contains(fullPath)
                           select p;
            }

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var projectPredicate = PredicateBuilder.True<Domain.Projects.Project>()
                .AndEquals(p => p.ProcedureId, procedureId)
                .AndDateTimeGreaterThanOrEqual(p => p.RegDate, fromDate)
                .AndDateTimeLessThanOrEqual(p => p.RegDate, toDate);

            var procedureSharePredicate = PredicateBuilder.True<ProcedureShare>()
                .And(ps => ps.IsPrimary && ps.IsActivated)
                .AndEquals(ps => ps.ProgrammeId, programmeId)
                .AndEquals(ps => ps.ProgrammePriorityId, programmePriorityId);

            var projectEvaluations =
                (from ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>()
                 join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on ese.ProjectId equals esp.ProjectId
                 join p in projects.Where(projectPredicate) on esp.ProjectId equals p.ProjectId
                 join es in this.unitOfWork.DbContext.Set<EvalSession>() on ese.EvalSessionId equals es.EvalSessionId
                 where es.EvalSessionStatus == EvalSessionStatus.Ended && !ese.IsDeleted && !esp.IsDeleted
                 select new { ese.ProjectId, ese.EvalTableType, ese.EvalIsPassed }).ToList();

            return (from p in projects.Where(projectPredicate)
                    join ipvx in this.unitOfWork.DbContext.Set<ProjectVersionXml>() on p.ProjectId equals ipvx.ProjectId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(procedureSharePredicate) on proc.ProcedureId equals ps.ProcedureId
                    join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                    join ct in this.unitOfWork.DbContext.Set<CompanyType>() on p.CompanyTypeId equals ct.CompanyTypeId
                    join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on p.CompanyLegalTypeId equals clt.CompanyLegalTypeId

                    join c1 in this.unitOfWork.DbContext.Set<Country>() on p.CompanySeatCountryId equals c1.CountryId into g0
                    from c1 in g0.DefaultIfEmpty()
                    join s1 in this.unitOfWork.DbContext.Set<Settlement>() on p.CompanySeatSettlementId equals s1.SettlementId into g1
                    from s1 in g1.DefaultIfEmpty()
                    join c2 in this.unitOfWork.DbContext.Set<Country>() on p.CompanyCorrespondenceCountryId equals c2.CountryId into g2
                    from c2 in g2.DefaultIfEmpty()
                    join s2 in this.unitOfWork.DbContext.Set<Settlement>() on p.CompanyCorrespondenceSettlementId equals s2.SettlementId into g3
                    from s2 in g3.DefaultIfEmpty()
                    join pkc in this.unitOfWork.DbContext.Set<KidCode>() on p.KidCodeId equals pkc.KidCodeId into g5
                    from pkc in g5.DefaultIfEmpty()
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>().Where(t => !t.IsDeleted) on p.ProjectId equals esp.ProjectId into g6
                    from esp in g6.DefaultIfEmpty()
                    join es in this.unitOfWork.DbContext.Set<EvalSession>().Where(t => t.EvalSessionStatus == EvalSessionStatus.Ended) on esp.EvalSessionId equals es.EvalSessionId into g7
                    from es in g7.DefaultIfEmpty()
                    join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(t => !t.IsDeleted) on new { esp.EvalSessionId, esp.ProjectId } equals new { esps.EvalSessionId, esps.ProjectId } into g8
                    from esps in g8.DefaultIfEmpty()

                    where ipvx.OrderNum == 1
                    select new
                    {
                        p.ProjectId,
                        Programme = prog.Name,
                        Procedure = proc.Name,
                        p.RegNumber,
                        p.Name,
                        p.RegDate,
                        p.RecieveDate,
                        p.RecieveType,
                        CompanyUin = p.CompanyUin,
                        CompanyName = p.CompanyName,
                        CompanyType = ct.Name,
                        CompanyLegalType = clt.Name,
                        CompanySeatCountryCode = c1.NutsCode,
                        CompanySeatCountry = c1.Name,
                        CompanySeatSettlement = s1.Name,
                        p.CompanySeatPostCode,
                        p.CompanySeatStreet,
                        p.CompanySeatAddress,
                        CompanyCorrespondenceCountryCode = c2.NutsCode,
                        CompanyCorrespondenceCountry = c2.Name,
                        CompanyCorrespondenceSettlement = s2.Name,
                        p.CompanyCorrespondencePostCode,
                        p.CompanyCorrespondenceStreet,
                        p.CompanyCorrespondenceAddress,
                        p.CompanyEmail,
                        p.Duration,
                        p.NutsAddressFullPathName,
                        ProjectKidCode = pkc.Code,
                        ProjectKidCodeName = pkc.Name,
                        InitialTotalBfpAmount = ipvx.TotalBfpAmount,
                        InitialCoFinancingAmount = ipvx.CoFinancingAmount,
                        ActualTotalBfpAmount = p.TotalBfpAmount,
                        IsCanceled = (bool?)esp.IsDeleted,
                        p.RegistrationStatus,
                        StandingStatus = (EvalSessionProjectStandingStatus?)esps.Status,
                    }).ToList()
                    .Select(o =>
                        {
                            var evaluations = projectEvaluations.Where(e => e.ProjectId == o.ProjectId);
                            var adminAdmiss = evaluations.SingleOrDefault(e => e.EvalTableType == ProcedureEvalTableType.AdminAdmiss);
                            var techFinance = evaluations.SingleOrDefault(e => e.EvalTableType == ProcedureEvalTableType.TechFinance);
                            var complex = evaluations.SingleOrDefault(e => e.EvalTableType == ProcedureEvalTableType.Complex);

                            return new ProjectsReportItem
                            {
                                Programme = o.Programme,
                                Procedure = o.Procedure,
                                RegNumber = o.RegNumber,
                                Name = o.Name,
                                RegDate = o.RegDate,
                                RecieveDate = o.RecieveDate,
                                RecieveType = o.RecieveType,
                                CompanyUin = o.CompanyUin,
                                CompanyName = o.CompanyName,
                                CompanyType = o.CompanyType,
                                CompanyLegalType = o.CompanyLegalType,
                                CompanyAddress = o.CompanySeatCountryCode == "BG" ?
                                    string.Format("{0}, {1} {2}, {3}", o.CompanySeatCountry, o.CompanySeatSettlement, o.CompanySeatPostCode, o.CompanySeatStreet) :
                                    string.Format("{0}, {1}", o.CompanySeatCountry, o.CompanySeatAddress),
                                CompanyCorrespondenceAddress = o.CompanyCorrespondenceCountryCode == "BG" ?
                                    string.Format("{0}, {1} {2}, {3}", o.CompanyCorrespondenceCountry, o.CompanyCorrespondenceSettlement, o.CompanyCorrespondencePostCode, o.CompanyCorrespondenceStreet) :
                                    string.Format("{0}, {1}", o.CompanyCorrespondenceCountry, o.CompanyCorrespondenceAddress),
                                CompanyEmail = o.CompanyEmail,
                                ProjectDuration = o.Duration,
                                ProjectPlace = o.NutsAddressFullPathName,
                                ProjectKidCode = string.IsNullOrWhiteSpace(o.ProjectKidCode) ?
                                    null : string.Format("{0} {1}", o.ProjectKidCode, o.ProjectKidCodeName),
                                InitialTotalBfpAmount = o.InitialTotalBfpAmount / divisor,
                                InitialCoFinancingAmount = o.InitialCoFinancingAmount / divisor,
                                ActualTotalBfpAmount = o.ActualTotalBfpAmount / divisor,
                                IsAdminAdmissPassed = adminAdmiss == null ?
                                    (ProjectsReportItemEvalResult?)null :
                                    (adminAdmiss.EvalIsPassed ? ProjectsReportItemEvalResult.Passed : ProjectsReportItemEvalResult.NotPassed),
                                IsTechFinancePassed = techFinance == null ?
                                    (ProjectsReportItemEvalResult?)null :
                                    (techFinance.EvalIsPassed ? ProjectsReportItemEvalResult.Passed : ProjectsReportItemEvalResult.NotPassed),
                                IsComplexPassed = complex == null ?
                                    (ProjectsReportItemEvalResult?)null :
                                    (complex.EvalIsPassed ? ProjectsReportItemEvalResult.Passed : ProjectsReportItemEvalResult.NotPassed),
                                StandingStatus = this.GetProjectStandingStatus(o.StandingStatus, o.IsCanceled, o.RegistrationStatus == ProjectRegistrationStatus.Withdrawn),
                            };
                        }).ToList();
        }

        public IList<ContractsReportItem> GetContractsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            var divisor = 1m;

            if (currency == Currency.Euro)
            {
                divisor = euroExchangeRates;
            }

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var predicate = PredicateBuilder.True<Domain.Contracts.Contract>()
                .AndEquals(c => c.ProgrammeId, programmeId)
                .AndEquals(c => c.ProcedureId, procedureId)
                .AndDateTimeGreaterThanOrEqual(t => t.ContractDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.ContractDate, toDate)
                .And(c => c.ContractStatus == ContractStatus.Entered);

            var subqueryPredicate = PredicateBuilder.True<ProcedureShare>();

            if (programmePriorityId.HasValue)
            {
                subqueryPredicate = subqueryPredicate.And(ps => ps.ProgrammePriorityId == programmePriorityId);
            }

            var subquery = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(subqueryPredicate)
                            select ps.ProcedureId).Distinct();

            IQueryable<Domain.Contracts.Contract> contracts =
                from c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(predicate)
                join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals cv.ContractId
                join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                where cv.Status == ContractVersionStatus.Active && subquery.Contains(p.ProcedureId)
                select c;

            var paidAmountPredicate = PredicateBuilder.True<ActuallyPaidAmount>();
            paidAmountPredicate = paidAmountPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var actuallyPaidAmounts = (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(paidAmountPredicate)
                                       join c in contracts on pa.ContractId equals c.ContractId

                                       join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on pa.ContractReportPaymentId equals crp.ContractReportPaymentId into g0
                                       from crp in g0.DefaultIfEmpty()

                                       where pa.Status == ActuallyPaidAmountStatus.Entered
                                       group pa by new
                                       {
                                           pa.ContractId,
                                           PaymentType = crp != null ?
                                                (advancePayments.Contains(crp.PaymentType.Value) ? ContractReportPaymentType.Advance : crp.PaymentType.Value) :
                                                ContractReportPaymentType.Intermediate,
                                       }
                                        into g
                                       select new
                                       {
                                           ContractId = g.Key.ContractId,
                                           PaymentType = g.Key.PaymentType,
                                           PaidBfpEuAmount = g.Sum(pa => pa.PaidBfpEuAmount),
                                           PaidBfpBgAmount = g.Sum(pa => pa.PaidBfpBgAmount),
                                           PaidBfpTotalAmount = g.Sum(pa => pa.PaidBfpTotalAmount),
                                           PaidSelfAmount = g.Sum(pa => pa.PaidSelfAmount),
                                           PaidTotalAmount = g.Sum(pa => pa.PaidTotalAmount),
                                       }).ToList();

            var reimbursedAmountPredicate = PredicateBuilder.True<ReimbursedAmount>();
            reimbursedAmountPredicate = reimbursedAmountPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var reimbursedAmounts =
               (from ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>().Where(reimbursedAmountPredicate)
                join c in contracts on ra.ContractId equals c.ContractId
                where ra.Status == ReimbursedAmountStatus.Entered
                group new
                {
                    ReimbursedPrincipalEuAmount = ra.PrincipalBfp.EuAmount,
                    ReimbursedPrincipalBgAmount = ra.PrincipalBfp.BgAmount,
                    ReimbursedPrincipalTotalAmount = ra.PrincipalBfp.TotalAmount,
                    ReimbursedInterestEuAmount = ra.InterestBfp.EuAmount,
                    ReimbursedInterestBgAmount = ra.InterestBfp.BgAmount,
                    ReimbursedInterestTotalAmount = ra.InterestBfp.TotalAmount,
                }
                by ra.ContractId into g
                select new
                {
                    ContractId = g.Key,
                    ReimbursedPrincipalEuAmount = g.Sum(t => t.ReimbursedPrincipalEuAmount),
                    ReimbursedPrincipalBgAmount = g.Sum(t => t.ReimbursedPrincipalBgAmount),
                    ReimbursedPrincipalTotalAmount = g.Sum(t => t.ReimbursedPrincipalTotalAmount),
                    ReimbursedInterestEuAmount = g.Sum(t => t.ReimbursedInterestEuAmount),
                    ReimbursedInterestBgAmount = g.Sum(t => t.ReimbursedInterestBgAmount),
                    ReimbursedInterestTotalAmount = g.Sum(t => t.ReimbursedInterestTotalAmount),
                }).ToList();

            string fullPath = this.GetNutsFullPath(
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);

            var amountPredicate = PredicateBuilder.True<ContractAmountDO>();
            amountPredicate = amountPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                contracts = from c in contracts
                            join cl in this.unitOfWork.DbContext.Set<ContractLocation>() on c.ContractId equals cl.ContractId
                            where cl.FullPath.Contains(fullPath)
                            select c;
            }

            var finalStatuses = new List<ContractExecutionStatus>()
            {
                ContractExecutionStatus.Canceled,
                ContractExecutionStatus.Ended,
            };

            Func<IQueryable<IGrouping<ContractsReportContractAmountsGroupingItem, ContractAmountDO>>, List<ContractsReportContractAmountsItem>> amountItemsMaker = (ca) =>
            {
                return ca
                .Select(t => new ContractsReportContractAmountsItem
                {
                    ContractId = t.Key.ContractId,
                    ContractBudgetLevel3AmountNutsFullPath = t.Key.ContractBudgetLevel3AmountNutsFullPath,
                    ContractBudgetLevel3AmountNutsFullPathName = t.Key.ContractBudgetLevel3AmountNutsFullPathName,

                    ContractExecutionStatus = t.Min(p => p.ContractExecutionStatus),

                    InitialContractedEuAmount = t.Sum(p => p.InitialContractedEuAmount),
                    InitialContractedBgAmount = t.Sum(p => p.InitialContractedBgAmount),
                    InitialContractedBfpTotalAmount = t.Sum(p => p.InitialContractedBfpTotalAmount),
                    InitialContractedSelfAmount = t.Sum(p => p.InitialContractedSelfAmount),
                    InitialContractedTotalAmount = t.Sum(p => p.InitialContractedTotalAmount),

                    ActualContractedEuAmount = t.Sum(p => p.ContractedEuAmount),
                    ActualContractedBgAmount = t.Sum(p => p.ContractedBgAmount),
                    ActualContractedBfpTotalAmount = t.Sum(p => p.ContractedBfpTotalAmount),
                    ActualContractedSelfAmount = t.Sum(p => p.ContractedSelfAmount),
                    ActualContractedTotalAmount = t.Sum(p => p.ContractedTotalAmount),

                    ReportedEuAmount = t.Sum(p => p.ReportedEuAmount),
                    ReportedBgAmount = t.Sum(p => p.ReportedBgAmount),
                    ReportedBfpTotalAmount = t.Sum(p => p.ReportedBfpTotalAmount),
                    ReportedSelfAmount = t.Sum(p => p.ReportedSelfAmount),
                    ReportedTotalAmount = t.Sum(p => p.ReportedTotalAmount),

                    ApprovedEuAmount = t.Sum(p => p.ApprovedEuAmount),
                    ApprovedBgAmount = t.Sum(p => p.ApprovedBgAmount),
                    ApprovedBfpTotalAmount = t.Sum(p => p.ApprovedBfpTotalAmount),
                    ApprovedSelfAmount = t.Sum(p => p.ApprovedSelfAmount),
                    ApprovedTotalAmount = t.Sum(p => p.ApprovedTotalAmount),

                    CertifiedEuAmount = t.Sum(p => p.CertifiedEuAmount),
                    CertifiedBgAmount = t.Sum(p => p.CertifiedBgAmount),
                    CertifiedBfpTotalAmount = t.Sum(p => p.CertifiedBfpTotalAmount),
                    CertifiedSelfAmount = t.Sum(p => p.CertifiedSelfAmount),
                    CertifiedTotalAmount = t.Sum(p => p.CertifiedTotalAmount),

                    CorrectedTotalAmount = t.Sum(p => p.CorrectedTotalAmount),
                    CorrectedBfpTotalAmount = t.Sum(p => p.CorrectedBfpTotalAmount),
                    CorrectedEuAmount = t.Sum(p => p.CorrectedEuAmount),
                    CorrectedBgAmount = t.Sum(p => p.CorrectedBgAmount),
                    CorrectedSelfAmount = t.Sum(p => p.CorrectedSelfAmount),

                    UnapprovedEuAmount = t.Sum(p => p.UnapprovedEuAmount),
                    UnapprovedBgAmount = t.Sum(p => p.UnapprovedBgAmount),
                    UnapprovedBfpTotalAmount = t.Sum(p => p.UnapprovedBfpTotalAmount),
                    UnapprovedSelfAmount = t.Sum(p => p.UnapprovedSelfAmount),
                    UnapprovedTotalAmount = t.Sum(p => p.UnapprovedTotalAmount),
                }).ToList();
            };

            var coveringAdvancePaymentAmountResults = amountItemsMaker(this.GetFinancialCSDBudgetItems()
                .Concat(this.GetFinancialCorrectionCSDs())
                .Concat(this.GetFinancialCertCorrectionCSDs())
                .Concat(this.GetFinancialRevalidationCSDs())
                .Where(t => t.AdvancePayment.HasValue && t.AdvancePayment.Value == YesNoNonApplicable.Yes)
                .Where(t => t.ContractId.HasValue)
                .Where(t => contracts.Select(p => p.ContractId).Contains(t.ContractId.Value))
                .Where(amountPredicate)
                .GroupBy(t => new ContractsReportContractAmountsGroupingItem
                {
                    ContractId = t.ContractId.Value,
                    ContractBudgetLevel3AmountNutsFullPath = null,
                    ContractBudgetLevel3AmountNutsFullPathName = "АП/Корекции на други нива",
                }))
                .ToDictionary(t => new
                {
                    t.ContractId,
                    t.ContractBudgetLevel3AmountNutsFullPath,
                    t.ContractBudgetLevel3AmountNutsFullPathName,
                });

            var contractAmountPredicate = amountPredicate
                .And(t => t.ContractId.HasValue)
                .And(t => contracts.Select(p => p.ContractId).Contains(t.ContractId.Value));

            var contractAmountList = this.GetActualContractContractedAmounts().Where(contractAmountPredicate).ToList();
            contractAmountList.AddRange(this.GetInitialContractContractedAmounts().Where(contractAmountPredicate).ToList());
            contractAmountList.AddRange(this.GetAdvancePaymentAmounts().Where(contractAmountPredicate).ToList());
            contractAmountList.AddRange(this.GetFinancialCSDBudgetItems().Where(contractAmountPredicate).ToList());
            contractAmountList.AddRange(this.GetCorrections().Where(contractAmountPredicate).ToList());
            contractAmountList.AddRange(this.GetFinancialCorrectionCSDs().Where(contractAmountPredicate).ToList());
            contractAmountList.AddRange(this.GetCertCorrections().Where(contractAmountPredicate).ToList());
            contractAmountList.AddRange(this.GetFinancialCertCorrectionCSDs().Where(contractAmountPredicate).ToList());
            contractAmountList.AddRange(this.GetRevalidations().Where(contractAmountPredicate).ToList());
            contractAmountList.AddRange(this.GetFinancialRevalidationCSDs().Where(contractAmountPredicate).ToList());

            var contractAmountGrouped = contractAmountList.GroupBy(t => new ContractsReportContractAmountsGroupingItem
            {
                ContractId = t.ContractId.Value,
                ContractBudgetLevel3AmountNutsFullPath = t.ContractBudgetLevel3AmountNutsFullPath ?? null,
                ContractBudgetLevel3AmountNutsFullPathName = t.ContractBudgetLevel3AmountNutsFullPathName ?? "АП/Корекции на други нива",
            });

            var contractAmountResults = amountItemsMaker(contractAmountGrouped.AsQueryable());

            var contractAmounts = contractAmountResults.Select(t =>
                {
                    ContractsReportContractAmountsItem capa = null;
                    coveringAdvancePaymentAmountResults.TryGetValue(
                        new
                        {
                            t.ContractId,
                            t.ContractBudgetLevel3AmountNutsFullPath,
                            t.ContractBudgetLevel3AmountNutsFullPathName,
                        },
                        out capa);

                    var actualContractedEuAmount = t.ActualContractedEuAmount != null ? t.ActualContractedEuAmount / divisor : 0m;
                    var actualContractedBgAmount = t.ActualContractedBgAmount != null ? t.ActualContractedBgAmount / divisor : 0m;
                    var actualContractedBfpTotalAmount = t.ActualContractedBfpTotalAmount != null ? t.ActualContractedBfpTotalAmount / divisor : 0m;
                    var actualContractedSelfAmount = t.ActualContractedSelfAmount != null ? t.ActualContractedSelfAmount / divisor : 0m;
                    var actualContractedTotalAmount = t.ActualContractedTotalAmount != null ? t.ActualContractedTotalAmount / divisor : 0m;

                    var approvedEuAmount = t.ApprovedEuAmount != null ? t.ApprovedEuAmount / divisor : 0m - (capa != null ? (capa.ApprovedEuAmount != null ? capa.ApprovedEuAmount / divisor : 0m) : 0m);
                    var approvedBgAmount = t.ApprovedBgAmount != null ? t.ApprovedBgAmount / divisor : 0m - (capa != null ? (capa.ApprovedBgAmount != null ? capa.ApprovedBgAmount / divisor : 0m) : 0m);
                    var approvedBfpTotalAmount = t.ApprovedBfpTotalAmount != null ? t.ApprovedBfpTotalAmount / divisor : 0m - (capa != null ? (capa.ApprovedBfpTotalAmount != null ? capa.ApprovedBfpTotalAmount / divisor : 0m) : 0m);
                    var approvedSelfAmount = t.ApprovedSelfAmount != null ? t.ApprovedSelfAmount / divisor : 0m - (capa != null ? (capa.ApprovedSelfAmount != null ? capa.ApprovedSelfAmount / divisor : 0m) : 0m);
                    var approvedTotalAmount = t.ApprovedTotalAmount != null ? t.ApprovedTotalAmount / divisor : 0m - (capa != null ? (capa.ApprovedTotalAmount != null ? capa.ApprovedTotalAmount / divisor : 0m) : 0m);

                    return new ContractsReportContractAmountsItem
                    {
                        ContractId = t.ContractId,
                        ContractBudgetLevel3AmountNutsFullPath = t.ContractBudgetLevel3AmountNutsFullPath,
                        ContractBudgetLevel3AmountNutsFullPathName = t.ContractBudgetLevel3AmountNutsFullPathName,

                        InitialContractedEuAmount = t.InitialContractedEuAmount != null ? t.InitialContractedEuAmount / divisor : 0m,
                        InitialContractedBgAmount = t.InitialContractedBgAmount != null ? t.InitialContractedBgAmount / divisor : 0m,
                        InitialContractedBfpTotalAmount = t.InitialContractedBfpTotalAmount != null ? t.InitialContractedBfpTotalAmount / divisor : 0m,
                        InitialContractedSelfAmount = t.InitialContractedSelfAmount != null ? t.InitialContractedSelfAmount / divisor : 0m,
                        InitialContractedTotalAmount = t.InitialContractedTotalAmount != null ? t.InitialContractedTotalAmount / divisor : 0m,

                        ActualContractedEuAmount = t.ContractExecutionStatus != null && finalStatuses.Contains(t.ContractExecutionStatus.Value) ? approvedEuAmount : actualContractedEuAmount,
                        ActualContractedBgAmount = t.ContractExecutionStatus != null && finalStatuses.Contains(t.ContractExecutionStatus.Value) ? approvedBgAmount : actualContractedBgAmount,
                        ActualContractedBfpTotalAmount = t.ContractExecutionStatus != null && finalStatuses.Contains(t.ContractExecutionStatus.Value) ? approvedBfpTotalAmount : actualContractedBfpTotalAmount,
                        ActualContractedSelfAmount = t.ContractExecutionStatus != null && finalStatuses.Contains(t.ContractExecutionStatus.Value) ? approvedSelfAmount : actualContractedSelfAmount,
                        ActualContractedTotalAmount = t.ContractExecutionStatus != null && finalStatuses.Contains(t.ContractExecutionStatus.Value) ? approvedTotalAmount : actualContractedTotalAmount,

                        ReportedEuAmount = t.ReportedEuAmount != null ? t.ReportedEuAmount / divisor : 0m,
                        ReportedBgAmount = t.ReportedBgAmount != null ? t.ReportedBgAmount / divisor : 0m,
                        ReportedBfpTotalAmount = t.ReportedBfpTotalAmount != null ? t.ReportedBfpTotalAmount / divisor : 0m,
                        ReportedSelfAmount = t.ReportedSelfAmount != null ? t.ReportedSelfAmount / divisor : 0m,
                        ReportedTotalAmount = t.ReportedTotalAmount != null ? t.ReportedTotalAmount / divisor : 0m,

                        ApprovedEuAmount = approvedEuAmount,
                        ApprovedBgAmount = approvedBgAmount,
                        ApprovedBfpTotalAmount = approvedBfpTotalAmount,
                        ApprovedSelfAmount = approvedSelfAmount,
                        ApprovedTotalAmount = approvedTotalAmount,

                        CertifiedEuAmount = t.CertifiedEuAmount != null ? t.CertifiedEuAmount / divisor : 0m - (capa != null ? (capa.CertifiedEuAmount != null ? capa.CertifiedEuAmount / divisor : 0m) : 0m),
                        CertifiedBgAmount = t.CertifiedBgAmount != null ? t.CertifiedBgAmount / divisor : 0m - (capa != null ? (capa.CertifiedBgAmount != null ? capa.CertifiedBgAmount / divisor : 0m) : 0m),
                        CertifiedBfpTotalAmount = t.CertifiedBfpTotalAmount != null ? t.CertifiedBfpTotalAmount / divisor : 0m - (capa != null ? (capa.CertifiedBfpTotalAmount != null ? capa.CertifiedBfpTotalAmount / divisor : 0m) : 0m),
                        CertifiedSelfAmount = t.CertifiedSelfAmount != null ? t.CertifiedSelfAmount / divisor : 0m - (capa != null ? (capa.CertifiedSelfAmount != null ? capa.CertifiedSelfAmount / divisor : 0m) : 0m),
                        CertifiedTotalAmount = t.CertifiedTotalAmount != null ? t.CertifiedTotalAmount / divisor : 0m - (capa != null ? (capa.CertifiedTotalAmount != null ? capa.CertifiedTotalAmount / divisor : 0m) : 0m),

                        CorrectedTotalAmount = t.CorrectedTotalAmount != null ? t.CorrectedTotalAmount / divisor : 0m,
                        CorrectedBfpTotalAmount = t.CorrectedBfpTotalAmount != null ? t.CorrectedBfpTotalAmount / divisor : 0m,
                        CorrectedEuAmount = t.CorrectedEuAmount != null ? t.CorrectedEuAmount / divisor : 0m,
                        CorrectedBgAmount = t.CorrectedBgAmount != null ? t.CorrectedBgAmount / divisor : 0m,
                        CorrectedSelfAmount = t.CorrectedSelfAmount != null ? t.CorrectedSelfAmount : 0m,

                        UnapprovedEuAmount = t.UnapprovedEuAmount != null ? t.UnapprovedEuAmount / divisor : 0m,
                        UnapprovedBgAmount = t.UnapprovedBgAmount != null ? t.UnapprovedBgAmount / divisor : 0m,
                        UnapprovedBfpTotalAmount = t.UnapprovedBfpTotalAmount != null ? t.UnapprovedBfpTotalAmount / divisor : 0m,
                        UnapprovedSelfAmount = t.UnapprovedSelfAmount != null ? t.UnapprovedSelfAmount / divisor : 0m,
                        UnapprovedTotalAmount = t.UnapprovedTotalAmount != null ? t.UnapprovedTotalAmount / divisor : 0m,
                    };
                })
                .GroupBy(t => t.ContractId)
                .ToDictionary(t => t.Key, t => t.ToList());

            return (from contr in contracts
                    join icv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on contr.ContractId equals icv.ContractId
                    join acv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on contr.ContractId equals acv.ContractId
                    join prog in this.unitOfWork.DbContext.Set<Programme>() on contr.ProgrammeId equals prog.MapNodeId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on contr.ProcedureId equals proc.ProcedureId

                    join ct in this.unitOfWork.DbContext.Set<CompanyType>() on contr.CompanyTypeId equals ct.CompanyTypeId into g0
                    from ct in g0.DefaultIfEmpty()
                    join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on contr.CompanyLegalTypeId equals clt.CompanyLegalTypeId into g1
                    from clt in g1.DefaultIfEmpty()
                    join pkc in this.unitOfWork.DbContext.Set<KidCode>() on contr.ProjectKidCodeId equals pkc.KidCodeId into g3
                    from pkc in g3.DefaultIfEmpty()
                    join c1 in this.unitOfWork.DbContext.Set<Country>() on contr.BeneficiarySeatCountryId equals c1.CountryId into g4
                    from c1 in g4.DefaultIfEmpty()
                    join s1 in this.unitOfWork.DbContext.Set<Settlement>() on contr.BeneficiarySeatSettlementId equals s1.SettlementId into g5
                    from s1 in g5.DefaultIfEmpty()
                    join c2 in this.unitOfWork.DbContext.Set<Country>() on contr.BeneficiaryCorrespondenceCountryId equals c2.CountryId into g6
                    from c2 in g6.DefaultIfEmpty()
                    join s2 in this.unitOfWork.DbContext.Set<Settlement>() on contr.BeneficiaryCorrespondenceSettlementId equals s2.SettlementId into g7
                    from s2 in g7.DefaultIfEmpty()

                    where icv.OrderNum == 1 && acv.Status == ContractVersionStatus.Active

                    select new
                    {
                        contr.ContractId,
                        Programme = prog.Name,
                        Procedure = proc.Name,
                        contr.RegNumber,
                        contr.Name,
                        contr.CompanyUin,
                        contr.CompanyName,
                        CompanyType = ct.Name,
                        CompanyLegalType = clt.Name,
                        BeneficiarySeatCountryCode = c1.NutsCode,
                        BeneficiarySeatCountry = c1.Name,
                        BeneficiarySeatSettlement = s1.Name,
                        contr.BeneficiarySeatPostCode,
                        contr.BeneficiarySeatStreet,
                        contr.BeneficiarySeatAddress,
                        BeneficiaryCorrespondenceCountryCode = c2.NutsCode,
                        BeneficiaryCorrespondenceCountry = c2.Name,
                        BeneficiaryCorrespondenceSettlement = s2.Name,
                        contr.BeneficiaryCorrespondencePostCode,
                        contr.BeneficiaryCorrespondenceStreet,
                        contr.BeneficiaryCorrespondenceAddress,
                        contr.CompanyEmail,
                        contr.Duration,
                        ProjectKidCode = pkc.Code,
                        ProjectKidCodeName = pkc.Name,

                        InitialContractDate = icv.ContractDate,
                        ActualContractDate = acv.ContractDate,
                        InitialStartDate = icv.StartDate,
                        InitialCompletionDate = icv.CompletionDate,
                        ActualStartDate = acv.StartDate,
                        ActualCompletionDate = acv.CompletionDate,
                        ContractTerminationDate = acv.TerminationDate,
                        ContractExecutionStatus = contr.ExecutionStatus,
                    }).ToList()
                    .Select(o =>
                        {
                            var cpa = actuallyPaidAmounts.Where(pa => pa.ContractId == o.ContractId);
                            var acpa = cpa.Where(t => t.PaymentType == ContractReportPaymentType.Advance).SingleOrDefault();
                            var icpa = cpa.Where(t => t.PaymentType == ContractReportPaymentType.Intermediate).SingleOrDefault();
                            var fcpa = cpa.Where(t => t.PaymentType == ContractReportPaymentType.Final).SingleOrDefault();

                            var cra = reimbursedAmounts.SingleOrDefault(ra => ra.ContractId == o.ContractId);

                            IEnumerable<ContractsReportContractAmountsItem> ca = new List<ContractsReportContractAmountsItem>();
                            if (contractAmounts.ContainsKey(o.ContractId))
                            {
                                ca = contractAmounts[o.ContractId];
                            }

                            if (!string.IsNullOrWhiteSpace(fullPath))
                            {
                                ca = ca.Where(a => a.ContractId == o.ContractId && a.ContractBudgetLevel3AmountNutsFullPath != null && a.ContractBudgetLevel3AmountNutsFullPath.Contains(fullPath));
                            }

                            var amounts = ca.ToList();

                            return new ContractsReportItem
                            {
                                Programme = o.Programme,
                                Procedure = o.Procedure,
                                RegNumber = o.RegNumber,
                                Name = o.Name,
                                CompanyUin = o.CompanyUin,
                                CompanyName = o.CompanyName,
                                CompanyType = o.CompanyType,
                                CompanyLegalType = o.CompanyLegalType,
                                CompanyAddress = o.BeneficiarySeatCountryCode == "BG" ?
                                    string.Format("{0}, {1} {2}, {3}", o.BeneficiarySeatCountry, o.BeneficiarySeatSettlement, o.BeneficiarySeatPostCode, o.BeneficiarySeatStreet) :
                                    string.Format("{0}, {1}", o.BeneficiarySeatCountry, o.BeneficiarySeatAddress),
                                CompanyCorrespondenceAddress = o.BeneficiaryCorrespondenceCountryCode == "BG" ?
                                    string.Format("{0}, {1} {2}, {3}", o.BeneficiaryCorrespondenceCountry, o.BeneficiaryCorrespondenceSettlement, o.BeneficiaryCorrespondencePostCode, o.BeneficiaryCorrespondenceStreet) :
                                    string.Format("{0}, {1}", o.BeneficiaryCorrespondenceCountry, o.BeneficiaryCorrespondenceAddress),
                                CompanyEmail = o.CompanyEmail,
                                ProjectDuration = o.Duration,
                                ProjectKidCode = string.IsNullOrWhiteSpace(o.ProjectKidCode) ?
                                    null : string.Format("{0} {1}", o.ProjectKidCode, o.ProjectKidCodeName),
                                InitialContractDate = o.InitialContractDate,
                                ActualContractDate = o.ActualContractDate,
                                InitialStartDate = o.InitialStartDate,
                                InitialCompletionDate = o.InitialCompletionDate,
                                ActualStartDate = o.ActualStartDate,
                                ActualCompletionDate = o.ActualCompletionDate,
                                ContractTerminationDate = o.ContractTerminationDate,
                                ContractExecutionStatus = o.ContractExecutionStatus,

                                ContractAmounts = amounts,

                                PaidAdvanceEuAmount = acpa != null ? acpa.PaidBfpEuAmount / divisor : 0m,
                                PaidAdvanceBgAmount = acpa != null ? acpa.PaidBfpBgAmount / divisor : 0m,
                                PaidAdvanceTotalAmount = acpa != null ? acpa.PaidBfpTotalAmount / divisor : 0m,

                                PaidIntermediateEuAmount = icpa != null ? icpa.PaidBfpEuAmount / divisor : 0m,
                                PaidIntermediateBgAmount = icpa != null ? icpa.PaidBfpBgAmount / divisor : 0m,
                                PaidIntermediateTotalAmount = icpa != null ? icpa.PaidBfpTotalAmount / divisor : 0m,

                                PaidFinalEuAmount = fcpa != null ? fcpa.PaidBfpEuAmount / divisor : 0m,
                                PaidFinalBgAmount = fcpa != null ? fcpa.PaidBfpBgAmount / divisor : 0m,
                                PaidFinalTotalAmount = fcpa != null ? fcpa.PaidBfpTotalAmount / divisor : 0m,

                                ReimbursedPrincipalEuAmount = cra != null ? cra.ReimbursedPrincipalEuAmount / divisor : 0m,
                                ReimbursedPrincipalBgAmount = cra != null ? cra.ReimbursedPrincipalBgAmount / divisor : 0m,
                                ReimbursedPrincipalTotalAmount = cra != null ? cra.ReimbursedPrincipalTotalAmount / divisor : 0m,

                                ReimbursedInterestEuAmount = cra != null ? cra.ReimbursedInterestEuAmount / divisor : 0m,
                                ReimbursedInterestBgAmount = cra != null ? cra.ReimbursedInterestBgAmount / divisor : 0m,
                                ReimbursedInterestTotalAmount = cra != null ? cra.ReimbursedInterestTotalAmount / divisor : 0m,
                            };
                        }).ToList();
        }

        public IList<ContractsReportReportItem> GetContractReportsReport(
            int? programmeId,
            int? programmePriorityId,
            int? procedureId,
            int? contractId,
            DateTime? toDate,
            ContractReportType? reportType,
            ContractReportStatus? reportStatus)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.Date.AddDays(1).AddMilliseconds(-1);
            }

            var contractPredicate = PredicateBuilder.True<Domain.Contracts.Contract>()
                .AndEquals(c => c.ProgrammeId, programmeId)
                .AndEquals(c => c.ProcedureId, procedureId)
                .AndEquals(c => c.ContractId, contractId);

            var contractReportPredicate = PredicateBuilder.True<ContractReport>()
                .AndDateTimeLessThanOrEqual(cr => cr.SubmitDate, toDate)
                .AndEquals(cr => cr.ReportType, reportType)
                .AndEquals(cr => cr.Status, reportStatus)
                .And(cr => cr.Status != ContractReportStatus.Draft)
                .And(r => r.ReportType == ContractReportType.AdvancePayment || r.ReportType == ContractReportType.PaymentTechnicalFinancial || r.ReportType == ContractReportType.PaymentFinancial);

            var subqueryPredicate = PredicateBuilder.True<ProcedureShare>();

            if (!procedureId.HasValue)
            {
                subqueryPredicate = subqueryPredicate.AndEquals(ps => ps.ProgrammePriorityId, programmePriorityId);
            }

            var subquery = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(subqueryPredicate)
                            select ps.ProcedureId).Distinct();

            var paidAmounts =
                (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                 join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(contractPredicate) on pa.ContractId equals c.ContractId
                 join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on pa.ContractReportPaymentId equals crp.ContractReportPaymentId
                 where pa.Status == ActuallyPaidAmountStatus.Entered
                 group pa by crp.ContractReportId into g
                 select new
                 {
                     ContractReportId = g.Key,
                     PaidBfpEuAmount = g.Sum(pa => pa.PaidBfpEuAmount ?? 0),
                     PaidBfpBgAmount = g.Sum(pa => pa.PaidBfpBgAmount ?? 0),
                     PaidSelfAmount = g.Sum(pa => pa.PaidSelfAmount ?? 0),
                 }).ToList();

            var certReportNums = this.GetContractReportCertReportNums(contractPredicate);

            var allowedCertReports = PredicateBuilder.True<CertReport>()
                .And(p => allowedCertReportStatuses.Contains(p.Status));

            var allowedAnnualAccountReports = PredicateBuilder.True<AnnualAccountReport>()
                .And(p => p.Status == AnnualAccountReportStatus.Ended);

            var financialCSDBudgetItems =
                from fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on fbi.ContractReportId equals cr.ContractReportId

                join crep in this.unitOfWork.DbContext.Set<CertReport>().Where(allowedCertReports) on fbi.CertReportId equals crep.CertReportId into g1
                from crep in g1.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted

                select new
                {
                    fbi.ContractReportId,
                    fbi.AdvancePayment,

                    fbi.ApprovedEuAmount,
                    fbi.ApprovedBgAmount,
                    fbi.ApprovedBfpTotalAmount,
                    fbi.ApprovedSelfAmount,
                    fbi.ApprovedTotalAmount,

                    CertifiedEuAmount = crep != null ? fbi.CertifiedApprovedEuAmount : 0,
                    CertifiedBgAmount = crep != null ? fbi.CertifiedApprovedBgAmount : 0,
                    CertifiedBfpTotalAmount = crep != null ? fbi.CertifiedApprovedBfpTotalAmount : 0,
                    CertifiedSelfAmount = crep != null ? fbi.CertifiedApprovedSelfAmount : 0,
                    CertifiedTotalAmount = crep != null ? fbi.CertifiedApprovedTotalAmount : 0,
                };

            var financialCorrectionCSDs =
                from fc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on fc.ContractReportId equals cr.ContractReportId

                join crep in this.unitOfWork.DbContext.Set<CertReport>().Where(allowedCertReports) on fc.CertReportId equals crep.CertReportId into g1
                from crep in g1.DefaultIfEmpty()

                join arfc in this.unitOfWork.DbContext.Set<AnnualAccountReportFinancialCorrectionCSD>() on fc.ContractReportFinancialCorrectionCSDId equals arfc.ContractReportFinancialCorrectionCSDId into g2
                from arfc in g2.DefaultIfEmpty()

                join ar in this.unitOfWork.DbContext.Set<AnnualAccountReport>().Where(allowedAnnualAccountReports) on arfc.AnnualAccountReportId equals ar.AnnualAccountReportId into g3
                from ar in g3.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted && fc.Status == ContractReportFinancialCorrectionCSDStatus.Ended

                select new
                {
                    fc.ContractReportId,
                    fbi.AdvancePayment,

                    ApprovedEuAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedEuAmount,
                    ApprovedBgAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedBgAmount,
                    ApprovedBfpTotalAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedBfpTotalAmount,
                    ApprovedSelfAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedSelfAmount,
                    ApprovedTotalAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedTotalAmount,

                    CertifiedEuAmount = (crep != null ? -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedEuAmount : 0) + (ar != null ? -1 * (int)fc.Sign * fc.CorrectedApprovedEuAmount : 0),
                    CertifiedBgAmount = (crep != null ? -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedBgAmount : 0) + (ar != null ? -1 * (int)fc.Sign * fc.CorrectedApprovedBgAmount : 0),
                    CertifiedBfpTotalAmount = (crep != null ? -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedBfpTotalAmount : 0) + (ar != null ? -1 * (int)fc.Sign * fc.CorrectedApprovedBfpTotalAmount : 0),
                    CertifiedSelfAmount = (crep != null ? -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedSelfAmount : 0) + (ar != null ? -1 * (int)fc.Sign * fc.CorrectedApprovedSelfAmount : 0),
                    CertifiedTotalAmount = (crep != null ? -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedTotalAmount : 0) + (ar != null ? -1 * (int)fc.Sign * fc.CorrectedApprovedTotalAmount : 0),
                };

            var corrections =
                from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crc.ContractReportPaymentId equals crp.ContractReportPaymentId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId

                join crep in this.unitOfWork.DbContext.Set<CertReport>().Where(allowedCertReports) on crc.CertReportId equals crep.CertReportId into g1
                from crep in g1.DefaultIfEmpty()

                join arcrc in this.unitOfWork.DbContext.Set<AnnualAccountReportContractReportCorrection>() on crc.ContractReportCorrectionId equals arcrc.ContractReportCorrectionId into g2
                from arcrc in g2.DefaultIfEmpty()

                join ar in this.unitOfWork.DbContext.Set<AnnualAccountReport>().Where(allowedAnnualAccountReports) on arcrc.AnnualAccountReportId equals ar.AnnualAccountReportId into g3
                from ar in g3.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted && crc.Status == ContractReportCorrectionStatus.Entered && crp.Status == ContractReportPaymentStatus.Actual

                select new
                {
                    cr.ContractReportId,
                    AdvancePayment = YesNoNonApplicable.NotApplicable,

                    ApprovedEuAmount = (int)crc.Sign * crc.CorrectedApprovedEuAmount,
                    ApprovedBgAmount = (int)crc.Sign * crc.CorrectedApprovedBgAmount,
                    ApprovedBfpTotalAmount = (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount,
                    ApprovedSelfAmount = (int)crc.Sign * crc.CorrectedApprovedSelfAmount,
                    ApprovedTotalAmount = (int)crc.Sign * crc.CorrectedApprovedTotalAmount,

                    CertifiedEuAmount = (crep != null ? (int)crc.Sign * crc.CertifiedCorrectedApprovedEuAmount : 0) + (ar != null ? (int)crc.Sign * crc.CorrectedApprovedEuAmount : 0),
                    CertifiedBgAmount = (crep != null ? (int)crc.Sign * crc.CertifiedCorrectedApprovedBgAmount : 0) + (ar != null ? (int)crc.Sign * crc.CorrectedApprovedBgAmount : 0),
                    CertifiedBfpTotalAmount = (crep != null ? (int)crc.Sign * crc.CertifiedCorrectedApprovedBfpTotalAmount : 0) + (ar != null ? (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount : 0),
                    CertifiedSelfAmount = (crep != null ? (int)crc.Sign * crc.CertifiedCorrectedApprovedSelfAmount : 0) + (ar != null ? (int)crc.Sign * crc.CorrectedApprovedSelfAmount : 0),
                    CertifiedTotalAmount = (crep != null ? (int)crc.Sign * crc.CertifiedCorrectedApprovedTotalAmount : 0) + (ar != null ? (int)crc.Sign * crc.CorrectedApprovedTotalAmount : 0),
                };

            var financialRevalidationCSDs =
                from fr in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fr.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on fr.ContractReportId equals cr.ContractReportId

                join crep in this.unitOfWork.DbContext.Set<CertReport>().Where(allowedCertReports) on fr.CertReportId equals crep.CertReportId into g1
                from crep in g1.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted && fr.Status == ContractReportFinancialRevalidationCSDStatus.Ended

                select new
                {
                    cr.ContractReportId,
                    fbi.AdvancePayment,

                    ApprovedEuAmount = (int)fr.Sign * fr.RevalidatedEuAmount,
                    ApprovedBgAmount = (int)fr.Sign * fr.RevalidatedBgAmount,
                    ApprovedBfpTotalAmount = (int)fr.Sign * fr.RevalidatedBfpTotalAmount,
                    ApprovedSelfAmount = (int)fr.Sign * fr.RevalidatedSelfAmount,
                    ApprovedTotalAmount = (int)fr.Sign * fr.RevalidatedTotalAmount,

                    CertifiedEuAmount = crep != null ? (int)fr.Sign * fr.CertifiedRevalidatedEuAmount : 0,
                    CertifiedBgAmount = crep != null ? (int)fr.Sign * fr.CertifiedRevalidatedBgAmount : 0,
                    CertifiedBfpTotalAmount = crep != null ? (int)fr.Sign * fr.CertifiedRevalidatedBfpTotalAmount : 0,
                    CertifiedSelfAmount = crep != null ? (int)fr.Sign * fr.CertifiedRevalidatedSelfAmount : 0,
                    CertifiedTotalAmount = crep != null ? (int)fr.Sign * fr.CertifiedRevalidatedTotalAmount : 0,
                };

            var revalidations =
                from r in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
                join p in this.unitOfWork.DbContext.Set<ContractReportPayment>() on r.ContractReportPaymentId equals p.ContractReportPaymentId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on p.ContractReportId equals cr.ContractReportId

                join crep in this.unitOfWork.DbContext.Set<CertReport>().Where(allowedCertReports) on r.CertReportId equals crep.CertReportId into g1
                from crep in g1.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted && r.Status == ContractReportRevalidationStatus.Entered && p.Status == ContractReportPaymentStatus.Actual

                select new
                {
                    cr.ContractReportId,
                    AdvancePayment = YesNoNonApplicable.NotApplicable,

                    ApprovedEuAmount = (int)r.Sign * r.RevalidatedEuAmount,
                    ApprovedBgAmount = (int)r.Sign * r.RevalidatedBgAmount,
                    ApprovedBfpTotalAmount = (int)r.Sign * r.RevalidatedBfpTotalAmount,
                    ApprovedSelfAmount = (int)r.Sign * r.RevalidatedSelfAmount,
                    ApprovedTotalAmount = (int)r.Sign * r.RevalidatedTotalAmount,

                    CertifiedEuAmount = crep != null ? (int)r.Sign * r.CertifiedRevalidatedEuAmount : 0,
                    CertifiedBgAmount = crep != null ? (int)r.Sign * r.CertifiedRevalidatedBgAmount : 0,
                    CertifiedBfpTotalAmount = crep != null ? (int)r.Sign * r.CertifiedRevalidatedBfpTotalAmount : 0,
                    CertifiedSelfAmount = crep != null ? (int)r.Sign * r.CertifiedRevalidatedSelfAmount : 0,
                    CertifiedTotalAmount = crep != null ? (int)r.Sign * r.CertifiedRevalidatedTotalAmount : 0,
                };

            var financialCertCorrectionCSDs =
                from fcc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fcc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on fcc.ContractReportId equals cr.ContractReportId

                join crep in this.unitOfWork.DbContext.Set<CertReport>().Where(allowedCertReports) on fcc.CertReportId equals crep.CertReportId into g1
                from crep in g1.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted && fcc.Status == ContractReportFinancialCertCorrectionCSDStatus.Ended

                select new
                {
                    cr.ContractReportId,
                    fbi.AdvancePayment,

                    ApprovedEuAmount = (decimal?)null,
                    ApprovedBgAmount = (decimal?)null,
                    ApprovedBfpTotalAmount = (decimal?)null,
                    ApprovedSelfAmount = (decimal?)null,
                    ApprovedTotalAmount = (decimal?)null,

                    CertifiedEuAmount = crep != null ? (int)fcc.Sign * fcc.CertifiedEuAmount : 0,
                    CertifiedBgAmount = crep != null ? (int)fcc.Sign * fcc.CertifiedBgAmount : 0,
                    CertifiedBfpTotalAmount = crep != null ? (int)fcc.Sign * fcc.CertifiedBfpTotalAmount : 0,
                    CertifiedSelfAmount = crep != null ? (int)fcc.Sign * fcc.CertifiedSelfAmount : 0,
                    CertifiedTotalAmount = crep != null ? (int)fcc.Sign * fcc.CertifiedTotalAmount : 0,
                };

            var certCorrections =
                from cc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()
                join p in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cc.ContractReportPaymentId equals p.ContractReportPaymentId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on p.ContractReportId equals cr.ContractReportId

                join crep in this.unitOfWork.DbContext.Set<CertReport>().Where(allowedCertReports) on cc.CertReportId equals crep.CertReportId into g1
                from crep in g1.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted && cc.Status == ContractReportCertCorrectionStatus.Entered && p.Status == ContractReportPaymentStatus.Actual

                select new
                {
                    cr.ContractReportId,
                    AdvancePayment = YesNoNonApplicable.NotApplicable,

                    ApprovedEuAmount = (decimal?)null,
                    ApprovedBgAmount = (decimal?)null,
                    ApprovedBfpTotalAmount = (decimal?)null,
                    ApprovedSelfAmount = (decimal?)null,
                    ApprovedTotalAmount = (decimal?)null,

                    CertifiedEuAmount = crep != null ? (int)cc.Sign * cc.CertifiedEuAmount : 0,
                    CertifiedBgAmount = crep != null ? (int)cc.Sign * cc.CertifiedBgAmount : 0,
                    CertifiedBfpTotalAmount = crep != null ? (int)cc.Sign * cc.CertifiedBfpTotalAmount : 0,
                    CertifiedSelfAmount = crep != null ? (int)cc.Sign * cc.CertifiedSelfAmount : 0,
                    CertifiedTotalAmount = crep != null ? (int)cc.Sign * cc.CertifiedTotalAmount : 0,
                };

            var certAuthorityFinancialCorrectionCSDs =
                from cac in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>()
                join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on cac.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on cac.ContractReportId equals cr.ContractReportId

                join arcac in this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>() on cac.ContractReportCertAuthorityFinancialCorrectionCSDId equals arcac.ContractReportCertAuthorityFinancialCorrectionCSDId into g1
                from arcac in g1.DefaultIfEmpty()

                join ar in this.unitOfWork.DbContext.Set<AnnualAccountReport>().Where(allowedAnnualAccountReports) on arcac.AnnualAccountReportId equals ar.AnnualAccountReportId into g2
                from ar in g2.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted && cac.Status == ContractReportCertAuthorityFinancialCorrectionCSDStatus.Ended

                select new
                {
                    cr.ContractReportId,
                    fbi.AdvancePayment,

                    ApprovedEuAmount = (decimal?)null,
                    ApprovedBgAmount = (decimal?)null,
                    ApprovedBfpTotalAmount = (decimal?)null,
                    ApprovedSelfAmount = (decimal?)null,
                    ApprovedTotalAmount = (decimal?)null,

                    CertifiedEuAmount = ar != null ? -1 * (int)cac.Sign * cac.CertifiedEuAmount : 0,
                    CertifiedBgAmount = ar != null ? -1 * (int)cac.Sign * cac.CertifiedBgAmount : 0,
                    CertifiedBfpTotalAmount = ar != null ? -1 * (int)cac.Sign * cac.CertifiedBfpTotalAmount : 0,
                    CertifiedSelfAmount = ar != null ? -1 * (int)cac.Sign * cac.CertifiedSelfAmount : 0,
                    CertifiedTotalAmount = ar != null ? -1 * (int)cac.Sign * cac.CertifiedTotalAmount : 0,
                };

            var certAuthorityCorrections =
                from cac in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>()
                join p in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cac.ContractReportPaymentId equals p.ContractReportPaymentId
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on p.ContractReportId equals cr.ContractReportId

                join arcc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertCorrection>() on cac.ContractReportCertAuthorityCorrectionId equals arcc.ContractReportCertAuthorityCorrectionId into g1
                from arcc in g1.DefaultIfEmpty()

                join ar in this.unitOfWork.DbContext.Set<AnnualAccountReport>().Where(allowedAnnualAccountReports) on arcc.AnnualAccountReportId equals ar.AnnualAccountReportId into g2
                from ar in g2.DefaultIfEmpty()

                where cr.Status == ContractReportStatus.Accepted && cac.Status == ContractReportCertAuthorityCorrectionStatus.Entered && p.Status == ContractReportPaymentStatus.Actual

                select new
                {
                    cr.ContractReportId,
                    AdvancePayment = YesNoNonApplicable.NotApplicable,

                    ApprovedEuAmount = (decimal?)null,
                    ApprovedBgAmount = (decimal?)null,
                    ApprovedBfpTotalAmount = (decimal?)null,
                    ApprovedSelfAmount = (decimal?)null,
                    ApprovedTotalAmount = (decimal?)null,

                    CertifiedEuAmount = ar != null ? -1 * (int)cac.Sign * cac.CertifiedEuAmount : 0,
                    CertifiedBgAmount = ar != null ? -1 * (int)cac.Sign * cac.CertifiedBgAmount : 0,
                    CertifiedBfpTotalAmount = ar != null ? -1 * (int)cac.Sign * cac.CertifiedBfpTotalAmount : 0,
                    CertifiedSelfAmount = ar != null ? -1 * (int)cac.Sign * cac.CertifiedSelfAmount : 0,
                    CertifiedTotalAmount = ar != null ? -1 * (int)cac.Sign * cac.CertifiedTotalAmount : 0,
                };

            var allowedContractReportsPredicate = PredicateBuilder.False<ContractReport>()
                .Or(p => p.Status == ContractReportStatus.Unchecked)
                .Or(p => p.Status == ContractReportStatus.Accepted)
                .Or(p => p.Status == ContractReportStatus.Refused);

            var reportedAmounts =
                from fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                join cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(allowedContractReportsPredicate) on fbi.ContractReportId equals cr.ContractReportId

                group fbi by fbi.ContractReportId into g

                select new
                {
                    ContractReportId = g.Key,

                    ReportedEuAmount = g.Sum(p => p.EuAmount),
                    ReportedBgAmount = g.Sum(p => p.BgAmount),
                    ReportedBfpTotalAmount = g.Sum(p => p.BfpTotalAmount),
                    ReportedSelfAmount = g.Sum(p => p.SelfAmount),
                };

            var contractReportAmounts = financialCSDBudgetItems
                .Concat(financialCorrectionCSDs)
                .Concat(corrections)
                .Concat(financialRevalidationCSDs)
                .Concat(revalidations)
                .Concat(financialCertCorrectionCSDs)
                .Concat(certCorrections)
                .Concat(certAuthorityFinancialCorrectionCSDs)
                .Concat(certAuthorityCorrections)
                .GroupBy(t => t.ContractReportId)
                .Select(t => new
                {
                    ContractReportId = t.Key,

                    ApprovedEuAmount = t.Sum(p => p.AdvancePayment != YesNoNonApplicable.Yes ? p.ApprovedEuAmount : 0),
                    ApprovedBgAmount = t.Sum(p => p.AdvancePayment != YesNoNonApplicable.Yes ? p.ApprovedBgAmount : 0),
                    ApprovedBfpTotalAmount = t.Sum(p => p.AdvancePayment != YesNoNonApplicable.Yes ? p.ApprovedBfpTotalAmount : 0),
                    ApprovedSelfAmount = t.Sum(p => p.AdvancePayment != YesNoNonApplicable.Yes ? p.ApprovedSelfAmount : 0),
                    ApprovedTotalAmount = t.Sum(p => p.AdvancePayment != YesNoNonApplicable.Yes ? p.ApprovedTotalAmount : 0),

                    CertifiedEuAmount = t.Sum(p => p.AdvancePayment != YesNoNonApplicable.Yes ? p.CertifiedEuAmount : 0),
                    CertifiedBgAmount = t.Sum(p => p.AdvancePayment != YesNoNonApplicable.Yes ? p.CertifiedBgAmount : 0),
                    CertifiedBfpTotalAmount = t.Sum(p => p.AdvancePayment != YesNoNonApplicable.Yes ? p.CertifiedBfpTotalAmount : 0),
                    CertifiedSelfAmount = t.Sum(p => p.AdvancePayment != YesNoNonApplicable.Yes ? p.CertifiedSelfAmount : 0),
                    CertifiedTotalAmount = t.Sum(p => p.AdvancePayment != YesNoNonApplicable.Yes ? p.CertifiedTotalAmount : 0),

                    AdvanceVerificationApprovedEuAmount = t.Sum(p => p.AdvancePayment == YesNoNonApplicable.Yes ? p.ApprovedEuAmount : 0),
                    AdvanceVerificationApprovedBgAmount = t.Sum(p => p.AdvancePayment == YesNoNonApplicable.Yes ? p.ApprovedBgAmount : 0),
                    AdvanceVerificationApprovedBfpTotalAmount = t.Sum(p => p.AdvancePayment == YesNoNonApplicable.Yes ? p.ApprovedBfpTotalAmount : 0),
                    AdvanceVerificationApprovedSelfAmount = t.Sum(p => p.AdvancePayment == YesNoNonApplicable.Yes ? p.ApprovedSelfAmount : 0),
                    AdvanceVerificationApprovedTotalAmount = t.Sum(p => p.AdvancePayment == YesNoNonApplicable.Yes ? p.ApprovedTotalAmount : 0),

                    AdvanceVerificationCertifiedEuAmount = t.Sum(p => p.AdvancePayment == YesNoNonApplicable.Yes ? p.CertifiedEuAmount : 0),
                    AdvanceVerificationCertifiedBgAmount = t.Sum(p => p.AdvancePayment == YesNoNonApplicable.Yes ? p.CertifiedBgAmount : 0),
                    AdvanceVerificationCertifiedBfpTotalAmount = t.Sum(p => p.AdvancePayment == YesNoNonApplicable.Yes ? p.CertifiedBfpTotalAmount : 0),
                    AdvanceVerificationCertifiedSelfAmount = t.Sum(p => p.AdvancePayment == YesNoNonApplicable.Yes ? p.CertifiedSelfAmount : 0),
                    AdvanceVerificationCertifiedTotalAmount = t.Sum(p => p.AdvancePayment == YesNoNonApplicable.Yes ? p.CertifiedTotalAmount : 0),
                });

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(contractReportPredicate)
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>().Where(p => p.Status != ContractReportPaymentStatus.Returned) on cr.ContractReportId equals crp.ContractReportId
                    join contr in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(contractPredicate) on cr.ContractId equals contr.ContractId
                    join prog in this.unitOfWork.DbContext.Set<Programme>() on contr.ProgrammeId equals prog.MapNodeId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on contr.ProcedureId equals proc.ProcedureId

                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(p => p.IsPrimary) on contr.ProcedureId equals ps.ProcedureId into gps
                    from ps in gps.DefaultIfEmpty()

                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId into gpp
                    from pp in gpp.DefaultIfEmpty()

                    where subquery.Contains(proc.ProcedureId)

                    select new
                    {
                        contr.ContractId,
                        cr.ContractReportId,
                        Programme = prog.Name,
                        ProgrammePriority = pp.Name,
                        Procedure = proc.Name,
                        cr.OrderNum,
                        cr.Status,
                        cr.ReportType,
                        cr.SubmitDate,
                        ContractRegNumber = contr.RegNumber,
                        ContractName = contr.Name,
                        CompanyUin = contr.CompanyUin,
                        CompanyName = contr.CompanyName,
                        contr.ExecutionStatus,
                        crp.DateFrom,
                        crp.DateTo,
                        crp.RequestedAmount,
                    }).ToList()
                    .Select(o =>
                        {
                            var contractReportPaidAmounts = paidAmounts.SingleOrDefault(a => a.ContractReportId == o.ContractReportId);
                            var reported = reportedAmounts.SingleOrDefault(p => p.ContractReportId == o.ContractReportId);
                            var amounts = contractReportAmounts.SingleOrDefault(ra => ra.ContractReportId == o.ContractReportId);
                            var reportCertReportNums = certReportNums
                                .Where(crn => crn.ContractReportId == o.ContractReportId)
                                .Select(crn => crn.OrderNum);

                            return new ContractsReportReportItem(
                                programme: o.Programme,
                                programmePriority: o.ProgrammePriority,
                                procedure: o.Procedure,
                                orderNum: o.OrderNum,
                                contractRegNumber: o.ContractRegNumber,
                                contractName: o.ContractName,
                                contractExecutionStatus: o.ExecutionStatus,
                                companyUin: o.CompanyUin,
                                companyName: o.CompanyName,
                                requestedAmount: o.RequestedAmount,
                                reportedEuAmount: reported != null ? reported.ReportedEuAmount : 0m,
                                reportedBgAmount: reported != null ? reported.ReportedBgAmount : 0m,
                                reportedSelfAmount: reported != null ? reported.ReportedSelfAmount : 0m,
                                verifiedEuAmount: amounts != null ? amounts.ApprovedEuAmount : 0m,
                                verifiedBgAmount: amounts != null ? amounts.ApprovedBgAmount : 0m,
                                verifiedSelfAmount: amounts != null ? amounts.ApprovedSelfAmount : 0m,
                                verifiedEuAdvancePaymentAmount: amounts != null ? amounts.AdvanceVerificationApprovedEuAmount : 0m,
                                verifiedBgAdvancePaymentAmount: amounts != null ? amounts.AdvanceVerificationApprovedBgAmount : 0m,
                                verifiedSelfAdvancePaymentAmount: amounts != null ? amounts.AdvanceVerificationApprovedSelfAmount : 0m,
                                certEuAmount: amounts != null ? amounts.CertifiedEuAmount : 0m,
                                certBgAmount: amounts != null ? amounts.CertifiedBgAmount : 0m,
                                certSelfAmount: amounts != null ? amounts.CertifiedSelfAmount : 0m,
                                certEuAdvancePaymentAmount: amounts != null ? amounts.AdvanceVerificationCertifiedEuAmount : 0m,
                                certBgAdvancePaymentAmount: amounts != null ? amounts.AdvanceVerificationCertifiedBgAmount : 0m,
                                certSelfAdvancePaymentAmount: amounts != null ? amounts.AdvanceVerificationCertifiedSelfAmount : 0m,
                                paidEuAmount: contractReportPaidAmounts != null ? contractReportPaidAmounts.PaidBfpEuAmount : 0m,
                                paidBgAmount: contractReportPaidAmounts != null ? contractReportPaidAmounts.PaidBfpBgAmount : 0m,
                                paidSelfAmount: contractReportPaidAmounts != null ? contractReportPaidAmounts.PaidSelfAmount : 0m,
                                certReportNumber: string.Join(", ", reportCertReportNums),
                                status: o.Status,
                                reportType: o.ReportType,
                                submitDate: o.SubmitDate,
                                dateFrom: o.DateFrom,
                                dateTo: o.DateTo);
                        }).ToList();
        }

        public IList<IndicatorReportItemVO> GetIndicatorsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? toDate = null,
            ContractExecutionStatus? contractExecutionStatus = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var contractsPredicate = PredicateBuilder.True<Domain.Contracts.Contract>()
                .AndEquals(c => c.ProgrammeId, programmeId)
                .AndEquals(c => c.ProcedureId, procedureId)
                .AndEquals(c => c.ExecutionStatus, contractExecutionStatus);

            IQueryable<Domain.Contracts.Contract> contracts = this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(contractsPredicate);

            string fullPath = this.GetNutsFullPath(
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);

            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                contracts = from c in contracts
                            join cl in this.unitOfWork.DbContext.Set<ContractLocation>() on c.ContractId equals cl.ContractId
                            where cl.FullPath.Contains(fullPath)
                            select c;
            }

            var contractLocations = (from cl in this.unitOfWork.DbContext.Set<ContractLocation>()
                                     join c in contracts on cl.ContractId equals c.ContractId
                                     group cl.FullPathName by cl.ContractId into g
                                     select new
                                     {
                                         ContractId = g.Key,
                                         FullPathNames = g.AsEnumerable(),
                                     })
                                .ToList()
                                .ToDictionary(t => t.ContractId, t => string.Join(";", t.FullPathNames));

            var contractReportsPredicate = PredicateBuilder.True<ContractReport>()
                .AndDateTimeLessThanOrEqual(c => c.CheckedDate, toDate);

            // last accepted contract reports having a contract report technical
            var lastAcceptedContractReports =
                from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(contractReportsPredicate)
                join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>() on cr.ContractReportId equals crt.ContractReportId
                where cr.Status == ContractReportStatus.Accepted
                group new { cr.ContractReportId, cr.OrderNum } by cr.ContractId into g
                select g.OrderByDescending(t => t.OrderNum).FirstOrDefault().ContractReportId;

            var contractIndicatorsPredicate = PredicateBuilder.True<ContractIndicator>()
                .AndEquals(c => c.ProgrammePriorityId, programmePriorityId);

            var actualContractReportTechnicalCorrectionIndicators =
                from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                join tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>() on tci.ContractReportTechnicalCorrectionId equals tc.ContractReportTechnicalCorrectionId
                where tc.Status == ContractReportTechnicalCorrectionStatus.Ended
                select tci;

            var oldTimeout = this.unitOfWork.DbContext.Database.CommandTimeout;
            try
            {
                this.unitOfWork.DbContext.Database.CommandTimeout = 60 * 5;

                return (from ci in this.unitOfWork.DbContext.Set<ContractIndicator>().Where(contractIndicatorsPredicate)
                        join i in this.unitOfWork.DbContext.Set<Domain.Indicators.Indicator>() on ci.IndicatorId equals i.IndicatorId
                        join m in this.unitOfWork.DbContext.Set<Measure>() on i.MeasureId equals m.MeasureId
                        join c in contracts on ci.ContractId equals c.ContractId
                        join p in this.unitOfWork.DbContext.Set<Programme>() on c.ProgrammeId equals p.MapNodeId
                        join proc in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals proc.ProcedureId

                        join ct in this.unitOfWork.DbContext.Set<CompanyType>() on c.CompanyTypeId equals ct.CompanyTypeId into g0
                        from ct in g0.DefaultIfEmpty()
                        join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on c.CompanyLegalTypeId equals clt.CompanyLegalTypeId into g1
                        from clt in g1.DefaultIfEmpty()

                        join acr in this.unitOfWork.DbContext.Set<ContractReportIndicator>().Where(t => lastAcceptedContractReports.Contains(t.ContractReportId)) on ci.ContractIndicatorId equals acr.ContractIndicatorId into g3
                        from acr in g3.DefaultIfEmpty()

                        join tci in actualContractReportTechnicalCorrectionIndicators on acr.ContractReportIndicatorId equals tci.ContractReportIndicatorId into g4
                        from tci in g4.DefaultIfEmpty()

                        orderby c.RegNumber, i.Name

                        select new
                        {
                            ContractId = c.ContractId,
                            Programme = p.Name,
                            Procedure = proc.Name,
                            ContractRegNum = c.RegNumber,
                            ContractName = c.Name,
                            ContractExecutionStatus = c.ExecutionStatus,
                            ContractEndTerminationDate = c.CompletionDate ?? c.TerminationDate ?? null,
                            CompanyName = c.CompanyName,
                            CompanyUin = c.CompanyUin,
                            CompanyType = ct.Name,
                            CompanyLegalType = clt.Name,
                            Name = i.Name,
                            Measure = m.Name,
                            BaseTotalValue = ci.BaseTotalValue,
                            TargetTotalValue = ci.TargetTotalValue,
                            ReportedTotalValue = acr != null ? acr.CumulativeAmountTotal : 0m,
                            ApprovedTotalValue = acr != null ? (tci == null ? acr.ApprovedCumulativeAmountTotal : tci.CorrectedApprovedCumulativeAmountTotal) ?? 0m : 0m,
                        })
                        .ToList()
                        .Select(t => new IndicatorReportItemVO
                        {
                            Programme = t.Programme,
                            Procedure = t.Procedure,
                            ContractRegNum = t.ContractRegNum,
                            ContractName = t.ContractName,
                            NutsFullPathName = contractLocations.ContainsKey(t.ContractId) ? contractLocations[t.ContractId] : null,
                            ContractExecutionStatus = t.ContractExecutionStatus,
                            ContractEndTerminationDate = t.ContractEndTerminationDate,
                            CompanyName = t.CompanyName,
                            CompanyUin = t.CompanyUin,
                            CompanyType = t.CompanyType,
                            CompanyLegalType = t.CompanyLegalType,
                            Name = t.Name,
                            Measure = t.Measure,
                            BaseTotalValue = t.BaseTotalValue,
                            TargetTotalValue = t.TargetTotalValue,
                            ReportedTotalValue = t.ReportedTotalValue,
                            ApprovedTotalValue = t.ApprovedTotalValue,
                        }).ToList();
            }
            finally
            {
                this.unitOfWork.DbContext.Database.CommandTimeout = oldTimeout;
            }
        }

        private string GetContractAddress(Rio.Address address)
        {
            if (address.Country == null)
            {
                return null;
            }

            var addrStr = address.Country.Name;
            if (address.Country.Code == "BG")
            {
                if (address.Settlement != null)
                {
                    addrStr = string.Format("{0} {1}", addrStr, address.Settlement.Name);
                }

                if (!string.IsNullOrWhiteSpace(address.PostCode))
                {
                    addrStr = string.Format("{0} {1}", addrStr, address.PostCode);
                }

                if (!string.IsNullOrWhiteSpace(address.Street))
                {
                    addrStr = string.Format("{0} {1}", addrStr, address.Street);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(address.FullAddress))
                {
                    addrStr = string.Format("{0} {1}", addrStr, address.FullAddress);
                }
            }

            return addrStr;
        }

        private ProjectsReportItemStandingStatus? GetProjectStandingStatus(EvalSessionProjectStandingStatus? standingStatus, bool? isCanceled, bool isWithdrawed)
        {
            ProjectsReportItemStandingStatus? status = null;
            if (isCanceled == true)
            {
                status = ProjectsReportItemStandingStatus.Canceled;
            }
            else if (isWithdrawed)
            {
                status = ProjectsReportItemStandingStatus.Withdrawed;
            }

            if (!standingStatus.HasValue)
            {
                status = ProjectsReportItemStandingStatus.WithoutStanding;
            }
            else
            {
                switch (standingStatus)
                {
                    case EvalSessionProjectStandingStatus.Approved:
                        status = ProjectsReportItemStandingStatus.Approved;
                        break;
                    case EvalSessionProjectStandingStatus.Reserve:
                        status = ProjectsReportItemStandingStatus.Reserve;
                        break;
                    case EvalSessionProjectStandingStatus.Rejected:
                        status = ProjectsReportItemStandingStatus.Rejected;
                        break;
                    case EvalSessionProjectStandingStatus.RejectedAtASD:
                        status = ProjectsReportItemStandingStatus.RejectedAtASD;
                        break;
                    case EvalSessionProjectStandingStatus.RejectedAtTFO:
                        status = ProjectsReportItemStandingStatus.RejectedAtTFO;
                        break;
                    case null:
                        status = ProjectsReportItemStandingStatus.WithoutStanding;
                        break;
                    default:
                        throw new InvalidOperationException("Invalid standing status.");
                }
            }

            return status;
        }

        private IList<ContractReportCertReportNum> GetContractReportCertReportNums(Expression<Func<Domain.Contracts.Contract, bool>> cpredicate)
        {
            var certReportIds =
                 (from csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                  where csdbi.Status == ContractReportFinancialCSDBudgetItemStatus.Ended && csdbi.CertReportId.HasValue
                  select new
                  {
                      ContractId = csdbi.ContractId,
                      ContractReportId = csdbi.ContractReportId,
                      CertReportId = csdbi.CertReportId,
                  })
                  .Union(
                     from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                     where fccsd.Status == ContractReportFinancialCorrectionCSDStatus.Ended && fccsd.CertReportId.HasValue
                     select new
                     {
                         ContractId = fccsd.ContractId,
                         ContractReportId = fccsd.ContractReportId,
                         CertReportId = fccsd.CertReportId,
                     })
                  .Union(
                     from frcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                     where frcsd.Status == ContractReportFinancialRevalidationCSDStatus.Ended && frcsd.CertReportId.HasValue
                     select new
                     {
                         ContractId = frcsd.ContractId,
                         ContractReportId = frcsd.ContractReportId,
                         CertReportId = frcsd.CertReportId,
                     })
                  .Union(
                     from fcccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                     where fcccsd.Status == ContractReportFinancialCertCorrectionCSDStatus.Ended && fcccsd.CertReportId.HasValue
                     select new
                     {
                         ContractId = fcccsd.ContractId,
                         ContractReportId = fcccsd.ContractReportId,
                         CertReportId = fcccsd.CertReportId,
                     });

            return (from cri in certReportIds
                    join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(cpredicate) on cri.ContractId equals c.ContractId
                    join cr in this.unitOfWork.DbContext.Set<CertReport>() on cri.CertReportId equals cr.CertReportId
                    where cr.Status == CertReportStatus.Approved || cr.Status == CertReportStatus.PartialyApproved
                    select new ContractReportCertReportNum
                    {
                        ContractReportId = cri.ContractReportId,
                        OrderNum = cr.OrderNum,
                    })
                 .Distinct()
                 .ToList();
        }

        public IList<BudgetLevelsReportItem> GetBudgetLevelsReport(
            BudgetLevel budgetLevel,
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            var divisor = 1m;

            if (currency == Currency.Euro)
            {
                divisor = euroExchangeRates;
            }

            var contractBudgetLevel3Amounts = this.GetFinancialCSDBudgetItems()
                .Concat(this.GetFinancialCorrectionCSDs())
                .Concat(this.GetFinancialCertCorrectionCSDs())
                .Concat(this.GetFinancialRevalidationCSDs())
            .Where(t => t.AdvancePayment != YesNoNonApplicable.Yes)
            .Where(t => t.ContractBudgetLevel3AmountId.HasValue)
            .GroupBy(t => t.ContractBudgetLevel3AmountId)
            .Select(t => new
            {
                ContractBudgetLevel3AmountId = t.Key,
                ReportedEuAmount = t.Sum(p => p.ReportedEuAmount),
                ReportedBgAmount = t.Sum(p => p.ReportedBgAmount),
                ReportedBfpTotalAmount = t.Sum(p => p.ReportedBfpTotalAmount),
                ReportedSelfAmount = t.Sum(p => p.ReportedSelfAmount),
                ReportedTotalAmount = t.Sum(p => p.ReportedTotalAmount),
                ApprovedEuAmount = t.Sum(p => p.ApprovedEuAmount),
                ApprovedBgAmount = t.Sum(p => p.ApprovedBgAmount),
                ApprovedBfpTotalAmount = t.Sum(p => p.ApprovedBfpTotalAmount),
                ApprovedSelfAmount = t.Sum(p => p.ApprovedSelfAmount),
                ApprovedTotalAmount = t.Sum(p => p.ApprovedTotalAmount),
                CertifiedEuAmount = t.Sum(p => p.CertifiedEuAmount),
                CertifiedBgAmount = t.Sum(p => p.CertifiedBgAmount),
                CertifiedBfpTotalAmount = t.Sum(p => p.CertifiedBfpTotalAmount),
                CertifiedSelfAmount = t.Sum(p => p.CertifiedSelfAmount),
                CertifiedTotalAmount = t.Sum(p => p.CertifiedTotalAmount),
            });

            IQueryable<Domain.Contracts.Contract> contracts = this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>();

            string fullPath = this.GetNutsFullPath(
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);

            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                contracts = from c in contracts
                            join cl in this.unitOfWork.DbContext.Set<ContractLocation>() on c.ContractId equals cl.ContractId
                            where cl.FullPath.Contains(fullPath)
                            select c;
            }

            var procedureSharePredicate = PredicateBuilder.True<ProcedureShare>();
            procedureSharePredicate = procedureSharePredicate
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndEquals(t => t.ProgrammeId, programmeId)
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var contractPredicate = PredicateBuilder.True<Domain.Contracts.Contract>();
            contractPredicate = contractPredicate
                .AndDateTimeGreaterThanOrEqual(t => t.ContractDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.ContractDate, toDate);

            var amounts = from cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>()
                          join c in contracts.Where(contractPredicate) on cbl3a.ContractId equals c.ContractId

                          join a in contractBudgetLevel3Amounts on cbl3a.ContractBudgetLevel3AmountId equals a.ContractBudgetLevel3AmountId into g1
                          from a in g1.DefaultIfEmpty()

                          group new
                          {
                              ContractedEuAmount = cbl3a.CurrentEuAmount,
                              ContractedBgAmount = cbl3a.CurrentBgAmount,
                              ContractedBfpTotalAmount = cbl3a.CurrentEuAmount + cbl3a.CurrentBgAmount,
                              ContractedSelfAmount = cbl3a.CurrentSelfAmount,
                              ContractedTotalAmount = cbl3a.CurrentEuAmount + cbl3a.CurrentBgAmount + cbl3a.CurrentSelfAmount,
                              ReportedEuAmount = a != null ? a.ReportedEuAmount : 0m,
                              ReportedBgAmount = a != null ? a.ReportedBgAmount : 0m,
                              ReportedBfpTotalAmount = a != null ? a.ReportedBfpTotalAmount : 0m,
                              ReportedSelfAmount = a != null ? a.ReportedSelfAmount : 0m,
                              ReportedTotalAmount = a != null ? a.ReportedTotalAmount : 0m,
                              ApprovedEuAmount = a != null ? a.ApprovedEuAmount : 0m,
                              ApprovedBgAmount = a != null ? a.ApprovedBgAmount : 0m,
                              ApprovedBfpTotalAmount = a != null ? a.ApprovedBfpTotalAmount : 0m,
                              ApprovedSelfAmount = a != null ? a.ApprovedSelfAmount : 0m,
                              ApprovedTotalAmount = a != null ? a.ApprovedTotalAmount : 0m,
                              CertifiedEuAmount = a != null ? a.CertifiedEuAmount : 0m,
                              CertifiedBgAmount = a != null ? a.CertifiedBgAmount : 0m,
                              CertifiedBfpTotalAmount = a != null ? a.CertifiedBfpTotalAmount : 0m,
                              CertifiedSelfAmount = a != null ? a.CertifiedSelfAmount : 0m,
                              CertifiedTotalAmount = a != null ? a.CertifiedTotalAmount : 0m,
                          }
                            by cbl3a.ProcedureBudgetLevel2Id into g
                          select new
                          {
                              ProcedureBudgetLevel2Id = g.Key,
                              ContractedEuAmount = g.Sum(t => t.ContractedEuAmount),
                              ContractedBgAmount = g.Sum(t => t.ContractedBgAmount),
                              ContractedBfpTotalAmount = g.Sum(t => t.ContractedBfpTotalAmount),
                              ContractedSelfAmount = g.Sum(t => t.ContractedSelfAmount),
                              ContractedTotalAmount = g.Sum(t => t.ContractedTotalAmount),
                              ReportedEuAmount = g.Sum(t => t.ReportedEuAmount),
                              ReportedBgAmount = g.Sum(t => t.ReportedBgAmount),
                              ReportedBfpTotalAmount = g.Sum(t => t.ReportedBfpTotalAmount),
                              ReportedSelfAmount = g.Sum(t => t.ReportedSelfAmount),
                              ReportedTotalAmount = g.Sum(t => t.ReportedTotalAmount),
                              ApprovedEuAmount = g.Sum(t => t.ApprovedEuAmount),
                              ApprovedBgAmount = g.Sum(t => t.ApprovedBgAmount),
                              ApprovedBfpTotalAmount = g.Sum(t => t.ApprovedBfpTotalAmount),
                              ApprovedSelfAmount = g.Sum(t => t.ApprovedSelfAmount),
                              ApprovedTotalAmount = g.Sum(t => t.ApprovedTotalAmount),
                              CertifiedEuAmount = g.Sum(t => t.CertifiedEuAmount),
                              CertifiedBgAmount = g.Sum(t => t.CertifiedBgAmount),
                              CertifiedBfpTotalAmount = g.Sum(t => t.CertifiedBfpTotalAmount),
                              CertifiedSelfAmount = g.Sum(t => t.CertifiedSelfAmount),
                              CertifiedTotalAmount = g.Sum(t => t.CertifiedTotalAmount),
                          };

            var initialResults = from pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>()
                                 join pl1 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel1>() on pl2.ProcedureBudgetLevel1Id equals pl1.ProcedureBudgetLevel1Id
                                 join et in this.unitOfWork.DbContext.Set<ExpenseType>() on pl1.ExpenseTypeId equals et.ExpenseTypeId
                                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(procedureSharePredicate) on pl2.ProcedureShareId equals ps.ProcedureShareId
                                 join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                 join p in this.unitOfWork.DbContext.Set<Procedure>() on pl1.ProcedureId equals p.ProcedureId

                                 join a in amounts on pl2.ProcedureBudgetLevel2Id equals a.ProcedureBudgetLevel2Id into g0
                                 from a in g0.DefaultIfEmpty()

                                 select new BudgetLevelsReportResultItem
                                 {
                                     ProcedureBudgetLevel1Id = pl1.ProcedureBudgetLevel1Id,
                                     ProcedureBudgetLevel2Id = pl2.ProcedureBudgetLevel2Id,
                                     BudgetLevel1Name = et.Name,
                                     BudgetLevel2Name = pl2.Name,
                                     ProcedureName = p.Name,
                                     ContractedEuAmount = a != null ? a.ContractedEuAmount : 0m,
                                     ContractedBgAmount = a != null ? a.ContractedBgAmount : 0m,
                                     ContractedBfpTotalAmount = a != null ? a.ContractedBfpTotalAmount : 0m,
                                     ContractedSelfAmount = a != null ? a.ContractedSelfAmount : 0m,
                                     ContractedTotalAmount = a != null ? a.ContractedTotalAmount : 0m,
                                     ReportedEuAmount = a != null ? a.ReportedEuAmount : 0m,
                                     ReportedBgAmount = a != null ? a.ReportedBgAmount : 0m,
                                     ReportedBfpTotalAmount = a != null ? a.ReportedBfpTotalAmount : 0m,
                                     ReportedSelfAmount = a != null ? a.ReportedSelfAmount : 0m,
                                     ReportedTotalAmount = a != null ? a.ReportedTotalAmount : 0m,
                                     ApprovedEuAmount = a != null ? a.ApprovedEuAmount : 0m,
                                     ApprovedBgAmount = a != null ? a.ApprovedBgAmount : 0m,
                                     ApprovedBfpTotalAmount = a != null ? a.ApprovedBfpTotalAmount : 0m,
                                     ApprovedSelfAmount = a != null ? a.ApprovedSelfAmount : 0m,
                                     ApprovedTotalAmount = a != null ? a.ApprovedTotalAmount : 0m,
                                     CertifiedEuAmount = a != null ? a.CertifiedEuAmount : 0m,
                                     CertifiedBgAmount = a != null ? a.CertifiedBgAmount : 0m,
                                     CertifiedBfpTotalAmount = a != null ? a.CertifiedBfpTotalAmount : 0m,
                                     CertifiedSelfAmount = a != null ? a.CertifiedSelfAmount : 0m,
                                     CertifiedTotalAmount = a != null ? a.CertifiedTotalAmount : 0m,

                                     AreAmountsNull = a == null,
                                 };

            IQueryable<BudgetLevelsReportResultItem> results = null;
            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                results = initialResults.Where(t => t.AreAmountsNull == false);
            }
            else
            {
                results = initialResults;
            }

            if (budgetLevel == BudgetLevel.First)
            {
                return results.GroupBy(t => new { t.ProcedureBudgetLevel1Id, t.BudgetLevel1Name, t.ProcedureName }).Select(p => new BudgetLevelsReportItem()
                {
                    BudgetLevel = BudgetLevel.First,
                    BudgetLevel1Name = p.Key.BudgetLevel1Name,
                    BudgetLevel2Name = null,
                    ProcedureName = p.Key.ProcedureName,
                    ContractedEuAmount = p.Sum(t => t.ContractedEuAmount) / divisor,
                    ContractedBgAmount = p.Sum(t => t.ContractedBgAmount) / divisor,
                    ContractedBfpTotalAmount = p.Sum(t => t.ContractedBfpTotalAmount) / divisor,
                    ContractedSelfAmount = p.Sum(t => t.ContractedSelfAmount) / divisor,
                    ContractedTotalAmount = p.Sum(t => t.ContractedTotalAmount) / divisor,
                    ReportedEuAmount = p.Sum(t => t.ReportedEuAmount) / divisor,
                    ReportedBgAmount = p.Sum(t => t.ReportedBgAmount) / divisor,
                    ReportedBfpTotalAmount = p.Sum(t => t.ReportedBfpTotalAmount) / divisor,
                    ReportedSelfAmount = p.Sum(t => t.ReportedSelfAmount) / divisor,
                    ReportedTotalAmount = p.Sum(t => t.ReportedTotalAmount) / divisor,
                    ApprovedEuAmount = p.Sum(t => t.ApprovedEuAmount) / divisor,
                    ApprovedBgAmount = p.Sum(t => t.ApprovedBgAmount) / divisor,
                    ApprovedBfpTotalAmount = p.Sum(t => t.ApprovedBfpTotalAmount) / divisor,
                    ApprovedSelfAmount = p.Sum(t => t.ApprovedSelfAmount) / divisor,
                    ApprovedTotalAmount = p.Sum(t => t.ApprovedTotalAmount) / divisor,
                    CertifiedEuAmount = p.Sum(t => t.CertifiedEuAmount) / divisor,
                    CertifiedBgAmount = p.Sum(t => t.CertifiedBgAmount) / divisor,
                    CertifiedBfpTotalAmount = p.Sum(t => t.CertifiedBfpTotalAmount) / divisor,
                    CertifiedSelfAmount = p.Sum(t => t.CertifiedSelfAmount) / divisor,
                    CertifiedTotalAmount = p.Sum(t => t.CertifiedTotalAmount) / divisor,
                }).OrderBy(t => t.ProcedureName).ToList();
            }
            else
            {
                return results.Select(p => new BudgetLevelsReportItem()
                {
                    BudgetLevel = BudgetLevel.Second,
                    BudgetLevel1Name = p.BudgetLevel1Name,
                    BudgetLevel2Name = p.BudgetLevel2Name,
                    ProcedureName = p.ProcedureName,
                    ContractedEuAmount = p.ContractedEuAmount / divisor,
                    ContractedBgAmount = p.ContractedBgAmount / divisor,
                    ContractedBfpTotalAmount = p.ContractedBfpTotalAmount / divisor,
                    ContractedSelfAmount = p.ContractedSelfAmount / divisor,
                    ContractedTotalAmount = p.ContractedTotalAmount / divisor,
                    ReportedEuAmount = p.ReportedEuAmount / divisor,
                    ReportedBgAmount = p.ReportedBgAmount / divisor,
                    ReportedBfpTotalAmount = p.ReportedBfpTotalAmount / divisor,
                    ReportedSelfAmount = p.ReportedSelfAmount / divisor,
                    ReportedTotalAmount = p.ReportedTotalAmount / divisor,
                    ApprovedEuAmount = p.ApprovedEuAmount / divisor,
                    ApprovedBgAmount = p.ApprovedBgAmount / divisor,
                    ApprovedBfpTotalAmount = p.ApprovedBfpTotalAmount / divisor,
                    ApprovedSelfAmount = p.ApprovedSelfAmount / divisor,
                    ApprovedTotalAmount = p.ApprovedTotalAmount / divisor,
                    CertifiedEuAmount = p.CertifiedEuAmount / divisor,
                    CertifiedBgAmount = p.CertifiedBgAmount / divisor,
                    CertifiedBfpTotalAmount = p.CertifiedBfpTotalAmount / divisor,
                    CertifiedSelfAmount = p.CertifiedSelfAmount / divisor,
                    CertifiedTotalAmount = p.CertifiedTotalAmount / divisor,
                }).OrderBy(t => t.ProcedureName).ToList();
            }
        }

        public IList<FinancialCorrectionsReportItem> GetFinancialCorrectionsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null)
        {
            var divisor = 1m;

            if (currency == Currency.Euro)
            {
                divisor = euroExchangeRates;
            }

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var contractPredicate = PredicateBuilder.True<Domain.Contracts.Contract>();
            contractPredicate = contractPredicate
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndEquals(t => t.ProgrammeId, programmeId);

            var financialCorrectionPredicate = PredicateBuilder.True<FinancialCorrection>();
            financialCorrectionPredicate = financialCorrectionPredicate
                .AndDateTimeGreaterThanOrEqual(t => t.ImpositionDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.ImpositionDate, toDate);

            var amountPredicate = PredicateBuilder.True<ContractAmountDO>();
            amountPredicate = amountPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var amounts = this.GetFinancialCSDBudgetItems()
                .Concat(this.GetFinancialCorrectionCSDs())
                .Concat(this.GetCorrections())
                .Where(t => t.FinancialCorrectionId.HasValue)
                .Where(amountPredicate)
                .GroupBy(t => t.FinancialCorrectionId).Select(t => new
                {
                    FinancialCorrectionId = t.Key,
                    CorrectedTotalAmount = t.Sum(a => a.CorrectedTotalAmount),
                    CorrectedBfpTotalAmount = t.Sum(a => a.CorrectedBfpTotalAmount),
                    CorrectedEuAmount = t.Sum(a => a.CorrectedEuAmount),
                    CorrectedBgAmount = t.Sum(a => a.CorrectedBgAmount),
                    CorrectedSelfAmount = t.Sum(a => a.CorrectedSelfAmount),
                });

            var results = (from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>().Where(financialCorrectionPredicate)

                           join ifcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>() on fc.FinancialCorrectionId equals ifcv.FinancialCorrectionId
                           join ifcir in this.unitOfWork.DbContext.Set<FinancialCorrectionImposingReason>() on ifcv.FinancialCorrectionImposingReasonId equals ifcir.FinancialCorrectionImposingReasonId

                           join afcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>() on fc.FinancialCorrectionId equals afcv.FinancialCorrectionId
                           join afcir in this.unitOfWork.DbContext.Set<FinancialCorrectionImposingReason>() on afcv.FinancialCorrectionImposingReasonId equals afcir.FinancialCorrectionImposingReasonId

                           join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(contractPredicate) on fc.ContractId equals c.ContractId

                           join ct in this.unitOfWork.DbContext.Set<CompanyType>() on c.CompanyTypeId equals ct.CompanyTypeId into g0
                           from ct in g0.DefaultIfEmpty()

                           join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on c.CompanyLegalTypeId equals clt.CompanyLegalTypeId into g1
                           from clt in g1.DefaultIfEmpty()

                           join p in this.unitOfWork.DbContext.Set<Programme>() on c.ProgrammeId equals p.MapNodeId
                           join pr in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals pr.ProcedureId

                           join cc in this.unitOfWork.DbContext.Set<ContractContract>() on fc.ContractContractId equals cc.ContractContractId into g2
                           from cc in g2.DefaultIfEmpty()

                           join cctor in this.unitOfWork.DbContext.Set<Domain.Contracts.ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into g3
                           from cctor in g3.DefaultIfEmpty()

                           join a in amounts on fc.FinancialCorrectionId equals a.FinancialCorrectionId into g4
                           from a in g4.DefaultIfEmpty()

                           where fc.Status == FinancialCorrectionStatus.Entered && afcv.Status == FinancialCorrectionVersionStatus.Actual && ifcv.OrderNum == 1
                           orderby c.RegNumber
                           select new
                           {
                               FinancialCorrectionId = fc.FinancialCorrectionId,
                               Programme = p.Name,
                               //// todo ProgrammePriority = null,
                               Procedure = pr.Name,
                               ContractRegNum = c.RegNumber,
                               CompanyUin = c.CompanyUin,
                               CompanyName = c.CompanyName,
                               CompanyType = ct != null ? ct.Name : null,
                               CompanyLegalType = clt != null ? clt.Name : null,
                               CorrectionDate = fc.ImpositionDate,
                               CorrectionNum = fc.OrderNum,
                               ContractContractNumber = cc != null ? cc.Number : null,
                               ContractContractSignDate = cc != null ? cc.SignDate : (DateTime?)null,
                               ContractContractCompanyName = cctor != null ? cctor.Name : null,
                               ContractContractUin = cctor != null ? cctor.Uin : null,
                               InitialFinancialCorrectionVersionId = ifcv.FinancialCorrectionVersionId,
                               InitialContractContractPercent = cc != null ? ifcv.Percent : null,
                               InitialAmountTotal = ifcv.TotalAmount,
                               InitialAmountBfp = ifcv.BfpAmount,
                               InitialAmountEu = ifcv.EuAmount,
                               InitialAmountBg = ifcv.BgAmount,
                               InitialAmountSelf = ifcv.SelfAmount,
                               InitialReason = ifcir.Name,
                               InitialViolationFoundBy = ifcv.ViolationFoundBy,
                               InitialBearer = ifcv.CorrectionBearer,
                               CurrentFinancialCorrectionVersionId = afcv.FinancialCorrectionVersionId,
                               CurrentContractContractPercent = cc != null ? afcv.Percent : null,
                               CurrentAmountTotal = afcv.TotalAmount,
                               CurrentAmountBfp = afcv.BfpAmount,
                               CurrentAmountEu = afcv.EuAmount,
                               CurrentAmountBg = afcv.BgAmount,
                               CurrentAmountSelf = afcv.SelfAmount,
                               AmendmentReason = afcv.AmendmentReason,
                               CurrentReason = afcir.Name,
                               CurrentViolationFoundBy = afcv.ViolationFoundBy,
                               CurrentBearer = afcv.CorrectionBearer,
                               //// todo ReasonBase = null,
                               CorretionAmountTotal = a != null ? a.CorrectedTotalAmount : 0m,
                               CorretionAmountBfp = a != null ? a.CorrectedBfpTotalAmount : 0m,
                               CorretionAmountEu = a != null ? a.CorrectedEuAmount : 0m,
                               CorretionAmountBg = a != null ? a.CorrectedBgAmount : 0m,
                               CorretionAmountSelf = a != null ? a.CorrectedSelfAmount : 0m,
                           }).ToList();

            var financialCorrectionIds = results.Select(t => t.FinancialCorrectionId).ToArray();

            var otherViolations = (from fcvv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersionViolation>()
                                   join fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>() on fcvv.FinancialCorrectionVersionId equals fcv.FinancialCorrectionVersionId
                                   join ov in this.unitOfWork.DbContext.Set<OtherViolation>() on fcvv.OtherViolationId equals ov.OtherViolationId
                                   where financialCorrectionIds.Contains(fcv.FinancialCorrectionId)
                                   group ov.Name by fcvv.FinancialCorrectionVersionId into g
                                   select new
                                   {
                                       FinancialCorrectionVersionId = g.Key,
                                       OtherViolations = g.AsEnumerable(),
                                   })
                    .ToList()
                    .ToDictionary(t => t.FinancialCorrectionVersionId, t => string.Join(", ", t.OtherViolations));

            var contractReportIds = this.GetFinancialCSDBudgetItems().Concat(this.GetFinancialCorrectionCSDs())
                .Where(t => financialCorrectionIds.Contains(t.FinancialCorrectionId.Value))
                .Select(t => new { t.FinancialCorrectionId, t.ContractReportId }).Distinct();

            var contractReportPayments = (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                                          join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cr.ContractReportId equals crp.ContractReportId
                                          join cri in contractReportIds on cr.ContractReportId equals cri.ContractReportId
                                          where crp.Status == ContractReportPaymentStatus.Actual
                                          group "ИП " + crp.VersionNum by cri.FinancialCorrectionId into g
                                          select new
                                          {
                                              FinancialCorrectionId = g.Key,
                                              OrderNums = g.AsEnumerable(),
                                          })
                                .ToList()
                                .ToDictionary(t => t.FinancialCorrectionId, t => string.Join(", ", t.OrderNums));

            var certReportIds = this.GetFinancialCSDBudgetItems().Concat(this.GetFinancialCorrectionCSDs())
                .Where(t => financialCorrectionIds.Contains(t.FinancialCorrectionId.Value))
                .Select(t => new { t.FinancialCorrectionId, t.CertReportId }).Distinct();

            var certReports = (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                               join cri in certReportIds on cr.CertReportId equals cri.CertReportId
                               group "ДС " + cr.OrderNum by cri.FinancialCorrectionId into g
                               select new
                               {
                                   FinancialCorrectionId = g.Key,
                                   OrderNums = g.AsEnumerable(),
                               })
                                .ToList()
                                .ToDictionary(t => t.FinancialCorrectionId, t => string.Join(", ", t.OrderNums));

            var irregularities = (from ifc in this.unitOfWork.DbContext.Set<IrregularityFinancialCorrection>()
                                  join irr in this.unitOfWork.DbContext.Set<Irregularity>() on ifc.IrregularityId equals irr.IrregularityId
                                  where financialCorrectionIds.Contains(ifc.FinancialCorrectionId)
                                  group irr.RegNumber by ifc.FinancialCorrectionId into g
                                  select new
                                  {
                                      FinancialCorrectionId = g.Key,
                                      RegNums = g.AsEnumerable(),
                                  })
                                .ToList()
                                .ToDictionary(t => t.FinancialCorrectionId, t => string.Join(", ", t.RegNums));

            return results.Select(t => new FinancialCorrectionsReportItem()
            {
                Programme = t.Programme,
                ProgrammePriority = null, // todo
                Procedure = t.Procedure,
                ContractRegNum = t.ContractRegNum,
                CompanyUin = t.CompanyUin,
                CompanyName = t.CompanyName,
                CompanyType = t.CompanyType,
                CompanyLegalType = t.CompanyLegalType,
                CorrectionDate = t.CorrectionDate,
                CorrectionNum = t.CorrectionNum,
                ContractContractName = t.ContractContractNumber != null ? string.Format("{0} {1: dd.MM.yyyy}", t.ContractContractNumber, t.ContractContractSignDate) : null,
                ContractContractCompanyName = t.ContractContractCompanyName,
                ContractContractUin = t.ContractContractUin,
                InitialContractContractPercent = t.InitialContractContractPercent,
                InitialAmountTotal = t.InitialAmountTotal / divisor,
                InitialAmountBfp = t.InitialAmountBfp / divisor,
                InitialAmountEu = t.InitialAmountEu / divisor,
                InitialAmountBg = t.InitialAmountBg / divisor,
                InitialAmountSelf = t.InitialAmountSelf / divisor,
                InitialReason = t.InitialReason,
                InitialViolations = otherViolations.ContainsKey(t.InitialFinancialCorrectionVersionId) ? otherViolations[t.InitialFinancialCorrectionVersionId] : null,
                InitialViolationFoundBy = t.InitialViolationFoundBy,
                InitialBearer = t.InitialBearer,
                CurrentContractContractPercent = t.CurrentContractContractPercent,
                CurrentAmountTotal = t.CurrentAmountTotal / divisor,
                CurrentAmountBfp = t.CurrentAmountBfp / divisor,
                CurrentAmountEu = t.CurrentAmountEu / divisor,
                CurrentAmountBg = t.CurrentAmountBg / divisor,
                CurrentAmountSelf = t.CurrentAmountSelf / divisor,
                AmendmentReason = t.AmendmentReason,
                CurrentReason = t.CurrentReason,
                CurrentViolations = otherViolations.ContainsKey(t.CurrentFinancialCorrectionVersionId) ? otherViolations[t.CurrentFinancialCorrectionVersionId] : null,
                CurrentViolationFoundBy = t.CurrentViolationFoundBy,
                CurrentBearer = t.CurrentBearer,
                Irregularity = irregularities.ContainsKey(t.FinancialCorrectionId) ? irregularities[t.FinancialCorrectionId] : null,
                ReasonBase = null, // TODO
                CorretionAmountTotal = t.CorretionAmountTotal / divisor,
                CorretionAmountBfp = t.CorretionAmountBfp / divisor,
                CorretionAmountEu = t.CorretionAmountEu / divisor,
                CorretionAmountBg = t.CorretionAmountBg / divisor,
                CorretionAmountSelf = t.CorretionAmountSelf / divisor,
                ContractReportPayments = contractReportPayments.ContainsKey(t.FinancialCorrectionId) ? contractReportPayments[t.FinancialCorrectionId] : null,
                CertReports = certReports.ContainsKey(t.FinancialCorrectionId) ? certReports[t.FinancialCorrectionId] : null,
            }).ToList();
        }

        public IList<ConcludedContractsReportItem> GetConcludedContractsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            string uin = null)
        {
            var divisor = 1m;

            if (currency == Currency.Euro)
            {
                divisor = euroExchangeRates;
            }

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var contractPredicate = PredicateBuilder.True<Domain.Contracts.Contract>();
            contractPredicate = contractPredicate
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndEquals(t => t.ProgrammeId, programmeId);

            var contractContractPredicate = PredicateBuilder.True<Domain.Contracts.ContractContract>();
            contractContractPredicate = contractContractPredicate
                .AndDateTimeGreaterThanOrEqual(t => t.SignDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.SignDate, toDate);

            var contractContractorPredicate = PredicateBuilder.True<Domain.Contracts.ContractContractor>();
            contractContractorPredicate = contractContractorPredicate.AndStringContains(t => t.Uin, uin);

            var financialCorrections = from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                                       join fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>() on fc.FinancialCorrectionId equals fcv.FinancialCorrectionId
                                       where fc.Status == FinancialCorrectionStatus.Entered && fcv.Status == FinancialCorrectionVersionStatus.Actual && fc.ContractContractId.HasValue
                                       group new
                                       {
                                           fcv.EuAmount,
                                           fcv.BgAmount,
                                           fcv.BfpAmount,
                                           fcv.SelfAmount,
                                           fcv.TotalAmount,
                                       }
                                       by fc.ContractContractId into g
                                       select new
                                       {
                                           ContractContractId = g.Key,
                                           CorrectedEuAmount = g.Sum(t => t.EuAmount),
                                           CorrectedBgAmount = g.Sum(t => t.BgAmount),
                                           CorrectedBfpAmount = g.Sum(t => t.BfpAmount),
                                           CorrectedSelfAmount = g.Sum(t => t.SelfAmount),
                                           CorrectedTotalAmount = g.Sum(t => t.TotalAmount),
                                       };

            var amounts = this.GetFinancialCSDBudgetItems()
                .Concat(this.GetFinancialCorrectionCSDs())
                .Concat(this.GetFinancialCertCorrectionCSDs())
                .Concat(this.GetFinancialRevalidationCSDs())
                .Where(t => t.ContractContractId.HasValue)
                .GroupBy(t => t.ContractContractId).Select(t => new
                {
                    ContractContractId = t.Key,
                    ReportedTotalAmount = t.Sum(a => a.ReportedTotalAmount),
                    ReportedBfpTotalAmount = t.Sum(a => a.ReportedBfpTotalAmount),
                    ReportedEuAmount = t.Sum(a => a.ReportedEuAmount),
                    ReportedBgAmount = t.Sum(a => a.ReportedBgAmount),
                    ReportedSelfAmount = t.Sum(a => a.ReportedSelfAmount),
                    ApprovedTotalAmount = t.Sum(a => a.ApprovedTotalAmount),
                    ApprovedBfpTotalAmount = t.Sum(a => a.ApprovedBfpTotalAmount),
                    ApprovedEuAmount = t.Sum(a => a.ApprovedEuAmount),
                    ApprovedBgAmount = t.Sum(a => a.ApprovedBgAmount),
                    ApprovedSelfAmount = t.Sum(a => a.ApprovedSelfAmount),
                    CertifiedTotalAmount = t.Sum(a => a.CertifiedTotalAmount),
                    CertifiedBfpTotalAmount = t.Sum(a => a.CertifiedBfpTotalAmount),
                    CertifiedEuAmount = t.Sum(a => a.CertifiedEuAmount),
                    CertifiedBgAmount = t.Sum(a => a.CertifiedBgAmount),
                    CertifiedSelfAmount = t.Sum(a => a.CertifiedSelfAmount),
                });

            var results = (from cc in this.unitOfWork.DbContext.Set<ContractContract>().Where(contractContractPredicate)
                           join cctor in this.unitOfWork.DbContext.Set<Domain.Contracts.ContractContractor>().Where(contractContractorPredicate) on cc.ContractContractorId equals cctor.ContractContractorId
                           join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(contractPredicate) on cc.ContractId equals c.ContractId

                           join ct in this.unitOfWork.DbContext.Set<CompanyType>() on c.CompanyTypeId equals ct.CompanyTypeId into g0
                           from ct in g0.DefaultIfEmpty()

                           join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on c.CompanyLegalTypeId equals clt.CompanyLegalTypeId into g1
                           from clt in g1.DefaultIfEmpty()

                           join cdp in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on cc.ContractContractId equals cdp.ContractContractId into g2
                           from cdp in g2.DefaultIfEmpty()

                           join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdp.ContractProcurementPlanId equals cpp.ContractProcurementPlanId into g3
                           from cpp in g3.DefaultIfEmpty()

                           join ea in this.unitOfWork.DbContext.Set<ErrandArea>() on cpp.ErrandAreaId equals ea.ErrandAreaId into g4
                           from ea in g4.DefaultIfEmpty()

                           join ela in this.unitOfWork.DbContext.Set<ErrandLegalAct>() on cpp.ErrandLegalActId equals ela.ErrandLegalActId into g5
                           from ela in g5.DefaultIfEmpty()

                           join et in this.unitOfWork.DbContext.Set<ErrandType>() on cpp.ErrandTypeId equals et.ErrandTypeId into g6
                           from et in g6.DefaultIfEmpty()

                           join fc in financialCorrections on cc.ContractContractId equals fc.ContractContractId into g7
                           from fc in g7.DefaultIfEmpty()

                           join a in amounts on cc.ContractContractId equals a.ContractContractId into g8
                           from a in g8.DefaultIfEmpty()

                           orderby c.RegNumber

                           select new
                           {
                               ContractContractId = cc.ContractContractId,
                               ContractContractNumber = cc.Number,
                               ContractContractSignDate = cc.SignDate,
                               CompanyName = cctor.Name,
                               CompanyUin = cctor.Uin,
                               ContractDate = cc.SignDate,
                               ContractRegNum = c.RegNumber,
                               ContractCompanyName = c.CompanyName,
                               ContractCompanyUin = c.CompanyUin,
                               ContractCompanyType = ct.Name,
                               ContractCompanyLegalType = clt.Name,
                               ContractContractName = cpp.Name,
                               ErrandArea = ea.Name,
                               ErrandLegalAct = ela.Name,
                               ErrandType = et.Name,
                               TotalFundedValue = cc.TotalFundedValue,
                               VATAmountIfEligible = cc.VATAmountIfEligible,
                               ReportedTotalAmount = a != null ? a.ReportedTotalAmount : 0m,
                               ReportedBfpAmount = a != null ? a.ReportedBfpTotalAmount : 0m,
                               ReportedSelfAmount = a != null ? a.ReportedSelfAmount : 0m,
                               ApprovedTotalAmount = a != null ? a.ApprovedTotalAmount : 0m,
                               ApprovedBfpAmount = a != null ? a.ApprovedBfpTotalAmount : 0m,
                               ApprovedSelfAmount = a != null ? a.ApprovedSelfAmount : 0m,
                               CertifiedTotalAmount = a != null ? a.CertifiedTotalAmount : 0m,
                               CertifiedBfpAmount = a != null ? a.CertifiedBfpTotalAmount : 0m,
                               CertifiedSelfAmount = a != null ? a.CertifiedSelfAmount : 0m,
                               FinancialCorrectionTotalAmount = fc != null ? fc.CorrectedTotalAmount : 0m,
                               FinancialCorrectionBfpAmount = fc != null ? fc.CorrectedBfpAmount : 0m,
                               FinancialCorrectionEuAmount = fc != null ? fc.CorrectedEuAmount : 0m,
                               FinancialCorrectionBgAmount = fc != null ? fc.CorrectedBgAmount : 0m,
                               FinancialCorrectionSelfAmount = fc != null ? fc.CorrectedSelfAmount : 0m,
                           }).ToList();

            var contractContractIds = results.Select(t => t.ContractContractId).ToArray();

            var contractSubcontracts = (from csc in this.unitOfWork.DbContext.Set<ContractSubcontract>()
                                        join cctor in this.unitOfWork.DbContext.Set<Domain.Contracts.ContractContractor>() on csc.ContractContractorId equals cctor.ContractContractorId
                                        where contractContractIds.Contains(csc.ContractContractId)
                                        select new
                                        {
                                            csc.ContractContractId,
                                            ContractSubcontractType = csc.Type,
                                            cctor.Name,
                                            cctor.Uin,
                                            cctor.UinType,
                                        })
                                .ToList();

            var subcontractors = contractSubcontracts.Where(t => t.ContractSubcontractType == ContractSubcontractType.Subcontractor)
                            .GroupBy(t => t.ContractContractId)
                            .ToDictionary(t => t.Key, t => string.Join(", ", t.Select(p => p.Name + " (" + p.UinType.GetEnumDescription() + ": " + p.Uin + ")")));

            var unionMembers = contractSubcontracts.Where(t => t.ContractSubcontractType == ContractSubcontractType.Member)
                            .GroupBy(t => t.ContractContractId)
                            .ToDictionary(t => t.Key, t => string.Join(", ", t.Select(p => p.Name + " (" + p.UinType.GetEnumDescription() + ": " + p.Uin + ")")));

            return results.Select(t => new ConcludedContractsReportItem
            {
                ContractContractRegNum = string.Format("{0} {1: dd.MM.yyyy}", t.ContractContractNumber, t.ContractContractSignDate),
                CompanyName = t.CompanyName,
                CompanyUin = t.CompanyUin,
                ContractDate = t.ContractDate,
                ContractRegNum = t.ContractRegNum,
                ContractCompanyName = t.ContractCompanyName,
                ContractCompanyUin = t.ContractCompanyUin,
                ContractCompanyType = t.ContractCompanyType,
                ContractCompanyLegalType = t.ContractCompanyLegalType,
                ContractContractName = t.ContractContractName,
                ErrandArea = t.ErrandArea,
                ErrandLegalAct = t.ErrandLegalAct,
                ErrandType = t.ErrandType,
                TotalFundedValue = t.TotalFundedValue / divisor,
                VATAmountIfEligible = t.VATAmountIfEligible / divisor,
                Subcontractors = subcontractors.ContainsKey(t.ContractContractId) ? subcontractors[t.ContractContractId] : null,
                UnionMembers = unionMembers.ContainsKey(t.ContractContractId) ? unionMembers[t.ContractContractId] : null,
                ReportedTotalAmount = t.ReportedTotalAmount / divisor,
                ReportedBfpAmount = t.ReportedBfpAmount / divisor,
                ReportedSelfAmount = t.ReportedSelfAmount / divisor,
                ApprovedTotalAmount = t.ApprovedTotalAmount / divisor,
                ApprovedBfpAmount = t.ApprovedBfpAmount / divisor,
                ApprovedSelfAmount = t.ApprovedSelfAmount / divisor,
                CertifiedTotalAmount = t.CertifiedTotalAmount / divisor,
                CertifiedBfpAmount = t.CertifiedBfpAmount / divisor,
                CertifiedSelfAmount = t.CertifiedSelfAmount / divisor,
                FinancialCorrectionTotalAmount = t.FinancialCorrectionTotalAmount / divisor,
                FinancialCorrectionBfpAmount = t.FinancialCorrectionBfpAmount / divisor,
                FinancialCorrectionEuAmount = t.FinancialCorrectionEuAmount / divisor,
                FinancialCorrectionBgAmount = t.FinancialCorrectionBgAmount / divisor,
                FinancialCorrectionSelfAmount = t.FinancialCorrectionSelfAmount / divisor,
            }).ToList();
        }

        public IList<BeneficiaryProjectsContractsReportItem> GetBeneficiaryProjectsContractsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null)
        {
            var divisor = 1m;

            if (currency == Currency.Euro)
            {
                divisor = euroExchangeRates;
            }

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var contractPredicate = PredicateBuilder.True<Domain.Contracts.Contract>();
            contractPredicate = contractPredicate
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndEquals(t => t.ProgrammeId, programmeId)
                .AndDateTimeGreaterThanOrEqual(t => t.ContractDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.ContractDate, toDate);

            var projectPredicate = PredicateBuilder.True<Domain.Projects.Project>();
            projectPredicate = projectPredicate
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndDateTimeGreaterThanOrEqual(t => t.RegDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.RegDate, toDate);

            var projects = from p in this.unitOfWork.DbContext.Set<Domain.Projects.Project>().Where(projectPredicate)
                           group new
                           {
                               SelfAmount = p.CoFinancingAmount,
                               TotalBfpAmount = p.TotalBfpAmount,
                               TotalAmount = p.TotalBfpAmount + p.CoFinancingAmount,
                           }
                           by p.CompanyId into g
                           select new
                           {
                               CompanyId = g.Key,
                               ProjectsCount = g.Count(),

                               SelfAmount = g.Sum(t => t.SelfAmount),
                               TotalBfpAmount = g.Sum(t => t.TotalBfpAmount),
                               TotalAmount = g.Sum(t => t.TotalAmount),
                           };

            var paidAmountPredicate = PredicateBuilder.True<ActuallyPaidAmount>();
            paidAmountPredicate = paidAmountPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var actuallyPaidAmounts = from apa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(paidAmountPredicate)
                                      where apa.Status == ActuallyPaidAmountStatus.Entered
                                      group new
                                      {
                                          apa.PaidBfpEuAmount,
                                          apa.PaidBfpBgAmount,
                                          apa.PaidBfpTotalAmount,
                                          apa.PaidSelfAmount,
                                          apa.PaidTotalAmount,
                                      }
                                      by apa.ContractId into g
                                      select new
                                      {
                                          ContractId = g.Key,
                                          ActuallyPaidEuAmount = g.Sum(t => t.PaidBfpEuAmount),
                                          ActuallyPaidBgAmount = g.Sum(t => t.PaidBfpBgAmount),
                                          ActuallyPaidBfpAmount = g.Sum(t => t.PaidBfpTotalAmount),
                                          ActuallyPaidSelfAmount = g.Sum(t => t.PaidSelfAmount),
                                          ActuallyPaidTotalAmount = g.Sum(t => t.PaidTotalAmount),
                                      };

            var irregularities = from i in this.unitOfWork.DbContext.Set<Irregularity>()
                                 where i.Status == IrregularityStatus.Entered
                                 group i.IrregularityId by i.ContractId into g
                                 select new
                                 {
                                     ContractId = g.Key,
                                     IrregularitesCount = g.Count(),
                                 };

            var irregularitySignals = from irs in this.unitOfWork.DbContext.Set<IrregularitySignal>()
                                      where irs.Status == IrregularitySignalStatus.Active || irs.Status == IrregularitySignalStatus.Ended
                                      group new { irs.IrregularitySignalId, irs.Status } by irs.ContractId into g
                                      select new
                                      {
                                          ContractId = g.Key,
                                          SignalsCount = g.Count(),
                                          SignalsActiveCount = g.Count(t => t.Status == IrregularitySignalStatus.Active),
                                      };

            var financialCorrections = from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                                       join fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>() on fc.FinancialCorrectionId equals fcv.FinancialCorrectionId

                                       where fc.Status == FinancialCorrectionStatus.Entered && fcv.Status == FinancialCorrectionVersionStatus.Actual
                                       group new
                                       {
                                           fcv.EuAmount,
                                           fcv.BgAmount,
                                           fcv.BfpAmount,
                                           fcv.SelfAmount,
                                           fcv.TotalAmount,
                                       }
                                       by fc.ContractId into g
                                       select new
                                       {
                                           ContractId = g.Key,

                                           CorrectedEuAmount = g.Sum(t => t.EuAmount),
                                           CorrectedBgAmount = g.Sum(t => t.BgAmount),
                                           CorrectedBfpAmount = g.Sum(t => t.BfpAmount),
                                           CorrectedSelfAmount = g.Sum(t => t.SelfAmount),
                                           CorrectedTotalAmount = g.Sum(t => t.TotalAmount),
                                       };

            var amountPredicate = PredicateBuilder.True<ContractAmountDO>();
            amountPredicate = amountPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var correctionAmounts = this.GetFinancialCSDBudgetItems()
                .Concat(this.GetFinancialCorrectionCSDs())
                .Concat(this.GetCorrections())
                .Where(t => t.FinancialCorrectionId.HasValue && t.ContractId.HasValue)
                .Where(amountPredicate)
                .GroupBy(t => t.ContractId).Select(t => new
                {
                    ContractId = t.Key,
                    CorrectionTotalAmount = t.Sum(a => a.CorrectedTotalAmount),
                    CorrectionBfpTotalAmount = t.Sum(a => a.CorrectedBfpTotalAmount),
                    CorrectionEuAmount = t.Sum(a => a.CorrectedEuAmount),
                    CorrectionBgAmount = t.Sum(a => a.CorrectedBgAmount),
                    CorrectionSelfAmount = t.Sum(a => a.CorrectedSelfAmount),
                });

            var contracts = from c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(contractPredicate)

                            join apa in actuallyPaidAmounts on c.ContractId equals apa.ContractId into g0
                            from apa in g0.DefaultIfEmpty()

                            join i in irregularities on c.ContractId equals i.ContractId into g1
                            from i in g1.DefaultIfEmpty()

                            join irs in irregularitySignals on c.ContractId equals irs.ContractId into g2
                            from irs in g2.DefaultIfEmpty()

                            join fc in financialCorrections on c.ContractId equals fc.ContractId into g3
                            from fc in g3.DefaultIfEmpty()

                            join ca in correctionAmounts on c.ContractId equals ca.ContractId into g4
                            from ca in g4.DefaultIfEmpty()

                            group new
                            {
                                EuAmount = c.TotalEuAmount,
                                BgAmount = c.TotalBgAmount,
                                BfpAmount = c.TotalBfpAmount,
                                SelfAmount = c.TotalSelfAmount,
                                TotalAmount = c.TotalAmount,

                                ActuallyPaidEuAmount = apa != null ? apa.ActuallyPaidEuAmount : 0m,
                                ActuallyPaidBgAmount = apa != null ? apa.ActuallyPaidBgAmount : 0m,
                                ActuallyPaidBfpAmount = apa != null ? apa.ActuallyPaidBfpAmount : 0m,
                                ActuallyPaidSelfAmount = apa != null ? apa.ActuallyPaidSelfAmount : 0m,
                                ActuallyPaidTotalAmount = apa != null ? apa.ActuallyPaidTotalAmount : 0m,

                                IrregularitySignalsCount = irs != null ? irs.SignalsCount : 0,
                                IrregularitySignalsActiveCount = irs != null ? irs.SignalsActiveCount : 0,
                                IrregularitiesCount = i != null ? i.IrregularitesCount : 0,

                                FinancialCorrectionEuAmount = fc != null ? fc.CorrectedEuAmount : 0m,
                                FinancialCorrectionBgAmount = fc != null ? fc.CorrectedBgAmount : 0m,
                                FinancialCorrectionBfpAmount = fc != null ? fc.CorrectedBfpAmount : 0m,
                                FinancialCorrectionSelfAmount = fc != null ? fc.CorrectedSelfAmount : 0m,
                                FinancialCorrectionTotalAmount = fc != null ? fc.CorrectedTotalAmount : 0m,

                                CorrectionTotalAmount = ca != null ? ca.CorrectionTotalAmount : 0m,
                                CorrectionBfpTotalAmount = ca != null ? ca.CorrectionBfpTotalAmount : 0m,
                                CorrectionEuAmount = ca != null ? ca.CorrectionEuAmount : 0m,
                                CorrectionBgAmount = ca != null ? ca.CorrectionBgAmount : 0m,
                                CorrectionSelfAmount = ca != null ? ca.CorrectionSelfAmount : 0m,
                            }
                            by c.CompanyId into g
                            select new
                            {
                                CompanyId = g.Key,
                                ContractsCount = g.Count(),

                                EuAmount = g.Sum(t => t.EuAmount),
                                BgAmount = g.Sum(t => t.BgAmount),
                                BfpAmount = g.Sum(t => t.BfpAmount),
                                SelfAmount = g.Sum(t => t.SelfAmount),
                                TotalAmount = g.Sum(t => t.TotalAmount),

                                ActuallyPaidEuAmount = g.Sum(t => t.ActuallyPaidEuAmount),
                                ActuallyPaidBgAmount = g.Sum(t => t.ActuallyPaidBgAmount),
                                ActuallyPaidBfpAmount = g.Sum(t => t.ActuallyPaidBfpAmount),
                                ActuallyPaidSelfAmount = g.Sum(t => t.ActuallyPaidSelfAmount),
                                ActuallyPaidTotalAmount = g.Sum(t => t.ActuallyPaidTotalAmount),

                                IrregularitySignalsCount = g.Sum(t => t.IrregularitySignalsCount),
                                IrregularitySignalsActiveCount = g.Sum(t => t.IrregularitySignalsActiveCount),
                                IrregularitiesCount = g.Sum(t => t.IrregularitiesCount),

                                FinancialCorrectionEuAmount = g.Sum(t => t.FinancialCorrectionEuAmount),
                                FinancialCorrectionBgAmount = g.Sum(t => t.FinancialCorrectionBgAmount),
                                FinancialCorrectionBfpAmount = g.Sum(t => t.FinancialCorrectionBfpAmount),
                                FinancialCorrectionSelfAmount = g.Sum(t => t.FinancialCorrectionSelfAmount),
                                FinancialCorrectionTotalAmount = g.Sum(t => t.FinancialCorrectionTotalAmount),

                                CorrectionsTotalAmount = g.Sum(t => t.CorrectionTotalAmount),
                                CorrectionsBfpTotalAmount = g.Sum(t => t.CorrectionBfpTotalAmount),
                                CorrectionsEuAmount = g.Sum(t => t.CorrectionEuAmount),
                                CorrectionsBgAmount = g.Sum(t => t.CorrectionBgAmount),
                                CorrectionsSelfAmount = g.Sum(t => t.CorrectionSelfAmount),
                            };

            var companyPredicate = PredicateBuilder.True<Domain.Companies.Company>();
            companyPredicate = companyPredicate
                .AndEquals(t => t.CompanyTypeId, companyTypeId)
                .AndEquals(t => t.CompanyLegalTypeId, companyLegalTypeId);

            var results = from cmp in this.unitOfWork.DbContext.Set<Eumis.Domain.Companies.Company>().Where(companyPredicate)
                          join ct in this.unitOfWork.DbContext.Set<CompanyType>() on cmp.CompanyTypeId equals ct.CompanyTypeId
                          join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on cmp.CompanyLegalTypeId equals clt.CompanyLegalTypeId

                          join p in projects on cmp.CompanyId equals p.CompanyId into g0
                          from p in g0.DefaultIfEmpty()

                          join c in contracts on cmp.CompanyId equals c.CompanyId into g1
                          from c in g1.DefaultIfEmpty()

                          select new BeneficiaryProjectsContractsReportItem
                          {
                              CompanyName = cmp.Name,
                              CompanyUin = cmp.Uin,
                              CompanyType = ct.Name,
                              CompanyLegalType = clt.Name,
                              ProjectsCount = p != null ? p.ProjectsCount : 0,
                              ProjectsTotalAmount = p != null ? p.TotalAmount / divisor : 0m,
                              ProjectsBfpAmount = p != null ? p.TotalBfpAmount / divisor : 0m,
                              ProjectsSelfAmount = p != null ? p.SelfAmount / divisor : 0m,

                              ContractsCount = c != null ? c.ContractsCount : 0,
                              ContractsTotalAmount = c != null ? c.TotalAmount / divisor : 0m,
                              ContractsEuAmount = c != null ? c.EuAmount / divisor : 0m,
                              ContractsBgAmount = c != null ? c.BgAmount / divisor : 0m,
                              ContractsSelfAmount = c != null ? c.SelfAmount / divisor : 0m,

                              ActuallyPaidTotalAmount = c != null ? c.ActuallyPaidTotalAmount / divisor : 0m,
                              ActuallyPaidEuAmount = c != null ? c.ActuallyPaidEuAmount / divisor : 0m,
                              ActuallyPaidBgAmount = c != null ? c.ActuallyPaidBgAmount / divisor : 0m,

                              IrregularitySignalsCount = c != null ? c.IrregularitySignalsCount : 0,
                              IrregularitySignalsActiveCount = c != null ? c.IrregularitySignalsActiveCount : 0,
                              IrregularitiesCount = c != null ? c.IrregularitiesCount : 0,

                              FinancialCorrectionTotalAmount = c != null ? c.FinancialCorrectionTotalAmount / divisor : 0m,
                              FinancialCorrectionBfpAmount = c != null ? c.FinancialCorrectionBfpAmount / divisor : 0m,
                              FinancialCorrectionSelfAmount = c != null ? c.FinancialCorrectionSelfAmount / divisor : 0m,

                              CorrectionsTotalAmount = c != null ? c.CorrectionsTotalAmount / divisor : 0m,
                              CorrectionsBfpAmount = c != null ? c.CorrectionsBfpTotalAmount / divisor : 0m,
                              CorrectionsSelfAmount = c != null ? c.CorrectionsSelfAmount / divisor : 0m,

                              HasProjects = p != null,
                              HasContracts = c != null,
                          };

            IQueryable<BeneficiaryProjectsContractsReportItem> filteredResults = null;
            if (programmeId.HasValue)
            {
                filteredResults = results.Where(t => t.HasContracts == true);
            }
            else if (procedureId.HasValue || fromDate.HasValue || toDate.HasValue)
            {
                filteredResults = results.Where(t => t.HasContracts == true && t.HasProjects == true);
            }
            else
            {
                filteredResults = results;
            }

            return filteredResults.ToList();
        }

        public IList<EvaluationsReportItem> GetEvaluationsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null)
        {
            var projectPredicate = PredicateBuilder.True<Domain.Projects.Project>();
            projectPredicate = projectPredicate
                .AndEquals(t => t.ProcedureId, procedureId);

            var projectCommunications = from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>()
                                        group new
                                        {
                                            QuestionDate = pc.Question.MessageDate,
                                            AnswerDate = pc.Answers
                                            .Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer)
                                            .Select(a => a.Answer.MessageDate)
                                            .FirstOrDefault(),
                                        }
                                        by pc.ProjectId into g
                                        select new
                                        {
                                            ProjectId = g.Key,
                                            FirstDate = g.Min(t => t.QuestionDate),
                                            LatestDate = g.Max(t => t.AnswerDate),
                                            CommunicationsCount = g.Count(),
                                        };

            var evalSessions = from es in this.unitOfWork.DbContext.Set<EvalSession>()
                               join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on es.EvalSessionId equals esp.EvalSessionId
                               where es.EvalSessionStatus != EvalSessionStatus.Canceled && esp.IsDeleted == false
                               select new
                               {
                                   ProjectId = esp.ProjectId,
                                   es.EvalSessionStatus,
                                   es.CreateDate,
                                   es.ModifyDate,
                               };

            var results = (from p in this.unitOfWork.DbContext.Set<Domain.Projects.Project>().Where(projectPredicate)
                           join apvx in this.unitOfWork.DbContext.Set<ProjectVersionXml>() on p.ProjectId equals apvx.ProjectId
                           join ipvx in this.unitOfWork.DbContext.Set<ProjectVersionXml>() on p.ProjectId equals ipvx.ProjectId

                           join pc in projectCommunications on p.ProjectId equals pc.ProjectId into g0
                           from pc in g0.DefaultIfEmpty()

                           join es in evalSessions on p.ProjectId equals es.ProjectId into g1
                           from es in g1.DefaultIfEmpty()

                           where apvx.Status == ProjectVersionXmlStatus.Actual && ipvx.OrderNum == 1 && ipvx.Status != ProjectVersionXmlStatus.Draft
                           select new
                           {
                               ProjectRegNum = p.RegNumber,
                               Company = p.CompanyName,
                               CompanyUin = p.CompanyUin,
                               InitialProjectTotalAmount = ipvx.TotalBfpAmount + ipvx.CoFinancingAmount,
                               InitialProjectBfpAmount = ipvx.TotalBfpAmount,
                               InitialProjectSelfAmount = ipvx.CoFinancingAmount,
                               ActualProjectTotalAmount = apvx.TotalBfpAmount + apvx.CoFinancingAmount,
                               ActualProjectBfpAmount = apvx.TotalBfpAmount,
                               ActualProjectSelfAmount = apvx.CoFinancingAmount,
                               CommitteeStartDate = es != null ? es.CreateDate : (DateTime?)null,
                               CommitteeEndDate = es != null ? (es.EvalSessionStatus == EvalSessionStatus.Ended ? es.ModifyDate : (DateTime?)null) : (DateTime?)null,
                               FirstCommunicationDate = pc != null ? pc.FirstDate : (DateTime?)null,
                               LatestCommunicationDate = pc != null ? pc.LatestDate : (DateTime?)null,
                               CommunicationsCount = pc != null ? pc.CommunicationsCount : 0,
                           }).ToList();

            return results.Select(t => new EvaluationsReportItem
            {
                ProjectRegNum = t.ProjectRegNum,
                Company = t.Company,
                CompanyUin = t.CompanyUin,
                InitialProjectTotalAmount = t.InitialProjectTotalAmount,
                InitialProjectBfpAmount = t.InitialProjectBfpAmount,
                InitialProjectSelfAmount = t.InitialProjectSelfAmount,
                ActualProjectTotalAmount = t.ActualProjectTotalAmount,
                ActualProjectBfpAmount = t.ActualProjectBfpAmount,
                ActualProjectSelfAmount = t.ActualProjectSelfAmount,
                CommitteeStartDate = t.CommitteeStartDate,
                CommitteeEndDate = t.CommitteeEndDate,
                CommunicationsDuration = (t.LatestCommunicationDate - t.FirstCommunicationDate).HasValue ? (t.LatestCommunicationDate - t.FirstCommunicationDate).Value.Days : 0,
                CommunicationsCount = t.CommunicationsCount,
            }).ToList();
        }

        public IList<ContractReportPaymentsReportItem> GetContractReportPaymentsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null)
        {
            var divisor = 1m;

            if (currency == Currency.Euro)
            {
                divisor = euroExchangeRates;
            }

            var contractPredicate = PredicateBuilder.True<Domain.Contracts.Contract>();
            contractPredicate = contractPredicate
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndEquals(t => t.ProgrammeId, programmeId);

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var paymentPredicate = PredicateBuilder.True<Domain.Contracts.ContractReportPayment>();
            paymentPredicate = paymentPredicate
                .AndDateTimeGreaterThanOrEqual(t => t.RegDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.RegDate, toDate);

            var paymentCheckAmountPredicate = PredicateBuilder.True<ContractReportPaymentCheckAmount>();
            paymentCheckAmountPredicate = paymentCheckAmountPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var paymentCheckAmounts = from crpca in this.unitOfWork.DbContext.Set<ContractReportPaymentCheckAmount>().Where(paymentCheckAmountPredicate)
                                      group new
                                      {
                                          crpca.ApprovedEuAmount,
                                          crpca.ApprovedBgAmount,
                                          crpca.ApprovedBfpTotalAmount,
                                          crpca.ApprovedCrossAmount,
                                          crpca.ApprovedSelfAmount,
                                          crpca.ApprovedTotalAmount,
                                          crpca.PaidEuAmount,
                                          crpca.PaidBgAmount,
                                          crpca.PaidBfpTotalAmount,
                                          crpca.PaidCrossAmount,
                                      }
                                      by crpca.ContractReportPaymentCheckId into g
                                      select new
                                      {
                                          ContractReportPaymentCheckId = g.Key,
                                          ApprovedEuAmount = g.Sum(t => t.ApprovedEuAmount),
                                          ApprovedBgAmount = g.Sum(t => t.ApprovedBgAmount),
                                          ApprovedBfpTotalAmount = g.Sum(t => t.ApprovedBfpTotalAmount),
                                          ApprovedCrossAmount = g.Sum(t => t.ApprovedCrossAmount),
                                          ApprovedSelfAmount = g.Sum(t => t.ApprovedSelfAmount),
                                          ApprovedTotalAmount = g.Sum(t => t.ApprovedTotalAmount),
                                          PaidEuAmount = g.Sum(t => t.PaidEuAmount),
                                          PaidBgAmount = g.Sum(t => t.PaidBgAmount),
                                          PaidBfpTotalAmount = g.Sum(t => t.PaidBfpTotalAmount),
                                          PaidCrossAmount = g.Sum(t => t.PaidCrossAmount),
                                      };

            var amountPredicate = PredicateBuilder.True<ContractAmountDO>();
            amountPredicate = amountPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var certifiedAmounts = this.GetAdvancePaymentAmounts()
                .Concat(this.GetFinancialCSDBudgetItems())
                .Concat(this.GetAttachedFinancialCorrectionCSDs())
                .Where(t => t.ContractReportPaymentId.HasValue)
                .Where(t => !t.AdvancePayment.HasValue || t.AdvancePayment.Value != YesNoNonApplicable.Yes)
                .Where(amountPredicate)
                .GroupBy(t => t.ContractReportPaymentId)
                .Select(t => new
                {
                    ContractReportPaymentId = t.Key,
                    CertifiedTotalAmount = t.Sum(a => a.CertifiedTotalAmount),
                    CertifiedBfpTotalAmount = t.Sum(a => a.CertifiedBfpTotalAmount),
                    CertifiedEuAmount = t.Sum(a => a.CertifiedEuAmount),
                    CertifiedBgAmount = t.Sum(a => a.CertifiedBgAmount),
                    CertifiedSelfAmount = t.Sum(a => a.CertifiedSelfAmount),
                });

            var paidAmountPredicate = PredicateBuilder.True<ActuallyPaidAmount>();
            paidAmountPredicate = paidAmountPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var actuallyPaidAmounts = from apa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(paidAmountPredicate)
                                      where apa.Status == ActuallyPaidAmountStatus.Entered && apa.ContractReportPaymentId.HasValue
                                      group new
                                      {
                                          apa.PaidBfpEuAmount,
                                          apa.PaidBfpBgAmount,
                                          apa.PaidBfpTotalAmount,
                                          apa.PaidSelfAmount,
                                          apa.PaidTotalAmount,
                                      }
                                      by new { apa.ContractReportPaymentId, apa.PaymentDate } into g
                                      select new
                                      {
                                          ContractReportPaymentId = g.Key.ContractReportPaymentId,
                                          PaymentDate = g.Key.PaymentDate,
                                          ActuallyPaidEuAmount = g.Sum(t => t.PaidBfpEuAmount),
                                          ActuallyPaidBgAmount = g.Sum(t => t.PaidBfpBgAmount),
                                          ActuallyPaidBfpAmount = g.Sum(t => t.PaidBfpTotalAmount),
                                          ActuallyPaidSelfAmount = g.Sum(t => t.PaidSelfAmount),
                                          ActuallyPaidTotalAmount = g.Sum(t => t.PaidTotalAmount),
                                      };

            var contractDebtPredicate = PredicateBuilder.True<ContractDebt>();
            contractDebtPredicate = contractDebtPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var debtReimbursedAmounts = from dra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                                        join cd in this.unitOfWork.DbContext.Set<ContractDebt>().Where(contractDebtPredicate) on dra.ContractDebtId equals cd.ContractDebtId
                                        join cdp in this.unitOfWork.DbContext.Set<ContractDebtPayment>() on cd.ContractDebtId equals cdp.ContractDebtId
                                        where dra.Status == ReimbursedAmountStatus.Entered
                                        group new
                                        {
                                            ReimbursedTotalAmount = dra.PrincipalBfp.TotalAmount,
                                            dra.ReimbursementDate,
                                        }
                                        by cdp.ContractReportPaymentId into g
                                        select new
                                        {
                                            ContractReportPaymentId = g.Key,
                                            ReimbursementDate = g.Max(t => t.ReimbursementDate),
                                            ReimbursedTotalAmount = g.Sum(t => t.ReimbursedTotalAmount),
                                        };

            var contractReimbursedAmountPredicate = PredicateBuilder.True<ContractReimbursedAmount>();
            contractReimbursedAmountPredicate = contractReimbursedAmountPredicate
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

            var contractReimbursedAmounts = from cra in this.unitOfWork.DbContext.Set<ContractReimbursedAmount>().Where(contractReimbursedAmountPredicate)
                                            join crap in this.unitOfWork.DbContext.Set<ContractReimbursedAmountPayment>() on cra.ReimbursedAmountId equals crap.ReimbursedAmountId
                                            where cra.Status == ReimbursedAmountStatus.Entered
                                            group new
                                            {
                                                ReimbursedTotalAmount = cra.PrincipalBfp.TotalAmount,
                                                cra.ReimbursementDate,
                                            }
                                            by crap.ContractReportPaymentId into g
                                            select new
                                            {
                                                ContractReportPaymentId = g.Key,
                                                ReimbursementDate = g.Max(t => t.ReimbursementDate),
                                                ReimbursedTotalAmount = g.Sum(t => t.ReimbursedTotalAmount),
                                            };

            var reimbursedAmounts = debtReimbursedAmounts
                .Concat(contractReimbursedAmounts)
                .GroupBy(t => t.ContractReportPaymentId)
                .Select(t => new
                {
                    ContractReportPaymentId = t.Key,
                    ReimbursementDate = t.Max(p => p.ReimbursementDate),
                    ReimbursedTotalAmount = t.Sum(p => p.ReimbursedTotalAmount),
                });

            return (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>().Where(paymentPredicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(contractPredicate) on crp.ContractId equals c.ContractId

                    join crpc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>().Where(t => t.Status == ContractReportPaymentCheckStatus.Active) on crp.ContractReportPaymentId equals crpc.ContractReportPaymentId into g0
                    from crpc in g0.DefaultIfEmpty()

                    join crpca in paymentCheckAmounts on crpc.ContractReportPaymentCheckId equals crpca.ContractReportPaymentCheckId into g1
                    from crpca in g1.DefaultIfEmpty()

                    join ca in certifiedAmounts on crp.ContractReportPaymentId equals ca.ContractReportPaymentId into g2
                    from ca in g2.DefaultIfEmpty()

                    join apa in actuallyPaidAmounts on crp.ContractReportPaymentId equals apa.ContractReportPaymentId into g3
                    from apa in g3.DefaultIfEmpty()

                    join ra in reimbursedAmounts on crp.ContractReportPaymentId equals ra.ContractReportPaymentId into g4
                    from ra in g4.DefaultIfEmpty()

                    where crp.Status == ContractReportPaymentStatus.Actual
                    orderby c.RegNumber
                    select new ContractReportPaymentsReportItem
                    {
                        ContractRegNum = c.RegNumber,
                        ReportNum = cr.OrderNum,
                        PaymentNum = crp.VersionNum,
                        CompanyUin = c.CompanyUin,
                        CompanyName = c.CompanyName,
                        RegDate = crp.RegDate,
                        PaymentType = crp.PaymentType,
                        PaymentStatus = cr.Status,
                        PaymentTotalAmount = crp.TotalAmount,
                        PaymentApprovedAmount = crpca != null ? (crpca.ApprovedBfpTotalAmount / divisor) : null,
                        PaymentPaidAmount = crpca != null ? (crpca.PaidBfpTotalAmount / divisor) : null,
                        PaymentCheckDate = crpc != null ? crpc.CheckedDate : (DateTime?)null,
                        PaymentCertifiedAmount = ca != null ? (ca.CertifiedBfpTotalAmount / divisor) : null,
                        PaymentActuallyPaidAmount = apa != null ? (apa.ActuallyPaidBfpAmount / divisor) : null,
                        PaymentActuallyPaidDate = apa != null ? apa.PaymentDate : (DateTime?)null,
                        PaymentReimbursedAmount = ra != null ? (ra.ReimbursedTotalAmount / divisor) : null,
                        PaymentReimbursementDate = ra != null ? ra.ReimbursementDate : (DateTime?)null,
                    }).ToList();
        }

        public IList<ProgrammeSummaryReportItem> GetProgrammeSummaryReport(
            GroupingLevel groupingLevel,
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            var divisor = 1m;

            if (currency == Currency.Euro)
            {
                divisor = euroExchangeRates;
            }

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var contractPredicate = PredicateBuilder.True<Domain.Contracts.Contract>();
            contractPredicate = contractPredicate
                .AndEquals(t => t.ProgrammeId, programmeId)
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndDateTimeGreaterThanOrEqual(t => t.ContractDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.ContractDate, toDate);

            IQueryable<Domain.Contracts.Contract> contracts = this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(contractPredicate);

            string fullPath = this.GetNutsFullPath(
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);

            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                contracts = from c in contracts
                            join cl in this.unitOfWork.DbContext.Set<ContractLocation>() on c.ContractId equals cl.ContractId
                            where cl.FullPath.Contains(fullPath)
                            select c;
            }

            var amountPredicate = PredicateBuilder.True<ContractAmountDO>();
            amountPredicate = amountPredicate
                .AndEquals(t => t.ProgrammeId, programmeId)
                .AndEquals(t => t.ProgrammePriorityId, programmePriorityId)
                .AndEquals(t => t.ProcedureId, procedureId);

            var initialAmounts = this.GetActualContractContractedAmounts()
                .Concat(this.GetAdvancePaymentAmounts())
                .Concat(this.GetFinancialCSDBudgetItems())
                .Concat(this.GetCorrections())
                .Concat(this.GetFinancialCorrectionCSDs())
                .Concat(this.GetCertCorrections())
                .Concat(this.GetFinancialCertCorrectionCSDs())
                .Concat(this.GetRevalidations())
                .Concat(this.GetFinancialRevalidationCSDs())
                .Where(amountPredicate)
                .Where(t => !t.AdvancePayment.HasValue || t.AdvancePayment.Value != YesNoNonApplicable.Yes);

            IQueryable<ContractAmountDO> amounts = null;
            if (!string.IsNullOrWhiteSpace(fullPath) || groupingLevel == GroupingLevel.Contract || fromDate.HasValue || toDate.HasValue)
            {
                amounts = initialAmounts.Where(t => t.ContractId.HasValue && contracts.Select(p => p.ContractId).Contains(t.ContractId.Value));
            }
            else
            {
                amounts = initialAmounts;
            }

            Func<IQueryable<IGrouping<ProgrammeSummaryReportAmountsGroupingItem, ContractAmountDO>>, IQueryable<ProgrammeSummaryReportAmountsItem>> amountsMaker = (ca) =>
            {
                return ca
                .Select(t => new ProgrammeSummaryReportAmountsItem
                {
                    ProgrammeId = t.Key.ProgrammeId,
                    ProgrammePriorityId = t.Key.ProgrammePriorityId,
                    ProcedureId = t.Key.ProcedureId,
                    ContractId = t.Key.ContractId,

                    ContractedEuAmount = t.Sum(p => p.ContractedEuAmount),
                    ContractedBgAmount = t.Sum(p => p.ContractedBgAmount),
                    ContractedBfpTotalAmount = t.Sum(p => p.ContractedBfpTotalAmount),
                    ContractedSelfAmount = t.Sum(p => p.ContractedSelfAmount),
                    ContractedTotalAmount = t.Sum(p => p.ContractedTotalAmount),

                    ReportedEuAmount = t.Sum(p => p.ReportedEuAmount),
                    ReportedBgAmount = t.Sum(p => p.ReportedBgAmount),
                    ReportedBfpTotalAmount = t.Sum(p => p.ReportedBfpTotalAmount),
                    ReportedSelfAmount = t.Sum(p => p.ReportedSelfAmount),
                    ReportedTotalAmount = t.Sum(p => p.ReportedTotalAmount),

                    ApprovedEuAmount = t.Sum(p => p.ApprovedEuAmount),
                    ApprovedBgAmount = t.Sum(p => p.ApprovedBgAmount),
                    ApprovedBfpTotalAmount = t.Sum(p => p.ApprovedBfpTotalAmount),
                    ApprovedSelfAmount = t.Sum(p => p.ApprovedSelfAmount),
                    ApprovedTotalAmount = t.Sum(p => p.ApprovedTotalAmount),

                    CertifiedEuAmount = t.Sum(p => p.CertifiedEuAmount),
                    CertifiedBgAmount = t.Sum(p => p.CertifiedBgAmount),
                    CertifiedBfpTotalAmount = t.Sum(p => p.CertifiedBfpTotalAmount),
                    CertifiedSelfAmount = t.Sum(p => p.CertifiedSelfAmount),
                    CertifiedTotalAmount = t.Sum(p => p.CertifiedTotalAmount),
                });
            };

            var mapNodeBudgetPredicate = PredicateBuilder.True<MapNodeBudget>();
            mapNodeBudgetPredicate = mapNodeBudgetPredicate
                .AndEquals(t => t.ProgrammeId, programmeId)
                .AndEquals(t => t.MapNodeId, programmePriorityId);

            var budgets = from mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>().Where(mapNodeBudgetPredicate)
                          group new
                          {
                              mnb.BgAmount,
                              mnb.EuAmount,
                          }
                          by new
                          {
                              ProgrammeId = mnb.ProgrammeId,
                              ProgrammePriorityId = mnb.MapNodeId,
                          }
                          into g
                          select new
                          {
                              ProgrammeId = g.Key.ProgrammeId,
                              ProgrammePriorityId = g.Key.ProgrammePriorityId,
                              BgAmount = g.Sum(t => t.BgAmount),
                              EuAmount = g.Sum(t => t.EuAmount),
                          };

            IQueryable<ProgrammeSummaryReportItem> results = null;
            if (groupingLevel == GroupingLevel.Contract)
            {
                var groupedAmounts = amountsMaker(amounts
                .GroupBy(t => new ProgrammeSummaryReportAmountsGroupingItem
                {
                    ProgrammeId = t.ProgrammeId,
                    ProcedureId = t.ProcedureId,
                    ContractId = t.ContractId,
                    ProgrammePriorityId = t.ProgrammePriorityId,
                }));

                var actuallyPaidAmounts = from apa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                                          join c in contracts on apa.ContractId equals c.ContractId
                                          where apa.Status == ActuallyPaidAmountStatus.Entered
                                          group new
                                          {
                                              apa.PaidBfpEuAmount,
                                              apa.PaidBfpBgAmount,
                                              apa.PaidBfpTotalAmount,
                                              apa.PaidSelfAmount,
                                              apa.PaidTotalAmount,
                                          }
                                          by new
                                          {
                                              apa.ProgrammeId,
                                              c.ProcedureId,
                                              apa.ContractId,
                                              apa.ProgrammePriorityId,
                                          }
                                          into g
                                          select new
                                          {
                                              ProgrammeId = g.Key.ProgrammeId,
                                              ProgrammePriorityId = g.Key.ProgrammePriorityId,
                                              ProcedureId = g.Key.ProcedureId,
                                              ContractId = g.Key.ContractId,

                                              ActuallyPaidEuAmount = g.Sum(t => t.PaidBfpEuAmount),
                                              ActuallyPaidBgAmount = g.Sum(t => t.PaidBfpBgAmount),
                                              ActuallyPaidBfpTotalAmount = g.Sum(t => t.PaidBfpTotalAmount),
                                              ActuallyPaidSelfAmount = g.Sum(t => t.PaidSelfAmount),
                                              ActuallyPaidTotalAmount = g.Sum(t => t.PaidTotalAmount),
                                          };

                results = from a in groupedAmounts
                          join p in this.unitOfWork.DbContext.Set<Programme>() on a.ProgrammeId equals p.MapNodeId
                          join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on a.ProgrammePriorityId equals pp.MapNodeId
                          join pr in this.unitOfWork.DbContext.Set<Procedure>() on a.ProcedureId equals pr.ProcedureId
                          join c in contracts on a.ContractId equals c.ContractId
                          join b in budgets on new { ProgrammeId = a.ProgrammeId.Value, ProgrammePriorityId = a.ProgrammePriorityId.Value } equals
                                               new { b.ProgrammeId, b.ProgrammePriorityId }

                          join apa in actuallyPaidAmounts on new { ProgrammeId = a.ProgrammeId.Value, ProcedureId = a.ProcedureId.Value, ContractId = a.ContractId.Value, ProgrammePriorityId = a.ProgrammePriorityId.Value } equals
                                                             new { apa.ProgrammeId, apa.ProcedureId, apa.ContractId, apa.ProgrammePriorityId } into g1
                          from apa in g1.DefaultIfEmpty()
                          orderby c.RegNumber
                          select new ProgrammeSummaryReportItem
                          {
                              ProgrammeName = p.Name,
                              ProgrammePriorityName = pp.Name,
                              ProcedureName = pr != null ? pr.Name : null,
                              ContractRegNum = c != null ? c.RegNumber : null,
                              ProgrammeBudgetEuAmount = b.EuAmount,
                              ProgrammeBudgetBgAmount = b.BgAmount,
                              ProgrammeBudgetBfpTotalAmount = b.EuAmount + b.BgAmount,

                              AreAmountsNull = false,

                              ContractedEuAmount = a.ContractedEuAmount ?? 0m,
                              ContractedBgAmount = a.ContractedBgAmount ?? 0m,
                              ContractedSelfAmount = a.ContractedSelfAmount ?? 0m,
                              ContractedTotalAmount = a.ContractedTotalAmount ?? 0m,
                              ReportedEuAmount = a.ReportedEuAmount ?? 0m,
                              ReportedBgAmount = a.ReportedBgAmount ?? 0m,
                              ReportedSelfAmount = a.ReportedSelfAmount ?? 0m,
                              ReportedTotalAmount = a.ReportedTotalAmount ?? 0m,
                              ActuallyPaidEuAmount = apa != null ? apa.ActuallyPaidEuAmount : 0m,
                              ActuallyPaidBgAmount = apa != null ? apa.ActuallyPaidBgAmount : 0m,
                              ActuallyPaidSelfAmount = apa != null ? apa.ActuallyPaidSelfAmount : 0m,
                              ActuallyPaidTotalAmount = apa != null ? apa.ActuallyPaidTotalAmount : 0m,
                              ApprovedEuAmount = a.ApprovedEuAmount ?? 0m,
                              ApprovedBgAmount = a.ApprovedBgAmount ?? 0m,
                              ApprovedSelfAmount = a.ApprovedSelfAmount ?? 0m,
                              ApprovedTotalAmount = a.ApprovedTotalAmount ?? 0m,
                              CertifiedEuAmount = a.CertifiedEuAmount ?? 0m,
                              CertifiedBgAmount = a.CertifiedBgAmount ?? 0m,
                              CertifiedSelfAmount = a.CertifiedSelfAmount ?? 0m,
                              CertifiedTotalAmount = a.CertifiedTotalAmount ?? 0m,
                          };
            }
            else if (groupingLevel == GroupingLevel.Procedure)
            {
                var procedureSharePredicate = PredicateBuilder.True<ProcedureShare>();
                procedureSharePredicate = procedureSharePredicate
                    .AndEquals(t => t.ProcedureId, procedureId)
                    .AndEquals(t => t.ProgrammeId, programmeId)
                    .AndEquals(t => t.ProgrammePriorityId, programmePriorityId);

                var groupedAmounts = amountsMaker(amounts
                .GroupBy(t => new ProgrammeSummaryReportAmountsGroupingItem
                {
                    ProgrammeId = t.ProgrammeId,
                    ProcedureId = t.ProcedureId,
                    ContractId = null,
                    ProgrammePriorityId = t.ProgrammePriorityId,
                }));

                var actuallyPaidAmounts = from apa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                                          join c in contracts on apa.ContractId equals c.ContractId
                                          where apa.Status == ActuallyPaidAmountStatus.Entered
                                          group new
                                          {
                                              apa.PaidBfpEuAmount,
                                              apa.PaidBfpBgAmount,
                                              apa.PaidBfpTotalAmount,
                                              apa.PaidSelfAmount,
                                              apa.PaidTotalAmount,
                                          }
                                           by new
                                           {
                                               apa.ProgrammeId,
                                               c.ProcedureId,
                                               apa.ProgrammePriorityId,
                                           }
                                            into g
                                          select new
                                          {
                                              ProgrammeId = g.Key.ProgrammeId,
                                              ProgrammePriorityId = g.Key.ProgrammePriorityId,
                                              ProcedureId = g.Key.ProcedureId,

                                              ActuallyPaidEuAmount = g.Sum(t => t.PaidBfpEuAmount),
                                              ActuallyPaidBgAmount = g.Sum(t => t.PaidBfpBgAmount),
                                              ActuallyPaidBfpTotalAmount = g.Sum(t => t.PaidBfpTotalAmount),
                                              ActuallyPaidSelfAmount = g.Sum(t => t.PaidSelfAmount),
                                              ActuallyPaidTotalAmount = g.Sum(t => t.PaidTotalAmount),
                                          };

                results =
                    from p in this.unitOfWork.DbContext.Set<Procedure>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(procedureSharePredicate) on p.ProcedureId equals ps.ProcedureId
                    join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                    join b in budgets on new { ps.ProgrammeId, ps.ProgrammePriorityId } equals new { b.ProgrammeId, b.ProgrammePriorityId }

                    join a in groupedAmounts on new { ps.ProgrammeId, ps.ProcedureId, ps.ProgrammePriorityId } equals new { ProgrammeId = a.ProgrammeId.Value, ProcedureId = a.ProcedureId.Value, ProgrammePriorityId = a.ProgrammePriorityId.Value } into g0
                    from a in g0.DefaultIfEmpty()

                    join apa in actuallyPaidAmounts on new { ps.ProgrammeId, ps.ProcedureId, ps.ProgrammePriorityId } equals
                                                       new { apa.ProgrammeId, apa.ProcedureId, apa.ProgrammePriorityId } into g1
                    from apa in g1.DefaultIfEmpty()

                    orderby p.Name
                    select new ProgrammeSummaryReportItem
                    {
                        ProgrammeName = prog.Name,
                        ProcedureName = p.Name,
                        ProgrammePriorityName = pp.Name,
                        ProgrammeBudgetEuAmount = b.EuAmount,
                        ProgrammeBudgetBgAmount = b.BgAmount,
                        ProgrammeBudgetBfpTotalAmount = b.EuAmount + b.BgAmount,

                        AreAmountsNull = a == null,

                        ContractedEuAmount = a.ContractedEuAmount ?? 0m,
                        ContractedBgAmount = a.ContractedBgAmount ?? 0m,
                        ContractedSelfAmount = a.ContractedSelfAmount ?? 0m,
                        ContractedTotalAmount = a.ContractedTotalAmount ?? 0m,
                        ReportedEuAmount = a.ReportedEuAmount ?? 0m,
                        ReportedBgAmount = a.ReportedBgAmount ?? 0m,
                        ReportedSelfAmount = a.ReportedSelfAmount ?? 0m,
                        ReportedTotalAmount = a.ReportedTotalAmount ?? 0m,
                        ActuallyPaidEuAmount = apa.ActuallyPaidEuAmount ?? 0m,
                        ActuallyPaidBgAmount = apa.ActuallyPaidBgAmount ?? 0m,
                        ActuallyPaidSelfAmount = apa.ActuallyPaidSelfAmount ?? 0m,
                        ActuallyPaidTotalAmount = apa.ActuallyPaidTotalAmount ?? 0m,
                        ApprovedEuAmount = a.ApprovedEuAmount ?? 0m,
                        ApprovedBgAmount = a.ApprovedBgAmount ?? 0m,
                        ApprovedSelfAmount = a.ApprovedSelfAmount ?? 0m,
                        ApprovedTotalAmount = a.ApprovedTotalAmount ?? 0m,
                        CertifiedEuAmount = a.CertifiedEuAmount ?? 0m,
                        CertifiedBgAmount = a.CertifiedBgAmount ?? 0m,
                        CertifiedSelfAmount = a.CertifiedSelfAmount ?? 0m,
                        CertifiedTotalAmount = a.CertifiedTotalAmount ?? 0m,
                    };
            }
            else if (groupingLevel == GroupingLevel.ProgrammePriority)
            {
                var groupedAmounts = amountsMaker(amounts
                .GroupBy(t => new ProgrammeSummaryReportAmountsGroupingItem
                {
                    ProgrammeId = t.ProgrammeId,
                    ProcedureId = null,
                    ContractId = null,
                    ProgrammePriorityId = t.ProgrammePriorityId,
                }));

                var actuallyPaidAmounts = from apa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                                          where apa.Status == ActuallyPaidAmountStatus.Entered
                                          group new
                                          {
                                              apa.PaidBfpEuAmount,
                                              apa.PaidBfpBgAmount,
                                              apa.PaidBfpTotalAmount,
                                              apa.PaidSelfAmount,
                                              apa.PaidTotalAmount,
                                          }
                                           by new
                                           {
                                               apa.ProgrammeId,
                                               apa.ProgrammePriorityId,
                                           }
                                            into g
                                          select new
                                          {
                                              ProgrammeId = g.Key.ProgrammeId,
                                              ProgrammePriorityId = g.Key.ProgrammePriorityId,

                                              ActuallyPaidEuAmount = g.Sum(t => t.PaidBfpEuAmount),
                                              ActuallyPaidBgAmount = g.Sum(t => t.PaidBfpBgAmount),
                                              ActuallyPaidBfpTotalAmount = g.Sum(t => t.PaidBfpTotalAmount),
                                              ActuallyPaidSelfAmount = g.Sum(t => t.PaidSelfAmount),
                                              ActuallyPaidTotalAmount = g.Sum(t => t.PaidTotalAmount),
                                          };

                results =
                    from b in budgets
                    join p in this.unitOfWork.DbContext.Set<Programme>() on b.ProgrammeId equals p.MapNodeId
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on b.ProgrammePriorityId equals pp.MapNodeId

                    join a in groupedAmounts on new { b.ProgrammeId, b.ProgrammePriorityId } equals new { ProgrammeId = a.ProgrammeId.Value, ProgrammePriorityId = a.ProgrammePriorityId.Value } into g0
                    from a in g0.DefaultIfEmpty()

                    join apa in actuallyPaidAmounts on new { b.ProgrammeId, b.ProgrammePriorityId } equals
                                                       new { apa.ProgrammeId, apa.ProgrammePriorityId } into g1
                    from apa in g1.DefaultIfEmpty()

                    orderby p.Name, pp.Name
                    select new ProgrammeSummaryReportItem
                    {
                        ProgrammeName = p.Name,
                        ProgrammePriorityName = pp.Name,
                        ProgrammeBudgetEuAmount = b.EuAmount,
                        ProgrammeBudgetBgAmount = b.BgAmount,
                        ProgrammeBudgetBfpTotalAmount = b.EuAmount + b.BgAmount,

                        AreAmountsNull = a == null,

                        ContractedEuAmount = a.ContractedEuAmount ?? 0m,
                        ContractedBgAmount = a.ContractedBgAmount ?? 0m,
                        ContractedSelfAmount = a.ContractedSelfAmount ?? 0m,
                        ContractedTotalAmount = a.ContractedTotalAmount ?? 0m,
                        ReportedEuAmount = a.ReportedEuAmount ?? 0m,
                        ReportedBgAmount = a.ReportedBgAmount ?? 0m,
                        ReportedSelfAmount = a.ReportedSelfAmount ?? 0m,
                        ReportedTotalAmount = a.ReportedTotalAmount ?? 0m,
                        ActuallyPaidEuAmount = apa.ActuallyPaidEuAmount ?? 0m,
                        ActuallyPaidBgAmount = apa.ActuallyPaidBgAmount ?? 0m,
                        ActuallyPaidSelfAmount = apa.ActuallyPaidSelfAmount ?? 0m,
                        ActuallyPaidTotalAmount = apa.ActuallyPaidTotalAmount ?? 0m,
                        ApprovedEuAmount = a.ApprovedEuAmount ?? 0m,
                        ApprovedBgAmount = a.ApprovedBgAmount ?? 0m,
                        ApprovedSelfAmount = a.ApprovedSelfAmount ?? 0m,
                        ApprovedTotalAmount = a.ApprovedTotalAmount ?? 0m,
                        CertifiedEuAmount = a.CertifiedEuAmount ?? 0m,
                        CertifiedBgAmount = a.CertifiedBgAmount ?? 0m,
                        CertifiedSelfAmount = a.CertifiedSelfAmount ?? 0m,
                        CertifiedTotalAmount = a.CertifiedTotalAmount ?? 0m,
                    };
            }
            else if (groupingLevel == GroupingLevel.Programme)
            {
                var programmeBudgets = from mnb in this.unitOfWork.DbContext.Set<MapNodeBudget>().Where(mapNodeBudgetPredicate)
                                       group new
                                       {
                                           mnb.BgAmount,
                                           mnb.EuAmount,
                                       }
                                       by new
                                       {
                                           mnb.ProgrammeId,
                                       }
                                        into g
                                       select new
                                       {
                                           ProgrammeId = g.Key.ProgrammeId,
                                           BgAmount = g.Sum(t => t.BgAmount),
                                           EuAmount = g.Sum(t => t.EuAmount),
                                       };

                var groupedAmounts = amountsMaker(amounts
                .GroupBy(t => new ProgrammeSummaryReportAmountsGroupingItem
                {
                    ProgrammeId = t.ProgrammeId,
                    ProcedureId = null,
                    ContractId = null,
                    ProgrammePriorityId = null,
                }));

                var actuallyPaidAmounts = from apa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                                          where apa.Status == ActuallyPaidAmountStatus.Entered
                                          group new
                                          {
                                              apa.PaidBfpEuAmount,
                                              apa.PaidBfpBgAmount,
                                              apa.PaidBfpTotalAmount,
                                              apa.PaidSelfAmount,
                                              apa.PaidTotalAmount,
                                          }
                                           by new
                                           {
                                               apa.ProgrammeId,
                                           }
                                            into g
                                          select new
                                          {
                                              ProgrammeId = g.Key.ProgrammeId,

                                              ActuallyPaidEuAmount = g.Sum(t => t.PaidBfpEuAmount),
                                              ActuallyPaidBgAmount = g.Sum(t => t.PaidBfpBgAmount),
                                              ActuallyPaidBfpTotalAmount = g.Sum(t => t.PaidBfpTotalAmount),
                                              ActuallyPaidSelfAmount = g.Sum(t => t.PaidSelfAmount),
                                              ActuallyPaidTotalAmount = g.Sum(t => t.PaidTotalAmount),
                                          };

                results =
                    from b in programmeBudgets
                    join p in this.unitOfWork.DbContext.Set<Programme>() on b.ProgrammeId equals p.MapNodeId

                    join a in groupedAmounts on new { b.ProgrammeId } equals new { ProgrammeId = a.ProgrammeId.Value } into g0
                    from a in g0.DefaultIfEmpty()

                    join apa in actuallyPaidAmounts on new { b.ProgrammeId } equals new { apa.ProgrammeId } into g1
                    from apa in g1.DefaultIfEmpty()

                    orderby p.Name
                    select new ProgrammeSummaryReportItem
                    {
                        ProgrammeName = p.Name,
                        ProgrammePriorityName = null,
                        ProcedureName = null,
                        ContractRegNum = null,
                        ProgrammeBudgetEuAmount = b.EuAmount,
                        ProgrammeBudgetBgAmount = b.BgAmount,
                        ProgrammeBudgetBfpTotalAmount = b.EuAmount + b.BgAmount,

                        AreAmountsNull = a == null,

                        ContractedEuAmount = a.ContractedEuAmount ?? 0m,
                        ContractedBgAmount = a.ContractedBgAmount ?? 0m,
                        ContractedSelfAmount = a.ContractedSelfAmount ?? 0m,
                        ContractedTotalAmount = a.ContractedTotalAmount ?? 0m,
                        ReportedEuAmount = a.ReportedEuAmount ?? 0m,
                        ReportedBgAmount = a.ReportedBgAmount ?? 0m,
                        ReportedSelfAmount = a.ReportedSelfAmount ?? 0m,
                        ReportedTotalAmount = a.ReportedTotalAmount ?? 0m,
                        ActuallyPaidEuAmount = apa.ActuallyPaidEuAmount ?? 0m,
                        ActuallyPaidBgAmount = apa.ActuallyPaidBgAmount ?? 0m,
                        ActuallyPaidSelfAmount = apa.ActuallyPaidSelfAmount ?? 0m,
                        ActuallyPaidTotalAmount = apa.ActuallyPaidTotalAmount ?? 0m,
                        ApprovedEuAmount = a.ApprovedEuAmount ?? 0m,
                        ApprovedBgAmount = a.ApprovedBgAmount ?? 0m,
                        ApprovedSelfAmount = a.ApprovedSelfAmount ?? 0m,
                        ApprovedTotalAmount = a.ApprovedTotalAmount ?? 0m,
                        CertifiedEuAmount = a.CertifiedEuAmount ?? 0m,
                        CertifiedBgAmount = a.CertifiedBgAmount ?? 0m,
                        CertifiedSelfAmount = a.CertifiedSelfAmount ?? 0m,
                        CertifiedTotalAmount = a.CertifiedTotalAmount ?? 0m,
                    };
            }
            else
            {
                throw new DataException("GroupingLevel not recognized");
            }

            IQueryable<ProgrammeSummaryReportItem> filteredResults = null;
            if (!string.IsNullOrWhiteSpace(fullPath) || fromDate.HasValue || toDate.HasValue)
            {
                filteredResults = results.Where(t => t.AreAmountsNull == false);
            }
            else
            {
                filteredResults = results;
            }

            return filteredResults.ToList().Select(t => new ProgrammeSummaryReportItem
            {
                ProgrammeName = t.ProgrammeName,
                ProgrammePriorityName = t.ProgrammePriorityName,
                ProcedureName = t.ProcedureName,
                ContractRegNum = t.ContractRegNum,
                ProgrammeBudgetEuAmount = t.ProgrammeBudgetEuAmount / divisor,
                ProgrammeBudgetBgAmount = t.ProgrammeBudgetBgAmount / divisor,
                ProgrammeBudgetBfpTotalAmount = t.ProgrammeBudgetBfpTotalAmount / divisor,

                ContractedEuAmount = t.ContractedEuAmount / divisor,
                ContractedBgAmount = t.ContractedBgAmount / divisor,
                ContractedSelfAmount = t.ContractedSelfAmount / divisor,
                ContractedTotalAmount = t.ContractedTotalAmount / divisor,
                ReportedEuAmount = t.ReportedEuAmount / divisor,
                ReportedBgAmount = t.ReportedBgAmount / divisor,
                ReportedSelfAmount = t.ReportedSelfAmount / divisor,
                ReportedTotalAmount = t.ReportedTotalAmount / divisor,
                ActuallyPaidEuAmount = t.ActuallyPaidEuAmount / divisor,
                ActuallyPaidBgAmount = t.ActuallyPaidBgAmount / divisor,
                ActuallyPaidSelfAmount = t.ActuallyPaidSelfAmount / divisor,
                ActuallyPaidTotalAmount = t.ActuallyPaidTotalAmount / divisor,
                ApprovedEuAmount = t.ApprovedEuAmount / divisor,
                ApprovedBgAmount = t.ApprovedBgAmount / divisor,
                ApprovedSelfAmount = t.ApprovedSelfAmount / divisor,
                ApprovedTotalAmount = t.ApprovedTotalAmount / divisor,
                CertifiedEuAmount = t.CertifiedEuAmount / divisor,
                CertifiedBgAmount = t.CertifiedBgAmount / divisor,
                CertifiedSelfAmount = t.CertifiedSelfAmount / divisor,
                CertifiedTotalAmount = t.CertifiedTotalAmount / divisor,
            }).ToList();
        }

        public IList<IrregularitiesReportItem> GetIrregularitiesReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var psPredicate = PredicateBuilder.True<ProcedureShare>()
                .AndEquals(ps => ps.ProcedureId, procedureId)
                .AndEquals(ps => ps.ProgrammeId, programmeId)
                .AndEquals(ps => ps.ProgrammePriorityId, programmePriorityId);

            var procedureShares = this.unitOfWork.DbContext.Set<ProcedureShare>().Where(psPredicate);

            var contracts =
                from c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>()

                join ct in this.unitOfWork.DbContext.Set<CompanyType>() on c.CompanyTypeId equals ct.CompanyTypeId into g0
                from ct in g0.DefaultIfEmpty()

                join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on c.CompanyLegalTypeId equals clt.CompanyLegalTypeId into g1
                from clt in g1.DefaultIfEmpty()

                join co1 in this.unitOfWork.DbContext.Set<Country>() on c.BeneficiarySeatCountryId equals co1.CountryId into g2
                from co1 in g2.DefaultIfEmpty()

                join s1 in this.unitOfWork.DbContext.Set<Settlement>() on c.BeneficiarySeatSettlementId equals s1.SettlementId into g3
                from s1 in g3.DefaultIfEmpty()

                join co2 in this.unitOfWork.DbContext.Set<Country>() on c.BeneficiaryCorrespondenceCountryId equals co2.CountryId into g4
                from co2 in g4.DefaultIfEmpty()

                join s2 in this.unitOfWork.DbContext.Set<Settlement>() on c.BeneficiaryCorrespondenceSettlementId equals s2.SettlementId into g5
                from s2 in g5.DefaultIfEmpty()

                where procedureShares.Where(ps => ps.ProcedureId == c.ProcedureId && ps.ProgrammeId == c.ProgrammeId).Any()

                select new
                {
                    Id = c.ContractId,
                    Project = c.Name,
                    ContractRegNum = c.RegNumber,

                    BeneficiaryName = c.CompanyName,
                    BeneficiaryUin = c.CompanyUin,
                    BeneficiaryType = ct.Name,
                    BeneficiaryLegalType = clt.Name,

                    BeneficiarySeatCountryCode = co1.NutsCode,
                    BeneficiarySeatCountry = co1.Name,
                    BeneficiarySeatSettlement = s1.Name,
                    BeneficiarySeatPostCode = c.BeneficiarySeatPostCode,
                    BeneficiarySeatStreet = c.BeneficiarySeatStreet,
                    BeneficiarySeatAddress = c.BeneficiarySeatAddress,

                    BeneficiaryCorrespondenceCountryCode = co2.NutsCode,
                    BeneficiaryCorrespondenceCountry = co2.Name,
                    BeneficiaryCorrespondenceSettlement = s2.Name,
                    BeneficiaryCorrespondencePostCode = c.BeneficiaryCorrespondencePostCode,
                    BeneficiaryCorrespondenceStreet = c.BeneficiaryCorrespondenceStreet,
                    BeneficiaryCorrespondenceAddress = c.BeneficiaryCorrespondenceAddress,
                };

            var projects =
                from p in this.unitOfWork.DbContext.Set<Domain.Projects.Project>()

                join pct in this.unitOfWork.DbContext.Set<CompanyType>() on p.CompanyTypeId equals pct.CompanyTypeId into gpct
                from pct in gpct.DefaultIfEmpty()

                join pclt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on p.CompanyLegalTypeId equals pclt.CompanyLegalTypeId into gpclt
                from pclt in gpclt.DefaultIfEmpty()

                join pco1 in this.unitOfWork.DbContext.Set<Country>() on p.CompanySeatCountryId equals pco1.CountryId into gpco1
                from pco1 in gpco1.DefaultIfEmpty()

                join ps1 in this.unitOfWork.DbContext.Set<Settlement>() on p.CompanySeatSettlementId equals ps1.SettlementId into gps1
                from ps1 in gps1.DefaultIfEmpty()

                join pco2 in this.unitOfWork.DbContext.Set<Country>() on p.CompanyCorrespondenceCountryId equals pco2.CountryId into gpco2
                from pco2 in gpco2.DefaultIfEmpty()

                join ps2 in this.unitOfWork.DbContext.Set<Settlement>() on p.CompanyCorrespondenceSettlementId equals ps2.SettlementId into gps2
                from ps2 in gps2.DefaultIfEmpty()

                where procedureShares.Where(ps => ps.ProcedureId == p.ProcedureId).Any()

                select new
                {
                    Id = p.ProjectId,
                    Project = p.Name,
                    ContractRegNum = string.Empty,

                    BeneficiaryName = p.CompanyName,
                    BeneficiaryUin = p.CompanyUin,
                    BeneficiaryType = pct.Name,
                    BeneficiaryLegalType = pclt.Name,

                    BeneficiarySeatCountryCode = pco1.NutsCode,
                    BeneficiarySeatCountry = pco1.Name,
                    BeneficiarySeatSettlement = ps1.Name,
                    BeneficiarySeatPostCode = p.CompanySeatPostCode,
                    BeneficiarySeatStreet = p.CompanySeatStreet,
                    BeneficiarySeatAddress = p.CompanySeatAddress,

                    BeneficiaryCorrespondenceCountryCode = pco2.NutsCode,
                    BeneficiaryCorrespondenceCountry = pco2.Name,
                    BeneficiaryCorrespondenceSettlement = ps2.Name,
                    BeneficiaryCorrespondencePostCode = p.CompanyCorrespondencePostCode,
                    BeneficiaryCorrespondenceStreet = p.CompanyCorrespondenceStreet,
                    BeneficiaryCorrespondenceAddress = p.CompanyCorrespondenceAddress,
                };

            var irregularityPredicate = PredicateBuilder.True<Irregularity>();
            irregularityPredicate = irregularityPredicate
                .AndDateTimeGreaterThanOrEqual(t => t.CreateDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.CreateDate, toDate);

            var financialCorrections = (from ifc in this.unitOfWork.DbContext.Set<IrregularityFinancialCorrection>()
                                        join fc in this.unitOfWork.DbContext.Set<FinancialCorrection>() on ifc.FinancialCorrectionId equals fc.FinancialCorrectionId
                                        group fc.OrderNum by ifc.IrregularityId into g
                                        select new
                                        {
                                            IrregularityId = g.Key,
                                            FinancialCorrectionOrdedNums = g.AsEnumerable(),
                                        })
                                .ToList()
                                .ToDictionary(t => t.IrregularityId, t => string.Join(", ", t.FinancialCorrectionOrdedNums));

            return (from i in this.unitOfWork.DbContext.Set<Irregularity>().Where(irregularityPredicate)
                    join iv in this.unitOfWork.DbContext.Set<IrregularityVersion>() on i.IrregularityId equals iv.IrregularityId
                    join irs in this.unitOfWork.DbContext.Set<IrregularitySignal>() on i.IrregularitySignalId equals irs.IrregularitySignalId

                    join p in projects on irs.ProjectId equals p.Id

                    join c in contracts on i.ContractId equals c.Id into gc
                    from c in gc.DefaultIfEmpty()

                    where i.Status == IrregularityStatus.Entered && iv.Status == IrregularityVersionStatus.Active

                    orderby c.ContractRegNum

                    select new
                    {
                        IrregularityId = i.IrregularityId,
                        IrregularitySignal = irs.RegNumber,
                        IrregularitySignalRegDate = irs.RegDate,
                        Status = i.CaseState,
                        IrregularityRegNum = i.RegNumber,
                        IrregularityRegDate = i.CreateDate,
                        IrregularityValue = iv.IrregularExpensesLv.TotalAmount,

                        p,
                        c,
                    })
                    .ToList()
                    .Select(t =>
                        {
                            var pi = t.c ?? t.p;

                            return new IrregularitiesReportItem
                            {
                                BeneficiaryName = pi.BeneficiaryName,
                                BeneficiaryUin = pi.BeneficiaryUin,
                                BeneficiaryType = pi.BeneficiaryType,
                                BeneficiaryLegalType = pi.BeneficiaryLegalType,

                                BeneficiarySeatAddress = pi.BeneficiarySeatCountryCode == "BG"
                                    ? string.Format("{0}, {1} {2}, {3}", pi.BeneficiarySeatCountry, pi.BeneficiarySeatSettlement, pi.BeneficiarySeatPostCode, pi.BeneficiarySeatStreet)
                                    : string.Format("{0}, {1}", pi.BeneficiarySeatCountry, pi.BeneficiarySeatAddress),

                                BeneficiaryCorrespondenceAddress = pi.BeneficiaryCorrespondenceCountryCode == "BG"
                                    ? string.Format("{0}, {1} {2}, {3}", pi.BeneficiarySeatCountry, pi.BeneficiaryCorrespondenceSettlement, pi.BeneficiaryCorrespondencePostCode, pi.BeneficiaryCorrespondenceStreet)
                                    : string.Format("{0}, {1}", pi.BeneficiaryCorrespondenceCountry, pi.BeneficiaryCorrespondenceAddress),

                                Project = pi.Project,
                                ContractRegNum = pi.ContractRegNum,

                                IrregularitySignal = t.IrregularitySignal,
                                IrregularitySignalRegDate = t.IrregularitySignalRegDate,
                                Status = t.Status,
                                IrregularityRegNum = t.IrregularityRegNum,
                                IrregularityRegDate = t.IrregularityRegDate,
                                IrregularityValue = t.IrregularityValue,
                                FinancialCorrections = financialCorrections.ContainsKey(t.IrregularityId)
                                    ? financialCorrections[t.IrregularityId]
                                    : null,
                            };
                        })
                    .ToList();
        }

        public IList<PinReportItem> GetPinReport(
            int? programmeId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string uin = null)
        {
            var contractPredicate = PredicateBuilder.True<Domain.Contracts.Contract>();
            contractPredicate = contractPredicate
                .AndEquals(t => t.ProgrammeId, programmeId);

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var memberPredicate = PredicateBuilder.True<Domain.Contracts.ContractReportTechnicalMember>();
            memberPredicate = memberPredicate
                .AndStringContains(t => t.Uin, uin)
                .AndDateTimeGreaterThanOrEqual(t => t.Date, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.Date, toDate);

            return (from crtm in this.unitOfWork.DbContext.Set<ContractReportTechnicalMember>().Where(memberPredicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crtm.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(contractPredicate) on crtm.ContractId equals c.ContractId
                    where cr.Status == ContractReportStatus.Accepted
                    orderby crtm.Uin
                    select new PinReportItem
                    {
                        Uin = crtm.Uin,
                        Name = crtm.Name,
                        Date = crtm.Date,
                        Hours = crtm.Hours,
                        ContractRegNum = c.RegNumber,
                    }).ToList();
        }

        public IList<MicrodataEsfReportItem> GetMicrodataEsfReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? toDate = null)
        {
            var contractPredicate = PredicateBuilder.True<Domain.Contracts.Contract>();
            contractPredicate = contractPredicate
                .AndEquals(t => t.ProcedureId, procedureId)
                .AndEquals(t => t.ProgrammeId, programmeId);

            if (toDate.HasValue)
            {
                toDate = toDate.Value.Date.AddDays(1).AddMilliseconds(-1);
            }

            var contractReportPredicate = PredicateBuilder.True<ContractReport>();
            contractReportPredicate = contractReportPredicate
                .AndDateTimeLessThanOrEqual(t => t.CheckedDate, toDate);

            var lastAcceptedContractReportsWithType2Micro =
                from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(contractReportPredicate)
                join crm in this.unitOfWork.DbContext.Set<ContractReportMicro>().Where(crm => crm.Type == ContractReportMicroType.Type2) on cr.ContractReportId equals crm.ContractReportId
                join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>().Where(contractPredicate) on cr.ContractId equals c.ContractId
                where cr.Status == ContractReportStatus.Accepted
                group new { cr.ContractReportId, cr.OrderNum } by cr.ContractId into g
                select g.OrderByDescending(t => t.OrderNum).FirstOrDefault().ContractReportId;

            return
                (from crmi in this.unitOfWork.DbContext.Set<ContractReportMicrosType2Item>()

                 join addrd in this.unitOfWork.DbContext.Set<ContractReportMicrosDistrict>() on crmi.AddressDistrictId equals addrd.ContractReportMicrosDistrictId into g0
                 from addrd in g0.DefaultIfEmpty()

                 join addrs in this.unitOfWork.DbContext.Set<ContractReportMicrosSettlement>() on crmi.AddressSettlementId equals addrs.ContractReportMicrosSettlementId into g1
                 from addrs in g1.DefaultIfEmpty()

                 join apd in this.unitOfWork.DbContext.Set<ContractReportMicrosDistrict>() on crmi.ActivityPlaceDistrictId equals apd.ContractReportMicrosDistrictId into g2
                 from apd in g2.DefaultIfEmpty()

                 join aps in this.unitOfWork.DbContext.Set<ContractReportMicrosSettlement>() on crmi.ActivityPlaceSettlementId equals aps.ContractReportMicrosSettlementId into g3
                 from aps in g3.DefaultIfEmpty()

                 join crm in this.unitOfWork.DbContext.Set<ContractReportMicro>() on crmi.ContractReportMicroId equals crm.ContractReportMicroId
                 join cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(t => lastAcceptedContractReportsWithType2Micro.Contains(t.ContractReportId)) on crm.ContractReportId equals cr.ContractReportId
                 join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on cr.ContractId equals c.ContractId

                 where crm.Status == ContractReportMicroStatus.Actual

                 orderby c.RegNumber, cr.ContractReportId, crmi.ContractReportMicroId

                 select new MicrodataEsfReportItem()
                 {
                     ContractRegNumber = c.RegNumber,
                     Number = crmi.Number,
                     FirstName = crmi.FirstName,
                     MiddleName = crmi.MiddleName,
                     LastName = crmi.LastName,
                     Uin = crmi.Uin,
                     Gender = crmi.Gender,
                     Age = crmi.Age,
                     Occupation = crmi.Occupation,
                     Education = crmi.Education,
                     AddressDistrictId = crmi.AddressDistrictId,
                     AddressDistrictName = addrd.Name,
                     AddressSettlementId = crmi.AddressSettlementId,
                     AddressSettlementName = addrs.Name,
                     Phone = crmi.Phone,
                     Email = crmi.Email,
                     IsEmigrant = crmi.IsEmigrant,
                     IsForeigner = crmi.IsForeigner,
                     IsMinority = crmi.IsMinority,
                     IsGypsy = crmi.IsGypsy,
                     IsDisabledPerson = crmi.IsDisabledPerson,
                     IsHomeless = crmi.IsHomeless,
                     DisadvantagedPerson = crmi.DisadvantagedPerson,
                     IsLivingInUnemployedHousehold = crmi.IsLivingInUnemployedHousehold,
                     IsLivingInUnemployedHouseholdWithChildren = crmi.IsLivingInUnemployedHouseholdWithChildren,
                     IsLivingInFamilyOfOneWithChildren = crmi.IsLivingInFamilyOfOneWithChildren,
                     JoiningDate = crmi.JoiningDate,
                     Activity = crmi.Activity,
                     ActivityPlaceDistrictId = crmi.ActivityPlaceDistrictId,
                     ActivityPlaceDistrictName = apd.Name,
                     ActivityPlaceSettlementId = crmi.ActivityPlaceSettlementId,
                     ActivityPlaceSettlementName = aps.Name,
                     ParticipationState = crmi.ParticipationState,
                     LeavingDate = crmi.LeavingDate,
                     CancelationReason = crmi.CancelationReason,
                     LeavingState = crmi.LeavingState,
                 }).ToList();
        }

        public IList<ExpenseTypesReportItem> GetExpenseTypesReport(int? programmeId = null, DateTime? toDate = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }
            else
            {
                toDate = DateTime.Now.AddDays(1).AddMilliseconds(-1);
            }

            var psPredicate = PredicateBuilder.True<ProcedureShare>()
                .AndEquals(ps => ps.ProgrammeId, programmeId);

            var allowedCertReports = PredicateBuilder.True<CertReport>()
                .And(p => allowedCertReportStatuses.Contains(p.Status));

            var baseAmounts = (from cbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                               join l3 in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on cbi.ContractBudgetLevel3AmountId equals l3.ContractBudgetLevel3AmountId
                               join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on l3.ProcedureBudgetLevel2Id equals l2.ProcedureBudgetLevel2Id
                               join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(psPredicate) on l2.ProcedureShareId equals ps.ProcedureShareId
                               join p in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals p.MapNodeId

                               join cr in this.unitOfWork.DbContext.Set<ContractReport>() on cbi.ContractReportId equals cr.ContractReportId
                               join crep in this.unitOfWork.DbContext.Set<CertReport>().Where(allowedCertReports) on cbi.CertReportId equals crep.CertReportId into g1
                               from crep in g1.DefaultIfEmpty()

                               where cr.Status == ContractReportStatus.Accepted && cbi.CreateDate <= toDate
                               select new InnerExpenseTypesReportItem
                               {
                                   Programme = p.Name,

                                   ApprovedBfpAmount = cbi.ApprovedBfpTotalAmount,
                                   ApprovedSelfAmount = cbi.ApprovedSelfAmount,
                                   ApprovedTotalAmount = cbi.ApprovedTotalAmount,

                                   CertifiedBfpAmount = cbi.CertifiedApprovedBfpTotalAmount,
                                   CertifiedSelfAmount = cbi.CertifiedApprovedSelfAmount,
                                   CertifiedTotalAmount = cbi.CertifiedApprovedTotalAmount,
                               }).ToList();

            Func<IEnumerable<InnerExpenseTypesReportItem>, string, int, IEnumerable<InnerExpenseTypesReportItem>> getTableAmount = (tbl, name, order) =>
            {
                var res = tbl.Select(x => new InnerExpenseTypesReportItem
                {
                    Programme = x.Programme,
                    ExpenseType = name,
                    OrderNum = order,

                    ApprovedBfpAmount = x.ApprovedBfpAmount,
                    ApprovedSelfAmount = x.ApprovedSelfAmount,
                    ApprovedTotalAmount = x.ApprovedTotalAmount,

                    CertifiedBfpAmount = x.CertifiedBfpAmount,
                    CertifiedSelfAmount = x.ApprovedSelfAmount,
                    CertifiedTotalAmount = x.CertifiedTotalAmount,
                });

                return res;
            };

            var expenseTypes = getTableAmount(baseAmounts, "Разход въз основа на еднократни суми общи суми, одобрени от ЕК", 5);

            var result = (from a in expenseTypes
                          group a by new
                          {
                              a.Programme,
                              a.ExpenseType,
                              a.OrderNum,
                          }
                          into g
                          orderby g.Key.Programme, g.Key.OrderNum
                          select new ExpenseTypesReportItem
                          {
                              Programme = g.Key.Programme,
                              ExpenseType = g.Key.ExpenseType,

                              ApprovedBfpAmount = g.Sum(x => x.ApprovedBfpAmount),
                              ApprovedSelfAmount = g.Sum(x => x.ApprovedSelfAmount),
                              ApprovedTotalAmount = g.Sum(x => x.ApprovedTotalAmount),

                              CertifiedBfpAmount = g.Sum(x => x.CertifiedBfpAmount),
                              CertifiedSelfAmount = g.Sum(x => x.CertifiedSelfAmount),
                              CertifiedTotalAmount = g.Sum(x => x.CertifiedTotalAmount),
                          }).ToList();

            return result;
        }

        private string GetNutsFullPath(
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            string fullPath = null;

            if (countryId.HasValue)
            {
                fullPath = this.unitOfWork.DbContext.Set<Country>().First(e => e.CountryId.Equals(countryId.Value)).NutsCode;
            }
            else if (nuts1Id.HasValue)
            {
                fullPath = this.unitOfWork.DbContext.Set<Nuts1>().First(e => e.Nuts1Id.Equals(nuts1Id.Value)).FullPath;
            }
            else if (nuts2Id.HasValue)
            {
                fullPath = this.unitOfWork.DbContext.Set<Nuts2>().First(e => e.Nuts2Id.Equals(nuts2Id.Value)).FullPath;
            }
            else if (districtId.HasValue)
            {
                fullPath = this.unitOfWork.DbContext.Set<District>().First(e => e.DistrictId.Equals(districtId.Value)).FullPath;
            }
            else if (municipalityId.HasValue)
            {
                fullPath = this.unitOfWork.DbContext.Set<Municipality>().First(e => e.MunicipalityId.Equals(municipalityId.Value)).FullPath;
            }
            else if (settlementId.HasValue)
            {
                fullPath = this.unitOfWork.DbContext.Set<Settlement>().First(e => e.SettlementId.Equals(settlementId.Value)).FullPath;
            }
            else if (protectedZoneId.HasValue)
            {
                fullPath = this.unitOfWork.DbContext.Set<ProtectedZone>().First(e => e.ProtectedZoneId.Equals(protectedZoneId.Value)).FullPath;
            }

            return fullPath;
        }

        private IQueryable<ContractAmountDO> GetActualContractContractedAmounts()
        {
            return from cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>()
                   join pbl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pbl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pbl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on cbl3a.ContractId equals c.ContractId
                   select new ContractAmountDO
                   {
                       Id = cbl3a.ContractBudgetLevel3AmountId,
                       ContractId = cbl3a.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = (int?)null,
                       ContractReportId = (int?)null,
                       ContractReportPaymentId = (int?)null,
                       ProgrammeId = c.ProgrammeId,
                       ProcedureId = c.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = cbl3a.ContractBudgetLevel3AmountId,
                       ContractBudgetLevel3AmountNutsFullPath = cbl3a.NutsFullPath,
                       ContractBudgetLevel3AmountNutsFullPathName = cbl3a.NutsFullPathName,
                       AdvancePayment = null,
                       FinancialCorrectionId = (int?)null,
                       ContractContractId = (int?)null,
                       ContractedEuAmount = cbl3a.CurrentEuAmount,
                       ContractedBgAmount = cbl3a.CurrentBgAmount,
                       ContractedBfpTotalAmount = cbl3a.CurrentEuAmount + cbl3a.CurrentBgAmount,
                       ContractedSelfAmount = cbl3a.CurrentSelfAmount,
                       ContractedTotalAmount = cbl3a.CurrentEuAmount + cbl3a.CurrentBgAmount + cbl3a.CurrentSelfAmount,
                       ReportedEuAmount = (decimal?)null,
                       ReportedBgAmount = (decimal?)null,
                       ReportedBfpTotalAmount = (decimal?)null,
                       ReportedSelfAmount = (decimal?)null,
                       ReportedTotalAmount = (decimal?)null,
                       ApprovedEuAmount = (decimal?)null,
                       ApprovedBgAmount = (decimal?)null,
                       ApprovedBfpTotalAmount = (decimal?)null,
                       ApprovedSelfAmount = (decimal?)null,
                       ApprovedTotalAmount = (decimal?)null,
                       CertifiedEuAmount = (decimal?)null,
                       CertifiedBgAmount = (decimal?)null,
                       CertifiedBfpTotalAmount = (decimal?)null,
                       CertifiedSelfAmount = (decimal?)null,
                       CertifiedTotalAmount = (decimal?)null,
                       CorrectedEuAmount = (decimal?)null,
                       CorrectedBgAmount = (decimal?)null,
                       CorrectedBfpTotalAmount = (decimal?)null,
                       CorrectedSelfAmount = (decimal?)null,
                       CorrectedTotalAmount = (decimal?)null,
                       UnapprovedEuAmount = (decimal?)null,
                       UnapprovedBgAmount = (decimal?)null,
                       UnapprovedBfpTotalAmount = (decimal?)null,
                       UnapprovedSelfAmount = (decimal?)null,
                       UnapprovedTotalAmount = (decimal?)null,
                       InitialContractedEuAmount = (decimal?)null,
                       InitialContractedBgAmount = (decimal?)null,
                       InitialContractedBfpTotalAmount = (decimal?)null,
                       InitialContractedSelfAmount = (decimal?)null,
                       InitialContractedTotalAmount = (decimal?)null,
                   };
        }

        private IQueryable<ContractAmountDO> GetInitialContractContractedAmounts()
        {
            return from cbl3a in this.unitOfWork.DbContext.Set<ContractVersionXmlAmount>()
                   join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on cbl3a.ContractVersionXmlId equals cv.ContractVersionXmlId
                   join pbl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pbl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pbl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on cbl3a.ContractId equals c.ContractId
                   where cv.VersionType == ContractVersionType.NewContract
                   select new ContractAmountDO
                   {
                       Id = cbl3a.ContractVersionXmlAmountId,
                       ContractId = cbl3a.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = (int?)null,
                       ContractReportId = (int?)null,
                       ContractReportPaymentId = (int?)null,
                       ProgrammeId = c.ProgrammeId,
                       ProcedureId = c.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = null,
                       ContractBudgetLevel3AmountNutsFullPath = cbl3a.NutsFullPath,
                       ContractBudgetLevel3AmountNutsFullPathName = cbl3a.NutsFullPathName,
                       AdvancePayment = null,
                       FinancialCorrectionId = (int?)null,
                       ContractContractId = (int?)null,
                       ContractedEuAmount = (decimal?)null,
                       ContractedBgAmount = (decimal?)null,
                       ContractedBfpTotalAmount = (decimal?)null,
                       ContractedSelfAmount = (decimal?)null,
                       ContractedTotalAmount = (decimal?)null,
                       ReportedEuAmount = (decimal?)null,
                       ReportedBgAmount = (decimal?)null,
                       ReportedBfpTotalAmount = (decimal?)null,
                       ReportedSelfAmount = (decimal?)null,
                       ReportedTotalAmount = (decimal?)null,
                       ApprovedEuAmount = (decimal?)null,
                       ApprovedBgAmount = (decimal?)null,
                       ApprovedBfpTotalAmount = (decimal?)null,
                       ApprovedSelfAmount = (decimal?)null,
                       ApprovedTotalAmount = (decimal?)null,
                       CertifiedEuAmount = (decimal?)null,
                       CertifiedBgAmount = (decimal?)null,
                       CertifiedBfpTotalAmount = (decimal?)null,
                       CertifiedSelfAmount = (decimal?)null,
                       CertifiedTotalAmount = (decimal?)null,
                       CorrectedEuAmount = (decimal?)null,
                       CorrectedBgAmount = (decimal?)null,
                       CorrectedBfpTotalAmount = (decimal?)null,
                       CorrectedSelfAmount = (decimal?)null,
                       CorrectedTotalAmount = (decimal?)null,
                       UnapprovedEuAmount = (decimal?)null,
                       UnapprovedBgAmount = (decimal?)null,
                       UnapprovedBfpTotalAmount = (decimal?)null,
                       UnapprovedSelfAmount = (decimal?)null,
                       UnapprovedTotalAmount = (decimal?)null,
                       InitialContractedEuAmount = cbl3a.CurrentEuAmount,
                       InitialContractedBgAmount = cbl3a.CurrentBgAmount,
                       InitialContractedBfpTotalAmount = cbl3a.CurrentEuAmount + cbl3a.CurrentBgAmount,
                       InitialContractedSelfAmount = cbl3a.CurrentSelfAmount,
                       InitialContractedTotalAmount = cbl3a.CurrentEuAmount + cbl3a.CurrentBgAmount + cbl3a.CurrentSelfAmount,
                   };
        }

        private IQueryable<ContractAmountDO> GetAttachedFinancialCorrectionCSDs()
        {
            return from afc in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>()
                   join f in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on afc.ContractReportFinancialCorrectionId equals f.ContractReportFinancialCorrectionId
                   join fc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on afc.ContractReportFinancialCorrectionId equals fc.ContractReportFinancialCorrectionId
                   join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                   join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on fbi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fc.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on fc.ContractId equals c.ContractId

                   join cc in this.unitOfWork.DbContext.Set<ContractContract>() on csd.ContractContractorGid equals cc.Gid into g0
                   from cc in g0.DefaultIfEmpty()

                   join crep in this.unitOfWork.DbContext.Set<CertReport>() on fc.CertReportId equals crep.CertReportId into g1
                   from crep in g1.DefaultIfEmpty()

                   where crp.Status == ContractReportPaymentStatus.Actual && f.Status == ContractReportFinancialCorrectionStatus.Ended
                   select new ContractAmountDO
                   {
                       Id = fc.ContractReportFinancialCorrectionCSDId,
                       ContractId = (int?)fc.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = fc.CertReportId.Value,
                       ContractReportId = afc.ContractReportId,
                       ContractReportPaymentId = (int?)crp.ContractReportPaymentId,
                       ProgrammeId = c.ProgrammeId,
                       ProcedureId = c.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = cbl3a.ContractBudgetLevel3AmountId,
                       ContractBudgetLevel3AmountNutsFullPath = cbl3a.NutsFullPath,
                       ContractBudgetLevel3AmountNutsFullPathName = cbl3a.NutsFullPathName,
                       AdvancePayment = fbi.AdvancePayment,
                       FinancialCorrectionId = fc.FinancialCorrectionId,
                       ContractContractId = cc != null ? cc.ContractContractId : (int?)null,
                       ContractedEuAmount = (decimal?)null,
                       ContractedBgAmount = (decimal?)null,
                       ContractedBfpTotalAmount = (decimal?)null,
                       ContractedSelfAmount = (decimal?)null,
                       ContractedTotalAmount = (decimal?)null,
                       ReportedEuAmount = (decimal?)null,
                       ReportedBgAmount = (decimal?)null,
                       ReportedBfpTotalAmount = (decimal?)null,
                       ReportedSelfAmount = (decimal?)null,
                       ReportedTotalAmount = (decimal?)null,
                       ApprovedEuAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedEuAmount,
                       ApprovedBgAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedBgAmount,
                       ApprovedBfpTotalAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedBfpTotalAmount,
                       ApprovedSelfAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedSelfAmount,
                       ApprovedTotalAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedTotalAmount,
                       CertifiedEuAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (-1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedEuAmount) : 0m,
                       CertifiedBgAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (-1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedBgAmount) : 0m,
                       CertifiedBfpTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (-1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedBfpTotalAmount) : 0m,
                       CertifiedSelfAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (-1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedSelfAmount) : 0m,
                       CertifiedTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (-1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedTotalAmount) : 0m,
                       CorrectedEuAmount = (int)fc.Sign * fc.CorrectedUnapprovedByCorrectionEuAmount,
                       CorrectedBgAmount = (int)fc.Sign * fc.CorrectedUnapprovedByCorrectionBgAmount,
                       CorrectedBfpTotalAmount = (int)fc.Sign * fc.CorrectedUnapprovedByCorrectionBfpTotalAmount,
                       CorrectedSelfAmount = (int)fc.Sign * fc.CorrectedUnapprovedByCorrectionSelfAmount,
                       CorrectedTotalAmount = (int)fc.Sign * fc.CorrectedUnapprovedByCorrectionTotalAmount,
                       UnapprovedEuAmount = (int)fc.Sign * fc.CorrectedUnapprovedEuAmount,
                       UnapprovedBgAmount = (int)fc.Sign * fc.CorrectedUnapprovedBgAmount,
                       UnapprovedBfpTotalAmount = (int)fc.Sign * fc.CorrectedUnapprovedBfpTotalAmount,
                       UnapprovedSelfAmount = (int)fc.Sign * fc.CorrectedUnapprovedSelfAmount,
                       UnapprovedTotalAmount = (int)fc.Sign * fc.CorrectedUnapprovedTotalAmount,
                       InitialContractedEuAmount = (decimal?)null,
                       InitialContractedBgAmount = (decimal?)null,
                       InitialContractedBfpTotalAmount = (decimal?)null,
                       InitialContractedSelfAmount = (decimal?)null,
                       InitialContractedTotalAmount = (decimal?)null,
                   };
        }

        private IQueryable<ContractAmountDO> GetAdvancePaymentAmounts()
        {
            return from apa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>()
                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on apa.ContractId equals c.ContractId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on apa.ProgrammePriorityId equals pp.MapNodeId

                   join cr in this.unitOfWork.DbContext.Set<ContractReport>() on apa.ContractReportId equals cr.ContractReportId

                   join crep in this.unitOfWork.DbContext.Set<CertReport>() on apa.CertReportId equals crep.CertReportId into g1
                   from crep in g1.DefaultIfEmpty()

                   select new ContractAmountDO
                   {
                       Id = apa.ContractReportAdvancePaymentAmountId,
                       ContractId = (int?)apa.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = apa.CertReportId.Value,
                       ContractReportId = apa.ContractReportId,
                       ContractReportPaymentId = (int?)apa.ContractReportPaymentId,
                       ProgrammeId = c.ProgrammeId,
                       ProcedureId = c.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = (int?)null,
                       ContractBudgetLevel3AmountNutsFullPath = null,
                       ContractBudgetLevel3AmountNutsFullPathName = null,
                       AdvancePayment = null,
                       FinancialCorrectionId = (int?)null,
                       ContractContractId = (int?)null,
                       ContractedEuAmount = (decimal?)null,
                       ContractedBgAmount = (decimal?)null,
                       ContractedBfpTotalAmount = (decimal?)null,
                       ContractedSelfAmount = (decimal?)null,
                       ContractedTotalAmount = (decimal?)null,
                       ReportedEuAmount = (decimal?)null,
                       ReportedBgAmount = (decimal?)null,
                       ReportedBfpTotalAmount = (decimal?)null,
                       ReportedSelfAmount = (decimal?)null,
                       ReportedTotalAmount = (decimal?)null,
                       ApprovedEuAmount = cr.Status == ContractReportStatus.Accepted ? apa.ApprovedEuAmount : 0m,
                       ApprovedBgAmount = cr.Status == ContractReportStatus.Accepted ? apa.ApprovedBgAmount : 0m,
                       ApprovedBfpTotalAmount = cr.Status == ContractReportStatus.Accepted ? apa.ApprovedBfpTotalAmount : 0m,
                       ApprovedSelfAmount = (decimal?)null,
                       ApprovedTotalAmount = cr.Status == ContractReportStatus.Accepted ? apa.ApprovedBfpTotalAmount : 0m,
                       CertifiedEuAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? apa.CertifiedApprovedEuAmount : 0m,
                       CertifiedBgAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? apa.CertifiedApprovedBgAmount : 0m,
                       CertifiedBfpTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? apa.CertifiedApprovedBfpTotalAmount : 0m,
                       CertifiedSelfAmount = (decimal?)null,
                       CertifiedTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? apa.CertifiedApprovedBfpTotalAmount : 0m,
                       CorrectedEuAmount = (decimal?)null,
                       CorrectedBgAmount = (decimal?)null,
                       CorrectedBfpTotalAmount = (decimal?)null,
                       CorrectedSelfAmount = (decimal?)null,
                       CorrectedTotalAmount = (decimal?)null,
                       UnapprovedEuAmount = (decimal?)null,
                       UnapprovedBgAmount = (decimal?)null,
                       UnapprovedBfpTotalAmount = (decimal?)null,
                       UnapprovedSelfAmount = (decimal?)null,
                       UnapprovedTotalAmount = (decimal?)null,
                       InitialContractedEuAmount = (decimal?)null,
                       InitialContractedBgAmount = (decimal?)null,
                       InitialContractedBfpTotalAmount = (decimal?)null,
                       InitialContractedSelfAmount = (decimal?)null,
                       InitialContractedTotalAmount = (decimal?)null,
                   };
        }

        private IQueryable<ContractAmountDO> GetFinancialCSDBudgetItems()
        {
            var allowedContractReportsPredicate = PredicateBuilder.False<ContractReport>();

            foreach (var status in allowedContractReportStatuses)
            {
                allowedContractReportsPredicate = allowedContractReportsPredicate.Or(cr => cr.Status == status);
            }

            return from fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                   join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on fbi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fbi.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on fbi.ContractId equals c.ContractId
                   join cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(allowedContractReportsPredicate) on fbi.ContractReportId equals cr.ContractReportId

                   join cc in this.unitOfWork.DbContext.Set<ContractContract>() on csd.ContractContractorGid equals cc.Gid into g0
                   from cc in g0.DefaultIfEmpty()

                   join crep in this.unitOfWork.DbContext.Set<CertReport>() on fbi.CertReportId equals crep.CertReportId into g1
                   from crep in g1.DefaultIfEmpty()

                   where crp.Status == ContractReportPaymentStatus.Actual
                   select new ContractAmountDO
                   {
                       Id = fbi.ContractReportFinancialCSDBudgetItemId,
                       ContractId = (int?)fbi.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = fbi.CertReportId.Value,
                       ContractReportId = fbi.ContractReportId,
                       ContractReportPaymentId = (int?)crp.ContractReportPaymentId,
                       ProgrammeId = c.ProgrammeId,
                       ProcedureId = c.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = cbl3a.ContractBudgetLevel3AmountId,
                       ContractBudgetLevel3AmountNutsFullPath = cbl3a.NutsFullPath,
                       ContractBudgetLevel3AmountNutsFullPathName = cbl3a.NutsFullPathName,
                       AdvancePayment = fbi.AdvancePayment,
                       FinancialCorrectionId = fbi.FinancialCorrectionId,
                       ContractContractId = cc != null ? cc.ContractContractId : (int?)null,
                       ContractedEuAmount = (decimal?)null,
                       ContractedBgAmount = (decimal?)null,
                       ContractedBfpTotalAmount = (decimal?)null,
                       ContractedSelfAmount = (decimal?)null,
                       ContractedTotalAmount = (decimal?)null,
                       ReportedEuAmount = fbi.EuAmount,
                       ReportedBgAmount = fbi.BgAmount,
                       ReportedBfpTotalAmount = fbi.BfpTotalAmount,
                       ReportedSelfAmount = fbi.SelfAmount,
                       ReportedTotalAmount = fbi.TotalAmount,
                       ApprovedEuAmount = cr.Status == ContractReportStatus.Accepted ? fbi.ApprovedEuAmount : 0m,
                       ApprovedBgAmount = cr.Status == ContractReportStatus.Accepted ? fbi.ApprovedBgAmount : 0m,
                       ApprovedBfpTotalAmount = cr.Status == ContractReportStatus.Accepted ? fbi.ApprovedBfpTotalAmount : 0m,
                       ApprovedSelfAmount = cr.Status == ContractReportStatus.Accepted ? fbi.ApprovedSelfAmount : 0m,
                       ApprovedTotalAmount = cr.Status == ContractReportStatus.Accepted ? fbi.ApprovedTotalAmount : 0m,
                       CertifiedEuAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? fbi.CertifiedApprovedEuAmount : 0m,
                       CertifiedBgAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? fbi.CertifiedApprovedBgAmount : 0m,
                       CertifiedBfpTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (fbi.CertifiedApprovedEuAmount + fbi.CertifiedApprovedBgAmount) : 0m,
                       CertifiedSelfAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? fbi.CertifiedApprovedSelfAmount : 0m,
                       CertifiedTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? fbi.CertifiedApprovedTotalAmount : 0m,
                       CorrectedEuAmount = cr.Status == ContractReportStatus.Accepted ? fbi.UnapprovedByCorrectionEuAmount : 0m,
                       CorrectedBgAmount = cr.Status == ContractReportStatus.Accepted ? fbi.UnapprovedByCorrectionBgAmount : 0m,
                       CorrectedBfpTotalAmount = cr.Status == ContractReportStatus.Accepted ? fbi.UnapprovedByCorrectionBfpTotalAmount : 0m,
                       CorrectedSelfAmount = cr.Status == ContractReportStatus.Accepted ? fbi.UnapprovedByCorrectionSelfAmount : 0m,
                       CorrectedTotalAmount = cr.Status == ContractReportStatus.Accepted ? fbi.UnapprovedByCorrectionTotalAmount : 0m,
                       UnapprovedEuAmount = cr.Status == ContractReportStatus.Accepted ? fbi.UnapprovedEuAmount : 0m,
                       UnapprovedBgAmount = cr.Status == ContractReportStatus.Accepted ? fbi.UnapprovedBgAmount : 0m,
                       UnapprovedBfpTotalAmount = cr.Status == ContractReportStatus.Accepted ? fbi.UnapprovedBfpTotalAmount : 0m,
                       UnapprovedSelfAmount = cr.Status == ContractReportStatus.Accepted ? fbi.UnapprovedSelfAmount : 0m,
                       UnapprovedTotalAmount = cr.Status == ContractReportStatus.Accepted ? fbi.UnapprovedTotalAmount : 0m,
                       InitialContractedEuAmount = (decimal?)null,
                       InitialContractedBgAmount = (decimal?)null,
                       InitialContractedBfpTotalAmount = (decimal?)null,
                       InitialContractedSelfAmount = (decimal?)null,
                       InitialContractedTotalAmount = (decimal?)null,
                   };
        }

        private IQueryable<ContractAmountDO> GetCorrections()
        {
            return from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()

                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on crc.ProgrammePriorityId equals pp.MapNodeId into g0
                   from pp in g0.DefaultIfEmpty()

                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on crc.ContractId equals c.ContractId into g1
                   from c in g1.DefaultIfEmpty()

                   join crep in this.unitOfWork.DbContext.Set<CertReport>() on crc.CertReportId equals crep.CertReportId into g2
                   from crep in g2.DefaultIfEmpty()

                   where crc.Status == ContractReportCorrectionStatus.Entered
                   select new ContractAmountDO
                   {
                       Id = crc.ContractReportCorrectionId,
                       ContractId = c.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = crc.CertReportId.Value,
                       ContractReportId = null,
                       ContractReportPaymentId = crc.ContractReportPaymentId,
                       ProgrammeId = crc.ProgrammeId,
                       ProcedureId = crc.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = (int?)null,
                       ContractBudgetLevel3AmountNutsFullPath = null,
                       ContractBudgetLevel3AmountNutsFullPathName = null,
                       AdvancePayment = null,
                       FinancialCorrectionId = crc.FinancialCorrectionId,
                       ContractContractId = (int?)null,
                       ContractedEuAmount = (decimal?)null,
                       ContractedBgAmount = (decimal?)null,
                       ContractedBfpTotalAmount = (decimal?)null,
                       ContractedSelfAmount = (decimal?)null,
                       ContractedTotalAmount = (decimal?)null,
                       ReportedEuAmount = (decimal?)null,
                       ReportedBgAmount = (decimal?)null,
                       ReportedBfpTotalAmount = (decimal?)null,
                       ReportedSelfAmount = (decimal?)null,
                       ReportedTotalAmount = (decimal?)null,
                       ApprovedEuAmount = (int)crc.Sign * crc.CorrectedApprovedEuAmount,
                       ApprovedBgAmount = (int)crc.Sign * crc.CorrectedApprovedBgAmount,
                       ApprovedBfpTotalAmount = (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount,
                       ApprovedSelfAmount = (int)crc.Sign * crc.CorrectedApprovedSelfAmount,
                       ApprovedTotalAmount = (int)crc.Sign * crc.CorrectedApprovedTotalAmount,
                       CertifiedEuAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (int)crc.Sign * crc.CertifiedCorrectedApprovedEuAmount : 0m,
                       CertifiedBgAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (int)crc.Sign * crc.CertifiedCorrectedApprovedBgAmount : 0m,
                       CertifiedBfpTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)crc.Sign * crc.CertifiedCorrectedApprovedBfpTotalAmount) : 0m,
                       CertifiedSelfAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (int)crc.Sign * crc.CertifiedCorrectedApprovedSelfAmount : 0m,
                       CertifiedTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (int)crc.Sign * crc.CertifiedCorrectedApprovedTotalAmount : 0m,
                       CorrectedEuAmount = (int)crc.Sign * crc.CorrectedApprovedEuAmount,
                       CorrectedBgAmount = (int)crc.Sign * crc.CorrectedApprovedBgAmount,
                       CorrectedBfpTotalAmount = (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount,
                       CorrectedSelfAmount = (int)crc.Sign * crc.CorrectedApprovedSelfAmount,
                       CorrectedTotalAmount = (int)crc.Sign * crc.CorrectedApprovedTotalAmount,
                       UnapprovedEuAmount = (decimal?)null,
                       UnapprovedBgAmount = (decimal?)null,
                       UnapprovedBfpTotalAmount = (decimal?)null,
                       UnapprovedSelfAmount = (decimal?)null,
                       UnapprovedTotalAmount = (decimal?)null,
                       InitialContractedEuAmount = (decimal?)null,
                       InitialContractedBgAmount = (decimal?)null,
                       InitialContractedBfpTotalAmount = (decimal?)null,
                       InitialContractedSelfAmount = (decimal?)null,
                       InitialContractedTotalAmount = (decimal?)null,
                   };
        }

        private IQueryable<ContractAmountDO> GetFinancialCorrectionCSDs()
        {
            return from fc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                   join f in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on fc.ContractReportFinancialCorrectionId equals f.ContractReportFinancialCorrectionId
                   join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                   join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on fbi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fc.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on fc.ContractId equals c.ContractId
                   join cr in this.unitOfWork.DbContext.Set<ContractReport>() on fc.ContractReportId equals cr.ContractReportId

                   join cc in this.unitOfWork.DbContext.Set<ContractContract>() on csd.ContractContractorGid equals cc.Gid into g0
                   from cc in g0.DefaultIfEmpty()

                   join crep in this.unitOfWork.DbContext.Set<CertReport>() on fbi.CertReportId equals crep.CertReportId into g1
                   from crep in g1.DefaultIfEmpty()

                   where crp.Status == ContractReportPaymentStatus.Actual && cr.Status == ContractReportStatus.Accepted && f.Status == ContractReportFinancialCorrectionStatus.Ended
                   select new ContractAmountDO
                   {
                       Id = fc.ContractReportFinancialCorrectionCSDId,
                       ContractId = (int?)fc.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = fc.CertReportId.Value,
                       ContractReportId = fc.ContractReportId,
                       ContractReportPaymentId = (int?)crp.ContractReportPaymentId,
                       ProgrammeId = c.ProgrammeId,
                       ProcedureId = c.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = cbl3a.ContractBudgetLevel3AmountId,
                       ContractBudgetLevel3AmountNutsFullPath = cbl3a.NutsFullPath,
                       ContractBudgetLevel3AmountNutsFullPathName = cbl3a.NutsFullPathName,
                       AdvancePayment = fbi.AdvancePayment,
                       FinancialCorrectionId = fc.FinancialCorrectionId,
                       ContractContractId = cc != null ? cc.ContractContractId : (int?)null,
                       ContractedEuAmount = (decimal?)null,
                       ContractedBgAmount = (decimal?)null,
                       ContractedBfpTotalAmount = (decimal?)null,
                       ContractedSelfAmount = (decimal?)null,
                       ContractedTotalAmount = (decimal?)null,
                       ReportedEuAmount = (decimal?)null,
                       ReportedBgAmount = (decimal?)null,
                       ReportedBfpTotalAmount = (decimal?)null,
                       ReportedSelfAmount = (decimal?)null,
                       ReportedTotalAmount = (decimal?)null,
                       ApprovedEuAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedEuAmount,
                       ApprovedBgAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedBgAmount,
                       ApprovedBfpTotalAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedBfpTotalAmount,
                       ApprovedSelfAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedSelfAmount,
                       ApprovedTotalAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedTotalAmount,
                       CertifiedEuAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (-1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedEuAmount) : 0m,
                       CertifiedBgAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (-1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedBgAmount) : 0m,
                       CertifiedBfpTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (-1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedBfpTotalAmount) : 0m,
                       CertifiedSelfAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (-1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedSelfAmount) : 0m,
                       CertifiedTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? (-1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedTotalAmount) : 0m,
                       CorrectedEuAmount = (int)fc.Sign * fc.CorrectedUnapprovedByCorrectionEuAmount,
                       CorrectedBgAmount = (int)fc.Sign * fc.CorrectedUnapprovedByCorrectionBgAmount,
                       CorrectedBfpTotalAmount = (int)fc.Sign * fc.CorrectedUnapprovedByCorrectionBfpTotalAmount,
                       CorrectedSelfAmount = (int)fc.Sign * fc.CorrectedUnapprovedByCorrectionSelfAmount,
                       CorrectedTotalAmount = (int)fc.Sign * fc.CorrectedUnapprovedByCorrectionTotalAmount,
                       UnapprovedEuAmount = (int)fc.Sign * fc.CorrectedUnapprovedEuAmount,
                       UnapprovedBgAmount = (int)fc.Sign * fc.CorrectedUnapprovedBgAmount,
                       UnapprovedBfpTotalAmount = (int)fc.Sign * fc.CorrectedUnapprovedBfpTotalAmount,
                       UnapprovedSelfAmount = (int)fc.Sign * fc.CorrectedUnapprovedSelfAmount,
                       UnapprovedTotalAmount = (int)fc.Sign * fc.CorrectedUnapprovedTotalAmount,
                       InitialContractedEuAmount = (decimal?)null,
                       InitialContractedBgAmount = (decimal?)null,
                       InitialContractedBfpTotalAmount = (decimal?)null,
                       InitialContractedSelfAmount = (decimal?)null,
                       InitialContractedTotalAmount = (decimal?)null,
                   };
        }

        private IQueryable<ContractAmountDO> GetCertCorrections()
        {
            return from cc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()

                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on cc.ProgrammePriorityId equals pp.MapNodeId into g0
                   from pp in g0.DefaultIfEmpty()

                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on cc.ContractId equals c.ContractId into g1
                   from c in g1.DefaultIfEmpty()

                   where cc.Status == ContractReportCertCorrectionStatus.Entered
                   select new ContractAmountDO
                   {
                       Id = cc.ContractReportCertCorrectionId,
                       ContractId = cc.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = cc.CertReportId.Value,
                       ContractReportId = null,
                       ContractReportPaymentId = cc.ContractReportPaymentId,
                       ProgrammeId = cc.ProgrammeId,
                       ProcedureId = cc.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = (int?)null,
                       ContractBudgetLevel3AmountNutsFullPath = null,
                       ContractBudgetLevel3AmountNutsFullPathName = null,
                       AdvancePayment = null,
                       FinancialCorrectionId = (int?)null,
                       ContractContractId = (int?)null,
                       ContractedEuAmount = (decimal?)null,
                       ContractedBgAmount = (decimal?)null,
                       ContractedBfpTotalAmount = (decimal?)null,
                       ContractedSelfAmount = (decimal?)null,
                       ContractedTotalAmount = (decimal?)null,
                       ReportedEuAmount = (decimal?)null,
                       ReportedBgAmount = (decimal?)null,
                       ReportedBfpTotalAmount = (decimal?)null,
                       ReportedSelfAmount = (decimal?)null,
                       ReportedTotalAmount = (decimal?)null,
                       ApprovedEuAmount = (decimal?)null,
                       ApprovedBgAmount = (decimal?)null,
                       ApprovedBfpTotalAmount = (decimal?)null,
                       ApprovedSelfAmount = (decimal?)null,
                       ApprovedTotalAmount = (decimal?)null,
                       CertifiedEuAmount = (int)cc.Sign * cc.CertifiedEuAmount,
                       CertifiedBgAmount = (int)cc.Sign * cc.CertifiedBgAmount,
                       CertifiedBfpTotalAmount = (int)cc.Sign * cc.CertifiedBfpTotalAmount,
                       CertifiedSelfAmount = (int)cc.Sign * cc.CertifiedSelfAmount,
                       CertifiedTotalAmount = (int)cc.Sign * cc.CertifiedTotalAmount,
                       CorrectedEuAmount = (decimal?)null,
                       CorrectedBgAmount = (decimal?)null,
                       CorrectedBfpTotalAmount = (decimal?)null,
                       CorrectedSelfAmount = (decimal?)null,
                       CorrectedTotalAmount = (decimal?)null,
                       UnapprovedEuAmount = (decimal?)null,
                       UnapprovedBgAmount = (decimal?)null,
                       UnapprovedBfpTotalAmount = (decimal?)null,
                       UnapprovedSelfAmount = (decimal?)null,
                       UnapprovedTotalAmount = (decimal?)null,
                       InitialContractedEuAmount = (decimal?)null,
                       InitialContractedBgAmount = (decimal?)null,
                       InitialContractedBfpTotalAmount = (decimal?)null,
                       InitialContractedSelfAmount = (decimal?)null,
                       InitialContractedTotalAmount = (decimal?)null,
                   };
        }

        private IQueryable<ContractAmountDO> GetFinancialCertCorrectionCSDs()
        {
            return from fcc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                   join f in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrection>() on fcc.ContractReportFinancialCertCorrectionId equals f.ContractReportFinancialCertCorrectionId
                   join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fcc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                   join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on fbi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fcc.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on fcc.ContractId equals c.ContractId

                   join cc in this.unitOfWork.DbContext.Set<ContractContract>() on csd.ContractContractorGid equals cc.Gid into g0
                   from cc in g0.DefaultIfEmpty()

                   where crp.Status == ContractReportPaymentStatus.Actual && f.Status == ContractReportFinancialCertCorrectionStatus.Ended
                   select new ContractAmountDO
                   {
                       Id = fcc.ContractReportFinancialCertCorrectionCSDId,
                       ContractId = (int?)fcc.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = fcc.CertReportId.Value,
                       ContractReportId = fcc.ContractReportId,
                       ContractReportPaymentId = (int?)crp.ContractReportPaymentId,
                       ProgrammeId = c.ProgrammeId,
                       ProcedureId = c.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = cbl3a.ContractBudgetLevel3AmountId,
                       ContractBudgetLevel3AmountNutsFullPath = cbl3a.NutsFullPath,
                       ContractBudgetLevel3AmountNutsFullPathName = cbl3a.NutsFullPathName,
                       AdvancePayment = fbi.AdvancePayment,
                       FinancialCorrectionId = (int?)null,
                       ContractContractId = cc != null ? cc.ContractContractId : (int?)null,
                       ContractedEuAmount = (decimal?)null,
                       ContractedBgAmount = (decimal?)null,
                       ContractedBfpTotalAmount = (decimal?)null,
                       ContractedSelfAmount = (decimal?)null,
                       ContractedTotalAmount = (decimal?)null,
                       ReportedEuAmount = (decimal?)null,
                       ReportedBgAmount = (decimal?)null,
                       ReportedBfpTotalAmount = (decimal?)null,
                       ReportedSelfAmount = (decimal?)null,
                       ReportedTotalAmount = (decimal?)null,
                       ApprovedEuAmount = (decimal?)null,
                       ApprovedBgAmount = (decimal?)null,
                       ApprovedBfpTotalAmount = (decimal?)null,
                       ApprovedSelfAmount = (decimal?)null,
                       ApprovedTotalAmount = (decimal?)null,
                       CertifiedEuAmount = (int)fcc.Sign * fcc.CertifiedEuAmount,
                       CertifiedBgAmount = (int)fcc.Sign * fcc.CertifiedBgAmount,
                       CertifiedBfpTotalAmount = (int)fcc.Sign * fcc.CertifiedBfpTotalAmount,
                       CertifiedSelfAmount = (int)fcc.Sign * fcc.CertifiedSelfAmount,
                       CertifiedTotalAmount = (int)fcc.Sign * fcc.CertifiedTotalAmount,
                       CorrectedEuAmount = (decimal?)null,
                       CorrectedBgAmount = (decimal?)null,
                       CorrectedBfpTotalAmount = (decimal?)null,
                       CorrectedSelfAmount = (decimal?)null,
                       CorrectedTotalAmount = (decimal?)null,
                       UnapprovedEuAmount = (decimal?)null,
                       UnapprovedBgAmount = (decimal?)null,
                       UnapprovedBfpTotalAmount = (decimal?)null,
                       UnapprovedSelfAmount = (decimal?)null,
                       UnapprovedTotalAmount = (decimal?)null,
                       InitialContractedEuAmount = (decimal?)null,
                       InitialContractedBgAmount = (decimal?)null,
                       InitialContractedBfpTotalAmount = (decimal?)null,
                       InitialContractedSelfAmount = (decimal?)null,
                       InitialContractedTotalAmount = (decimal?)null,
                   };
        }

        private IQueryable<ContractAmountDO> GetRevalidations()
        {
            return from r in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()

                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on r.ProgrammePriorityId equals pp.MapNodeId into g0
                   from pp in g0.DefaultIfEmpty()

                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on r.ContractId equals c.ContractId into g1
                   from c in g1.DefaultIfEmpty()

                   join crep in this.unitOfWork.DbContext.Set<CertReport>() on r.CertReportId equals crep.CertReportId into g2
                   from crep in g2.DefaultIfEmpty()

                   where r.Status == ContractReportRevalidationStatus.Entered
                   select new ContractAmountDO
                   {
                       Id = r.ContractReportRevalidationId,
                       ContractId = r.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = r.CertReportId.Value,
                       ContractReportId = null,
                       ContractReportPaymentId = r.ContractReportPaymentId,
                       ProgrammeId = r.ProgrammeId,
                       ProcedureId = r.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = (int?)null,
                       ContractBudgetLevel3AmountNutsFullPath = null,
                       ContractBudgetLevel3AmountNutsFullPathName = null,
                       AdvancePayment = null,
                       FinancialCorrectionId = (int?)null,
                       ContractContractId = (int?)null,
                       ContractedEuAmount = (decimal?)null,
                       ContractedBgAmount = (decimal?)null,
                       ContractedBfpTotalAmount = (decimal?)null,
                       ContractedSelfAmount = (decimal?)null,
                       ContractedTotalAmount = (decimal?)null,
                       ReportedEuAmount = (decimal?)null,
                       ReportedBgAmount = (decimal?)null,
                       ReportedBfpTotalAmount = (decimal?)null,
                       ReportedSelfAmount = (decimal?)null,
                       ReportedTotalAmount = (decimal?)null,
                       ApprovedEuAmount = (int)r.Sign * r.RevalidatedEuAmount,
                       ApprovedBgAmount = (int)r.Sign * r.RevalidatedBgAmount,
                       ApprovedBfpTotalAmount = (int)r.Sign * r.RevalidatedBfpTotalAmount,
                       ApprovedSelfAmount = (int)r.Sign * r.RevalidatedSelfAmount,
                       ApprovedTotalAmount = (int)r.Sign * r.RevalidatedTotalAmount,
                       CertifiedEuAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)r.Sign * r.CertifiedRevalidatedEuAmount) : 0m,
                       CertifiedBgAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)r.Sign * r.CertifiedRevalidatedBgAmount) : 0m,
                       CertifiedBfpTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)r.Sign * r.CertifiedRevalidatedBfpTotalAmount) : 0m,
                       CertifiedSelfAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)r.Sign * r.CertifiedRevalidatedSelfAmount) : 0m,
                       CertifiedTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)r.Sign * r.CertifiedRevalidatedTotalAmount) : 0m,
                       CorrectedEuAmount = (decimal?)null,
                       CorrectedBgAmount = (decimal?)null,
                       CorrectedBfpTotalAmount = (decimal?)null,
                       CorrectedSelfAmount = (decimal?)null,
                       CorrectedTotalAmount = (decimal?)null,
                       UnapprovedEuAmount = (decimal?)null,
                       UnapprovedBgAmount = (decimal?)null,
                       UnapprovedBfpTotalAmount = (decimal?)null,
                       UnapprovedSelfAmount = (decimal?)null,
                       UnapprovedTotalAmount = (decimal?)null,
                       InitialContractedEuAmount = (decimal?)null,
                       InitialContractedBgAmount = (decimal?)null,
                       InitialContractedBfpTotalAmount = (decimal?)null,
                       InitialContractedSelfAmount = (decimal?)null,
                       InitialContractedTotalAmount = (decimal?)null,
                   };
        }

        private IQueryable<ContractAmountDO> GetFinancialRevalidationCSDs()
        {
            return from fr in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                   join f in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidation>() on fr.ContractReportFinancialRevalidationId equals f.ContractReportFinancialRevalidationId
                   join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fr.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                   join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on fbi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fr.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Domain.Contracts.Contract>() on fr.ContractId equals c.ContractId

                   join cc in this.unitOfWork.DbContext.Set<ContractContract>() on csd.ContractContractorGid equals cc.Gid into g0
                   from cc in g0.DefaultIfEmpty()

                   join crep in this.unitOfWork.DbContext.Set<CertReport>() on fr.CertReportId equals crep.CertReportId into g1
                   from crep in g1.DefaultIfEmpty()

                   where crp.Status == ContractReportPaymentStatus.Actual && f.Status == ContractReportFinancialRevalidationStatus.Ended
                   select new ContractAmountDO
                   {
                       Id = fr.ContractReportFinancialRevalidationCSDId,
                       ContractId = (int?)fr.ContractId,
                       ContractType = c.ContractType,
                       ContractExecutionStatus = c.ExecutionStatus,
                       CertReportId = fr.CertReportId.Value,
                       ContractReportId = fr.ContractReportId,
                       ContractReportPaymentId = (int?)crp.ContractReportPaymentId,
                       ProgrammeId = c.ProgrammeId,
                       ProcedureId = c.ProcedureId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       ContractBudgetLevel3AmountId = cbl3a.ContractBudgetLevel3AmountId,
                       ContractBudgetLevel3AmountNutsFullPath = cbl3a.NutsFullPath,
                       ContractBudgetLevel3AmountNutsFullPathName = cbl3a.NutsFullPathName,
                       AdvancePayment = fbi.AdvancePayment,
                       FinancialCorrectionId = (int?)null,
                       ContractContractId = cc != null ? cc.ContractContractId : (int?)null,
                       ContractedEuAmount = (decimal?)null,
                       ContractedBgAmount = (decimal?)null,
                       ContractedBfpTotalAmount = (decimal?)null,
                       ContractedSelfAmount = (decimal?)null,
                       ContractedTotalAmount = (decimal?)null,
                       ReportedEuAmount = (decimal?)null,
                       ReportedBgAmount = (decimal?)null,
                       ReportedBfpTotalAmount = (decimal?)null,
                       ReportedSelfAmount = (decimal?)null,
                       ReportedTotalAmount = (decimal?)null,
                       ApprovedEuAmount = (int)fr.Sign * fr.RevalidatedEuAmount,
                       ApprovedBgAmount = (int)fr.Sign * fr.RevalidatedBgAmount,
                       ApprovedBfpTotalAmount = (int)fr.Sign * fr.RevalidatedBfpTotalAmount,
                       ApprovedSelfAmount = (int)fr.Sign * fr.RevalidatedSelfAmount,
                       ApprovedTotalAmount = (int)fr.Sign * fr.RevalidatedTotalAmount,
                       CertifiedEuAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)fr.Sign * fr.CertifiedRevalidatedEuAmount) : 0m,
                       CertifiedBgAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)fr.Sign * fr.CertifiedRevalidatedBgAmount) : 0m,
                       CertifiedBfpTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)fr.Sign * (fr.CertifiedRevalidatedEuAmount + fr.CertifiedRevalidatedBgAmount)) : 0m,
                       CertifiedSelfAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)fr.Sign * fr.CertifiedRevalidatedSelfAmount) : 0m,
                       CertifiedTotalAmount = crep != null && allowedCertReportStatuses.Contains(crep.Status) ? ((int)fr.Sign * fr.CertifiedRevalidatedTotalAmount) : 0m,
                       CorrectedEuAmount = (decimal?)null,
                       CorrectedBgAmount = (decimal?)null,
                       CorrectedBfpTotalAmount = (decimal?)null,
                       CorrectedSelfAmount = (decimal?)null,
                       CorrectedTotalAmount = (decimal?)null,
                       UnapprovedEuAmount = (decimal?)null,
                       UnapprovedBgAmount = (decimal?)null,
                       UnapprovedBfpTotalAmount = (decimal?)null,
                       UnapprovedSelfAmount = (decimal?)null,
                       UnapprovedTotalAmount = (decimal?)null,
                       InitialContractedEuAmount = (decimal?)null,
                       InitialContractedBgAmount = (decimal?)null,
                       InitialContractedBfpTotalAmount = (decimal?)null,
                       InitialContractedSelfAmount = (decimal?)null,
                       InitialContractedTotalAmount = (decimal?)null,
                   };
        }

        private IList<ProjectAmountsQueryItem> GetProgrammeBudgetAmounts(DateTime toDate)
        {
            var oldTimeout = this.unitOfWork.DbContext.Database.CommandTimeout;
            this.unitOfWork.DbContext.Database.CommandTimeout = 10 * 60;

            List<System.Data.SqlClient.SqlParameter> selectSqlParams = new List<System.Data.SqlClient.SqlParameter>();
            selectSqlParams.Add(new System.Data.SqlClient.SqlParameter("@toDate", toDate));

            string query = $@"WITH XMLNAMESPACES (
                    N'http://ereg.egov.bg/segment/R-10007' as R10007,
                    N'http://ereg.egov.bg/segment/R-10008' as R10008,
                    N'http://ereg.egov.bg/segment/R-10009' as R10009,
                    N'http://ereg.egov.bg/segment/R-10010' as R10010,
                    N'http://ereg.egov.bg/segment/R-10019' as R10019,
                    N'http://ereg.egov.bg/segment/R-09991' as R09991,
                    N'http://ereg.egov.bg/segment/R-09998' as R09998
                )
                SELECT
                       SUM(tb1.GrandAmount) as 'BfpAmount'
                FROM ( pebPdebGa.i.value('.', 'MONEY') as 'GrandAmount'
                      FROM ProjectVersionXmls
                      CROSS APPLY Xml.nodes('/Project/R10019:DimensionsBudgetContract/R09998:Budget/R10010:ProgrammeBudget/R10009:ProgrammeExpenseBudget') AS peb(i)
                      OUTER APPLY peb.i.nodes('R10008:ProgrammeDetailsExpenseBudget/R10007:GrandAmount') pebPdebGa(i)
                      WHERE Status = 2 and CreateDate <= @toDate) tb1";

            var result = this.SqlQuery<ProjectAmountsQueryItem>(query, selectSqlParams).ToList();

            this.unitOfWork.DbContext.Database.CommandTimeout = oldTimeout;

            return result;
        }

        private class ProjectAmountsQueryItem
        {
            public decimal? BfpAmount { get; set; }
        }

        private class ProjectAmountsItem
        {
            public decimal BfpAmount { get; set; }
        }

        private class ProgrammeSummaryReportAmountsGroupingItem
        {
            public int? ProgrammeId { get; set; }

            public int? ProcedureId { get; set; }

            public int? ContractId { get; set; }

            public int? ProgrammePriorityId { get; set; }
        }

        private class ProgrammeSummaryReportAmountsItem
        {
            public int? ProgrammeId { get; set; }

            public int? ProcedureId { get; set; }

            public int? ContractId { get; set; }

            public int? ProgrammePriorityId { get; set; }

            public decimal? ContractedEuAmount { get; set; }

            public decimal? ContractedBgAmount { get; set; }

            public decimal? ContractedBfpTotalAmount { get; set; }

            public decimal? ContractedSelfAmount { get; set; }

            public decimal? ContractedTotalAmount { get; set; }

            public decimal? ReportedEuAmount { get; set; }

            public decimal? ReportedBgAmount { get; set; }

            public decimal? ReportedBfpTotalAmount { get; set; }

            public decimal? ReportedSelfAmount { get; set; }

            public decimal? ReportedTotalAmount { get; set; }

            public decimal? ApprovedEuAmount { get; set; }

            public decimal? ApprovedBgAmount { get; set; }

            public decimal? ApprovedBfpTotalAmount { get; set; }

            public decimal? ApprovedSelfAmount { get; set; }

            public decimal? ApprovedTotalAmount { get; set; }

            public decimal? CertifiedEuAmount { get; set; }

            public decimal? CertifiedBgAmount { get; set; }

            public decimal? CertifiedBfpTotalAmount { get; set; }

            public decimal? CertifiedSelfAmount { get; set; }

            public decimal? CertifiedTotalAmount { get; set; }
        }

        private class ContractAmountDO
        {
            public int Id { get; set; }

            public int? ContractId { get; set; }

            public ContractType? ContractType { get; set; }

            public ContractExecutionStatus? ContractExecutionStatus { get; set; }

            public int? CertReportId { get; set; }

            public int? ContractReportId { get; set; }

            public int? ContractReportPaymentId { get; set; }

            public int? ProgrammeId { get; set; }

            public int? ProgrammePriorityId { get; set; }

            public string ProgrammePriorityName { get; set; }

            public int? ProcedureId { get; set; }

            public int? ContractBudgetLevel3AmountId { get; set; }

            public string ContractBudgetLevel3AmountNutsFullPath { get; set; }

            public string ContractBudgetLevel3AmountNutsFullPathName { get; set; }

            public YesNoNonApplicable? AdvancePayment { get; set; }

            public int? FinancialCorrectionId { get; set; }

            public int? ContractContractId { get; set; }

            public decimal? ContractedEuAmount { get; set; }

            public decimal? ContractedBgAmount { get; set; }

            public decimal? ContractedBfpTotalAmount { get; set; }

            public decimal? ContractedSelfAmount { get; set; }

            public decimal? ContractedTotalAmount { get; set; }

            public decimal? ReportedEuAmount { get; set; }

            public decimal? ReportedBgAmount { get; set; }

            public decimal? ReportedBfpTotalAmount { get; set; }

            public decimal? ReportedSelfAmount { get; set; }

            public decimal? ReportedTotalAmount { get; set; }

            public decimal? ApprovedEuAmount { get; set; }

            public decimal? ApprovedBgAmount { get; set; }

            public decimal? ApprovedBfpTotalAmount { get; set; }

            public decimal? ApprovedSelfAmount { get; set; }

            public decimal? ApprovedTotalAmount { get; set; }

            public decimal? CertifiedEuAmount { get; set; }

            public decimal? CertifiedBgAmount { get; set; }

            public decimal? CertifiedBfpTotalAmount { get; set; }

            public decimal? CertifiedSelfAmount { get; set; }

            public decimal? CertifiedTotalAmount { get; set; }

            public decimal? CorrectedEuAmount { get; set; }

            public decimal? CorrectedBgAmount { get; set; }

            public decimal? CorrectedBfpTotalAmount { get; set; }

            public decimal? CorrectedSelfAmount { get; set; }

            public decimal? CorrectedTotalAmount { get; set; }

            public decimal? UnapprovedEuAmount { get; set; }

            public decimal? UnapprovedBgAmount { get; set; }

            public decimal? UnapprovedBfpTotalAmount { get; set; }

            public decimal? UnapprovedSelfAmount { get; set; }

            public decimal? UnapprovedTotalAmount { get; set; }

            public decimal? InitialContractedEuAmount { get; set; }

            public decimal? InitialContractedBgAmount { get; set; }

            public decimal? InitialContractedBfpTotalAmount { get; set; }

            public decimal? InitialContractedSelfAmount { get; set; }

            public decimal? InitialContractedTotalAmount { get; set; }
        }

        private class ContractReportCertReportNum
        {
            public int ContractReportId { get; set; }

            public int OrderNum { get; set; }
        }

        private class InnerExpenseTypesReportItem : ExpenseTypesReportItem
        {
            public int ContractRerportId { get; set; }

            public int CertReportId { get; set; }

            public int OrderNum { get; set; }
        }
    }
}
