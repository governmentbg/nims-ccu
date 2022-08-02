using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Procedures.Repositories
{
    internal class ProcedureMassCommunicationsRespository : AggregateRepository<ProcedureMassCommunication>, IProcedureMassCommunicationsRepository
    {
        public ProcedureMassCommunicationsRespository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ProcedureMassCommunication, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ProcedureMassCommunication, object>>[]
                {
                    p => p.Documents,
                    p => p.Recipients,
                };
            }
        }

        public void DeleteProcedureMassCommunication(int procedureMassCommunicationId, byte[] vers)
        {
            var communication = this.FindForUpdate(procedureMassCommunicationId, vers);

            communication.AssertIsDraft();

            this.Remove(communication);
        }

        public ProcedureMassCommunicationInfoVO GetInfo(int procedureMassCommunicationId)
        {
            return (from pmc in this.Set().Where(x => x.ProcedureMassCommunicationId == procedureMassCommunicationId)
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on pmc.ProcedureId equals p.ProcedureId
                    select new ProcedureMassCommunicationInfoVO
                    {
                        ProcedureCode = p.Code,
                        Status = pmc.Status,
                        Version = pmc.Version,
                    }).Single();
        }

        public IList<ProcedureMassCommunicationVO> GetProcedureMassCommunications(int[] programmeIds)
        {
            var availableProcedures = this.unitOfWork.DbContext.Set<ProcedureShare>().Where(x => programmeIds.Contains(x.ProgrammeId)).Select(x => x.ProcedureId);

            return (from pmc in this.Set()
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on pmc.ProcedureId equals p.ProcedureId
                    where availableProcedures.Contains(p.ProcedureId)
                    select new ProcedureMassCommunicationVO
                    {
                        ProcedureMassCommunicationId = pmc.ProcedureMassCommunicationId,
                        ProcedureCode = p.Code,
                        Status = pmc.Status,
                        Subject = pmc.Subject,
                        ModifyDate = pmc.ModifyDate,
                    }).ToList();
        }

        public int GetCommunicationProcedureId(int procedureMassCommunicationId)
        {
            return this.Set()
                .Where(x => x.ProcedureMassCommunicationId == procedureMassCommunicationId)
                .Select(x => x.ProcedureId)
                .Single();
        }

        public IList<ProcedureMassCommunicationDocumentVO> GetCommunicationDocuments(int communicationId)
        {
            return (from pmcd in this.unitOfWork.DbContext.Set<ProcedureMassCommunicationDocument>().Where(x => x.ProcedureMassCommunicationId == communicationId)
                    select new ProcedureMassCommunicationDocumentVO
                    {
                        Description = pmcd.Description,
                        Name = pmcd.Name,
                        DocumentId = pmcd.ProcedureMassCommunicationDocumentId,
                        ProcedureMassCommunicationId = pmcd.ProcedureMassCommunicationId,
                        File = pmcd.FileKey.HasValue ? new Domain.Core.FileVO
                        {
                            Key = pmcd.FileKey.Value,
                            Name = pmcd.FileName,
                        }
                        : null,
                    }).ToList();
        }

        public IList<ProcedureMassCommunicationRecipientVO> GetAttachedContracts(int communicationId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<ProcedureMassCommunicationRecipient>().Where(x => x.ProcedureMassCommunicationId == communicationId)
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cr.ContractId equals c.ContractId
                    select new ProcedureMassCommunicationRecipientVO
                    {
                        ContractId = cr.ContractId,
                        BeneficiaryName = c.CompanyName,
                        ContractDate = c.ContractDate,
                        ContractName = c.Name,
                        ContractRegNumber = c.RegNumber,
                    }).ToList();
        }

        public IList<ProcedureMassCommunicationRecipientVO> GetUnattachedContracts(int communicationId, int procedureId)
        {
            var attachedContractIds = this.unitOfWork.DbContext.Set<ProcedureMassCommunicationRecipient>()
                .Where(x => x.ProcedureMassCommunicationId == communicationId)
                .Select(x => x.ContractId);

            return (from c in this.unitOfWork.DbContext.Set<Contract>().Where(x => x.ProcedureId == procedureId && !attachedContractIds.Contains(x.ContractId) && x.ContractStatus == ContractStatus.Entered)
                    select new ProcedureMassCommunicationRecipientVO
                    {
                        ContractId = c.ContractId,
                        BeneficiaryName = c.CompanyName,
                        ContractDate = c.ContractDate,
                        ContractName = c.Name,
                        ContractRegNumber = c.RegNumber,
                    }).ToList();
        }
    }
}
