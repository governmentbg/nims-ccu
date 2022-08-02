using Eumis.Common.Db;
using Eumis.Data.CompensationDocuments.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Domain.OperationalMap.Programmes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.CompensationDocuments.Repositories
{
    internal class CompensationDocumentsRepository : AggregateRepository<CompensationDocument>, ICompensationDocumentsRepository
    {
        public CompensationDocumentsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<CompensationDocument, object>>[] Includes
        {
            get
            {
                return new Expression<Func<CompensationDocument, object>>[]
                {
                    cd => cd.Documents,
                };
            }
        }

        public IList<CompensationDocumentVO> GetCompensationDocuments(
            int[] programmeIds,
            CompensationDocumentType? type = null,
            CompensationDocumentStatus? status = null)
        {
            var predicate = PredicateBuilder.True<CompensationDocument>()
                .AndEquals(cd => cd.Type, type)
                .AndEquals(cd => cd.Status, status);

            return (from cd in this.unitOfWork.DbContext.Set<CompensationDocument>().Where(predicate)
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on cd.ProgrammeId equals pr.MapNodeId
                    where programmeIds.Contains(cd.ProgrammeId)
                    orderby cd.CreateDate descending
                    select new CompensationDocumentVO
                    {
                        CompensationDocumentId = cd.CompensationDocumentId,
                        ProgrammeName = pr.Name,
                        RegNumber = cd.RegNumber,
                        StatusDescr = cd.Status,
                        Status = cd.Status,
                        Type = cd.Type,
                        CompensationDocDate = cd.CompensationDocDate,
                    }).ToList();
        }

        public CompensationDocumentInfoVO GetInfo(int compensationDocumentId)
        {
            return (from cd in this.unitOfWork.DbContext.Set<CompensationDocument>()
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on cd.ProgrammeId equals pr.MapNodeId
                    where cd.CompensationDocumentId == compensationDocumentId
                    select new CompensationDocumentInfoVO
                    {
                        ProgrammeCode = pr.Code,
                        Status = cd.Status,
                        StatusDescr = cd.Status,
                    }).Single();
        }

        public CompensationDocumentBasicDataVO GetBasicData(int compensationDocumentId)
        {
            return (from cd in this.unitOfWork.DbContext.Set<CompensationDocument>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cd.ContractId equals c.ContractId
                    join crp in this.unitOfWork.DbContext.Set<ContractReportPayment>() on cd.ContractReportPaymentId equals crp.ContractReportPaymentId into g1
                    from crp in g1.DefaultIfEmpty()
                    join crpc in this.unitOfWork.DbContext.Set<ContractReportPaymentCheck>() on crp.ContractReportPaymentId equals crpc.ContractReportPaymentId into g2
                    from crpc in g2.DefaultIfEmpty()
                    join crpca in this.unitOfWork.DbContext.Set<ContractReportPaymentCheckAmount>() on crpc.ContractReportPaymentCheckId equals crpca.ContractReportPaymentCheckId into g3
                    from crpca in g3.DefaultIfEmpty()
                    where cd.CompensationDocumentId == compensationDocumentId &&
                          (crp == null || crp.Status == ContractReportPaymentStatus.Actual) &&
                          (crpc == null || crpc.Status == ContractReportPaymentCheckStatus.Active)

                    group new
                    {
                        crpca.PaidBfpTotalAmount,
                    }
                    by new
                    {
                        CompensationDocumentId = cd.CompensationDocumentId,
                        RegNumber = cd.RegNumber,
                        Status = cd.Status,
                        Type = cd.Type,
                        IsActivated = cd.IsActivated,
                        DeleteNote = cd.DeleteNote,
                        ProgrammeId = cd.ProgrammeId,
                        ProcedureId = cd.ProcedureId,
                        ProgrammePriorityId = cd.ProgrammePriorityId,
                        ContractId = cd.ContractId,
                        ContractName = c.Name,
                        ContractRegNumber = c.RegNumber,
                        CompanyName = c.CompanyName,
                        CompanyUin = c.CompanyUin,
                        CompanyUinType = c.CompanyUinType,
                        ContractReportPaymentId = cd.ContractReportPaymentId,
                        PaymentVersionNum = crp.VersionNum,
                        RequestedAmount = crp.RequestedAmount,
                        CheckedDate = crpc.CheckedDate,
                        Version = cd.Version,
                    }
                    into g
                    select new CompensationDocumentBasicDataVO
                    {
                        CompensationDocumentId = g.Key.CompensationDocumentId,
                        RegNumber = g.Key.RegNumber,
                        Status = g.Key.Status,
                        Type = g.Key.Type,
                        IsActivated = g.Key.IsActivated,
                        DeleteNote = g.Key.DeleteNote,
                        ProgrammeId = g.Key.ProgrammeId,
                        ProcedureId = g.Key.ProcedureId,
                        ContractId = g.Key.ContractId,
                        ContractName = g.Key.ContractName,
                        ContractRegNumber = g.Key.RegNumber,
                        CompanyName = g.Key.CompanyName,
                        CompanyUin = g.Key.CompanyUin,
                        CompanyUinType = g.Key.CompanyUinType,
                        ContractReportPaymentId = g.Key.ContractReportPaymentId,
                        PaymentVersionNum = g.Key.PaymentVersionNum,
                        RequestedAmount = g.Key.RequestedAmount,
                        PaidBfpTotalAmount = g.Sum(t => t.PaidBfpTotalAmount),
                        CheckedDate = g.Key.CheckedDate,
                        Version = g.Key.Version,
                    }).Single();
        }

        public IList<CompensationDocumentDocVO> GetDocuments(int compensationDocumentId)
        {
            return (from cdd in this.unitOfWork.DbContext.Set<CompensationDocumentDoc>()
                    where cdd.CompensationDocumentId == compensationDocumentId
                    orderby cdd.CompensationDocumentDocId descending
                    select new CompensationDocumentDocVO
                    {
                        DocumentId = cdd.CompensationDocumentDocId,
                        Description = cdd.Description,
                        File = new FileVO
                        {
                            Key = cdd.FileKey,
                            Name = cdd.FileName,
                        },
                    }).ToList();
        }

        public int GetProgrammeId(int compensationDocumentId)
        {
            return (from cd in this.unitOfWork.DbContext.Set<CompensationDocument>()
                    where cd.CompensationDocumentId == compensationDocumentId
                    select cd.ProgrammeId).Single();
        }

        public new void Remove(CompensationDocument compensationDocument)
        {
            if (compensationDocument.IsActivated || compensationDocument.Status != CompensationDocumentStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete CompensationDocument which is not in draft status or is activated.");
            }

            base.Remove(compensationDocument);
        }
    }
}
