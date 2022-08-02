using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReportCertCorrections.Repositories
{
    internal class ContractReportCertCorrectionsRepository : AggregateRepository<ContractReportCertCorrection>, IContractReportCertCorrectionsRepository
    {
        public ContractReportCertCorrectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportCertCorrection, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportCertCorrection, object>>[]
                {
                    cd => cd.Documents,
                };
            }
        }

        public IList<ContractReportCertCorrectionVO> GetContractReportCertCorrections(
            int[] programmeIds,
            ContractReportCertCorrectionType? type = null,
            ContractReportCertCorrectionStatus? status = null)
        {
            var predicate = PredicateBuilder.True<ContractReportCertCorrection>()
                .AndEquals(cd => cd.Type, type)
                .AndEquals(cd => cd.Status, status);

            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>().Where(predicate)
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where programmeIds.Contains(crc.ProgrammeId)
                    orderby crc.CreateDate descending
                    select new ContractReportCertCorrectionVO
                    {
                        ContractReportCertCorrectionId = crc.ContractReportCertCorrectionId,
                        ProgrammeName = pr.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                    }).ToList();
        }

        public ContractReportCertCorrectionInfoVO GetInfo(int contractReportCertCorrectionId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where crc.ContractReportCertCorrectionId == contractReportCertCorrectionId
                    select new ContractReportCertCorrectionInfoVO
                    {
                        ProgrammeCode = pr.Code,
                        Status = crc.Status,
                        StatusDescr = crc.Status,
                    }).Single();
        }

        public ContractReportCertCorrectionBasicDataVO GetBasicData(int contractReportCertCorrectionId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crc.ContractId equals c.ContractId into g0
                    from c in g0.DefaultIfEmpty()
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crc.ContractReportPaymentId equals crp.ContractReportPaymentId into g2
                    from crp in g2.DefaultIfEmpty()
                    join crpc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>() on crp.ContractReportPaymentId equals crpc.ContractReportPaymentId into g3
                    from crpc in g3.DefaultIfEmpty()
                    join crpca in this.unitOfWork.DbContext.Set<ContractReportPaymentCheckAmount>() on crpc.ContractReportPaymentCheckId equals crpca.ContractReportPaymentCheckId into g5
                    from crpca in g5.DefaultIfEmpty()
                    join u in this.unitOfWork.DbContext.Set<User>() on crc.CheckedByUserId equals u.UserId into g4
                    from u in g4.DefaultIfEmpty()

                    where crc.ContractReportCertCorrectionId == contractReportCertCorrectionId &&
                          (crp == null || crp.Status == ContractReportPaymentStatus.Actual) &&
                          (crpc == null || crpc.Status == ContractReportPaymentCheckStatus.Active)

                    group new
                    {
                        crpca.PaidBfpTotalAmount,
                    }
                    by new
                    {
                        ContractReportCertCorrectionId = crc.ContractReportCertCorrectionId,
                        RegNumber = crc.RegNumber,
                        Status = crc.Status,
                        Type = crc.Type,
                        IsActivated = crc.IsActivated,
                        DeleteNote = crc.DeleteNote,
                        ProgrammeId = crc.ProgrammeId,
                        ProgrammePriorityId = crc.ProgrammePriorityId,
                        ProcedureId = crc.ProcedureId,
                        ContractId = crc.ContractId,
                        ContractName = c.Name,
                        ContractRegNumber = c.RegNumber,
                        CompanyName = c.CompanyName,
                        CompanyUin = c.CompanyUin,
                        CompanyUinType = c.CompanyUinType,
                        ContractReportPaymentId = crp.ContractReportPaymentId,
                        PaymentVersionNum = crp.VersionNum,
                        RequestedAmount = crp.RequestedAmount,
                        PaymentCheckedDate = crpc.CheckedDate,
                        CheckedByUser = u != null ? u.Fullname + "(" + u.Username + ")" : null,
                        CheckedDate = crc.CheckedDate,
                        Version = crc.Version,
                    }
                    into g
                    select new ContractReportCertCorrectionBasicDataVO
                    {
                        ContractReportCertCorrectionId = g.Key.ContractReportCertCorrectionId,
                        RegNumber = g.Key.RegNumber,
                        Status = g.Key.Status,
                        Type = g.Key.Type,
                        IsActivated = g.Key.IsActivated,
                        DeleteNote = g.Key.DeleteNote,
                        ProgrammeId = g.Key.ProgrammeId,
                        ProgrammePriorityId = g.Key.ProgrammePriorityId,
                        ProcedureId = g.Key.ProcedureId,
                        ContractId = g.Key.ContractId,
                        ContractName = g.Key.ContractName,
                        ContractRegNumber = g.Key.ContractRegNumber,
                        CompanyName = g.Key.CompanyName,
                        CompanyUin = g.Key.CompanyUin,
                        CompanyUinType = g.Key.CompanyUinType,
                        ContractReportPaymentId = g.Key.ContractReportPaymentId,
                        PaymentVersionNum = g.Key.PaymentVersionNum,
                        RequestedAmount = g.Key.RequestedAmount,
                        PaidBfpTotalAmount = g.Sum(t => t.PaidBfpTotalAmount),
                        PaymentCheckedDate = g.Key.PaymentCheckedDate,
                        CheckedByUser = g.Key.CheckedByUser,
                        CheckedDate = g.Key.CheckedDate,
                        Version = g.Key.Version,
                    }).Single();
        }

        public IList<ContractReportCertCorrectionDocumentVO> GetDocuments(int contractReportCertCorrectionId)
        {
            return (from crcd in this.unitOfWork.DbContext.Set<ContractReportCertCorrectionDocument>()
                    where crcd.ContractReportCertCorrectionId == contractReportCertCorrectionId
                    orderby crcd.ContractReportCertCorrectionDocumentId descending
                    select new ContractReportCertCorrectionDocumentVO
                    {
                        DocumentId = crcd.ContractReportCertCorrectionDocumentId,
                        Description = crcd.Description,
                        File = new FileVO
                        {
                            Key = crcd.FileKey,
                            Name = crcd.FileName,
                        },
                    }).ToList();
        }

        public int GetProgrammeId(int contractReportCertCorrectionId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()
                    where crc.ContractReportCertCorrectionId == contractReportCertCorrectionId
                    select crc.ProgrammeId).Single();
        }

        public new void Remove(ContractReportCertCorrection contractReportCertCorrection)
        {
            if (contractReportCertCorrection.IsActivated || contractReportCertCorrection.Status != ContractReportCertCorrectionStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete ContractReportCertCorrection which is not in draft status or is activated.");
            }

            base.Remove(contractReportCertCorrection);
        }

        public bool HasCertContractReportCertCorrections(int certReportId)
        {
            return (from rcc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()
                    where rcc.CertReportId == certReportId
                    select rcc.ContractReportCertCorrectionId).Any();
        }

        public bool IsIncludedInCertReport(int contractReportCertCorrectionId)
        {
            return (from rcc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()
                    where rcc.ContractReportCertCorrectionId == contractReportCertCorrectionId
                    select rcc.CertReportId).Single().HasValue;
        }

        public IList<ContractReportCertCorrectionVO> GetContractReportCertCorrectionsForProjectDossier(int contractId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertCorrection>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where crc.ContractId == contractId && crc.Status != ContractReportCertCorrectionStatus.Draft
                    orderby crc.CreateDate descending
                    select new ContractReportCertCorrectionVO
                    {
                        ContractReportCertCorrectionId = crc.ContractReportCertCorrectionId,
                        ProgrammeName = pr.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                        BfpTotalAmount = crc.CertifiedBfpTotalAmount,
                        SelfAmount = crc.CertifiedSelfAmount,
                    }).ToList();
        }
    }
}
