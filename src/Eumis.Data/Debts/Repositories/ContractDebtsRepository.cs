using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Debts.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.CertReports;
using Eumis.Domain.Contracts;
using Eumis.Domain.Debts;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Domain.InterestSchemes;
using Eumis.Domain.Irregularities;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Debts.Repositories
{
    internal class ContractDebtsRepository : AggregateRepository<ContractDebt>, IContractDebtsRepository
    {
        public ContractDebtsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractDebt, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractDebt, object>>[]
                {
                    e => e.ContractDebtInterests,
                    e => e.ContractDebtPayments,
                };
            }
        }

        public IList<ContractDebtVO> GetContractDebts(int[] programmeIds, int userId)
        {
            var predicate = PredicateBuilder.True<Contract>()
                .And(c => programmeIds.Contains(c.ProgrammeId));

            var externalVerificatorContractDebts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                   join c in this.unitOfWork.DbContext.Set<Contract>() on cu.ContractId equals c.ContractId
                                                   select c;

            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate).Union(externalVerificatorContractDebts) on cd.ContractId equals c.ContractId
                    join cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>().Where(cdv => cdv.Status == ContractDebtVersionStatus.Actual) on cd.ContractDebtId equals cdv.ContractDebtId into g0
                    from cdv in g0.DefaultIfEmpty()
                    select new
                    {
                        ContractDebtId = cd.ContractDebtId,
                        ContractRegNumber = c.RegNumber,
                        CompanyName = c.CompanyName,
                        CompanyUin = c.CompanyUin,
                        CompanyUinType = c.CompanyUinType,
                        RegNumber = cd.RegNumber,
                        RegDate = cd.RegDate,
                        ExecutionStatus = cd.ExecutionStatus,
                        Status = cd.Status,
                        ModifyDate = (DateTime?)cdv.ModifyDate,
                        EuAmount = cdv.EuAmount,
                        BgAmount = cdv.BgAmount,
                        TotalAmount = cdv.TotalAmount,
                    })
                .Distinct()
                .ToList()
                .Select(c => new ContractDebtVO
                {
                    ContractDebtId = c.ContractDebtId,
                    ContractRegNumber = c.ContractRegNumber,
                    CompanyName = string.Format("{0} ({1}: {2})", c.CompanyName, c.CompanyUinType.GetEnumDescription(), c.CompanyUin),
                    RegNumber = c.RegNumber,
                    RegDate = c.RegDate,
                    ExecutionStatus = c.ExecutionStatus,
                    IsRemoved = c.Status == ContractDebtStatus.Removed,
                    ModifyDate = c.ModifyDate,
                    EuAmount = c.EuAmount,
                    BgAmount = c.BgAmount,
                    TotalAmount = c.TotalAmount,
                })
                .ToList();
        }

        public IList<ContractDebtReportVO> GetContractDebtReport(int[] programmeIds, DateTime dateFrom, DateTime dateTo)
        {
            var maxBeginVesrsion =
                from cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>()
                where cdv.ActivationDate < dateFrom && cdv.Status != ContractDebtVersionStatus.Draft
                group cdv by cdv.ContractDebtId into gr
                let lastActivationDate = gr.Max(g => g.ActivationDate)
                from cdv in gr
                where cdv.ActivationDate == lastActivationDate
                select new
                {
                    ContractDebtId = cdv.ContractDebtId,
                    ExecutionStatus = cdv.ExecutionStatus,
                    OrderNum = cdv.OrderNum,
                    EuAmount = cdv.EuAmount,
                    BgAmount = cdv.BgAmount,
                    TotalAmount = cdv.TotalAmount,
                };

            var firstDebtVesrsion =
                from cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>()
                where cdv.Status != ContractDebtVersionStatus.Draft
                group cdv by cdv.ContractDebtId into gr
                let firstActivationDate = gr.Min(g => g.ActivationDate)
                from cdv in gr
                where cdv.ActivationDate == firstActivationDate
                select new
                {
                    ContractDebtId = cdv.ContractDebtId,
                    ExecutionStatus = cdv.ExecutionStatus,
                    OrderNum = cdv.OrderNum,
                    EuAmount = cdv.EuAmount,
                    BgAmount = cdv.BgAmount,
                    TotalAmount = cdv.TotalAmount,
                };

            var maxCurrentVesrsion =
                from cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>()
                where cdv.ActivationDate >= dateFrom && cdv.ActivationDate <= dateTo && cdv.Status != ContractDebtVersionStatus.Draft
                group cdv by cdv.ContractDebtId into gr
                let lastActivationDate = gr.Max(g => g.ActivationDate)
                from cdv in gr
                where cdv.ActivationDate == lastActivationDate
                select new
                {
                    ContractDebtId = cdv.ContractDebtId,
                    OrderNum = cdv.OrderNum,
                    EuAmount = cdv.EuAmount,
                    BgAmount = cdv.BgAmount,
                    TotalAmount = cdv.TotalAmount,
                };

            var beginInterest =
                from cdi in this.unitOfWork.DbContext.Set<ContractDebtInterest>()
                where cdi.DateTo < dateFrom
                group cdi by new
                {
                    cdi.ContractDebtId,
                }
                into gr
                select new
                {
                    gr.Key.ContractDebtId,
                    EuInterestAmount = (decimal?)gr.Sum(cdi => cdi.EuInterestAmount),
                    BgInterestAmount = (decimal?)gr.Sum(cdi => cdi.BgInterestAmount),
                    TotalInterestAmount = (decimal?)gr.Sum(cdi => cdi.TotalInterestAmount),
                };

            var currentInterest =
                from cdi in this.unitOfWork.DbContext.Set<ContractDebtInterest>()
                where cdi.DateFrom >= dateFrom && cdi.DateTo <= dateTo
                group cdi by new
                {
                    cdi.ContractDebtId,
                }
                into gr
                select new
                {
                    gr.Key.ContractDebtId,
                    EuInterestAmount = (decimal?)gr.Sum(cdi => cdi.EuInterestAmount),
                    BgInterestAmount = (decimal?)gr.Sum(cdi => cdi.BgInterestAmount),
                    TotalInterestAmount = (decimal?)gr.Sum(cdi => cdi.TotalInterestAmount),
                };

            var reimbursementDeduction =
                from dra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                where dra.ReimbursementDate >= dateFrom && dra.ReimbursementDate <= dateTo
                      && dra.Status == ReimbursedAmountStatus.Entered && dra.Reimbursement == Reimbursement.Deduction
                group dra by new
                {
                    dra.ContractDebtId,
                }
                into gr
                select new
                {
                    gr.Key.ContractDebtId,
                    ReimbursementDate = gr.Max(dra => dra.ReimbursementDate),
                    PrincipalBfpEuAmount = gr.Sum(dra => dra.PrincipalBfp.EuAmount),
                    PrincipalBfpBgAmount = gr.Sum(dra => dra.PrincipalBfp.BgAmount),
                    PrincipalBfpTotalAmount = gr.Sum(dra => dra.PrincipalBfp.TotalAmount),
                    InterestBfpEuAmount = gr.Sum(dra => dra.InterestBfp.EuAmount),
                    InterestBfpBgAmount = gr.Sum(dra => dra.InterestBfp.BgAmount),
                    InterestBfpTotalAmount = gr.Sum(dra => dra.InterestBfp.TotalAmount),
                };

            var reimbursementBank =
                from dra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                where dra.ReimbursementDate >= dateFrom && dra.ReimbursementDate <= dateTo
                && dra.Status == ReimbursedAmountStatus.Entered
                && (dra.Reimbursement == Reimbursement.Bank || dra.Reimbursement == Reimbursement.DistributedLimitDeduction)
                group dra by new
                {
                    dra.ContractDebtId,
                }
                into gr
                select new
                {
                    gr.Key.ContractDebtId,
                    ReimbursementDate = gr.Max(dra => dra.ReimbursementDate),
                    PrincipalBfpEuAmount = gr.Sum(dra => dra.PrincipalBfp.EuAmount),
                    PrincipalBfpBgAmount = gr.Sum(dra => dra.PrincipalBfp.BgAmount),
                    PrincipalBfpTotalAmount = gr.Sum(dra => dra.PrincipalBfp.TotalAmount),
                    InterestBfpEuAmount = gr.Sum(dra => dra.InterestBfp.EuAmount),
                    InterestBfpBgAmount = gr.Sum(dra => dra.InterestBfp.BgAmount),
                    InterestBfpTotalAmount = gr.Sum(dra => dra.InterestBfp.TotalAmount),
                };

            var beginReimbursement =
                from dra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                where dra.ReimbursementDate < dateFrom
                && ((dra.Status == ReimbursedAmountStatus.Entered && dra.Reimbursement == Reimbursement.Bank)
                || (dra.Status == ReimbursedAmountStatus.Entered && dra.Reimbursement == Reimbursement.Deduction)
                || (dra.Status == ReimbursedAmountStatus.Entered && dra.Reimbursement == Reimbursement.DistributedLimitDeduction))
                group dra by new
                {
                    dra.ContractDebtId,
                }
                into gr
                select new
                {
                    gr.Key.ContractDebtId,
                    ReimbursementDate = gr.Max(dra => dra.ReimbursementDate),
                    PrincipalBfpEuAmount = gr.Sum(dra => dra.PrincipalBfp.EuAmount),
                    PrincipalBfpBgAmount = gr.Sum(dra => dra.PrincipalBfp.BgAmount),
                    PrincipalBfpTotalAmount = gr.Sum(dra => dra.PrincipalBfp.TotalAmount),
                    InterestBfpEuAmount = gr.Sum(dra => dra.InterestBfp.EuAmount),
                    InterestBfpBgAmount = gr.Sum(dra => dra.InterestBfp.BgAmount),
                    InterestBfpTotalAmount = gr.Sum(dra => dra.InterestBfp.TotalAmount),
                };

            var paymentCertReports =
                from cdp in this.unitOfWork.DbContext.Set<ContractDebtPayment>()
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cdp.ContractReportPaymentId equals crp.ContractReportPaymentId
                join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crp.ContractReportId equals csd.ContractReportId
                join cr in this.unitOfWork.DbContext.Set<CertReport>() on csd.CertReportId equals cr.CertReportId
                where
                    crp.PaymentType != ContractReportPaymentType.AdvanceVerification
                    && crp.Status == ContractReportPaymentStatus.Actual
                    && cr.Status != CertReportStatus.Draft
                select new
                {
                    cdp.ContractDebtId,
                    crp.VersionNum,
                    cr.OrderNum,
                };

            var advancePaymentCertReports =
                from cdp in this.unitOfWork.DbContext.Set<ContractDebtPayment>()
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cdp.ContractReportPaymentId equals crp.ContractReportPaymentId
                join crapa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>() on crp.ContractReportId equals crapa.ContractReportId
                where crapa.CertReportId != null
                join cr in this.unitOfWork.DbContext.Set<CertReport>() on crapa.CertReportId equals cr.CertReportId
                where
                    crp.PaymentType == ContractReportPaymentType.AdvanceVerification
                    && crp.Status == ContractReportPaymentStatus.Actual
                    && crapa.Status == ContractReportAdvancePaymentAmountStatus.Ended
                    && cr.Status != CertReportStatus.Draft
                select new
                {
                    cdp.ContractDebtId,
                    crp.VersionNum,
                    cr.OrderNum,
                };

            paymentCertReports = paymentCertReports.Concat(advancePaymentCertReports);

            var allPaymentsCertReports =
                from pcr in paymentCertReports
                group pcr by pcr.ContractDebtId into gr
                select new
                {
                    ContractDebtId = gr.Key,
                    Payments = gr.ToList().Distinct(),
                };

            var reportPaymentCertReport =
                from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                join crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>() on cd.FinancialCorrectionId equals crc.FinancialCorrectionId
                join cr in this.unitOfWork.DbContext.Set<CertReport>() on crc.CertReportId equals cr.CertReportId
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crc.ContractReportPaymentId equals crp.ContractReportPaymentId
                where
                    cd.FinancialCorrectionId != null && crc.FinancialCorrectionId != null
                    && cr.Status != CertReportStatus.Draft && crp.Status == ContractReportPaymentStatus.Actual
                    && crc.Status == ContractReportCorrectionStatus.Entered
                select new
                {
                    cd.ContractDebtId,
                    cr.OrderNum,
                    crp.VersionNum,
                };

            var correctionCSDPaymentCertReport =
                from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on cd.FinancialCorrectionId equals csd.FinancialCorrectionId
                join cr in this.unitOfWork.DbContext.Set<CertReport>() on csd.CertReportId equals cr.CertReportId
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on csd.ContractReportId equals crp.ContractReportId
                where cr.Status != CertReportStatus.Draft && crp.Status == ContractReportPaymentStatus.Actual
                select new
                {
                    cd.ContractDebtId,
                    cr.OrderNum,
                    crp.VersionNum,
                };

            reportPaymentCertReport = reportPaymentCertReport.Concat(correctionCSDPaymentCertReport);

            var allCorrectionPaymentCertReport =
                from rpcr in reportPaymentCertReport
                group rpcr by rpcr.ContractDebtId into gr
                select new
                {
                    ContractDebtId = gr.Key,
                    CorrectionPaymentCertReports = gr.ToList().Distinct(),
                };

            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cd.ContractId equals c.ContractId
                    join fdb in firstDebtVesrsion on cd.ContractDebtId equals fdb.ContractDebtId
                    join irr in this.unitOfWork.DbContext.Set<Irregularity>() on cd.IrregularityId equals irr.IrregularityId into g0
                    from irr in g0.DefaultIfEmpty()
                    join fc in this.unitOfWork.DbContext.Set<FinancialCorrection>() on cd.FinancialCorrectionId equals fc.FinancialCorrectionId into g1
                    from fc in g1.DefaultIfEmpty()
                    join bp in maxBeginVesrsion on cd.ContractDebtId equals bp.ContractDebtId into g2
                    from bp in g2.DefaultIfEmpty()
                    join bi in beginInterest on cd.ContractDebtId equals bi.ContractDebtId into g3
                    from bi in g3.DefaultIfEmpty()
                    join cp in maxCurrentVesrsion on cd.ContractDebtId equals cp.ContractDebtId into g4
                    from cp in g4.DefaultIfEmpty()
                    join i in currentInterest on cd.ContractDebtId equals i.ContractDebtId into g5
                    from i in g5.DefaultIfEmpty()
                    join rd in reimbursementDeduction on cd.ContractDebtId equals rd.ContractDebtId into g6
                    from rd in g6.DefaultIfEmpty()
                    join rb in reimbursementBank on cd.ContractDebtId equals rb.ContractDebtId into g7
                    from rb in g7.DefaultIfEmpty()
                    join br in beginReimbursement on cd.ContractDebtId equals br.ContractDebtId into g8
                    from br in g8.DefaultIfEmpty()
                    join acr in allPaymentsCertReports on cd.ContractDebtId equals acr.ContractDebtId into g9
                    from acr in g9.DefaultIfEmpty()
                    join acp in allCorrectionPaymentCertReport on cd.ContractDebtId equals acp.ContractDebtId into g10
                    from acp in g10.DefaultIfEmpty()
                    where programmeIds.Contains(c.ProgrammeId) && cd.Status == ContractDebtStatus.Entered
                    && cd.RegDate < dateTo

                    let isInCurrentPeriod = cd.RegDate <= dateTo && cd.RegDate >= dateFrom

                    select new ContractDebtReportVO
                    {
                        ContractDebtId = cd.ContractDebtId,
                        ExecutionStatus = bp.ExecutionStatus ?? fdb.ExecutionStatus,
                        RegNumber = cd.RegNumber,
                        Company = c.CompanyName,
                        ContractNumber = c.RegNumber,
                        RegDate = cd.RegDate,
                        ModifyDate = cd.ModifyDate,
                        IrregularityNum = irr.RegNumber,
                        FinancialCorrectionNum = fc.OrderNum,

                        NewDebtPrincipalBfpEuAmount = isInCurrentPeriod ? (cp.EuAmount ?? (fdb.EuAmount ?? 0)) : 0,
                        NewDebtPrincipalBfpBgAmount = isInCurrentPeriod ? (cp.BgAmount ?? (fdb.BgAmount ?? 0)) : 0,
                        NewDebtPrincipalTotalAmount = isInCurrentPeriod ? (cp.TotalAmount ?? (fdb.TotalAmount ?? 0)) : 0,

                        DebtPrincipalBfpEuAmount = (bp.EuAmount ?? ((!isInCurrentPeriod ? fdb.EuAmount : 0) ?? 0)) - (br.PrincipalBfpEuAmount ?? 0),
                        DebtPrincipalBfpBgAmount = (bp.BgAmount ?? ((!isInCurrentPeriod ? fdb.BgAmount : 0) ?? 0)) - (br.PrincipalBfpBgAmount ?? 0),
                        DebtPrincipalTotalAmount = (bp.TotalAmount ?? ((!isInCurrentPeriod ? fdb.TotalAmount : 0) ?? 0)) - (br.PrincipalBfpTotalAmount ?? 0),

                        DebtInterestBfpEuAmount = (bi.EuInterestAmount ?? 0) - (br.InterestBfpEuAmount ?? 0),
                        DebtInterestBfpBgAmount = (bi.BgInterestAmount ?? 0) - (br.InterestBfpBgAmount ?? 0),
                        DebtInterestTotalAmount = (bi.TotalInterestAmount ?? 0) - (br.InterestBfpTotalAmount ?? 0),

                        DebtTotalAmount =
                            (bp.TotalAmount ?? ((!isInCurrentPeriod ? fdb.TotalAmount : 0) ?? 0)) - (br.PrincipalBfpTotalAmount ?? 0) +
                            (bi.TotalInterestAmount ?? 0) - (br.InterestBfpTotalAmount ?? 0),

                        RaPrincipalBfpEuAmount = rb.PrincipalBfpEuAmount ?? 0,
                        RaPrincipalBfpBgAmount = rb.PrincipalBfpBgAmount ?? 0,
                        RaPrincipalBfpTotalAmount = rb.PrincipalBfpTotalAmount ?? 0,
                        RaInterestBfpEuAmount = rb.InterestBfpEuAmount ?? 0,
                        RaInterestBfpBgAmount = rb.InterestBfpBgAmount ?? 0,
                        RaInterestBfpTotalAmount = rb.InterestBfpTotalAmount ?? 0,

                        RaBfpTotalAmount = (rb.PrincipalBfpTotalAmount ?? 0) + (rb.InterestBfpTotalAmount ?? 0),

                        ReimbursementDate = rb.ReimbursementDate,

                        DaPrincipalBfpEuAmount = rd.PrincipalBfpEuAmount ?? 0,
                        DaPrincipalBfpBgAmount = rd.PrincipalBfpBgAmount ?? 0,
                        DaPrincipalBfpTotalAmount = rd.PrincipalBfpTotalAmount ?? 0,
                        DaInterestBfpEuAmount = rd.InterestBfpEuAmount ?? 0,
                        DaInterestBfpBgAmount = rd.InterestBfpBgAmount ?? 0,
                        DaInterestBfpTotalAmount = rd.InterestBfpTotalAmount ?? 0,

                        DaBfpTotalAmount = (rd.PrincipalBfpTotalAmount ?? 0) + (rd.InterestBfpTotalAmount ?? 0),

                        DeductionDate = rd.ReimbursementDate,

                        InterestBfpEuAmount = i.EuInterestAmount ?? 0,
                        InterestBfpBgAmount = i.BgInterestAmount ?? 0,
                        InterestTotalAmount = i.TotalInterestAmount ?? 0,

                        RemainingDebtPrincipalBfpEuAmount =
                            (bp.EuAmount ?? ((!isInCurrentPeriod ? fdb.EuAmount : 0) ?? 0)) - (br.PrincipalBfpEuAmount ?? 0) -
                            ((rb.PrincipalBfpEuAmount ?? 0) + (rd.PrincipalBfpEuAmount ?? 0)) +
                            (isInCurrentPeriod ? (cp.EuAmount ?? (fdb.EuAmount ?? 0)) : 0),
                        RemainingDebtPrincipalBfpBgAmount =
                            (bp.BgAmount ?? ((!isInCurrentPeriod ? fdb.BgAmount : 0) ?? 0)) - (br.PrincipalBfpBgAmount ?? 0) -
                            ((rb.PrincipalBfpBgAmount ?? 0) + (rd.PrincipalBfpBgAmount ?? 0)) +
                            (isInCurrentPeriod ? (cp.BgAmount ?? (fdb.BgAmount ?? 0)) : 0),
                        RemainingDebtPrincipalBfpTotalAmount =
                            (bp.TotalAmount ?? ((!isInCurrentPeriod ? fdb.TotalAmount : 0) ?? 0)) - (br.PrincipalBfpTotalAmount ?? 0) -
                            ((rb.PrincipalBfpTotalAmount ?? 0) + (rd.PrincipalBfpTotalAmount ?? 0)) +
                            (isInCurrentPeriod ? (cp.TotalAmount ?? (fdb.TotalAmount ?? 0)) : 0),
                        RemainingDebtInterestBfpEuAmount =
                            (bi.EuInterestAmount ?? 0) - (br.InterestBfpEuAmount ?? 0) -
                            ((rb.InterestBfpEuAmount ?? 0) + (rd.InterestBfpEuAmount ?? 0)) +
                            (i.EuInterestAmount ?? 0),
                        RemainingDebtInterestBfpBgAmount =
                            (bi.BgInterestAmount ?? 0) - (br.InterestBfpBgAmount ?? 0) -
                            ((rb.InterestBfpBgAmount ?? 0) + (rd.InterestBfpBgAmount ?? 0)) +
                            (i.BgInterestAmount ?? 0),
                        RemainingDebtInterestBfpTotalAmount =
                            (bi.TotalInterestAmount ?? 0) - (br.InterestBfpTotalAmount ?? 0) -
                            ((rb.InterestBfpTotalAmount ?? 0) + (rd.InterestBfpTotalAmount ?? 0)) +
                            (i.TotalInterestAmount ?? 0),
                        RemainingDebtBfpTotalAmount =
                            (bp.TotalAmount ?? ((!isInCurrentPeriod ? fdb.TotalAmount : 0) ?? 0)) - (br.PrincipalBfpTotalAmount ?? 0) -
                            ((rb.PrincipalBfpTotalAmount ?? 0) + (rd.PrincipalBfpTotalAmount ?? 0)) +
                            (isInCurrentPeriod ? (cp.TotalAmount ?? (fdb.TotalAmount ?? 0)) : 0) +
                            (bi.TotalInterestAmount ?? 0) - (br.InterestBfpTotalAmount ?? 0) -
                            ((rb.InterestBfpTotalAmount ?? 0) + (rd.InterestBfpTotalAmount ?? 0)) +
                            (i.TotalInterestAmount ?? 0),
                        PaymentsCertReports = acr
                            .Payments
                            .Select(
                                x => new ContractDebtPaymentCertReportVO()
                                {
                                    CertReportOrderNumber = x.OrderNum,
                                    PaymentOrderNumber = x.VersionNum,
                                }).ToList(),
                        CorrectionsCertReports = acp
                            .CorrectionPaymentCertReports
                            .Select(
                                x => new ContractDebtPaymentCertReportVO()
                                {
                                    CertReportOrderNumber = x.OrderNum,
                                    PaymentOrderNumber = x.VersionNum,
                                }).ToList(),
                    }).ToList();
        }

        public int GetContractId(int contractDebtId)
        {
            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                    where cd.ContractDebtId == contractDebtId
                    select cd.ContractId)
                .Single();
        }

        public IList<ContractDebtInterestVO> GetContractDebtInterests(int contractDebtId)
        {
            return (from cdi in this.unitOfWork.DbContext.Set<ContractDebtInterest>()
                    join isc in this.unitOfWork.DbContext.Set<InterestScheme>() on cdi.InterestSchemeId equals isc.InterestSchemeId
                    where cdi.ContractDebtId == contractDebtId
                    orderby cdi.OrderNum descending
                    select new ContractDebtInterestVO
                    {
                        ContractDebtInterestId = cdi.ContractDebtInterestId,
                        ContractDebtId = cdi.ContractDebtId,
                        OrderNum = cdi.OrderNum,
                        DateFrom = cdi.DateFrom,
                        DateTo = cdi.DateTo,
                        EuInterestAmount = cdi.EuInterestAmount,
                        BgInterestAmount = cdi.BgInterestAmount,
                        TotalInterestAmount = cdi.TotalInterestAmount,
                        EuAmount = cdi.EuAmount,
                        BgAmount = cdi.BgAmount,
                        TotalAmount = cdi.TotalAmount,
                        InterestScheme = isc.Name,
                    })
                .ToList();
        }

        public int GetNextContractDebtInterestOrderNum(int contractDebtId)
        {
            var lastOrderNumber = this.unitOfWork.DbContext.Set<ContractDebtInterest>()
                .Where(t => t.ContractDebtId == contractDebtId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public IList<ContractDebtVO> GetFinancialCorrectionContractDebts(int financialCorrectionId)
        {
            var predicate = PredicateBuilder.True<ContractDebt>()
                .And(cd => cd.FinancialCorrectionId == financialCorrectionId);

            return this.GetDebts(predicate);
        }

        public IList<ContractDebtVO> GetIrregularityContractDebts(int irregularityId)
        {
            var predicate = PredicateBuilder.True<ContractDebt>()
                .And(cd => cd.IrregularityId == irregularityId);

            return this.GetDebts(predicate);
        }

        private IList<ContractDebtVO> GetDebts(Expression<Func<ContractDebt, bool>> predicate)
        {
            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>().Where(predicate)
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cd.ContractId equals c.ContractId
                    join cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>() on cd.ContractDebtId equals cdv.ContractDebtId into g0
                    from cdv in g0.DefaultIfEmpty()
                    where cdv.Status == ContractDebtVersionStatus.Actual
                    select new
                    {
                        ContractDebtId = cd.ContractDebtId,
                        ContractRegNumber = c.RegNumber,
                        CompanyName = c.CompanyName,
                        CompanyUin = c.CompanyUin,
                        CompanyUinType = c.CompanyUinType,
                        RegNumber = cd.RegNumber,
                        RegDate = cd.RegDate,
                        ExecutionStatus = cd.ExecutionStatus,
                        Status = cd.Status,
                        ModifyDate = (DateTime?)cdv.ModifyDate,
                        EuAmount = cdv.EuAmount,
                        BgAmount = cdv.BgAmount,
                        TotalAmount = cdv.TotalAmount,
                    })
                .Distinct()
                .ToList()
                .Select(c => new ContractDebtVO
                {
                    ContractDebtId = c.ContractDebtId,
                    ContractRegNumber = c.ContractRegNumber,
                    CompanyName = string.Format("{0} ({1}: {2})", c.CompanyName, c.CompanyUinType.GetEnumDescription(), c.CompanyUin),
                    RegNumber = c.RegNumber,
                    RegDate = c.RegDate,
                    ExecutionStatus = c.ExecutionStatus,
                    IsRemoved = c.Status == ContractDebtStatus.Removed,
                    ModifyDate = c.ModifyDate,
                    EuAmount = c.EuAmount,
                    BgAmount = c.BgAmount,
                    TotalAmount = c.TotalAmount,
                })
                .ToList();
        }

        public ContractDebtInfoVO GetInfo(int contractDebtId)
        {
            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                    where cd.ContractDebtId == contractDebtId
                    select new ContractDebtInfoVO
                    {
                        ContractDebtId = cd.ContractDebtId,
                        ContractId = cd.ContractId,
                        ProgrammePriorityId = cd.ProgrammePriorityId,
                        Status = cd.Status,
                    }).Single();
        }

        public ContractDebtStatus GetStatus(int contractDebtId)
        {
            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                    where cd.ContractDebtId == contractDebtId
                    select cd.Status).Single();
        }

        public IList<Tuple<int, int, int>> GetContractDebtsData(int[] contractIds)
        {
            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                    join cdp in this.unitOfWork.DbContext.Set<ContractDebtPayment>() on cd.ContractDebtId equals cdp.ContractDebtId
                    where cd.Status == ContractDebtStatus.Entered &&
                        contractIds.Contains(cd.ContractId)
                    select new
                    {
                        cd.ContractId,
                        cdp.ContractReportPaymentId,
                        cd.ContractDebtId,
                    }).ToList()
                    .Select(o => Tuple.Create<int, int, int>(o.ContractId, o.ContractReportPaymentId, o.ContractDebtId))
                    .ToList();
        }

        public new void Remove(ContractDebt contractDebt)
        {
            if (contractDebt.Status != ContractDebtStatus.New)
            {
                throw new DomainValidationException("Cannot delete contract debt with status different from new.");
            }

            base.Remove(contractDebt);
        }

        public IList<ContractDebtVO> GetContractDebtsForProjectDossier(int contractId)
        {
            var totalInterestAmountsLookup = (
                from cdi in this.unitOfWork.DbContext.Set<ContractDebtInterest>()

                join cd in this.unitOfWork.DbContext.Set<ContractDebt>() on cdi.ContractDebtId equals cd.ContractDebtId into g0
                from cd in g0.DefaultIfEmpty()

                join cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>() on cd.ContractDebtId equals cdv.ContractDebtId into g1
                from cdv in g1.DefaultIfEmpty()

                where cd.ContractId == contractId && cd.Status != ContractDebtStatus.New
                group cdi by cdv.ContractDebtVersionId into g
                select new
                {
                    ContractDebtVersionId = g.Key,
                    TotalInterestAmount = g.Sum(cdi => cdi.TotalInterestAmount),
                })
                .ToLookup(cdv => cdv.ContractDebtVersionId);

            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cd.ContractId equals c.ContractId
                    join cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>() on cd.ContractDebtId equals cdv.ContractDebtId into g0
                    from cdv in g0.DefaultIfEmpty()

                    join cr in this.unitOfWork.DbContext.Set<CertReport>() on cd.CertReportId equals cr.CertReportId into g1
                    from cr in g1.DefaultIfEmpty()

                    where cd.ContractId == contractId && cd.Status != ContractDebtStatus.New && cdv.Status == ContractDebtVersionStatus.Actual && cr.Status == CertReportStatus.Approved
                    select new
                    {
                        ContractDebtId = cd.ContractDebtId,
                        OrderNum = cdv.OrderNum,
                        ContractDebtVersionId = cdv.ContractDebtVersionId,
                        ContractRegNumber = c.RegNumber,
                        CompanyName = c.CompanyName,
                        CompanyUin = c.CompanyUin,
                        CompanyUinType = c.CompanyUinType,
                        RegNumber = cd.RegNumber,
                        RegDate = cd.RegDate,
                        ExecutionStatus = cd.ExecutionStatus,
                        Status = cd.Status,
                        ModifyDate = (DateTime?)cdv.ModifyDate,
                        TotalAmount = cdv.TotalAmount,
                        CertReportNumber = cr.CertReportNumber,
                    })
                .ToList()
                .Select(c => new ContractDebtVO
                {
                    ContractDebtId = c.ContractDebtId,
                    OrderNum = c.OrderNum,
                    ContractRegNumber = c.ContractRegNumber,
                    CompanyName = string.Format("{0} ({1}: {2})", c.CompanyName, c.CompanyUinType.GetEnumDescription(), c.CompanyUin),
                    RegNumber = c.RegNumber,
                    ExecutionStatus = c.ExecutionStatus,
                    RegDate = c.RegDate,
                    IsRemoved = c.Status == ContractDebtStatus.Removed,
                    ModifyDate = c.ModifyDate,
                    TotalAmount = c.TotalAmount,
                    TotalInterestAmount = totalInterestAmountsLookup[c.ContractDebtVersionId].First().TotalInterestAmount,
                    CertReportNumber = c.CertReportNumber,
                })
                .ToList();
        }

        public bool HasCertContractReportContractDebts(int certReportId)
        {
            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                    where cd.CertReportId == certReportId
                    select cd.ContractDebtId).Any();
        }
    }
}
