using Eumis.Common.Db;
using Eumis.Domain;
using Eumis.Domain.Debts;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Debts.Repositories
{
    internal class CorrectionDebtVersionsRepository : AggregateRepository<CorrectionDebtVersion>, ICorrectionDebtVersionsRepository
    {
        public CorrectionDebtVersionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<CorrectionDebtVersion, object>>[] Includes
        {
            get
            {
                return Array.Empty<Expression<Func<CorrectionDebtVersion, object>>>();
            }
        }

        public IList<CorrectionDebtVersionVO> GetCorrectionDebtVersions(int correctionDebtId)
        {
            return
                (from cdv in this.unitOfWork.DbContext.Set<CorrectionDebtVersion>()
                 join u in this.unitOfWork.DbContext.Set<User>() on cdv.CreatedByUserId equals u.UserId
                 where cdv.CorrectionDebtId == correctionDebtId
                 orderby cdv.CreateDate descending
                 select new CorrectionDebtVersionVO()
                 {
                     CorrectionDebtVersionId = cdv.CorrectionDebtVersionId,
                     CorrectionDebtId = cdv.CorrectionDebtId,
                     OrderNum = cdv.OrderNum,
                     Status = cdv.Status,
                     DebtEuAmount = cdv.DebtEuAmount,
                     DebtBgAmount = cdv.DebtBgAmount,
                     CertEuAmount = cdv.CertEuAmount,
                     CertBgAmount = cdv.CertBgAmount,
                     ReimbursedEuAmount = cdv.ReimbursedEuAmount,
                     ReimbursedBgAmount = cdv.ReimbursedBgAmount,
                     CreatedByUser = u.Fullname,
                     CreateDate = cdv.CreateDate,
                     ModifyDate = cdv.ModifyDate,
                     Version = cdv.Version,
                 })
                .ToList();
        }

        public bool HasCorrectionDebtVersionsInProgress(int correctionDebtId)
        {
            return this.unitOfWork.DbContext.Set<CorrectionDebtVersion>()
                .Where(cdv => cdv.CorrectionDebtId == correctionDebtId && cdv.Status == CorrectionDebtVersionStatus.Draft)
                .Any();
        }

        public bool HasNonDraftCorrectionDebtVersions(int correctionDebtId)
        {
            return this.unitOfWork.DbContext.Set<CorrectionDebtVersion>()
                .Where(cdv => cdv.CorrectionDebtId == correctionDebtId && cdv.Status != CorrectionDebtVersionStatus.Draft)
                .Any();
        }

        public CorrectionDebtVersion GetActualVersion(int correctionDebtId)
        {
            return this.Set()
                .Where(cdv => cdv.CorrectionDebtId == correctionDebtId && cdv.Status == CorrectionDebtVersionStatus.Actual)
                .Single();
        }

        public void RemoveByDebtId(int correctionDebtId)
        {
            var version = this.Set()
                .Single(cdv => cdv.CorrectionDebtId == correctionDebtId);

            if (version.Status != CorrectionDebtVersionStatus.Draft)
            {
                throw new InvalidOperationException("Cannot delete nondraft version");
            }

            base.Remove(version);
        }

        public new void Remove(CorrectionDebtVersion version)
        {
            if (version.Status != CorrectionDebtVersionStatus.Draft || version.OrderNum == 1)
            {
                throw new DomainValidationException("Cannot delete nondraft correction debt version or the first version.");
            }

            base.Remove(version);
        }
    }
}
