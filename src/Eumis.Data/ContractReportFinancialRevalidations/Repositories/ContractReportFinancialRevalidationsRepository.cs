using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReportFinancialRevalidations.Repositories
{
    internal class ContractReportFinancialRevalidationsRepository : AggregateRepository<ContractReportFinancialRevalidation>, IContractReportFinancialRevalidationsRepository
    {
        public ContractReportFinancialRevalidationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportFinancialRevalidation, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportFinancialRevalidation, object>>[]
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

        public int GetContractReportId(int contractReportFinancialRevalidationId)
        {
            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidation>()
                    where crfc.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId
                    select crfc.ContractReportId).Single();
        }

        public IList<ContractReportFinancialRevalidationVO> GetContractReportFinancialRevalidations(
            int[] programmeIds,
            string contractRegNum = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var predicate = PredicateBuilder.True<ContractReportFinancialRevalidation>()
                .AndDateTimeGreaterThanOrEqual(t => t.CreateDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.CreateDate, toDate);

            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidation>().Where(predicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where programmeIds.Contains(c.ProgrammeId)
                    select new ContractReportFinancialRevalidationVO()
                    {
                        ContractReportFinancialRevalidationId = crfc.ContractReportFinancialRevalidationId,
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
                    })
                .ToList();
        }

        public bool CanCreate(int contractReportId)
        {
            return !(from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidation>()
                     where crfc.ContractReportId == contractReportId && crfc.Status == ContractReportFinancialRevalidationStatus.Draft
                     select crfc.ContractReportFinancialRevalidationId).Any();
        }

        public bool IsIncludedInCertReport(int contractReportFinancialRevalidationId)
        {
            return (from crfrcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                    where crfrcsd.ContractReportFinancialRevalidationId == contractReportFinancialRevalidationId &&
                          crfrcsd.CertReportId != null
                    select crfrcsd.ContractReportFinancialRevalidationCSDId)
                .Any();
        }

        public IList<ContractReportFinancialRevalidationVO> GetContractReportFinancialRevalidationsForProjectDossier(int contractId)
        {
            var revalidatedAmountsLookup = (
                from crfcsd in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidationCSD>()
                where crfcsd.ContractId == contractId
                group crfcsd by crfcsd.ContractReportFinancialRevalidationId into g
                select new
                {
                    ContractReportFinancialRevalidationId = g.Key,
                    TotalRevalidatedBfpTotalAmount = g.Sum(csd => csd.RevalidatedBfpTotalAmount),
                    TotalRevalidatedSelfAmount = g.Sum(csd => csd.RevalidatedSelfAmount),
                })
                .ToLookup(csd => csd.ContractReportFinancialRevalidationId);

            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialRevalidation>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crfc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crfc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where crfc.ContractId == contractId && crfc.Status != ContractReportFinancialRevalidationStatus.Draft
                    select new
                    {
                        crfc.ContractReportFinancialRevalidationId,
                        crfc.ContractReportId,
                        crfc.ContractId,
                        crfc.OrderNum,
                        crfc.Status,
                        crfc.Notes,
                        crfc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                    })
                    .ToList()
                    .Select(fr => new ContractReportFinancialRevalidationVO
                    {
                        ContractReportFinancialRevalidationId = fr.ContractReportFinancialRevalidationId,
                        ContractReportId = fr.ContractReportId,
                        ContractId = fr.ContractId,
                        OrderNum = fr.OrderNum,
                        Status = fr.Status,
                        Notes = fr.Notes,
                        CreateDate = fr.CreateDate,
                        ContractName = fr.ContractName,
                        ContractRegNum = fr.ContractRegNum,
                        ProcedureName = fr.ProcedureName,
                        ReportOrderNum = fr.ReportOrderNum,
                        TotalRevalidatedBfpTotalAmount = revalidatedAmountsLookup[fr.ContractReportFinancialRevalidationId].First().TotalRevalidatedBfpTotalAmount,
                        TotalRevalidatedSelfAmount = revalidatedAmountsLookup[fr.ContractReportFinancialRevalidationId].First().TotalRevalidatedSelfAmount,
                    })
                    .ToList();
        }
    }
}
