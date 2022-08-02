using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.FinancialCorrections.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;

namespace Eumis.Data.FinancialCorrections.Repositories
{
    internal class FinancialCorrectionVersionsRepository : AggregateRepository<FinancialCorrectionVersion>, IFinancialCorrectionVersionsRepository
    {
        public FinancialCorrectionVersionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<FinancialCorrectionVersion, object>>[] Includes
        {
            get
            {
                return new Expression<Func<FinancialCorrectionVersion, object>>[]
                {
                    e => e.File,
                    e => e.FinancialCorrectionVersionViolations,
                };
            }
        }

        public IList<FinancialCorrectionVersionVO> GetFinancialCorrectionVersions(int financialCorrectionId)
        {
            return (from fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>()
                    where fcv.FinancialCorrectionId == financialCorrectionId
                    orderby fcv.CreateDate descending
                    select new FinancialCorrectionVersionVO
                    {
                        FinancialCorrectionVersionId = fcv.FinancialCorrectionVersionId,
                        FinancialCorrectionId = fcv.FinancialCorrectionId,
                        OrderNum = fcv.OrderNum,
                        Status = fcv.Status,
                        Percent = fcv.Percent,
                        EuAmount = fcv.EuAmount,
                        BgAmount = fcv.BgAmount,
                        SelfAmount = fcv.SelfAmount,
                        TotalAmount = fcv.TotalAmount,
                        AmendmentReason = fcv.AmendmentReason,
                        CorrectionBearer = fcv.CorrectionBearer,
                    }).ToList();
        }

        public bool HasFinancialCorrectionVersionsInProgress(int financialCorrectionId)
        {
            return (from fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>()
                    where fcv.FinancialCorrectionId == financialCorrectionId && fcv.Status == FinancialCorrectionVersionStatus.Draft
                    select fcv.FinancialCorrectionVersionId).Any();
        }

        public bool HasNonDraftFinancialCorrectionVersions(int financialCorrectionId)
        {
            return (from fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>()
                    where fcv.FinancialCorrectionId == financialCorrectionId && fcv.Status != FinancialCorrectionVersionStatus.Draft
                    select fcv.FinancialCorrectionVersionId).Any();
        }

        public FinancialCorrectionVersion GetActualVersion(int financialCorrectionId)
        {
            return this.Set()
                .Where(cv => cv.FinancialCorrectionId == financialCorrectionId && cv.Status == FinancialCorrectionVersionStatus.Actual)
                .Single();
        }

        public FinancialCorrectionStatus GetFinancialCorrectionStatus(int versionId)
        {
            return (from fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>()
                    join fc in this.unitOfWork.DbContext.Set<FinancialCorrection>() on fcv.FinancialCorrectionId equals fc.FinancialCorrectionId
                    where fcv.FinancialCorrectionVersionId == versionId
                    select fc.Status).Single();
        }

        public void RemoveByFinancialCorrectionId(int financialCorrectionId)
        {
            var version = this.Set()
                .Single(fcv => fcv.FinancialCorrectionId == financialCorrectionId);

            if (version.Status != FinancialCorrectionVersionStatus.Draft)
            {
                throw new InvalidOperationException("Cannot delete nondraft version");
            }

            base.Remove(version);
        }

        public new void Remove(FinancialCorrectionVersion version)
        {
            if (version.Status != FinancialCorrectionVersionStatus.Draft || version.OrderNum == 1)
            {
                throw new DomainValidationException("Cannot delete nondraft FinancialCorrectionVersion or the first version.");
            }

            base.Remove(version);
        }
    }
}
