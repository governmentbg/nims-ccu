using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.Registrations;
using Eumis.Web.Api.Core;

namespace Eumis.Data.Projects.Repositories
{
    internal class ProjectCommunicationsRepository : AggregateRepository<ProjectCommunication, ProjectCommonCommunication>, IProjectCommunicationsRepository
    {
        public ProjectCommunicationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ProjectCommunication, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ProjectCommunication, object>>[]
                {
                    c => c.Files,
                    c => c.Answers,
                };
            }
        }

        public IList<CommunicationVO> GetProjectCommunications(int projectId, bool sentOnly = false)
        {
            var predicate = PredicateBuilder.True<ProjectCommunication>();

            if (sentOnly)
            {
                predicate = predicate.And(c => c.Status != ProjectCommunicationStatus.DraftQuestion);
            }

            var answerPredicate = PredicateBuilder.True<ProjectCommunicationAnswer>()
                .And(a => a.Status == ProjectCommunicationAnswerStatus.Answer);

            return (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>().Where(predicate)
                    join es in this.unitOfWork.DbContext.Set<EvalSession>() on pc.EvalSessionId equals es.EvalSessionId

                    join pca in this.unitOfWork.DbContext.Set<ProjectCommunicationAnswer>().Where(answerPredicate) on pc.ProjectCommunicationId equals pca.ProjectCommunicationId into g1
                    from pca in g1.DefaultIfEmpty()

                    where pc.ProjectId == projectId
                    orderby pc.OrderNum descending
                    select new
                    {
                        Message = pc,
                        SessionNum = es.SessionNum,
                        AnswerDate = pca != null ? pca.Answer.MessageDate : (DateTime?)null,
                    })
                    .ToList()
                    .GroupBy(g => g.Message.ProjectCommunicationId)
                    .Select(r => new CommunicationVO(r.First().Message, r.First().SessionNum, r.First().AnswerDate))
                    .ToList();
        }

        public IList<EvalSessionCommunicationVO> GetProjectCommunicationsForEvalSession(
            int evalSessionId,
            int? projectId = null,
            ProjectCommunicationStatus? statusId = null,
            DateTime? questionDateFrom = null,
            DateTime? questionDateTo = null)
        {
            var predicate = PredicateBuilder.True<ProjectCommunication>()
                .AndEquals(c => c.ProjectId, projectId)
                .AndEquals(c => c.Status, statusId);

            if (questionDateFrom.HasValue)
            {
                predicate = predicate.And(c => c.Question.MessageDate >= questionDateFrom);
            }

            if (questionDateTo.HasValue)
            {
                var endDate = questionDateTo.Value.AddDays(1);
                predicate = predicate.And(c => c.Question.MessageDate < endDate);
            }

            var results = (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>().Where(predicate)
                           join p in this.unitOfWork.DbContext.Set<Project>() on pc.ProjectId equals p.ProjectId
                           where pc.EvalSessionId == evalSessionId
                           orderby pc.CreateDate descending
                           select new
                           {
                               pc.ProjectCommunicationId,
                               pc.ProjectId,
                               pc.EvalSessionId,
                               pc.CreateDate,
                               pc.Status,
                               pc.RegNumber,
                               QuestionDate = pc.Question.MessageDate,
                               pc.QuestionEndingDate,
                               AnswerDate = pc.Answers
                               .Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer)
                               .Select(a => a.Answer.MessageDate)
                               .FirstOrDefault(),
                               pc.QuestionReadDate,
                               p,
                           })
                    .ToList();

            return results
                .GroupBy(g => g.ProjectCommunicationId)
                .Select(g => new EvalSessionCommunicationVO
                {
                    ProjectCommunicationId = g.Key,
                    ProjectId = g.First().ProjectId,
                    EvalSessionId = g.First().EvalSessionId,
                    CreateDate = g.First().CreateDate,
                    Status = ProjectCommunication.EvalSessionInvisibleStatuses.Contains(g.First().Status) ?
                        ProjectCommunicationStatus.Question :
                        g.First().Status,
                    RegNumber = g.First().RegNumber,
                    QuestionDate = g.First().QuestionDate,
                    QuestionEndingDate = g.First().QuestionEndingDate,
                    AnswerDate = g.First().AnswerDate,
                    QuestionReadDate = g.First().QuestionReadDate,
                    CompanyNameBg = g.First().p.CompanyName,
                    CompanyNameEn = g.First().p.CompanyNameAlt,
                    ProjectNameBg = g.First().p.Name,
                    ProjectNameEn = g.First().p.NameAlt,
                    ProjectRegNumber = g.First().p.RegNumber,
                })
                .ToList();
        }

        public CommunicationRegVO GetCommunicationRegData(int projectCommunicationId)
        {
            var answerPredicate = PredicateBuilder.True<ProjectCommunicationAnswer>()
                .And(c => c.Status == ProjectCommunicationAnswerStatus.Answer);

            return (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>()

                    join pca in this.unitOfWork.DbContext.Set<ProjectCommunicationAnswer>().Where(answerPredicate) on pc.ProjectCommunicationId equals pca.ProjectCommunicationId into g1
                    from pca in g1.DefaultIfEmpty()

                    join proj in this.unitOfWork.DbContext.Set<Project>() on pc.ProjectId equals proj.ProjectId
                    join pr in this.unitOfWork.DbContext.Set<Procedure>() on proj.ProcedureId equals pr.ProcedureId
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pr.ProcedureId equals ps.ProcedureId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals p.MapNodeId
                    where pc.ProjectCommunicationId == projectCommunicationId && ps.IsPrimary
                    select new CommunicationRegVO
                    {
                        Status = pc.Status,
                        ProgrammeName = p.Name,
                        ProcedureName = pr.Name,
                        ProjectName = proj.Name,
                        RegNumber = pc.RegNumber,
                        RegDate = pca != null ? pca.Answer.MessageDate : (DateTime?)null,
                        ProjectRegNumber = proj.RegNumber,
                        AnswerXmlHash = pca != null ? pca.Answer.Hash : string.Empty,
                        CompanyName = proj.CompanyName,
                        Uin = proj.CompanyUin,
                        UinType = proj.CompanyUinType,
                    }).Single();
        }

        public ProjectCommunication Find(Guid gid)
        {
            return this.Set()
                .Where(pc => pc.Gid == gid)
                .Single();
        }

        public ProjectCommunication Find(int registrationId, Guid gid)
        {
            var projectCommunication = this.GetProjectCommunicationInternal(registrationId, gid);

            this.LoadCommunication(projectCommunication);

            return projectCommunication;
        }

        public ProjectCommunication FindForUpdate(Guid gid, byte[] version)
        {
            var projectCommunication = this.Find(gid);

            this.CheckVersion(projectCommunication.Version, version);

            return projectCommunication;
        }

        public ProjectCommunication FindForUpdate(int registrationId, Guid gid, byte[] version)
        {
            var projectCommunication = this.GetProjectCommunicationInternal(registrationId, gid);

            this.CheckVersion(projectCommunication.Version, version);

            this.LoadCommunication(projectCommunication);

            return projectCommunication;
        }

        public bool HasProjectCommunicationInProgress(int evalSessionId, int? projectId = null)
        {
            var predicate = PredicateBuilder.True<ProjectCommunication>()
                .AndEquals(c => c.EvalSessionId, evalSessionId);

            if (projectId.HasValue)
            {
                predicate = predicate.AndEquals(c => c.ProjectId, projectId);
            }

            return (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>().Where(predicate)
                    where !ProjectCommunication.FinalStatuses.Contains(pc.Status) && pc.Status != ProjectCommunicationStatus.Answer
                    select pc.ProjectCommunicationId)
                    .Any();
        }

        public PagePVO<RegMessagePVO> GetAllForRegistration(
            int registrationId,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<ProjectCommunication>();
            predicate = predicate.And(pc => ProjectCommunication.PortalInProgressStatuses.Contains(pc.Status) || ProjectCommunication.PortalHistoryStatuses.Contains(pc.Status));

            // joining with Projetcs and Companies table because when activating ProjectVersions
            // the record in RegProjectXml table is not updated
            var query = from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>().Where(predicate)
                        join rp in this.unitOfWork.DbContext.Set<RegProjectXml>() on pc.ProjectId equals rp.ProjectId
                        join proj in this.unitOfWork.DbContext.Set<Project>() on rp.ProjectId equals proj.ProjectId
                        join p in this.unitOfWork.DbContext.Set<Procedure>() on rp.ProcedureId equals p.ProcedureId
                        where rp.RegistrationId == registrationId
                        select new
                        {
                            Gid = pc.Gid,
                            SentDate = pc.Question.MessageDate.Value,
                            QuestionEndingDate = pc.QuestionEndingDate,
                            QuestionReadDate = pc.QuestionReadDate,
                            Status = pc.Status,
                            RegistrationNumber = pc.RegNumber,
                            ProcedureCode = p.Code,
                            ProcedureName = p.Name,
                            ProcedureNameAlt = p.NameAlt,
                            ProjectName = proj.Name,
                            ProjectNameAlt = proj.NameAlt,
                            ProjectRegistrationStatus = proj.RegistrationStatus,
                            CompanyName = proj.CompanyName,
                            CompanyNameAlt = proj.CompanyNameAlt,
                            Question = pc.Question.Content,
                            OrderNum = pc.OrderNum,
                            Version = pc.Version,
                            Answers = pc.Answers,
                        };

            return new PagePVO<RegMessagePVO>
            {
                Results = query
                    .OrderByDescending(p => p.SentDate)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList()
                    .Select(r => new RegMessagePVO(
                        r.Gid,
                        r.SentDate,
                        r.QuestionEndingDate,
                        r.QuestionReadDate,
                        r.Status,
                        r.RegistrationNumber,
                        r.ProcedureCode,
                        r.ProcedureName,
                        r.ProcedureNameAlt,
                        r.ProjectName,
                        r.ProjectNameAlt,
                        r.ProjectRegistrationStatus,
                        r.CompanyName,
                        r.CompanyNameAlt,
                        r.Question.TruncateWithEllipsis(200),
                        r.Version,
                        r.Answers))
                    .ToList(),
                Count = query.Count(),
            };
        }

        public RegMessageCountPVO GetCountForRegistration(int registrationId)
        {
            var result = (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>()
                          join rp in this.unitOfWork.DbContext.Set<RegProjectXml>() on pc.ProjectId equals rp.ProjectId
                          where rp.RegistrationId == registrationId && pc.Status != ProjectCommunicationStatus.DraftQuestion
                          group pc by pc.Status into g
                          select new { Status = g.Key, Count = g.Count() }).ToList();

            var newStatus = result.SingleOrDefault(r => r.Status == ProjectCommunicationStatus.Question);
            var draftStatus = result.SingleOrDefault(r => r.Status == ProjectCommunicationStatus.DraftAnswer);
            var finalizedStatus = result.SingleOrDefault(r => r.Status == ProjectCommunicationStatus.AnswerFinalized);
            var paperSubmittedStatus = result.SingleOrDefault(r => r.Status == ProjectCommunicationStatus.PaperAnswer);
            var submittedStatus = result.SingleOrDefault(r => r.Status == ProjectCommunicationStatus.Answer);
            var appliedStatus = result.SingleOrDefault(r => r.Status == ProjectCommunicationStatus.Applied);
            var rejectedStatus = result.SingleOrDefault(r => r.Status == ProjectCommunicationStatus.Rejected);
            var cancelledStatus = result.SingleOrDefault(r => r.Status == ProjectCommunicationStatus.Canceled);
            var expiredStatus = result.SingleOrDefault(r => r.Status == ProjectCommunicationStatus.Expired);

            return new RegMessageCountPVO()
            {
                NewCount = newStatus == null ? 0 : newStatus.Count,
                DraftCount = draftStatus == null ? 0 : draftStatus.Count,
                FinalizedCount = finalizedStatus == null ? 0 : finalizedStatus.Count,
                PaperSubmittedCount = paperSubmittedStatus == null ? 0 : paperSubmittedStatus.Count,
                SubmittedCount = submittedStatus == null ? 0 : submittedStatus.Count,
                AppliedCount = appliedStatus == null ? 0 : appliedStatus.Count,
                RejectedCount = rejectedStatus == null ? 0 : rejectedStatus.Count,
                CancelledCount = cancelledStatus == null ? 0 : cancelledStatus.Count,
                ExpiredCount = expiredStatus == null ? 0 : expiredStatus.Count,
            };
        }

        public int GetNextOrderNumber(int projectId)
        {
            var lastOrderNumver = this.Set()
                .Where(p => p.ProjectId == projectId)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumver.HasValue ? lastOrderNumver.Value + 1 : 1;
        }

        public int GetProjectId(int communicationId)
        {
            return (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>()
                    where pc.ProjectCommunicationId == communicationId
                    select pc.ProjectId).SingleOrDefault();
        }

        public int GetCommunicationId(Guid gid)
        {
            return (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>()
                    where pc.Gid == gid
                    select pc.ProjectCommunicationId).SingleOrDefault();
        }

        public IEnumerable<ProjectCommunication> GetProjectCommunicationsToExpire()
        {
            return (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>()
                    where ProjectCommunication.ExpirableStatuses.Contains(pc.Status)
                    && pc.QuestionEndingDate.HasValue
                    && pc.QuestionEndingDate < DateTime.Now
                    select pc).ToArray();
        }

        private void LoadCommunication(ProjectCommunication communication)
        {
            var entry = this.unitOfWork.DbContext.Entry(communication);

            entry.Collection(n => n.Files).Load();
            entry.Collection(n => n.Answers).Load();
        }

        public IList<ProjectCommunicationAnswerVO> GetProjectCommunicationAnswers(int communicationId)
        {
            return (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>().Where(pc => pc.ProjectCommunicationId == communicationId)
                    join ca in this.unitOfWork.DbContext.Set<ProjectCommunicationAnswer>() on pc.ProjectCommunicationId equals ca.ProjectCommunicationId
                    where ca.Status != ProjectCommunicationAnswerStatus.Draft && ca.Status != ProjectCommunicationAnswerStatus.AnswerFinalized
                    orderby ca.OrderNum descending
                    select new ProjectCommunicationAnswerVO()
                    {
                        ProjectCommunicationAnswerId = ca.ProjectCommunicationAnswerId,
                        XmlGid = ca.Gid,
                        OrderNum = ca.OrderNum,
                        Status = ca.Status,
                        StatusDescr = ca.Status,
                        AnswerDate = ca.Answer.MessageDate,
                        ProjectCommunicationId = pc.ProjectCommunicationId,
                        Source = ca.Source,
                    })
                    .ToList();
        }

        public ProjectCommunicationAnswer FindAnswer(int registrationId, string answerHash)
        {
            var answer = (from a in this.unitOfWork.DbContext.Set<ProjectCommunicationAnswer>()
                                        join pc in this.unitOfWork.DbContext.Set<ProjectCommunication>() on a.ProjectCommunicationId equals pc.ProjectCommunicationId
                                        join rp in this.unitOfWork.DbContext.Set<RegProjectXml>() on pc.ProjectId equals rp.ProjectId
                                        where a.Answer.Hash == answerHash && rp.RegistrationId == registrationId
                                        select a)
                                        .SingleOrDefault();

            if (answer == null)
            {
                throw new DataObjectNotFoundException("ProjectCommunicationAnswer", answerHash);
            }

            return answer;
        }

        public ProjectCommunication GetActiveProjectCommunication(int projectId)
        {
            return (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>()
                    where pc.Status == ProjectCommunicationStatus.Answer && pc.ProjectId == projectId
                    select pc)
                    .SingleOrDefault();
        }

        public IQueryable<ProjectQuestionExpirationEmailVO> GetCurrentExpiringQuestions()
        {
            var beginOfCurrentDay = DateTime.Now.ToStartOfDay();
            var endOfCurrentDay = DateTime.Now.ToEndOfDay();

            var predicate = PredicateBuilder.True<ProjectCommunication>()
                .And(s => ProjectCommunication.ExpirableStatuses.Contains(s.Status))
                .And(s => s.QuestionEndingDate <= endOfCurrentDay)
                .And(s => s.QuestionEndingDate >= beginOfCurrentDay);

            return from pc in this.Set().Where(predicate)
                   join p in this.unitOfWork.DbContext.Set<Project>() on pc.ProjectId equals p.ProjectId
                   join rpvxml in this.unitOfWork.DbContext.Set<RegProjectXml>() on p.ProjectId equals rpvxml.ProjectId
                   join r in this.unitOfWork.DbContext.Set<Registration>() on rpvxml.RegistrationId equals r.RegistrationId

                   select new ProjectQuestionExpirationEmailVO
                   {
                       ProjectCommunicationId = pc.ProjectCommunicationId,
                       ProjectName = p.Name,
                       ProjectRegNumber = p.RegNumber,
                       QuestionRegNumber = pc.RegNumber,
                       Recipient = r.Email,
                   };
        }

        private ProjectCommunication GetProjectCommunicationInternal(int registrationId, Guid projectCommunicationGid)
        {
            var projectCommunication = (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>()
                                        join rp in this.unitOfWork.DbContext.Set<RegProjectXml>() on pc.ProjectId equals rp.ProjectId
                                        where pc.Gid == projectCommunicationGid && rp.RegistrationId == registrationId
                                        select pc).SingleOrDefault();

            if (projectCommunication == null)
            {
                throw new UnauthorizedAccessException("Избраната комуникация не принадлежи на автентикирания потребител");
            }

            return projectCommunication;
        }
    }
}
