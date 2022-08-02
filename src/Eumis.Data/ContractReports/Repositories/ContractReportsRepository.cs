using Eumis.Common.Db;
using Eumis.Data.ContractReports.PortalViewObjects;
using Eumis.Data.ContractReports.ViewObjects.ContractBudgetTree;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.CertReports;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportsRepository : AggregateRepository<ContractReport>, IContractReportsRepository
    {
        public ContractReportsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReport, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReport, object>>[]
                {
                    c => c.ContractReportAttachedFinancialCorrections,
                };
            }
        }

        public ContractReport Find(Guid gid)
        {
            return this.Set()
                .Where(p => p.Gid == gid)
                .Single();
        }

        public async Task<ContractReport> FindAsync(Guid gid, CancellationToken ct)
        {
            return await this.Set()
                .Where(p => p.Gid == gid)
                .SingleAsync(ct);
        }

        public async Task<ContractReport> FindForUpdateAsync(Guid gid, byte[] version, CancellationToken ct)
        {
            var contractReport = await this.FindAsync(gid, ct);

            this.CheckVersion(contractReport.Version, version);

            return contractReport;
        }

        public ContractReport FindByNum(int contractId, string contractReportNum)
        {
            return this.Set()
                .Where(p => p.ContractId == contractId && p.OrderNum.ToString() == contractReportNum)
                .Single();
        }

        public int GetContractId(int contractReportId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    where cr.ContractReportId == contractReportId
                    select cr.ContractId).Single();
        }

        public int GetNextOrderNumber(int contractId)
        {
            var lastOrderNumber = this.Set()
                .Where(t => t.ContractId == contractId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public async Task<int> GetNextOrderNumberAsync(int contractId, CancellationToken ct)
        {
            var lastOrderNumber = await this.Set()
                .Where(t => t.ContractId == contractId)
                .MaxAsync(p => (int?)p.OrderNum, ct);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public int GetContractReportId(Guid gid)
        {
            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    where cr.Gid == gid
                    select cr.ContractReportId).Single();
        }

        public IList<ContractReportVO> GetContractReports(
            int[] programmeIds,
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null)
        {
            var predicate = PredicateBuilder.True<ContractReport>()
                .AndStringContains(t => t.OrderNum.ToString(), contractReportNum);

            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            var procedurePredicate = PredicateBuilder.True<Procedure>()
                .AndEquals(t => t.ProcedureId, procedureId);

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(predicate)
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>().Where(procedurePredicate) on c.ProcedureId equals p.ProcedureId

                    where programmeIds.Contains(c.ProgrammeId) && (cr.Status != ContractReportStatus.Draft || (cr.Status == ContractReportStatus.Draft && cr.Source == Source.AdministrativeAuthority))
                    orderby cr.CreateDate descending
                    select new ContractReportVO()
                    {
                        ContractReportId = cr.ContractReportId,
                        ContractId = cr.ContractId,
                        Gid = cr.Gid,
                        ReportType = cr.ReportType,
                        OrderNum = cr.OrderNum,
                        Status = cr.Status,
                        StatusNote = cr.StatusNote,
                        Source = cr.Source,
                        SubmitDate = cr.SubmitDate,
                        SubmitDeadline = cr.SubmitDeadline,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureId = p.ProcedureId,
                    })
                .ToList();
        }

        public bool CanCreate(int contractId)
        {
            var allowedStatuses = ContractReport.GetCreationStatuses();

            return !(from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                     where cr.ContractId == contractId && !allowedStatuses.Contains(cr.Status)
                     select cr.ContractReportId).Any();
        }

        public async Task<bool> CanCreateAsync(int contractId, CancellationToken ct)
        {
            var allowedStatuses = ContractReport.GetCreationStatuses();

            var result = await (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                                where cr.ContractId == contractId && !allowedStatuses.Contains(cr.Status)
                                select cr.ContractReportId).AnyAsync(ct);
            return !result;
        }

        public bool CanEditSentContractReport(int contractId)
        {
            var hasManySentReports = (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                                      where cr.ContractId == contractId && cr.Status == ContractReportStatus.SentChecked
                                      select cr).Count() > 1;

            return !hasManySentReports;
        }

        public async Task<bool> CanEditSentContractReportAsync(int contractId, CancellationToken ct)
        {
            var hasManySentReports = await (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                                            where cr.ContractId == contractId && cr.Status == ContractReportStatus.SentChecked
                                            select cr).CountAsync(ct) > 1;

            return !hasManySentReports;
        }

        public bool CanChangeContractReportStatusToUnchecked(int contractReportId)
        {
            return true;
        }

        public async Task<PagePVO<ContractReportPVO>> GetPortalContractReportsAsync(Guid contractGid, CancellationToken ct, int offset = 0, int? limit = null)
        {
            var query = from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                        join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId

                        where c.Gid == contractGid && (cr.Status != ContractReportStatus.Draft || (cr.Status == ContractReportStatus.Draft && cr.Source == Source.Beneficiary))
                        orderby cr.OrderNum descending
                        select new
                        {
                            cr.ContractReportId,
                            cr.Gid,
                            cr.ReportType,
                            cr.Status,
                            cr.StatusNote,
                            cr.OrderNum,
                            cr.Source,
                            cr.OtherRegistration,
                            cr.StoragePlace,
                            cr.SubmitDate,
                            cr.SubmitDeadline,
                            cr.CreateDate,
                            cr.ModifyDate,
                            cr.Version,
                        };

            var queryWithOffsetAndLimit = query.WithOffsetAndLimit(offset, limit);

            var financials = (await (from crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                                     where queryWithOffsetAndLimit.Any(t => t.ContractReportId == crf.ContractReportId)
                                     select crf)
                              .ToListAsync(ct))
                              .GroupBy(t => t.ContractReportId)
                              .ToDictionary(t => t.Key, t => t);

            var technicals = (from crf in this.unitOfWork.DbContext.Set<ContractReportTechnical>()
                              where queryWithOffsetAndLimit.Any(t => t.ContractReportId == crf.ContractReportId)
                              select crf)
                              .ToList()
                              .GroupBy(t => t.ContractReportId)
                              .ToDictionary(t => t.Key, t => t);

            var payments = (await (from crf in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                                   where queryWithOffsetAndLimit.Any(t => t.ContractReportId == crf.ContractReportId)
                                   select crf)
                            .ToListAsync(ct))
                            .GroupBy(t => t.ContractReportId)
                            .ToDictionary(t => t.Key, t => t);

            var micros = (await (from crf in this.unitOfWork.DbContext.Set<ContractReportMicro>()
                                 where queryWithOffsetAndLimit.Any(t => t.ContractReportId == crf.ContractReportId)
                                 select crf)
                          .ToListAsync(ct))
                          .GroupBy(t => t.ContractReportId)
                          .ToDictionary(t => t.Key, t => t);

            List<ContractReportMicroStatus> allowedMicroStatus = new List<ContractReportMicroStatus>();
            allowedMicroStatus.Add(ContractReportMicroStatus.Returned);
            allowedMicroStatus.Add(ContractReportMicroStatus.Actual);

            return new PagePVO<ContractReportPVO>()
            {
                Results = (await queryWithOffsetAndLimit
                    .ToListAsync())
                    .Select((cr, i) =>
                    {
                        var reportFinancials = financials.Keys.Contains(cr.ContractReportId) && financials[cr.ContractReportId].Any() ? financials[cr.ContractReportId] : null;
                        var reportTechnicals = technicals.Keys.Contains(cr.ContractReportId) && technicals[cr.ContractReportId].Any() ? technicals[cr.ContractReportId] : null;
                        var reportPayments = payments.Keys.Contains(cr.ContractReportId) && payments[cr.ContractReportId].Any() ? payments[cr.ContractReportId] : null;
                        var reportMicros = micros.Keys.Contains(cr.ContractReportId) && micros[cr.ContractReportId].Any() ? micros[cr.ContractReportId] : null;

                        var hasReturnedDocuments = offset == 0 && i == 0 &&
                        this.HasReturnedContractReportDocuments(
                            reportTechnicals ?? Enumerable.Empty<ContractReportTechnical>(),
                            reportFinancials ?? Enumerable.Empty<ContractReportFinancial>(),
                            reportPayments ?? Enumerable.Empty<ContractReportPayment>(),
                            reportMicros ?? Enumerable.Empty<ContractReportMicro>());

                        return new ContractReportPVO
                        {
                            Gid = cr.Gid,
                            ContractReportType = new EnumPVO<ContractReportType>()
                            {
                                Description = cr.ReportType,
                                Value = cr.ReportType,
                            },
                            Status = new EnumPVO<ContractReportStatus>()
                            {
                                Description = cr.Status,
                                Value = cr.Status,
                            },
                            StatusNote = cr.StatusNote,
                            OrderNum = cr.OrderNum,
                            Source = new EnumPVO<Source>()
                            {
                                Description = cr.Source,
                                Value = cr.Source,
                            },
                            OtherRegistration = cr.OtherRegistration,
                            StoragePlace = cr.StoragePlace,
                            SubmitDate = cr.SubmitDate,
                            SubmitDeadline = cr.SubmitDeadline,
                            CreateDate = cr.CreateDate,
                            ModifyDate = cr.ModifyDate,
                            Version = cr.Version,
                            ContractReportFinancials =
                                reportFinancials?.Select(t => new ContractReportFinancialPVO(t)).OrderByDescending(t => t.CreateDate).ToList(),
                            ContractReportTechnicals =
                                reportTechnicals?.Select(t => new ContractReportTechnicalPVO(t)).OrderByDescending(t => t.CreateDate).ToList(),
                            ContractReportPayments =
                                reportPayments?.Select(t => new ContractReportPaymentPVO(t)).OrderByDescending(t => t.CreateDate).ToList(),
                            ContractReportType1Micros =
                                reportMicros?.Where(m => m.Type == ContractReportMicroType.Type1).Select(m => new ContractReportMicroPVO(m)).OrderByDescending(m => m.CreateDate).ToList(),
                            ContractReportType2Micros =
                                reportMicros?.Where(m => m.Type == ContractReportMicroType.Type2 && ((allowedMicroStatus.Contains(m.Status) && m.Source == Source.AdministrativeAuthority) || m.Source == Source.Beneficiary)).Select(m => new ContractReportMicroPVO(m)).OrderByDescending(m => m.CreateDate).ToList(),
                            ContractReportType3Micros =
                                reportMicros?.Where(m => m.Type == ContractReportMicroType.Type3).Select(m => new ContractReportMicroPVO(m)).OrderByDescending(m => m.CreateDate).ToList(),
                            ContractReportType4Micros =
                                reportMicros?.Where(m => m.Type == ContractReportMicroType.Type4).Select(m => new ContractReportMicroPVO(m)).OrderByDescending(m => m.CreateDate).ToList(),
                            HasReturnedDocuments = hasReturnedDocuments,
                        };
                    }).ToList(),
                Count = query.Count(),
            };
        }

        public bool HasContractReportInProgress(int contractId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    where cr.ContractId == contractId && !ContractReport.FinalStatuses.Contains(cr.Status)
                    select cr.ContractReportId).Any();
        }

        public async Task<bool> HasContractReportInProgressAsync(int contractId, CancellationToken ct)
        {
            return await (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                          where cr.ContractId == contractId && !ContractReport.FinalStatuses.Contains(cr.Status)
                          select cr.ContractReportId).AnyAsync(ct);
        }

        public bool HasContractReportInProgress(int contractId, int contractReportId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    where cr.ContractId == contractId && cr.ContractReportId != contractReportId && !ContractReport.FinalStatuses.Contains(cr.Status)
                    select cr.ContractReportId).Any();
        }

        public async Task<bool> HasContractReportInProgressAsync(int contractId, int contractReportId, CancellationToken ct)
        {
            return await (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                          where cr.ContractId == contractId && cr.ContractReportId != contractReportId && !ContractReport.FinalStatuses.Contains(cr.Status)
                          select cr.ContractReportId).AnyAsync(ct);
        }

        public bool HasContractReportDraft(int contractId, int contractReportId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    where cr.ContractId == contractId && cr.ContractReportId != contractReportId && cr.Status == ContractReportStatus.Draft
                    select cr.ContractReportId).Any();
        }

        public async Task<bool> HasContractReportDraftAsync(int contractId, int contractReportId, CancellationToken ct)
        {
            return await (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                          where cr.ContractId == contractId && cr.ContractReportId != contractReportId && cr.Status == ContractReportStatus.Draft
                          select cr.ContractReportId).AnyAsync(ct);
        }

        public bool HasAdvanceVerificationPayment(int contractId, int contractReportId)
        {
            return (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                    where
                    crp.ContractId == contractId &&
                    cr.ContractReportId == contractReportId &&
                    cr.Status == ContractReportStatus.Accepted &&
                    crp.PaymentType == ContractReportPaymentType.AdvanceVerification
                    select crp)
                    .Any();
        }

        public async Task<bool> HasAdvanceVerificationPaymentAsync(int contractId, int contractReportId, CancellationToken ct)
        {
            var result = await (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                    where
                    crp.ContractId == contractId &&
                    cr.ContractReportId == contractReportId &&
                    cr.Status == ContractReportStatus.Accepted &&
                    crp.PaymentType == ContractReportPaymentType.AdvanceVerification
                    select crp)
                    .AnyAsync(ct);

            return result;
        }

        public IList<ContractReportVO> GetContractReportChecksContractReports(
            int[] programmeIds,
            int userId,
            string contractRegNum = null)
        {
            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            var statusPredicate = PredicateBuilder.False<ContractReport>();
            ContractReport.MonitoringStatuses.ToList().ForEach(s => statusPredicate = statusPredicate.Or(o => o.Status == s));

            var contractProgrammePredicate = PredicateBuilder.False<Contract>();

            programmeIds.ToList().ForEach(p => contractProgrammePredicate = contractProgrammePredicate.Or(o => o.ProgrammeId == p));

            var externalVerificatorContracts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                               join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on cu.ContractId equals c.ContractId
                                               select c;

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(statusPredicate)
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate).Where(contractProgrammePredicate).Union(externalVerificatorContracts) on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    select new ContractReportVO()
                    {
                        ContractReportId = cr.ContractReportId,
                        ContractId = cr.ContractId,
                        Gid = cr.Gid,
                        ReportType = cr.ReportType,
                        OrderNum = cr.OrderNum,
                        Status = cr.Status,
                        StatusNote = cr.StatusNote,
                        Source = cr.Source,
                        SubmitDate = cr.SubmitDate,
                        SubmitDeadline = cr.SubmitDeadline,
                        CheckedDate = cr.CheckedDate,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                    })
                .Distinct()
                .OrderByDescending(cr => cr.SubmitDate)
                .ToList();
        }

        public bool IsContractReportNumExisting(string contractReportNum)
        {
            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    where cr.OrderNum.ToString() == contractReportNum
                    select cr.ContractReportId).Any();
        }

        public IList<ContractReportFinancialCorrectionVO> GetContractReportAttachedFinancialCorrections(int contractReportId)
        {
            return (from fc in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>()
                    join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on fc.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on fc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where fc.ContractReportId == contractReportId
                    select new ContractReportFinancialCorrectionVO
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
                .ToList();
        }

        public IList<ContractReportFinancialCorrectionVO> GetFinancialCorrectionsForContractReport(int contractId)
        {
            var subquery = from fc in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>()
                           where fc.ContractId == contractId
                           select fc.ContractReportFinancialCorrectionId;

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where !subquery.Contains(crfc.ContractReportFinancialCorrectionId) && crfc.ContractId == contractId
                    select new ContractReportFinancialCorrectionVO
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
                .ToList();
        }

        public IList<ContractReportFinancialCorrectionCSDsVO> GetContractReportAttachedFinancialCorrectionSignCorrectedAmounts(int contractReportId)
        {
            return (from fc in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>()
                    join fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on fc.ContractReportFinancialCorrectionId equals fccsd.ContractReportFinancialCorrectionId
                    join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                    join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on bi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                    join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                    where fc.ContractReportId == contractReportId
                    select new ContractReportFinancialCorrectionCSDsVO()
                    {
                        AdvancePayment = bi.AdvancePayment,
                        CrossFinancing = bi.CrossFinancing,
                        ProgrammePriorityId = ps.ProgrammePriorityId,
                        Sign = fccsd.Sign,
                        CorrectedApprovedEuAmount = fccsd.CorrectedApprovedEuAmount.Value,
                        CorrectedApprovedBgAmount = fccsd.CorrectedApprovedBgAmount.Value,
                        CorrectedApprovedBfpTotalAmount = fccsd.CorrectedApprovedBfpTotalAmount.Value,
                        CorrectedApprovedSelfAmount = fccsd.CorrectedApprovedSelfAmount.Value,
                        CorrectedApprovedTotalAmount = fccsd.CorrectedApprovedTotalAmount.Value,
                    })
                .ToList();
        }

        public async Task<IList<ContractReportFinancialCorrectionCSDsVO>> GetContractReportAttachedFinancialCorrectionSignCorrectedAmountsAsync(int contractReportId, CancellationToken ct)
        {
            var result = await (from fc in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>()
                                join fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on fc.ContractReportFinancialCorrectionId equals fccsd.ContractReportFinancialCorrectionId
                                join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                                join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on bi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                                join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                                where fc.ContractReportId == contractReportId
                                select new ContractReportFinancialCorrectionCSDsVO()
                                {
                                    AdvancePayment = bi.AdvancePayment,
                                    CrossFinancing = bi.CrossFinancing,
                                    ProgrammePriorityId = ps.ProgrammePriorityId,
                                    Sign = fccsd.Sign,
                                    CorrectedApprovedEuAmount = fccsd.CorrectedApprovedEuAmount.Value,
                                    CorrectedApprovedBgAmount = fccsd.CorrectedApprovedBgAmount.Value,
                                    CorrectedApprovedBfpTotalAmount = fccsd.CorrectedApprovedBfpTotalAmount.Value,
                                    CorrectedApprovedSelfAmount = fccsd.CorrectedApprovedSelfAmount.Value,
                                    CorrectedApprovedTotalAmount = fccsd.CorrectedApprovedTotalAmount.Value,
                                })
                                .ToListAsync(ct);
            return result;
        }

        public IList<ContractReportExcelVO> GetContractReportChecksContractReportsExcelExport(
            int[] programmeIds,
            string contractRegNum = null)
        {
            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            var paymentCheckAmounts = from crpc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>()
                                      join crpca in this.unitOfWork.DbContext.Set<ContractReportPaymentCheckAmount>() on crpc.ContractReportPaymentCheckId equals crpca.ContractReportPaymentCheckId
                                      where crpc.Status == ContractReportPaymentCheckStatus.Active
                                      group new
                                      {
                                          ApprovedBfpTotalAmount = crpca.ApprovedBfpTotalAmount,
                                          ApprovedSelfAmount = crpca.ApprovedSelfAmount,
                                      }
                                      by crpc.ContractReportId into g
                                      select new
                                      {
                                          ContractReportId = g.Key,
                                          ApprovedBfpTotalAmount = g.Sum(t => t.ApprovedBfpTotalAmount),
                                          ApprovedSelfAmount = g.Sum(t => t.ApprovedSelfAmount),
                                      };

            var statusPredicate = PredicateBuilder.False<ContractReport>();
            ContractReport.MonitoringStatuses.ToList().ForEach(s => statusPredicate = statusPredicate.Or(o => o.Status == s));

            var contractProgrammePredicate = PredicateBuilder.False<Contract>();

            programmeIds.ToList().ForEach(p => contractProgrammePredicate = contractProgrammePredicate.Or(o => o.ProgrammeId == p));

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(statusPredicate)
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate).Where(contractProgrammePredicate) on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join pca in paymentCheckAmounts on cr.ContractReportId equals pca.ContractReportId into g1
                    from pca in g1.DefaultIfEmpty()

                    orderby cr.CreateDate descending
                    select new ContractReportExcelVO()
                    {
                        ReportType = cr.ReportType,
                        OrderNum = cr.OrderNum,
                        Status = cr.Status,
                        SubmitDate = cr.SubmitDate,
                        CheckedDate = cr.CheckedDate,
                        ApprovedBfpTotalAmount = pca.ApprovedBfpTotalAmount,
                        ApprovedSelfAmount = pca.ApprovedSelfAmount,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                    })
                .ToList();
        }

        public ContractBudgetTreeVO GetContractReportPaymentRequests(int contractReportId)
        {
            var contractReport = this.Set().Where(t => t.ContractReportId == contractReportId).Single();
            var submitDate = contractReport.SubmitDate;
            var contractId = contractReport.ContractId;
            var contractVersion = (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                                   where cv.ContractId == contractId
                                   select cv)
                            .OrderByDescending(c => c.VersionNum)
                            .ThenByDescending(c => c.VersionSubNum)
                            .First();

            var contractDoc = contractVersion.GetDocument();
            var contractBudget = contractDoc
                .BFPContractDirectionsBudgetContract
                .BFPContractBudget
                .BFPContractProgrammeBudgetCollection;

            var correctedCSDAmounts = (from cbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                                       join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on cbi.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                                       join cr in this.unitOfWork.DbContext.Set<ContractReport>() on cbi.ContractReportId equals cr.ContractReportId
                                       join crc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on cbi.ContractReportFinancialCorrectionId equals crc.ContractReportFinancialCorrectionId
                                       where cbi.ContractId == contractId &&
                                           crc.Status == ContractReportFinancialCorrectionStatus.Ended &&
                                           cr.Status == ContractReportStatus.Accepted &&
                                           cr.SubmitDate < submitDate
                                       select new { cbi, bi })
                            .ToList()
                            .GroupBy(t => t.bi.BudgetDetailGid)
                            .ToDictionary(t => t.Key, t => new Tuple<decimal, decimal, decimal, decimal>(
                                t.Select(p => -1 * (int)p.cbi.Sign.Value * p.cbi.CorrectedApprovedEuAmount.Value).Aggregate(0M, (a, b) => a + b),
                                t.Select(p => -1 * (int)p.cbi.Sign.Value * p.cbi.CorrectedApprovedBgAmount.Value).Aggregate(0M, (a, b) => a + b),
                                t.Select(p => -1 * (int)p.cbi.Sign.Value * p.cbi.CorrectedApprovedSelfAmount.Value).Aggregate(0M, (a, b) => a + b),
                                t.Select(p => -1 * (int)p.cbi.Sign.Value * p.cbi.CorrectedApprovedTotalAmount.Value).Aggregate(0M, (a, b) => a + b)));

            var csdAmounts = (from bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                              join cr in this.unitOfWork.DbContext.Set<ContractReport>() on bi.ContractReportId equals cr.ContractReportId
                              where bi.ContractId == contractId && cr.Status == ContractReportStatus.Accepted && cr.SubmitDate < submitDate
                              select bi)
                    .ToList()
                    .GroupBy(t => t.BudgetDetailGid)
                    .ToDictionary(t => t.Key, t => new Tuple<decimal, decimal, decimal, decimal>(
                        t.Select(p => p.ApprovedEuAmount.Value).Aggregate(0M, (a, b) => a + b),
                        t.Select(p => p.ApprovedBgAmount.Value).Aggregate(0M, (a, b) => a + b),
                        t.Select(p => p.ApprovedSelfAmount.Value).Aggregate(0M, (a, b) => a + b),
                        t.Select(p => p.ApprovedTotalAmount.Value).Aggregate(0M, (a, b) => a + b)));

            Func<Guid, string, decimal> getCSDAmount = (budgetDetailGid, amountType) =>
            {
                decimal result = 0m;
                if (correctedCSDAmounts.Keys.Contains(budgetDetailGid))
                {
                    switch (amountType)
                    {
                        case "euAmount":
                            result += correctedCSDAmounts[budgetDetailGid].Item1;
                            break;
                        case "bgAmount":
                            result += correctedCSDAmounts[budgetDetailGid].Item2;
                            break;
                        case "selfAmount":
                            result += correctedCSDAmounts[budgetDetailGid].Item3;
                            break;
                        case "totalAmount":
                            result += correctedCSDAmounts[budgetDetailGid].Item4;
                            break;
                        default:
                            throw new Exception("amountType not supported");
                    }
                }

                if (csdAmounts.Keys.Contains(budgetDetailGid))
                {
                    switch (amountType)
                    {
                        case "euAmount":
                            result += csdAmounts[budgetDetailGid].Item1;
                            break;
                        case "bgAmount":
                            result += csdAmounts[budgetDetailGid].Item2;
                            break;
                        case "selfAmount":
                            result += csdAmounts[budgetDetailGid].Item3;
                            break;
                        case "totalAmount":
                            result += csdAmounts[budgetDetailGid].Item4;
                            break;
                        default:
                            throw new Exception("amountType not supported");
                    }
                }

                return result;
            };

            var contractBudgetTree = new ContractBudgetTreeVO()
            {
                ContractReportId = contractReportId,
                ContractId = contractId,
            };

            var contractBudgetLevel0 = new ContractBudgetLevel0TreeVO()
            {
                DisplayName = contractDoc.BFPContractBasicData.ProgrammeBasicData.Programme.Name,
                Level1Items = new List<ContractBudgetLevel1TreeVO>(),
            };

            decimal level0EuAmount, level0BgAmount, level0SelfAmount, level0TotalAmount;
            decimal level1EuAmount, level1BgAmount, level1SelfAmount, level1TotalAmount;
            decimal level2EuAmount, level2BgAmount, level2SelfAmount, level2TotalAmount;
            decimal level3EuAmount, level3BgAmount, level3SelfAmount, level3TotalAmount;

            level0EuAmount = level0BgAmount = level0SelfAmount = level0TotalAmount = 0m;

            foreach (var level1BudgetItem in contractBudget)
            {
                var contractBudgetLevel1 = new ContractBudgetLevel1TreeVO()
                {
                    DisplayName = level1BudgetItem.Name,
                    Level2Items = new List<ContractBudgetLevel2TreeVO>(),
                };

                level1EuAmount = level1BgAmount = level1SelfAmount = level1TotalAmount = 0m;

                foreach (var level2BudgetItem in level1BudgetItem.BFPContractProgrammeExpenseBudgetCollection)
                {
                    var contractBudgetLevel2 = new ContractBudgetLevel2TreeVO()
                    {
                        DisplayName = string.Format(
                            "{0} ( режим на финансиране: {1}, {2} )",
                            level2BudgetItem.Name,
                            level2BudgetItem.AidMode.Description,
                            "допустим"),
                        Level3Items = new List<ContractBudgetLevel3TreeVO>(),
                    };

                    level2EuAmount = level2BgAmount = level2SelfAmount = level2TotalAmount = 0m;

                    foreach (var level3BudgetItem in level2BudgetItem.BFPContractProgrammeDetailsExpenseBudgetCollection)
                    {
                        var contractBudgetLevel3 = new ContractBudgetLevel3TreeVO()
                        {
                            DisplayName = level3BudgetItem.Name,
                        };

                        var level3BudgetItemGid = Guid.Parse(level3BudgetItem.gid);
                        level3EuAmount = getCSDAmount(level3BudgetItemGid, "euAmount");
                        level3BgAmount = getCSDAmount(level3BudgetItemGid, "bgAmount");
                        level3SelfAmount = getCSDAmount(level3BudgetItemGid, "selfAmount");
                        level3TotalAmount = getCSDAmount(level3BudgetItemGid, "totalAmount");

                        contractBudgetLevel3.EuAmount = level3EuAmount;
                        contractBudgetLevel3.BgAmount = level3BgAmount;
                        contractBudgetLevel3.SelfAmount = level3SelfAmount;
                        contractBudgetLevel3.TotalAmount = level3TotalAmount;

                        level2EuAmount += level3EuAmount;
                        level2BgAmount += level3BgAmount;
                        level2SelfAmount += level3SelfAmount;
                        level2TotalAmount += level3TotalAmount;

                        contractBudgetLevel2.Level3Items.Add(contractBudgetLevel3);
                    }

                    contractBudgetLevel2.EuAmount = level2EuAmount;
                    contractBudgetLevel2.BgAmount = level2BgAmount;
                    contractBudgetLevel2.SelfAmount = level2SelfAmount;
                    contractBudgetLevel2.TotalAmount = level2TotalAmount;

                    level1EuAmount += level2EuAmount;
                    level1BgAmount += level2BgAmount;
                    level1SelfAmount += level2SelfAmount;
                    level1TotalAmount += level2TotalAmount;

                    contractBudgetLevel1.Level2Items.Add(contractBudgetLevel2);
                }

                contractBudgetLevel1.EuAmount = level1EuAmount;
                contractBudgetLevel1.BgAmount = level1BgAmount;
                contractBudgetLevel1.SelfAmount = level1SelfAmount;
                contractBudgetLevel1.TotalAmount = level1TotalAmount;

                level0EuAmount += level1EuAmount;
                level0BgAmount += level1BgAmount;
                level0SelfAmount += level1SelfAmount;
                level0TotalAmount += level1TotalAmount;

                contractBudgetLevel0.Level1Items.Add(contractBudgetLevel1);
            }

            contractBudgetLevel0.EuAmount = level0EuAmount;
            contractBudgetLevel0.BgAmount = level0BgAmount;
            contractBudgetLevel0.SelfAmount = level0SelfAmount;
            contractBudgetLevel0.TotalAmount = level0TotalAmount;

            contractBudgetTree.Programme = contractBudgetLevel0;

            return contractBudgetTree;
        }

        public IList<ContractReportVO> GetFinancialCorrectionContractReports(int financialCorrectionId)
        {
            var subquery1 = from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                            join crfcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on cr.ContractReportId equals crfcsd.ContractReportId
                            where crfcsd.FinancialCorrectionId == financialCorrectionId
                            select cr.ContractReportId;

            var subquery2 = from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                            join crafc in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>() on cr.ContractReportId equals crafc.ContractReportId
                            join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crafc.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                            join crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on crfc.ContractReportFinancialCorrectionId equals crfccsd.ContractReportFinancialCorrectionId
                            where crfccsd.FinancialCorrectionId == financialCorrectionId
                            select cr.ContractReportId;

            var subquery = subquery1.Concat(subquery2).Distinct();

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where subquery.Contains(cr.ContractReportId)
                    select new ContractReportVO
                    {
                        ContractReportId = cr.ContractReportId,
                        ContractId = cr.ContractId,
                        Gid = cr.Gid,
                        ReportType = cr.ReportType,
                        OrderNum = cr.OrderNum,
                        Status = cr.Status,
                        StatusNote = cr.StatusNote,
                        Source = cr.Source,
                        SubmitDate = cr.SubmitDate,
                        SubmitDeadline = cr.SubmitDeadline,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureId = p.ProcedureId,
                    })
                .ToList();
        }

        public IList<ContractReportVO> GetContractReportsForFinancial(
            int[] programmeIds,
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null,
            int? userId = null)
        {
            var predicate = PredicateBuilder.True<ContractReport>()
                .AndStringContains(t => t.OrderNum.ToString(), contractReportNum);

            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            var contractProgrammePredicate = contractPredicate
                .And(t => programmeIds.Contains(t.ProgrammeId));

            var procedurePredicate = PredicateBuilder.True<Procedure>()
                .AndEquals(t => t.ProcedureId, procedureId);

            var exeternalUserContracts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => userId.HasValue && x.UserId == userId.Value)
                                         join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on cu.ContractId equals c.ContractId
                                         select c;

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(predicate)
                    join crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>() on cr.ContractReportId equals crf.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractProgrammePredicate).Union(exeternalUserContracts) on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>().Where(procedurePredicate) on c.ProcedureId equals p.ProcedureId

                    where cr.Status == ContractReportStatus.Accepted && crf.Status == ContractReportFinancialStatus.Actual
                    orderby cr.CreateDate descending
                    select new ContractReportVO()
                    {
                        ContractReportId = cr.ContractReportId,
                        ContractId = cr.ContractId,
                        Gid = cr.Gid,
                        ReportType = cr.ReportType,
                        OrderNum = cr.OrderNum,
                        Status = cr.Status,
                        StatusNote = cr.StatusNote,
                        Source = cr.Source,
                        SubmitDate = cr.SubmitDate,
                        SubmitDeadline = cr.SubmitDeadline,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureId = p.ProcedureId,
                    })
                .ToList();
        }

        public IList<ContractReportVO> GetContractReportsForTechnical(
            int[] programmeIds,
            int userId,
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null)
        {
            var predicate = PredicateBuilder.True<ContractReport>()
                .AndStringContains(t => t.OrderNum.ToString(), contractReportNum);

            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            var procedurePredicate = PredicateBuilder.True<Procedure>()
                .AndEquals(t => t.ProcedureId, procedureId);

            var externalUserContracts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                        join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on cu.ContractId equals c.ContractId
                                        select c;

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(predicate)
                    join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>() on cr.ContractReportId equals crt.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate.And(t => programmeIds.Contains(t.ProgrammeId))).Union(externalUserContracts) on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>().Where(procedurePredicate) on c.ProcedureId equals p.ProcedureId

                    where cr.Status == ContractReportStatus.Accepted && crt.Status == ContractReportTechnicalStatus.Actual
                    orderby cr.CreateDate descending
                    select new ContractReportVO()
                    {
                        ContractReportId = cr.ContractReportId,
                        ContractId = cr.ContractId,
                        Gid = cr.Gid,
                        ReportType = cr.ReportType,
                        OrderNum = cr.OrderNum,
                        Status = cr.Status,
                        StatusNote = cr.StatusNote,
                        Source = cr.Source,
                        SubmitDate = cr.SubmitDate,
                        SubmitDeadline = cr.SubmitDeadline,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureId = p.ProcedureId,
                    })
                .Distinct()
                .ToList();
        }

        public IList<ContractReportRequestedAmountVO> GetContractReportRequestedAmountsForProjectDossier(int contractId)
        {
            var requestedAmountsLookup = (
                from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cr.ContractReportId equals crp.ContractReportId
                where crp.ContractId == contractId && ContractReport.FinalStatuses.Contains(cr.Status) && crp.Status == ContractReportPaymentStatus.Actual
                group crp
                by cr.ContractReportId into g
                select new
                {
                    ContractReportId = g.Key,
                    RequestedAmount = g.Sum(t => t.RequestedAmount),
                })
                .ToLookup(ra => ra.ContractReportId);

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where cr.ContractId == contractId && ContractReport.FinalStatuses.Contains(cr.Status)
                    orderby cr.CreateDate descending
                    select new
                    {
                        cr.ContractReportId,
                        cr.ContractId,
                        cr.ReportType,
                        cr.OrderNum,
                        cr.Status,
                        cr.Source,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                    })
                    .ToList()
                    .Select(ra => new ContractReportRequestedAmountVO
                    {
                        ContractReportId = ra.ContractReportId,
                        ContractId = ra.ContractId,
                        ReportType = ra.ReportType,
                        OrderNum = ra.OrderNum,
                        Status = ra.Status,
                        Source = ra.Source,
                        ProcedureName = ra.ProcedureName,
                        ContractName = ra.ContractName,
                        ContractRegNum = ra.ContractRegNum,
                        RequestedAmount = requestedAmountsLookup[ra.ContractReportId].Any() ? requestedAmountsLookup[ra.ContractReportId].First().RequestedAmount : null,
                    })
                    .ToList();
        }

        public IList<ContractReportApprovedAmountVO> GetContractReportApprovedAmountsForProjectDossier(int contractId)
        {
            var approvedTotalAmountsLookup = (
                from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                join crfcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on cr.ContractReportId equals crfcsd.ContractReportId into g0
                from crfcsd in g0.DefaultIfEmpty()

                join crapa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>() on cr.ContractReportId equals crapa.ContractReportId into g1
                from crapa in g1.DefaultIfEmpty()

                where cr.ContractId == contractId
                group new
                {
                    ApprovedTotalAmount = (crfcsd.ApprovedTotalAmount ?? 0) + (crapa.ApprovedBfpTotalAmount ?? 0),
                }
                by cr.ContractReportId into g
                select new
                {
                    ContractReportId = g.Key,
                    ApprovedTotalAmount = g.Sum(t => t.ApprovedTotalAmount),
                })
                .ToLookup(a => a.ContractReportId);

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where cr.ContractId == contractId && ContractReport.FinalStatuses.Contains(cr.Status)
                    orderby cr.CreateDate descending
                    select new
                    {
                        cr.ContractReportId,
                        cr.ContractId,
                        cr.ReportType,
                        cr.OrderNum,
                        cr.Status,
                        cr.Source,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                    })
                    .ToList()
                    .Select(a => new ContractReportApprovedAmountVO
                    {
                        ContractReportId = a.ContractReportId,
                        ContractId = a.ContractId,
                        ReportType = a.ReportType,
                        OrderNum = a.OrderNum,
                        Status = a.Status,
                        Source = a.Source,
                        ProcedureName = a.ProcedureName,
                        ContractName = a.ContractName,
                        ContractRegNum = a.ContractRegNum,
                        ApprovedAmount = approvedTotalAmountsLookup[a.ContractReportId].Any() ? approvedTotalAmountsLookup[a.ContractReportId].First().ApprovedTotalAmount : 0,
                    })
                    .ToList();
        }

        public IList<ContractReportCertifiedAmountVO> GetContractReportCertifiedAmountsForProjectDossier(int contractId)
        {
            var approvedCertReportIds = (
                from cr in this.unitOfWork.DbContext.Set<CertReport>()
                where cr.Status == CertReportStatus.Approved
                select cr.CertReportId)
                .ToHashSet();

            var certifiedApprovedTotalAmountsLookup = (
                from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                join crfcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(csd => approvedCertReportIds.Contains(csd.CertReportId.Value)) on cr.ContractReportId equals crfcsd.ContractReportId into g0
                from crfcsd in g0.DefaultIfEmpty()

                join crapa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>().Where(csd => approvedCertReportIds.Contains(csd.CertReportId.Value)) on cr.ContractReportId equals crapa.ContractReportId into g1
                from crapa in g1.DefaultIfEmpty()

                where cr.ContractId == contractId
                group new
                {
                    CertifiedApprovedTotalAmount = (crfcsd.CertifiedApprovedTotalAmount ?? 0) + (crapa.CertifiedApprovedBfpTotalAmount ?? 0),
                }
                by cr.ContractReportId into g
                select new
                {
                    ContractReportId = g.Key,
                    CertifiedApprovedTotalAmount = g.Sum(t => t.CertifiedApprovedTotalAmount),
                })
                .ToLookup(ca => ca.ContractReportId);

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where cr.ContractId == contractId && ContractReport.FinalStatuses.Contains(cr.Status)
                    orderby cr.CreateDate descending
                    select new
                    {
                        cr.ContractReportId,
                        cr.ContractId,
                        cr.ReportType,
                        cr.OrderNum,
                        cr.Status,
                        cr.Source,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                    })
                    .ToList()
                    .Select(ca => new ContractReportCertifiedAmountVO
                    {
                        ContractReportId = ca.ContractReportId,
                        ContractId = ca.ContractId,
                        ReportType = ca.ReportType,
                        OrderNum = ca.OrderNum,
                        Status = ca.Status,
                        Source = ca.Source,
                        ProcedureName = ca.ProcedureName,
                        ContractName = ca.ContractName,
                        ContractRegNum = ca.ContractRegNum,
                        CertifiedApprovedAmount = certifiedApprovedTotalAmountsLookup[ca.ContractReportId].Any() ? certifiedApprovedTotalAmountsLookup[ca.ContractReportId].First().CertifiedApprovedTotalAmount : 0,
                    })
                    .ToList();
        }

        public IList<ContractReportVO> GetContractReportWithTechnicalForProjectDossier(int contractId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>() on cr.ContractReportId equals crt.ContractReportId

                    where cr.ContractId == contractId && ContractReport.FinalStatuses.Contains(cr.Status)
                    orderby cr.CreateDate descending
                    select new ContractReportVO
                    {
                        ContractReportId = cr.ContractReportId,
                        ContractId = cr.ContractId,
                        Gid = cr.Gid,
                        ReportType = cr.ReportType,
                        OrderNum = cr.OrderNum,
                        Status = cr.Status,
                        StatusNote = cr.StatusNote,
                        Source = cr.Source,
                        SubmitDate = cr.SubmitDate,
                        SubmitDeadline = cr.SubmitDeadline,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureId = p.ProcedureId,
                    }).ToList();
        }

        public IList<string> CanReturnContractReportStatusToUnchecked(int contractReportId)
        {
            var errors = new List<string>();
            var contractReport = this.unitOfWork.DbContext.Set<ContractReport>().Where(t => t.ContractReportId == contractReportId).Single();

            bool isIncludedInCertReport = this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                .Where(t => t.ContractReportId == contractReportId && t.CertReportId.HasValue)
                .Any();

            isIncludedInCertReport = isIncludedInCertReport || this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>()
                .Where(t => t.ContractReportId == contractReportId && t.CertReportId.HasValue)
                .Any();

            if (isIncludedInCertReport)
            {
                errors.Add("Не можете да промените статуса на пакета на 'В проверка', защото той е включен в доклад по сертификация");
            }

            if (this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>().Where(t => t.ContractReportId == contractReportId).Any())
            {
                errors.Add("Не можете да промените статуса на пакета на 'В проверка', защото към него има асоциирани корекции на верифицирани суми на ниво РОД");
            }

            if (this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidation>().Where(t => t.ContractReportId == contractReportId).Any())
            {
                errors.Add("Не можете да промените статуса на пакета на 'В проверка', защото към него има асоциирани препотвърждавания на верифицирани суми на ниво РОД");
            }

            if (this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrection>().Where(t => t.ContractReportId == contractReportId).Any())
            {
                errors.Add("Не можете да промените статуса на пакета на 'В проверка', защото към него има асоциирани изравнявания на сертифицирани суми на ниво РОД");
            }

            var actualPayment = this.unitOfWork.DbContext.Set<ContractReportPayment>()
                .Where(p => p.ContractReportId == contractReportId && p.Status == ContractReportPaymentStatus.Actual)
                .SingleOrDefault();

            if (actualPayment != null)
            {
                if (this.unitOfWork.DbContext.Set<ContractReportCorrection>().Where(t => t.ContractReportPaymentId == actualPayment.ContractReportPaymentId && t.Status != ContractReportCorrectionStatus.Deleted).Any())
                {
                    errors.Add("Не можете да промените статуса на пакета на 'В проверка', защото към него има асоциирани неанулирани корекции на верифицирани суми на други нива(ИП)");
                }

                if (this.unitOfWork.DbContext.Set<ContractReportRevalidation>().Where(t => t.ContractReportPaymentId == actualPayment.ContractReportPaymentId && t.Status != ContractReportRevalidationStatus.Deleted).Any())
                {
                    errors.Add("Не можете да промените статуса на пакета на 'В проверка', защото към него има асоциирани неанулирани препотвърждавания на верифицирани суми на други нива(ИП)");
                }

                if (this.unitOfWork.DbContext.Set<ContractReportCertCorrection>().Where(t => t.ContractReportPaymentId == actualPayment.ContractReportPaymentId && t.Status != ContractReportCertCorrectionStatus.Deleted).Any())
                {
                    errors.Add("Не можете да промените статуса на пакета на 'В проверка', защото към него има асоциирани неанулирани изравнявания на сертифицирани суми на други нива(ИП)");
                }

                if (this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(t => t.ContractReportPaymentId == actualPayment.ContractReportPaymentId && t.Status != ActuallyPaidAmountStatus.Deleted).Any())
                {
                    errors.Add("Не можете да промените статуса на пакета на 'В проверка', защото към него има асоциирани неанулирани реално изплатени суми");
                }
            }

            var acceptedOrRefusedReports = this.unitOfWork.DbContext.Set<ContractReport>().Where(t => t.ContractId == contractReport.ContractId && ContractReport.FinalStatuses.Contains(t.Status)).ToList();

            if (acceptedOrRefusedReports.OrderBy(t => t.OrderNum).Last().OrderNum != contractReport.OrderNum)
            {
                errors.Add("Можете да промените статуса на пакета на 'В проверка' само на последния Приет/Отхвърлен пакет към този договор");
            }

            var nonAcceptedNorRefused = this.unitOfWork.DbContext.Set<ContractReport>().Where(t => t.ContractId == contractReport.ContractId && !ContractReport.FinalStatuses.Contains(t.Status)).ToList();
            if (nonAcceptedNorRefused.Count > 1)
            {
                errors.Add("Не можете да промените статуса на пакета на 'В проверка', защото съществуват повече от един пакет със статус различен от 'Приет' или 'Отхвърлен'");
            }
            else if (nonAcceptedNorRefused.Count == 1)
            {
                var report = nonAcceptedNorRefused.Single();

                var finances = this.unitOfWork.DbContext.Set<ContractReportFinancial>().Where(t => t.ContractReportId == report.ContractReportId).ToList();
                var payments = this.unitOfWork.DbContext.Set<ContractReportPayment>().Where(t => t.ContractReportId == report.ContractReportId).ToList();
                var technicals = this.unitOfWork.DbContext.Set<ContractReportTechnical>().Where(t => t.ContractReportId == report.ContractReportId).ToList();
                var micros = this.unitOfWork.DbContext.Set<ContractReportMicro>().Where(t => t.ContractReportId == report.ContractReportId).ToList();

                if (report.Status != ContractReportStatus.Draft ||
                    finances.Any(t => t.Status != ContractReportFinancialStatus.Draft) ||
                    payments.Any(t => t.Status != ContractReportPaymentStatus.Draft) ||
                    technicals.Any(t => t.Status != ContractReportTechnicalStatus.Draft) ||
                    micros.Any(t => t.Status != ContractReportMicroStatus.Draft))
                {
                    errors.Add("Не можете да промените статуса на пакета на 'В проверка', защото съществува пакет, който не е в 'Чернова' или съдържа документи, които не са в статус 'Чернова'");
                }
            }

            return errors;
        }

        public IList<ContractReportSAPDataVO> GetContractReportSAPData(int contractReportId)
        {
            return (from crpca in this.unitOfWork.DbContext.Set<ContractReportPaymentCheckAmount>()
                    join crpc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>() on crpca.ContractReportPaymentCheckId equals crpc.ContractReportPaymentCheckId
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crpca.ContractReportPaymentId equals crp.ContractReportPaymentId
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crpca.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crpca.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on c.ProgrammeId equals pr.MapNodeId
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on crpca.ProgrammePriorityId equals pp.MapNodeId

                    where crpca.ContractReportId == contractReportId && crp.Status == ContractReportPaymentStatus.Actual && crpc.Status == ContractReportPaymentCheckStatus.Active
                    select new ContractReportSAPDataVO
                    {
                        ContractReportId = cr.ContractReportId,
                        ContractId = cr.ContractId,
                        OrderNum = cr.OrderNum,
                        ProgrammeCode = pr.Code,
                        ProgrammePriorityId = pp.MapNodeId,
                        ProgrammePriorityCode = pp.Code,
                        ProgrammePriorityName = pp.Name,
                        ProcedureCode = p.Code,
                        ContractCode = c.RegNumber,
                        PaymentOrderNum = crp.VersionNum,

                        CompanyUin = c.CompanyUin,
                        CompanyUinType = c.CompanyUinType,
                        CompanyName = c.CompanyName,

                        PaidEuAmount = crpca.PaidEuAmount,
                        PaidBgAmount = crpca.PaidBgAmount,
                        PaidCrossAmount = crpca.PaidCrossAmount,
                        PaidBfpTotalAmount = crpca.PaidBfpTotalAmount,

                        ApprovedEuAmount = crpca.ApprovedEuAmount,
                        ApprovedBgAmount = crpca.ApprovedBgAmount,
                        ApprovedCrossAmount = crpca.ApprovedCrossAmount,
                        ApprovedBfpTotalAmount = crpca.ApprovedBfpTotalAmount,

                        Currency = Domain.Core.Currency.Lev,
                        SubmitDate = cr.SubmitDate,
                        CheckedDate = cr.CheckedDate,
                        CurrentDate = DateTime.Now,

                        ReportType = cr.ReportType,
                        PaymentType = crp.PaymentType.Value,
                    })
                    .ToList();
        }

        public bool HasReturnedContractReportDocuments(int contractReportId)
        {
            var technicals = this.unitOfWork.DbContext.Set<ContractReportTechnical>()
                .Where(t => t.ContractReportId == contractReportId)
                .ToList();

            var financials = this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                .Where(t => t.ContractReportId == contractReportId)
                .ToList();

            var payments = this.unitOfWork.DbContext.Set<ContractReportPayment>()
                .Where(t => t.ContractReportId == contractReportId)
                .ToList();

            var micros = this.unitOfWork.DbContext.Set<ContractReportMicro>()
                .Where(t => t.ContractReportId == contractReportId)
                .ToList();

            return this.HasReturnedContractReportDocuments(technicals, financials, payments, micros);
        }

        public async Task<bool> HasReturnedContractReportDocumentsAsync(int contractReportId, CancellationToken ct)
        {
            var technicals = await this.unitOfWork.DbContext.Set<ContractReportTechnical>()
                .Where(t => t.ContractReportId == contractReportId)
                .ToListAsync(ct);

            var financials = await this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                .Where(t => t.ContractReportId == contractReportId)
                .ToListAsync(ct);

            var payments = await this.unitOfWork.DbContext.Set<ContractReportPayment>()
                .Where(t => t.ContractReportId == contractReportId)
                .ToListAsync(ct);

            var micros = await this.unitOfWork.DbContext.Set<ContractReportMicro>()
                .Where(t => t.ContractReportId == contractReportId)
                .ToListAsync(ct);

            return this.HasReturnedContractReportDocuments(technicals, financials, payments, micros);
        }

        private bool HasReturnedContractReportDocuments(
            IEnumerable<ContractReportTechnical> technicals,
            IEnumerable<ContractReportFinancial> financials,
            IEnumerable<ContractReportPayment> payments,
            IEnumerable<ContractReportMicro> micros)
        {
            technicals = technicals.OrderByDescending(t => t.VersionSubNum);
            if (technicals.ElementAtOrDefault(0)?.Status == ContractReportTechnicalStatus.Draft &&
                technicals.ElementAtOrDefault(1)?.Status == ContractReportTechnicalStatus.Returned)
            {
                return true;
            }

            financials = financials.OrderByDescending(t => t.VersionSubNum);
            if (financials.ElementAtOrDefault(0)?.Status == ContractReportFinancialStatus.Draft &&
                financials.ElementAtOrDefault(1)?.Status == ContractReportFinancialStatus.Returned)
            {
                return true;
            }

            payments = payments.OrderByDescending(t => t.VersionSubNum);
            if (payments.ElementAtOrDefault(0)?.Status == ContractReportPaymentStatus.Draft &&
                payments.ElementAtOrDefault(1)?.Status == ContractReportPaymentStatus.Returned)
            {
                return true;
            }

            var microsByType = micros
                .GroupBy(t => t.Type)
                .Select(g => g.OrderByDescending(t => t.VersionSubNum));

            if (microsByType.Any(g =>
                g.ElementAtOrDefault(0)?.Status == ContractReportMicroStatus.Draft &&
                g.ElementAtOrDefault(1)?.Status == ContractReportMicroStatus.Returned))
            {
                return true;
            }

            return false;
        }

        public bool ContractReportHasFinancial(int contractReportId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                .Where(x => x.ContractReportId == contractReportId)
                .Any();
        }

        public async Task<bool> ContractReportHasFinancialAsync(int contractReportId, CancellationToken ct)
        {
            return await this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                .Where(x => x.ContractReportId == contractReportId)
                .AnyAsync(ct);
        }

        public bool ContractReportHasTechnical(int contractReportId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportTechnical>()
                .Where(x => x.ContractReportId == contractReportId)
                .Any();
        }

        public async Task<bool> ContractReportHasTechnicalAsync(int contractReportId, CancellationToken ct)
        {
            return await this.unitOfWork.DbContext.Set<ContractReportTechnical>()
                .Where(x => x.ContractReportId == contractReportId)
                .AnyAsync(ct);
        }

        public bool ContractReportHasPayment(int contractReportId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportPayment>()
                .Where(x => x.ContractReportId == contractReportId &&
                    (x.PaymentType == ContractReportPaymentType.Final ||
                    x.PaymentType == ContractReportPaymentType.Intermediate))
                .Any();
        }

        public async Task<bool> ContractReportHasPaymentAsync(int contractReportId, CancellationToken ct)
        {
            return await this.unitOfWork.DbContext.Set<ContractReportPayment>()
                .Where(x => x.ContractReportId == contractReportId &&
                (x.PaymentType == ContractReportPaymentType.Final || x.PaymentType == ContractReportPaymentType.Intermediate))
                .AnyAsync(ct);
        }

        public bool ContractReportHasAdvancePayment(int contractReportId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportPayment>()
                .Where(x => x.ContractReportId == contractReportId &&
                    (x.PaymentType == ContractReportPaymentType.Advance ||
                    x.PaymentType == ContractReportPaymentType.AdvanceVerification))
                .Any();
        }

        public async Task<bool> ContractReportHasAdvancePaymentAsync(int contractReportId, CancellationToken ct)
        {
            return await this.unitOfWork.DbContext.Set<ContractReportPayment>()
                .Where(x => x.ContractReportId == contractReportId &&
                    (x.PaymentType == ContractReportPaymentType.Advance ||
                    x.PaymentType == ContractReportPaymentType.AdvanceVerification))
                .AnyAsync(ct);
        }

        public List<ContractReport> GetPreviousContractReport(int contractReportId)
        {
            var contractReport = this.Find(contractReportId);

            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    where cr.ContractId == contractReport.ContractId && cr.OrderNum < contractReport.OrderNum
                    orderby cr.OrderNum descending
                    select cr)
                .ToList();
        }

        public int GetContractReportProcedureId(int contractReportId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                    where cr.ContractReportId == contractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    select c.ProcedureId)
                .Single();
        }

        public async Task<int> GetContractReportProcedureIdAsync(int contractReportId, CancellationToken ct)
        {
            var result = (from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                          where cr.ContractReportId == contractReportId
                          join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                          select c.ProcedureId)
                          .SingleAsync(ct);

            return await result;
        }
    }
}
