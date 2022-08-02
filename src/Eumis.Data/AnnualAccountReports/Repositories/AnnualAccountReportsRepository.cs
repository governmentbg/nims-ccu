using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.AnnualAccountReports.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.AnnualAccountReports.ViewObjects;
using Eumis.Domain.CertReports;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.AnnualAccountReports.Repositories
{
    internal class AnnualAccountReportsRepository : AggregateRepository<AnnualAccountReport>, IAnnualAccountReportsRepository
    {
        public AnnualAccountReportsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<AnnualAccountReport, object>>[] Includes
        {
            get
            {
                return new Expression<Func<AnnualAccountReport, object>>[]
                {
                    c => c.CertificationDocuments.Select(t => t.File),
                    c => c.AuditDocuments.Select(t => t.File),
                    c => c.CertReports,
                    c => c.Corrections,
                    c => c.FinancialCorrectionCSDs,
                    c => c.CertifiedCorrections,
                    c => c.CertifiedFinancialCorrectionCSDs,
                    c => c.CertifiedRevalidationCorrections,
                    c => c.CertifiedRevalidationFinancialCorrectionCSDs,
                    c => c.Appendices,
                };
            }
        }

        public IList<AnnualAccountReportVO> GetAnnualAccountReports(int[] programmeIds)
        {
            var certifiedAmounts = this.GetAllCertifiedAmounts();

            return (from aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on aar.ProgrammeId equals p.MapNodeId
                    join ca in certifiedAmounts on aar.AnnualAccountReportId equals ca.Id into g1
                    from ca in g1.DefaultIfEmpty()

                    where programmeIds.Contains(aar.ProgrammeId)
                    orderby aar.ProgrammeId, aar.OrderNum, aar.OrderVersionNum descending
                    select new AnnualAccountReportVO
                    {
                        AnnualAccountReportId = aar.AnnualAccountReportId,
                        ProgrammeId = aar.ProgrammeId,
                        ProgrammeName = p.Name,
                        OrderNum = aar.OrderNum,
                        OrderVersionNum = aar.OrderVersionNum,
                        RegDate = aar.RegDate,
                        AccountPeriod = aar.AccountPeriod,
                        StatusDesc = aar.Status,
                        Status = aar.Status,
                        CreateDate = aar.CreateDate,
                        ApprovalDate = aar.ApprovalDate,
                        CertifiedBfpTotalAmount = ca == null ? 0m : ca.CertifiedBfpTotalAmount ?? 0m,
                        CertifiedSelfAmount = ca == null ? 0m : ca.CertifiedSelfAmount ?? 0m,
                    })
                .ToList();
        }

        public AnnualAccountReportInfoVO GetInfo(int annualAccountReportId)
        {
            return (from aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on aar.ProgrammeId equals pr.MapNodeId
                    where aar.AnnualAccountReportId == annualAccountReportId
                    select new AnnualAccountReportInfoVO
                    {
                        OrderNum = aar.OrderNum,
                        OrderVersionNum = aar.OrderVersionNum,
                        ProgrammeShortName = pr.ShortName,
                        StatusDescription = aar.Status,
                        Status = aar.Status,
                        AccountPeriod = aar.AccountPeriod,
                        Version = aar.Version,
                    }).Single();
        }

        public int GetProgrammeId(int annualAccountReportId)
        {
            return (from aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>()
                    where aar.AnnualAccountReportId == annualAccountReportId
                    select aar.ProgrammeId).Single();
        }

        public int GetNextOrderNum(int programmeId)
        {
            var lastOrderNumber = this.unitOfWork.DbContext.Set<AnnualAccountReport>()
                .Where(t => t.ProgrammeId == programmeId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public IList<AnnualAccountReportCertificationDocumentVO> GetAnnualAccountReportCertificationDocuments(int annualAccountReportId)
        {
            return (from aar in this.unitOfWork.DbContext.Set<AnnualAccountReportCertificationDocument>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on aar.BlobKey equals b.Key into g1
                    from b in g1.DefaultIfEmpty()
                    where aar.AnnualAccountReportId == annualAccountReportId
                    select new AnnualAccountReportCertificationDocumentVO
                    {
                        AnnualAccountReportId = aar.AnnualAccountReportId,
                        AnnualAccountReportCertificationDocumentId = aar.AnnualAccountReportCertificationDocumentId,
                        Name = aar.Name,
                        Description = aar.Description,
                        File = (b.Key == null) ? null : new FileVO
                        {
                            Key = b.Key,
                            Name = b.FileName,
                        },
                    }).ToList();
        }

        public IList<AnnualAccountReportAuditDocumentVO> GetAnnualAccountReportAuditDocuments(int annualAccountReportId)
        {
            return (from aar in this.unitOfWork.DbContext.Set<AnnualAccountReportAuditDocument>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on aar.BlobKey equals b.Key into g1
                    from b in g1.DefaultIfEmpty()
                    where aar.AnnualAccountReportId == annualAccountReportId
                    select new AnnualAccountReportAuditDocumentVO
                    {
                        AnnualAccountReportId = aar.AnnualAccountReportId,
                        AnnualAccountReportAuditDocumentId = aar.AnnualAccountReportAuditDocumentId,
                        Name = aar.Name,
                        Description = aar.Description,
                        File = (b.Key == null) ? null : new FileVO
                        {
                            Key = b.Key,
                            Name = b.FileName,
                        },
                    }).ToList();
        }

        public IList<CertReportVO> GetCertReportsForAnnualAccountReportAttachedCertReports(int annualAccountReportId)
        {
            var programmeId = this.GetProgrammeId(annualAccountReportId);

            var subquery = (from cracr in this.unitOfWork.DbContext.Set<AnnualAccountReportCertReport>()
                            where cracr.AnnualAccountReportId == annualAccountReportId
                            select cracr.CertReportId)
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

        public IList<CertReportVO> GetAnnualAccountReportAttachedCertReports(int annualAccountReportId)
        {
            return (from cracr in this.unitOfWork.DbContext.Set<AnnualAccountReportCertReport>()
                    join cr in this.unitOfWork.DbContext.Set<CertReport>() on cracr.CertReportId equals cr.CertReportId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on cr.ProgrammeId equals p.MapNodeId
                    where cracr.AnnualAccountReportId == annualAccountReportId
                    select new CertReportVO
                    {
                        CertReportId = cr.CertReportId,
                        AttachedCertReportId = cracr.CertReportId,
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

        public IList<ContractReportFinancialCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportFinancialCorrections(int annualAccountReportId)
        {
            var programmeId = this.GetProgrammeId(annualAccountReportId);

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

            var attachedFinancialCorrections = this.unitOfWork.DbContext.Set<AnnualAccountReportFinancialCorrectionCSD>()
                .Where(x => x.AnnualAccountReport.Status != AnnualAccountReportStatus.Opened)
                .Select(x => x.ContractReportFinancialCorrectionCSDId);

            return (from crfccds in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                        .Where(x => !attachedFinancialCorrections.Contains(x.ContractReportFinancialCorrectionCSDId) && x.Status == ContractReportFinancialCorrectionCSDStatus.Ended && x.CertReportId == null)
                    join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crfccds.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(x => x.ProgrammeId == programmeId) on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join ccsd in correctedCsds on crfc.ContractReportFinancialCorrectionId equals ccsd.ContractReportFinancialCorrectionId into g0
                    from ccsd in g0.DefaultIfEmpty()
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
                        CorrectionDate = crfc.CorrectionDate,
                        CorrectedApprovedBfpTotalAmount = ccsd != null ? ccsd.CorrectedApprovedBfpTotalAmount : null,
                        CorrectedApprovedSelfAmount = ccsd != null ? ccsd.CorrectedApprovedSelfAmount : null,
                    }).Distinct()
                    .ToList();
        }

        public IList<AnnualAccountReportFinancialCorrectionVO> GetAnnualAccountReportFinancialCorrections(int annualAccountReportId)
        {
            var financialCorrectionCSDs = this.GetAnnualAccountReportFinancialCorrectionCSDs().Where(t => t.AnnualAccountReportId == annualAccountReportId);
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
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    join csd in csds on crfc.ContractReportFinancialCorrectionId equals csd.ContractReportFinancialCorrectionId

                    select new AnnualAccountReportFinancialCorrectionVO
                    {
                        ContractReportFinancialCorrectionId = crfc.ContractReportFinancialCorrectionId,
                        AnnualAccountReportId = annualAccountReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        Notes = crfc.Notes,
                        CreateDate = crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                        ApprovedBfpTotalAmount = csd.ApprovedBfpTotalAmount,
                        ApprovedSelfAmount = csd.ApprovedSelfAmount,
                        CertifiedBfpTotalAmount = csd.CertifiedBfpTotalAmount,
                        CertifiedSelfAmount = csd.CertifiedSelfAmount,
                    })
                .ToList();
        }

        private IQueryable<AnnualAccountReportApprovedCertifiedAmountVO> GetAnnualAccountReportFinancialCorrectionCSDs()
        {
            return from aarcsd in this.unitOfWork.DbContext.Set<AnnualAccountReportFinancialCorrectionCSD>()
                   join fc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>() on aarcsd.ContractReportFinancialCorrectionCSDId equals fc.ContractReportFinancialCorrectionCSDId
                   join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fc.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Contract>() on fc.ContractId equals c.ContractId
                   where crp.Status == ContractReportPaymentStatus.Actual && fc.Status == ContractReportFinancialCorrectionCSDStatus.Ended
                   select new AnnualAccountReportApprovedCertifiedAmountVO
                   {
                       Id = fc.ContractReportFinancialCorrectionCSDId,
                       ContractId = (int?)fc.ContractId,
                       ContractType = c.ContractType,
                       AnnualAccountReportId = aarcsd.AnnualAccountReportId,
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
                       CertifiedEuAmount = (int)fc.Sign * fc.CertifiedCorrectedApprovedEuAmount,
                       CertifiedBgAmount = (int)fc.Sign * fc.CertifiedCorrectedApprovedBgAmount,
                       CertifiedBfpTotalAmount = (int)fc.Sign * fc.CertifiedCorrectedApprovedBfpTotalAmount,
                       CertifiedSelfAmount = (int)fc.Sign * fc.CertifiedCorrectedApprovedSelfAmount,
                       CertifiedTotalAmount = (int)fc.Sign * fc.CertifiedCorrectedApprovedTotalAmount,
                   };
        }

        private IQueryable<AnnualAccountReportApprovedCertifiedAmountVO> GetAnnualAccountReportCertFinancialCorrectionCSDs()
        {
            return from aarcsd in this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>()
                   join fc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>() on aarcsd.ContractReportCertAuthorityFinancialCorrectionCSDId equals fc.ContractReportCertAuthorityFinancialCorrectionCSDId
                   join fbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fc.ContractReportFinancialCSDBudgetItemId equals fbi.ContractReportFinancialCSDBudgetItemId
                   join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                   join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                   join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                   join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fc.ContractReportId equals crp.ContractReportId
                   join c in this.unitOfWork.DbContext.Set<Contract>() on fc.ContractId equals c.ContractId
                   where crp.Status == ContractReportPaymentStatus.Actual && fc.Status == ContractReportCertAuthorityFinancialCorrectionCSDStatus.Ended
                   select new AnnualAccountReportApprovedCertifiedAmountVO
                   {
                       Id = fc.ContractReportCertAuthorityFinancialCorrectionCSDId,
                       ContractId = (int?)fc.ContractId,
                       ContractType = c.ContractType,
                       AnnualAccountReportId = aarcsd.AnnualAccountReportId,
                       ContractReportId = fc.ContractReportId,
                       ContractReportPaymentId = (int?)crp.ContractReportPaymentId,
                       ProgrammePriorityId = pp.MapNodeId,
                       ProgrammePriorityName = pp.Name,
                       AdvancePayment = fbi.AdvancePayment,
                       ApprovedEuAmount = (decimal?)null,
                       ApprovedBgAmount = (decimal?)null,
                       ApprovedBfpTotalAmount = (decimal?)null,
                       ApprovedSelfAmount = (decimal?)null,
                       ApprovedTotalAmount = (decimal?)null,
                       CertifiedEuAmount = (int)fc.Sign * fc.CertifiedEuAmount,
                       CertifiedBgAmount = (int)fc.Sign * fc.CertifiedBgAmount,
                       CertifiedBfpTotalAmount = (int)fc.Sign * fc.CertifiedBfpTotalAmount,
                       CertifiedSelfAmount = (int)fc.Sign * fc.CertifiedSelfAmount,
                       CertifiedTotalAmount = (int)fc.Sign * fc.CertifiedTotalAmount,
                   };
        }

        public int[] FindAllUnattachedFinancialCorrectionCSDs(int contractReportFinancialCorrectionId)
        {
            var attachedCsds = this.unitOfWork.DbContext.Set<AnnualAccountReportFinancialCorrectionCSD>()
                .Where(x => x.AnnualAccountReport.Status != AnnualAccountReportStatus.Opened)
                .Select(x => x.ContractReportFinancialCorrectionCSDId);

            var result = (from crfcscd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                          .Where(x => x.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId && !attachedCsds.Contains(x.ContractReportFinancialCorrectionCSDId) && x.Status == ContractReportFinancialCorrectionCSDStatus.Ended)
                          join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crfcscd.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                          select crfcscd.ContractReportFinancialCorrectionCSDId).ToArray();

            return result;
        }

        public int[] FindAllAttachedFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId)
        {
            return (from aarcsd in this.unitOfWork.DbContext.Set<AnnualAccountReportFinancialCorrectionCSD>().Where(x => x.AnnualAccountReportId == annualAccountReportId)
                    join crfcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>().Where(x => x.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId) on aarcsd.ContractReportFinancialCorrectionCSDId equals crfcsd.ContractReportFinancialCorrectionCSDId
                    select aarcsd.ContractReportFinancialCorrectionCSDId).ToArray();
        }

        public int[] FindAllUnattachedCertFinancialCorrectionCSDs(int contractReportCertAuthorityFinancialCorrectionId)
        {
            var attachedCsds = this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>()
                .Where(x => x.AnnualAccountReport.Status != AnnualAccountReportStatus.Opened)
                .Select(x => x.ContractReportCertAuthorityFinancialCorrectionCSDId);

            var result = (from crfcscd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>()
                          .Where(x => x.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId && !attachedCsds.Contains(x.ContractReportCertAuthorityFinancialCorrectionCSDId) && x.Status == ContractReportCertAuthorityFinancialCorrectionCSDStatus.Ended)
                          join crfc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrection>() on crfcscd.ContractReportCertAuthorityFinancialCorrectionId equals crfc.ContractReportCertAuthorityFinancialCorrectionId
                          select crfcscd.ContractReportCertAuthorityFinancialCorrectionCSDId).ToArray();

            return result;
        }

        public int[] FindAllAttachedCertFinancialCorrectionCSDs(int annualAccountReportId, int certAuthorityFinancialCorrectionId)
        {
            return (from aarcfccsd in this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>().Where(x => x.AnnualAccountReportId == annualAccountReportId)
                    join crfcacsd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>().Where(x => x.ContractReportCertAuthorityFinancialCorrectionId == certAuthorityFinancialCorrectionId) on aarcfccsd.ContractReportCertAuthorityFinancialCorrectionCSDId equals crfcacsd.ContractReportCertAuthorityFinancialCorrectionCSDId
                    select aarcfccsd.ContractReportCertAuthorityFinancialCorrectionCSDId).ToArray();
        }

        public int[] FindCertAuthorityFinancialCorrectionIds(int[] contractReportFinancialCorrectionCSDIds)
        {
            var result = (from crfcscd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>()
                          .Where(x => contractReportFinancialCorrectionCSDIds.Contains(x.ContractReportCertAuthorityFinancialCorrectionCSDId))
                          join crfc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrection>() on crfcscd.ContractReportCertAuthorityFinancialCorrectionId equals crfc.ContractReportCertAuthorityFinancialCorrectionId
                          select crfc.ContractReportCertAuthorityFinancialCorrectionId).Distinct().ToArray();

            return result;
        }

        public int[] FindFinancialCorrectionCSDs(int[] contractReportFinancialCorrectionCSDIds)
        {
            var result = (from crfcscd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                          .Where(x => contractReportFinancialCorrectionCSDIds.Contains(x.ContractReportFinancialCorrectionCSDId))
                          join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crfcscd.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                          select crfcscd.ContractReportFinancialCorrectionCSDId).ToArray();

            return result;
        }

        public IList<ContractReportFinancialCorrectionCSDsVO> GetContractReportFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId)
        {
            var predicate = PredicateBuilder.True<ContractReportFinancialCorrectionCSD>()
                .And(x => x.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId);

            var results = (from aarcsd in this.unitOfWork.DbContext.Set<AnnualAccountReportFinancialCorrectionCSD>().Where(x => x.AnnualAccountReportId == annualAccountReportId)
                           join fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>().Where(predicate) on aarcsd.ContractReportFinancialCorrectionCSDId equals fccsd.ContractReportFinancialCorrectionCSDId
                           join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           select new ContractReportFinancialCorrectionCSDsVO
                           {
                               ContractReportFinancialCorrectionCSDId = fccsd.ContractReportFinancialCorrectionCSDId,
                               ContractReportFinancialCSDBudgetItemId = bi.ContractReportFinancialCSDBudgetItemId,
                               ContractReportFinancialCSDId = bi.ContractReportFinancialCSDId,
                               ContractReportFinancialId = bi.ContractReportFinancialId,
                               ContractReportId = bi.ContractReportId,
                               ContractId = bi.ContractId,

                               Type = csd.Type,
                               Number = csd.Number,
                               Date = csd.Date,
                               PaymentDate = csd.PaymentDate,
                               CompanyType = csd.CompanyType,
                               CompanyName = csd.CompanyName,
                               CompanyUin = csd.CompanyUin,
                               CompanyUinType = csd.CompanyUinType,
                               ContractContractorName = csd.ContractContractorName,

                               BudgetDetailName = bi.BudgetDetailName,
                               ContractActivityName = bi.ContractActivityName,
                               EuAmount = bi.EuAmount,
                               BgAmount = bi.BgAmount,
                               SelfAmount = bi.SelfAmount,
                               TotalAmount = bi.TotalAmount,

                               Status = fccsd.Status,
                               Sign = fccsd.Sign,
                               CostSupportingDocumentApproved = bi.CostSupportingDocumentApproved,

                               ApprovedEuAmount = bi.ApprovedEuAmount,
                               ApprovedBgAmount = bi.ApprovedBgAmount,
                               ApprovedSelfAmount = bi.ApprovedSelfAmount,
                               ApprovedTotalAmount = bi.ApprovedTotalAmount,

                               CorrectedApprovedEuAmount = fccsd.CorrectedApprovedEuAmount,
                               CorrectedApprovedBgAmount = fccsd.CorrectedApprovedBgAmount,
                               CorrectedApprovedSelfAmount = fccsd.CorrectedApprovedSelfAmount,
                               CorrectedApprovedTotalAmount = fccsd.CorrectedApprovedTotalAmount,

                               CertStatus = fccsd.CertStatus,
                               CertifiedCorrectedApprovedEuAmount = fccsd.CertifiedCorrectedApprovedEuAmount,
                               CertifiedCorrectedApprovedBgAmount = fccsd.CertifiedCorrectedApprovedBgAmount,
                               CertifiedCorrectedApprovedSelfAmount = fccsd.CertifiedCorrectedApprovedSelfAmount,
                               CertifiedCorrectedApprovedTotalAmount = fccsd.CertifiedCorrectedApprovedTotalAmount,
                               Version = fccsd.Version,
                           })
                    .ToList();

            return results;
        }

        public IList<ContractReportFinancialCorrectionCSDsVO> GetFinancialCorrectionCSDsForContractReportFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId)
        {
            var attachedCsds = this.unitOfWork.DbContext.Set<AnnualAccountReportFinancialCorrectionCSD>().Select(x => x.ContractReportFinancialCorrectionCSDId);

            var predicate = PredicateBuilder.True<ContractReportFinancialCorrectionCSD>()
                .And(x => x.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId)
                .And(x => !attachedCsds.Contains(x.ContractReportFinancialCorrectionCSDId));

            var results = (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>().Where(predicate)
                           join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           select new ContractReportFinancialCorrectionCSDsVO
                           {
                               ContractReportFinancialCorrectionCSDId = fccsd.ContractReportFinancialCorrectionCSDId,
                               ContractReportFinancialCSDBudgetItemId = bi.ContractReportFinancialCSDBudgetItemId,
                               ContractReportFinancialCSDId = bi.ContractReportFinancialCSDId,
                               ContractReportFinancialId = bi.ContractReportFinancialId,
                               ContractReportId = bi.ContractReportId,
                               ContractId = bi.ContractId,

                               Type = csd.Type,
                               Number = csd.Number,
                               Date = csd.Date,
                               PaymentDate = csd.PaymentDate,
                               CompanyType = csd.CompanyType,
                               CompanyName = csd.CompanyName,
                               CompanyUin = csd.CompanyUin,
                               CompanyUinType = csd.CompanyUinType,
                               ContractContractorName = csd.ContractContractorName,

                               BudgetDetailName = bi.BudgetDetailName,
                               ContractActivityName = bi.ContractActivityName,
                               EuAmount = bi.EuAmount,
                               BgAmount = bi.BgAmount,
                               SelfAmount = bi.SelfAmount,
                               TotalAmount = bi.TotalAmount,

                               Status = fccsd.Status,
                               Sign = fccsd.Sign,
                               CostSupportingDocumentApproved = bi.CostSupportingDocumentApproved,

                               ApprovedEuAmount = bi.ApprovedEuAmount,
                               ApprovedBgAmount = bi.ApprovedBgAmount,
                               ApprovedSelfAmount = bi.ApprovedSelfAmount,
                               ApprovedTotalAmount = bi.ApprovedTotalAmount,

                               CorrectedApprovedEuAmount = fccsd.CorrectedApprovedEuAmount,
                               CorrectedApprovedBgAmount = fccsd.CorrectedApprovedBgAmount,
                               CorrectedApprovedSelfAmount = fccsd.CorrectedApprovedSelfAmount,
                               CorrectedApprovedTotalAmount = fccsd.CorrectedApprovedTotalAmount,

                               CertStatus = fccsd.CertStatus,
                               CertifiedCorrectedApprovedEuAmount = fccsd.CertifiedCorrectedApprovedEuAmount,
                               CertifiedCorrectedApprovedBgAmount = fccsd.CertifiedCorrectedApprovedBgAmount,
                               CertifiedCorrectedApprovedSelfAmount = fccsd.CertifiedCorrectedApprovedSelfAmount,
                               CertifiedCorrectedApprovedTotalAmount = fccsd.CertifiedCorrectedApprovedTotalAmount,
                               Version = fccsd.Version,
                           })
                    .ToList();

            return results;
        }

        public IList<AnnualAccountReportCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCorrections(int annualAccountReportId)
        {
            var programmeId = this.GetProgrammeId(annualAccountReportId);

            var attachedCorrections = this.unitOfWork.DbContext.Set<AnnualAccountReportContractReportCorrection>()
                .Where(x => x.AnnualAccountReport.Status != AnnualAccountReportStatus.Opened)
                .Select(x => x.ContractReportCorrectionId);

            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals p.MapNodeId

                    where programmeId == crc.ProgrammeId &&
                          crc.CertReportId == null &&
                          crc.Status == ContractReportCorrectionStatus.Entered &&
                          !attachedCorrections.Contains(crc.ContractReportCorrectionId)
                    select new AnnualAccountReportCorrectionVO
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

        public IList<AnnualAccountReportCorrectionVO> GetAnnualAccountReportCorrections(int annualAccountReportId)
        {
            return (from aarcrc in this.unitOfWork.DbContext.Set<AnnualAccountReportContractReportCorrection>().Where(x => x.AnnualAccountReportId == annualAccountReportId)
                    join crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>() on aarcrc.ContractReportCorrectionId equals crc.ContractReportCorrectionId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals p.MapNodeId
                    orderby crc.ContractReportCorrectionId descending
                    select new AnnualAccountReportCorrectionVO
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

        public IList<ContractReportCertAuthorityFinancialCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCertFinancialCorrections(int annualAccountReportId)
        {
            var programmeId = this.GetProgrammeId(annualAccountReportId);

            var attachedCertFinancialCorrections = this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>()
                .Where(x => x.AnnualAccountReport.Status != AnnualAccountReportStatus.Opened)
                .Select(x => x.ContractReportCertAuthorityFinancialCorrectionCSDId);

            return (from crfccds in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>()
                        .Where(x => !attachedCertFinancialCorrections.Contains(x.ContractReportCertAuthorityFinancialCorrectionCSDId) && x.Status == ContractReportCertAuthorityFinancialCorrectionCSDStatus.Ended)
                    join crfc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrection>()
                        .Where(c => c.Status == ContractReportCertAuthorityFinancialCorrectionStatus.Ended) on crfccds.ContractReportCertAuthorityFinancialCorrectionId equals crfc.ContractReportCertAuthorityFinancialCorrectionId
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(x => x.ProgrammeId == programmeId) on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    select new ContractReportCertAuthorityFinancialCorrectionVO
                    {
                        ContractReportCertAuthorityFinancialCorrectionId = crfc.ContractReportCertAuthorityFinancialCorrectionId,
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
                    }).Distinct()
                    .ToList();
        }

        public IList<AnnualAccountReportCertFinancialCorrectionVO> GetAnnualAccountReportCertFinancialCorrections(int annualAccountReportId)
        {
            var certFinancialCorrectionCSDs = this.GetAnnualAccountReportCertFinancialCorrectionCSDs().Where(t => t.AnnualAccountReportId == annualAccountReportId);
            var csds = from csd in certFinancialCorrectionCSDs
                       join crfccsd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>() on csd.Id equals crfccsd.ContractReportCertAuthorityFinancialCorrectionCSDId
                       group new
                       {
                           csd.CertifiedBfpTotalAmount,
                           csd.CertifiedSelfAmount,
                       }
                       by crfccsd.ContractReportCertAuthorityFinancialCorrectionId
                        into g
                       select new
                       {
                           ContractReportCertAuthorityFinancialCorrectionId = g.Key,
                           CertifiedBfpTotalAmount = g.Sum(t => t.CertifiedBfpTotalAmount),
                           CertifiedSelfAmount = g.Sum(t => t.CertifiedSelfAmount),
                       };

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrection>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    join csd in csds on crfc.ContractReportCertAuthorityFinancialCorrectionId equals csd.ContractReportCertAuthorityFinancialCorrectionId

                    select new AnnualAccountReportCertFinancialCorrectionVO
                    {
                        ContractReportCertAuthorityFinancialCorrectionId = crfc.ContractReportCertAuthorityFinancialCorrectionId,
                        AnnualAccountReportId = annualAccountReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        Notes = crfc.Notes,
                        CreateDate = crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                        CertifiedBfpTotalAmount = csd.CertifiedBfpTotalAmount,
                        CertifiedSelfAmount = csd.CertifiedSelfAmount,
                    })
                .ToList();
        }

        public List<ContractReportCertAuthorityFinancialCorrectionCSDsVO> GetContractReportCertAuthorityFinancialCorrectionCSDs(int annualAccountReportId, int contractReportCertAuthorityFinancialCorrectionId)
        {
            var results = (from aarcrfcscd in this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>().Where(x => x.AnnualAccountReportId == annualAccountReportId)
                           join crcafccsd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>() on aarcrfcscd.ContractReportCertAuthorityFinancialCorrectionCSDId equals crcafccsd.ContractReportCertAuthorityFinancialCorrectionCSDId
                           join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crcafccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           where crcafccsd.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId
                           select new ContractReportCertAuthorityFinancialCorrectionCSDsVO
                           {
                               ContractReportCertAuthorityFinancialCorrectionCSDId = crcafccsd.ContractReportCertAuthorityFinancialCorrectionCSDId,
                               ContractReportFinancialCSDBudgetItemId = bi.ContractReportFinancialCSDBudgetItemId,
                               ContractReportFinancialCSDId = bi.ContractReportFinancialCSDId,
                               ContractReportFinancialId = bi.ContractReportFinancialId,
                               ContractReportId = bi.ContractReportId,
                               ContractId = bi.ContractId,

                               Type = csd.Type,
                               Number = csd.Number,
                               Date = csd.Date,
                               PaymentDate = csd.PaymentDate,
                               CompanyType = csd.CompanyType,
                               CompanyName = csd.CompanyName,
                               CompanyUin = csd.CompanyUin,
                               CompanyUinType = csd.CompanyUinType,
                               ContractContractorName = csd.ContractContractorName,

                               BudgetDetailName = bi.BudgetDetailName,
                               ContractActivityName = bi.ContractActivityName,
                               EuAmount = bi.EuAmount,
                               BgAmount = bi.BgAmount,
                               SelfAmount = bi.SelfAmount,
                               TotalAmount = bi.TotalAmount,

                               Status = crcafccsd.Status,
                               Sign = crcafccsd.Sign,
                               CostSupportingDocumentApproved = bi.CostSupportingDocumentApproved,

                               ApprovedEuAmount = bi.ApprovedEuAmount,
                               ApprovedBgAmount = bi.ApprovedBgAmount,
                               ApprovedSelfAmount = bi.ApprovedSelfAmount,
                               ApprovedTotalAmount = bi.ApprovedTotalAmount,

                               CorrectedCertifiedBgAmount = (int)crcafccsd.Sign * crcafccsd.CertifiedBgAmount,
                               CorrectedCertifiedEuAmount = (int)crcafccsd.Sign * crcafccsd.CertifiedEuAmount,
                               CorrectedCertifiedSelfAmount = (int)crcafccsd.Sign * crcafccsd.CertifiedSelfAmount,
                               CorrectedCertifiedTotalAmount = (int)crcafccsd.Sign * crcafccsd.CertifiedTotalAmount,

                               CertifiedEuAmount = bi.CertifiedApprovedEuAmount,
                               CertifiedBgAmount = bi.CertifiedApprovedBgAmount,
                               CertifiedSelfAmount = bi.CertifiedApprovedSelfAmount,
                               CertifiedTotalAmount = bi.CertifiedApprovedTotalAmount,
                               Version = crcafccsd.Version,
                           })
                    .ToList();

            IEnumerable<ContractReportCertAuthorityFinancialCorrectionCSDsVO> filteredResult = results;

            return filteredResult.ToList();
        }

        public List<ContractReportCertAuthorityFinancialCorrectionCSDsVO> GetFinancialCorrectionCSDsForContractReportCertAuthorityFinancialCorrectionCSDs(int contractReportCertAuthorityFinancialCorrectionId)
        {
            var attachedCSDs = this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>().Select(x => x.ContractReportCertAuthorityFinancialCorrectionCSDId);

            var results = (from crcafccsd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>().Where(x => !attachedCSDs.Contains(x.ContractReportCertAuthorityFinancialCorrectionCSDId))
                           join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crcafccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           where crcafccsd.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId && crcafccsd.Status == ContractReportCertAuthorityFinancialCorrectionCSDStatus.Ended
                           select new ContractReportCertAuthorityFinancialCorrectionCSDsVO
                           {
                               ContractReportCertAuthorityFinancialCorrectionCSDId = crcafccsd.ContractReportCertAuthorityFinancialCorrectionCSDId,
                               ContractReportFinancialCSDBudgetItemId = bi.ContractReportFinancialCSDBudgetItemId,
                               ContractReportFinancialCSDId = bi.ContractReportFinancialCSDId,
                               ContractReportFinancialId = bi.ContractReportFinancialId,
                               ContractReportId = bi.ContractReportId,
                               ContractId = bi.ContractId,

                               Type = csd.Type,
                               Number = csd.Number,
                               Date = csd.Date,
                               PaymentDate = csd.PaymentDate,
                               CompanyType = csd.CompanyType,
                               CompanyName = csd.CompanyName,
                               CompanyUin = csd.CompanyUin,
                               CompanyUinType = csd.CompanyUinType,
                               ContractContractorName = csd.ContractContractorName,

                               BudgetDetailName = bi.BudgetDetailName,
                               ContractActivityName = bi.ContractActivityName,
                               EuAmount = bi.EuAmount,
                               BgAmount = bi.BgAmount,
                               SelfAmount = bi.SelfAmount,
                               TotalAmount = bi.TotalAmount,

                               Status = crcafccsd.Status,
                               Sign = crcafccsd.Sign,
                               CostSupportingDocumentApproved = bi.CostSupportingDocumentApproved,

                               ApprovedEuAmount = bi.ApprovedEuAmount,
                               ApprovedBgAmount = bi.ApprovedBgAmount,
                               ApprovedSelfAmount = bi.ApprovedSelfAmount,
                               ApprovedTotalAmount = bi.ApprovedTotalAmount,

                               CorrectedCertifiedBgAmount = (int)crcafccsd.Sign * crcafccsd.CertifiedBgAmount,
                               CorrectedCertifiedEuAmount = (int)crcafccsd.Sign * crcafccsd.CertifiedEuAmount,
                               CorrectedCertifiedSelfAmount = (int)crcafccsd.Sign * crcafccsd.CertifiedSelfAmount,
                               CorrectedCertifiedTotalAmount = (int)crcafccsd.Sign * crcafccsd.CertifiedTotalAmount,

                               CertifiedEuAmount = bi.CertifiedApprovedEuAmount,
                               CertifiedBgAmount = bi.CertifiedApprovedBgAmount,
                               CertifiedSelfAmount = bi.CertifiedApprovedSelfAmount,
                               CertifiedTotalAmount = bi.CertifiedApprovedTotalAmount,
                               Version = crcafccsd.Version,
                           })
                   .ToList();

            IEnumerable<ContractReportCertAuthorityFinancialCorrectionCSDsVO> filteredResult = results;

            return filteredResult.ToList();
        }

        public IList<ContractReportCertAuthorityCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCertCorrections(int annualAccountReportId)
        {
            var programmeId = this.GetProgrammeId(annualAccountReportId);

            var attachedCertAuthorityCorrections = this.unitOfWork.DbContext.Set<AnnualAccountReportCertCorrection>()
                .Where(x => x.AnnualAccountReport.Status != AnnualAccountReportStatus.Opened)
                .Select(x => x.ContractReportCertAuthorityCorrectionId);

            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>().Where(x => x.ProgrammeId == programmeId)
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where !attachedCertAuthorityCorrections.Contains(crc.ContractReportCertAuthorityCorrectionId) && crc.Status == ContractReportCertAuthorityCorrectionStatus.Entered
                    orderby crc.CreateDate descending
                    select new ContractReportCertAuthorityCorrectionVO
                    {
                        ContractReportCertAuthorityCorrectionId = crc.ContractReportCertAuthorityCorrectionId,
                        ProgrammeName = pr.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                    }).ToList();
        }

        public IList<AnnualAccountReportCertRevalidationFinancialCorrectionVO> GetAnnualAccountReportCertRevalidationFinancialCorrections(int annualAccountReportId)
        {
            var certRevalidationFinancialCorrectionCSDs =
                from aarcsd in this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationFinancialCorrectionCSD>().Where(t => t.AnnualAccountReportId == annualAccountReportId)
                join fc in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>() on aarcsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId equals fc.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fc.ContractReportId equals crp.ContractReportId
                where crp.Status == ContractReportPaymentStatus.Actual && fc.Status == ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Ended
                select new
                {
                    Id = fc.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId,
                    CorrectedEuAmount = (int)fc.Sign * fc.RevalidatedEuAmount,
                    CorrectedBgAmount = (int)fc.Sign * fc.RevalidatedBgAmount,
                    CorrectedBfpTotalAmount = (int)fc.Sign * fc.RevalidatedBfpTotalAmount,
                    CorrectedSelfAmount = (int)fc.Sign * fc.RevalidatedSelfAmount,
                    CorrectedTotalAmount = (int)fc.Sign * fc.RevalidatedTotalAmount,
                };

            var csds = from csd in certRevalidationFinancialCorrectionCSDs
                       join crfccsd in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>() on csd.Id equals crfccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId
                       group new
                       {
                           csd.CorrectedBfpTotalAmount,
                           csd.CorrectedSelfAmount,
                       }
                       by crfccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionId
                       into g
                       select new
                       {
                           ContractReportCertAuthorityFinancialCorrectionId = g.Key,
                           CorrectedBfpTotalAmount = g.Sum(t => t.CorrectedBfpTotalAmount),
                           CorrectedSelfAmount = g.Sum(t => t.CorrectedSelfAmount),
                       };

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrection>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    join csd in csds on crfc.ContractReportRevalidationCertAuthorityFinancialCorrectionId equals csd.ContractReportCertAuthorityFinancialCorrectionId

                    select new AnnualAccountReportCertRevalidationFinancialCorrectionVO
                    {
                        ContractReportRevalidationCertAuthorityFinancialCorrectionId = crfc.ContractReportRevalidationCertAuthorityFinancialCorrectionId,
                        AnnualAccountReportId = annualAccountReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        Notes = crfc.Notes,
                        CreateDate = crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                        CorrectedBfpTotalAmount = csd.CorrectedBfpTotalAmount,
                        CorrectedSelfAmount = csd.CorrectedSelfAmount,
                    })
                .ToList();
        }

        public IList<ContractReportRevalidationCertAuthorityFinancialCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCertRevalidationFinancialCorrections(int annualAccountReportId)
        {
            var programmeId = this.GetProgrammeId(annualAccountReportId);

            var attachedCertRevalidationFinancialCorrections = this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationFinancialCorrectionCSD>()
                .Where(x => x.AnnualAccountReport.Status != AnnualAccountReportStatus.Opened)
                .Select(x => x.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId);

            return (from crfccds in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>()
                        .Where(x => !attachedCertRevalidationFinancialCorrections.Contains(x.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId) && x.Status == ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Ended)
                    join crfc in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrection>()
                        .Where(c => c.Status == ContractReportRevalidationCertAuthorityFinancialCorrectionStatus.Ended) on crfccds.ContractReportRevalidationCertAuthorityFinancialCorrectionId equals crfc.ContractReportRevalidationCertAuthorityFinancialCorrectionId
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(x => x.ProgrammeId == programmeId) on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    select new ContractReportRevalidationCertAuthorityFinancialCorrectionVO
                    {
                        ContractReportRevalidationCertAuthorityFinancialCorrectionId = crfc.ContractReportRevalidationCertAuthorityFinancialCorrectionId,
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
                    }).Distinct()
                    .ToList();
        }

        public IList<ContractReportRevalidationCertAuthorityCorrectionVO> GetUnattachedContractReportRevalidationCorrections(int annualAccountReportId)
        {
            var programmeId = this.GetProgrammeId(annualAccountReportId);

            var attachedCertAuthorityRevalidationCorrections = this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationCorrection>()
                .Where(x => x.AnnualAccountReport.Status != AnnualAccountReportStatus.Opened)
                .Select(x => x.ContractReportRevalidationCertAuthorityCorrectionId);

            return (from crr in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityCorrection>().Where(x => x.ProgrammeId == programmeId)
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crr.ProgrammeId equals pr.MapNodeId
                    where !attachedCertAuthorityRevalidationCorrections.Contains(crr.ContractReportRevalidationCertAuthorityCorrectionId) && crr.Status == ContractReportRevalidationCertAuthorityCorrectionStatus.Entered
                    orderby crr.CreateDate descending
                    select new ContractReportRevalidationCertAuthorityCorrectionVO
                    {
                        ContractReportRevalidationCertAuthorityCorrectionId = crr.ContractReportRevalidationCertAuthorityCorrectionId,
                        ProgrammeName = pr.Name,
                        RegNumber = crr.RegNumber,
                        StatusDescr = crr.Status,
                        Status = crr.Status,
                        Type = crr.Type,
                        Date = crr.Date,
                    }).ToList();
        }

        public IList<ContractReportRevalidationCertAuthorityCorrectionVO> GetAnnualAccountReportCertRevalidationCorrections(int annualAccountReportId)
        {
            return (from aarcrc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationCorrection>().Where(x => x.AnnualAccountReportId == annualAccountReportId)
                    join crr in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityCorrection>() on aarcrc.ContractReportRevalidationCertAuthorityCorrectionId equals crr.ContractReportRevalidationCertAuthorityCorrectionId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crr.ProgrammeId equals pr.MapNodeId
                    orderby crr.CreateDate descending
                    select new ContractReportRevalidationCertAuthorityCorrectionVO
                    {
                        ContractReportRevalidationCertAuthorityCorrectionId = crr.ContractReportRevalidationCertAuthorityCorrectionId,
                        ProgrammeName = pr.Name,
                        RegNumber = crr.RegNumber,
                        StatusDescr = crr.Status,
                        Status = crr.Status,
                        Type = crr.Type,
                        Date = crr.Date,
                    }).ToList();
        }

        public IList<ContractReportCertAuthorityCorrectionVO> GetAnnualAccountReportCertCorrections(int annualAccountReportId)
        {
            return (from aarcc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertCorrection>().Where(x => x.AnnualAccountReportId == annualAccountReportId)
                    join crc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>() on aarcc.ContractReportCertAuthorityCorrectionId equals crc.ContractReportCertAuthorityCorrectionId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    orderby crc.CreateDate descending
                    select new ContractReportCertAuthorityCorrectionVO
                    {
                        ContractReportCertAuthorityCorrectionId = crc.ContractReportCertAuthorityCorrectionId,
                        ProgrammeName = pr.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                    }).ToList();
        }

        public IList<AnnualAccountReportAppendixVO> GetAnnualAccountReportAppendices(int annualAccountReportId, AnnualAccountReportAppendixType type)
        {
            return (from aara in this.unitOfWork.DbContext.Set<AnnualAccountReportAppendix>().Where(x => x.AnnualAccountReportId == annualAccountReportId && x.Type == type)
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on aara.ProgrammePriorityId equals pp.MapNodeId
                    select new AnnualAccountReportAppendixVO
                    {
                        AnnualAccountReportAppendixId = aara.AnnualAccountReportAppendixId,
                        Comment = aara.Comment,
                        ProgrammePriority = pp.Name,
                    }).ToList();
        }

        public IList<int> GetUnattachedCertReports(AnnualAccountReport annualAccountReport)
        {
            var attachedReports = this.unitOfWork.DbContext.Set<AnnualAccountReportCertReport>().Where(x => x.AnnualAccountReport.Status == AnnualAccountReportStatus.Ended).Select(x => x.CertReportId);
            var dateTo = annualAccountReport.GetDateTo();
            var dateFrom = annualAccountReport.GetDateFrom();

            return (from cr in this.unitOfWork.DbContext.Set<CertReport>().Where(x =>
                    x.ProgrammeId == annualAccountReport.ProgrammeId &&
                    !attachedReports.Contains(x.CertReportId) &&
                    (x.Status == CertReportStatus.Ended ||
                    x.Status == CertReportStatus.Approved ||
                    x.Status == CertReportStatus.PartialyApproved) &&
                    ((x.DateFrom <= dateFrom && x.DateTo >= dateTo) ||
                    (x.DateFrom >= dateFrom && x.DateTo <= dateTo) ||
                    (x.DateFrom >= dateFrom && x.DateFrom <= dateTo) ||
                    (x.DateTo >= dateFrom && x.DateTo <= dateTo)))
                    select cr.CertReportId)
                    .Distinct()
                    .ToList();
        }

        public int[] GetUnattachedFinancialCorrectionsCSDs(int[] attachedCertReports)
        {
            return (from crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                    join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crfccsd.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                    join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crfccsd.ContractReportId equals bi.ContractReportId

                    where crfccsd.CertReportId == null && bi.CertReportId != null && attachedCertReports.Contains(bi.CertReportId.Value)
                    select crfccsd.ContractReportFinancialCorrectionCSDId)
                    .Distinct()
                    .ToArray();
        }

        public int[] GetUnattachedCorrections(int[] attachedCertReports)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crc.ContractReportPaymentId equals crp.ContractReportPaymentId
                    join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crp.ContractReportId equals bi.ContractReportId

                    where crc.Status == ContractReportCorrectionStatus.Entered && crc.CertReportId == null && attachedCertReports.Contains(bi.CertReportId.Value)
                    select crc.ContractReportCorrectionId)
                    .Distinct()
                    .ToArray();
        }

        private IQueryable<CertifiedAmountVO> GetAllCertifiedAmounts()
        {
            var csds = this.GetAnnualAccountReportFinancialCorrectionCSDs()
                .Concat(this.GetAnnualAccountReportCertFinancialCorrectionCSDs())
                .Select(x => new
                {
                    AnnualAccountReportId = x.AnnualAccountReportId,
                    CertifiedBfpTotalAmount = x.CertifiedBfpTotalAmount,
                    CertifiedSelfAmount = x.CertifiedSelfAmount,
                });

            var certRevalidationFinancialCorrectionCSDs =
                from aarcsd in this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationFinancialCorrectionCSD>()
                join fc in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>() on aarcsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId equals fc.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId
                join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fc.ContractReportId equals crp.ContractReportId
                where crp.Status == ContractReportPaymentStatus.Actual && fc.Status == ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Ended
                select new
                {
                    AnnualAccountReportId = aarcsd.AnnualAccountReportId,
                    CertifiedBfpTotalAmount = (int)fc.Sign * fc.RevalidatedBfpTotalAmount,
                    CertifiedSelfAmount = (int)fc.Sign * fc.RevalidatedSelfAmount,
                };

            var corrections = from aarcrc in this.unitOfWork.DbContext.Set<AnnualAccountReportContractReportCorrection>()
                              join crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>() on aarcrc.ContractReportCorrectionId equals crc.ContractReportCorrectionId
                              select new
                              {
                                  AnnualAccountReportId = aarcrc.AnnualAccountReportId,
                                  CertifiedBfpTotalAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedBfpTotalAmount,
                                  CertifiedSelfAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedSelfAmount,
                              };

            var certCorrections = from aarcc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertCorrection>()
                                  join crc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>() on aarcc.ContractReportCertAuthorityCorrectionId equals crc.ContractReportCertAuthorityCorrectionId
                                  select new
                                  {
                                      AnnualAccountReportId = aarcc.AnnualAccountReportId,
                                      CertifiedBfpTotalAmount = (int)crc.Sign * crc.CertifiedBfpTotalAmount,
                                      CertifiedSelfAmount = (int)crc.Sign * crc.CertifiedSelfAmount,
                                  };

            var certRevalidationCorrections = from aarcrc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationCorrection>()
                                              join crr in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityCorrection>() on aarcrc.ContractReportRevalidationCertAuthorityCorrectionId equals crr.ContractReportRevalidationCertAuthorityCorrectionId
                                              select new
                                              {
                                                  AnnualAccountReportId = aarcrc.AnnualAccountReportId,
                                                  CertifiedBfpTotalAmount = (int)crr.Sign * crr.CertifiedRevalidatedBfpTotalAmount,
                                                  CertifiedSelfAmount = (int)crr.Sign * crr.CertifiedRevalidatedSelfAmount,
                                              };

            return csds
                .Concat(certRevalidationFinancialCorrectionCSDs)
                .Concat(corrections)
                .Concat(certCorrections)
                .Concat(certRevalidationCorrections)
                .GroupBy(x => x.AnnualAccountReportId)
                .Select(g => new CertifiedAmountVO
                {
                    Id = g.Key,
                    CertifiedBfpTotalAmount = g.Sum(p => p.CertifiedBfpTotalAmount),
                    CertifiedSelfAmount = g.Sum(p => p.CertifiedSelfAmount),
                });
        }

        public int[] GetAllUnattachedCertRevalidationFinancialCorrectionCSDs(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            var attachedCsds = this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationFinancialCorrectionCSD>()
                .Where(x => x.AnnualAccountReport.Status != AnnualAccountReportStatus.Opened)
                .Select(x => x.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId);

            var result = (from crrfcscd in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>()
                          .Where(x => x.ContractReportRevalidationCertAuthorityFinancialCorrectionId == contractReportRevalidationCertAuthorityFinancialCorrectionId && !attachedCsds.Contains(x.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId) && x.Status == ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Ended)
                          join crrfc in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrection>() on crrfcscd.ContractReportRevalidationCertAuthorityFinancialCorrectionId equals crrfc.ContractReportRevalidationCertAuthorityFinancialCorrectionId
                          select crrfcscd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId).ToArray();

            return result;
        }

        public int[] GetAllAttachedCertRevalidationFinancialCorrectionCSDs(int annualAccountReportId, int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            return (from aarcrfccsd in this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationFinancialCorrectionCSD>().Where(x => x.AnnualAccountReportId == annualAccountReportId)
                    join crrfcacsd in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>().Where(x => x.ContractReportRevalidationCertAuthorityFinancialCorrectionId == contractReportRevalidationCertAuthorityFinancialCorrectionId) on aarcrfccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId equals crrfcacsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId
                    select aarcrfccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId).ToArray();
        }

        public IList<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO> GetContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(
            int annualAccountReportId,
            int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            var results =
                (from aarcrfccsd in this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationFinancialCorrectionCSD>().Where(a => a.AnnualAccountReportId == annualAccountReportId)
                 join crcafccsd in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>() on aarcrfccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId equals crcafccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId
                 join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crcafccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                 join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                 where crcafccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionId == contractReportRevalidationCertAuthorityFinancialCorrectionId
                 select new ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO
                 {
                     ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId = crcafccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId,
                     ContractReportFinancialCSDBudgetItemId = bi.ContractReportFinancialCSDBudgetItemId,
                     ContractReportFinancialCSDId = bi.ContractReportFinancialCSDId,
                     ContractReportFinancialId = bi.ContractReportFinancialId,
                     ContractReportId = bi.ContractReportId,
                     ContractId = bi.ContractId,

                     Type = csd.Type,
                     Number = csd.Number,
                     Date = csd.Date,
                     PaymentDate = csd.PaymentDate,
                     CompanyType = csd.CompanyType,
                     CompanyName = csd.CompanyName,
                     CompanyUin = csd.CompanyUin,
                     CompanyUinType = csd.CompanyUinType,
                     ContractContractorName = csd.ContractContractorName,

                     BudgetDetailName = bi.BudgetDetailName,
                     ContractActivityName = bi.ContractActivityName,
                     EuAmount = bi.EuAmount,
                     BgAmount = bi.BgAmount,
                     SelfAmount = bi.SelfAmount,
                     TotalAmount = bi.TotalAmount,

                     Status = crcafccsd.Status,
                     Sign = crcafccsd.Sign,
                     CostSupportingDocumentApproved = bi.CostSupportingDocumentApproved,

                     ApprovedEuAmount = bi.ApprovedEuAmount,
                     ApprovedBgAmount = bi.ApprovedBgAmount,
                     ApprovedSelfAmount = bi.ApprovedSelfAmount,
                     ApprovedTotalAmount = bi.ApprovedTotalAmount,

                     CorrectedRevalidatedBgAmount = (int)crcafccsd.Sign * crcafccsd.RevalidatedBgAmount,
                     CorrectedRevalidatedEuAmount = (int)crcafccsd.Sign * crcafccsd.RevalidatedEuAmount,
                     CorrectedRevalidatedSelfAmount = (int)crcafccsd.Sign * crcafccsd.RevalidatedSelfAmount,
                     CorrectedRevalidatedTotalAmount = (int)crcafccsd.Sign * crcafccsd.RevalidatedTotalAmount,

                     CertifiedEuAmount = bi.CertifiedApprovedEuAmount,
                     CertifiedBgAmount = bi.CertifiedApprovedBgAmount,
                     CertifiedSelfAmount = bi.CertifiedApprovedSelfAmount,
                     CertifiedTotalAmount = bi.CertifiedApprovedTotalAmount,
                     Version = crcafccsd.Version,
                 }).ToList();

            return results;
        }

        public IList<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO> GetFinancialCorrectionCSDsForContractReportCertAuthorityRevalidationFinancialCorrectionCSDs(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            var attachedCSDs = this.unitOfWork.DbContext.Set<AnnualAccountReportCertRevalidationFinancialCorrectionCSD>().Select(x => x.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId);

            var results =
                (from crcafccsd in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>().Where(c => !attachedCSDs.Contains(c.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId))
                 join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crcafccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                 join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                 where crcafccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionId == contractReportRevalidationCertAuthorityFinancialCorrectionId
                 select new ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO
                 {
                     ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId = crcafccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId,
                     ContractReportFinancialCSDBudgetItemId = bi.ContractReportFinancialCSDBudgetItemId,
                     ContractReportFinancialCSDId = bi.ContractReportFinancialCSDId,
                     ContractReportFinancialId = bi.ContractReportFinancialId,
                     ContractReportId = bi.ContractReportId,
                     ContractId = bi.ContractId,

                     Type = csd.Type,
                     Number = csd.Number,
                     Date = csd.Date,
                     PaymentDate = csd.PaymentDate,
                     CompanyType = csd.CompanyType,
                     CompanyName = csd.CompanyName,
                     CompanyUin = csd.CompanyUin,
                     CompanyUinType = csd.CompanyUinType,
                     ContractContractorName = csd.ContractContractorName,

                     BudgetDetailName = bi.BudgetDetailName,
                     ContractActivityName = bi.ContractActivityName,
                     EuAmount = bi.EuAmount,
                     BgAmount = bi.BgAmount,
                     SelfAmount = bi.SelfAmount,
                     TotalAmount = bi.TotalAmount,

                     Status = crcafccsd.Status,
                     Sign = crcafccsd.Sign,
                     CostSupportingDocumentApproved = bi.CostSupportingDocumentApproved,

                     ApprovedEuAmount = bi.ApprovedEuAmount,
                     ApprovedBgAmount = bi.ApprovedBgAmount,
                     ApprovedSelfAmount = bi.ApprovedSelfAmount,
                     ApprovedTotalAmount = bi.ApprovedTotalAmount,

                     CorrectedRevalidatedBgAmount = (int)crcafccsd.Sign * crcafccsd.RevalidatedBgAmount,
                     CorrectedRevalidatedEuAmount = (int)crcafccsd.Sign * crcafccsd.RevalidatedEuAmount,
                     CorrectedRevalidatedSelfAmount = (int)crcafccsd.Sign * crcafccsd.RevalidatedSelfAmount,
                     CorrectedRevalidatedTotalAmount = (int)crcafccsd.Sign * crcafccsd.RevalidatedTotalAmount,

                     CertifiedEuAmount = bi.CertifiedApprovedEuAmount,
                     CertifiedBgAmount = bi.CertifiedApprovedBgAmount,
                     CertifiedSelfAmount = bi.CertifiedApprovedSelfAmount,
                     CertifiedTotalAmount = bi.CertifiedApprovedTotalAmount,
                     Version = crcafccsd.Version,
                 }).ToList();

            return results;
        }

        private class CertifiedAmountVO
        {
            public int Id { get; set; }

            public decimal? CertifiedBfpTotalAmount { get; set; }

            public decimal? CertifiedSelfAmount { get; set; }
        }

        private class AnnualAccountReportApprovedCertifiedAmountVO
        {
            public int Id { get; set; }

            public int? ContractId { get; set; }

            public ContractType? ContractType { get; set; }

            public int AnnualAccountReportId { get; set; }

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
