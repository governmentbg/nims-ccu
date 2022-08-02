using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.ContractReportFinancialRevalidations.Repositories
{
    internal class ContractReportFinancialRevalidationCSDsRepository : AggregateRepository<ContractReportFinancialRevalidationCSD>, IContractReportFinancialRevalidationCSDsRepository
    {
        public ContractReportFinancialRevalidationCSDsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportFinancialRevalidationCSD> FindAll(int contractReportFinancialRevalidationId)
        {
            return this.Set().Where(t => t.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId).ToList();
        }

        public IList<ContractReportFinancialRevalidationCSD> FindAll(int contractReportFinancialRevalidationId, int[] contractReportFinancialRevalidationCSDIds)
        {
            return this.Set().Where(t => t.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId &&
                                           contractReportFinancialRevalidationCSDIds.Contains(t.ContractReportFinancialRevalidationCSDId)).ToList();
        }

        public IList<ContractReportFinancialRevalidationCSD> FindAllUnattached(int contractReportFinancialRevalidationId)
        {
            return this.Set().Where(t => t.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId && t.CertReportId == null).ToList();
        }

        public IList<ContractReportFinancialRevalidationCSD> FindAllByCertReport(int certReportId, int contractReportFinancialRevalidationId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId && t.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId).ToList();
        }

        public IList<ContractReportFinancialRevalidationCSD> FindAllByCertReport(int certReportId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId).ToList();
        }

        public IList<ContractReportFinancialRevalidationCSDsVO> GetContractReportFinancialRevalidationCSDs(
            int contractReportFinancialRevalidationId,
            string csdFilter,
            string company,
            bool? isAttachedToCertReport = null,
            int? certReportId = null)
        {
            var predicate = PredicateBuilder.True<ContractReportFinancialRevalidationCSD>();

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

            var results = (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>().Where(predicate)
                           join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           where fccsd.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId
                           select new ContractReportFinancialRevalidationCSDsVO
                           {
                               ContractReportFinancialRevalidationCSDId = fccsd.ContractReportFinancialRevalidationCSDId,
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

                               RevalidatedEuAmount = fccsd.RevalidatedEuAmount,
                               RevalidatedBgAmount = fccsd.RevalidatedBgAmount,
                               RevalidatedSelfAmount = fccsd.RevalidatedSelfAmount,
                               RevalidatedTotalAmount = fccsd.RevalidatedTotalAmount,

                               CertStatus = fccsd.CertStatus,
                               CertifiedRevalidatedEuAmount = fccsd.CertifiedRevalidatedEuAmount,
                               CertifiedRevalidatedBgAmount = fccsd.CertifiedRevalidatedBgAmount,
                               CertifiedRevalidatedSelfAmount = fccsd.CertifiedRevalidatedSelfAmount,
                               CertifiedRevalidatedTotalAmount = fccsd.CertifiedRevalidatedTotalAmount,
                               Version = fccsd.Version,
                           })
                    .ToList();

            IEnumerable<ContractReportFinancialRevalidationCSDsVO> filteredResult = results;

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

        public IList<ContractReportFinancialRevalidationCSDsVO> GetContractReportFinancialRevalidationCSDsForContractReportRevalidationCertAuthorityFinancialCorrection(
            int contractReportId,
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            string csdFilter,
            string company)
        {
            var subquery = from cbi in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>()
                           where cbi.ContractReportRevalidationCertAuthorityFinancialCorrectionId == contractReportRevalidationCertAuthorityFinancialCorrectionId
                           select cbi.ContractReportFinancialCSDBudgetItemId;

            var results = (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                           join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           where bi.ContractReportId == contractReportId && !subquery.Contains(bi.ContractReportFinancialCSDBudgetItemId)
                           select new ContractReportFinancialRevalidationCSDsVO
                           {
                               ContractReportFinancialRevalidationCSDId = fccsd.ContractReportFinancialRevalidationCSDId,
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

                               RevalidatedEuAmount = fccsd.RevalidatedEuAmount,
                               RevalidatedBgAmount = fccsd.RevalidatedBgAmount,
                               RevalidatedSelfAmount = fccsd.RevalidatedSelfAmount,
                               RevalidatedTotalAmount = fccsd.RevalidatedTotalAmount,

                               CertStatus = fccsd.CertStatus,
                               CertifiedRevalidatedEuAmount = fccsd.CertifiedRevalidatedEuAmount,
                               CertifiedRevalidatedBgAmount = fccsd.CertifiedRevalidatedBgAmount,
                               CertifiedRevalidatedSelfAmount = fccsd.CertifiedRevalidatedSelfAmount,
                               CertifiedRevalidatedTotalAmount = fccsd.CertifiedRevalidatedTotalAmount,
                               Version = fccsd.Version,
                           })
                    .ToList();

            IEnumerable<ContractReportFinancialRevalidationCSDsVO> filteredResult = results;

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

        public ContractReportFinancialRevalidationCSD GetContractReportFinancialRevalidationCSDByBudgetItem(int contractReportId, int contractReportFinancialCSDBudgetItemId)
        {
            return this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                .Where(t => t.ContractReportId == contractReportId)
                .Where(t => t.ContractReportFinancialCSDBudgetItemId == contractReportFinancialCSDBudgetItemId)
                .Single();
        }

        public bool HasContractReportFinancialRevalidationCSDs(int contractReportFinancialRevalidationId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                    where fccsd.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId
                    select fccsd.ContractReportFinancialRevalidationCSDId).Any();
        }

        public bool HasDraftContractReportFinancialRevalidationCSDs(int contractReportFinancialRevalidationId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                    where fccsd.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId && fccsd.Status == ContractReportFinancialRevalidationCSDStatus.Draft
                    select fccsd.ContractReportFinancialRevalidationCSDId).Any();
        }

        public bool HasCertDraftContractReportFinancialRevalidationCSDs(int certReportId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                    where fccsd.CertReportId == certReportId && fccsd.CertStatus == ContractReportFinancialRevalidationCSDCertStatus.Draft
                    select fccsd.ContractReportFinancialRevalidationCSDId).Any();
        }

        public bool HasCertContractReportFinancialRevalidationCSDs(int certReportId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                    where fccsd.CertReportId == certReportId
                    select fccsd.ContractReportFinancialRevalidationCSDId).Any();
        }
    }
}
