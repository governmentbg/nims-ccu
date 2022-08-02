using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;
using Eumis.Domain.OperationalMap.Programmes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ReimbursedAmounts.Repositories
{
    internal class ContractReimbursedAmountsRepository : AggregateRepository<ContractReimbursedAmount, ReimbursedAmount>, IContractReimbursedAmountsRepository
    {
        public ContractReimbursedAmountsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReimbursedAmount, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReimbursedAmount, object>>[]
                {
                    e => e.ContractReimbursedAmountPayments,
                };
            }
        }

        public IList<ContractReimbursedAmountVO> GetReimbursedAmounts(int[] programmeIds, int userId, int? contractId = null, ReimbursementType? type = null)
        {
            var basePredicate = PredicateBuilder.True<ContractReimbursedAmount>()
                .AndEquals(ra => ra.ContractId, contractId)
                .AndEquals(ra => ra.Type, type);

            var predicate = basePredicate
                .And(cra => programmeIds.Contains(cra.ProgrammeId));

            var externalVerificatorReimbursedAmounts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                       join cra in this.unitOfWork.DbContext.Set<ContractReimbursedAmount>().Where(basePredicate) on cu.ContractId equals cra.ContractId
                                                       select cra;

            return (from ra in this.unitOfWork.DbContext.Set<ContractReimbursedAmount>().Where(predicate).Union(externalVerificatorReimbursedAmounts)
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ra.ContractId equals c.ContractId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on ra.ProgrammeId equals pr.MapNodeId
                    orderby ra.CreateDate descending
                    select new ContractReimbursedAmountVO
                    {
                        AmountId = ra.ReimbursedAmountId,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        RegNumber = ra.RegNumber,
                        StatusDescr = ra.Status,
                        Status = ra.Status,
                        Type = ra.Type,
                        Reimbursement = ra.Reimbursement,
                        ReimbursementDate = ra.ReimbursementDate,
                        PrincipalEuAmount = ra.PrincipalBfp.EuAmount,
                        PrincipalBgAmount = ra.PrincipalBfp.BgAmount,
                        PrincipalTotalAmount = ra.PrincipalBfp.TotalAmount,
                        InterestEuAmount = ra.InterestBfp.EuAmount,
                        InterestBgAmount = ra.InterestBfp.BgAmount,
                        InterestTotalAmount = ra.InterestBfp.TotalAmount,
                    })
                    .Distinct()
                    .ToList();
        }

        public ContractReimbursedAmountInfoVO GetInfo(int reimbursedAmountId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<ContractReimbursedAmount>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ra.ContractId equals c.ContractId
                    where ra.ReimbursedAmountId == reimbursedAmountId
                    select new ContractReimbursedAmountInfoVO
                    {
                        ContractRegNumber = c.RegNumber,
                        Status = ra.Status,
                        StatusDescr = ra.Status,
                    }).Single();
        }

        public ContractReimbursedAmountBasicDataVO GetBasicData(int reimbursedAmountId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<ContractReimbursedAmount>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ra.ContractId equals c.ContractId
                    where ra.ReimbursedAmountId == reimbursedAmountId
                    select new ContractReimbursedAmountBasicDataVO
                    {
                        ReimbursedAmountId = ra.ReimbursedAmountId,
                        ContractId = ra.ContractId,
                        RegNumber = ra.RegNumber,
                        Status = ra.Status,
                        IsActivated = ra.IsActivated,
                        IsDeletedNote = ra.IsDeletedNote,
                        ProgrammeId = ra.ProgrammeId,
                        ContractName = c.Name,
                        ContractRegNumber = c.RegNumber,
                        CompanyName = c.CompanyName,
                        CompanyUin = c.CompanyUin,
                        CompanyUinType = c.CompanyUinType,
                        Version = ra.Version,
                    }).Single();
        }

        public int GetProgrammeId(int reimbursedAmountId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<ContractReimbursedAmount>()
                    where ra.ReimbursedAmountId == reimbursedAmountId
                    select ra.ProgrammeId).Single();
        }

        public new void Remove(ContractReimbursedAmount reimbursedAmount)
        {
            if (reimbursedAmount.IsActivated || reimbursedAmount.Status != ReimbursedAmountStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete ContractReimbursedAmount which is in draft status or is activated.");
            }

            base.Remove(reimbursedAmount);
        }

        public IList<ContractReimbursedAmountVO> GetReimbursedAmountsForProjectDossier(int contractId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<ContractReimbursedAmount>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ra.ContractId equals c.ContractId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on ra.ProgrammeId equals pr.MapNodeId
                    where ra.ContractId == contractId && ra.Status != ReimbursedAmountStatus.Draft
                    orderby ra.CreateDate descending
                    select new ContractReimbursedAmountVO
                    {
                        AmountId = ra.ReimbursedAmountId,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        RegNumber = ra.RegNumber,
                        StatusDescr = ra.Status,
                        Status = ra.Status,
                        Type = ra.Type,
                        Reimbursement = ra.Reimbursement,
                        ReimbursementDate = ra.ReimbursementDate,
                        PrincipalEuAmount = ra.PrincipalBfp.EuAmount,
                        PrincipalBgAmount = ra.PrincipalBfp.BgAmount,
                        PrincipalTotalAmount = ra.PrincipalBfp.TotalAmount,
                        InterestEuAmount = ra.InterestBfp.EuAmount,
                        InterestBgAmount = ra.InterestBfp.BgAmount,
                        InterestTotalAmount = ra.InterestBfp.TotalAmount,
                    }).ToList();
        }

        public void SwitchToDebtReimbursedAmount(int reimbursedAmountId, int contractDebtId)
        {
            var updateSql = @"UPDATE [dbo].[ReimbursedAmounts] SET [ContractDebtId] = @contractDebtId, [Discriminator] = @discriminator WHERE [ReimbursedAmountId] = @reimbursedAmountId;";

            this.ExecuteSqlCommand(
                updateSql,
                new SqlParameter("@contractDebtId", contractDebtId),
                new SqlParameter("@discriminator", (int)ReimbursedAmountDiscriminator.Debt),
                new SqlParameter("@reimbursedAmountId", reimbursedAmountId));
        }

        public int GetContractId(int reimbursedAmountId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<ContractReimbursedAmount>()
                    where ra.ReimbursedAmountId == reimbursedAmountId
                    select ra.ContractId).Single();
        }
    }
}
