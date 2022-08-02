using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.ContractReportFinancialCertCorrections.Repositories
{
    internal class ContractReportFinancialCertCorrectionCSDsRepository : AggregateRepository<ContractReportFinancialCertCorrectionCSD>, IContractReportFinancialCertCorrectionCSDsRepository
    {
        public ContractReportFinancialCertCorrectionCSDsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportFinancialCertCorrectionCSD> FindAll(int contractReportFinancialCertCorrectionId)
        {
            return this.Set().Where(t => t.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId).ToList();
        }

        public IList<ContractReportFinancialCertCorrectionCSD> FindAll(int contractReportFinancialCertCorrectionId, int[] contractReportFinancialCertCorrectionCSDIds)
        {
            return this.Set().Where(t => t.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId &&
                                           contractReportFinancialCertCorrectionCSDIds.Contains(t.ContractReportFinancialCertCorrectionCSDId)).ToList();
        }

        public IList<ContractReportFinancialCertCorrectionCSD> FindAllUnattached(int contractReportFinancialCertCorrectionId)
        {
            return this.Set().Where(t => t.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId && t.CertReportId == null).ToList();
        }

        public IList<ContractReportFinancialCertCorrectionCSD> FindAllByCertReport(int certReportId, int contractReportFinancialCertCorrectionId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId && t.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId).ToList();
        }

        public IList<ContractReportFinancialCertCorrectionCSDsVO> GetContractReportFinancialCertCorrectionCSDs(
            int contractReportFinancialCertCorrectionId,
            string csdFilter,
            string company,
            bool? isAttachedToCertReport = null,
            int? certReportId = null)
        {
            var predicate = PredicateBuilder.True<ContractReportFinancialCertCorrectionCSD>();

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

            var results = (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>().Where(predicate)
                           join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on fccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                           join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on bi.ContractReportFinancialCSDId equals csd.ContractReportFinancialCSDId
                           where fccsd.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId
                           select new ContractReportFinancialCertCorrectionCSDsVO
                           {
                               ContractReportFinancialCertCorrectionCSDId = fccsd.ContractReportFinancialCertCorrectionCSDId,
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

                               CorrectedCertifiedBgAmount = (int)fccsd.Sign * fccsd.CertifiedBgAmount,
                               CorrectedCertifiedEuAmount = (int)fccsd.Sign * fccsd.CertifiedEuAmount,
                               CorrectedCertifiedSelfAmount = (int)fccsd.Sign * fccsd.CertifiedSelfAmount,
                               CorrectedCertifiedTotalAmount = (int)fccsd.Sign * fccsd.CertifiedTotalAmount,

                               CertifiedEuAmount = bi.CertifiedApprovedEuAmount,
                               CertifiedBgAmount = bi.CertifiedApprovedBgAmount,
                               CertifiedSelfAmount = bi.CertifiedApprovedSelfAmount,
                               CertifiedTotalAmount = bi.CertifiedApprovedTotalAmount,
                               Version = fccsd.Version,
                           })
                    .ToList();

            IEnumerable<ContractReportFinancialCertCorrectionCSDsVO> filteredResult = results;

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

        public bool HasContractReportFinancialCertCorrectionCSDs(int contractReportFinancialCertCorrectionId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                    where fccsd.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId
                    select fccsd.ContractReportFinancialCertCorrectionCSDId).Any();
        }

        public bool HasDraftContractReportFinancialCertCorrectionCSDs(int contractReportFinancialCertCorrectionId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                    where fccsd.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId && fccsd.Status == ContractReportFinancialCertCorrectionCSDStatus.Draft
                    select fccsd.ContractReportFinancialCertCorrectionCSDId).Any();
        }

        public bool HasCertContractReportFinancialCertCorrectionCSDs(int certReportId)
        {
            return (from fccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                    where fccsd.CertReportId == certReportId
                    select fccsd.ContractReportFinancialCertCorrectionCSDId).Any();
        }
    }
}
