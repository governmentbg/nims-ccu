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

namespace Eumis.Data.ContractReportRevalidations.Repositories
{
    internal class ContractReportRevalidationsRepository : AggregateRepository<ContractReportRevalidation>, IContractReportRevalidationsRepository
    {
        public ContractReportRevalidationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportRevalidation, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportRevalidation, object>>[]
                {
                    cd => cd.Documents,
                };
            }
        }

        public IList<ContractReportRevalidation> FindAllByCertReport(int certReportId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId).ToList();
        }

        public IList<ContractReportRevalidationVO> GetContractReportRevalidations(
            int[] programmeIds,
            ContractReportRevalidationType? type = null,
            ContractReportRevalidationStatus? status = null)
        {
            var predicate = PredicateBuilder.True<ContractReportRevalidation>()
                .AndEquals(cd => cd.Type, type)
                .AndEquals(cd => cd.Status, status);

            return (from crc in this.unitOfWork.DbContext.Set<ContractReportRevalidation>().Where(predicate)
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where programmeIds.Contains(crc.ProgrammeId)
                    orderby crc.CreateDate descending
                    select new ContractReportRevalidationVO
                    {
                        ContractReportRevalidationId = crc.ContractReportRevalidationId,
                        ProgrammeName = pr.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                    }).ToList();
        }

        public ContractReportRevalidationInfoVO GetInfo(int contractReportRevalidationId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where crc.ContractReportRevalidationId == contractReportRevalidationId
                    select new ContractReportRevalidationInfoVO
                    {
                        ProgrammeCode = pr.Code,
                        Status = crc.Status,
                        StatusDescr = crc.Status,
                    }).Single();
        }

        public ContractReportRevalidationBasicDataVO GetBasicData(int contractReportRevalidationId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
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

                    where crc.ContractReportRevalidationId == contractReportRevalidationId &&
                          (crp == null || crp.Status == ContractReportPaymentStatus.Actual) &&
                          (crpc == null || crpc.Status == ContractReportPaymentCheckStatus.Active)

                    group new
                    {
                        crpca.PaidBfpTotalAmount,
                    }
                    by new
                    {
                        ContractReportRevalidationId = crc.ContractReportRevalidationId,
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
                    select new ContractReportRevalidationBasicDataVO
                    {
                        ContractReportRevalidationId = g.Key.ContractReportRevalidationId,
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

        public IList<ContractReportRevalidationDocumentVO> GetDocuments(int contractReportRevalidationId)
        {
            return (from crcd in this.unitOfWork.DbContext.Set<ContractReportRevalidationDocument>()
                    where crcd.ContractReportRevalidationId == contractReportRevalidationId
                    orderby crcd.ContractReportRevalidationDocumentId descending
                    select new ContractReportRevalidationDocumentVO
                    {
                        DocumentId = crcd.ContractReportRevalidationDocumentId,
                        Description = crcd.Description,
                        File = new FileVO
                        {
                            Key = crcd.FileKey,
                            Name = crcd.FileName,
                        },
                    }).ToList();
        }

        public int GetProgrammeId(int contractReportRevalidationId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
                    where crc.ContractReportRevalidationId == contractReportRevalidationId
                    select crc.ProgrammeId).Single();
        }

        public new void Remove(ContractReportRevalidation contractReportRevalidation)
        {
            if (contractReportRevalidation.IsActivated || contractReportRevalidation.Status != ContractReportRevalidationStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete ContractReportRevalidation which is not in draft status or is activated.");
            }

            base.Remove(contractReportRevalidation);
        }

        public bool HasCertDraftContractReportRevalidations(int certReportId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
                    where crc.CertReportId == certReportId && crc.CertStatus == ContractReportRevalidationCertStatus.Draft
                    select crc.ContractReportRevalidationId).Any();
        }

        public bool HasCertContractReportRevalidations(int certReportId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
                    where crc.CertReportId == certReportId
                    select crc.ContractReportRevalidationId).Any();
        }

        public bool IsIncludedInCertReport(int contractReportRevalidationId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
                    where crc.ContractReportRevalidationId == contractReportRevalidationId
                    select crc.CertReportId).Single().HasValue;
        }

        public IList<ContractReportRevalidationVO> GetContractReportRevalidationsForProjectDossier(int contractId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportRevalidation>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where crc.ContractId == contractId && crc.Status != ContractReportRevalidationStatus.Draft
                    orderby crc.CreateDate descending
                    select new ContractReportRevalidationVO
                    {
                        ContractReportRevalidationId = crc.ContractReportRevalidationId,
                        ProgrammeName = pr.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                        BfpTotalAmount = crc.RevalidatedBfpTotalAmount,
                        SelfAmount = crc.RevalidatedSelfAmount,
                    }).ToList();
        }
    }
}
