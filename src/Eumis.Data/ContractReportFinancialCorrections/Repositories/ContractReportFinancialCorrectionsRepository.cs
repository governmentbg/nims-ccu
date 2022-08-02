using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain.CertReports;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReportFinancialCorrections.Repositories
{
    internal class ContractReportFinancialCorrectionsRepository : AggregateRepository<ContractReportFinancialCorrection>, IContractReportFinancialCorrectionsRepository
    {
        public ContractReportFinancialCorrectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportFinancialCorrection, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportFinancialCorrection, object>>[]
                {
                    c => c.File,
                };
            }
        }

        public int GetNextOrderNum(int contractId)
        {
            var lastOrderNumber = this.Set()
                .Where(t => t.ContractId == contractId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public int GetContractReportId(int contractReportFinancialCorrectionId)
        {
            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>()
                    where crfc.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId
                    select crfc.ContractReportId).Single();
        }

        public IList<ContractReportFinancialCorrectionVO> GetContractReportFinancialCorrections(
            int[] programmeIds,
            int userId,
            string contractRegNum = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var predicate = PredicateBuilder.True<ContractReportFinancialCorrection>()
                .AndDateTimeGreaterThanOrEqual(t => t.CreateDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.CreateDate, toDate);

            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            var contractProgrammePredicate = contractPredicate.And(x => programmeIds.Contains(x.ProgrammeId));

            var correctedCsds = from crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                                group new
                                {
                                    CorrectedApprovedBfpTotalAmount = (crfccsd.Sign.HasValue ? (int)crfccsd.Sign : 1) * crfccsd.CorrectedApprovedBfpTotalAmount,
                                    CorrectedApprovedSelfAmount = (crfccsd.Sign.HasValue ? (int)crfccsd.Sign : 1) * crfccsd.CorrectedApprovedSelfAmount,
                                }
                               by crfccsd.ContractReportFinancialCorrectionId
                                 into g
                                select new
                                {
                                    ContractReportFinancialCorrectionId = g.Key,
                                    CorrectedApprovedBfpTotalAmount = g.Sum(t => t.CorrectedApprovedBfpTotalAmount),
                                    CorrectedApprovedSelfAmount = g.Sum(t => t.CorrectedApprovedSelfAmount),
                                };

            var externalUserFinancialCorrections = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                   join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on cu.ContractId equals c.ContractId
                                                   select c;

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>().Where(predicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractProgrammePredicate).Union(externalUserFinancialCorrections) on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join ccsd in correctedCsds on crfc.ContractReportFinancialCorrectionId equals ccsd.ContractReportFinancialCorrectionId into g0
                    from ccsd in g0.DefaultIfEmpty()

                    select new { crfc, cr, c, p, ccsd })
                .Distinct()
                .ToList()
                .Select(t => new ContractReportFinancialCorrectionVO()
                {
                    ContractReportFinancialCorrectionId = t.crfc.ContractReportFinancialCorrectionId,
                    ContractReportId = t.crfc.ContractReportId,
                    ContractId = t.crfc.ContractId,
                    OrderNum = t.crfc.OrderNum,
                    Status = t.crfc.Status,
                    Notes = t.crfc.Notes,
                    CreateDate = t.crfc.CreateDate,
                    ContractName = t.c.Name,
                    ContractRegNum = t.c.RegNumber,
                    ProcedureName = t.p.Name,
                    ReportOrderNum = t.cr.OrderNum,
                    CorrectionDate = t.crfc.CorrectionDate,
                    CorrectedApprovedBfpTotalAmount = t.ccsd != null ? t.ccsd.CorrectedApprovedBfpTotalAmount : null,
                    CorrectedApprovedSelfAmount = t.ccsd != null ? t.ccsd.CorrectedApprovedSelfAmount : null,
                })
                .ToList();
        }

        public bool CanCreate(int contractReportId)
        {
            return !(from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>()
                     where crfc.ContractReportId == contractReportId && crfc.Status == ContractReportFinancialCorrectionStatus.Draft
                     select crfc.ContractReportFinancialCorrectionId).Any();
        }

        public IList<ContractReportFinancialCorrectionVO> GetFinancialCorrectionContractReportFinancialCorrections(int financialCorrectionId)
        {
            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    join crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on crfc.ContractReportFinancialCorrectionId equals crfccsd.ContractReportFinancialCorrectionId

                    join cratfc in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>() on crfc.ContractReportFinancialCorrectionId equals cratfc.ContractReportFinancialCorrectionId into g1
                    from cratfc in g1.DefaultIfEmpty()

                    where crfccsd.FinancialCorrectionId == financialCorrectionId && cratfc == null
                    select new ContractReportFinancialCorrectionVO()
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

        public bool IsIncludedInCertReport(int contractReportFinancialCorrectionId)
        {
            return (from crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                    where crfccsd.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId &&
                          crfccsd.CertReportId != null
                    select crfccsd.ContractReportFinancialCorrectionCSDId)
                .Any();
        }

        public IList<ContractReportFinancialCorrectionVO> GetContractReportFinancialCorrectionsForProjectDossier(int contractId)
        {
            var correctedCsds = from crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                                group new
                                {
                                    CorrectedApprovedBfpTotalAmount = (crfccsd.Sign.HasValue ? (int)crfccsd.Sign : 1) * crfccsd.CorrectedApprovedBfpTotalAmount,
                                    CorrectedApprovedSelfAmount = (crfccsd.Sign.HasValue ? (int)crfccsd.Sign : 1) * crfccsd.CorrectedApprovedSelfAmount,
                                }
                                by crfccsd.ContractReportFinancialCorrectionId into g
                                select new
                                {
                                    ContractReportFinancialCorrectionId = g.Key,
                                    CorrectedApprovedBfpTotalAmount = g.Sum(t => t.CorrectedApprovedBfpTotalAmount),
                                    CorrectedApprovedSelfAmount = g.Sum(t => t.CorrectedApprovedSelfAmount),
                                };

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join ccsd in correctedCsds on crfc.ContractReportFinancialCorrectionId equals ccsd.ContractReportFinancialCorrectionId into g0
                    from ccsd in g0.DefaultIfEmpty()

                    where crfc.ContractId == contractId && crfc.Status != ContractReportFinancialCorrectionStatus.Draft
                    select new ContractReportFinancialCorrectionVO()
                    {
                        ContractReportFinancialCorrectionId = crfc.ContractReportFinancialCorrectionId,
                        ContractReportId = crfc.ContractReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        CorrectedApprovedBfpTotalAmount = ccsd.CorrectedApprovedBfpTotalAmount,
                        CorrectedApprovedSelfAmount = ccsd.CorrectedApprovedSelfAmount,
                        Notes = crfc.Notes,
                        CreateDate = crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                    })
                .ToList();
        }

        public IList<ContractReportCertifiedAmountFinancialCorrectionVO> GetContractReportCertifiedAmountFinancialCorrectionsForProjectDossier(int contractId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                    join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on fccsd.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                    join contR in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals contR.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join certR in this.unitOfWork.DbContext.Set<CertReport>() on fccsd.CertReportId equals certR.CertReportId
                    where crfc.ContractId == contractId && crfc.Status != ContractReportFinancialCorrectionStatus.Draft && (certR.Status == CertReportStatus.Approved || certR.Status == CertReportStatus.PartialyApproved)
                    group new
                    {
                        CertifiedCorrectedApprovedBfpTotalAmount = (int)fccsd.Sign * fccsd.CertifiedCorrectedApprovedBfpTotalAmount,
                        CertifiedCorrectedApprovedSelfAmount = (int)fccsd.Sign * fccsd.CertifiedCorrectedApprovedSelfAmount,
                    }
                    by new
                    {
                        ContractReportFinancialCorrectionId = crfc.ContractReportFinancialCorrectionId,
                        ContractReportId = crfc.ContractReportId,
                        ContractId = crfc.ContractId,
                        ContractRegNum = c.RegNumber,
                        ReportOrderNum = contR.OrderNum,
                        OrderNum = crfc.OrderNum,
                        CertReportNumber = certR.CertReportNumber,
                        Notes = crfc.Notes,
                    }
                    into g
                    select new ContractReportCertifiedAmountFinancialCorrectionVO
                    {
                        ContractReportFinancialCorrectionId = g.Key.ContractReportFinancialCorrectionId,
                        ContractReportId = g.Key.ContractReportId,
                        ContractId = g.Key.ContractId,
                        ContractRegNum = g.Key.ContractRegNum,
                        ReportOrderNum = g.Key.ReportOrderNum,
                        OrderNum = g.Key.OrderNum,
                        CertifiedCorrectedApprovedBfpTotalAmount = g.Sum(csd => csd.CertifiedCorrectedApprovedBfpTotalAmount),
                        CertifiedCorrectedApprovedSelfAmount = g.Sum(csd => csd.CertifiedCorrectedApprovedSelfAmount),
                        CertReportNumber = g.Key.CertReportNumber,
                        Notes = g.Key.Notes,
                    })
                    .ToList();
        }
    }
}
