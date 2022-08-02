using Eumis.Common.Db;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportMicroChecksRepository : AggregateRepository<ContractReportMicroCheck>, IContractReportMicroChecksRepository
    {
        public ContractReportMicroChecksRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportMicroCheck, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportMicroCheck, object>>[]
                {
                    c => c.File,
                };
            }
        }

        public IList<ContractReportMicroCheckVO> GetContractReportMicroChecks(int contractReportId)
        {
            return (from crmc in this.unitOfWork.DbContext.Set<ContractReportMicroCheck>()
                    join crm in this.unitOfWork.DbContext.Set<ContractReportMicro>() on crmc.ContractReportMicroId equals crm.ContractReportMicroId

                    join u in this.unitOfWork.DbContext.Set<User>() on crmc.CheckedByUserId equals u.UserId into g1
                    from u in g1.DefaultIfEmpty()

                    where crmc.ContractReportId == contractReportId
                    orderby crmc.CreateDate descending
                    select new ContractReportMicroCheckVO
                    {
                        ContractReportMicroCheckId = crmc.ContractReportMicroCheckId,
                        ContractReportMicroId = crmc.ContractReportMicroId,
                        ContractReportId = crmc.ContractReportId,
                        ContractId = crmc.ContractId,
                        OrderNum = crmc.OrderNum,
                        Type = crm.Type,
                        Status = crmc.Status,
                        CheckedByUser = u != null ? u.Fullname + " (" + u.Username + ")" : null,
                        CreateDate = crmc.CreateDate,
                        MicroVersionNum = crm.VersionNum,
                        MicroVersionSubNum = crm.VersionSubNum,
                    })
                    .ToList();
        }

        public IList<ContractReportMicroCheck> FindAll(int contractReportId, ContractReportMicroType type)
        {
            return (from mc in this.Set()
                    join m in this.unitOfWork.DbContext.Set<ContractReportMicro>() on mc.ContractReportMicroId equals m.ContractReportMicroId
                    where mc.ContractReportId == contractReportId && m.Type == type
                    select mc).ToList();
        }

        public int GetNextOrderNum(int contractReportMicroId)
        {
            var lastOrderNumber = this.Set()
                .Where(p => p.ContractReportMicroId == contractReportMicroId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public bool HasContractReportMicroCheckInProgress(int contractReportId, ContractReportMicroType type)
        {
            return (from mc in this.unitOfWork.DbContext.Set<ContractReportMicroCheck>()
                    join m in this.unitOfWork.DbContext.Set<ContractReportMicro>() on mc.ContractReportMicroId equals m.ContractReportMicroId
                    where mc.ContractReportId == contractReportId && mc.Status == ContractReportMicroCheckStatus.Draft && m.Type == type
                    select mc.ContractReportMicroCheckId).Any();
        }

        public ContractReportMicroCheck GetActualContractReportMicroCheck(int contractReportId, ContractReportMicroType type)
        {
            return (from mc in this.Set()
                    join m in this.unitOfWork.DbContext.Set<ContractReportMicro>() on mc.ContractReportMicroId equals m.ContractReportMicroId
                    where mc.ContractReportId == contractReportId && m.Type == type && mc.Status == ContractReportMicroCheckStatus.Active
                    select mc).SingleOrDefault();
        }
    }
}
