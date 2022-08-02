using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.CertReports;
using Eumis.Domain.Contracts;
using Eumis.Domain.Debts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;
using Eumis.Domain.OperationalMap.Programmes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.ReimbursedAmounts.Repositories
{
    internal class DebtReimbursedAmountsRepository : AggregateRepository<DebtReimbursedAmount, ReimbursedAmount>, IDebtReimbursedAmountsRepository
    {
        public DebtReimbursedAmountsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<DebtReimbursedAmountVO> GetReimbursedAmounts(int[] programmeIds, int userId, int? contractId = null, ReimbursementType? type = null)
        {
            var basePredicate = PredicateBuilder.True<DebtReimbursedAmount>()
                .AndEquals(ra => ra.ContractId, contractId)
                .AndEquals(ra => ra.Type, type);

            var predicate = basePredicate
                .And(dra => programmeIds.Contains(dra.ProgrammeId));

            var externalVerificatorReimbursedAmounts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                       join cd in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>().Where(basePredicate) on cu.ContractId equals cd.ContractId
                                                       select cd;

            return (from ra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>().Where(predicate).Union(externalVerificatorReimbursedAmounts)
                    join cd in this.unitOfWork.DbContext.Set<ContractDebt>() on ra.ContractDebtId equals cd.ContractDebtId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on ra.ProgrammeId equals pr.MapNodeId
                    orderby ra.CreateDate descending
                    select new DebtReimbursedAmountVO
                    {
                        AmountId = ra.ReimbursedAmountId,
                        ProgrammeName = pr.Name,
                        DebtRegNumber = cd.RegNumber,
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

        public IList<DebtReimbursedAmountVO> GetReimbursedAmountsForProjectDossier(int contractId)
        {
            return (from dra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                    join cd in this.unitOfWork.DbContext.Set<ContractDebt>() on dra.ContractDebtId equals cd.ContractDebtId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on dra.ContractId equals c.ContractId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on dra.ProgrammeId equals pr.MapNodeId
                    where dra.ContractId == contractId && dra.Status != ReimbursedAmountStatus.Draft
                    orderby dra.CreateDate descending
                    select new DebtReimbursedAmountVO
                    {
                        AmountId = dra.ReimbursedAmountId,
                        ProgrammeName = pr.Name,
                        DebtRegNumber = cd.RegNumber,
                        RegNumber = dra.RegNumber,
                        StatusDescr = dra.Status,
                        Status = dra.Status,
                        Type = dra.Type,
                        Reimbursement = dra.Reimbursement,
                        ReimbursementDate = dra.ReimbursementDate,
                        PrincipalEuAmount = dra.PrincipalBfp.EuAmount,
                        PrincipalBgAmount = dra.PrincipalBfp.BgAmount,
                        PrincipalTotalAmount = dra.PrincipalBfp.TotalAmount,
                        InterestEuAmount = dra.InterestBfp.EuAmount,
                        InterestBgAmount = dra.InterestBfp.BgAmount,
                        InterestTotalAmount = dra.InterestBfp.TotalAmount,
                    }).ToList();
        }

        public IList<DebtReimbursedAmount> FindAllEnteredForDebt(int contractDebtId)
        {
            return this.Set()
                .Where(t => t.ContractDebtId == contractDebtId && t.Status == ReimbursedAmountStatus.Entered)
                .ToList();
        }

        public IList<DebtReimbursedAmount> FindAllForDebt(int contractDebtId)
        {
            return this.Set()
                .Where(t => t.ContractDebtId == contractDebtId)
                .ToList();
        }

        public DebtReimbursedAmountInfoVO GetInfo(int reimbursedAmountId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                    join cd in this.unitOfWork.DbContext.Set<ContractDebt>() on ra.ContractDebtId equals cd.ContractDebtId
                    where ra.ReimbursedAmountId == reimbursedAmountId
                    select new DebtReimbursedAmountInfoVO
                    {
                        DebtRegNumber = cd.RegNumber,
                        Status = ra.Status,
                        StatusDescr = ra.Status,
                    }).Single();
        }

        public DebtReimbursedAmountBasicDataVO GetBasicData(int reimbursedAmountId)
        {
            var result = (from ra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                          join c in this.unitOfWork.DbContext.Set<Contract>() on ra.ContractId equals c.ContractId
                          join cd in this.unitOfWork.DbContext.Set<ContractDebt>() on ra.ContractDebtId equals cd.ContractDebtId

                          join cr in this.unitOfWork.DbContext.Set<CertReport>() on ra.CertReportId equals cr.CertReportId into g1
                          from cr in g1.DefaultIfEmpty()

                          where ra.ReimbursedAmountId == reimbursedAmountId
                          select new
                          {
                              ra.ReimbursedAmountId,
                              ra.RegNumber,
                              ra.Status,
                              ra.IsActivated,
                              ra.IsDeletedNote,
                              ra.ProgrammeId,
                              ra.SapFileId,
                              ContractName = c.Name,
                              ContractRegNumber = c.RegNumber,
                              c.CompanyName,
                              c.CompanyUin,
                              c.CompanyUinType,
                              ra.ContractDebtId,
                              ra.Version,
                              CertReportNum = (int?)cr.OrderNum,
                          }).Single();

            var payments = (from cdp in this.unitOfWork.DbContext.Set<ContractDebtPayment>()
                            join p in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cdp.ContractReportPaymentId equals p.ContractReportPaymentId
                            join pc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>() on p.ContractReportPaymentId equals pc.ContractReportPaymentId
                            where cdp.ContractDebtId == result.ContractDebtId &&
                                  pc.Status == ContractReportPaymentCheckStatus.Active
                            select new
                            {
                                VersionNum = (int?)p.VersionNum,
                                RequestedAmount = (decimal?)p.RequestedAmount,
                                ApprovedTotalAmount = (decimal?)pc.ContractReportPaymentCheckAmounts.Sum(t => t.ApprovedTotalAmount),
                                CheckedDate = (DateTime?)pc.CheckedDate,
                            }).ToList();

            return new DebtReimbursedAmountBasicDataVO
            {
                ReimbursedAmountId = result.ReimbursedAmountId,
                RegNumber = result.RegNumber,
                Status = result.Status,
                IsActivated = result.IsActivated,
                IsDeletedNote = result.IsDeletedNote,
                ProgrammeId = result.ProgrammeId,
                ContractName = result.ContractName,
                ContractRegNumber = result.ContractRegNumber,
                CompanyName = result.CompanyName,
                CompanyUin = result.CompanyUin,
                CompanyUinType = result.CompanyUinType,
                ContractDebtId = result.ContractDebtId,
                Version = result.Version,
                CertReportNum = result.CertReportNum,
                CreationType = result.SapFileId == null ? DebtReimbursedAmountCreationType.Manual : DebtReimbursedAmountCreationType.SAPImport,
                Payments = payments.Select(p => new DebtReimbursedAmountBasicDataPaymentVO
                {
                    PaymentVersionNum = p.VersionNum,
                    RequestedAmount = p.RequestedAmount,
                    ApprovedTotalAmount = p.ApprovedTotalAmount,
                    CheckedDate = p.CheckedDate,
                }).ToList(),
            };
        }

        public int GetProgrammeId(int reimbursedAmountId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                    where ra.ReimbursedAmountId == reimbursedAmountId
                    select ra.ProgrammeId).Single();
        }

        public new void Remove(DebtReimbursedAmount reimbursedAmount)
        {
            if (reimbursedAmount.IsActivated || reimbursedAmount.Status != ReimbursedAmountStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete DebtReimbursedAmount which is in draft status or is activated.");
            }

            base.Remove(reimbursedAmount);
        }

        public IList<string> CanEnter(int reimbursedAmountId)
        {
            var errors = new List<string>();
            var reimbursedAmount = this.Find(reimbursedAmountId);

            if (!reimbursedAmount.PrincipalBfp.BgAmount.HasValue ||
               !reimbursedAmount.PrincipalBfp.EuAmount.HasValue ||
               !reimbursedAmount.PrincipalBfp.TotalAmount.HasValue ||
               !reimbursedAmount.InterestBfp.EuAmount.HasValue ||
               !reimbursedAmount.InterestBfp.EuAmount.HasValue ||
               !reimbursedAmount.InterestBfp.TotalAmount.HasValue)
            {
                errors.Add("Всички полета от секциите 'Главница' и 'Лихва' трябва да са попълнени, за да въведете възстановената сума");
            }

            return errors;
        }

        public IList<string> CanSetToDraft(int reimbursedAmountId)
        {
            var errors = new List<string>();
            var reimbursedAmount = this.Find(reimbursedAmountId);

            if (reimbursedAmount.CertReportId.HasValue)
            {
                errors.Add("Не можете да промените статуса на възстановената сума на 'Чернова', защото тя е включена в доклад по сертификация");
            }

            return errors;
        }

        public int GetContractId(int reimbursedAmountId)
        {
            return (from ra in this.unitOfWork.DbContext.Set<DebtReimbursedAmount>()
                    where ra.ReimbursedAmountId == reimbursedAmountId
                    select ra.ContractId).Single();
        }
    }
}
