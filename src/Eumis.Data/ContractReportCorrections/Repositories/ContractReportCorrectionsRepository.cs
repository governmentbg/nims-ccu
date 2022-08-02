using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.CertReports;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReportCorrections.Repositories
{
    internal class ContractReportCorrectionsRepository : AggregateRepository<ContractReportCorrection>, IContractReportCorrectionsRepository
    {
        public ContractReportCorrectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportCorrection, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportCorrection, object>>[]
                {
                    cd => cd.Documents,
                };
            }
        }

        public IList<ContractReportCorrection> FindAllByCertReport(int certReportId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId).ToList();
        }

        public IList<ContractReportCorrectionVO> GetContractReportCorrections(
            int[] programmeIds,
            int userId,
            ContractReportCorrectionType? type = null,
            ContractReportCorrectionStatus? status = null)
        {
            var predicate = PredicateBuilder.True<ContractReportCorrection>()
                .AndEquals(cd => cd.Type, type)
                .AndEquals(cd => cd.Status, status);

            var programmePredicate = predicate.And(cd => programmeIds.Contains(cd.ProgrammeId));

            var externalUserCorrections = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                          join crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>().Where(predicate) on cu.ContractId equals crc.ContractId
                                          select crc;

            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>().Where(programmePredicate).Union(externalUserCorrections)
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    orderby crc.CreateDate descending
                    select new ContractReportCorrectionVO
                    {
                        ContractReportCorrectionId = crc.ContractReportCorrectionId,
                        ProgrammeName = pr.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                        CorrectedApprovedBfpTotalAmount = (int)crc.Sign * crc.CorrectedApprovedBfpTotalAmount,
                        CorrectedApprovedSelfAmount = (int)crc.Sign * crc.CorrectedApprovedSelfAmount,
                    })
                    .Distinct()
                    .ToList();
        }

        public ContractReportCorrectionInfoVO GetInfo(int contractReportCorrectionId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where crc.ContractReportCorrectionId == contractReportCorrectionId
                    select new ContractReportCorrectionInfoVO
                    {
                        ProgrammeCode = pr.Code,
                        Status = crc.Status,
                        StatusDescr = crc.Status,
                    }).Single();
        }

        public ContractReportCorrectionBasicDataVO GetBasicData(int contractReportCorrectionId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
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

                    where crc.ContractReportCorrectionId == contractReportCorrectionId &&
                          (crp == null || crp.Status == ContractReportPaymentStatus.Actual) &&
                          (crpc == null || crpc.Status == ContractReportPaymentCheckStatus.Active)

                    group new
                    {
                        crpca.PaidBfpTotalAmount,
                    }
                    by new
                    {
                        ContractReportCorrectionId = crc.ContractReportCorrectionId,
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
                    select new ContractReportCorrectionBasicDataVO
                    {
                        ContractReportCorrectionId = g.Key.ContractReportCorrectionId,
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

        public IList<ContractReportCorrectionDocumentVO> GetDocuments(int contractReportCorrectionId)
        {
            return (from crcd in this.unitOfWork.DbContext.Set<ContractReportCorrectionDocument>()
                    where crcd.ContractReportCorrectionId == contractReportCorrectionId
                    orderby crcd.ContractReportCorrectionDocumentId descending
                    select new ContractReportCorrectionDocumentVO
                    {
                        DocumentId = crcd.ContractReportCorrectionDocumentId,
                        Description = crcd.Description,
                        File = new FileVO
                        {
                            Key = crcd.FileKey,
                            Name = crcd.FileName,
                        },
                    }).ToList();
        }

        public int GetProgrammeId(int contractReportCorrectionId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    where crc.ContractReportCorrectionId == contractReportCorrectionId
                    select crc.ProgrammeId).Single();
        }

        public new void Remove(ContractReportCorrection contractReportCorrection)
        {
            if (contractReportCorrection.IsActivated || contractReportCorrection.Status != ContractReportCorrectionStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete ContractReportCorrection which is not in draft status or is activated.");
            }

            base.Remove(contractReportCorrection);
        }

        public bool HasCertDraftContractReportCorrections(int certReportId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    where crc.CertReportId == certReportId && crc.CertStatus == ContractReportCorrectionCertStatus.Draft
                    select crc.ContractReportCorrectionId).Any();
        }

        public bool HasCertContractReportCorrections(int certReportId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    where crc.CertReportId == certReportId
                    select crc.ContractReportCorrectionId).Any();
        }

        public bool IsIncludedInCertReport(int contractReportCorrectionId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    where crc.ContractReportCorrectionId == contractReportCorrectionId
                    select crc.CertReportId).Single().HasValue;
        }

        public IList<ContractReportCorrectionVO> GetContractReportCorrectionsForProjectDossier(int contractId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where crc.ContractId == contractId && crc.Status != ContractReportCorrectionStatus.Draft
                    orderby crc.CreateDate descending
                    select new ContractReportCorrectionVO
                    {
                        ContractReportCorrectionId = crc.ContractReportCorrectionId,
                        ProgrammeName = pr.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                        CorrectedApprovedBfpTotalAmount = crc.CorrectedApprovedBfpTotalAmount,
                        CorrectedApprovedSelfAmount = crc.CorrectedApprovedSelfAmount,
                    }).ToList();
        }

        public IList<ContractReportCertifiedAmountCorrectionVO> GetContractReportCertifiedAmountCorrectionsForProjectDossier(int contractId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCorrection>()
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crc.ContractReportPaymentId equals crp.ContractReportPaymentId
                    join contR in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals contR.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crc.ContractId equals c.ContractId
                    join certR in this.unitOfWork.DbContext.Set<CertReport>() on crc.CertReportId equals certR.CertReportId
                    where crc.ContractId == contractId && crc.Status != ContractReportCorrectionStatus.Draft && (certR.Status == CertReportStatus.Approved || certR.Status == CertReportStatus.PartialyApproved)
                    select new ContractReportCertifiedAmountCorrectionVO
                    {
                        ContractReportCorrectionId = crc.ContractReportCorrectionId,
                        ContractReportId = crp.ContractReportId,
                        ContractId = crp.ContractId,
                        ContractRegNum = c.RegNumber,
                        ReportOrderNum = contR.OrderNum,
                        RegNumber = crc.RegNumber,
                        CertifiedCorrectedApprovedBfpTotalAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedBfpTotalAmount,
                        CertifiedCorrectedApprovedSelfAmount = (int)crc.Sign * crc.CertifiedCorrectedApprovedSelfAmount,
                        CertReportNumber = certR.CertReportNumber,
                        Description = crc.Description,
                    }).ToList();
        }

        public int? GetContractId(int contractReportCorrectionId)
        {
            return this.Set()
                .Where(x => x.ContractReportCorrectionId == contractReportCorrectionId)
                .Select(x => x.ContractId)
                .Single();
        }
    }
}
