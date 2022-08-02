using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.ActuallyPaidAmounts.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;

namespace Eumis.Data.ActuallyPaidAmounts.Repositories
{
    internal class ActuallyPaidAmountsRepository : AggregateRepository<ActuallyPaidAmount>, IActuallyPaidAmountsRepository
    {
        public ActuallyPaidAmountsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ActuallyPaidAmount, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ActuallyPaidAmount, object>>[]
                {
                    e => e.ActuallyPaidAmountDocuments.Select(f => f.File),
                };
            }
        }

        public IList<ActuallyPaidAmountVO> GetActuallyPaidAmounts(int[] programmeIds, int userId, int? contractId = null, PaymentReason? paymentReason = null)
        {
            var predicate = PredicateBuilder.True<ActuallyPaidAmount>()
                .AndEquals(pa => pa.ContractId, contractId)
                .AndEquals(pa => pa.PaymentReason, paymentReason);

            var externalVerificatorPaidAmounts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                 join c in this.unitOfWork.DbContext.Set<Contract>() on cu.ContractId equals c.ContractId
                                                 select c;

            return (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(predicate)
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(c => programmeIds.Contains(c.ProgrammeId)).Union(externalVerificatorPaidAmounts) on pa.ContractId equals c.ContractId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on pa.ProgrammeId equals pr.MapNodeId

                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on pa.ContractReportPaymentId equals crp.ContractReportPaymentId into g1
                    from crp in g1.DefaultIfEmpty()

                    orderby pa.CreateDate descending
                    select new ActuallyPaidAmountVO
                    {
                        AmountId = pa.ActuallyPaidAmountId,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        RegNumber = pa.RegNumber,
                        StatusDescr = pa.Status,
                        Status = pa.Status,
                        PaymentDate = pa.PaymentDate,
                        PaymentReason = pa.PaymentReason,
                        PaidBfpTotalAmount = pa.PaidBfpTotalAmount,
                        ContractReportPaymentNum = crp.VersionNum,
                        ContractReportPaymentType = crp.PaymentType,
                    })
                    .Distinct()
                    .ToList();
        }

        public ActuallyPaidAmountInfoVO GetInfo(int paidAmountId)
        {
            return (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on pa.ContractId equals c.ContractId
                    where pa.ActuallyPaidAmountId == paidAmountId
                    select new ActuallyPaidAmountInfoVO
                    {
                        ContractNum = c.RegNumber,
                        Status = pa.Status,
                        StatusDescr = pa.Status,
                    }).Single();
        }

        public ActuallyPaidAmountBasicDataVO GetBasicData(int paidAmountId)
        {
            return (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on pa.ContractId equals c.ContractId
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on pa.ContractReportPaymentId equals crp.ContractReportPaymentId into g1
                    from crp in g1.DefaultIfEmpty()
                    join crpc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>() on crp.ContractReportPaymentId equals crpc.ContractReportPaymentId into g2
                    from crpc in g2.DefaultIfEmpty()
                    join crpca in this.unitOfWork.DbContext.Set<ContractReportPaymentCheckAmount>() on crpc.ContractReportPaymentCheckId equals crpca.ContractReportPaymentCheckId into g3
                    from crpca in g3.DefaultIfEmpty()
                    where pa.ActuallyPaidAmountId == paidAmountId &&
                          (crp == null || crp.Status == ContractReportPaymentStatus.Actual) &&
                          (crpc == null || crpc.Status == ContractReportPaymentCheckStatus.Active)

                    group new
                    {
                        crpca.PaidBfpTotalAmount,
                    }
                    by new
                    {
                        PaidAmountId = pa.ActuallyPaidAmountId,
                        RegNumber = pa.RegNumber,
                        Status = pa.Status,
                        IsActivated = pa.IsActivated,
                        IsDeletedNote = pa.IsDeletedNote,
                        ProgrammeId = pa.ProgrammeId,
                        SapFileId = pa.SapFileId,
                        ContractName = c.Name,
                        ContractRegNumber = c.RegNumber,
                        CompanyName = c.CompanyName,
                        CompanyUin = c.CompanyUin,
                        CompanyUinType = c.CompanyUinType,
                        ContractReportPaymentId = pa.ContractReportPaymentId,
                        PaymentVersionNum = crp.VersionNum,
                        RequestedAmount = crp.RequestedAmount,
                        CheckedDate = crpc.CheckedDate,
                        Version = pa.Version,
                    }
                    into g
                    select new
                    {
                        PaidAmountId = g.Key.PaidAmountId,
                        RegNumber = g.Key.RegNumber,
                        Status = g.Key.Status,
                        IsActivated = g.Key.IsActivated,
                        IsDeletedNote = g.Key.IsDeletedNote,
                        ProgrammeId = g.Key.ProgrammeId,
                        ContractName = g.Key.ContractName,
                        ContractRegNumber = g.Key.ContractRegNumber,
                        CompanyName = g.Key.CompanyName,
                        CompanyUin = g.Key.CompanyUin,
                        CompanyUinType = g.Key.CompanyUinType,
                        ContractReportPaymentId = g.Key.ContractReportPaymentId,
                        PaymentVersionNum = (int?)g.Key.PaymentVersionNum,
                        RequestedAmount = g.Key.RequestedAmount,
                        PaidBfpTotalAmount = g.Sum(t => t.PaidBfpTotalAmount),
                        CheckedDate = g.Key.CheckedDate,
                        SapFileId = g.Key.SapFileId,
                        Version = g.Key.Version,
                    })
                    .ToList()
                    .Select(r => new ActuallyPaidAmountBasicDataVO
                    {
                        PaidAmountId = r.PaidAmountId,
                        RegNumber = r.RegNumber,
                        Status = r.Status,
                        IsActivated = r.IsActivated,
                        IsDeletedNote = r.IsDeletedNote,
                        ProgrammeId = r.ProgrammeId,
                        ContractName = r.ContractName,
                        ContractRegNumber = r.ContractRegNumber,
                        CompanyName = r.CompanyName,
                        CompanyUin = r.CompanyUin,
                        CompanyUinType = r.CompanyUinType,
                        ContractReportPaymentId = r.ContractReportPaymentId,
                        PaymentVersionNum = r.PaymentVersionNum,
                        RequestedAmount = r.RequestedAmount,
                        PaidBfpTotalAmount = r.PaidBfpTotalAmount,
                        CheckedDate = r.CheckedDate,
                        CreationType = r.SapFileId == null ? ActuallyPaidAmountCreationType.Manual : ActuallyPaidAmountCreationType.SAPImport,
                        Version = r.Version,
                    })
                    .Single();
        }

        public int GetProgrammeId(int paidAmountId)
        {
            return (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                    where pa.ActuallyPaidAmountId == paidAmountId
                    select pa.ProgrammeId).Single();
        }

        public int GetContractId(int paidAmountId)
        {
            return (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                    where pa.ActuallyPaidAmountId == paidAmountId
                    select pa.ContractId).Single();
        }

        public new void Remove(ActuallyPaidAmount paidAmount)
        {
            if (paidAmount.IsActivated || paidAmount.Status != ActuallyPaidAmountStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete ActuallyPaidAmount which is in draft status or is activated.");
            }

            base.Remove(paidAmount);
        }

        public IList<ActuallyPaidAmountVO> GetActuallyPaidAmountsForProjectDossier(int contractId)
        {
            return (from pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on pa.ContractId equals c.ContractId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on pa.ProgrammeId equals pr.MapNodeId

                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on pa.ContractReportPaymentId equals crp.ContractReportPaymentId into g1
                    from crp in g1.DefaultIfEmpty()

                    where pa.ContractId == contractId && pa.Status != ActuallyPaidAmountStatus.Draft
                    orderby pa.CreateDate descending
                    select new ActuallyPaidAmountVO
                    {
                        AmountId = pa.ActuallyPaidAmountId,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        RegNumber = pa.RegNumber,
                        StatusDescr = pa.Status,
                        Status = pa.Status,
                        PaymentDate = pa.PaymentDate,
                        PaymentReason = pa.PaymentReason,
                        PaidBfpTotalAmount = pa.PaidBfpTotalAmount,
                        ContractReportPaymentNum = crp.VersionNum,
                        ContractReportPaymentType = crp.PaymentType,
                    }).ToList();
        }

        public IList<ActuallyPaidAmountDocumentVO> GetActuallyPaidAmountDocuments(int paymentAmountId)
        {
            var result = (from apad in this.unitOfWork.DbContext.Set<ActuallyPaidAmountDocument>()
                          join b in this.unitOfWork.DbContext.Set<Blob>() on apad.BlobKey equals b.Key into g1
                          from b in g1.DefaultIfEmpty()
                          where apad.ActuallyPaidAmountId == paymentAmountId
                          select new
                          {
                              ActuallyPaidAmountId = apad.ActuallyPaidAmountId,
                              DocumentId = apad.ActuallyPaidAmountDocumentId,
                              Name = apad.Name,
                              Description = apad.Description,
                              FileKey = b.Key,
                              FileName = b.FileName,
                          }).ToList();

            return result.Select(p => new ActuallyPaidAmountDocumentVO
            {
                ActuallyPaidAmountId = p.ActuallyPaidAmountId,
                DocumentId = p.DocumentId,
                Name = p.Name,
                Description = p.Description,
                File = (p.FileKey == null) ? null : new FileVO
                {
                    Key = p.FileKey,
                    Name = p.FileName,
                },
            }).ToList();
        }
    }
}
