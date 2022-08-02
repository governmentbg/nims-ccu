using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories
{
    internal class ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository : AggregateRepository<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>, IContractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository
    {
        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO> GetContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            string csdFilter,
            string company)
        {
            var results = (from crcafccsd in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>()
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
                           })
                    .ToList();

            IEnumerable<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO> filteredResult = results;

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

        public bool HasContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            return (from crrcafccsd in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>()
                    where crrcafccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionId == contractReportRevalidationCertAuthorityFinancialCorrectionId
                    select crrcafccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId).Any();
        }

        public bool HasDraftContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            return (from crrcafccsd in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>()
                    where crrcafccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionId == contractReportRevalidationCertAuthorityFinancialCorrectionId && crrcafccsd.Status == ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Draft
                    select crrcafccsd.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId).Any();
        }
    }
}
