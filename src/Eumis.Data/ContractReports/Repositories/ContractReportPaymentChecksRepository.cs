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
    internal class ContractReportPaymentChecksRepository : AggregateRepository<ContractReportPaymentCheck>, IContractReportPaymentChecksRepository
    {
        public ContractReportPaymentChecksRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportPaymentCheck, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportPaymentCheck, object>>[]
                {
                    c => c.ContractReportPaymentCheckAmounts,
                    c => c.File,
                };
            }
        }

        public IList<ContractReportPaymentCheckVO> GetContractReportPaymentChecks(int contractReportId)
        {
            return (from crpc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>()
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crpc.ContractReportPaymentId equals crp.ContractReportPaymentId

                    join u in this.unitOfWork.DbContext.Set<User>() on crpc.CheckedByUserId equals u.UserId into g1
                    from u in g1.DefaultIfEmpty()

                    where crpc.ContractReportId == contractReportId
                    orderby crpc.CreateDate descending
                    select new ContractReportPaymentCheckVO
                    {
                        ContractReportPaymentCheckId = crpc.ContractReportPaymentCheckId,
                        ContractReportPaymentId = crpc.ContractReportPaymentId,
                        ContractReportId = crpc.ContractReportId,
                        ContractId = crpc.ContractId,
                        OrderNum = crpc.OrderNum,
                        Status = crpc.Status,
                        CheckedByUser = u != null ? u.Fullname + " (" + u.Username + ")" : null,
                        CreateDate = crpc.CreateDate,
                        PaymentVersionNum = crp.VersionNum,
                        PaymentVersionSubNum = crp.VersionSubNum,
                    })
                    .ToList();
        }

        public IList<ContractReportPaymentCheck> FindAll(int contractReportId)
        {
            return this.Set()
                .Where(t => t.ContractReportId == contractReportId)
                .ToList();
        }

        public async Task<IList<ContractReportPaymentCheck>> FindAllAsync(int contractReportId, CancellationToken ct)
        {
            var result = await this.Set()
                .Where(t => t.ContractReportId == contractReportId)
                .ToListAsync(ct);

            return result;
        }

        public int GetNextOrderNum(int contractReportPaymentId)
        {
            var lastOrderNumber = this.Set()
                .Where(p => p.ContractReportPaymentId == contractReportPaymentId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public bool HasContractReportPaymentCheckInProgress(int contractReportId)
        {
            return (from crpc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>()
                    where crpc.ContractReportId == contractReportId && crpc.Status == ContractReportPaymentCheckStatus.Draft
                    select crpc.ContractReportPaymentCheckId).Any();
        }

        public ContractReportPaymentCheck GetActualContractReportPaymentCheck(int contractReportId)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId && t.Status == ContractReportPaymentCheckStatus.Active).SingleOrDefault();
        }
    }
}
