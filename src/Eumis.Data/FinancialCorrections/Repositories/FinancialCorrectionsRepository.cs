using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.FinancialCorrections.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;

namespace Eumis.Data.FinancialCorrections.Repositories
{
    internal class FinancialCorrectionsRepository : AggregateRepository<FinancialCorrection>, IFinancialCorrectionsRepository
    {
        public FinancialCorrectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<FinancialCorrectionVO> GetFinancialCorrections(int[] programmeIds, int userId)
        {
            var externalVerificatorCorrections = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                 join c in this.unitOfWork.DbContext.Set<Contract>() on cu.ContractId equals c.ContractId
                                                 select c;

            return (from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(x => programmeIds.Contains(x.ProgrammeId)).Union(externalVerificatorCorrections) on fc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on fc.ContractContractId equals cc.ContractContractId into g1
                    from cc in g1.DefaultIfEmpty()

                    join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into g2
                    from cctor in g2.DefaultIfEmpty()

                    join cbla in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fc.ContractBudgetLevel3AmountId equals cbla.ContractBudgetLevel3AmountId into g3
                    from cbla in g3.DefaultIfEmpty()

                    select new { fc, c, cc, cctor, cbla })
                .ToList()
                .Select(t => new FinancialCorrectionVO()
                {
                    FinancialCorrectionId = t.fc.FinancialCorrectionId,
                    OrderNum = t.fc.OrderNum,
                    Status = t.fc.Status,
                    ImpositionDate = t.fc.ImpositionDate,
                    ContractName = t.c.Name,
                    ContractRegNumber = t.c.RegNumber,
                    ContractCompany = string.Format("{0} ({1}: {2})", t.c.CompanyName, t.c.CompanyUinType.GetEnumDescription(), t.c.CompanyUin),
                    ContractContractNumber = t.cc != null ? t.cc.Number : null,
                    ContractContractorCompany = t.cctor != null ? string.Format("{0} ({1}: {2})", t.cctor.Name, t.cctor.UinType.GetEnumDescription(), t.cctor.Uin) : null,
                    ContractBudgetLevel3Name = t.cbla != null ? t.cbla.Code + " " + t.cbla.Name : null,
                })
                .Distinct()
                .ToList();
        }

        public FinancialCorrectionInfoVO GetInfo(int financialCorrectionId)
        {
            return (from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                    where fc.FinancialCorrectionId == financialCorrectionId
                    select new FinancialCorrectionInfoVO
                    {
                        FinancialCorrectionId = fc.FinancialCorrectionId,
                        ContractId = fc.ContractId,
                        OrderNum = fc.OrderNum,
                        Status = fc.Status,
                    }).Single();
        }

        public int GetNextOrderNumber(int cotractId)
        {
            var lastOrderNumber = this.Set()
                .Where(p => p.ContractId == cotractId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public int GetContractId(int financialCorrectionId)
        {
            return this.unitOfWork.DbContext.Set<FinancialCorrection>()
                .Where(t => t.FinancialCorrectionId == financialCorrectionId)
                .Select(t => t.ContractId)
                .Single();
        }

        public new void Remove(FinancialCorrection financialCorrection)
        {
            if (financialCorrection.Status != FinancialCorrectionStatus.New)
            {
                throw new DomainValidationException("Cannot delete financial correction with status different from new.");
            }

            base.Remove(financialCorrection);
        }

        public IList<FinancialCorrectionVO> GetFinancialCorrectionsForProjectDossier(int contractId)
        {
            return (from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on fc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on fc.ContractContractId equals cc.ContractContractId into g1
                    from cc in g1.DefaultIfEmpty()

                    join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into g2
                    from cctor in g2.DefaultIfEmpty()

                    join fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>().Where(fcv => fcv.Status == FinancialCorrectionVersionStatus.Actual) on fc.FinancialCorrectionId equals fcv.FinancialCorrectionId into g3
                    from fcv in g3.DefaultIfEmpty()

                    join ir in this.unitOfWork.DbContext.Set<FinancialCorrectionImposingReason>() on fcv.FinancialCorrectionImposingReasonId equals ir.FinancialCorrectionImposingReasonId into g4
                    from ir in g4.DefaultIfEmpty()

                    where fc.ContractId == contractId && fc.Status != FinancialCorrectionStatus.New
                    select new
                    {
                        FinancialCorrectionId = fc.FinancialCorrectionId,
                        FinancialCorrectionOrderNum = fc.OrderNum,
                        ContractRegNumber = c.RegNumber,
                        ImpositionDate = fc.ImpositionDate,
                        ContractContractNumber = cc != null ? cc.Number : null,
                        ContractContractSignDate = cc != null ? cc.SignDate : (DateTime?)null,
                        ContractContractorUinType = cctor != null ? cctor.UinType : (UinType?)null,
                        ContractContractorUin = cctor != null ? cctor.Uin : null,
                        ContractContractorName = cctor != null ? cctor.Name : null,
                        ContractName = c.Name,
                        FinancialCorrectionVersionOrderNum = fcv != null ? fcv.OrderNum : (int?)null,
                        FinancialCorrectionVersionPercent = fcv != null ? fcv.Percent : (decimal?)null,
                        FinancialCorrectionVersionTotalAmount = fcv != null ? fcv.TotalAmount : (decimal?)null,
                        FinancialCorrectionImposingReason = ir.Name,
                    })
                    .ToList()
                    .Select(fc => new FinancialCorrectionVO()
                    {
                        FinancialCorrectionId = fc.FinancialCorrectionId,
                        OrderNum = fc.FinancialCorrectionOrderNum,
                        ContractRegNumber = fc.ContractRegNumber,
                        ImpositionDate = fc.ImpositionDate,
                        ContractContractNumber = fc.ContractContractNumber,
                        ContractContractorCompany = fc.ContractContractorName != null ? string.Format(
                            "{0: dd.MM.yyyy} ({1}: {2}) {3}",
                            fc.ContractContractSignDate.Value,
                            fc.ContractContractorUinType.Value.GetEnumDescription(),
                            fc.ContractContractorUin,
                            fc.ContractContractorName) : null,
                        ContractName = fc.ContractName,
                        FinancialCorrectionVersionOrderNum = fc.FinancialCorrectionVersionOrderNum != null ? string.Format(
                            "{0}.{1}",
                            fc.FinancialCorrectionOrderNum,
                            fc.FinancialCorrectionVersionOrderNum.Value) :
                            fc.FinancialCorrectionOrderNum.ToString(),
                        FinancialCorrectionVersionPercent = fc.FinancialCorrectionVersionPercent,
                        FinancialCorrectionVersionTotalAmount = fc.FinancialCorrectionVersionTotalAmount,
                        ImposingReason = fc.FinancialCorrectionImposingReason,
                    })
                    .OrderByDescending(fc => fc.FinancialCorrectionVersionOrderNum)
                    .ToList();
        }
    }
}
