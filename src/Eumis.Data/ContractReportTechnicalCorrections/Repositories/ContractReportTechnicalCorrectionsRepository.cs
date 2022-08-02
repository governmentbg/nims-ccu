using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Procedures;

namespace Eumis.Data.ContractReportTechnicalCorrections.Repositories
{
    internal class ContractReportTechnicalCorrectionsRepository : AggregateRepository<ContractReportTechnicalCorrection>, IContractReportTechnicalCorrectionsRepository
    {
        public ContractReportTechnicalCorrectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportTechnicalCorrection, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportTechnicalCorrection, object>>[]
                {
                    c => c.File,
                };
            }
        }

        public ContractReportTechnicalCorrection FindEndedContractReportTechnicalCorrection(int contractReportId)
        {
            return this.Set()
                .Where(tc => tc.ContractReportId == contractReportId && tc.Status == ContractReportTechnicalCorrectionStatus.Ended)
                .SingleOrDefault();
        }

        public ContractReportTechnicalCorrection FindLastArchivedContractReportTechnicalCorrection(int contractReportId)
        {
            return this.Set()
                .Where(tc => tc.ContractReportId == contractReportId && tc.Status == ContractReportTechnicalCorrectionStatus.Archived)
                .OrderByDescending(tc => tc.OrderNum)
                .FirstOrDefault();
        }

        public int GetNextOrderNum(int contractId)
        {
            var lastOrderNumber = this.Set()
                .Where(t => t.ContractId == contractId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public int GetContractReportId(int contractReportTechnicalCorrectionId)
        {
            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>()
                    where crfc.ContractReportTechnicalCorrectionId == contractReportTechnicalCorrectionId
                    select crfc.ContractReportId).Single();
        }

        public IList<ContractReportTechnicalCorrectionVO> GetContractReportTechnicalCorrections(
            int[] programmeIds,
            int userId,
            string contractRegNum = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var predicate = PredicateBuilder.True<ContractReportTechnicalCorrection>()
                .AndDateTimeGreaterThanOrEqual(t => t.CreateDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.CreateDate, toDate);

            var contractPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.RegNumber, contractRegNum);

            var contractProgrammePredicate = contractPredicate.And(t => programmeIds.Contains(t.ProgrammeId));

            var externalUserContracts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                        join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on cu.ContractId equals c.ContractId
                                        select c;

            return (from tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>().Where(predicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on tc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractProgrammePredicate).Union(externalUserContracts) on tc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    select new ContractReportTechnicalCorrectionVO()
                    {
                        ContractReportTechnicalCorrectionId = tc.ContractReportTechnicalCorrectionId,
                        ContractReportId = tc.ContractReportId,
                        ContractId = tc.ContractId,
                        OrderNum = tc.OrderNum,
                        Status = tc.Status,
                        Notes = tc.Notes,
                        CreateDate = tc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                        CorrectionDate = tc.CorrectionDate,
                    })
                    .Distinct()
                    .ToList();
        }

        public bool ExistsCorrectionForContractReport(int contractReportId)
        {
            return (from tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>()
                    where tc.ContractReportId == contractReportId
                    select tc.ContractReportTechnicalCorrectionId).Any();
        }

        public bool ExistsDraftCorrectionForContractReport(int contractReportId)
        {
            return (from tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>()
                    where tc.ContractReportId == contractReportId && tc.Status == ContractReportTechnicalCorrectionStatus.Draft
                    select tc.ContractReportTechnicalCorrectionId).Any();
        }

        public bool ExistsEndedCorrectionForContractReport(int contractReportId)
        {
            return (from tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>()
                    where tc.ContractReportId == contractReportId && tc.Status == ContractReportTechnicalCorrectionStatus.Ended
                    select tc.ContractReportTechnicalCorrectionId).Any();
        }

        public IList<ContractReportTechnicalCorrectionVO> GetContractReportTechnicalCorrectionsForProjectDossier(int contractId)
        {
            return (from tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on tc.ContractReportId equals cr.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on tc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where tc.ContractId == contractId && tc.Status != ContractReportTechnicalCorrectionStatus.Draft
                    select new ContractReportTechnicalCorrectionVO()
                    {
                        ContractReportTechnicalCorrectionId = tc.ContractReportTechnicalCorrectionId,
                        ContractReportId = tc.ContractReportId,
                        ContractId = tc.ContractId,
                        OrderNum = tc.OrderNum,
                        Status = tc.Status,
                        Notes = tc.Notes,
                        CreateDate = tc.CreateDate,
                        ContractName = c.Name,
                        ContractRegNum = c.RegNumber,
                        ProcedureName = p.Name,
                        ReportOrderNum = cr.OrderNum,
                        CorrectionDate = tc.CorrectionDate,
                    })
                    .ToList();
        }
    }
}
