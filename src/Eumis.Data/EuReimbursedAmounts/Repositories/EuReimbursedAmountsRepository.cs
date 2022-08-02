using Eumis.Common.Db;
using Eumis.Data.EuReimbursedAmounts.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.CertReports;
using Eumis.Domain.EuReimbursedAmounts;
using Eumis.Domain.OperationalMap.Programmes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.EuReimbursedAmounts.Repositories
{
    internal class EuReimbursedAmountsRepository : AggregateRepository<EuReimbursedAmount>, IEuReimbursedAmountsRepository
    {
        public EuReimbursedAmountsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<EuReimbursedAmount, object>>[] Includes
        {
            get
            {
                return new Expression<Func<EuReimbursedAmount, object>>[]
                {
                    era => era.CertReports,
                };
            }
        }

        public IList<EuReimbursedAmountVO> GetEuReimbursedAmounts(int[] programmeIds, EuReimbursedAmountStatus? status = null)
        {
            var predicate = PredicateBuilder.True<EuReimbursedAmount>()
                .AndEquals(era => era.Status, status);

            return (from era in this.unitOfWork.DbContext.Set<EuReimbursedAmount>().Where(predicate)
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on era.ProgrammeId equals pr.MapNodeId
                    where programmeIds.Contains(era.ProgrammeId)
                    orderby new { era.CreateDate } descending
                    select new EuReimbursedAmountVO
                    {
                        EuReimbursedAmountId = era.EuReimbursedAmountId,
                        ProgrammeName = pr.Name,
                        StatusDescr = era.Status,
                        Status = era.Status,
                        PaymentType = era.PaymentType,
                        Date = era.Date,
                        EuTranche = era.EuTranche,
                    }).ToList();
        }

        public EuReimbursedAmountInfoVO GetInfo(int euReimbursedAmountId)
        {
            return (from era in this.unitOfWork.DbContext.Set<EuReimbursedAmount>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on era.ProgrammeId equals pr.MapNodeId
                    where era.EuReimbursedAmountId == euReimbursedAmountId
                    select new EuReimbursedAmountInfoVO
                    {
                        ProgrammeCode = pr.Code,
                        Status = era.Status,
                        StatusDescr = era.Status,
                        Version = era.Version,
                    }).Single();
        }

        public IList<EuReimbursedAmountCertReportVO> GetCertReports(int euReimbursedAmountId)
        {
            return (from eracr in this.unitOfWork.DbContext.Set<EuReimbursedAmountCertReport>()
                    join cr in this.unitOfWork.DbContext.Set<CertReport>() on eracr.CertReportId equals cr.CertReportId
                    where eracr.EuReimbursedAmountId == euReimbursedAmountId
                    orderby cr.OrderNum descending
                    select new EuReimbursedAmountCertReportVO
                    {
                        EuReimbursedAmountCertReportId = eracr.EuReimbursedAmountCertReportId,
                        CertReportId = eracr.CertReportId,
                        OrderNum = cr.OrderNum,
                        RegDate = cr.RegDate,
                        DateFrom = cr.DateFrom,
                        DateTo = cr.DateTo,
                        Type = cr.Type,
                    }).ToList();
        }

        public IList<EuReimbursedAmountCertReportVO> GetNotIncludedCertReports(int euReimbursedAmountId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<EuReimbursedAmountCertReport>()
                           where item.EuReimbursedAmountId == euReimbursedAmountId
                           select item.CertReportId;

            return (from cr in this.unitOfWork.DbContext.Set<CertReport>()
                    join era in this.unitOfWork.DbContext.Set<EuReimbursedAmount>() on cr.ProgrammeId equals era.ProgrammeId
                    where !subquery.Contains(cr.CertReportId) && era.EuReimbursedAmountId == euReimbursedAmountId && (cr.Status == CertReportStatus.Approved || cr.Status == CertReportStatus.PartialyApproved)
                    orderby cr.OrderNum descending
                    select new EuReimbursedAmountCertReportVO
                    {
                        CertReportId = cr.CertReportId,
                        OrderNum = cr.OrderNum,
                        RegDate = cr.RegDate,
                        DateFrom = cr.DateFrom,
                        DateTo = cr.DateTo,
                        Type = cr.Type,
                    })
                .ToList();
        }

        public int GetProgrammeId(int euReimbursedAmountId)
        {
            return (from era in this.unitOfWork.DbContext.Set<EuReimbursedAmount>()
                    where era.EuReimbursedAmountId == euReimbursedAmountId
                    select era.ProgrammeId).Single();
        }

        public new void Remove(EuReimbursedAmount euReimbursedAmount)
        {
            if (euReimbursedAmount.IsActivated || euReimbursedAmount.Status != EuReimbursedAmountStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete euReimbursedAmount which is in draft status or is activated");
            }

            base.Remove(euReimbursedAmount);
        }
    }
}
