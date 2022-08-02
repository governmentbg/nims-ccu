using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportFinancialsRepository : AggregateRepository<ContractReportFinancial>, IContractReportFinancialsRepository
    {
        public ContractReportFinancialsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportFinancial, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportFinancial, object>>[]
                {
                    c => c.Files,
                };
            }
        }

        public IList<ContractReportFinancial> FindAll(int contractReportId)
        {
            // Load the related entities first and than load the principal entity
            // without using EF's "Include" feature so that we can split the loading
            // into two queries and avoid downloading an expaneded result set
            // with multiple duplicated columns
            (from crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>()
             join crff in this.unitOfWork.DbContext.Set<ContractReportFinancialFile>() on crf.ContractReportFinancialId equals crff.ContractReportFinancialId
             where crf.ContractReportId == contractReportId
             select crff).Load();

            return this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                .Where(p => p.ContractReportId == contractReportId)
                .ToList();
        }

        public async Task<IList<ContractReportFinancial>> FindAllAsync(int contractReportId, CancellationToken ct)
        {
            // Load the related entities first and than load the principal entity
            // without using EF's "Include" feature so that we can split the loading
            // into two queries and avoid downloading an expaneded result set
            // with multiple duplicated columns
            await (from crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                   join crff in this.unitOfWork.DbContext.Set<ContractReportFinancialFile>() on crf.ContractReportFinancialId equals crff.ContractReportFinancialId
                   where crf.ContractReportId == contractReportId
                   select crff).LoadAsync(ct);

            var result = await this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                .Where(p => p.ContractReportId == contractReportId)
                .ToListAsync(ct);

            return result;
        }

        public ContractReportFinancial Find(Guid gid)
        {
            return this.Set()
                .Where(p => p.Gid == gid)
                .Single();
        }

        public async Task<ContractReportFinancial> FindAsync(Guid gid, CancellationToken ct)
        {
            return await this.Set()
                .Where(p => p.Gid == gid)
                .SingleAsync(ct);
        }

        public ContractReportFinancial GetActualContractReportFinancial(int contractReportId)
        {
            return this.Set()
                .Where(p => p.ContractReportId == contractReportId && p.Status == ContractReportFinancialStatus.Actual)
                .SingleOrDefault();
        }

        public async Task<ContractReportFinancial> GetActualContractReportFinancialAsync(int contractReportId, CancellationToken ct)
        {
            var result = await this.Set()
                .Where(p => p.ContractReportId == contractReportId && p.Status == ContractReportFinancialStatus.Actual)
                .SingleOrDefaultAsync(ct);

            return result;
        }

        public async Task<ContractReportFinancial> GetLastContractReportFinancialAsync(Guid contractReportGid, CancellationToken ct)
        {
            var result = await (from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(x => x.Gid == contractReportGid)
                                join crf in this.Set() on cr.ContractReportId equals crf.ContractReportId
                                select crf)
                                .OrderByDescending(x => x.CreateDate)
                                .FirstOrDefaultAsync(ct);

            return result;
        }

        public int GetNextVersionNum(int contractId)
        {
            var lastVersionNumber = this.Set()
                .Where(t => t.ContractId == contractId)
                .Max(p => (int?)p.VersionNum);

            return lastVersionNumber.HasValue ? lastVersionNumber.Value + 1 : 1;
        }

        public async Task<int> GetNextVersionNumAsync(int contractId, CancellationToken ct)
        {
            var lastVersionNumber = await this.Set()
                .Where(t => t.ContractId == contractId)
                .MaxAsync(p => (int?)p.VersionNum, ct);

            return lastVersionNumber.HasValue ? lastVersionNumber.Value + 1 : 1;
        }

        public int GetNextVersionSubNum(int contractReportId)
        {
            var lastVersionSubNumber = this.Set()
                .Where(p => p.ContractReportId == contractReportId)
                .Max(p => (int?)p.VersionSubNum);

            return lastVersionSubNumber.HasValue ? lastVersionSubNumber.Value + 1 : 1;
        }

        public async Task<int> GetNextVersionSubNumAsync(int contractReportId, CancellationToken ct)
        {
            var lastVersionSubNumber = await this.Set()
                .Where(p => p.ContractReportId == contractReportId)
                .MaxAsync(p => (int?)p.VersionSubNum, ct);

            return lastVersionSubNumber.HasValue ? lastVersionSubNumber.Value + 1 : 1;
        }

        public IList<ContractReportFinancialVO> GetContractReportFinancials(int contractReportId)
        {
            return (from crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>()
                    where crf.ContractReportId == contractReportId
                    join creg in this.unitOfWork.DbContext.Set<ContractRegistration>() on crf.SenderContractRegistrationId equals creg.ContractRegistrationId into cregs
                    from creg in cregs.DefaultIfEmpty()

                    let nextVersionSubNum = crf.VersionSubNum + 1

                    join crfNext in this.unitOfWork.DbContext.Set<ContractReportFinancial>() on new { crf.ContractReportId, VersionSubNum = nextVersionSubNum } equals new { crfNext.ContractReportId, crfNext.VersionSubNum } into gcrfNext
                    from crfNext in gcrfNext.DefaultIfEmpty()

                    orderby crf.CreateDate descending
                    select new ContractReportFinancialVO
                    {
                        ContractReportFinancialId = crf.ContractReportFinancialId,
                        ContractReportId = crf.ContractReportId,
                        ContractId = crf.ContractId,
                        VersionNum = crf.VersionNum,
                        VersionSubNum = crf.VersionSubNum,
                        Status = crf.Status,
                        StatusName = crf.Status,
                        StatusNote = crf.StatusNote,
                        StartDate = crf.StartDate,
                        EndDate = crf.EndDate,
                        SubmitDate = crf.SubmitDate,
                        CreateDate = crf.CreateDate,
                        ReturnDate = crfNext.CreateDate,
                        ContractRegistrationEmail = creg.Email,
                    }).ToList();
        }

        public ContractReportFinancial GetLastContractReportFinancial(int contractId)
        {
            var contractFinancialPredicate = PredicateBuilder.True<ContractReportFinancial>()
                .And(x => x.ContractId == contractId);

            var contractRerportPredicate = PredicateBuilder.False<ContractReport>()
                .Or(x => x.Status == ContractReportStatus.Accepted);

            return (from crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>().Where(contractFinancialPredicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(contractRerportPredicate) on crf.ContractReportId equals cr.ContractReportId
                    orderby new { crf.VersionNum, crf.VersionSubNum } descending
                    select crf)
                .FirstOrDefault();
        }

        public async Task<ContractReportFinancial> GetLastContractReportFinancialAsync(int contractId, CancellationToken ct)
        {
            var contractFinancialPredicate = PredicateBuilder.True<ContractReportFinancial>()
                .And(x => x.ContractId == contractId);

            var contractReportPredicate = PredicateBuilder.False<ContractReport>()
                .Or(x => x.Status == ContractReportStatus.Accepted);

            var result = await (from crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>().Where(contractFinancialPredicate)
                                join cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(contractReportPredicate) on crf.ContractReportId equals cr.ContractReportId
                                orderby new { crf.VersionNum, crf.VersionSubNum } descending
                                select crf)
                                .FirstOrDefaultAsync(ct);

            return result;
        }
    }
}
