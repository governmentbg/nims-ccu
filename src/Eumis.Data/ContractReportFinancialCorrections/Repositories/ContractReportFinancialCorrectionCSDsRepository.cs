using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.ContractReportFinancialCorrections.Repositories
{
    internal class ContractReportFinancialCorrectionCSDsRepository : AggregateRepository<ContractReportFinancialCorrectionCSD>, IContractReportFinancialCorrectionCSDsRepository
    {
        public ContractReportFinancialCorrectionCSDsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportFinancialCorrectionCSD> FindAll(int contractReportFinancialCorrectionId)
        {
            return this.Set().Where(t => t.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId).ToList();
        }

        public IList<ContractReportFinancialCorrectionCSD> FindAll(int contractReportFinancialCorrectionId, int[] contractReportFinancialCorrectionCSDIds)
        {
            return this.Set().Where(t => t.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId &&
                                           contractReportFinancialCorrectionCSDIds.Contains(t.ContractReportFinancialCorrectionCSDId)).ToList();
        }

        public IList<ContractReportFinancialCorrectionCSD> FindAllUnattached(int contractReportFinancialCorrectionId)
        {
            return this.Set().Where(t => t.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId && t.CertReportId == null).ToList();
        }

        public IList<ContractReportFinancialCorrectionCSD> FindAllByCertReport(int certReportId, int contractReportFinancialCorrectionId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId && t.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId).ToList();
        }

        public IList<ContractReportFinancialCorrectionCSD> FindAllByCertReport(int certReportId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId).ToList();
        }

        public IList<ContractReportFinancialCorrectionCSDsVO> GetContractReportFinancialCorrectionCSDs(
            int contractReportFinancialCorrectionId,
            string csdFilter,
            string company,
            bool? isAttachedToCertReport = null,
            int? certReportId = null)
        {
            var predicate = PredicateBuilder.True<ContractReportFinancialCorrectionCSD>();

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

            var results = (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>().Where(predicate)
                           join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           where fccsd.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId
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

            IEnumerable<ContractReportFinancialCorrectionCSDsVO> filteredResult = results;

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

        public bool HasContractReportFinancialCorrectionCSDs(int contractReportFinancialCorrectionId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                    where fccsd.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId
                    select fccsd.ContractReportFinancialCorrectionCSDId).Any();
        }

        public bool HasDraftContractReportFinancialCorrectionCSDs(int contractReportFinancialCorrectionId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                    where fccsd.ContractReportFinancialCorrectionId == contractReportFinancialCorrectionId && fccsd.Status == ContractReportFinancialCorrectionCSDStatus.Draft
                    select fccsd.ContractReportFinancialCorrectionCSDId).Any();
        }

        public bool HasCertDraftContractReportFinancialCorrectionCSDs(int certReportId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                    where fccsd.CertReportId == certReportId && fccsd.CertStatus == ContractReportFinancialCorrectionCSDCertStatus.Draft
                    select fccsd.ContractReportFinancialCorrectionCSDId).Any();
        }

        public bool HasCertContractReportFinancialCorrectionCSDs(int certReportId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCorrectionCSD>()
                    where fccsd.CertReportId == certReportId
                    select fccsd.ContractReportFinancialCorrectionCSDId).Any();
        }
    }
}
