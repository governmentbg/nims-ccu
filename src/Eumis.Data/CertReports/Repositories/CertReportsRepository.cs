using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.CertReports.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.CertReports;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Domain.CertReports.ViewObjects.SummaryVOs;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.Debts;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.CertReports.Repositories
{
    internal partial class CertReportsRepository : AggregateRepository<CertReport>, ICertReportsRepository
    {
        public CertReportsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<CertReport, object>>[] Includes
        {
            get
            {
                return new Expression<Func<CertReport, object>>[]
                {
                    c => c.CertReportDocuments.Select(t => t.File),
                    c => c.CertReportCertificationDocuments.Select(t => t.File),
                    c => c.CertReportAttachedCertReports,
                };
            }
        }

        public IList<CertReportVO> GetCertReports(int[] programmeIds)
        {
            var groupedAmounts = this.GetCertReportAmountsGroupedByCertReport();

            return (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on cr.ProgrammeId equals p.MapNodeId

                    join ga in groupedAmounts on cr.CertReportId equals ga.CertReportId into g0
                    from ga in g0.DefaultIfEmpty()

                    join crs in this.unitOfWork.DbContext.Set<CertReportSnapshot>() on cr.CertReportId equals crs.CertReportId into g1
                    from crs in g1.DefaultIfEmpty()

                    where programmeIds.Contains(cr.ProgrammeId)
                    orderby cr.ProgrammeId, cr.OrderNum, cr.OrderVersionNum descending
                    select new CertReportVO
                    {
                        CertReportId = cr.CertReportId,
                        ProgrammeId = cr.ProgrammeId,
                        ProgrammeName = p.Name,
                        OrderNum = cr.OrderNum,
                        OrderVersionNum = cr.OrderVersionNum,
                        RegDate = cr.RegDate,
                        DateFrom = cr.DateFrom,
                        DateTo = cr.DateTo,
                        StatusDesc = cr.Status,
                        Status = cr.Status,
                        Type = cr.Type,
                        CreateDate = cr.CreateDate,
                        ApprovalDate = cr.ApprovalDate,
                        CertReportOriginId = cr.CertReportOriginId,
                        ApprovedBfpTotalAmount = ga != null ? ga.ApprovedBfpTotalAmount : (crs != null ? crs.ApprovedBfpTotalAmount : null),
                        ApprovedSelfAmount = ga != null ? ga.ApprovedSelfAmount : (crs != null ? crs.ApprovedSelfAmount : null),
                        CertifiedBfpTotalAmount = ga != null ? ga.CertifiedBfpTotalAmount : (crs != null ? crs.CertifiedBfpTotalAmount : null),
                        CertifiedSelfAmount = ga != null ? ga.CertifiedSelfAmount : (crs != null ? crs.CertifiedSelfAmount : null),
                        CertReportNumber = cr.CertReportNumber,
                    })
                .ToList();
        }

        public IList<CertReportVO> GetCertReportChecksCertReports(int[] programmeIds)
        {
            var groupedAmounts = this.GetCertReportAmountsGroupedByCertReport();

            return (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on cr.ProgrammeId equals p.MapNodeId

                    join ga in groupedAmounts on cr.CertReportId equals ga.CertReportId into g0
                    from ga in g0.DefaultIfEmpty()

                    join crs in this.unitOfWork.DbContext.Set<CertReportSnapshot>() on cr.CertReportId equals crs.CertReportId into g1
                    from crs in g1.DefaultIfEmpty()

                    where programmeIds.Contains(cr.ProgrammeId) && cr.Status != CertReportStatus.Draft

                    orderby cr.ProgrammeId, cr.OrderNum, cr.OrderVersionNum descending
                    select new CertReportVO
                    {
                        CertReportId = cr.CertReportId,
                        ProgrammeId = cr.ProgrammeId,
                        ProgrammeName = p.Name,
                        OrderNum = cr.OrderNum,
                        OrderVersionNum = cr.OrderVersionNum,
                        RegDate = cr.RegDate,
                        DateFrom = cr.DateFrom,
                        DateTo = cr.DateTo,
                        StatusDesc = cr.Status,
                        Status = cr.Status,
                        Type = cr.Type,
                        CreateDate = cr.CreateDate,
                        ApprovalDate = cr.ApprovalDate,
                        CertReportOriginId = cr.CertReportOriginId,
                        ApprovedBfpTotalAmount = ga != null ? ga.ApprovedBfpTotalAmount : (crs != null ? crs.ApprovedBfpTotalAmount : null),
                        ApprovedSelfAmount = ga != null ? ga.ApprovedSelfAmount : (crs != null ? crs.ApprovedSelfAmount : null),
                        CertifiedBfpTotalAmount = ga != null ? ga.CertifiedBfpTotalAmount : (crs != null ? crs.CertifiedBfpTotalAmount : null),
                        CertifiedSelfAmount = ga != null ? ga.CertifiedSelfAmount : (crs != null ? crs.CertifiedSelfAmount : null),
                        CertReportNumber = cr.CertReportNumber,
                    })
                .ToList();
        }

        public int GetNextOrderNum(int programmeId)
        {
            var lastOrderNumber = this.Set()
                .Where(t => t.ProgrammeId == programmeId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public int GetProgrammeId(int certReportId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                    where cr.CertReportId == certReportId
                    select cr.ProgrammeId).Single();
        }

        public int GetOrderNum(int certReportId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                    where cr.CertReportId == certReportId
                    select cr.OrderNum).Single();
        }

        public IList<CertReportVO> GetCertReportAttachedCertReports(int certReportId)
        {
            return (from cracr in this.unitOfWork.DbContext.Set<CertReportAttachedCertReport>()
                    join cr in this.unitOfWork.DbContext.Set<CertReport>() on cracr.AttachedCertReportId equals cr.CertReportId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on cr.ProgrammeId equals p.MapNodeId
                    where cracr.CertReportId == certReportId
                    select new CertReportVO
                    {
                        CertReportId = cr.CertReportId,
                        AttachedCertReportId = cracr.AttachedCertReportId,
                        ProgrammeId = cr.ProgrammeId,
                        ProgrammeName = p.Name,
                        OrderNum = cr.OrderNum,
                        OrderVersionNum = cr.OrderVersionNum,
                        RegDate = cr.RegDate,
                        DateFrom = cr.DateFrom,
                        DateTo = cr.DateTo,
                        StatusDesc = cr.Status,
                        Status = cr.Status,
                        Type = cr.Type,
                        CreateDate = cr.CreateDate,
                        CertReportOriginId = cr.CertReportOriginId,
                    })
                .ToList();
        }

        public IList<CertReportVO> GetCertReportsForAttachedCertReports(int certReportId)
        {
            var programmeId = (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                               where cr.CertReportId == certReportId
                               select cr.ProgrammeId)
                            .Single();

            var subquery = (from cracr in this.unitOfWork.DbContext.Set<CertReportAttachedCertReport>()
                            where cracr.CertReportId == certReportId
                            select cracr.AttachedCertReportId)
                    .Distinct();

            return (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on cr.ProgrammeId equals p.MapNodeId
                    where cr.ProgrammeId == programmeId && !subquery.Contains(cr.CertReportId) && CertReport.FinalStatuses.Contains(cr.Status)
                    select new CertReportVO()
                    {
                        CertReportId = cr.CertReportId,
                        ProgrammeId = cr.ProgrammeId,
                        ProgrammeName = p.Name,
                        OrderNum = cr.OrderNum,
                        OrderVersionNum = cr.OrderVersionNum,
                        RegDate = cr.RegDate,
                        DateFrom = cr.DateFrom,
                        DateTo = cr.DateTo,
                        StatusDesc = cr.Status,
                        Status = cr.Status,
                        Type = cr.Type,
                        CreateDate = cr.CreateDate,
                        CertReportOriginId = cr.CertReportOriginId,
                    })
                .ToList();
        }

        public IList<ContractDebtVO> GetCertReportContractDebts(int certReportId)
        {
            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cd.ContractId equals c.ContractId
                    join cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>() on cd.ContractDebtId equals cdv.ContractDebtId

                    where cd.CertReportId == certReportId && cdv.Status == ContractDebtVersionStatus.Actual
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

        public IList<ContractDebtVO> GetContractDebtsForCertReport(int certReportId)
        {
            var programmeId = this.GetProgrammeId(certReportId);

            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cd.ContractId equals c.ContractId
                    join cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>() on cd.ContractDebtId equals cdv.ContractDebtId

                    where cd.Status == ContractDebtStatus.Entered &&
                          cdv.Status == ContractDebtVersionStatus.Actual &&
                          c.ProgrammeId == programmeId &&
                          cd.CertReportId == null
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

        public IList<DebtReimbursedAmountVO> GetCertReportDebtReimbursedAmounts(int certReportId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                    join cd in this.unitOfWork.DbContext.Set<ContractDebt>() on ra.ContractDebtId equals cd.ContractDebtId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on ra.ProgrammeId equals pr.MapNodeId

                    where ra.CertReportId == certReportId
                    orderby ra.CreateDate descending
                    select new DebtReimbursedAmountVO
                    {
                        AmountId = ra.ReimbursedAmountId,
                        ProgrammeName = pr.Name,
                        DebtRegNumber = cd.RegNumber,
                        RegNumber = ra.RegNumber,
                        StatusDescr = ra.Status,
                        Status = ra.Status,
                        Type = ra.Type,
                        Reimbursement = ra.Reimbursement,
                        ReimbursementDate = ra.ReimbursementDate,
                    }).ToList();
        }

        public IList<DebtReimbursedAmountVO> GetDebtReimbursedAmountsForCertReport(int certReportId)
        {
            var programmeId = this.GetProgrammeId(certReportId);

            return (from ra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                    join cd in this.unitOfWork.DbContext.Set<ContractDebt>() on ra.ContractDebtId equals cd.ContractDebtId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on ra.ProgrammeId equals pr.MapNodeId

                    where ra.Status == ReimbursedAmountStatus.Entered &&
                          ra.ProgrammeId == programmeId &&
                          ra.CertReportId == null &&
                          cd.CertReportId.HasValue &&
                          cd.Status == ContractDebtStatus.Entered
                    orderby ra.CreateDate descending
                    select new DebtReimbursedAmountVO
                    {
                        AmountId = ra.ReimbursedAmountId,
                        ProgrammeName = pr.Name,
                        DebtRegNumber = cd.RegNumber,
                        RegNumber = ra.RegNumber,
                        StatusDescr = ra.Status,
                        Status = ra.Status,
                        Type = ra.Type,
                        Reimbursement = ra.Reimbursement,
                        ReimbursementDate = ra.ReimbursementDate,
                    }).ToList();
        }

        public IList<CertReportDocumentsVO> GetCertReportDocuments(int certReportId)
        {
            return (from crd in this.unitOfWork.DbContext.Set<CertReportDocument>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on crd.BlobKey equals b.Key into g1
                    from b in g1.DefaultIfEmpty()
                    where crd.CertReportId == certReportId
                    select new CertReportDocumentsVO
                    {
                        CertReportId = crd.CertReportId,
                        CertReportDocumentId = crd.CertReportDocumentId,
                        Name = crd.Name,
                        Description = crd.Description,
                        File = (b.Key == null) ? null : new FileVO
                        {
                            Key = b.Key,
                            Name = b.FileName,
                        },
                    }).ToList();
        }

        public IList<CertReportCertificationDocumentsVO> GetCertReportCertificationDocuments(int certReportId)
        {
            return (from crd in this.unitOfWork.DbContext.Set<CertReportCertificationDocument>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on crd.BlobKey equals b.Key into g1
                    from b in g1.DefaultIfEmpty()
                    where crd.CertReportId == certReportId
                    select new CertReportCertificationDocumentsVO
                    {
                        CertReportId = crd.CertReportId,
                        CertReportCertificationDocumentId = crd.CertReportCertificationDocumentId,
                        Name = crd.Name,
                        Description = crd.Description,
                        File = (b.Key == null) ? null : new FileVO
                        {
                            Key = b.Key,
                            Name = b.FileName,
                        },
                    }).ToList();
        }

        public IList<CertReportVO> GetFinancialCorrectionCertReports(int financialCorrectionId)
        {
            var certReportsFromCSDs = from crbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                                      where crbi.FinancialCorrectionId == financialCorrectionId && crbi.CertReportId != null
                                      select crbi.CertReportId;

            var certReportsFromCCSDs = from crcbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                                       join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crcbi.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                                       join crad in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>() on crfc.ContractReportFinancialCorrectionId equals crad.ContractReportFinancialCorrectionId

                                       where crcbi.FinancialCorrectionId == financialCorrectionId && crcbi.CertReportId != null
                                       select crcbi.CertReportId;

            var certReportsFromCCSDs2 = from crcbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                                        where crcbi.FinancialCorrectionId == financialCorrectionId && crcbi.CertReportId != null
                                        select crcbi.CertReportId;

            var unionCertReports = certReportsFromCSDs.Concat(certReportsFromCCSDs).Concat(certReportsFromCCSDs2);

            return (from ucr in unionCertReports
                    join cr in this.unitOfWork.DbContext.Set<CertReport>() on ucr equals cr.CertReportId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on cr.ProgrammeId equals p.MapNodeId
                    select new CertReportVO
                    {
                        CertReportId = cr.CertReportId,
                        ProgrammeId = cr.ProgrammeId,
                        ProgrammeName = p.Name,
                        OrderNum = cr.OrderNum,
                        OrderVersionNum = cr.OrderVersionNum,
                        RegDate = cr.RegDate,
                        DateFrom = cr.DateFrom,
                        DateTo = cr.DateTo,
                        StatusDesc = cr.Status,
                        Status = cr.Status,
                        Type = cr.Type,
                        CreateDate = cr.CreateDate,
                        CertReportOriginId = cr.CertReportOriginId,
                    })
                .ToList();
        }

        public IList<CertReportPaymentVO> GetCertReportPayments(int certReportId)
        {
            var financialCSDBudgetItems = this.GetCertReportFinancialCSDBudgetItems()
                .Where(t => t.CertReportId == certReportId)
                .ToList();

            var contractReportIds = financialCSDBudgetItems
                .Select(t => t.ContractReportId.Value)
                .Distinct()
                .ToArray();

            var amounts =
                (from bi in financialCSDBudgetItems
                 where bi.AdvancePayment != YesNoNonApplicable.Yes
                 group new
                 {
                     bi.ApprovedBfpTotalAmount,
                     bi.ApprovedSelfAmount,
                     bi.CertifiedBfpTotalAmount,
                     bi.CertifiedSelfAmount,
                 }
                 by bi.ContractReportId into g
                 select new
                 {
                     ContractReportId = g.Key.Value,
                     ApprovedBfpTotalAmount = g.Sum(t => t.ApprovedBfpTotalAmount),
                     ApprovedSelfAmount = g.Sum(t => t.ApprovedSelfAmount),
                     CertifiedBfpTotalAmount = g.Sum(t => t.CertifiedBfpTotalAmount),
                     CertifiedSelfAmount = g.Sum(t => t.CertifiedSelfAmount),
                 })
                .ToDictionary(t => t.ContractReportId);

            var res =
                (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                 join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cr.ContractReportId equals crp.ContractReportId
                 join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                 join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                 where contractReportIds.Contains(cr.ContractReportId) &&
                     crp.Status == ContractReportPaymentStatus.Actual

                 select new
                 {
                     ContractReportPaymentId = crp.ContractReportPaymentId,
                     PaymentType = crp.PaymentType,
                     RequestedAmount = crp.RequestedAmount,
                     PaymentRegDate = crp.RegDate,
                     PaymentVersionNum = crp.VersionNum,
                     ContractReportId = cr.ContractReportId,
                     ContractId = cr.ContractId,
                     Gid = cr.Gid,
                     ReportType = cr.ReportType,
                     OrderNum = cr.OrderNum,
                     CheckedDate = cr.CheckedDate,
                     Status = cr.Status,
                     StatusNote = cr.StatusNote,
                     Source = cr.Source,
                     SubmitDate = cr.SubmitDate,
                     SubmitDeadline = cr.SubmitDeadline,
                     DateFrom = crp.DateFrom,
                     DateTo = crp.DateTo,
                     ProcedureName = p.Name,
                     ContractName = c.Name,
                     ContractRegNum = c.RegNumber,
                     ProcedureId = p.ProcedureId,
                 })
                .ToList();

            return (from r in res
                    let amount = amounts.ContainsKey(r.ContractReportId) ? amounts[r.ContractReportId] : null
                    select new CertReportPaymentVO
                    {
                        ContractReportPaymentId = r.ContractReportPaymentId,
                        PaymentType = r.PaymentType,
                        RequestedAmount = r.RequestedAmount,
                        PaymentRegDate = r.PaymentRegDate,
                        PaymentVersionNum = r.PaymentVersionNum,
                        ContractReportId = r.ContractReportId,
                        ContractId = r.ContractId,
                        Gid = r.Gid,
                        ReportType = r.ReportType,
                        OrderNum = r.OrderNum,
                        CheckedDate = r.CheckedDate,
                        Status = r.Status,
                        StatusNote = r.StatusNote,
                        Source = r.Source,
                        SubmitDate = r.SubmitDate,
                        SubmitDeadline = r.SubmitDeadline,
                        DateFrom = r.DateFrom,
                        DateTo = r.DateTo,
                        ProcedureName = r.ProcedureName,
                        ContractName = r.ContractName,
                        ContractRegNum = r.ContractRegNum,
                        ProcedureId = r.ProcedureId,
                        ApprovedBfpTotalAmount = amount?.ApprovedBfpTotalAmount ?? 0,
                        ApprovedSelfAmount = amount?.ApprovedSelfAmount ?? 0,
                        CertifiedBfpTotalAmount = amount?.CertifiedBfpTotalAmount ?? 0,
                        CertifiedSelfAmount = amount?.CertifiedSelfAmount ?? 0,
                    }).ToList();
        }

        public IList<CertReportAdvancePaymentVO> GetCertReportAdvancePayments(int certReportId)
        {
            var advancePaymentAmounts = this.GetCertReportAdvancePaymentAmounts().Where(t => t.CertReportId == certReportId);
            var apas = from apa in advancePaymentAmounts
                       group new
                       {
                           apa.ApprovedBfpTotalAmount,
                           apa.CertifiedBfpTotalAmount,
                       }
                       by apa.ContractReportId into g
                       select new
                       {
                           ContractReportId = g.Key,
                           ApprovedBfpTotalAmount = g.Sum(t => t.ApprovedBfpTotalAmount),
                           CertifiedBfpTotalAmount = g.Sum(t => t.CertifiedBfpTotalAmount),
                       };

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cr.ContractReportId equals crp.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    join apa in apas on cr.ContractReportId equals apa.ContractReportId

                    where crp.Status == ContractReportPaymentStatus.Actual

                    select new CertReportAdvancePaymentVO
                    {
                        ContractReportPaymentId = crp.ContractReportPaymentId,
                        PaymentType = crp.PaymentType,
                        RequestedAmount = crp.RequestedAmount,
                        PaymentRegDate = crp.RegDate,
                        PaymentVersionNum = crp.VersionNum,
                        ContractReportId = cr.ContractReportId,
                        ContractId = cr.ContractId,
                        Gid = cr.Gid,
                        ReportType = cr.ReportType,
                        OrderNum = cr.OrderNum,
                        CheckedDate = cr.CheckedDate,
                        Status = cr.Status,
                        StatusNote = cr.StatusNote,
                        Source = cr.Source,
                        SubmitDate = cr.SubmitDate,
                        SubmitDeadline = cr.SubmitDeadline,
                        DateFrom = crp.DateFrom,
                        DateTo = crp.DateTo,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureId = p.ProcedureId,
                        ApprovedBfpTotalAmount = apa.ApprovedBfpTotalAmount,
                        CertifiedBfpTotalAmount = apa.CertifiedBfpTotalAmount,
                    })
                .ToList();
        }

        public IList<CertReportFinancialCorrectionVO> GetCertReportFinancialCorrections(int certReportId)
        {
            var financialCorrectionCSDs = this.GetCertReportFinancialCorrectionCSDs().Where(t => t.CertReportId == certReportId);
            var csds = from csd in financialCorrectionCSDs
                       join crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on csd.Id equals crfccsd.ContractReportFinancialCorrectionCSDId
                       group new
                       {
                           csd.ApprovedBfpTotalAmount,
                           csd.ApprovedSelfAmount,
                           csd.CertifiedBfpTotalAmount,
                           csd.CertifiedSelfAmount,
                       }
                       by crfccsd.ContractReportFinancialCorrectionId
                        into g
                       select new
                       {
                           ContractReportFinancialCorrectionId = g.Key,
                           ApprovedBfpTotalAmount = g.Sum(t => t.ApprovedBfpTotalAmount),
                           ApprovedSelfAmount = g.Sum(t => t.ApprovedSelfAmount),
                           CertifiedBfpTotalAmount = g.Sum(t => t.CertifiedBfpTotalAmount),
                           CertifiedSelfAmount = g.Sum(t => t.CertifiedSelfAmount),
                       };

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cr.ContractReportId equals crp.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    join csd in csds on crfc.ContractReportFinancialCorrectionId equals csd.ContractReportFinancialCorrectionId

                    select new CertReportFinancialCorrectionVO
                    {
                        ContractReportFinancialCorrectionId = crfc.ContractReportFinancialCorrectionId,
                        ContractReportId = crfc.ContractReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        Notes = crfc.Notes,
                        CreateDate = crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                        PaymentVersionNum = crp.VersionNum,
                        ApprovedBfpTotalAmount = csd.ApprovedBfpTotalAmount,
                        ApprovedSelfAmount = csd.ApprovedSelfAmount,
                        CertifiedBfpTotalAmount = csd.CertifiedBfpTotalAmount,
                        CertifiedSelfAmount = csd.CertifiedSelfAmount,
                    })
                .ToList();
        }

        public IList<CertReportCorrectionVO> GetCertReportCorrections(int certReportId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals p.MapNodeId
                    where crc.CertReportId == certReportId
                    orderby crc.ContractReportCorrectionId descending
                    select new CertReportCorrectionVO
                    {
                        ContractReportCorrectionId = crc.ContractReportCorrectionId,
                        ProgrammeName = p.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                        CertStatus = crc.CertStatus,
                        CertCheckedDate = crc.CertCheckedDate,
                        Sign = crc.Sign,
                        ApprovedBfpTotalAmount = crc.CorrectedApprovedBfpTotalAmount,
                        ApprovedSelfAmount = crc.CorrectedApprovedSelfAmount,
                        CertifiedBfpTotalAmount = crc.CertifiedCorrectedApprovedBfpTotalAmount,
                        CertifiedSelfAmount = crc.CertifiedCorrectedApprovedSelfAmount,
                    })
                    .ToList();
        }

        public IList<CertReportFinancialRevalidationVO> GetCertReportFinancialRevalidations(int certReportId)
        {
            var financialRevalidationCSDs = this.GetCertReportFinancialRevalidationCSDs().Where(t => t.CertReportId == certReportId);
            var csds = from csd in financialRevalidationCSDs
                       join crfrcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>() on csd.Id equals crfrcsd.ContractReportFinancialRevalidationCSDId
                       group new
                       {
                           csd.ApprovedBfpTotalAmount,
                           csd.ApprovedSelfAmount,
                           csd.CertifiedBfpTotalAmount,
                           csd.CertifiedSelfAmount,
                       }
                       by crfrcsd.ContractReportFinancialRevalidationId
                        into g
                       select new
                       {
                           ContractReportFinancialRevalidationId = g.Key,
                           ApprovedBfpTotalAmount = g.Sum(t => t.ApprovedBfpTotalAmount),
                           ApprovedSelfAmount = g.Sum(t => t.ApprovedSelfAmount),
                           CertifiedBfpTotalAmount = g.Sum(t => t.CertifiedBfpTotalAmount),
                           CertifiedSelfAmount = g.Sum(t => t.CertifiedSelfAmount),
                       };

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidation>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cr.ContractReportId equals crp.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    join csd in csds on crfc.ContractReportFinancialRevalidationId equals csd.ContractReportFinancialRevalidationId

                    select new CertReportFinancialRevalidationVO
                    {
                        ContractReportFinancialRevalidationId = crfc.ContractReportFinancialRevalidationId,
                        ContractReportId = crfc.ContractReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        Notes = crfc.Notes,
                        CreateDate = crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                        PaymentVersionNum = crp.VersionNum,
                        ApprovedBfpTotalAmount = csd.ApprovedBfpTotalAmount,
                        ApprovedSelfAmount = csd.ApprovedSelfAmount,
                        CertifiedBfpTotalAmount = csd.CertifiedBfpTotalAmount,
                        CertifiedSelfAmount = csd.CertifiedSelfAmount,
                    })
                .ToList();
        }

        public IList<CertReportRevalidationVO> GetCertReportRevalidations(int certReportId)
        {
            return (from crr in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on crr.ProgrammeId equals p.MapNodeId
                    where crr.CertReportId == certReportId
                    orderby crr.ContractReportRevalidationId descending
                    select new CertReportRevalidationVO
                    {
                        ContractReportRevalidationId = crr.ContractReportRevalidationId,
                        ProgrammeName = p.Name,
                        RegNumber = crr.RegNumber,
                        StatusDescr = crr.Status,
                        Status = crr.Status,
                        Type = crr.Type,
                        Date = crr.Date,
                        CertStatus = crr.CertStatus,
                        CertCheckedDate = crr.CertCheckedDate,
                        Sign = crr.Sign,
                        ApprovedBfpTotalAmount = crr.RevalidatedBfpTotalAmount,
                        ApprovedSelfAmount = crr.RevalidatedSelfAmount,
                        CertifiedBfpTotalAmount = crr.CertifiedRevalidatedBfpTotalAmount,
                        CertifiedSelfAmount = crr.CertifiedRevalidatedSelfAmount,
                    })
                    .ToList();
        }

        public IList<CertReportFinancialCertCorrectionVO> GetCertReportFinancialCertCorrections(int certReportId)
        {
            var financialCertCorrectionCSDs = this.GetCertReportFinancialCertCorrectionCSDs().Where(t => t.CertReportId == certReportId);
            var csds = from csd in financialCertCorrectionCSDs
                       join crfrcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>() on csd.Id equals crfrcsd.ContractReportFinancialCertCorrectionCSDId
                       group new
                       {
                           csd.ApprovedBfpTotalAmount,
                           csd.ApprovedSelfAmount,
                           csd.CertifiedBfpTotalAmount,
                           csd.CertifiedSelfAmount,
                       }
                       by crfrcsd.ContractReportFinancialCertCorrectionId
                        into g
                       select new
                       {
                           ContractReportFinancialCertCorrectionId = g.Key,
                           CertifiedBfpTotalAmount = g.Sum(t => t.CertifiedBfpTotalAmount),
                           CertifiedSelfAmount = g.Sum(t => t.CertifiedSelfAmount),
                       };

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrection>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cr.ContractReportId equals crp.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    join csd in csds on crfc.ContractReportFinancialCertCorrectionId equals csd.ContractReportFinancialCertCorrectionId

                    select new CertReportFinancialCertCorrectionVO
                    {
                        ContractReportFinancialCertCorrectionId = crfc.ContractReportFinancialCertCorrectionId,
                        ContractReportId = crfc.ContractReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        Notes = crfc.Notes,
                        CreateDate = crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                        PaymentVersionNum = crp.VersionNum,
                        CertifiedBfpTotalAmount = csd.CertifiedBfpTotalAmount,
                        CertifiedSelfAmount = csd.CertifiedSelfAmount,
                    })
                .ToList();
        }

        public IList<CertReportCertCorrectionVO> GetCertReportCertCorrections(int certReportId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals p.MapNodeId
                    where crc.CertReportId == certReportId
                    orderby crc.ContractReportCertCorrectionId descending
                    select new CertReportCertCorrectionVO
                    {
                        ContractReportCertCorrectionId = crc.ContractReportCertCorrectionId,
                        ProgrammeName = p.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                        CertCheckedDate = crc.CheckedDate,
                        Sign = crc.Sign,
                        CertifiedBfpTotalAmount = crc.CertifiedBfpTotalAmount,
                        CertifiedSelfAmount = crc.CertifiedSelfAmount,
                    })
                    .ToList();
        }

        public IList<CertReportPaymentVO> GetContractReportsForCertReportPayments(int certReportId)
        {
            var programmeId = this.GetProgrammeId(certReportId);

            var subquery = (from crfcsdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                            where crfcsdbi.CertReportId == null
                            select crfcsdbi.ContractReportId)
                    .Distinct();

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cr.ContractReportId equals crp.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where subquery.Contains(cr.ContractReportId) &&
                          programmeId == c.ProgrammeId &&
                          cr.Status == ContractReportStatus.Accepted &&
                          crp.Status == ContractReportPaymentStatus.Actual &&
                          (crp.PaymentType == ContractReportPaymentType.Intermediate || crp.PaymentType == ContractReportPaymentType.Final)
                    orderby cr.CreateDate descending
                    select new { cr, crp, c, p })
                    .GroupBy(t => t.cr.ContractReportId)
                    .Select(t => t.FirstOrDefault())
                    .Select(t => new CertReportPaymentVO
                    {
                        ContractReportId = t.cr.ContractReportId,
                        ContractReportPaymentId = t.crp.ContractReportPaymentId,
                        PaymentType = t.crp.PaymentType,
                        RequestedAmount = t.crp.RequestedAmount,
                        PaymentRegDate = t.crp.RegDate,
                        PaymentVersionNum = t.crp.VersionNum,
                        ContractId = t.cr.ContractId,
                        Gid = t.cr.Gid,
                        ReportType = t.cr.ReportType,
                        OrderNum = t.cr.OrderNum,
                        CheckedDate = t.cr.CheckedDate,
                        Status = t.cr.Status,
                        StatusNote = t.cr.StatusNote,
                        Source = t.cr.Source,
                        SubmitDate = t.cr.SubmitDate,
                        SubmitDeadline = t.cr.SubmitDeadline,
                        DateFrom = t.crp.DateFrom,
                        DateTo = t.crp.DateTo,
                        ProcedureName = t.p.Name,
                        ContractName = t.c.Name,
                        ContractRegNum = t.c.RegNumber,
                        ProcedureId = t.p.ProcedureId,
                    })
                .ToList();
        }

        public IList<CertReportAdvancePaymentVO> GetContractReportsForCertReportAdvancePayments(int certReportId)
        {
            var programmeId = this.GetProgrammeId(certReportId);

            var subquery = (from crapa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>()
                            where crapa.CertReportId == null
                            select crapa.ContractReportId)
                    .Distinct();

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cr.ContractReportId equals crp.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where subquery.Contains(cr.ContractReportId) &&
                          programmeId == c.ProgrammeId &&
                          cr.Status == ContractReportStatus.Accepted &&
                          crp.Status == ContractReportPaymentStatus.Actual &&
                          crp.PaymentType == ContractReportPaymentType.AdvanceVerification
                    orderby cr.CreateDate descending
                    select new { crp, cr, c, p })
                    .GroupBy(t => t.cr.ContractReportId)
                    .Select(t => t.FirstOrDefault())
                    .Select(t => new CertReportAdvancePaymentVO
                    {
                        ContractReportId = t.cr.ContractReportId,
                        ContractReportPaymentId = t.crp.ContractReportPaymentId,
                        PaymentType = t.crp.PaymentType,
                        RequestedAmount = t.crp.RequestedAmount,
                        PaymentRegDate = t.crp.RegDate,
                        PaymentVersionNum = t.crp.VersionNum,
                        ContractId = t.cr.ContractId,
                        Gid = t.cr.Gid,
                        ReportType = t.cr.ReportType,
                        OrderNum = t.cr.OrderNum,
                        CheckedDate = t.cr.CheckedDate,
                        Status = t.cr.Status,
                        StatusNote = t.cr.StatusNote,
                        Source = t.cr.Source,
                        SubmitDate = t.cr.SubmitDate,
                        SubmitDeadline = t.cr.SubmitDeadline,
                        DateFrom = t.crp.DateFrom,
                        DateTo = t.crp.DateTo,
                        ProcedureName = t.p.Name,
                        ContractName = t.c.Name,
                        ContractRegNum = t.c.RegNumber,
                        ProcedureId = t.p.ProcedureId,
                    })
                .ToList();
        }

        public IList<CertReportFinancialCorrectionVO> GetContractReportFinancialCorrectionsForCertReportFinancialCorrections(int certReportId)
        {
            var programmeId = this.GetProgrammeId(certReportId);

            return (from crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                    join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crfccsd.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join crafc in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>() on crfc.ContractReportFinancialCorrectionId equals crafc.ContractReportFinancialCorrectionId into g1
                    from crafc in g1.DefaultIfEmpty()

                    where crfccsd.CertReportId == null &&
                          programmeId == c.ProgrammeId &&
                          crfc.Status == ContractReportFinancialCorrectionStatus.Ended
                    select new CertReportFinancialCorrectionVO
                    {
                        ContractReportFinancialCorrectionId = crfc.ContractReportFinancialCorrectionId,
                        ContractReportId = crfc.ContractReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        Notes = crfc.Notes,
                        CreateDate = crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                    })
                .Distinct()
                .ToList();
        }

        public IList<CertReportCorrectionVO> GetContractReportCorrectionsForCertReportCorrections(int certReportId)
        {
            var programmeId = this.GetProgrammeId(certReportId);

            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals p.MapNodeId

                    where crc.CertReportId == null &&
                          programmeId == crc.ProgrammeId &&
                          crc.Status == ContractReportCorrectionStatus.Entered
                    select new CertReportCorrectionVO
                    {
                        ContractReportCorrectionId = crc.ContractReportCorrectionId,
                        ProgrammeName = p.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                    })
                .Distinct()
                .ToList();
        }

        public IList<CertReportFinancialRevalidationVO> GetContractReportFinancialRevalidationsForCertReportFinancialRevalidations(int certReportId)
        {
            var programmeId = this.GetProgrammeId(certReportId);

            return (from crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                    join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidation>() on crfccsd.ContractReportFinancialRevalidationId equals crfc.ContractReportFinancialRevalidationId
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where crfccsd.CertReportId == null &&
                          programmeId == c.ProgrammeId &&
                          crfc.Status == ContractReportFinancialRevalidationStatus.Ended
                    select new CertReportFinancialRevalidationVO
                    {
                        ContractReportFinancialRevalidationId = crfc.ContractReportFinancialRevalidationId,
                        ContractReportId = crfc.ContractReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        Notes = crfc.Notes,
                        CreateDate = crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                    })
                .Distinct()
                .ToList();
        }

        public IList<CertReportRevalidationVO> GetContractReportRevalidationsForCertReportRevalidations(int certReportId)
        {
            var programmeId = this.GetProgrammeId(certReportId);

            return (from crc in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals p.MapNodeId

                    where crc.CertReportId == null &&
                          programmeId == crc.ProgrammeId &&
                          crc.Status == ContractReportRevalidationStatus.Entered
                    select new CertReportRevalidationVO
                    {
                        ContractReportRevalidationId = crc.ContractReportRevalidationId,
                        ProgrammeName = p.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                    })
                    .Distinct()
                    .ToList();
        }

        public IList<CertReportFinancialCertCorrectionVO> GetContractReportFinancialCertCorrectionsForCertReportFinancialCertCorrections(int certReportId)
        {
            var programmeId = this.GetProgrammeId(certReportId);

            return (from crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                    join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrection>() on crfccsd.ContractReportFinancialCertCorrectionId equals crfc.ContractReportFinancialCertCorrectionId
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where crfccsd.CertReportId == null &&
                          programmeId == c.ProgrammeId &&
                          crfc.Status == ContractReportFinancialCertCorrectionStatus.Ended
                    select new CertReportFinancialCertCorrectionVO
                    {
                        ContractReportFinancialCertCorrectionId = crfc.ContractReportFinancialCertCorrectionId,
                        ContractReportId = crfc.ContractReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        Notes = crfc.Notes,
                        CreateDate = crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                    })
                .Distinct()
                .ToList();
        }

        public IList<CertReportCertCorrectionVO> GetContractReportCertCorrectionsForCertReportCertCorrections(int certReportId)
        {
            var programmeId = this.GetProgrammeId(certReportId);

            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals p.MapNodeId

                    where crc.CertReportId == null &&
                          programmeId == crc.ProgrammeId &&
                          crc.Status == ContractReportCertCorrectionStatus.Entered
                    select new CertReportCertCorrectionVO
                    {
                        ContractReportCertCorrectionId = crc.ContractReportCertCorrectionId,
                        ProgrammeName = p.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                    })
                .Distinct()
                .ToList();
        }

        public CertReportEligibleProgrammePriorityExpensesResultVO GetCertReportIntermediateFinalEligibleProgrammePriorityExpenses(int certReportId)
        {
            var attachedCertReportsQuery = from cracr in this.unitOfWork.DbContext.Set<CertReportAttachedCertReport>()
                                           where cracr.CertReportId == certReportId
                                           select cracr.AttachedCertReportId;

            var debtInterests =
                from dra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                join c in this.unitOfWork.DbContext.Set<Contract>() on dra.ContractId equals c.ContractId
                join cd in this.unitOfWork.DbContext.Set<ContractDebt>() on dra.ContractDebtId equals cd.ContractDebtId
                join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on cd.ProgrammePriorityId equals pp.MapNodeId
                select new CertReportApprovedCertifiedAmountVO
                {
                    Id = dra.ContractDebtId,
                    ContractId = c.ContractId,
                    ContractType = c.ContractType,
                    CertReportId = dra.CertReportId.Value,
                    ContractReportId = null,
                    ContractReportPaymentId = null,
                    ProgrammePriorityId = cd.ProgrammePriorityId,
                    ProgrammePriorityName = pp.Name,
                    AdvancePayment = null,
                    ApprovedEuAmount = -1 * dra.InterestBfp.EuAmount,
                    ApprovedBgAmount = -1 * dra.InterestBfp.BgAmount,
                    ApprovedBfpTotalAmount = -1 * dra.InterestBfp.TotalAmount,
                    ApprovedSelfAmount = 0m,
                    ApprovedTotalAmount = -1 * dra.InterestBfp.TotalAmount,
                    CertifiedEuAmount = -1 * dra.InterestBfp.EuAmount,
                    CertifiedBgAmount = -1 * dra.InterestBfp.BgAmount,
                    CertifiedBfpTotalAmount = -1 * dra.InterestBfp.TotalAmount,
                    CertifiedSelfAmount = 0m,
                    CertifiedTotalAmount = -1 * dra.InterestBfp.TotalAmount,
                };

            var currentUnion = this.GetCertReportAdvancePaymentAmounts()
                .Concat(this.GetCertReportFinancialCSDBudgetItems().Where(t => t.AdvancePayment != YesNoNonApplicable.Yes))
                .Concat(this.GetCertReportCorrections())
                .Concat(this.GetCertReportFinancialCorrectionCSDs().Where(t => t.AdvancePayment != YesNoNonApplicable.Yes))
                .Concat(this.GetCertReportRevalidations())
                .Concat(this.GetCertReportFinancialRevalidationCSDs().Where(t => t.AdvancePayment != YesNoNonApplicable.Yes))
                .Concat(debtInterests)
                .Where(t => t.CertReportId == certReportId)
                .ToList()
                .GroupBy(g => new { g.ProgrammePriorityId, g.ProgrammePriorityName })
                .Select(t => new
                {
                    t.Key,
                    CurrentApprovedEuAmount = t.Sum(p => p.ApprovedEuAmount),
                    CurrentApprovedBgAmount = t.Sum(p => p.ApprovedBgAmount),
                    CurrentApprovedSelfAmount = t.Sum(p => p.ApprovedSelfAmount),
                });

            var currentCertCorrectionUnion = this.GetCertReportCertCorrections()
                .Concat(this.GetCertReportFinancialCertCorrectionCSDs().Where(t => t.AdvancePayment != YesNoNonApplicable.Yes))
                .Where(t => t.CertReportId == certReportId)
                .ToList()
                .GroupBy(g => new { g.ProgrammePriorityId, g.ProgrammePriorityName })
                .Select(t => new
                {
                    t.Key,
                    OtherApprovedEuAmount = t.Sum(p => p.CertifiedEuAmount),
                    OtherApprovedBgAmount = t.Sum(p => p.CertifiedBgAmount),
                    OtherApprovedSelfAmount = t.Sum(p => p.CertifiedSelfAmount),
                });

            var otherUnion = this.GetCertReportAdvancePaymentAmounts()
                .Concat(this.GetCertReportFinancialCSDBudgetItems().Where(t => t.AdvancePayment != YesNoNonApplicable.Yes))
                .Concat(this.GetCertReportCorrections())
                .Concat(this.GetCertReportFinancialCorrectionCSDs().Where(t => t.AdvancePayment != YesNoNonApplicable.Yes))
                .Concat(this.GetCertReportRevalidations())
                .Concat(this.GetCertReportFinancialRevalidationCSDs().Where(t => t.AdvancePayment != YesNoNonApplicable.Yes))
                .Concat(this.GetCertReportCertCorrections())
                .Concat(this.GetCertReportFinancialCertCorrectionCSDs().Where(t => t.AdvancePayment != YesNoNonApplicable.Yes))
                .Concat(debtInterests)
                .Where(t => attachedCertReportsQuery.Contains(t.CertReportId))
                .ToList()
                .GroupBy(g => new { g.ProgrammePriorityId, g.ProgrammePriorityName })
                .Select(t => new
                {
                    t.Key,
                    OtherApprovedEuAmount = t.Sum(p => p.CertifiedEuAmount),
                    OtherApprovedBgAmount = t.Sum(p => p.CertifiedBgAmount),
                    OtherApprovedSelfAmount = t.Sum(p => p.CertifiedSelfAmount),
                });

            var allUnion = currentUnion.Select(t => t.Key).Union(otherUnion.Select(t => t.Key)).Distinct();

            var list = (from au in allUnion
                        join cu in currentUnion on new { au.ProgrammePriorityId } equals new { cu.Key.ProgrammePriorityId } into g1
                        from cu in g1.DefaultIfEmpty()

                        join cccu in currentCertCorrectionUnion on new { au.ProgrammePriorityId } equals new { cccu.Key.ProgrammePriorityId } into g2
                        from cccu in g2.DefaultIfEmpty()

                        join ou in otherUnion on new { au.ProgrammePriorityId } equals new { ou.Key.ProgrammePriorityId } into g3
                        from ou in g3.DefaultIfEmpty()

                        select new EligibleProgrammePriorityExpensesVO
                        {
                            ProgrammePriorityName = au.ProgrammePriorityName,
                            CurrentApprovedEuAmount = cu?.CurrentApprovedEuAmount ?? 0,
                            CurrentApprovedBgAmount = cu?.CurrentApprovedBgAmount ?? 0,
                            CurrentApprovedSelfAmount = cu?.CurrentApprovedSelfAmount ?? 0,
                            OtherApprovedEuAmount = (ou?.OtherApprovedEuAmount ?? 0) + (cccu?.OtherApprovedEuAmount ?? 0),
                            OtherApprovedBgAmount = (ou?.OtherApprovedBgAmount ?? 0) + (cccu?.OtherApprovedBgAmount ?? 0),
                            OtherApprovedSelfAmount = (ou?.OtherApprovedSelfAmount ?? 0) + (cccu?.OtherApprovedSelfAmount ?? 0),
                        })
                .ToList();

            var total = new EligibleProgrammePriorityExpensesVO();
            var totalYEI = new EligibleProgrammePriorityExpensesVO();
            bool hasTotalYEI = false;

            foreach (var listItem in list)
            {
                listItem.CurrentApprovedBgEuAmount = listItem.CurrentApprovedEuAmount + listItem.CurrentApprovedBgAmount;
                listItem.OtherApprovedBgEuAmount = listItem.OtherApprovedEuAmount + listItem.OtherApprovedBgAmount;

                listItem.CurrentApprovedBgEuSelfAmount = listItem.CurrentApprovedBgEuAmount + listItem.CurrentApprovedSelfAmount;
                listItem.OtherApprovedBgEuSelfAmount = listItem.OtherApprovedBgEuAmount + listItem.OtherApprovedSelfAmount;

                total.CurrentApprovedEuAmount += listItem.CurrentApprovedEuAmount;
                total.CurrentApprovedBgAmount += listItem.CurrentApprovedBgAmount;
                total.CurrentApprovedSelfAmount += listItem.CurrentApprovedSelfAmount;
                total.CurrentApprovedBgEuAmount += listItem.CurrentApprovedBgEuAmount;
                total.CurrentApprovedBgEuSelfAmount += listItem.CurrentApprovedBgEuSelfAmount;

                total.OtherApprovedEuAmount += listItem.OtherApprovedEuAmount;
                total.OtherApprovedBgAmount += listItem.OtherApprovedBgAmount;
                total.OtherApprovedSelfAmount += listItem.OtherApprovedSelfAmount;
                total.OtherApprovedBgEuAmount += listItem.OtherApprovedBgEuAmount;
                total.OtherApprovedBgEuSelfAmount += listItem.OtherApprovedBgEuSelfAmount;
            }

            return new CertReportEligibleProgrammePriorityExpensesResultVO
            {
                Items = list,
                Total = list.Count != 0 ? total : null,
                TotalYEI = hasTotalYEI ? totalYEI : null,
            };
        }

        public CertReportApprovedAmountsCorrectionsResultVO GetCertReportApprovedAmountsCorrections(int certReportId)
        {
            var attachedCertReportsQuery = from cracr in this.unitOfWork.DbContext.Set<CertReportAttachedCertReport>()
                                           where cracr.CertReportId == certReportId
                                           select cracr.AttachedCertReportId;

            var currentCertReportCCSDs = (from crcbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                                          join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crcbi.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                                          join crbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crcbi.ContractReportFinancialCSDBudgetItemId equals crbi.ContractReportFinancialCSDBudgetItemId
                                          join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on crbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                                          join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                                          join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                                          join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                          join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                                          join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId

                                          where crcbi.CertReportId == certReportId && crbi.AdvancePayment != YesNoNonApplicable.Yes
                                          group new
                                          {
                                              ApprovedEuAmount = (int)crcbi.Sign * crcbi.CorrectedApprovedEuAmount,
                                              ApprovedBgAmount = (int)crcbi.Sign * crcbi.CorrectedApprovedBgAmount,
                                              ApprovedSelfAmount = (int)crcbi.Sign * crcbi.CorrectedApprovedSelfAmount,
                                          }
                                          by new
                                          {
                                              ProgrammePriorityId = pp.MapNodeId,
                                              ProgrammePriorityName = pp.Name,
                                              crfc.ContractId,
                                              crfc.ContractReportId,
                                              crfc.ContractReportFinancialCorrectionId,
                                              ContractNum = c.RegNumber,
                                              ReportNum = cr.OrderNum,
                                              ReportDate = cr.SubmitDate,
                                              CorrectionNum = crfc.OrderNum,
                                              CorrectionDate = crfc.CorrectionDate,
                                              CorrectionNote = crfc.Notes,
                                          }
                                          into g
                                          select new
                                          {
                                              g.Key,
                                              ApprovedBgAmount = g.Sum(t => t.ApprovedBgAmount),
                                              ApprovedEuAmount = g.Sum(t => t.ApprovedEuAmount),
                                              ApprovedSelfAmount = g.Sum(t => t.ApprovedSelfAmount),
                                          }).ToList();

            var crIds = currentCertReportCCSDs.Select(t => t.Key.ContractReportId);

            var otherCertReports = (from crbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                                    join c in this.unitOfWork.DbContext.Set<Contract>() on crbi.ContractId equals c.ContractId
                                    join cert in this.unitOfWork.DbContext.Set<CertReport>() on crbi.CertReportId equals cert.CertReportId

                                    where crIds.Contains(crbi.ContractReportId) && crbi.CertReportId.HasValue && attachedCertReportsQuery.Contains(crbi.CertReportId.Value)
                                    select new
                                    {
                                        cert.OrderNum,
                                        cert.RegDate,
                                        crbi.ContractReportId,
                                    })
                                    .Distinct()
                                    .ToList();

            var reimbursedAmounts = (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>()
                                     join crcbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on crfc.ContractReportFinancialCorrectionId equals crcbi.ContractReportFinancialCorrectionId
                                     join cd in this.unitOfWork.DbContext.Set<ContractDebt>() on crcbi.IrregularityId equals cd.IrregularityId
                                     join dra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>() on cd.ContractDebtId equals dra.ContractDebtId

                                     where crcbi.CertReportId == certReportId && cd.Status == ContractDebtStatus.Entered
                                     select new
                                     {
                                         crfc.ContractReportFinancialCorrectionId,
                                         PrincipalBfpTotalAmount = dra.PrincipalBfp.TotalAmount,
                                     })
                           .ToList()
                           .GroupBy(g => g.ContractReportFinancialCorrectionId)
                           .ToDictionary(d => d.Key, d => d.Sum(t => t.PrincipalBfpTotalAmount));

            var list = (from ccr in currentCertReportCCSDs
                        join ocr in otherCertReports on ccr.Key.ContractReportId equals ocr.ContractReportId
                        select new ApprovedAmountsCorrectionsVO
                        {
                            ContractReportFinancialCorrectionId = ccr.Key.ContractReportFinancialCorrectionId,
                            ProgrammePriorityName = ccr.Key.ProgrammePriorityName,
                            CertNum = ocr.OrderNum,
                            CertDate = ocr.RegDate,
                            ContractNum = ccr.Key.ContractNum,
                            ReportNum = ccr.Key.ReportNum,
                            ReportDate = ccr.Key.ReportDate,
                            CorrectionNum = ccr.Key.CorrectionNum,
                            CorrectionDate = ccr.Key.CorrectionDate,
                            CorrectionNote = ccr.Key.CorrectionNote,
                            ApprovedEuAmount = ccr.ApprovedEuAmount,
                            ApprovedBgAmount = ccr.ApprovedBgAmount,
                            ApprovedSelfAmount = ccr.ApprovedSelfAmount,
                            ReimbursedBgEuAmount = reimbursedAmounts.Keys.Contains(ccr.Key.ContractReportFinancialCorrectionId) ?
                                reimbursedAmounts[ccr.Key.ContractReportFinancialCorrectionId] :
                                null,
                        })
                        .ToList();

            var total = new ApprovedAmountsCorrectionsVO();
            var totalYEI = new ApprovedAmountsCorrectionsVO();
            bool hasTotalYEI = false;

            foreach (var listItem in list)
            {
                listItem.ApprovedBgEuAmount += listItem.ApprovedEuAmount + listItem.ApprovedBgAmount;

                total.ApprovedEuAmount += listItem.ApprovedEuAmount ?? 0;
                total.ApprovedBgAmount += listItem.ApprovedBgAmount ?? 0;
                total.ApprovedSelfAmount += listItem.ApprovedSelfAmount ?? 0;
                total.ApprovedBgEuAmount += listItem.ApprovedBgEuAmount;
                total.ReimbursedBgEuAmount += listItem.ReimbursedBgEuAmount ?? 0;
            }

            return new CertReportApprovedAmountsCorrectionsResultVO
            {
                Items = list,
                Total = list.Count != 0 ? total : null,
                TotalYEI = hasTotalYEI ? totalYEI : null,
            };
        }

        public CertReportStateAidPaidAdvancePaymentsResultVO GetCertReportIntermediateFinalStateAidPaidAdvancePayments(int certReportId)
        {
            CertReport currentCertReport = (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                                            where cr.CertReportId == certReportId
                                            select cr).Single();

            var oldTimeout = this.unitOfWork.DbContext.Database.CommandTimeout;

            var anualAccountReportPredicate = PredicateBuilder.True<AnnualAccountReport>()
                .AndDateTimeLessThanOrEqual(x => x.ApprovalDate, currentCertReport.DateTo)
                .And(x => x.Status == AnnualAccountReportStatus.Ended)
                .And(x => x.ProgrammeId == currentCertReport.ProgrammeId);

            try
            {
                this.unitOfWork.DbContext.Database.CommandTimeout = 60 * 30;

                var approvedOlderCertReports = (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                                                where cr.ProgrammeId == currentCertReport.ProgrammeId && (cr.Status == CertReportStatus.Approved || cr.Status == CertReportStatus.PartialyApproved) && cr.OrderNum < currentCertReport.OrderNum
                                                select cr.CertReportId).ToList();

                var notFinishedCertReport = (int?)null;

                if (new[] { CertReportStatus.Draft, CertReportStatus.Ended, CertReportStatus.Unchecked }.Contains(currentCertReport.Status))
                {
                    notFinishedCertReport = currentCertReport.CertReportId;
                }
                else if (currentCertReport.Status == CertReportStatus.Approved || currentCertReport.Status == CertReportStatus.PartialyApproved)
                {
                    approvedOlderCertReports.Add(currentCertReport.CertReportId);
                }

                var advancePaymentIds = this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>().Select(x => x.ContractReportPaymentId).Distinct().ToList();

                var certifiedAPA = from adv in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>().Where(t => t.CertReportId.HasValue && approvedOlderCertReports.Contains(t.CertReportId.Value))

                                   join crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>().Where(t => t.CertReportId.HasValue && approvedOlderCertReports.Contains(t.CertReportId.Value)) on adv.ContractReportPaymentId equals crc.ContractReportPaymentId into g1
                                   from crc in g1.DefaultIfEmpty()

                                   join crr in this.unitOfWork.DbContext.Set<ContractReportRevalidation>().Where(t => t.CertReportId.HasValue && approvedOlderCertReports.Contains(t.CertReportId.Value)) on adv.ContractReportPaymentId equals crr.ContractReportPaymentId into g2
                                   from crr in g2.DefaultIfEmpty()

                                   join c in this.unitOfWork.DbContext.Set<Contract>() on adv.ContractId equals c.ContractId
                                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId

                                   where ps.IsPrimary == true

                                   select new
                                   {
                                       ProgrammePriorityId = pp.MapNodeId,
                                       ProgrammePriorityName = pp.Name,
                                       TotalAmount = adv.CertifiedApprovedBfpTotalAmount,
                                       CorrectedTotalAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedBfpTotalAmount,
                                       RevalidatedTotalAmount = (int)crr.Sign * crr.CertifiedRevalidatedBfpTotalAmount,
                                       CSDTotalAmount = (decimal?)null,
                                       CSDCorrectedTotalAmount = (decimal?)null,
                                       CSDRevalidatedTotalAmount = (decimal?)null,
                                   };

                var certifiedCSD = from csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(t => t.CertReportId.HasValue && approvedOlderCertReports.Contains(t.CertReportId.Value) && t.AdvancePayment == YesNoNonApplicable.Yes)

                                   join crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>().Where(t => t.CertReportId.HasValue && approvedOlderCertReports.Contains(t.CertReportId.Value)) on csd.ContractReportFinancialCSDBudgetItemId equals crfccsd.ContractReportFinancialCSDBudgetItemId into g1
                                   from crfccsd in g1.DefaultIfEmpty()

                                   join crfrcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>().Where(t => t.CertReportId.HasValue && approvedOlderCertReports.Contains(t.CertReportId.Value)) on csd.ContractReportFinancialCSDBudgetItemId equals crfrcsd.ContractReportFinancialCSDBudgetItemId into g2
                                   from crfrcsd in g2.DefaultIfEmpty()

                                   join c in this.unitOfWork.DbContext.Set<Contract>() on csd.ContractId equals c.ContractId
                                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId

                                   where ps.IsPrimary == true

                                   select new
                                   {
                                       ProgrammePriorityId = pp.MapNodeId,
                                       ProgrammePriorityName = pp.Name,
                                       TotalAmount = (decimal?)null,
                                       CorrectedTotalAmount = (decimal?)null,
                                       RevalidatedTotalAmount = (decimal?)null,
                                       CSDTotalAmount = csd.CertifiedApprovedBfpTotalAmount,
                                       CSDCorrectedTotalAmount = -1 * (int)crfccsd.Sign * crfccsd.CertifiedCorrectedApprovedBfpTotalAmount,
                                       CSDRevalidatedTotalAmount = (int)crfrcsd.Sign * crfrcsd.CertifiedRevalidatedBfpTotalAmount,
                                   };

                var approvedAPA = from adv in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>().Where(t => t.CertReportId.HasValue && t.CertReportId.Value == notFinishedCertReport)

                                  join crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>().Where(t => t.CertReportId.HasValue && t.CertReportId.Value == notFinishedCertReport && t.ProgrammePriorityId.HasValue) on new { adv.ContractReportPaymentId, adv.ProgrammePriorityId } equals new { ContractReportPaymentId = crc.ContractReportPaymentId.Value, ProgrammePriorityId = crc.ProgrammePriorityId.Value } into g1
                                  from crc in g1.DefaultIfEmpty()

                                  join crr in this.unitOfWork.DbContext.Set<ContractReportRevalidation>().Where(t => t.CertReportId.HasValue && t.CertReportId.Value == notFinishedCertReport && t.ProgrammePriorityId.HasValue) on new { adv.ContractReportPaymentId, adv.ProgrammePriorityId } equals new { ContractReportPaymentId = crr.ContractReportPaymentId.Value, ProgrammePriorityId = crr.ProgrammePriorityId.Value } into g2
                                  from crr in g2.DefaultIfEmpty()

                                  join c in this.unitOfWork.DbContext.Set<Contract>() on adv.ContractId equals c.ContractId
                                  join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on adv.ProgrammePriorityId equals pp.MapNodeId

                                  select new
                                  {
                                      ProgrammePriorityId = pp.MapNodeId,
                                      ProgrammePriorityName = pp.Name,
                                      TotalAmount = adv.ApprovedBfpTotalAmount,
                                      CorrectedTotalAmount = (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount,
                                      RevalidatedTotalAmount = (int)crr.Sign * crr.RevalidatedBfpTotalAmount,
                                      CSDTotalAmount = (decimal?)null,
                                      CSDCorrectedTotalAmount = (decimal?)null,
                                      CSDRevalidatedTotalAmount = (decimal?)null,
                                  };

                var approvedCSD = from csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(t => t.CertReportId.HasValue && t.CertReportId.Value == notFinishedCertReport && t.AdvancePayment == YesNoNonApplicable.Yes)

                                  join crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>().Where(t => t.CertReportId.HasValue && t.CertReportId.Value == notFinishedCertReport) on csd.ContractReportFinancialCSDBudgetItemId equals crfccsd.ContractReportFinancialCSDBudgetItemId into g1
                                  from crfccsd in g1.DefaultIfEmpty()

                                  join crfrcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>().Where(t => t.CertReportId.HasValue && t.CertReportId.Value == notFinishedCertReport) on csd.ContractReportFinancialCSDBudgetItemId equals crfrcsd.ContractReportFinancialCSDBudgetItemId into g2
                                  from crfrcsd in g2.DefaultIfEmpty()

                                  join c in this.unitOfWork.DbContext.Set<Contract>() on csd.ContractId equals c.ContractId
                                  join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                                  join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId

                                  where ps.IsPrimary == true

                                  select new
                                  {
                                      ProgrammePriorityId = pp.MapNodeId,
                                      ProgrammePriorityName = pp.Name,
                                      TotalAmount = (decimal?)null,
                                      CorrectedTotalAmount = (decimal?)null,
                                      RevalidatedTotalAmount = (decimal?)null,
                                      CSDTotalAmount = csd.ApprovedBfpTotalAmount,
                                      CSDCorrectedTotalAmount = -1 * (int)crfccsd.Sign * crfccsd.CorrectedApprovedBfpTotalAmount,
                                      CSDRevalidatedTotalAmount = (int)crfrcsd.Sign * crfrcsd.RevalidatedBfpTotalAmount,
                                  };

                var aarCorrectedAPA = from aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>().Where(anualAccountReportPredicate)
                                      join aarcrc in this.unitOfWork.DbContext.Set<AnnualAccountReportContractReportCorrection>() on aar.AnnualAccountReportId equals aarcrc.AnnualAccountReportId
                                      join crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>().Where(x => advancePaymentIds.Contains(x.ContractReportPaymentId.Value)) on aarcrc.ContractReportCorrectionId equals crc.ContractReportCorrectionId

                                      join c in this.unitOfWork.DbContext.Set<Contract>() on crc.ContractId equals c.ContractId
                                      join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on new { ProcedureId = crc.ProcedureId.Value, ProgrammePriorityId = crc.ProgrammePriorityId.Value } equals new { ps.ProcedureId, ps.ProgrammePriorityId }
                                      join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on crc.ProgrammePriorityId equals pp.MapNodeId

                                      select new
                                      {
                                          ProgrammePriorityId = pp.MapNodeId,
                                          ProgrammePriorityName = pp.Name,
                                          TotalAmount = (decimal?)0,
                                          CorrectedTotalAmount = (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount ?? (decimal?)0,
                                          RevalidatedTotalAmount = (decimal?)0,
                                          CSDTotalAmount = (decimal?)0,
                                          CSDCorrectedTotalAmount = (decimal?)0,
                                          CSDRevalidatedTotalAmount = (decimal?)0,
                                      };

                var aarCorrectedCSD = from aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>().Where(anualAccountReportPredicate)
                                      join aarcsd in this.unitOfWork.DbContext.Set<AnnualAccountReportFinancialCorrectionCSD>() on aar.AnnualAccountReportId equals aarcsd.AnnualAccountReportId
                                      join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on aarcsd.ContractReportFinancialCorrectionCSDId equals csd.ContractReportFinancialCorrectionCSDId
                                      join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(t => t.AdvancePayment == YesNoNonApplicable.Yes) on csd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId

                                      join c in this.unitOfWork.DbContext.Set<Contract>() on csd.ContractId equals c.ContractId
                                      join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                                      join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId

                                      where ps.IsPrimary == true

                                      select new
                                      {
                                          ProgrammePriorityId = pp.MapNodeId,
                                          ProgrammePriorityName = pp.Name,
                                          TotalAmount = (decimal?)0,
                                          CorrectedTotalAmount = (decimal?)0,
                                          RevalidatedTotalAmount = (decimal?)0,
                                          CSDTotalAmount = (decimal?)0,
                                          CSDCorrectedTotalAmount = -1 * (int)csd.Sign * (csd.CorrectedApprovedBfpTotalAmount ?? (decimal?)0),
                                          CSDRevalidatedTotalAmount = (decimal?)0,
                                      };

                var aarccCorrectedAPA = from aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>().Where(anualAccountReportPredicate)
                                        join aarcrc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertCorrection>() on aar.AnnualAccountReportId equals aarcrc.AnnualAccountReportId
                                        join crc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>().Where(x => advancePaymentIds.Contains(x.ContractReportPaymentId.Value)) on aarcrc.ContractReportCertAuthorityCorrectionId equals crc.ContractReportCertAuthorityCorrectionId

                                        join c in this.unitOfWork.DbContext.Set<Contract>() on crc.ContractId equals c.ContractId
                                        join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on new { ProcedureId = crc.ProcedureId.Value, ProgrammePriorityId = crc.ProgrammePriorityId.Value } equals new { ps.ProcedureId, ps.ProgrammePriorityId }
                                        join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on crc.ProgrammePriorityId equals pp.MapNodeId

                                        select new
                                        {
                                            ProgrammePriorityId = pp.MapNodeId,
                                            ProgrammePriorityName = pp.Name,
                                            TotalAmount = (decimal?)0,
                                            CorrectedTotalAmount = (int)crc.Sign * crc.CertifiedBfpTotalAmount ?? (decimal?)0,
                                            RevalidatedTotalAmount = (decimal?)0,
                                            CSDTotalAmount = (decimal?)0,
                                            CSDCorrectedTotalAmount = (decimal?)0,
                                            CSDRevalidatedTotalAmount = (decimal?)0,
                                        };

                var aarccCorrectedCSD = from aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>().Where(anualAccountReportPredicate)
                                        join aarcsd in this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>() on aar.AnnualAccountReportId equals aarcsd.AnnualAccountReportId
                                        join fc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>() on aarcsd.ContractReportCertAuthorityFinancialCorrectionCSDId equals fc.ContractReportCertAuthorityFinancialCorrectionCSDId
                                        join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(t => t.AdvancePayment == YesNoNonApplicable.Yes) on fc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId

                                        join c in this.unitOfWork.DbContext.Set<Contract>() on fc.ContractId equals c.ContractId
                                        join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                                        join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId

                                        select new
                                        {
                                            ProgrammePriorityId = pp.MapNodeId,
                                            ProgrammePriorityName = pp.Name,
                                            TotalAmount = (decimal?)0,
                                            CorrectedTotalAmount = (decimal?)0,
                                            RevalidatedTotalAmount = (decimal?)0,
                                            CSDTotalAmount = (decimal?)0,
                                            CSDCorrectedTotalAmount = (int)fc.Sign * fc.CertifiedBfpTotalAmount ?? (decimal?)0,
                                            CSDRevalidatedTotalAmount = (decimal?)0,
                                        };

                var list = (from i in certifiedAPA
                            .Concat(certifiedCSD)
                            .Concat(approvedAPA)
                            .Concat(approvedCSD)
                            .Concat(aarCorrectedAPA)
                            .Concat(aarCorrectedCSD)
                            .Concat(aarccCorrectedAPA)
                            .Concat(aarccCorrectedCSD)
                            group i
                            by new
                            {
                                i.ProgrammePriorityId,
                                i.ProgrammePriorityName,
                            }
                            into g
                            let approvedAmount = (g.Sum(t => t.TotalAmount) ?? 0) + (g.Sum(t => t.CorrectedTotalAmount) ?? 0) + (g.Sum(t => t.RevalidatedTotalAmount) ?? 0)
                            let csdAmount = (g.Sum(t => t.CSDTotalAmount) ?? 0) + (g.Sum(t => t.CSDCorrectedTotalAmount) ?? 0) + (g.Sum(t => t.CSDRevalidatedTotalAmount) ?? 0)
                            select new StateAidPaidAdvancePaymentsVO
                            {
                                ProgrammePriorityName = g.Key.ProgrammePriorityName,
                                ApprovedTotalAmount = approvedAmount,
                                ApprovedAdvancedTotalAmountCSD = csdAmount,
                                UncoveredAmountByBeneficient = approvedAmount - csdAmount,
                            }).ToList();

                var total = new StateAidPaidAdvancePaymentsVO();
                var totalYEI = new StateAidPaidAdvancePaymentsVO();
                bool hasTotalYEI = false;

                foreach (var listItem in list)
                {
                    total.ApprovedTotalAmount += listItem.ApprovedTotalAmount ?? 0;
                    total.ApprovedAdvancedTotalAmountCSD += listItem.ApprovedAdvancedTotalAmountCSD ?? 0;
                    total.UncoveredAmountByBeneficient += listItem.UncoveredAmountByBeneficient ?? 0;
                }

                return new CertReportStateAidPaidAdvancePaymentsResultVO
                {
                    Items = list,
                    Total = list.Count != 0 ? total : null,
                    TotalYEI = hasTotalYEI ? totalYEI : null,
                };
            }
            finally
            {
                this.unitOfWork.DbContext.Database.CommandTimeout = oldTimeout;
            }
        }

        public CertReportReaffirmedCostsByAdministrativeAuthorityResultVO GetCertReportReaffirmedCostsByAdministrativeAuthority(int certReportId)
        {
            var currentUnion = this.GetCertReportFinancialRevalidationCSDs().Where(t => t.AdvancePayment != YesNoNonApplicable.Yes)
                .Concat(this.GetCertReportRevalidations())
                .Where(t => t.CertReportId == certReportId &&
                            (!t.ContractType.HasValue))
                .ToList()
                .GroupBy(g => new { g.ProgrammePriorityId, g.ProgrammePriorityName })
                .Select(t => new
                {
                    t.Key,
                    RevalidatedEuAmount = t.Sum(p => p.ApprovedEuAmount),
                    RevalidatedBgAmount = t.Sum(p => p.ApprovedBgAmount),
                    RevalidatedSelfAmount = t.Sum(p => p.ApprovedSelfAmount),
                    RevalidatedTotalAmount = t.Sum(p => p.ApprovedTotalAmount),
                });

            var list = (from ccr in currentUnion
                        select new ReaffirmedCostsByAdministrativeAuthorityVO
                        {
                            ProgrammePriorityName = ccr.Key.ProgrammePriorityName,
                            RevalidatedTotalAmount = ccr.RevalidatedTotalAmount,
                        })
                        .ToList();

            var total = new ReaffirmedCostsByAdministrativeAuthorityVO();
            var totalYEI = new ReaffirmedCostsByAdministrativeAuthorityVO();
            bool hasTotalYEI = false;

            foreach (var listItem in list)
            {
                total.RevalidatedTotalAmount += listItem.RevalidatedTotalAmount ?? 0;
            }

            return new CertReportReaffirmedCostsByAdministrativeAuthorityResultVO
            {
                Items = list,
                Total = list.Count != 0 ? total : null,
                TotalYEI = hasTotalYEI ? totalYEI : null,
            };
        }

        public CertReportProgrammePaidContributionInfoForFinancialInstrumentsResultVO GetCertReportProgrammePaidContributionInfoForFinancialInstruments(int certReportId)
        {
            CertReport currentCertReport = (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                                            where cr.CertReportId == certReportId
                                            select cr).Single();

            var approvedOlderCertReports =
                (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                 where cr.ProgrammeId == currentCertReport.ProgrammeId &&
                 (cr.Status == CertReportStatus.Approved || cr.Status == CertReportStatus.PartialyApproved) &&
                 cr.OrderNum < currentCertReport.OrderNum
                 select cr.CertReportId)
                .ToList();

            if (currentCertReport.Status != CertReportStatus.Unapproved)
            {
                approvedOlderCertReports.Add(currentCertReport.CertReportId);
            }

            var certifiedAmount = this.GetCertReportApprovedCertifiedAmounts()
                .Where(t =>
                    approvedOlderCertReports.Contains(t.CertReportId) &&
                    t.ContractType.HasValue)
                .GroupBy(g => g.ProgrammePriorityId)
                .Select(g => new
                {
                    ProgrammePriorityId = g.Key,
                    CertifiedTotalAmount = g.Sum(p => p.CertifiedTotalAmount),
                })
                .ToList();

            var approvedOlderContractReportFinancials =
                (from crbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                 where approvedOlderCertReports.Contains(crbi.CertReportId.Value)
                 select crbi.ContractReportFinancialId)
                .ToList();

            var financialInstrumentsAmountPrimaryPrioroty =
                (from crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                 join c in this.unitOfWork.DbContext.Set<Contract>() on crf.ContractId equals c.ContractId
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                 where approvedOlderContractReportFinancials.Contains(crf.ContractReportFinancialId) &&
                     crf.Status == ContractReportFinancialStatus.Actual &&
                     ps.IsPrimary
                 select new
                 {
                     crf.ContractReportFinancialId,
                     ps.ProgrammePriorityId,
                 }).Distinct();

            var financialInstrumentsAmountOwnPriority =
                (from crfcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>()
                 join crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>() on crfcsd.ContractReportFinancialId equals crf.ContractReportFinancialId
                 join crbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(x => x.TotalAmount == 0) on crfcsd.ContractReportFinancialCSDId equals crbi.ContractReportFinancialCSDId
                 join l3Amount in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on crbi.ContractBudgetLevel3AmountId equals l3Amount.ContractBudgetLevel3AmountId
                 join l2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on l3Amount.ProcedureBudgetLevel2Id equals l2.ProcedureBudgetLevel2Id
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on l2.ProcedureShareId equals ps.ProcedureShareId
                 where approvedOlderContractReportFinancials.Contains(crf.ContractReportFinancialId) &&
                     crf.Status == ContractReportFinancialStatus.Actual
                 select new
                 {
                     crf.ContractReportFinancialId,
                     ps.ProgrammePriorityId,
                 }).Distinct();

            var financialInstrumentsAmount =
                (from crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                 join fiapp in financialInstrumentsAmountPrimaryPrioroty on crf.ContractReportFinancialId equals fiapp.ContractReportFinancialId
                 join fiaop in financialInstrumentsAmountOwnPriority on crf.ContractReportFinancialId equals fiaop.ContractReportFinancialId into g1
                 from fiaop in g1.DefaultIfEmpty()
                 select new
                 {
                     ProgrammePriority = fiaop == null ? fiapp.ProgrammePriorityId : fiaop.ProgrammePriorityId,
                     crf.ContractReportFinancialId,
                     crf.PaymentsFinalRecipientsAmount,
                     crf.CommitmentsGuaranteeAmount,
                     crf.ExpenseManagementAmount,
                     crf.ManagementFeesAmount,
                     crf.CorrespondingPublicSpendingAmount,
                 }
                 into g2
                 group g2 by g2.ProgrammePriority into g3
                 select new
                 {
                     ProgrammePriorityId = g3.Key,
                     FinancialInstrumentsAmount =
                         g3.Sum(i => i.PaymentsFinalRecipientsAmount) +
                         g3.Sum(i => i.CommitmentsGuaranteeAmount) +
                         g3.Sum(i => i.ExpenseManagementAmount) +
                         g3.Sum(i => i.ManagementFeesAmount),
                     CorrespondingPublicSpendingAmount = g3.Sum(i => i.CorrespondingPublicSpendingAmount),
                 }).ToList();

            var allProgrammePriorities =
                (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                 where pp.MapNodeRelation.ProgrammeId == currentCertReport.ProgrammeId
                 select new
                 {
                     ProgrammePriorityId = pp.MapNodeId,
                     ProgrammePriorityName = pp.Name,
                 })
                 .ToList();

            var res =
                (from pp in allProgrammePriorities

                 join ca in certifiedAmount on pp.ProgrammePriorityId equals ca.ProgrammePriorityId into j1
                 from ca in j1.DefaultIfEmpty()

                 join fia in financialInstrumentsAmount on pp.ProgrammePriorityId equals fia.ProgrammePriorityId into j2
                 from fia in j2.DefaultIfEmpty()

                 select new ProgrammePaidContributionInfoForFinancialInstrumentsVO
                 {
                     ProgrammePriorityName = pp.ProgrammePriorityName,
                     CertifiedTotalAmount = ca?.CertifiedTotalAmount ?? 0,
                     FinancialInstrumentsAmount = fia?.FinancialInstrumentsAmount ?? 0,
                     CorrespondingPublicSpendingAmount = fia?.CorrespondingPublicSpendingAmount ?? 0,
                 })
                 .ToList();

            return new CertReportProgrammePaidContributionInfoForFinancialInstrumentsResultVO
            {
                Items = res,
                Total = res.Count == 0 ?
                    null :
                    new ProgrammePaidContributionInfoForFinancialInstrumentsVO()
                    {
                        CertifiedTotalAmount = res.Sum(i => i.CertifiedTotalAmount),
                        FinancialInstrumentsAmount = res.Sum(i => i.FinancialInstrumentsAmount),
                        CorrespondingPublicSpendingAmount = res.Sum(i => i.CorrespondingPublicSpendingAmount),
                    },
            };
        }

        public CertReportAnnex4aResultVO GetAnnex4A(int certReportId)
        {
            var reportedAmounts =
                from csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on csdbi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on csdbi.ContractReportId equals crp.ContractReportId
                join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on csdbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId

                where csdbi.CertReportId == certReportId && crp.Status == ContractReportPaymentStatus.Actual

                group csdbi by new
                {
                    csdbi.ContractId,
                    cbl3a.ProcedureBudgetLevel2Id,
                    csd.ContractContractorGid,
                    crp.VersionNum,
                }
                into g
                select new
                {
                    g.Key,

                    BfpTotalAmount = g.Sum(p => p.BfpTotalAmount),
                    SelfAmount = g.Sum(p => p.SelfAmount),
                    UnapprovedByCorrectionBfpTotalAmount = g.Sum(p => p.UnapprovedByCorrectionBfpTotalAmount),
                    UnapprovedByCorrectionSelfAmount = g.Sum(p => p.UnapprovedByCorrectionSelfAmount),
                    UnapprovedBfpTotalAmount = g.Sum(p => p.UnapprovedBfpTotalAmount),
                    UnapprovedSelfAmount = g.Sum(p => p.UnapprovedSelfAmount),

                    RevalidatedBfpTotalAmount = (decimal?)0,
                    RevalidatedSelfAmount = (decimal?)0,

                    CorrectedApprovedBfpTotalAmountCert = (decimal?)0,
                    CorrectedApprovedSelfAmountCert = (decimal?)0,
                    CorrectedApprovedBfpTotalAmountNoCert = (decimal?)0,
                    CorrectedApprovedSelfAmountNoCert = (decimal?)0,
                };

            var revalidated =
                from fr in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fr.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on fbi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fr.ContractReportId equals crp.ContractReportId
                join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId

                where fr.CertReportId == certReportId && crp.Status == ContractReportPaymentStatus.Actual

                group new
                {
                    RevalidatedBfpTotalAmount = (int)fr.Sign * fr.RevalidatedBfpTotalAmount,
                    RevalidatedSelfAmount = (int)fr.Sign * fr.RevalidatedSelfAmount,
                }
                by new
                {
                    fr.ContractId,
                    cbl3a.ProcedureBudgetLevel2Id,
                    csd.ContractContractorGid,
                    crp.VersionNum,
                }
                into g
                select new
                {
                    g.Key,

                    BfpTotalAmount = 0m,
                    SelfAmount = 0m,
                    UnapprovedByCorrectionBfpTotalAmount = (decimal?)0,
                    UnapprovedByCorrectionSelfAmount = (decimal?)0,
                    UnapprovedBfpTotalAmount = (decimal?)0,
                    UnapprovedSelfAmount = (decimal?)0,

                    RevalidatedBfpTotalAmount = g.Sum(p => p.RevalidatedBfpTotalAmount),
                    RevalidatedSelfAmount = g.Sum(p => p.RevalidatedSelfAmount),

                    CorrectedApprovedBfpTotalAmountCert = (decimal?)0,
                    CorrectedApprovedSelfAmountCert = (decimal?)0,
                    CorrectedApprovedBfpTotalAmountNoCert = (decimal?)0,
                    CorrectedApprovedSelfAmountNoCert = (decimal?)0,
                };

            var corrections =
                from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fccsd.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on fbi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fccsd.ContractReportId equals crp.ContractReportId
                join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId

                where fccsd.CertReportId == certReportId && crp.Status == ContractReportPaymentStatus.Actual

                let certified = fccsd.CertStatus.HasValue && fccsd.CertStatus == ContractReportFinancialCorrectionCSDCertStatus.Ended

                group new
                {
                    CorrectedApprovedBfpTotalAmountCert = certified ? -1 * (int)fccsd.Sign * fccsd.CorrectedApprovedBfpTotalAmount : 0,
                    CorrectedApprovedSelfAmountCert = certified ? -1 * (int)fccsd.Sign * fccsd.CorrectedApprovedSelfAmount : 0,
                    CorrectedApprovedBfpTotalAmountNoCert = !certified ? -1 * (int)fccsd.Sign * fccsd.CorrectedApprovedBfpTotalAmount : 0,
                    CorrectedApprovedSelfAmountNoCert = !certified ? -1 * (int)fccsd.Sign * fccsd.CorrectedApprovedSelfAmount : 0,
                }
                by new
                {
                    fccsd.ContractId,
                    cbl3a.ProcedureBudgetLevel2Id,
                    csd.ContractContractorGid,
                    crp.VersionNum,
                }
                into g
                select new
                {
                    g.Key,

                    BfpTotalAmount = 0m,
                    SelfAmount = 0m,
                    UnapprovedByCorrectionBfpTotalAmount = (decimal?)0,
                    UnapprovedByCorrectionSelfAmount = (decimal?)0,
                    UnapprovedBfpTotalAmount = (decimal?)0,
                    UnapprovedSelfAmount = (decimal?)0,

                    RevalidatedBfpTotalAmount = (decimal?)0,
                    RevalidatedSelfAmount = (decimal?)0,

                    CorrectedApprovedBfpTotalAmountCert = g.Sum(p => p.CorrectedApprovedBfpTotalAmountCert),
                    CorrectedApprovedSelfAmountCert = g.Sum(p => p.CorrectedApprovedSelfAmountCert),
                    CorrectedApprovedBfpTotalAmountNoCert = g.Sum(p => p.CorrectedApprovedBfpTotalAmountNoCert),
                    CorrectedApprovedSelfAmountNoCert = g.Sum(p => p.CorrectedApprovedSelfAmountNoCert),
                };

            var union =
                from amnts in reportedAmounts.Concat(revalidated).Concat(corrections)
                group amnts by amnts.Key into g
                select new
                {
                    g.Key,

                    BfpTotalAmount = g.Sum(p => p.BfpTotalAmount),
                    SelfAmount = g.Sum(p => p.SelfAmount),
                    UnapprovedByCorrectionBfpTotalAmount = g.Sum(p => p.UnapprovedByCorrectionBfpTotalAmount),
                    UnapprovedByCorrectionSelfAmount = g.Sum(p => p.UnapprovedByCorrectionSelfAmount),
                    UnapprovedBfpTotalAmount = g.Sum(p => p.UnapprovedBfpTotalAmount),
                    UnapprovedSelfAmount = g.Sum(p => p.UnapprovedSelfAmount),

                    RevalidatedBfpTotalAmount = g.Sum(p => p.RevalidatedBfpTotalAmount),
                    RevalidatedSelfAmount = g.Sum(p => p.RevalidatedSelfAmount),

                    CorrectedApprovedBfpTotalAmountCert = g.Sum(p => p.CorrectedApprovedBfpTotalAmountCert),
                    CorrectedApprovedSelfAmountCert = g.Sum(p => p.CorrectedApprovedSelfAmountCert),
                    CorrectedApprovedBfpTotalAmountNoCert = g.Sum(p => p.CorrectedApprovedBfpTotalAmountNoCert),
                    CorrectedApprovedSelfAmountNoCert = g.Sum(p => p.CorrectedApprovedSelfAmountNoCert),
                };

            var contractVersionXmls = this.unitOfWork.DbContext.Set<ContractVersionXml>();

            var certReportData =
                from cert in this.unitOfWork.DbContext.Set<CertReport>()
                where cert.CertReportId == certReportId
                select new
                {
                    cert.RegDate,
                    cert.CertReportNumber,
                };

            var contractAmounts =
                from cva in this.unitOfWork.DbContext.Set<ContractVersionXmlAmount>()
                group cva
                by new
                {
                    cva.ContractVersionXmlId,
                    cva.ProcedureBudgetLevel2Id,
                }
                into g
                select new
                {
                    g.Key,
                    TotalAmount = g.Sum(p => p.ContractBgAmount + p.ContractEuAmount + p.ContractSelfAmount),
                };

            var items =
               (from amnts in union
                join c in this.unitOfWork.DbContext.Set<Contract>() on amnts.Key.ContractId equals c.ContractId
                join pbl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on amnts.Key.ProcedureBudgetLevel2Id equals pbl2.ProcedureBudgetLevel2Id

                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on amnts.Key.ContractContractorGid equals cc.Gid into g1
                from cc in g1.DefaultIfEmpty()

                join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into g2
                from cctor in g2.DefaultIfEmpty()

                let contractVersionXmlId =
                    (from cv in contractVersionXmls
                     from certd in certReportData
                     where cv.CreateDate < certd.RegDate && cv.ContractId == amnts.Key.ContractId
                     orderby cv.CreateDate descending
                     select cv.ContractVersionXmlId).FirstOrDefault()

                join camnts in contractAmounts on new { amnts.Key.ProcedureBudgetLevel2Id, ContractVersionXmlId = contractVersionXmlId } equals new { camnts.Key.ProcedureBudgetLevel2Id, camnts.Key.ContractVersionXmlId }

                from certd in certReportData

                select new
                {
                    ContractRegNumber = c.RegNumber,
                    PaymentVersionNum = amnts.Key.VersionNum,

                    HasContractor = cc != null,
                    ContractContractNumber = cc.Number,
                    SignDate = cc != null ? cc.SignDate : (DateTime?)null,
                    Contractor = cctor.Name,
                    VATAmountIfEligible = cc != null ? cc.VATAmountIfEligible : (decimal?)null,
                    ContractContractTotalAmount = cc != null ? cc.TotalFundedValue : (decimal?)null,

                    BudgetL2Name = cc == null ? pbl2.Name : null,
                    ContractTotalAmount = cc == null ? camnts.TotalAmount : (decimal?)null,

                    BfpTotalAmount = amnts.BfpTotalAmount,
                    SelfAmount = amnts.SelfAmount,

                    UnapprovedByCorrectionBfpTotalAmount = amnts.UnapprovedByCorrectionBfpTotalAmount,
                    UnapprovedByCorrectionSelfAmount = amnts.UnapprovedByCorrectionSelfAmount,

                    UnapprovedBfpTotalAmount = amnts.UnapprovedBfpTotalAmount,
                    UnapprovedSelfAmount = amnts.UnapprovedSelfAmount,

                    ApprovedBfpTotalAmount = amnts.BfpTotalAmount - amnts.UnapprovedByCorrectionBfpTotalAmount - amnts.UnapprovedBfpTotalAmount,
                    ApprovedSelfAmount = amnts.SelfAmount - amnts.UnapprovedByCorrectionSelfAmount - amnts.UnapprovedSelfAmount,

                    RevalidatedBfpTotalAmount = amnts.RevalidatedBfpTotalAmount,
                    RevalidatedSelfAmount = amnts.RevalidatedSelfAmount,

                    CorrectedApprovedBfpTotalAmountNoCert = amnts.CorrectedApprovedBfpTotalAmountNoCert,
                    CorrectedApprovedSelfAmountNoCert = amnts.CorrectedApprovedSelfAmountNoCert,

                    CorrectedApprovedBfpTotalAmountCert = amnts.CorrectedApprovedBfpTotalAmountCert,
                    CorrectedApprovedSelfAmountCert = amnts.CorrectedApprovedSelfAmountCert,

                    IncludedInCertReportBfpTotalAmount = (amnts.BfpTotalAmount - amnts.UnapprovedByCorrectionBfpTotalAmount - amnts.UnapprovedBfpTotalAmount)
                        + amnts.RevalidatedBfpTotalAmount
                        - 0
                        - amnts.CorrectedApprovedBfpTotalAmountNoCert
                        - 0
                        - amnts.CorrectedApprovedBfpTotalAmountCert,

                    IncludedInCertReportSelfAmount = (amnts.SelfAmount - amnts.UnapprovedByCorrectionSelfAmount - amnts.UnapprovedSelfAmount)
                        + amnts.RevalidatedSelfAmount
                        - 0
                        - amnts.CorrectedApprovedSelfAmountNoCert
                        - 0
                        - amnts.CorrectedApprovedSelfAmountCert,

                    certd.CertReportNumber,
                }).ToList()
                .Select(p => new CertReportAnnex4aVO
                {
                    ContractRegNumber = p.ContractRegNumber,
                    PaymentVersionNum = p.PaymentVersionNum,

                    ContractNumber = p.HasContractor ? p.ContractContractNumber + " / " + p.SignDate?.ToString("dd.MM.yyyy") : p.BudgetL2Name,
                    ContractContractor = p.Contractor,
                    ContractVATAmountIfEligible = p.VATAmountIfEligible,
                    ContractTotalAmount = p.HasContractor ? p.ContractContractTotalAmount : p.ContractTotalAmount,

                    BfpTotalAmount = p.BfpTotalAmount,
                    SelfAmount = p.SelfAmount,

                    UnapprovedByCorrectionBfpTotalAmount = p.UnapprovedByCorrectionBfpTotalAmount,
                    UnapprovedByCorrectionSelfAmount = p.UnapprovedByCorrectionSelfAmount,

                    UnapprovedBfpTotalAmount = p.UnapprovedBfpTotalAmount,
                    UnapprovedSelfAmount = p.UnapprovedSelfAmount,

                    ApprovedBfpTotalAmount = p.ApprovedBfpTotalAmount,
                    ApprovedSelfAmount = p.ApprovedSelfAmount,

                    RevalidatedBfpTotalAmount = p.RevalidatedBfpTotalAmount,
                    RevalidatedSelfAmount = p.RevalidatedSelfAmount,

                    CorrectedApprovedBfpTotalAmountNoCert = p.CorrectedApprovedBfpTotalAmountNoCert,
                    CorrectedApprovedSelfAmountNoCert = p.CorrectedApprovedSelfAmountNoCert,

                    CorrectedApprovedBfpTotalAmountCert = p.CorrectedApprovedBfpTotalAmountCert,
                    CorrectedApprovedSelfAmountCert = p.CorrectedApprovedSelfAmountCert,

                    IncludedInCertReportBfpTotalAmount = p.IncludedInCertReportBfpTotalAmount,
                    IncludedInCertReportSelfAmount = p.IncludedInCertReportSelfAmount,

                    CertReportNumber = p.CertReportNumber,
                }).ToList();

            var result = new CertReportAnnex4aResultVO
            {
                Items = items,
                TotalContractAmountWithoutVAT = items.Sum(i => i.ContractAmountWithoutVAT),
                TotalContractVATAmountIfEligible = items.Sum(i => i.ContractVATAmountIfEligible),
                TotalContractTotalAmount = items.Sum(i => i.ContractTotalAmount),

                TotalBfpTotalAmount = items.Sum(i => i.BfpTotalAmount),
                TotalSelfAmount = items.Sum(i => i.SelfAmount),

                TotalUnapprovedByCorrectionBfpTotalAmount = items.Sum(i => i.UnapprovedByCorrectionBfpTotalAmount),
                TotalUnapprovedByCorrectionSelfAmount = items.Sum(i => i.UnapprovedByCorrectionSelfAmount),

                TotalUnapprovedBfpTotalAmount = items.Sum(i => i.UnapprovedBfpTotalAmount),
                TotalUnapprovedSelfAmount = items.Sum(i => i.UnapprovedSelfAmount),

                TotalApprovedBfpTotalAmount = items.Sum(i => i.ApprovedBfpTotalAmount),
                TotalApprovedSelfAmount = items.Sum(i => i.ApprovedSelfAmount),

                TotalRevalidatedBfpTotalAmount = items.Sum(i => i.RevalidatedBfpTotalAmount),
                TotalRevalidatedSelfAmount = items.Sum(i => i.RevalidatedSelfAmount),

                TotalDebtReimbursedAmount = items.Sum(i => i.DebtReimbursedAmount),

                TotalCorrectedApprovedBfpTotalAmountNoCert = items.Sum(i => i.CorrectedApprovedBfpTotalAmountNoCert),
                TotalCorrectedApprovedSelfAmountNoCert = items.Sum(i => i.CorrectedApprovedSelfAmountNoCert),

                TotalCorrectedApprovedBfpTotalAmountCert = items.Sum(i => i.CorrectedApprovedBfpTotalAmountCert),
                TotalCorrectedApprovedSelfAmountCert = items.Sum(i => i.CorrectedApprovedSelfAmountCert),

                TotalIncludedInCertReportBfpTotalAmount = items.Sum(i => i.IncludedInCertReportBfpTotalAmount),
                TotalIncludedInCertReportSelfAmount = items.Sum(i => i.IncludedInCertReportSelfAmount),
            };

            return result;
        }

        public IList<SapCertReportVO> GetSapCertReports(int? certReportId)
        {
            var predicate = PredicateBuilder.True<CertReport>()
                .AndEquals(cr => cr.CertReportId, certReportId);

            var certReports =
                from cr in this.unitOfWork.DbContext.Set<CertReport>().Where(predicate)
                where cr.Status == CertReportStatus.Approved || cr.Status == CertReportStatus.PartialyApproved
                select cr;

            var amounts = this.GetCertReportAdvancePaymentAmounts()
                .Concat(this.GetCertReportFinancialCSDBudgetItems())
                .Concat(this.GetCertReportCorrections())
                .Concat(this.GetCertReportFinancialCorrectionCSDs())
                .Concat(this.GetCertReportCertCorrections())
                .Concat(this.GetCertReportFinancialCertCorrectionCSDs())
                .Concat(this.GetCertReportRevalidations())
                .Concat(this.GetCertReportFinancialRevalidationCSDs());

            var groupedAmounts = (from a in amounts
                                  join cert in certReports on a.CertReportId equals cert.CertReportId
                                  join c in this.unitOfWork.DbContext.Set<Contract>() on a.ContractId equals c.ContractId
                                  join p in this.unitOfWork.DbContext.Set<ContractReportPayment>() on a.ContractReportPaymentId equals p.ContractReportPaymentId
                                  join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on a.ProgrammePriorityId equals pp.MapNodeId
                                  group new
                                  {
                                      a.CertifiedBfpTotalAmount,
                                      a.CertifiedSelfAmount,
                                  }
                                  by new
                                  {
                                      ContractRegNumber = c.RegNumber,
                                      ContractReportPaymentNum = p.VersionNum,
                                      ContractReportPaymentType = p.PaymentType,
                                      ProgrammePriorityCode = pp.Code,
                                      a.AdvancePayment,
                                      cert.CertReportId,
                                      cert.ApprovalDate,
                                      cert.DateFrom,
                                      cert.DateTo,
                                      cert.CertReportNumber,
                                  }
                           into g
                                  orderby
                                      g.Key.ApprovalDate,
                                      g.Key.CertReportNumber,
                                      g.Key.ContractRegNumber,
                                      g.Key.ContractReportPaymentNum,
                                      g.Key.ProgrammePriorityCode
                                  select new
                                  {
                                      g.Key.ContractRegNumber,
                                      g.Key.ContractReportPaymentNum,
                                      g.Key.ContractReportPaymentType,
                                      g.Key.ProgrammePriorityCode,
                                      g.Key.AdvancePayment,
                                      g.Key.CertReportId,
                                      g.Key.ApprovalDate,
                                      g.Key.DateFrom,
                                      g.Key.DateTo,
                                      g.Key.CertReportNumber,
                                      CertifiedBfpTotalAmount = g.Sum(o => o.CertifiedBfpTotalAmount ?? 0m),
                                      CertifiedSelfAmount = g.Sum(o => o.CertifiedSelfAmount ?? 0m),
                                  })
                           .ToList()
                           .GroupBy(
                                g => new
                                {
                                    g.ContractRegNumber,
                                    g.ContractReportPaymentNum,
                                    g.ContractReportPaymentType,
                                    g.ProgrammePriorityCode,
                                    CoversAdvancePayment = g.AdvancePayment == YesNoNonApplicable.Yes,
                                    g.CertReportId,
                                    g.ApprovalDate,
                                    g.DateFrom,
                                    g.DateTo,
                                    g.CertReportNumber,
                                },
                                (key, g) => new
                                {
                                    key.ContractRegNumber,
                                    key.ContractReportPaymentNum,
                                    key.ContractReportPaymentType,
                                    key.ProgrammePriorityCode,
                                    key.CoversAdvancePayment,
                                    key.CertReportId,
                                    key.ApprovalDate,
                                    key.DateFrom,
                                    key.DateTo,
                                    key.CertReportNumber,
                                    CertifiedBfpTotalAmount = g.Sum(o => o.CertifiedBfpTotalAmount),
                                    CertifiedSelfAmount = g.Sum(o => o.CertifiedSelfAmount),
                                });

            List<SapCertReportVO> scrvo = new List<SapCertReportVO>();

            foreach (var r in groupedAmounts)
            {
                if (r.ContractReportPaymentType == ContractReportPaymentType.AdvanceVerification)
                {
                    scrvo.Add(
                        new SapCertReportVO(
                            r.ContractRegNumber,
                            r.ContractReportPaymentNum,
                            r.CertReportNumber,
                            r.ProgrammePriorityCode,
                            this.GetCertFinancialPeriodString(r.DateFrom, r.DateTo),
                            r.ApprovalDate,
                            FinaceSourcesSAP.AdvanceVerification,
                            r.CertifiedBfpTotalAmount));
                    continue;
                }

                if (r.CertifiedBfpTotalAmount != 0)
                {
                    scrvo.Add(
                        new SapCertReportVO(
                            r.ContractRegNumber,
                            r.ContractReportPaymentNum,
                            r.CertReportNumber,
                            r.ProgrammePriorityCode,
                            this.GetCertFinancialPeriodString(r.DateFrom, r.DateTo),
                            r.ApprovalDate,
                            FinaceSourcesSAP.Bfp,
                            r.CertifiedBfpTotalAmount));

                    if (r.CoversAdvancePayment)
                    {
                        scrvo.Add(
                            new SapCertReportVO(
                                r.ContractRegNumber,
                                r.ContractReportPaymentNum,
                                r.CertReportNumber,
                                r.ProgrammePriorityCode,
                                this.GetCertFinancialPeriodString(r.DateFrom, r.DateTo),
                                r.ApprovalDate,
                                FinaceSourcesSAP.AdvanceVerification,
                                -r.CertifiedBfpTotalAmount));
                    }
                }

                if (r.CertifiedSelfAmount != 0)
                {
                    scrvo.Add(
                        new SapCertReportVO(
                            r.ContractRegNumber,
                            r.ContractReportPaymentNum,
                            r.CertReportNumber,
                            r.ProgrammePriorityCode,
                            this.GetCertFinancialPeriodString(r.DateFrom, r.DateTo),
                            r.ApprovalDate,
                            FinaceSourcesSAP.SelfFinancing,
                            r.CertifiedSelfAmount));
                }
            }

            return scrvo;
        }

        public CertReportInfoVO GetInfo(int certReportId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on cr.ProgrammeId equals pr.MapNodeId
                    where cr.CertReportId == certReportId
                    select new CertReportInfoVO
                    {
                        OrderNum = cr.OrderNum,
                        OrderVersionNum = cr.OrderVersionNum,
                        ProgrammeShortName = pr.ShortName,
                        StatusDescription = cr.Status,
                        TypeDescription = cr.Type,

                        DateFrom = cr.DateFrom,
                        DateTo = cr.DateTo,
                        Status = cr.Status,
                        Type = cr.Type,
                        Version = cr.Version,
                    }).Single();
        }

        private (int, int) GetFinancialYear(DateTime date)
        {
            if (date.Date <= new DateTime(date.Year, 6, 30))
            {
                return (date.Year - 1, date.Year);
            }
            else
            {
                return (date.Year, date.Year + 1);
            }
        }

        private string GetCertFinancialPeriodString(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate.HasValue && toDate.HasValue)
            {
                var p1 = this.GetFinancialYear(fromDate.Value);
                var p2 = this.GetFinancialYear(toDate.Value);

                if (ValueTuple.Equals(p1, p2))
                {
                    return $"{p1.Item1}-{p1.Item2}";
                }
                else
                {
                    return $"{p1.Item1}-{p1.Item2}, {p2.Item1}-{p2.Item2}";
                }
            }
            else
            {
                return null;
            }
        }

        private IQueryable<CertReportApprovedCertifiedAmountVO> GetCertReportAdvancePaymentAmounts()
        {
            return from apa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>()
                   join c in this.unitOfWork.DbContext.Set<Contract>() on apa.ContractId equals c.ContractId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on apa.ProgrammePriorityId equals pp.MapNodeId
                   where apa.CertReportId.HasValue && apa.Status == ContractReportAdvancePaymentAmountStatus.Ended
                   select new CertReportApprovedCertifiedAmountVO
                   {
                       Id = apa.ContractReportAdvancePaymentAmountId,
                       ContractId = (int?)apa.ContractId,
                       ContractType = c.ContractType,
                       CertReportId = apa.CertReportId.Value,
                       ContractReportId = apa.ContractReportId,
                       ContractReportPaymentId = (int?)apa.ContractReportPaymentId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       AdvancePayment = null,
                       ApprovedEuAmount = apa.ApprovedEuAmount,
                       ApprovedBgAmount = apa.ApprovedBgAmount,
                       ApprovedBfpTotalAmount = apa.ApprovedBfpTotalAmount,
                       ApprovedSelfAmount = (decimal?)null,
                       ApprovedTotalAmount = apa.ApprovedBfpTotalAmount,
                       CertifiedEuAmount = apa.CertifiedApprovedEuAmount,
                       CertifiedBgAmount = apa.CertifiedApprovedBgAmount,
                       CertifiedBfpTotalAmount = apa.CertifiedApprovedBfpTotalAmount,
                       CertifiedSelfAmount = (decimal?)null,
                       CertifiedTotalAmount = apa.CertifiedApprovedBfpTotalAmount,
                   };
        }

        private IQueryable<CertReportApprovedCertifiedAmountVO> GetCertReportFinancialCSDBudgetItems()
        {
            return from fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fbi.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Contract>() on fbi.ContractId equals c.ContractId
                   where fbi.CertReportId.HasValue && crp.Status == ContractReportPaymentStatus.Actual && fbi.Status == ContractReportFinancialCSDBudgetItemStatus.Ended
                   select new CertReportApprovedCertifiedAmountVO
                   {
                       Id = fbi.ContractReportFinancialCSDBudgetItemId,
                       ContractId = (int?)fbi.ContractId,
                       ContractType = c.ContractType,
                       CertReportId = fbi.CertReportId.Value,
                       ContractReportId = fbi.ContractReportId,
                       ContractReportPaymentId = (int?)crp.ContractReportPaymentId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       AdvancePayment = fbi.AdvancePayment,
                       ApprovedEuAmount = fbi.ApprovedEuAmount,
                       ApprovedBgAmount = fbi.ApprovedBgAmount,
                       ApprovedBfpTotalAmount = fbi.ApprovedBfpTotalAmount,
                       ApprovedSelfAmount = fbi.ApprovedSelfAmount,
                       ApprovedTotalAmount = fbi.ApprovedTotalAmount,
                       CertifiedEuAmount = fbi.CertifiedApprovedEuAmount,
                       CertifiedBgAmount = fbi.CertifiedApprovedBgAmount,
                       CertifiedBfpTotalAmount = fbi.CertifiedApprovedEuAmount + fbi.CertifiedApprovedBgAmount,
                       CertifiedSelfAmount = fbi.CertifiedApprovedSelfAmount,
                       CertifiedTotalAmount = fbi.CertifiedApprovedTotalAmount,
                   };
        }

        private IQueryable<CertReportApprovedCertifiedAmountVO> GetCertReportCorrections()
        {
            return from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()

                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on crc.ProgrammePriorityId equals pp.MapNodeId into g0
                   from pp in g0.DefaultIfEmpty()

                   join c in this.unitOfWork.DbContext.Set<Contract>() on crc.ContractId equals c.ContractId into g1
                   from c in g1.DefaultIfEmpty()

                   where crc.CertReportId.HasValue && crc.Status == ContractReportCorrectionStatus.Entered
                   select new CertReportApprovedCertifiedAmountVO
                   {
                       Id = crc.ContractReportCorrectionId,
                       ContractId = c.ContractId,
                       ContractType = c.ContractType,
                       CertReportId = crc.CertReportId.Value,
                       ContractReportId = null,
                       ContractReportPaymentId = crc.ContractReportPaymentId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       AdvancePayment = null,
                       ApprovedEuAmount = (int)crc.Sign * crc.CorrectedApprovedEuAmount,
                       ApprovedBgAmount = (int)crc.Sign * crc.CorrectedApprovedBgAmount,
                       ApprovedBfpTotalAmount = (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount,
                       ApprovedSelfAmount = (int)crc.Sign * crc.CorrectedApprovedSelfAmount,
                       ApprovedTotalAmount = (int)crc.Sign * crc.CorrectedApprovedTotalAmount,
                       CertifiedEuAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedEuAmount,
                       CertifiedBgAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedBgAmount,
                       CertifiedBfpTotalAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedBfpTotalAmount,
                       CertifiedSelfAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedSelfAmount,
                       CertifiedTotalAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedTotalAmount,
                   };
        }

        private IQueryable<CertReportApprovedCertifiedAmountVO> GetCertReportFinancialCorrectionCSDs()
        {
            return from fc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                   join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fc.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Contract>() on fc.ContractId equals c.ContractId
                   where fc.CertReportId.HasValue && crp.Status == ContractReportPaymentStatus.Actual && fc.Status == ContractReportFinancialCorrectionCSDStatus.Ended
                   select new CertReportApprovedCertifiedAmountVO
                   {
                       Id = fc.ContractReportFinancialCorrectionCSDId,
                       ContractId = (int?)fc.ContractId,
                       ContractType = c.ContractType,
                       CertReportId = fc.CertReportId.Value,
                       ContractReportId = fc.ContractReportId,
                       ContractReportPaymentId = (int?)crp.ContractReportPaymentId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       AdvancePayment = fbi.AdvancePayment,
                       ApprovedEuAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedEuAmount,
                       ApprovedBgAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedBgAmount,
                       ApprovedBfpTotalAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedBfpTotalAmount,
                       ApprovedSelfAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedSelfAmount,
                       ApprovedTotalAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedTotalAmount,
                       CertifiedEuAmount = -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedEuAmount,
                       CertifiedBgAmount = -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedBgAmount,
                       CertifiedBfpTotalAmount = -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedBfpTotalAmount,
                       CertifiedSelfAmount = -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedSelfAmount,
                       CertifiedTotalAmount = -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedTotalAmount,
                   };
        }

        private IQueryable<CertReportApprovedCertifiedAmountVO> GetCertReportCertCorrections()
        {
            return from cc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()

                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on cc.ProgrammePriorityId equals pp.MapNodeId into g0
                   from pp in g0.DefaultIfEmpty()

                   join c in this.unitOfWork.DbContext.Set<Contract>() on cc.ContractId equals c.ContractId into g1
                   from c in g1.DefaultIfEmpty()

                   where cc.CertReportId.HasValue && cc.Status == ContractReportCertCorrectionStatus.Entered
                   select new CertReportApprovedCertifiedAmountVO
                   {
                       Id = cc.ContractReportCertCorrectionId,
                       ContractId = cc.ContractId,
                       ContractType = c.ContractType,
                       CertReportId = cc.CertReportId.Value,
                       ContractReportId = null,
                       ContractReportPaymentId = cc.ContractReportPaymentId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       AdvancePayment = null,
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
                   };
        }

        private IQueryable<CertReportApprovedCertifiedAmountVO> GetCertReportFinancialCertCorrectionCSDs()
        {
            return from fcc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                   join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fcc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fcc.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Contract>() on fcc.ContractId equals c.ContractId
                   where fcc.CertReportId.HasValue && crp.Status == ContractReportPaymentStatus.Actual && fcc.Status == ContractReportFinancialCertCorrectionCSDStatus.Ended
                   select new CertReportApprovedCertifiedAmountVO
                   {
                       Id = fcc.ContractReportFinancialCertCorrectionCSDId,
                       ContractId = (int?)fcc.ContractId,
                       ContractType = c.ContractType,
                       CertReportId = fcc.CertReportId.Value,
                       ContractReportId = fcc.ContractReportId,
                       ContractReportPaymentId = (int?)crp.ContractReportPaymentId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       AdvancePayment = fbi.AdvancePayment,
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
                   };
        }

        private IQueryable<CertReportApprovedCertifiedAmountVO> GetCertReportRevalidations()
        {
            return from r in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()

                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on r.ProgrammePriorityId equals pp.MapNodeId into g0
                   from pp in g0.DefaultIfEmpty()

                   join c in this.unitOfWork.DbContext.Set<Contract>() on r.ContractId equals c.ContractId into g1
                   from c in g1.DefaultIfEmpty()

                   where r.CertReportId.HasValue && r.Status == ContractReportRevalidationStatus.Entered
                   select new CertReportApprovedCertifiedAmountVO
                   {
                       Id = r.ContractReportRevalidationId,
                       ContractId = r.ContractId,
                       ContractType = c.ContractType,
                       CertReportId = r.CertReportId.Value,
                       ContractReportId = null,
                       ContractReportPaymentId = r.ContractReportPaymentId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       AdvancePayment = null,
                       ApprovedEuAmount = (int)r.Sign * r.RevalidatedEuAmount,
                       ApprovedBgAmount = (int)r.Sign * r.RevalidatedBgAmount,
                       ApprovedBfpTotalAmount = (int)r.Sign * r.RevalidatedBfpTotalAmount,
                       ApprovedSelfAmount = (int)r.Sign * r.RevalidatedSelfAmount,
                       ApprovedTotalAmount = (int)r.Sign * r.RevalidatedTotalAmount,
                       CertifiedEuAmount = (int)r.Sign * r.CertifiedRevalidatedEuAmount,
                       CertifiedBgAmount = (int)r.Sign * r.CertifiedRevalidatedBgAmount,
                       CertifiedBfpTotalAmount = (int)r.Sign * r.CertifiedRevalidatedBfpTotalAmount,
                       CertifiedSelfAmount = (int)r.Sign * r.CertifiedRevalidatedSelfAmount,
                       CertifiedTotalAmount = (int)r.Sign * r.CertifiedRevalidatedTotalAmount,
                   };
        }

        private IQueryable<CertReportApprovedCertifiedAmountVO> GetCertReportFinancialRevalidationCSDs()
        {
            return from fr in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                   join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fr.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fr.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Contract>() on fr.ContractId equals c.ContractId
                   where fr.CertReportId.HasValue && crp.Status == ContractReportPaymentStatus.Actual && fr.Status == ContractReportFinancialRevalidationCSDStatus.Ended
                   select new CertReportApprovedCertifiedAmountVO
                   {
                       Id = fr.ContractReportFinancialRevalidationCSDId,
                       ContractId = (int?)fr.ContractId,
                       ContractType = c.ContractType,
                       CertReportId = fr.CertReportId.Value,
                       ContractReportId = fr.ContractReportId,
                       ContractReportPaymentId = (int?)crp.ContractReportPaymentId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       AdvancePayment = fbi.AdvancePayment,
                       ApprovedEuAmount = (int)fr.Sign * fr.RevalidatedEuAmount,
                       ApprovedBgAmount = (int)fr.Sign * fr.RevalidatedBgAmount,
                       ApprovedBfpTotalAmount = (int)fr.Sign * fr.RevalidatedBfpTotalAmount,
                       ApprovedSelfAmount = (int)fr.Sign * fr.RevalidatedSelfAmount,
                       ApprovedTotalAmount = (int)fr.Sign * fr.RevalidatedTotalAmount,
                       CertifiedEuAmount = (int)fr.Sign * fr.CertifiedRevalidatedEuAmount,
                       CertifiedBgAmount = (int)fr.Sign * fr.CertifiedRevalidatedBgAmount,
                       CertifiedBfpTotalAmount = (int)fr.Sign * (fr.CertifiedRevalidatedEuAmount + fr.CertifiedRevalidatedBgAmount),
                       CertifiedSelfAmount = (int)fr.Sign * fr.CertifiedRevalidatedSelfAmount,
                       CertifiedTotalAmount = (int)fr.Sign * fr.CertifiedRevalidatedTotalAmount,
                   };
        }

        private IQueryable<CertReportApprovedCertifiedAmountVO> GetCertReportApprovedCertifiedAmounts()
        {
            return this.GetCertReportAdvancePaymentAmounts()
                .Concat(this.GetCertReportFinancialCSDBudgetItems().Where(csd => csd.AdvancePayment != YesNoNonApplicable.Yes))
                .Concat(this.GetCertReportCorrections())
                .Concat(this.GetCertReportFinancialCorrectionCSDs().Where(csd => csd.AdvancePayment != YesNoNonApplicable.Yes))
                .Concat(this.GetCertReportCertCorrections())
                .Concat(this.GetCertReportFinancialCertCorrectionCSDs())
                .Concat(this.GetCertReportRevalidations())
                .Concat(this.GetCertReportFinancialRevalidationCSDs());
        }

        public IQueryable<CertReportApprovedCertifiedGroupedAmountVO> GetCertReportAmountsGroupedByCertReport()
        {
            var advancePaymentAmounts =
                from apa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>()
                where apa.CertReportId.HasValue &&
                   apa.Status == ContractReportAdvancePaymentAmountStatus.Ended
                select new
                {
                    CertReportId = apa.CertReportId.Value,
                    ApprovedEuAmount = apa.ApprovedEuAmount,
                    ApprovedBgAmount = apa.ApprovedBgAmount,
                    ApprovedBfpTotalAmount = apa.ApprovedBfpTotalAmount,
                    ApprovedSelfAmount = (decimal?)null,
                    ApprovedTotalAmount = apa.ApprovedBfpTotalAmount,
                    CertifiedEuAmount = apa.CertifiedApprovedEuAmount,
                    CertifiedBgAmount = apa.CertifiedApprovedBgAmount,
                    CertifiedBfpTotalAmount = apa.CertifiedApprovedBfpTotalAmount,
                    CertifiedSelfAmount = (decimal?)null,
                    CertifiedTotalAmount = apa.CertifiedApprovedBfpTotalAmount,
                };

            var financialCSDBudgetItems =
                from fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fbi.ContractReportId equals crp.ContractReportId
                where fbi.CertReportId.HasValue &&
                   crp.Status == ContractReportPaymentStatus.Actual &&
                   fbi.Status == ContractReportFinancialCSDBudgetItemStatus.Ended &&
                   fbi.AdvancePayment != YesNoNonApplicable.Yes
                select new
                {
                    CertReportId = fbi.CertReportId.Value,
                    ApprovedEuAmount = fbi.ApprovedEuAmount,
                    ApprovedBgAmount = fbi.ApprovedBgAmount,
                    ApprovedBfpTotalAmount = fbi.ApprovedBfpTotalAmount,
                    ApprovedSelfAmount = fbi.ApprovedSelfAmount,
                    ApprovedTotalAmount = fbi.ApprovedTotalAmount,
                    CertifiedEuAmount = fbi.CertifiedApprovedEuAmount,
                    CertifiedBgAmount = fbi.CertifiedApprovedBgAmount,
                    CertifiedBfpTotalAmount = fbi.CertifiedApprovedEuAmount + fbi.CertifiedApprovedBgAmount,
                    CertifiedSelfAmount = fbi.CertifiedApprovedSelfAmount,
                    CertifiedTotalAmount = fbi.CertifiedApprovedTotalAmount,
                };

            var corrections =
                from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                where crc.CertReportId.HasValue &&
                   crc.Status == ContractReportCorrectionStatus.Entered
                select new
                {
                    CertReportId = crc.CertReportId.Value,
                    ApprovedEuAmount = (int)crc.Sign * crc.CorrectedApprovedEuAmount,
                    ApprovedBgAmount = (int)crc.Sign * crc.CorrectedApprovedBgAmount,
                    ApprovedBfpTotalAmount = (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount,
                    ApprovedSelfAmount = (int)crc.Sign * crc.CorrectedApprovedSelfAmount,
                    ApprovedTotalAmount = (int)crc.Sign * crc.CorrectedApprovedTotalAmount,
                    CertifiedEuAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedEuAmount,
                    CertifiedBgAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedBgAmount,
                    CertifiedBfpTotalAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedBfpTotalAmount,
                    CertifiedSelfAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedSelfAmount,
                    CertifiedTotalAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedTotalAmount,
                };

            var financialCorrectionCSDs =
                from fc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fc.ContractReportId equals crp.ContractReportId
                where fc.CertReportId.HasValue &&
                   crp.Status == ContractReportPaymentStatus.Actual &&
                   fc.Status == ContractReportFinancialCorrectionCSDStatus.Ended &&
                   fbi.AdvancePayment != YesNoNonApplicable.Yes
                select new
                {
                    CertReportId = fc.CertReportId.Value,
                    ApprovedEuAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedEuAmount,
                    ApprovedBgAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedBgAmount,
                    ApprovedBfpTotalAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedBfpTotalAmount,
                    ApprovedSelfAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedSelfAmount,
                    ApprovedTotalAmount = -1 * (int)fc.Sign * fc.CorrectedApprovedTotalAmount,
                    CertifiedEuAmount = -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedEuAmount,
                    CertifiedBgAmount = -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedBgAmount,
                    CertifiedBfpTotalAmount = -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedBfpTotalAmount,
                    CertifiedSelfAmount = -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedSelfAmount,
                    CertifiedTotalAmount = -1 * (int)fc.Sign * fc.CertifiedCorrectedApprovedTotalAmount,
                };

            var certCorrections =
                from cc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()
                where cc.CertReportId.HasValue &&
                   cc.Status == ContractReportCertCorrectionStatus.Entered
                select new
                {
                    CertReportId = cc.CertReportId.Value,
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
                };

            var financialCertCorrectionCSDs =
                from fcc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fcc.ContractReportId equals crp.ContractReportId
                where fcc.CertReportId.HasValue &&
                   crp.Status == ContractReportPaymentStatus.Actual &&
                   fcc.Status == ContractReportFinancialCertCorrectionCSDStatus.Ended
                select new
                {
                    CertReportId = fcc.CertReportId.Value,
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
                };

            var revalidations =
                from r in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
                where r.CertReportId.HasValue && r.Status == ContractReportRevalidationStatus.Entered
                select new
                {
                    CertReportId = r.CertReportId.Value,
                    ApprovedEuAmount = (int)r.Sign * r.RevalidatedEuAmount,
                    ApprovedBgAmount = (int)r.Sign * r.RevalidatedBgAmount,
                    ApprovedBfpTotalAmount = (int)r.Sign * r.RevalidatedBfpTotalAmount,
                    ApprovedSelfAmount = (int)r.Sign * r.RevalidatedSelfAmount,
                    ApprovedTotalAmount = (int)r.Sign * r.RevalidatedTotalAmount,
                    CertifiedEuAmount = (int)r.Sign * r.CertifiedRevalidatedEuAmount,
                    CertifiedBgAmount = (int)r.Sign * r.CertifiedRevalidatedBgAmount,
                    CertifiedBfpTotalAmount = (int)r.Sign * r.CertifiedRevalidatedBfpTotalAmount,
                    CertifiedSelfAmount = (int)r.Sign * r.CertifiedRevalidatedSelfAmount,
                    CertifiedTotalAmount = (int)r.Sign * r.CertifiedRevalidatedTotalAmount,
                };

            var financialRevalidationCSDs =
                from fr in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fr.ContractReportId equals crp.ContractReportId
                where fr.CertReportId.HasValue &&
                   crp.Status == ContractReportPaymentStatus.Actual &&
                   fr.Status == ContractReportFinancialRevalidationCSDStatus.Ended
                select new
                {
                    CertReportId = fr.CertReportId.Value,
                    ApprovedEuAmount = (int)fr.Sign * fr.RevalidatedEuAmount,
                    ApprovedBgAmount = (int)fr.Sign * fr.RevalidatedBgAmount,
                    ApprovedBfpTotalAmount = (int)fr.Sign * fr.RevalidatedBfpTotalAmount,
                    ApprovedSelfAmount = (int)fr.Sign * fr.RevalidatedSelfAmount,
                    ApprovedTotalAmount = (int)fr.Sign * fr.RevalidatedTotalAmount,
                    CertifiedEuAmount = (int)fr.Sign * fr.CertifiedRevalidatedEuAmount,
                    CertifiedBgAmount = (int)fr.Sign * fr.CertifiedRevalidatedBgAmount,
                    CertifiedBfpTotalAmount = (int)fr.Sign * (fr.CertifiedRevalidatedEuAmount + fr.CertifiedRevalidatedBgAmount),
                    CertifiedSelfAmount = (int)fr.Sign * fr.CertifiedRevalidatedSelfAmount,
                    CertifiedTotalAmount = (int)fr.Sign * fr.CertifiedRevalidatedTotalAmount,
                };

            var debtInterests =
                from dra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                join cr in this.unitOfWork.DbContext.Set<CertReport>() on dra.CertReportId equals cr.CertReportId
                select new
                {
                    CertReportId = cr.CertReportId,
                    ApprovedEuAmount = -1 * dra.InterestBfp.EuAmount,
                    ApprovedBgAmount = -1 * dra.InterestBfp.BgAmount,
                    ApprovedBfpTotalAmount = -1 * dra.InterestBfp.TotalAmount,
                    ApprovedSelfAmount = (decimal?)0m,
                    ApprovedTotalAmount = -1 * dra.InterestBfp.TotalAmount,
                    CertifiedEuAmount = -1 * dra.InterestBfp.EuAmount,
                    CertifiedBgAmount = -1 * dra.InterestBfp.BgAmount,
                    CertifiedBfpTotalAmount = -1 * dra.InterestBfp.TotalAmount,
                    CertifiedSelfAmount = (decimal?)0m,
                    CertifiedTotalAmount = -1 * dra.InterestBfp.TotalAmount,
                };

            var amounts =
                advancePaymentAmounts
                .Concat(financialCSDBudgetItems)
                .Concat(corrections)
                .Concat(financialCorrectionCSDs)
                .Concat(certCorrections)
                .Concat(financialCertCorrectionCSDs)
                .Concat(revalidations)
                .Concat(financialRevalidationCSDs)
                .Concat(debtInterests);

            return from a in amounts
                   group new
                   {
                       a.ApprovedEuAmount,
                       a.ApprovedBgAmount,
                       a.ApprovedBfpTotalAmount,
                       a.ApprovedSelfAmount,
                       a.ApprovedTotalAmount,
                       a.CertifiedEuAmount,
                       a.CertifiedBgAmount,
                       a.CertifiedBfpTotalAmount,
                       a.CertifiedSelfAmount,
                       a.CertifiedTotalAmount,
                   }
                   by a.CertReportId
                    into g
                   select new CertReportApprovedCertifiedGroupedAmountVO
                   {
                       CertReportId = g.Key,
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
        }

        private class CertReportApprovedCertifiedAmountVO
        {
            public int Id { get; set; }

            public int? ContractId { get; set; }

            public ContractType? ContractType { get; set; }

            public int CertReportId { get; set; }

            public int? ContractReportId { get; set; }

            public int? ContractReportPaymentId { get; set; }

            public int? ProgrammePriorityId { get; set; }

            public string ProgrammePriorityName { get; set; }

            public YesNoNonApplicable? AdvancePayment { get; set; }

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
    }
}
