using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories
{
    internal class ContractReportCertAuthorityFinancialCorrectionCSDsRepository : AggregateRepository<ContractReportCertAuthorityFinancialCorrectionCSD>, IContractReportCertAuthorityFinancialCorrectionCSDsRepository
    {
        public ContractReportCertAuthorityFinancialCorrectionCSDsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportCertAuthorityFinancialCorrectionCSD> FindAll(int contractReportCertAuthorityFinancialCorrectionId)
        {
            return this.Set().Where(t => t.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId).ToList();
        }

        public IList<ContractReportCertAuthorityFinancialCorrectionCSD> FindAll(int contractReportCertAuthorityFinancialCorrectionId, int[] contractReportCertAuthorityFinancialCorrectionCSDIds)
        {
            return this.Set().Where(t => t.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId &&
                                           contractReportCertAuthorityFinancialCorrectionCSDIds.Contains(t.ContractReportCertAuthorityFinancialCorrectionCSDId)).ToList();
        }

        public IList<ContractReportCertAuthorityFinancialCorrectionCSDsVO> GetContractReportCertAuthorityFinancialCorrectionCSDs(
            int contractReportCertAuthorityFinancialCorrectionId,
            string csdFilter,
            string company)
        {
            var results = (from crcafccsd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>()
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

        public bool HasContractReportCertAuthorityFinancialCorrectionCSDs(int contractReportCertAuthorityFinancialCorrectionId)
        {
            return (from crcafccsd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>()
                    where crcafccsd.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId
                    select crcafccsd.ContractReportCertAuthorityFinancialCorrectionCSDId).Any();
        }

        public bool HasDraftContractReportCertAuthorityFinancialCorrectionCSDs(int contractReportCertAuthorityFinancialCorrectionId)
        {
            return (from crcafccsd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>()
                    where crcafccsd.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId && crcafccsd.Status == ContractReportCertAuthorityFinancialCorrectionCSDStatus.Draft
                    select crcafccsd.ContractReportCertAuthorityFinancialCorrectionCSDId).Any();
        }
    }
}
