using Eumis.Common.Db;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportFinancialChecksRepository : AggregateRepository<ContractReportFinancialCheck>, IContractReportFinancialChecksRepository
    {
        public ContractReportFinancialChecksRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportFinancialCheck, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportFinancialCheck, object>>[]
                {
                    c => c.File,
                };
            }
        }

        public IList<ContractReportFinancialCheckVO> GetContractReportFinancialChecks(int contractReportId)
        {
            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCheck>()
                    join crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>() on crfc.ContractReportFinancialId equals crf.ContractReportFinancialId

                    join u in this.unitOfWork.DbContext.Set<User>() on crfc.CheckedByUserId equals u.UserId into g1
                    from u in g1.DefaultIfEmpty()

                    where crfc.ContractReportId == contractReportId
                    orderby crfc.CreateDate descending
                    select new ContractReportFinancialCheckVO
                    {
                        ContractReportFinancialCheckId = crfc.ContractReportFinancialCheckId,
                        ContractReportFinancialId = crfc.ContractReportFinancialId,
                        ContractReportId = crfc.ContractReportId,
                        ContractId = crfc.ContractId,
                        OrderNum = crfc.OrderNum,
                        Status = crfc.Status,
                        CheckedByUser = u != null ? u.Fullname + " (" + u.Username + ")" : null,
                        CreateDate = crfc.CreateDate,
                        FinancialVersionNum = crf.VersionNum,
                        FinancialVersionSubNum = crf.VersionSubNum,
                    })
                    .ToList();
        }

        public IList<ContractReportFinancialCheck> FindAll(int contractReportId)
        {
            return this.Set()
                .Where(t => t.ContractReportId == contractReportId)
                .ToList();
        }

        public async Task<IList<ContractReportFinancialCheck>> FindAllAsync(int contractReportId, CancellationToken ct)
        {
            var result = await this.Set()
                .Where(t => t.ContractReportId == contractReportId)
                .ToListAsync(ct);

            return result;
        }

        public int GetNextOrderNum(int contractReportFinancialId)
        {
            var lastOrderNumber = this.Set()
                .Where(p => p.ContractReportFinancialId == contractReportFinancialId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public bool HasContractReportFinancialCheckInProgress(int contractReportId)
        {
            return (from crfc in this.unitOfWork.DbContext.Set<ContractReportFinancialCheck>()
                    where crfc.ContractReportId == contractReportId && crfc.Status == ContractReportFinancialCheckStatus.Draft
                    select crfc.ContractReportFinancialCheckId).Any();
        }

        public ContractReportFinancialCheck GetActualContractReportFinancialCheck(int contractReportId)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId && t.Status == ContractReportFinancialCheckStatus.Active).SingleOrDefault();
        }
    }
}
