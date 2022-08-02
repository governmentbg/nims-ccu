using Eumis.Common.Db;
using Eumis.Data.FIReimbursedAmounts.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.FIReimbursedAmounts;
using Eumis.Domain.OperationalMap.Programmes;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.FIReimbursedAmounts.Repositories
{
    internal class FIReimbursedAmountsRepository : AggregateRepository<FIReimbursedAmount>, IFIReimbursedAmountsRepository
    {
        public FIReimbursedAmountsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<FIReimbursedAmountVO> GetReimbursedAmounts(int[] programmeIds, int? contractId = null, FIReimbursementType? type = null)
        {
            var predicate = PredicateBuilder.True<FIReimbursedAmount>()
                .AndEquals(ra => ra.ContractId, contractId)
                .AndEquals(ra => ra.Type, type);

            return (from ra in this.unitOfWork.DbContext.Set<FIReimbursedAmount>().Where(predicate)
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ra.ContractId equals c.ContractId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on ra.ProgrammeId equals pr.MapNodeId
                    where programmeIds.Contains(ra.ProgrammeId)
                    orderby ra.CreateDate descending
                    select new FIReimbursedAmountVO
                    {
                        AmountId = ra.FIReimbursedAmountId,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        RegNumber = ra.RegNumber,
                        StatusDescr = ra.Status,
                        Status = ra.Status,
                        Type = ra.Type,
                        Reimbursement = ra.Reimbursement,
                        ReimbursementDate = ra.ReimbursementDate,
                    }).ToList();
        }

        public IList<FIReimbursedAmountVO> GetReimbursedAmountsForProjectDossier(int contractId)
        {
            return (from fira in this.unitOfWork.DbContext.Set<FIReimbursedAmount>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on fira.ContractId equals c.ContractId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on fira.ProgrammeId equals pr.MapNodeId
                    where fira.ContractId == contractId && fira.Status != FIReimbursedAmountStatus.Draft
                    orderby fira.CreateDate descending
                    select new FIReimbursedAmountVO
                    {
                        AmountId = fira.FIReimbursedAmountId,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        RegNumber = fira.RegNumber,
                        StatusDescr = fira.Status,
                        Status = fira.Status,
                        Type = fira.Type,
                        Reimbursement = fira.Reimbursement,
                        ReimbursementDate = fira.ReimbursementDate,
                    }).ToList();
        }

        public FIReimbursedAmountInfoVO GetInfo(int fiReimbursedAmountId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<FIReimbursedAmount>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ra.ContractId equals c.ContractId
                    where ra.FIReimbursedAmountId == fiReimbursedAmountId
                    select new FIReimbursedAmountInfoVO
                    {
                        ContractRegNumber = c.RegNumber,
                        Status = ra.Status,
                        StatusDescr = ra.Status,
                    }).Single();
        }

        public FIReimbursedAmountBasicDataVO GetBasicData(int fiReimbursedAmountId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<FIReimbursedAmount>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ra.ContractId equals c.ContractId
                    where ra.FIReimbursedAmountId == fiReimbursedAmountId
                    select new FIReimbursedAmountBasicDataVO
                    {
                        ReimbursedAmountId = ra.FIReimbursedAmountId,
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

        public int GetProgrammeId(int fiReimbursedAmountId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<FIReimbursedAmount>()
                    where ra.FIReimbursedAmountId == fiReimbursedAmountId
                    select ra.ProgrammeId).Single();
        }

        public new void Remove(FIReimbursedAmount reimbursedAmount)
        {
            if (reimbursedAmount.IsActivated || reimbursedAmount.Status != FIReimbursedAmountStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete FIReimbursedAmount which is in draft status or is activated.");
            }

            base.Remove(reimbursedAmount);
        }
    }
}
