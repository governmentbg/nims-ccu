using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ContractReportCertAuthorityCorrections.Repositories
{
    internal class ContractReportCertAuthorityCorrectionsRepository : AggregateRepository<ContractReportCertAuthorityCorrection>, IContractReportCertAuthorityCorrectionsRepository
    {
        public ContractReportCertAuthorityCorrectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractReportCertAuthorityCorrection, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractReportCertAuthorityCorrection, object>>[]
                {
                    cd => cd.Documents,
                };
            }
        }

        public IList<ContractReportCertAuthorityCorrectionVO> GetContractReportCertAuthorityCorrections(
            int[] programmeIds,
            ContractReportCertAuthorityCorrectionType? type = null,
            ContractReportCertAuthorityCorrectionStatus? status = null)
        {
            var predicate = PredicateBuilder.True<ContractReportCertAuthorityCorrection>()
                .AndEquals(cd => cd.Type, type)
                .AndEquals(cd => cd.Status, status);

            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>().Where(predicate)
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where programmeIds.Contains(crc.ProgrammeId)
                    orderby crc.CreateDate descending
                    select new ContractReportCertAuthorityCorrectionVO
                    {
                        ContractReportCertAuthorityCorrectionId = crc.ContractReportCertAuthorityCorrectionId,
                        ProgrammeName = pr.Name,
                        RegNumber = crc.RegNumber,
                        StatusDescr = crc.Status,
                        Status = crc.Status,
                        Type = crc.Type,
                        Date = crc.Date,
                    }).ToList();
        }

        public ContractReportCertAuthorityCorrectionInfoVO GetInfo(int contractReportCertAuthorityCorrectionId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on crc.ProgrammeId equals pr.MapNodeId
                    where crc.ContractReportCertAuthorityCorrectionId == contractReportCertAuthorityCorrectionId
                    select new ContractReportCertAuthorityCorrectionInfoVO
                    {
                        ProgrammeCode = pr.Code,
                        Status = crc.Status,
                        StatusDescr = crc.Status,
                    }).Single();
        }

        public ContractReportCertAuthorityCorrectionBasicDataVO GetBasicData(int contractReportCertAuthorityCorrectionId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>()
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

                    where crc.ContractReportCertAuthorityCorrectionId == contractReportCertAuthorityCorrectionId &&
                          (crp == null || crp.Status == ContractReportPaymentStatus.Actual) &&
                          (crpc == null || crpc.Status == ContractReportPaymentCheckStatus.Active)

                    group new
                    {
                        crpca.PaidBfpTotalAmount,
                    }
                    by new
                    {
                        ContractReportCertAuthorityCorrectionId = crc.ContractReportCertAuthorityCorrectionId,
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
                    select new ContractReportCertAuthorityCorrectionBasicDataVO
                    {
                        ContractReportCertAuthorityCorrectionId = g.Key.ContractReportCertAuthorityCorrectionId,
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

        public IList<ContractReportCertAuthorityCorrectionDocumentVO> GetDocuments(int contractReportCertAuthorityCorrectionId)
        {
            return (from crcd in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrectionDocument>()
                    where crcd.ContractReportCertAuthorityCorrectionId == contractReportCertAuthorityCorrectionId
                    orderby crcd.ContractReportCertAuthorityCorrectionDocumentId descending
                    select new ContractReportCertAuthorityCorrectionDocumentVO
                    {
                        DocumentId = crcd.ContractReportCertAuthorityCorrectionDocumentId,
                        Description = crcd.Description,
                        File = new FileVO
                        {
                            Key = crcd.FileKey,
                            Name = crcd.FileName,
                        },
                    }).ToList();
        }

        public int GetProgrammeId(int contractReportCertAuthorityCorrectionId)
        {
            return (from crc in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>()
                    where crc.ContractReportCertAuthorityCorrectionId == contractReportCertAuthorityCorrectionId
                    select crc.ProgrammeId).Single();
        }

        public new void Remove(ContractReportCertAuthorityCorrection contractReportCertAuthorityCorrection)
        {
            if (contractReportCertAuthorityCorrection.IsActivated || contractReportCertAuthorityCorrection.Status != ContractReportCertAuthorityCorrectionStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete ContractReportCertAuthorityCorrection which is not in draft status or is activated.");
            }

            base.Remove(contractReportCertAuthorityCorrection);
        }

        public bool IsIncludedInCertReport(int contractReportCertAuthorityCorrectionId)
        {
            return false;
        }

        public IList<ContractReportCertAuthorityCorrectionVO> GetContractReportCertAuthorityCorrectionsForProjectDossier(int contractId)
        {
            return (from aarcrc in this.unitOfWork.DbContext.Set<AnnualAccountReportCertCorrection>()
                    join crcac in this.unitOfWork.DbContext.Set<ContractReportCertAuthorityCorrection>() on aarcrc.ContractReportCertAuthorityCorrectionId equals crcac.ContractReportCertAuthorityCorrectionId
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on crcac.ContractReportPaymentId equals crp.ContractReportPaymentId
                    join contR in this.unitOfWork.DbContext.Set<ContractReport>() on crp.ContractReportId equals contR.ContractReportId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crcac.ContractId equals c.ContractId
                    join aar in this.unitOfWork.DbContext.Set<AnnualAccountReport>() on aarcrc.AnnualAccountReportId equals aar.AnnualAccountReportId
                    where crcac.ContractId == contractId && crcac.Status != ContractReportCertAuthorityCorrectionStatus.Draft && aar.Status == AnnualAccountReportStatus.Ended
                    select new ContractReportCertAuthorityCorrectionVO
                    {
                        ContractReportCertAuthorityCorrectionId = crcac.ContractReportCertAuthorityCorrectionId,
                        ContractReportId = crp.ContractReportId,
                        ContractId = crp.ContractId,
                        StatusDescr = crcac.Status,
                        Status = crcac.Status,
                        Type = crcac.Type,
                        Date = crcac.Date,
                        ContractRegNum = c.RegNumber,
                        ReportOrderNum = contR.OrderNum,
                        RegNumber = crcac.RegNumber,
                        CertifiedBfpTotalAmount = (int)crcac.Sign * crcac.CertifiedBfpTotalAmount,
                        CertifiedSelfAmount = (int)crcac.Sign * crcac.CertifiedSelfAmount,
                        AnnualAccountReportOrderNum = aar.OrderNum,
                        Description = crcac.Description,
                    }).ToList();
        }
    }
}
