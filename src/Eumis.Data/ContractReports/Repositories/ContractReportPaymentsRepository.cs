using Eumis.Common.Db;
using Eumis.Data.ContractReports.ViewObjects;
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
    internal class ContractReportPaymentsRepository : AggregateRepository<ContractReportPayment>, IContractReportPaymentsRepository
    {
        public ContractReportPaymentsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportPayment, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportPayment, object>>[]
                {
                    c => c.Files,
                };
            }
        }

        public IList<ContractReportPayment> FindAll(int contractReportId)
        {
            // Load the related entities first and than load the principal entity
            // without using EF's "Include" feature so that we can split the loading
            // into two queries and avoid downloading an expaneded result set
            // with multiple duplicated columns
            (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
             join crpxf in this.unitOfWork.DbContext.Set<ContractReportPaymentXmlFile>() on crp.ContractReportPaymentId equals crpxf.ContractReportPaymentId
             where crp.ContractReportId == contractReportId
             select crpxf).Load();

            return this.unitOfWork.DbContext.Set<ContractReportPayment>()
                .Where(p => p.ContractReportId == contractReportId)
                .ToList();
        }

        public async Task<IList<ContractReportPayment>> FindAllAsync(int contractReportId, CancellationToken ct)
        {
            // Load the related entities first and than load the principal entity
            // without using EF's "Include" feature so that we can split the loading
            // into two queries and avoid downloading an expaneded result set
            // with multiple duplicated columns
            await (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                   join crpxf in this.unitOfWork.DbContext.Set<ContractReportPaymentXmlFile>() on crp.ContractReportPaymentId equals crpxf.ContractReportPaymentId
                   where crp.ContractReportId == contractReportId
                   select crpxf).LoadAsync(ct);

            return await this.unitOfWork.DbContext.Set<ContractReportPayment>()
                .Where(p => p.ContractReportId == contractReportId)
                .ToListAsync(ct);
        }

        public ContractReportPayment Find(Guid gid)
        {
            return this.Set()
                .Where(p => p.Gid == gid)
                .Single();
        }

        public ContractReportPayment GetActualContractReportPayment(int contractReportId)
        {
            return this.Set()
                .Where(p => p.ContractReportId == contractReportId && p.Status == ContractReportPaymentStatus.Actual)
                .SingleOrDefault();
        }

        public IList<Tuple<int, int, int>> GetActualContractReportPaymentsData(int[] contractIds)
        {
            return (from p in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                    where p.Status == ContractReportPaymentStatus.Actual &&
                        contractIds.Contains(p.ContractId)
                    select new { p.VersionNum, p.ContractReportPaymentId, p.ContractId })
                    .ToList()
                    .Select(p => Tuple.Create<int, int, int>(p.ContractId, p.VersionNum, p.ContractReportPaymentId))
                    .ToList();
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

        public IList<ContractReportPaymentVO> GetContractReportPayments(int contractReportId)
        {
            return (from p in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                    where p.ContractReportId == contractReportId
                    join creg in this.unitOfWork.DbContext.Set<ContractRegistration>() on p.SenderContractRegistrationId equals creg.ContractRegistrationId into cregs
                    from creg in cregs.DefaultIfEmpty()

                    let nextVersionSubNum = p.VersionSubNum + 1

                    join pNext in this.unitOfWork.DbContext.Set<ContractReportPayment>() on new { p.ContractReportId, VersionSubNum = nextVersionSubNum } equals new { pNext.ContractReportId, pNext.VersionSubNum } into gpNext
                    from pNext in gpNext.DefaultIfEmpty()

                    orderby p.CreateDate descending
                    select new ContractReportPaymentVO
                    {
                        ContractReportPaymentId = p.ContractReportPaymentId,
                        ContractReportId = p.ContractReportId,
                        ContractId = p.ContractId,
                        VersionNum = p.VersionNum,
                        VersionSubNum = p.VersionSubNum,
                        StatusName = p.Status,
                        Status = p.Status,
                        StatusNote = p.StatusNote,
                        PaymentType = p.PaymentType,
                        RegDate = p.RegDate,
                        OtherRegistration = p.OtherRegistration,
                        SubmitDate = p.SubmitDate,
                        SubmitDeadline = p.SubmitDeadline,
                        DateFrom = p.DateFrom,
                        DateTo = p.DateTo,
                        ReturnDate = pNext.CreateDate,
                        RequestedAmount = p.RequestedAmount,
                        CreateDate = p.CreateDate,
                        ContractRegistrationEmail = creg.Email,
                    }).ToList();
        }

        public IList<ActuallyPaidAmountContractReportPaymentVO> GetActuallyPaidAmountContractReportPayments(int contractId)
        {
            return (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                    where crp.ContractId == contractId &&
                        crp.Status == ContractReportPaymentStatus.Actual &&
                        cr.Status == ContractReportStatus.Accepted
                    orderby crp.CreateDate descending
                    select new ActuallyPaidAmountContractReportPaymentVO
                    {
                        ContractReportPaymentId = crp.ContractReportPaymentId,
                        VersionNum = crp.VersionNum,
                        VersionSubNum = crp.VersionSubNum,
                        StatusName = crp.Status,
                        PaymentTypeName = crp.PaymentType,
                        RegDate = crp.RegDate,
                        DateFrom = crp.DateFrom,
                        DateTo = crp.DateTo,
                        RequestedAmount = crp.RequestedAmount,
                        CreateDate = crp.CreateDate,
                    }).ToList();
        }

        public ContractReportPayment GetLastAdvanceVerificationContractReportPayment(int contractId)
        {
            return (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                    where crp.ContractId == contractId && crp.PaymentType == ContractReportPaymentType.AdvanceVerification && cr.Status == ContractReportStatus.Accepted
                    orderby new { crp.VersionNum, crp.VersionSubNum } descending
                    select crp)
                .FirstOrDefault();
        }

        public async Task<ContractReportPayment> GetLastAdvanceVerificationContractReportPaymentAsync(int contractId, CancellationToken ct)
        {
            var result = await (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                                where crp.ContractId == contractId && crp.PaymentType == ContractReportPaymentType.AdvanceVerification && cr.Status == ContractReportStatus.Accepted
                                orderby new { crp.VersionNum, crp.VersionSubNum } descending
                                select crp)
                                .FirstOrDefaultAsync(ct);

            return result;
        }

        public bool CanCreateAdvanceVerificationPayment(int contractId)
        {
            return !(from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                     join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                     where crp.ContractId == contractId && crp.PaymentType == ContractReportPaymentType.AdvanceVerification && cr.Status == ContractReportStatus.Accepted
                     select crp)
                .Any();
        }

        public ContractReportStatus GetContractReportStatus(int paymentId)
        {
            return (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                    join cr in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals cr.ContractReportId
                    where crp.ContractReportPaymentId == paymentId
                    select cr.Status).Single();
        }

        public ContractReportPaymentStatus GetContractReportPaymentStatus(int paymentId)
        {
            return (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                    where crp.ContractReportPaymentId == paymentId
                    select crp.Status).Single();
        }

        public int GetContractId(int paymentId)
        {
            return (from crp in this.unitOfWork.DbContext.Set<ContractReportPayment>()
                    where crp.ContractReportPaymentId == paymentId
                    select crp.ContractId).Single();
        }
    }
}
