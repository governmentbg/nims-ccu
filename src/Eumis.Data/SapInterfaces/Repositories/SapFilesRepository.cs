using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.SapInterfaces.ViewObjects;
using Eumis.Domain.SapInterfaces;
using Eumis.Domain.Users;

namespace Eumis.Data.SapInterfaces.Repositories
{
    internal class SapFilesRepository : AggregateRepository<SapFile>, ISapFilesRepository
    {
        public SapFilesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<SapFileVO> GetSapFiles(SapFileStatus? status = null, SapFileType? type = null)
        {
            var predicate = PredicateBuilder.True<SapFile>()
                .AndEquals(sf => sf.Status, status)
                .AndEquals(sf => sf.Type, type);

            return (from sf in this.unitOfWork.DbContext.Set<SapFile>().Where(predicate)
                    join u in this.unitOfWork.DbContext.Set<User>() on sf.CreatedByUserId equals u.UserId
                    orderby sf.CreateDate descending
                    select new
                    {
                        sf.SapFileId,
                        sf.Status,
                        sf.Type,
                        sf.CreateDate,
                        u.Username,
                        u.Fullname,
                    }).ToList()
                    .Select(o => new SapFileVO
                    {
                        SapFileId = o.SapFileId,
                        Status = o.Status,
                        Type = o.Type,
                        CreateDate = o.CreateDate,
                        CreatedByUser = o.Fullname + " (" + o.Username + ")",
                    }).ToList();
        }

        public IList<SapPaidAmount> GetCorrectPaidAmounts(int sapFileId)
        {
            return (from pa in this.unitOfWork.DbContext.Set<SapPaidAmount>()
                    where pa.SapFileId == sapFileId && !pa.HasError
                    select pa).ToList();
        }

        public IList<SapDistributedLimit> GetCorrectDistributedLimits(int sapFileId)
        {
            return (from dl in this.unitOfWork.DbContext.Set<SapDistributedLimit>()
                    where dl.SapFileId == sapFileId && !dl.HasError
                    select dl).ToList();
        }

        public IList<SapFilePaidAmountVO> GetSapPaidAmounts(int sapFileId)
        {
            return (from pa in this.unitOfWork.DbContext.Set<SapPaidAmount>()
                    where pa.SapFileId == sapFileId
                    select new
                    {
                        pa.SapPaidAmountId,
                        pa.ContractSapNum,
                        pa.Fund,
                        pa.ContractReportPaymentNum,
                        pa.ContractReportPaymentDate,
                        pa.PaidBfpBgAmount,
                        pa.PaidBfpEuAmount,
                        pa.PaymentType,
                        pa.Comment,
                        pa.HasWarning,
                        pa.Warnings,
                        pa.HasError,
                        pa.Errors,
                        pa.IsImported,
                        pa.ActuallyPaidAmountId,
                        pa.ReimbursedAmountId,
                    })
                    .ToList()
                    .Select(pa => new SapFilePaidAmountVO
                    {
                        SapPaidAmountId = pa.SapPaidAmountId,
                        ContractSapNum = pa.ContractSapNum,
                        Fund = pa.Fund,
                        ContractReportPaymentNum = pa.ContractReportPaymentNum,
                        ContractReportPaymentDate = pa.ContractReportPaymentDate,
                        PaidAmount = pa.PaidBfpBgAmount + pa.PaidBfpEuAmount,
                        PaymentType = pa.PaymentType,
                        Comment = pa.Comment,
                        HasWarning = pa.HasWarning,
                        Warnings = pa.Warnings.Split(new string[] { SapPaidAmount.ERRORS_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                        HasError = pa.HasError,
                        Errors = pa.Errors.Split(new string[] { SapPaidAmount.ERRORS_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                        IsImported = pa.IsImported,
                        ActuallyPaidAmountId = pa.ActuallyPaidAmountId,
                        ReimbursedAmountId = pa.ReimbursedAmountId,
                    })
                    .ToList();
        }

        public IList<SapFileDistributedLimitVO> GetSapDistributedLimits(int sapFileId)
        {
            return (from pa in this.unitOfWork.DbContext.Set<SapDistributedLimit>()
                    where pa.SapFileId == sapFileId
                    select new
                    {
                        pa.SapDistributedLimitId,
                        pa.ContractSapNum,
                        pa.ContractReportPaymentNum,
                        pa.ContractReportPaymentDate,
                        pa.PaidBfpBgAmount,
                        pa.PaidBfpEuAmount,
                        pa.PaymentType,
                        pa.Comment,
                        pa.HasWarning,
                        pa.Warnings,
                        pa.HasError,
                        pa.Errors,
                        pa.IsImported,
                        pa.ActuallyPaidAmountId,
                    })
                    .ToList()
                    .Select(pa => new SapFileDistributedLimitVO
                    {
                        SapDistributedLimitId = pa.SapDistributedLimitId,
                        ContractSapNum = pa.ContractSapNum,
                        ContractReportPaymentNum = pa.ContractReportPaymentNum,
                        ContractReportPaymentDate = pa.ContractReportPaymentDate,
                        PaidAmount = pa.PaidBfpBgAmount + pa.PaidBfpEuAmount,
                        PaymentType = pa.PaymentType,
                        Comment = pa.Comment,
                        HasWarning = pa.HasWarning,
                        Warnings = pa.Warnings.Split(new string[] { SapPaidAmount.ERRORS_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                        HasError = pa.HasError,
                        Errors = pa.Errors.Split(new string[] { SapPaidAmount.ERRORS_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                        IsImported = pa.IsImported,
                        ActuallyPaidAmountId = pa.ActuallyPaidAmountId,
                    })
                    .ToList();
        }

        public SapFileInfoVO GetSapFileInfo(int sapFileId)
        {
            return (from sf in this.unitOfWork.DbContext.Set<SapFile>()
                    where sf.SapFileId == sapFileId
                    select new SapFileInfoVO
                    {
                        SapFileId = sf.SapFileId,
                        Status = sf.Status,
                        StatusDescr = sf.Status,
                        FileName = sf.FileName,
                        Type = sf.Type,
                        TypeDescr = sf.Type,
                    }).Single();
        }
    }
}
