using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories
{
    internal class ContractReportCertAuthorityFinancialCorrectionsRepository : AggregateRepository<ContractReportCertAuthorityFinancialCorrection>, IContractReportCertAuthorityFinancialCorrectionsRepository
    {
        public ContractReportCertAuthorityFinancialCorrectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportCertAuthorityFinancialCorrection, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportCertAuthorityFinancialCorrection, object>>[]
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

        public int GetContractReportId(int contractReportCertAuthorityFinancialCorrectionId)
        {
            return (from crcafc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrection>()
                    where crcafc.ContractReportCertAuthorityFinancialCorrectionId == contractReportCertAuthorityFinancialCorrectionId
                    select crcafc.ContractReportId).Single();
        }

        public IList<ContractReportCertAuthorityFinancialCorrectionVO> GetContractReportCertAuthorityFinancialCorrections(
            int[] programmeIds,
            string contractRegNum = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var predicate = PredicateBuilder.True<ContractReportCertAuthorityFinancialCorrection>()
                .AndDateTimeGreaterThanOrEqual(t => t.CreateDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.CreateDate, toDate);

            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            return (from crcafc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrection>().Where(predicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crcafc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on crcafc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where programmeIds.Contains(c.ProgrammeId)
                    select new ContractReportCertAuthorityFinancialCorrectionVO()
                    {
                        ContractReportCertAuthorityFinancialCorrectionId = crcafc.ContractReportCertAuthorityFinancialCorrectionId,
                        ContractReportId = crcafc.ContractReportId,
                        ContractId = crcafc.ContractId,
                        OrderNum = crcafc.OrderNum,
                        Status = crcafc.Status,
                        Notes = crcafc.Notes,
                        CreateDate = crcafc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                    }).ToList();
        }

        public bool CanCreate(int contractReportId)
        {
            return !(from crcafc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrection>()
                     where crcafc.ContractReportId == contractReportId && crcafc.Status == ContractReportCertAuthorityFinancialCorrectionStatus.Draft
                     select crcafc.ContractReportCertAuthorityFinancialCorrectionId).Any();
        }

        public bool IsIncludedInCertReport(int contractReportCertAuthorityFinancialCorrectionId)
        {
            return false;
        }

        public IList<ContractReportCertAuthorityFinancialCorrectionVO> GetContractReportCertAuthorityFinancialCorrectionsForProjectDossier(int contractId)
        {
            return (from aarcrfcscd in this.unitOfWork.DbContext.Set<AnnualAccountReportCertFinancialCorrectionCSD>()
                    join crcafccsd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrectionCSD>() on aarcrfcscd.ContractReportCertAuthorityFinancialCorrectionCSDId equals crcafccsd.ContractReportCertAuthorityFinancialCorrectionCSDId
                    join bi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>() on crcafccsd.ContractReportFinancialCSDBudgetItemId equals bi.ContractReportFinancialCSDBudgetItemId
                    join crcafc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityFinancialCorrection>() on crcafccsd.ContractReportCertAuthorityFinancialCorrectionId equals crcafc.ContractReportCertAuthorityFinancialCorrectionId
                    join contR in this.unitOfWork.DbContext.Set<ContractReport>() on crcafc.ContractReportId equals contR.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crcafc.ContractId equals c.ContractId
                    join aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>() on aarcrfcscd.AnnualAccountReportId equals aar.AnnualAccountReportId
                    where crcafc.ContractId == contractId && crcafc.Status != ContractReportCertAuthorityFinancialCorrectionStatus.Draft && aar.Status == AnnualAccountReportStatus.Ended
                    group new
                    {
                        CertifiedApprovedBfpTotalAmount = bi.CertifiedApprovedBfpTotalAmount,
                        CertifiedApprovedSelfAmount = bi.CertifiedApprovedSelfAmount,
                    }
                    by new
                    {
                        ContractReportCertAuthorityFinancialCorrectionId = crcafc.ContractReportCertAuthorityFinancialCorrectionId,
                        ContractReportId = crcafc.ContractReportId,
                        ContractId = crcafc.ContractId,
                        ContractRegNum = c.RegNumber,
                        ReportOrderNum = contR.OrderNum,
                        OrderNum = crcafc.OrderNum,
                        Status = crcafc.Status,
                        AnnualAccountReportOrderNum = aar.OrderNum,
                        Notes = crcafc.Notes,
                    }
                    into g
                    select new ContractReportCertAuthorityFinancialCorrectionVO
                    {
                        ContractReportCertAuthorityFinancialCorrectionId = g.Key.ContractReportCertAuthorityFinancialCorrectionId,
                        ContractReportId = g.Key.ContractReportId,
                        ContractId = g.Key.ContractId,
                        ContractRegNum = g.Key.ContractRegNum,
                        ReportOrderNum = g.Key.ReportOrderNum,
                        OrderNum = g.Key.OrderNum,
                        Status = g.Key.Status,
                        CertifiedApprovedBfpTotalAmount = g.Sum(csd => csd.CertifiedApprovedBfpTotalAmount),
                        CertifiedApprovedSelfAmount = g.Sum(csd => csd.CertifiedApprovedSelfAmount),
                        AnnualAccountReportOrderNum = g.Key.AnnualAccountReportOrderNum,
                        Notes = g.Key.Notes,
                    })
                    .ToList();
        }
    }
}
