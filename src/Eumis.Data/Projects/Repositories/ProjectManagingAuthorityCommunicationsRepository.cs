using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Data.Registrations.PortalViewObjects;
using Eumis.Domain;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.Registrations;

namespace Eumis.Data.Projects.Repositories
{
    internal class ProjectManagingAuthorityCommunicationsRepository : AggregateRepository<ProjectManagingAuthorityCommunication, ProjectCommonCommunication>, IProjectManagingAuthorityCommunicationsRepository
    {
        public ProjectManagingAuthorityCommunicationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ProjectManagingAuthorityCommunication, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ProjectManagingAuthorityCommunication, object>>[]
                {
                    c => c.Files,
                    c => c.Answers,
                };
            }
        }

        public ProjectCommunicationPVO GetProjectCommunications(Guid registeredGid, int offset, int? limit)
        {
            var registration = this.unitOfWork.DbContext.Set<RegProjectXml>().Where(r => r.Gid == registeredGid).SingleOrDefault();

            if (registration == null)
            {
                throw new DomainObjectNotFoundException("Cannot find RegProjectXml with gid " + registeredGid);
            }

            var projectId = registration.ProjectId;

            if (projectId == null)
            {
                throw new InvalidOperationException("Cannot get communications for not registered Project");
            }

            var project = this.unitOfWork.DbContext.Set<Project>().Where(p => p.ProjectId == projectId).SingleOrDefault();

            if (project == null)
            {
                throw new DomainObjectNotFoundException("Cannot find Project with id " + projectId);
            }

            var communications = (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>()
                                  where pc.ProjectId == projectId &&
                                  !(pc.Source == ProjectManagingAuthorityCommunicationSource.ManagingAuthority &&
                                  pc.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.DraftQuestion)
                                  orderby pc.CreateDate descending
                                  select new
                                  {
                                      CommunicationGid = pc.Gid,
                                      MessageDate = pc.Question.MessageDate,
                                      ReadDate = pc.QuestionReadDate,
                                      QuestionEndingDate = pc.QuestionEndingDate,
                                      RegistrationNumber = pc.RegNumber,
                                      Status = pc.ManagingAuthorityCommunicationStatus,
                                      Source = pc.Source,
                                      Answers = pc.Answers,
                                      Version = pc.Version,
                                  })
                                  .ToList()
                                  .Select(pc => new ProjectCommunicationQuestionPVO(
                                      pc.CommunicationGid,
                                      pc.RegistrationNumber,
                                      pc.MessageDate,
                                      pc.Answers.Max(a => a.Answer.MessageDate),
                                      pc.ReadDate,
                                      pc.QuestionEndingDate,
                                      pc.Version,
                                      pc.Status,
                                      pc.Source,
                                      pc.Answers));

            var communiationsWithOffsetAndLimit = communications
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return new ProjectCommunicationPVO
            {
                Communications = new PagePVO<ProjectCommunicationQuestionPVO>()
                {
                    Results = communiationsWithOffsetAndLimit,
                    Count = communications.Count(),
                },
                ProjectRegNumber = project.RegNumber,
            };
        }

        public IList<ProjectManagingAuthorityCommunicationVO> GetAllCommunications(
            int[] programmeIds,
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            ProjectManagingAuthorityCommunicationSource? source = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var predicate = PredicateBuilder.True<ProjectManagingAuthorityCommunication>()
                .And(c => !(c.Source == ProjectManagingAuthorityCommunicationSource.Beneficiary &&
                    c.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.DraftQuestion))
                .AndDateTimeGreaterThanOrEqual(c => c.Question.MessageDate, fromDate)
                .AndDateTimeLessThanOrEqual(c => c.Question.MessageDate, toDate)
                .AndEquals(c => c.Source, source);

            var projectPredicate = PredicateBuilder.True<Project>()
                .AndEquals(p => p.ProcedureId, procedureId);

            var subqueryPredicate = PredicateBuilder.True<ProcedureShare>();

            if (programmeId.HasValue)
            {
                subqueryPredicate = subqueryPredicate.And(ps => ps.ProgrammeId == programmeId);
            }

            if (programmePriorityId.HasValue)
            {
                subqueryPredicate = subqueryPredicate.And(ps => ps.ProgrammePriorityId == programmePriorityId);
            }

            var subquery = from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(subqueryPredicate)
                           where programmeIds.Contains(ps.ProgrammeId)
                           select ps.ProcedureId;

            return (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>().Where(predicate)
                    join p in this.unitOfWork.DbContext.Set<Project>().Where(projectPredicate) on pc.ProjectId equals p.ProjectId
                    join ca in this.unitOfWork.DbContext.Set<ProjectCommunicationAnswer>() on pc.ProjectCommunicationId equals ca.ProjectCommunicationId into g1
                    from ca in g1.DefaultIfEmpty()
                    where subquery.Contains(p.ProcedureId)
                    group new
                    {
                        AnswerDate = ca.Answer.MessageDate,
                    }
                    by new
                    {
                        ProjectCommunicationId = pc.ProjectCommunicationId,
                        ProjectId = pc.ProjectId,
                        OrderNum = pc.OrderNum,
                        QuestionSendDate = pc.Question.MessageDate,
                        QuestionReadDate = pc.QuestionReadDate,
                        ProjectRegNumber = p.RegNumber,
                        Status = pc.ManagingAuthorityCommunicationStatus,
                        Source = pc.Source,
                        RegNumber = pc.RegNumber,
                        Subject = pc.Subject,
                    }
                    into g
                    select new ProjectManagingAuthorityCommunicationVO()
                    {
                        ProjectCommunicationId = g.Key.ProjectCommunicationId,
                        ProjectId = g.Key.ProjectId,
                        OrderNum = g.Key.OrderNum,
                        QuestionSendDate = g.Key.QuestionSendDate,
                        QuestionReadDate = g.Key.QuestionReadDate,
                        AnswerDate = g.Max(x => x.AnswerDate),
                        ProjectRegNumber = g.Key.ProjectRegNumber,
                        Status = g.Key.Status,
                        StatusDescr = g.Key.Status,
                        Source = g.Key.Source,
                        RegNumber = g.Key.RegNumber,
                        Subject = g.Key.Subject,
                    })
                    .OrderByDescending(c => c.QuestionSendDate)
                    .ToList();
        }

        public IList<ProjectManagingAuthorityCommunicationVO> GetProjectManagingAuthorityCommunications(int projectId)
        {
            var predicate = PredicateBuilder.True<ProjectManagingAuthorityCommunication>()
                .And(c => !(c.Source == ProjectManagingAuthorityCommunicationSource.Beneficiary &&
                c.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.DraftQuestion))
                .AndEquals(c => c.ProjectId, projectId);

            return (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>().Where(predicate)
                    join p in this.unitOfWork.DbContext.Set<Project>() on pc.ProjectId equals p.ProjectId
                    join ca in this.unitOfWork.DbContext.Set<ProjectCommunicationAnswer>() on pc.ProjectCommunicationId equals ca.ProjectCommunicationId into g1
                    from ca in g1.DefaultIfEmpty()
                    group new
                    {
                        AnswerDate = ca.Answer.MessageDate,
                    }
                    by new
                    {
                        ProjectCommunicationId = pc.ProjectCommunicationId,
                        ProjectId = pc.ProjectId,
                        OrderNum = pc.OrderNum,
                        QuestionSendDate = pc.Question.MessageDate,
                        QuestionReadDate = pc.QuestionReadDate,
                        ProjectRegNumber = p.RegNumber,
                        Status = pc.ManagingAuthorityCommunicationStatus,
                        StatusDescr = pc.ManagingAuthorityCommunicationStatus,
                        Source = pc.Source,
                        RegNumber = pc.RegNumber,
                        Subject = pc.Subject,
                        CreateDate = pc.CreateDate,
                    }
                    into g
                    orderby g.Key.CreateDate descending
                    select new ProjectManagingAuthorityCommunicationVO()
                    {
                        ProjectCommunicationId = g.Key.ProjectCommunicationId,
                        ProjectId = g.Key.ProjectId,
                        OrderNum = g.Key.OrderNum,
                        QuestionSendDate = g.Key.QuestionSendDate,
                        QuestionReadDate = g.Key.QuestionReadDate,
                        AnswerDate = g.Max(x => x.AnswerDate),
                        ProjectRegNumber = g.Key.ProjectRegNumber,
                        Status = g.Key.Status,
                        StatusDescr = g.Key.Status,
                        Source = g.Key.Source,
                        RegNumber = g.Key.RegNumber,
                        Subject = g.Key.Subject,
                    })
                    .ToList();
        }

        public ProjectManagingAuthorityCommunication Find(Guid communicationGid)
        {
            return this.Set()
                .Where(p => p.Gid == communicationGid)
                .Single();
        }

        public ProjectManagingAuthorityCommunication FindForUpdate(int registrationId, Guid communicationGid, byte[] version)
        {
            var projectCommunication = (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>()
                                        join rp in this.unitOfWork.DbContext.Set<RegProjectXml>() on pc.ProjectId equals rp.ProjectId
                                        where pc.Gid == communicationGid && rp.RegistrationId == registrationId
                                        select pc)
                                        .SingleOrDefault();

            this.CheckVersion(projectCommunication.Version, version);

            this.LoadCommunication(projectCommunication);

            return projectCommunication;
        }

        public ProjectManagingAuthorityCommunication FindForUpdate(Guid communicationGid, byte[] version)
        {
            var projectCommunication = (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>()
                                        join rp in this.unitOfWork.DbContext.Set<RegProjectXml>() on pc.ProjectId equals rp.ProjectId
                                        where pc.Gid == communicationGid
                                        select pc)
                                        .SingleOrDefault();

            this.CheckVersion(projectCommunication.Version, version);

            this.LoadCommunication(projectCommunication);

            return projectCommunication;
        }

        public int GetProjectPrimaryProgrammeId(int projectCommunicationId)
        {
            return (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>()
                    join p in this.unitOfWork.DbContext.Set<Project>() on pc.ProjectId equals p.ProjectId
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where pc.ProjectCommunicationId == projectCommunicationId && ps.IsPrimary
                    select ps.ProgrammeId)
                    .Single();
        }

        public int GetNextOrderNumber(int projectId)
        {
            var lastOrderNumber = this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>()
                .Where(p => p.ProjectId == projectId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public IList<ProjectCommunicationAnswerVO> GetProjectManagingAuthorityCommunicationAnswers(int communicationId)
        {
            return (from ca in this.unitOfWork.DbContext.Set<ProjectCommunicationAnswer>().Where(ca => ca.ProjectCommunicationId == communicationId)
                    where !(ca.Status == ProjectCommunicationAnswerStatus.Draft && ca.Source == ProjectCommunicationAnswerSource.Beneficiary)
                    orderby ca.OrderNum descending
                    select new ProjectCommunicationAnswerVO()
                    {
                        ProjectCommunicationAnswerId = ca.ProjectCommunicationAnswerId,
                        XmlGid = ca.Gid,
                        Status = ca.Status,
                        StatusDescr = ca.Status,
                        AnswerDate = ca.Answer.MessageDate,
                        ProjectCommunicationId = ca.ProjectCommunicationId,
                        Source = ca.Source,
                    })
                    .ToList();
        }

        private void LoadCommunication(ProjectManagingAuthorityCommunication communication)
        {
            var entry = this.unitOfWork.DbContext.Entry(communication);

            entry.Collection(n => n.Files).Load();
            entry.Collection(n => n.Answers).Load();
        }

        public ProjectCommunicationSentPVO GetSentCommunicationInfo(Guid communicationGid)
        {
            return (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>()
                    join proj in this.unitOfWork.DbContext.Set<Project>() on pc.ProjectId equals proj.ProjectId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on proj.ProcedureId equals proc.ProcedureId
                    join reg in this.unitOfWork.DbContext.Set<RegProjectXml>() on proj.ProjectId equals reg.ProjectId into g1
                    from reg in g1.DefaultIfEmpty()
                    where pc.Gid == communicationGid
                    select new ProjectCommunicationSentPVO()
                    {
                        RegisteredGid = reg.Gid,
                        CommunicationRegNumber = pc.RegNumber,
                        ProjectRegNumber = proj.RegNumber,
                        ProcedureCode = proc.Code,
                        ProcedureName = proc.Name,
                        ProcedureNameAlt = proc.NameAlt,
                        ProjectName = proj.Name,
                        ProjectNameAlt = proj.NameAlt,
                        Subject = new EnumPVO<ProjectManagingAuthorityCommunicationSubject>
                        {
                            Description = pc.Subject,
                            Value = pc.Subject,
                        },
                    })
                    .Single();
        }

        public ProjectCommunicationSentPVO GetSentAnswerInfo(Guid answerGid)
        {
            return (from pca in this.unitOfWork.DbContext.Set<ProjectCommunicationAnswer>()
                    join pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>() on pca.ProjectCommunicationId equals pc.ProjectCommunicationId
                    join proj in this.unitOfWork.DbContext.Set<Project>() on pc.ProjectId equals proj.ProjectId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on proj.ProcedureId equals proc.ProcedureId
                    join reg in this.unitOfWork.DbContext.Set<RegProjectXml>() on proj.ProjectId equals reg.ProjectId into g1
                    from reg in g1.DefaultIfEmpty()
                    where pca.Gid == answerGid
                    select new ProjectCommunicationSentPVO()
                    {
                        RegisteredGid = reg.Gid,
                        CommunicationRegNumber = pc.RegNumber,
                        ProjectRegNumber = proj.RegNumber,
                        ProcedureCode = proc.Code,
                        ProcedureName = proc.Name,
                        ProcedureNameAlt = proc.NameAlt,
                        ProjectName = proj.Name,
                        ProjectNameAlt = proj.NameAlt,
                    })
                    .Single();
        }

        public bool ProjectHasExistingCommunications(Guid registeredGid)
        {
            return (from pr in this.unitOfWork.DbContext.Set<RegProjectXml>().Where(r => r.Gid == registeredGid)
                    join pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>() on pr.ProjectId equals pc.ProjectId
                    select pc)
                    .Where(c => !(c.Source == ProjectManagingAuthorityCommunicationSource.ManagingAuthority &&
                    c.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.DraftQuestion))
                    .Any();
        }

        public bool RegistrationHasNewCommunications(int registrationId)
        {
            var hasNewQuestions = (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>()
                                   join rp in this.unitOfWork.DbContext.Set<RegProjectXml>() on pc.ProjectId equals rp.ProjectId
                                   where rp.RegistrationId == registrationId &&
                                         pc.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.Question &&
                                         pc.QuestionReadDate == null &&
                                         pc.Source == ProjectManagingAuthorityCommunicationSource.ManagingAuthority
                                   select pc)
                                   .Any();

            var hasNewAnswers = (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>()
                                 join rp in this.unitOfWork.DbContext.Set<RegProjectXml>() on pc.ProjectId equals rp.ProjectId
                                 join a in this.unitOfWork.DbContext.Set<ProjectCommunicationAnswer>() on pc.ProjectCommunicationId equals a.ProjectCommunicationId
                                 where rp.RegistrationId == registrationId &&
                                       pc.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.Question &&
                                       a.Status == ProjectCommunicationAnswerStatus.Answer &&
                                       a.Source == ProjectCommunicationAnswerSource.ManagingAuthority &&
                                       !a.ReadDate.HasValue
                                 select a)
                                 .Any();

            return hasNewQuestions || hasNewAnswers;
        }
    }
}
