using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReportFinancialCertCorrections.Repositories
{
    internal class ContractReportFinancialCertCorrectionsRepository : AggregateRepository<ContractReportFinancialCertCorrection>, IContractReportFinancialCertCorrectionsRepository
    {
        public ContractReportFinancialCertCorrectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportFinancialCertCorrection, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportFinancialCertCorrection, object>>[]
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

        public int GetContractReportId(int contractReportFinancialCertCorrectionId)
        {
            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrection>()
                    where crfc.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId
                    select crfc.ContractReportId).Single();
        }

        public IList<ContractReportFinancialCertCorrectionVO> GetContractReportFinancialCertCorrections(
            int[] programmeIds,
            string contractRegNum = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var predicate = PredicateBuilder.True<ContractReportFinancialCertCorrection>()
                .AndDateTimeGreaterThanOrEqual(t => t.CreateDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.CreateDate, toDate);

            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrection>().Where(predicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where programmeIds.Contains(c.ProgrammeId)
                    select new ContractReportFinancialCertCorrectionVO()
                    {
                        ContractReportFinancialCertCorrectionId = crfc.ContractReportFinancialCertCorrectionId,
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
                    }).ToList();
        }

        public bool CanCreate(int contractReportId)
        {
            return !(from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrection>()
                     where crfc.ContractReportId == contractReportId && crfc.Status == ContractReportFinancialCertCorrectionStatus.Draft
                     select crfc.ContractReportFinancialCertCorrectionId).Any();
        }

        public bool IsIncludedInCertReport(int contractReportFinancialCertCorrectionId)
        {
            return (from crfcccsd in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrectionCSD>()
                    where crfcccsd.ContractReportFinancialCertCorrectionId == contractReportFinancialCertCorrectionId &&
                          crfcccsd.CertReportId != null
                    select crfcccsd.ContractReportFinancialCertCorrectionCSDId)
                .Any();
        }

        public IList<ContractReportFinancialCertCorrectionVO> GetContractReportFinancialCertCorrectionsForProjectDossier(int contractId)
        {
            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCertCorrection>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where crfc.ContractId == contractId && crfc.Status != ContractReportFinancialCertCorrectionStatus.Draft
                    select new ContractReportFinancialCertCorrectionVO()
                    {
                        ContractReportFinancialCertCorrectionId = crfc.ContractReportFinancialCertCorrectionId,
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
                    }).ToList();
        }
    }
}
