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
    internal class ContractReportTechnicalsRepository : AggregateRepository<ContractReportTechnical>, IContractReportTechnicalsRepository
    {
        public ContractReportTechnicalsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportTechnical, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportTechnical, object>>[]
                {
                    c => c.Files,
                };
            }
        }

        public IList<ContractReportTechnical> FindAll(int contractReportId)
        {
            // Load the related entities first and than load the principal entity
            // without using EF's "Include" feature so that we can split the loading
            // into two queries and avoid downloading an expaneded result set
            // with multiple duplicated columns
            (from crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>()
             join crtf in this.unitOfWork.DbContext.Set<ContractReportTechnicalFile>() on crt.ContractReportTechnicalId equals crtf.ContractReportTechnicalId
             where crt.ContractReportId == contractReportId
             select crtf).Load();

            return this.unitOfWork.DbContext.Set<ContractReportTechnical>()
                .Where(p => p.ContractReportId == contractReportId)
                .ToList();
        }

        public async Task<IList<ContractReportTechnical>> FindAllAsync(int contractReportId, CancellationToken ct)
        {
            // Load the related entities first and than load the principal entity
            // without using EF's "Include" feature so that we can split the loading
            // into two queries and avoid downloading an expaneded result set
            // with multiple duplicated columns
            await (from crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>()
                   join crtf in this.unitOfWork.DbContext.Set<ContractReportTechnicalFile>() on crt.ContractReportTechnicalId equals crtf.ContractReportTechnicalId
                   where crt.ContractReportId == contractReportId
                   select crtf).LoadAsync(ct);

            return await this.unitOfWork.DbContext.Set<ContractReportTechnical>()
                .Where(p => p.ContractReportId == contractReportId)
                .ToListAsync(ct);
        }

        public ContractReportTechnical Find(Guid gid)
        {
            return this.Set()
                .Where(p => p.Gid == gid)
                .Single();
        }

        public async Task<ContractReportTechnical> FindAsync(Guid gid, CancellationToken ct)
        {
            return await this.Set()
                .Where(p => p.Gid == gid)
                .SingleAsync(ct);
        }

        public ContractReportTechnical GetActualContractReportTechnical(int contractReportId)
        {
            return this.Set()
                .Where(p => p.ContractReportId == contractReportId && p.Status == ContractReportTechnicalStatus.Actual)
                .SingleOrDefault();
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

        public IList<ContractReportTechnicalVO> GetContractReportTechnicals(int contractReportId)
        {
            return (from crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>()
                    where crt.ContractReportId == contractReportId
                    join creg in this.unitOfWork.DbContext.Set<ContractRegistration>() on crt.SenderContractRegistrationId equals creg.ContractRegistrationId into cregs
                    from creg in cregs.DefaultIfEmpty()

                    let nextVersionSubNum = crt.VersionSubNum + 1

                    join crtNext in this.unitOfWork.DbContext.Set<ContractReportTechnical>() on new { crt.ContractReportId, VersionSubNum = nextVersionSubNum } equals new { crtNext.ContractReportId, crtNext.VersionSubNum } into gcrt
                    from crtNext in gcrt.DefaultIfEmpty()

                    orderby crt.CreateDate descending
                    select new ContractReportTechnicalVO
                    {
                        ContractReportTechnicalId = crt.ContractReportTechnicalId,
                        ContractReportId = crt.ContractReportId,
                        ContractId = crt.ContractId,
                        VersionNum = crt.VersionNum,
                        VersionSubNum = crt.VersionSubNum,
                        StatusName = crt.Status,
                        Status = crt.Status,
                        StatusNote = crt.StatusNote,
                        Type = crt.Type,
                        RegDate = crt.RegDate,
                        SubmitDate = crt.SubmitDate,
                        DateFrom = crt.DateFrom,
                        DateTo = crt.DateTo,
                        CreateDate = crt.CreateDate,
                        ReturnDate = crtNext.CreateDate,
                        ContractRegistrationId = creg.ContractRegistrationId,
                        ContractRegistrationEmail = creg.Email,
                    }).ToList();
        }

        public ContractReportTechnical GetLastContractReportTechnical(int contractId)
        {
            var contractTechnicalPredicate = PredicateBuilder.True<ContractReportTechnical>()
                .And(x => x.ContractId == contractId);

            var contractRerportPredicate = PredicateBuilder.False<ContractReport>()
                .Or(x => x.Status == ContractReportStatus.Accepted);

            return (from crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>().Where(contractTechnicalPredicate)
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(contractRerportPredicate) on crt.ContractReportId equals cr.ContractReportId
                    orderby new { crt.VersionNum, crt.VersionSubNum } descending
                    select crt)
                .FirstOrDefault();
        }

        public async Task<ContractReportTechnical> GetLastContractReportTechnicalAsync(int contractId, CancellationToken ct)
        {
            var contractTechnicalPredicate = PredicateBuilder.True<ContractReportTechnical>()
                .And(x => x.ContractId == contractId);

            var contractRerportPredicate = PredicateBuilder.False<ContractReport>()
                .Or(x => x.Status == ContractReportStatus.Accepted);

            var result = (from crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>().Where(contractTechnicalPredicate)
                          join cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(contractRerportPredicate) on crt.ContractReportId equals cr.ContractReportId
                          orderby new { crt.VersionNum, crt.VersionSubNum } descending
                          select crt)
                          .FirstOrDefaultAsync(ct);

            return await result;
        }
    }
}
