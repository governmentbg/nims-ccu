using Eumis.Common.Db;
using Eumis.Domain;
using Eumis.Domain.Debts;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Debts.Repositories
{
    internal class ContractDebtVersionsRepository : AggregateRepository<ContractDebtVersion>, IContractDebtVersionsRepository
    {
        public ContractDebtVersionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractDebtVersionVO> GetContractDebtVersions(int contractDebtId)
        {
            return
                (from cdv in this.unitOfWork.DbContext.Set<ContractDebtVersion>()
                 join u in this.unitOfWork.DbContext.Set<User>() on cdv.CreatedByUserId equals u.UserId
                 where cdv.ContractDebtId == contractDebtId
                 select new ContractDebtVersionVO()
                 {
                     ContractDebtVersionId = cdv.ContractDebtVersionId,
                     ContractDebtId = cdv.ContractDebtId,
                     OrderNum = cdv.OrderNum,
                     Status = cdv.Status,
                     ExecutionStatus = cdv.ExecutionStatus,
                     EuAmount = cdv.EuAmount,
                     BgAmount = cdv.BgAmount,
                     TotalAmount = cdv.TotalAmount,
                     CreatedByUser = u.Fullname,
                     CreateDate = cdv.CreateDate,
                     ModifyDate = cdv.ModifyDate,
                     Version = cdv.Version,
                 })
                .OrderByDescending(cdv => cdv.CreateDate)
                .ToList();
        }

        public bool HasContractDebtVersionsInProgress(int contractDebtId)
        {
            return this.unitOfWork.DbContext.Set<ContractDebtVersion>()
                .Where(cdv => cdv.ContractDebtId == contractDebtId && cdv.Status == ContractDebtVersionStatus.Draft)
                .Any();
        }

        public bool HasNonDraftContractDebtVersions(int contractDebtId)
        {
            return this.unitOfWork.DbContext.Set<ContractDebtVersion>()
                .Where(cdv => cdv.ContractDebtId == contractDebtId && cdv.Status != ContractDebtVersionStatus.Draft)
                .Any();
        }

        public ContractDebtVersion GetActualVersion(int contractDebtId)
        {
            return this.Set()
                .SingleOrDefault(cdv => cdv.ContractDebtId == contractDebtId && cdv.Status == ContractDebtVersionStatus.Actual);
        }

        public void RemoveByContractDebtId(int contractDebtId)
        {
            var version = this.Set()
                .Single(cdv => cdv.ContractDebtId == contractDebtId);

            if (version.Status != ContractDebtVersionStatus.Draft)
            {
                throw new InvalidOperationException("Cannot delete nondraft version");
            }

            base.Remove(version);
        }

        public new void Remove(ContractDebtVersion version)
        {
            if (version.Status != ContractDebtVersionStatus.Draft || version.OrderNum == 1)
            {
                throw new DomainValidationException("Cannot delete nondraft contract debt version or the first version.");
            }

            base.Remove(version);
        }
    }
}
