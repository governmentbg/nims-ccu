using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.ContractReportFinancialCSDs.PortalViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReportFinancialCSDs.Repositories
{
    internal class ContractReportFinancialCSDBudgetItemsRepository : AggregateRepository<ContractReportFinancialCSDBudgetItem>, IContractReportFinancialCSDBudgetItemsRepository
    {
        public ContractReportFinancialCSDBudgetItemsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportFinancialCSDBudgetItem> FindAll(int contractReportId)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId).ToList();
        }

        public IList<ContractReportFinancialCSDBudgetItem> FindAll(int contractReportId, int[] contractReportFinancialCSDBudgetItemIds)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId && contractReportFinancialCSDBudgetItemIds.Contains(t.ContractReportFinancialCSDBudgetItemId)).ToList();
        }

        public IList<ContractReportFinancialCSDBudgetItem> FindAllUnattached(int contractReportId)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId && t.CertReportId == null).ToList();
        }

        public IList<ContractReportFinancialCSDBudgetItem> FindAllByCertReport(int certReportId, int contractReportId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId && t.ContractReportId == contractReportId).ToList();
        }

        public IList<ContractReportFinancialCSDBudgetItem> FindAllByCertReport(int certReportId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId).ToList();
        }

        public IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItems(
            int contractReportId,
            string csdFilter,
            string company,
            bool? isAttachedToCertReport = null,
            int? certReportId = null)
        {
            var predicate = PredicateBuilder.True<ContractReportFinancialCSDBudgetItem>();

            if (isAttachedToCertReport.HasValue)
            {
                if (isAttachedToCertReport.Value == true)
                {
                    predicate = predicate.AndPropertyEquals(t => t.CertReportId, certReportId.Value);
                }
                else
                {
                    predicate = predicate.AndPropertyEquals(t => t.CertReportId, null);
                }
            }

            var results = (from bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(predicate)
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on bi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                           join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                           join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                           where bi.ContractReportId == contractReportId
                           select new ContractReportFinancialCSDBudgetItemsVO
                           {
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
                               AdvancePayment = bi.AdvancePayment,
                               CrossFinancing = bi.CrossFinancing,

                               BudgetDetailName = bi.BudgetDetailName,
                               ContractActivityName = bi.ContractActivityName,
                               BgAmount = bi.BgAmount,
                               SelfAmount = bi.SelfAmount,
                               TotalAmount = bi.TotalAmount,

                               ProgrammePriorityId = ps.ProgrammePriorityId,

                               Status = bi.Status,
                               CostSupportingDocumentApproved = bi.CostSupportingDocumentApproved,

                               ApprovedEuAmount = bi.ApprovedEuAmount,
                               ApprovedBgAmount = bi.ApprovedBgAmount,
                               ApprovedBfpTotalAmount = bi.ApprovedBfpTotalAmount,
                               ApprovedSelfAmount = bi.ApprovedSelfAmount,
                               ApprovedTotalAmount = bi.ApprovedTotalAmount,

                               CertStatus = bi.CertStatus,
                               CertifiedApprovedEuAmount = bi.CertifiedApprovedEuAmount,
                               CertifiedApprovedBgAmount = bi.CertifiedApprovedBgAmount,
                               CertifiedApprovedSelfAmount = bi.CertifiedApprovedSelfAmount,
                               CertifiedApprovedTotalAmount = bi.CertifiedApprovedTotalAmount,
                               Version = bi.Version,
                           })
                    .ToList();

            IEnumerable<ContractReportFinancialCSDBudgetItemsVO> filteredResult = results;

            if (!string.IsNullOrEmpty(csdFilter))
            {
                filteredResult = results.Where(t =>
                {
                    var name = string.Format("{0} {1} {2: dd.MM.yyyy}", t.Type.GetEnumDescription(), t.Number, t.Date);
                    return name.IndexOf(csdFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                });
            }

            if (!string.IsNullOrEmpty(company))
            {
                filteredResult = filteredResult.Where(t =>
                {
                    var name = string.Format("({0}){1} {2} {3}", t.CompanyType.GetEnumDescription(), t.CompanyUinType.GetEnumDescription(), t.CompanyUin, t.CompanyName);
                    return name.IndexOf(company, StringComparison.OrdinalIgnoreCase) >= 0;
                });
            }

            return filteredResult.ToList();
        }

        public IList<ContractReportFinancialCSDsVO> GetContractReportFinancialCSDsForProjectDossier(int contractId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                    join csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fccsd.ContractReportFinancialCSDBudgetItemId equals csdbi.ContractReportFinancialCSDBudgetItemId
                    join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on csdbi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on fccsd.ContractReportId equals crp.ContractReportId
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on csdbi.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    where fccsd.ContractId == contractId && cr.Status == ContractReportStatus.Accepted
                    select new ContractReportFinancialCSDsVO
                    {
                        ContractReportFinancialCorrectionId = fccsd.ContractReportFinancialCorrectionId,
                        ContractReportFinancialCSDBudgetItemId = csdbi.ContractReportFinancialCSDBudgetItemId,
                        ContractReportFinancialCSDId = csdbi.ContractReportFinancialCSDId,
                        ContractReportFinancialId = csdbi.ContractReportFinancialId,
                        ContractReportId = csdbi.ContractReportId,
                        ContractRegNum = c.RegNumber,
                        PaymentVersionNum = crp.VersionNum,
                        PaymentVersionSubNum = crp.VersionSubNum,
                        PaymentSubmitDate = crp.SubmitDate,
                        ReportNum = cr.OrderNum,
                        ReportSubmitDate = cr.SubmitDate,
                        Type = csd.Type,
                        Number = csd.Number,
                        Date = csd.Date,
                        CompanyType = csd.CompanyType,
                        CompanyName = csd.CompanyName,
                        CompanyUin = csd.CompanyUin,
                        CompanyUinType = csd.CompanyUinType,
                        BfpTotalAmount = csdbi.BfpTotalAmount,
                        TotalAmount = csdbi.TotalAmount,
                        ApprovedBfpTotalAmount = csdbi.ApprovedBfpTotalAmount,
                        ApprovedTotalAmount = csdbi.ApprovedTotalAmount,
                        CorrectedApprovedBfpTotalAmount = -1 * (int)fccsd.Sign * fccsd.CorrectedApprovedBfpTotalAmount,
                        CorrectedApprovedTotalAmount = -1 * (int)fccsd.Sign * fccsd.CorrectedApprovedTotalAmount,
                    })
                    .ToList();
        }

        public async Task<IList<ContractReportFinancialCSDBudgetItemsVO>> GetContractReportFinancialCSDBudgetItemsAsync(
            int contractReportId,
            CancellationToken ct,
            string csdFilter,
            string company,
            bool? isAttachedToCertReport = null,
            int? certReportId = null)
        {
            var predicate = PredicateBuilder.True<ContractReportFinancialCSDBudgetItem>();

            if (isAttachedToCertReport.HasValue)
            {
                if (isAttachedToCertReport.Value == true)
                {
                    predicate = predicate.AndPropertyEquals(t => t.CertReportId, certReportId.Value);
                }
                else
                {
                    predicate = predicate.AndPropertyEquals(t => t.CertReportId, null);
                }
            }

            var results = (from bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(predicate)
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on bi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                           join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                           join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId
                           where bi.ContractReportId == contractReportId
                           select new ContractReportFinancialCSDBudgetItemsVO
                           {
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
                               AdvancePayment = bi.AdvancePayment,
                               CrossFinancing = bi.CrossFinancing,

                               BudgetDetailName = bi.BudgetDetailName,
                               ContractActivityName = bi.ContractActivityName,
                               BgAmount = bi.BgAmount,
                               SelfAmount = bi.SelfAmount,
                               TotalAmount = bi.TotalAmount,

                               ProgrammePriorityId = ps.ProgrammePriorityId,

                               Status = bi.Status,
                               CostSupportingDocumentApproved = bi.CostSupportingDocumentApproved,

                               ApprovedEuAmount = bi.ApprovedEuAmount,
                               ApprovedBgAmount = bi.ApprovedBgAmount,
                               ApprovedBfpTotalAmount = bi.ApprovedBfpTotalAmount,
                               ApprovedSelfAmount = bi.ApprovedSelfAmount,
                               ApprovedTotalAmount = bi.ApprovedTotalAmount,

                               CertStatus = bi.CertStatus,
                               CertifiedApprovedEuAmount = bi.CertifiedApprovedEuAmount,
                               CertifiedApprovedBgAmount = bi.CertifiedApprovedBgAmount,
                               CertifiedApprovedSelfAmount = bi.CertifiedApprovedSelfAmount,
                               CertifiedApprovedTotalAmount = bi.CertifiedApprovedTotalAmount,
                               Version = bi.Version,
                           })
                    .ToListAsync(ct);

            IEnumerable<ContractReportFinancialCSDBudgetItemsVO> filteredResult = await results;

            if (!string.IsNullOrEmpty(csdFilter))
            {
                filteredResult = filteredResult.Where(t =>
                {
                    var name = string.Format("{0} {1} {2: dd.MM.yyyy}", t.Type.GetEnumDescription(), t.Number, t.Date);
                    return name.IndexOf(csdFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                });
            }

            if (!string.IsNullOrEmpty(company))
            {
                filteredResult = filteredResult.Where(t =>
                {
                    var name = string.Format("({0}){1} {2} {3}", t.CompanyType.GetEnumDescription(), t.CompanyUinType.GetEnumDescription(), t.CompanyUin, t.CompanyName);
                    return name.IndexOf(company, StringComparison.OrdinalIgnoreCase) >= 0;
                });
            }

            return filteredResult.ToList();
        }

        public bool HasDraftContractReportFinancialCSDBudgetItem(int contractReportId)
        {
            return (from bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                    where bi.ContractReportId == contractReportId && bi.Status == ContractReportFinancialCSDBudgetItemStatus.Draft
                    select bi.ContractReportFinancialCSDBudgetItemId).Any();
        }

        public IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportFinancialCorrection(
            int contractReportId,
            int contractReportFinancialCorrectionId,
            string csdFilter,
            string company)
        {
            var subquery = from cbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                           where cbi.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId
                           select cbi.ContractReportFinancialCSDBudgetItemId;

            return this.GetContractReportFinancialCSDBudgetItemsInternal(contractReportId, subquery, csdFilter, company);
        }

        public IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportFinancialRevalidation(
            int contractReportId,
            int contractReportFinancialRevalidationId,
            string csdFilter,
            string company)
        {
            var subquery = from cbi in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                           where cbi.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId
                           select cbi.ContractReportFinancialCSDBudgetItemId;

            return this.GetContractReportFinancialCSDBudgetItemsInternal(contractReportId, subquery, csdFilter, company);
        }

        public IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportFinancialCertCorrection(
            int contractReportId,
            int contractReportFinancialCertCorrectionId,
            string csdFilter,
            string company)
        {
            var subquery = from cbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                           where cbi.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId
                           select cbi.ContractReportFinancialCSDBudgetItemId;

            return this.GetContractReportFinancialCSDBudgetItemsInternal(contractReportId, subquery, csdFilter, company);
        }

        public IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportCertAuthorityFinancialCorrection(
            int contractReportId,
            int contractReportCertAuthorityFinancialCorrectionId,
            string csdFilter,
            string company)
        {
            var subquery = from cbi in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>()
                           where cbi.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId
                           select cbi.ContractReportFinancialCSDBudgetItemId;

            return this.GetContractReportFinancialCSDBudgetItemsInternal(contractReportId, subquery, csdFilter, company);
        }

        private IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsInternal(int contractReportId, IQueryable<int> subquery, string csdFilter, string company)
        {
            var results = (from bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           where bi.ContractReportId == contractReportId && !subquery.Contains(bi.ContractReportFinancialCSDBudgetItemId)
                           select new ContractReportFinancialCSDBudgetItemsVO
                           {
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
                               BgAmount = bi.BgAmount,
                               SelfAmount = bi.SelfAmount,
                               TotalAmount = bi.TotalAmount,

                               Status = bi.Status,
                               CostSupportingDocumentApproved = bi.CostSupportingDocumentApproved,

                               ApprovedEuAmount = bi.ApprovedEuAmount,
                               ApprovedBgAmount = bi.ApprovedBgAmount,
                               ApprovedSelfAmount = bi.ApprovedSelfAmount,
                               ApprovedTotalAmount = bi.ApprovedTotalAmount,
                           })
                    .ToList();

            IEnumerable<ContractReportFinancialCSDBudgetItemsVO> filteredResult = results;

            if (!string.IsNullOrEmpty(csdFilter))
            {
                filteredResult = results.Where(t =>
                {
                    var name = string.Format("{0} {1} {2: dd.MM.yyyy}", t.Type.GetEnumDescription(), t.Number, t.Date);
                    return name.IndexOf(csdFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                });
            }

            if (!string.IsNullOrEmpty(company))
            {
                filteredResult = filteredResult.Where(t =>
                {
                    var name = string.Format("({0}){1} {2} {3}", t.CompanyType.GetEnumDescription(), t.CompanyUinType.GetEnumDescription(), t.CompanyUin, t.CompanyName);
                    return name.IndexOf(company, StringComparison.OrdinalIgnoreCase) >= 0;
                });
            }

            return filteredResult.ToList();
        }

        public bool HasCertDraftContractReportFinancialCSDBudgetItems(int certReportId)
        {
            return (from bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                    where bi.CertReportId == certReportId && bi.CertStatus == ContractReportFinancialCSDBudgetItemCertStatus.Draft
                    select bi.ContractReportFinancialCSDBudgetItemId).Any();
        }

        public bool HasCertContractReportFinancialCSDBudgetItems(int certReportId)
        {
            return (from bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                    where bi.CertReportId == certReportId
                    select bi.ContractReportFinancialCSDBudgetItemId).Any();
        }

        public IList<ContractReportFinancialCSDBudgetItemPVO> GetPortalContractReportFinancialCSDBudgetItems(int contractId)
        {
            var financialCorrections =
                from crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crfccsd.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                join crafc in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>() on crfccsd.ContractReportFinancialCorrectionId equals crafc.ContractReportFinancialCorrectionId
                join crfbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crfccsd.ContractReportFinancialCSDBudgetItemId equals crfbi.ContractReportFinancialCSDBudgetItemId
                join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on crfbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                where crfccsd.ContractId == contractId && crfc.Status == ContractReportFinancialCorrectionStatus.Ended
                select new
                {
                    ContractBudgetLevel3AmountGid = cbl3a.Gid,
                    ApprovedEuAmount = (int?)crfccsd.Sign * crfccsd.CorrectedApprovedEuAmount,
                    ApprovedBgAmount = (int?)crfccsd.Sign * crfccsd.CorrectedApprovedBgAmount,
                    ApprovedBfpTotalAmount = (int?)crfccsd.Sign * crfccsd.CorrectedApprovedBfpTotalAmount,
                    ApprovedSelfAmount = (int?)crfccsd.Sign * crfccsd.CorrectedApprovedSelfAmount,
                    ApprovedTotalAmount = (int?)crfccsd.Sign * crfccsd.CorrectedApprovedTotalAmount,
                };

            var predicate = PredicateBuilder.False<ContractReport>()
                .Or(x => x.Status == ContractReportStatus.Accepted);

            var budgetItems =
                from crfbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(x => x.ContractId == contractId)
                join cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(predicate) on crfbi.ContractReportId equals cr.ContractReportId
                join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on crfbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                select new
                {
                    ContractBudgetLevel3AmountGid = cbl3a.Gid,
                    ApprovedEuAmount = crfbi.ApprovedEuAmount,
                    ApprovedBgAmount = crfbi.ApprovedBgAmount,
                    ApprovedBfpTotalAmount = crfbi.ApprovedBfpTotalAmount,
                    ApprovedSelfAmount = crfbi.ApprovedSelfAmount,
                    ApprovedTotalAmount = crfbi.ApprovedTotalAmount,
                };

            var unionResults = budgetItems.Concat(financialCorrections);

            return (from ur in unionResults
                    group new
                    {
                        ApprovedEuAmount = ur.ApprovedEuAmount,
                        ApprovedBgAmount = ur.ApprovedBgAmount,
                        ApprovedBfpTotalAmount = ur.ApprovedBfpTotalAmount,
                        ApprovedSelfAmount = ur.ApprovedSelfAmount,
                        ApprovedTotalAmount = ur.ApprovedTotalAmount,
                    }
                    by new
                    {
                        ContractBudgetLevel3AmountGid = ur.ContractBudgetLevel3AmountGid,
                    }
                    into g
                    select new ContractReportFinancialCSDBudgetItemPVO
                    {
                        ContractBudgetLevel3AmountGid = g.Key.ContractBudgetLevel3AmountGid,

                        ApprovedCumulativeEuAmount = g.Sum(t => t.ApprovedEuAmount),
                        ApprovedCumulativeBgAmount = g.Sum(t => t.ApprovedBgAmount),
                        ApprovedCumulativeBfpTotalAmount = g.Sum(t => t.ApprovedBfpTotalAmount),
                        ApprovedCumulativeSelfAmount = g.Sum(t => t.ApprovedSelfAmount),
                        ApprovedCumulativeTotalAmount = g.Sum(t => t.ApprovedTotalAmount),
                    }).ToList();
        }

        public async Task<IList<ContractReportFinancialCSDBudgetItemPVO>> GetPortalContractReportFinancialCSDBudgetItemsAsync(int contractId, CancellationToken ct)
        {
            var financialCorrections =
                from crfccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                join crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrection>() on crfccsd.ContractReportFinancialCorrectionId equals crfc.ContractReportFinancialCorrectionId
                join crafc in this.unitOfWork.DbContext.Set<ContractReportAttachedFinancialCorrection>() on crfccsd.ContractReportFinancialCorrectionId equals crafc.ContractReportFinancialCorrectionId
                join crfbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crfccsd.ContractReportFinancialCSDBudgetItemId equals crfbi.ContractReportFinancialCSDBudgetItemId
                join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on crfbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                where crfccsd.ContractId == contractId && crfc.Status == ContractReportFinancialCorrectionStatus.Ended
                select new
                {
                    ContractBudgetLevel3AmountGid = cbl3a.Gid,
                    ApprovedEuAmount = (int?)crfccsd.Sign * crfccsd.CorrectedApprovedEuAmount,
                    ApprovedBgAmount = (int?)crfccsd.Sign * crfccsd.CorrectedApprovedBgAmount,
                    ApprovedBfpTotalAmount = (int?)crfccsd.Sign * crfccsd.CorrectedApprovedBfpTotalAmount,
                    ApprovedSelfAmount = (int?)crfccsd.Sign * crfccsd.CorrectedApprovedSelfAmount,
                    ApprovedTotalAmount = (int?)crfccsd.Sign * crfccsd.CorrectedApprovedTotalAmount,
                };

            var predicate = PredicateBuilder.False<ContractReport>()
                .Or(x => x.Status == ContractReportStatus.Accepted);

            var budgetItems =
                from crfbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>().Where(x => x.ContractId == contractId)
                join cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(predicate) on crfbi.ContractReportId equals cr.ContractReportId
                join cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on crfbi.ContractBudgetLevel3AmountId equals cbl3a.ContractBudgetLevel3AmountId
                select new
                {
                    ContractBudgetLevel3AmountGid = cbl3a.Gid,
                    ApprovedEuAmount = crfbi.ApprovedEuAmount,
                    ApprovedBgAmount = crfbi.ApprovedBgAmount,
                    ApprovedBfpTotalAmount = crfbi.ApprovedBfpTotalAmount,
                    ApprovedSelfAmount = crfbi.ApprovedSelfAmount,
                    ApprovedTotalAmount = crfbi.ApprovedTotalAmount,
                };

            var unionResults = budgetItems.Concat(financialCorrections);

            var result = await (from ur in unionResults
                                group new
                                {
                                    ApprovedEuAmount = ur.ApprovedEuAmount,
                                    ApprovedBgAmount = ur.ApprovedBgAmount,
                                    ApprovedBfpTotalAmount = ur.ApprovedBfpTotalAmount,
                                    ApprovedSelfAmount = ur.ApprovedSelfAmount,
                                    ApprovedTotalAmount = ur.ApprovedTotalAmount,
                                }
                                by new
                                {
                                    ContractBudgetLevel3AmountGid = ur.ContractBudgetLevel3AmountGid,
                                }
                                into g
                                select new ContractReportFinancialCSDBudgetItemPVO
                                {
                                    ContractBudgetLevel3AmountGid = g.Key.ContractBudgetLevel3AmountGid,

                                    ApprovedCumulativeEuAmount = g.Sum(t => t.ApprovedEuAmount),
                                    ApprovedCumulativeBgAmount = g.Sum(t => t.ApprovedBgAmount),
                                    ApprovedCumulativeBfpTotalAmount = g.Sum(t => t.ApprovedBfpTotalAmount),
                                    ApprovedCumulativeSelfAmount = g.Sum(t => t.ApprovedSelfAmount),
                                    ApprovedCumulativeTotalAmount = g.Sum(t => t.ApprovedTotalAmount),
                                }).ToListAsync(ct);
            return result;
        }
    }
}
