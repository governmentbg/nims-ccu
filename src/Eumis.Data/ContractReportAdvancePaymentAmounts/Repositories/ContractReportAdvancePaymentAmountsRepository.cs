using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReportAdvancePaymentAmounts.Repositories
{
    internal class ContractReportAdvancePaymentAmountsRepository : AggregateRepository<ContractReportAdvancePaymentAmount>, IContractReportAdvancePaymentAmountsRepository
    {
        public ContractReportAdvancePaymentAmountsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportAdvancePaymentAmount> FindAll(int contractReportId)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId).ToList();
        }

        public IList<ContractReportAdvancePaymentAmount> FindAll(int contractReportId, int[] contractReportAdvancePaymentAmountIds)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId && contractReportAdvancePaymentAmountIds.Contains(t.ContractReportAdvancePaymentAmountId)).ToList();
        }

        public IList<ContractReportAdvancePaymentAmount> FindAllByCertReport(int certReportId, int contractReportId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId && t.ContractReportId == contractReportId).ToList();
        }

        public IList<ContractReportAdvancePaymentAmount> FindAllByCertReport(int certReportId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId).ToList();
        }

        public IList<ContractReportAdvancePaymentAmountsVO> GetContractReportAdvancePaymentAmounts(int contractReportId, bool? isAttachedToCertReport = null, int? certReportId = null)
        {
            var predicate = PredicateBuilder.True<ContractReportAdvancePaymentAmount>();

            if (isAttachedToCertReport.HasValue)
            {
                if (isAttachedToCertReport.Value == true)
                {
                    predicate = predicate.AndPropertyEquals(t => t.CertReportId, certReportId.Value);
                }
                else
                {
                    predicate = predicate.AndPropertyEquals(t => t.CertReportId, null);
                }
            }

            return (from crapa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>().Where(predicate)
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on crapa.ProgrammePriorityId equals pp.MapNodeId
                    where crapa.ContractReportId == contractReportId
                    select new ContractReportAdvancePaymentAmountsVO()
                    {
                        ContractReportAdvancePaymentAmountId = crapa.ContractReportAdvancePaymentAmountId,
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
                        ApprovedEuAmount = crapa.ApprovedEuAmount,
                        ApprovedBgAmount = crapa.ApprovedBgAmount,
                        ApprovedBfpTotalAmount = crapa.ApprovedBfpTotalAmount,

                        CertStatus = crapa.CertStatus,
                        CertifiedApprovedEuAmount = crapa.CertifiedApprovedEuAmount,
                        CertifiedApprovedBgAmount = crapa.CertifiedApprovedBgAmount,
                        CertifiedApprovedBfpTotalAmount = crapa.CertifiedApprovedBfpTotalAmount,
                    })
                .ToList();
        }

        public async Task<IList<ContractReportAdvancePaymentAmountsVO>> GetContractReportAdvancePaymentAmountsAsync(int contractReportId, CancellationToken ct, bool? isAttachedToCertReport = null, int? certReportId = null)
        {
            var predicate = PredicateBuilder.True<ContractReportAdvancePaymentAmount>();

            if (isAttachedToCertReport.HasValue)
            {
                if (isAttachedToCertReport.Value == true)
                {
                    predicate = predicate.AndPropertyEquals(t => t.CertReportId, certReportId.Value);
                }
                else
                {
                    predicate = predicate.AndPropertyEquals(t => t.CertReportId, null);
                }
            }

            var result = await (from crapa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>().Where(predicate)
                                join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on crapa.ProgrammePriorityId equals pp.MapNodeId
                                where crapa.ContractReportId == contractReportId
                                select new ContractReportAdvancePaymentAmountsVO()
                                {
                                    ContractReportAdvancePaymentAmountId = crapa.ContractReportAdvancePaymentAmountId,
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
                                    ApprovedEuAmount = crapa.ApprovedEuAmount,
                                    ApprovedBgAmount = crapa.ApprovedBgAmount,
                                    ApprovedBfpTotalAmount = crapa.ApprovedBfpTotalAmount,

                                    CertStatus = crapa.CertStatus,
                                    CertifiedApprovedEuAmount = crapa.CertifiedApprovedEuAmount,
                                    CertifiedApprovedBgAmount = crapa.CertifiedApprovedBgAmount,
                                    CertifiedApprovedBfpTotalAmount = crapa.CertifiedApprovedBfpTotalAmount,
                                })
                                .ToListAsync(ct);

            return result;
        }

        public bool HasDraftContractReportAdvancePaymentAmounts(int contractReportId)
        {
            return (from apa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>()
                    where apa.ContractReportId == contractReportId && apa.Status == ContractReportAdvancePaymentAmountStatus.Draft
                    select apa.ContractReportAdvancePaymentAmountId).Any();
        }

        public bool HasCertDraftContractReportAdvancePaymentAmounts(int certReportId)
        {
            return (from apa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>()
                    where apa.CertReportId == certReportId && apa.CertStatus == ContractReportAdvancePaymentAmountCertStatus.Draft
                    select apa.ContractReportAdvancePaymentAmountId).Any();
        }

        public bool HasCertContractReportAdvancePaymentAmounts(int certReportId)
        {
            return (from apa in this.unitOfWork.DbContext.Set<ContractReportAdvancePaymentAmount>()
                    where apa.CertReportId == certReportId
                    select apa.ContractReportAdvancePaymentAmountId).Any();
        }
    }
}
