using Eumis.Common.Db;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReportAdvanceNVPaymentAmounts.Repositories
{
    internal class ContractReportAdvanceNVPaymentAmountsRepository : AggregateRepository<ContractReportAdvanceNVPaymentAmount>, IContractReportAdvanceNVPaymentAmountsRepository
    {
        public ContractReportAdvanceNVPaymentAmountsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportAdvanceNVPaymentAmount> FindAll(int contractReportId)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId).ToList();
        }

        public IList<ContractReportAdvanceNVPaymentAmount> FindAll(int contractReportId, int[] contractReportAdvanceNVPaymentAmountIds)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId && contractReportAdvanceNVPaymentAmountIds.Contains(t.ContractReportAdvanceNVPaymentAmountId)).ToList();
        }

        public IList<ContractReportAdvanceNVPaymentAmountsVO> GetContractReportAdvanceNVPaymentAmounts(int contractReportId)
        {
            return (from crapa in this.unitOfWork.DbContext.Set<ContractReportAdvanceNVPaymentAmount>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on crapa.ProgrammePriorityId equals pp.MapNodeId
                    where crapa.ContractReportId == contractReportId
                    select new ContractReportAdvanceNVPaymentAmountsVO()
                    {
                        ContractReportAdvanceNVPaymentAmountId = crapa.ContractReportAdvanceNVPaymentAmountId,
                        ContractReportPaymentId = crapa.ContractReportPaymentId,
                        ContractReportId = crapa.ContractReportId,
                        ContractId = crapa.ContractId,
                        Gid = crapa.Gid,

                        Status = crapa.Status,
                        Approval = crapa.Approval,
                        Notes = crapa.Notes,
                        CheckedByUserId = crapa.CheckedByUserId,
                        CheckedDate = crapa.CheckedDate,

                        ProgrammePriorityName = pp.Name,
                        ProgrammePriorityId = crapa.ProgrammePriorityId,
                        EuAmount = crapa.EuAmount,
                        BgAmount = crapa.BgAmount,
                        BfpTotalAmount = crapa.BfpTotalAmount,
                    })
                .ToList();
        }

        public async Task<IList<ContractReportAdvanceNVPaymentAmountsVO>> GetContractReportAdvanceNVPaymentAmountsAsync(int contractReportId, CancellationToken ct)
        {
            var result = await (from crapa in this.unitOfWork.DbContext.Set<ContractReportAdvanceNVPaymentAmount>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on crapa.ProgrammePriorityId equals pp.MapNodeId
                    where crapa.ContractReportId == contractReportId
                    select new ContractReportAdvanceNVPaymentAmountsVO()
                    {
                        ContractReportAdvanceNVPaymentAmountId = crapa.ContractReportAdvanceNVPaymentAmountId,
                        ContractReportPaymentId = crapa.ContractReportPaymentId,
                        ContractReportId = crapa.ContractReportId,
                        ContractId = crapa.ContractId,
                        Gid = crapa.Gid,

                        Status = crapa.Status,
                        Approval = crapa.Approval,
                        Notes = crapa.Notes,
                        CheckedByUserId = crapa.CheckedByUserId,
                        CheckedDate = crapa.CheckedDate,

                        ProgrammePriorityName = pp.Name,
                        ProgrammePriorityId = crapa.ProgrammePriorityId,
                        EuAmount = crapa.EuAmount,
                        BgAmount = crapa.BgAmount,
                        BfpTotalAmount = crapa.BfpTotalAmount,
                    })
                .ToListAsync(ct);

            return result;
        }

        public bool HasDraftContractReportAdvanceNVPaymentAmounts(int contractReportId)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId && t.Status == ContractReportAdvanceNVPaymentAmountStatus.Draft).Any();
        }
    }
}
