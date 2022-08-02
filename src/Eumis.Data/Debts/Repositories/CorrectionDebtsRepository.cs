using Eumis.Common.Db;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Debts;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Debts.Repositories
{
    internal class CorrectionDebtsRepository : AggregateRepository<CorrectionDebt>, ICorrectionDebtsRepository
    {
        public CorrectionDebtsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<CorrectionDebtVO> GetCorrectionDebts(int[] programmeIds)
        {
            return (from cd in this.unitOfWork.DbContext.Set<CorrectionDebt>()
                    join ffc in this.unitOfWork.DbContext.Set<FlatFinancialCorrection>() on cd.FlatFinancialCorrectionId equals ffc.FlatFinancialCorrectionId
                    join cdv in this.unitOfWork.DbContext.Set<CorrectionDebtVersion>().Where(v => v.Status == CorrectionDebtVersionStatus.Actual) on cd.CorrectionDebtId equals cdv.CorrectionDebtId into g0
                    from cdv in g0.DefaultIfEmpty()
                    where programmeIds.Contains(ffc.ProgrammeId)
                    select new
                    {
                        CorrectionDebtId = cd.CorrectionDebtId,
                        CorrectionRegNumber = ffc.OrderNum,
                        RegNumber = cd.RegNumber,
                        RegDate = cd.RegDate,
                        ModifyDate = (DateTime?)cdv.ModifyDate,
                        cdv.DebtEuAmount,
                        cdv.DebtBgAmount,
                        cdv.DebtBfpAmount,
                        cdv.CertEuAmount,
                        cdv.CertBgAmount,
                        cdv.CertBfpAmount,
                        cdv.ReimbursedEuAmount,
                        cdv.ReimbursedBgAmount,
                        cdv.ReimbursedBfpAmount,
                    })
                .ToList()
                .Select(c => new CorrectionDebtVO
                {
                    CorrectionDebtId = c.CorrectionDebtId,
                    CorrectionRegNumber = c.CorrectionRegNumber.ToString(),
                    RegNumber = c.RegNumber,
                    RegDate = c.RegDate,
                    ModifyDate = c.ModifyDate,
                    DebtEuAmount = c.DebtEuAmount,
                    DebtBgAmount = c.DebtBgAmount,
                    DebtTotalAmount = c.DebtBfpAmount,
                    CertEuAmount = c.CertEuAmount,
                    CertBgAmount = c.CertBgAmount,
                    CertTotalAmount = c.CertBfpAmount,
                    ReimbursedEuAmount = c.ReimbursedEuAmount,
                    ReimbursedBgAmount = c.ReimbursedBgAmount,
                    ReimbursedTotalAmount = c.ReimbursedBfpAmount,
                }).ToList();
        }

        public int GetFlatFinancialCorrectionId(int correctionDebtId)
        {
            return this.unitOfWork.DbContext.Set<CorrectionDebt>()
                .Where(t => t.CorrectionDebtId == correctionDebtId)
                .Select(t => t.FlatFinancialCorrectionId)
                .Single();
        }

        public IList<CorrectionDebtReportVO> GetCorrectionDebtReport(int[] programmeIds)
        {
            return (from cd in this.unitOfWork.DbContext.Set<CorrectionDebt>()
                    join cdv in this.unitOfWork.DbContext.Set<CorrectionDebtVersion>() on cd.CorrectionDebtId equals cdv.CorrectionDebtId
                    join fc in this.unitOfWork.DbContext.Set<FlatFinancialCorrection>() on cd.FlatFinancialCorrectionId equals fc.FlatFinancialCorrectionId
                    where cd.Status == CorrectionDebtStatus.Entered && cdv.Status == CorrectionDebtVersionStatus.Actual && programmeIds.Contains(fc.ProgrammeId)
                    group new
                    {
                        cdv.DebtBgAmount,
                        cdv.DebtEuAmount,
                        cdv.ReimbursedBgAmount,
                        cdv.ReimbursedEuAmount,
                    }
                    by
                    new
                    {
                        fc.OrderNum,
                        fc.ImpositionDate,
                        fc.ImpositionNumber,
                        fc.Level,
                    }
                    into g
                    select new
                    {
                        CorrectionOrderNum = g.Key.OrderNum,
                        CorrectionImpositionDate = g.Key.ImpositionDate,
                        CorrectionImpositionNumber = g.Key.ImpositionNumber,
                        CorrectionLevel = g.Key.Level,
                        DebtBgAmount = g.Sum(i => i.DebtBgAmount),
                        DebtEuAmount = g.Sum(i => i.DebtEuAmount),
                        ReimbursedBgAmount = g.Sum(i => i.ReimbursedBgAmount),
                        ReimbursedEuAmount = g.Sum(i => i.ReimbursedEuAmount),
                    })
                   .AsEnumerable()
                   .Select(c => new CorrectionDebtReportVO
                   {
                       CorrectionOrderNum = c.CorrectionOrderNum,
                       CorrectionImpositionDate = c.CorrectionImpositionDate,
                       CorrectionImpositionNumber = c.CorrectionImpositionNumber,
                       CorrectionLevel = c.CorrectionLevel,
                       DebtBgAmount = c.DebtBgAmount ?? 0m,
                       DebtEuAmount = c.DebtEuAmount ?? 0m,
                       DebtTotalAmount = (c.DebtBgAmount ?? 0m) + (c.DebtEuAmount ?? 0m),
                       ReimbursedBgAmount = c.ReimbursedBgAmount ?? 0m,
                       ReimbursedEuAmount = c.ReimbursedEuAmount ?? 0m,
                       ReimbursedTotalAmount = (c.ReimbursedBgAmount ?? 0m) + (c.ReimbursedEuAmount ?? 0m),
                       RemainingBgAmount = (c.DebtBgAmount ?? 0m) - (c.ReimbursedBgAmount ?? 0m),
                       RemainingEuAmount = (c.DebtEuAmount ?? 0m) - (c.ReimbursedEuAmount ?? 0m),
                       RemainingTotalAmount = (c.DebtBgAmount ?? 0m) + (c.DebtEuAmount ?? 0m) - (c.ReimbursedBgAmount ?? 0m) - (c.ReimbursedEuAmount ?? 0m),
                   })
                   .ToList();
        }

        public new void Remove(CorrectionDebt correctionDebt)
        {
            if (correctionDebt.Status != CorrectionDebtStatus.New)
            {
                throw new DomainValidationException("Cannot delete correction debt with status different from new.");
            }

            base.Remove(correctionDebt);
        }

        public IList<CorrectionDebtVO> GetCorrectionDebtsForProjectDossier(int contractId)
        {
            return (
                from cd in this.unitOfWork.DbContext.Set<CorrectionDebt>()
                join fc in this.unitOfWork.DbContext.Set<FlatFinancialCorrection>() on cd.FlatFinancialCorrectionId equals fc.FlatFinancialCorrectionId
                join c in this.unitOfWork.DbContext.Set<Contract>() on fc.ProgrammeId equals c.ProgrammeId
                join cdv in this.unitOfWork.DbContext.Set<CorrectionDebtVersion>() on cd.CorrectionDebtId equals cdv.CorrectionDebtId into g0
                from cdv in g0.DefaultIfEmpty()
                where cd.Status == CorrectionDebtStatus.Entered && cdv.Status == CorrectionDebtVersionStatus.Actual && c.ContractId == contractId
                select new CorrectionDebtVO
                {
                    CorrectionDebtId = cd.CorrectionDebtId,
                    OrderNum = cdv.OrderNum,
                    CorrectionRegNumber = c.RegNumber,
                    RegNumber = cd.RegNumber,
                    RegDate = cd.RegDate,
                    ModifyDate = (DateTime?)cdv.ModifyDate,
                    DebtTotalAmount = (cdv.DebtBgAmount ?? 0m) + (cdv.DebtEuAmount ?? 0m),
                    CertTotalAmount = (cdv.CertBgAmount ?? 0m) + (cdv.CertEuAmount ?? 0m),
                    ReimbursedTotalAmount = (cdv.ReimbursedBgAmount ?? 0m) + (cdv.ReimbursedEuAmount ?? 0m),
                })
                .ToList();
        }
    }
}
