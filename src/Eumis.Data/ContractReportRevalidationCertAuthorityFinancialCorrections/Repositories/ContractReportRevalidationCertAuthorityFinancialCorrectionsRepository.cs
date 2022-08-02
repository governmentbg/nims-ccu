using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories
{
    internal class ContractReportRevalidationCertAuthorityFinancialCorrectionsRepository : AggregateRepository<ContractReportRevalidationCertAuthorityFinancialCorrection>, IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository
    {
        public ContractReportRevalidationCertAuthorityFinancialCorrectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportRevalidationCertAuthorityFinancialCorrection, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportRevalidationCertAuthorityFinancialCorrection, object>>[]
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

        public int GetContractReportId(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            return (from crrcafc in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrection>()
                    where crrcafc.ContractReportRevalidationCertAuthorityFinancialCorrectionId == contractReportRevalidationCertAuthorityFinancialCorrectionId
                    select crrcafc.ContractReportId).Single();
        }

        public IList<ContractReportRevalidationCertAuthorityFinancialCorrectionVO> GetContractReportRevalidationCertAuthorityFinancialCorrections(
            int[] programmeIds,
            string contractRegNum = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var predicate = PredicateBuilder.True<ContractReportRevalidationCertAuthorityFinancialCorrection>()
                .AndDateTimeGreaterThanOrEqual(t => t.CreateDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.CreateDate, toDate);

            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            return (from crrcafc in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrection>().Where(predicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crrcafc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on crrcafc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where programmeIds.Contains(c.ProgrammeId)
                    select new ContractReportRevalidationCertAuthorityFinancialCorrectionVO()
                    {
                        ContractReportRevalidationCertAuthorityFinancialCorrectionId = crrcafc.ContractReportRevalidationCertAuthorityFinancialCorrectionId,
                        ContractReportId = crrcafc.ContractReportId,
                        ContractId = crrcafc.ContractId,
                        OrderNum = crrcafc.OrderNum,
                        Status = crrcafc.Status,
                        Notes = crrcafc.Notes,
                        CreateDate = crrcafc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                    }).ToList();
        }

        public bool CanCreate(int contractReportId)
        {
            return !(from crrcafc in this.unitOfWork.DbContext.Set<ContractReportRevalidationCertAuthorityFinancialCorrection>()
                     where crrcafc.ContractReportId == contractReportId && crrcafc.Status == ContractReportRevalidationCertAuthorityFinancialCorrectionStatus.Draft
                     select crrcafc.ContractReportRevalidationCertAuthorityFinancialCorrectionId).Any();
        }

        public bool IsIncludedInCertReport(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            return false;
        }
    }
}
