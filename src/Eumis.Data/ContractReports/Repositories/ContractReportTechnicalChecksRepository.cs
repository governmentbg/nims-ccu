using Eumis.Common.Db;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportTechnicalChecksRepository : AggregateRepository<ContractReportTechnicalCheck>, IContractReportTechnicalChecksRepository
    {
        public ContractReportTechnicalChecksRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportTechnicalCheck, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportTechnicalCheck, object>>[]
                {
                    c => c.File,
                };
            }
        }

        public IList<ContractReportTechnicalCheckVO> GetContractReportTechnicalChecks(int contractReportId)
        {
            return (from crtc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCheck>()
                    join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>() on crtc.ContractReportTechnicalId equals crt.ContractReportTechnicalId

                    join u in this.unitOfWork.DbContext.Set<User>() on crtc.CheckedByUserId equals u.UserId into g1
                    from u in g1.DefaultIfEmpty()

                    where crtc.ContractReportId == contractReportId
                    orderby crtc.CreateDate descending
                    select new ContractReportTechnicalCheckVO
                    {
                        ContractReportTechnicalCheckId = crtc.ContractReportTechnicalCheckId,
                        ContractReportTechnicalId = crtc.ContractReportTechnicalId,
                        ContractReportId = crtc.ContractReportId,
                        ContractId = crtc.ContractId,
                        OrderNum = crtc.OrderNum,
                        Status = crtc.Status,
                        CheckedByUser = u != null ? u.Fullname + " (" + u.Username + ")" : null,
                        CreateDate = crtc.CreateDate,
                        TechnicalVersionNum = crt.VersionNum,
                        TechnicalVersionSubNum = crt.VersionSubNum,
                    })
                    .ToList();
        }

        public IList<ContractReportTechnicalCheck> FindAll(int contractReportId)
        {
            return this.Set()
                .Where(t => t.ContractReportId == contractReportId)
                .ToList();
        }

        public int GetNextOrderNum(int contractReportTechnicalId)
        {
            var lastOrderNumber = this.Set()
                .Where(p => p.ContractReportTechnicalId == contractReportTechnicalId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public bool HasContractReportTechnicalCheckInProgress(int contractReportId)
        {
            return (from crtc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCheck>()
                    where crtc.ContractReportId == contractReportId && crtc.Status == ContractReportTechnicalCheckStatus.Draft
                    select crtc.ContractReportTechnicalCheckId).Any();
        }

        public ContractReportTechnicalCheck GetActualContractReportTechnicalCheck(int contractReportId)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId && t.Status == ContractReportTechnicalCheckStatus.Active).SingleOrDefault();
        }
    }
}
