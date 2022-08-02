using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Projects.Repositories
{
    internal class ProjectMassManagingAuthorityCommunicationsRepository : AggregateRepository<ProjectMassManagingAuthorityCommunication>, IProjectMassManagingAuthorityCommunicationsRepository
    {
        public ProjectMassManagingAuthorityCommunicationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ProjectMassManagingAuthorityCommunication, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ProjectMassManagingAuthorityCommunication, object>>[]
                {
                    p => p.Documents,
                    p => p.Recipients,
                };
            }
        }

        public IList<ProjectMassManagingAuthorityCommunicationVO> GetProjectMassManagingAuthorityCommunications(int[] programmeIds)
        {
            var availableProcedures = this.unitOfWork.DbContext.Set<ProcedureShare>().Where(x => programmeIds.Contains(x.ProgrammeId)).Select(x => x.ProcedureId);

            var communications =
                (from pmc in this.unitOfWork.DbContext.Set<ProjectMassManagingAuthorityCommunication>()
                 join p in this.unitOfWork.DbContext.Set<Procedure>() on pmc.ProcedureId equals p.ProcedureId
                 join prog in this.unitOfWork.DbContext.Set<Programme>() on pmc.ProgrammeId equals prog.MapNodeId
                 where availableProcedures.Contains(p.ProcedureId)
                 orderby pmc.CreateDate descending
                 select new ProjectMassManagingAuthorityCommunicationVO
                 {
                     ProjectMassManagingAuthorityCommunicationId = pmc.ProjectMassManagingAuthorityCommunicationId,
                     ProcedureCode = p.Code,
                     ProgrammeName = prog.Name,
                     OrderNum = pmc.OrderNum,
                     EndingDate = pmc.EndingDate,
                     Status = pmc.Status,
                     Subject = pmc.Subject,
                     ModifyDate = pmc.ModifyDate,
                 }).ToList();

            return communications;
        }

        public ProjectMassManagingAuthorityCommunicationInfoVO GetInfo(int communicationId)
        {
            var info = (from pmc in this.unitOfWork.DbContext.Set<ProjectMassManagingAuthorityCommunication>().Where(x => x.ProjectMassManagingAuthorityCommunicationId == communicationId)
                        join p in this.unitOfWork.DbContext.Set<Procedure>() on pmc.ProcedureId equals p.ProcedureId
                        select new ProjectMassManagingAuthorityCommunicationInfoVO
                        {
                            ProcedureCode = p.Code,
                            Status = pmc.Status,
                            Version = pmc.Version,
                        }).Single();

            return info;
        }

        public void DeleteProjectMassManagingAuthorityCommunication(int communicationId, byte[] vers)
        {
            var communication = this.FindForUpdate(communicationId, vers);

            communication.AssertIsDraft();

            this.Remove(communication);
        }

        public IList<ProjectMassManagingAuthorityCommunicationRecipientVO> GetUnattachedProjects(int communicationId, int procedureId)
        {
            var attachedProjectIds = this.unitOfWork.DbContext.Set<ProjectMassManagingAuthorityCommunicationRecipient>()
                .Where(x => x.ProjectMassManagingAuthorityCommunicationId == communicationId)
                .Select(x => x.ProjectId);

            var predicate = PredicateBuilder.True<Project>()
                .AndEquals(p => p.ProcedureId, procedureId)
                .And(p => !attachedProjectIds.Contains(p.ProjectId))
                .And(p => p.RegistrationStatus != ProjectRegistrationStatus.Withdrawn)
                .And(p => p.EvalStatus != ProjectEvalStatus.Contracted);

            var unattachedProjects =
                (from p in this.unitOfWork.DbContext.Set<Project>().Where(predicate)
                 select new ProjectMassManagingAuthorityCommunicationRecipientVO
                 {
                     ProjectId = p.ProjectId,
                     BeneficiaryName = p.CompanyName,
                     RecieveDate = p.RecieveDate,
                     ProjectName = p.Name,
                     ProjectRegNumber = p.RegNumber,
                 }).ToList();

            return unattachedProjects;
        }

        public IList<ProjectMassManagingAuthorityCommunicationRecipientVO> GetAttachedProjects(int communicationId)
        {
            var attachedProjects =
                (from cr in this.unitOfWork.DbContext.Set<ProjectMassManagingAuthorityCommunicationRecipient>().Where(x => x.ProjectMassManagingAuthorityCommunicationId == communicationId)
                 join p in this.unitOfWork.DbContext.Set<Project>() on cr.ProjectId equals p.ProjectId
                 select new ProjectMassManagingAuthorityCommunicationRecipientVO
                 {
                     ProjectId = cr.ProjectId,
                     BeneficiaryName = p.CompanyName,
                     RecieveDate = p.RecieveDate,
                     ProjectName = p.Name,
                     ProjectRegNumber = p.RegNumber,
                 }).ToList();

            return attachedProjects;
        }

        public IList<ProjectMassManagingAuthorityCommunicationDocumentVO> GetCommunicationDocuments(int communicationId)
        {
            var documents =
                (from pmcd in this.unitOfWork.DbContext.Set<ProjectMassManagingAuthorityCommunicationDocument>().Where(x => x.ProjectMassManagingAuthorityCommunicationId == communicationId)
                 select new ProjectMassManagingAuthorityCommunicationDocumentVO
                 {
                     Description = pmcd.Description,
                     Name = pmcd.Name,
                     DocumentId = pmcd.ProjectMassManagingAuthorityCommunicationDocumentId,
                     ProjectMassManagingAuthorityCommunicationId = pmcd.ProjectMassManagingAuthorityCommunicationId,
                     File = pmcd.FileKey.HasValue ? new Domain.Core.FileVO
                     {
                         Key = pmcd.FileKey.Value,
                         Name = pmcd.FileName,
                     }
                     : null,
                 }).ToList();

            return documents;
        }

        public int GetPrimaryProcedureShareProgrammeId(int communicationId)
        {
            return (from pmc in this.unitOfWork.DbContext.Set<ProjectMassManagingAuthorityCommunication>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pmc.ProcedureId equals ps.ProcedureId
                    where pmc.ProjectMassManagingAuthorityCommunicationId == communicationId && ps.IsPrimary
                    select ps.ProgrammeId)
                    .Single();
        }

        public int GetNextOrderNum(int programmeId)
        {
            var lastOrderNumber = this.unitOfWork.DbContext.Set<ProjectMassManagingAuthorityCommunication>()
                .Where(t => t.ProgrammeId == programmeId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }
    }
}
