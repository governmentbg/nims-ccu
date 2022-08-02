using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Data.Registrations.PortalViewObjects;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.Registrations;

namespace Eumis.Data.Registrations.Repositories
{
    internal class RegProjectXmlsRepository : AggregateRepository<RegProjectXml>, IRegProjectXmlsRepository
    {
        public RegProjectXmlsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<RegProjectXml, object>>[] Includes
        {
            get
            {
                return new Expression<Func<RegProjectXml, object>>[]
                {
                    c => c.Files,
                };
            }
        }

        public RegProjectXml Find(int registrationId, Guid gid)
        {
            var regProjectXml = this.Set()
                .Where(r => r.Gid == gid && r.RegistrationId == registrationId)
                .SingleOrDefault();

            if (regProjectXml == null)
            {
                throw new DataObjectNotFoundException("RegProjectXml", gid);
            }

            return regProjectXml;
        }

        public RegProjectXml FindOrDefault(int registrationId, string hash)
        {
            var regProjectXml = this.Set()
                .Where(r => r.Hash == hash && r.RegistrationId == registrationId)
                .SingleOrDefault();

            return regProjectXml;
        }

        public RegProjectXml Find(string hashStartsWith)
        {
            var hashes = this.unitOfWork.DbContext.Set<RegProjectXml>()
                .Where(r => r.Hash.StartsWith(hashStartsWith))
                .Select(r => r.Hash)
                .Distinct()
                .ToArray();

            if (hashes.Length == 0)
            {
                throw new DataObjectNotFoundException("RegProjectXml", "hash startsWith - " + hashStartsWith);
            }

            if (hashes.Length > 1)
            {
                throw new DataException("more than one RegProjectXml.Hash starting with " + hashStartsWith);
            }

            var hash = hashes[0];

            return this.Set()
                .Where(r => r.Hash == hash)
                .Single();
        }

        public RegProjectXml FindForUpdate(int registrationId, Guid gid, byte[] version)
        {
            var regProjectXml = this.Find(registrationId, gid);

            this.CheckVersion(version, regProjectXml.Version);

            return regProjectXml;
        }

        public PagePVO<RegProjectXmlPVO> GetAllForRegistration(int registrationId, RegProjectXmlStatus status, int offset = 0, int? limit = null)
        {
            var query = from xml in this.unitOfWork.DbContext.Set<RegProjectXml>()
                        join pr in this.unitOfWork.DbContext.Set<Procedure>() on xml.ProcedureId equals pr.ProcedureId
                        join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pr.ProcedureId equals ps.ProcedureId
                        join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId

                        join proj in this.unitOfWork.DbContext.Set<Project>() on xml.ProjectId equals proj.ProjectId into g1
                        from proj in g1.DefaultIfEmpty()

                        where xml.RegistrationId == registrationId && xml.Status == status && ps.IsPrimary
                        select new
                        {
                            Gid = xml.Gid,
                            Status = xml.Status,
                            ModifyDate = xml.ModifyDate,
                            ProjectName = xml.ProjectName,
                            ProjectNameAlt = xml.ProjectNameAlt,
                            ProjectId = (int?)proj.ProjectId,
                            EvalStatus = (ProjectEvalStatus?)proj.EvalStatus,
                            RegStatus = (ProjectRegistrationStatus?)proj.RegistrationStatus,
                            CompanyName = xml.CompanyName,
                            CompanyNameAlt = xml.CompanyNameAlt,
                            RegistrationNumber = proj.RegNumber,
                            RegistrationDate = (DateTime?)proj.RegDate,
                            RegistrationType = xml.RegistrationType,
                            ProcedureId = pr.ProcedureId,
                            ProcedureName = pr.Name,
                            ProcedureNameAlt = pr.NameAlt,
                            ProcedureCode = pr.Code,
                            ProgrammeName = prog.Name,
                            ProgrammeNameAlt = prog.NameAlt,
                            Hash = xml.Hash,
                        };

            var enumeratedQuery = query
                .OrderByDescending(p => p.ModifyDate)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            int count = query.Count();

            int[] procedureIds = enumeratedQuery.Select(p => p.ProcedureId).Distinct().ToArray();

            var procedureTimeLimits =
               (from ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                where procedureIds.Contains(ptl.ProcedureId)
                group ptl by ptl.ProcedureId into g
                select new
                {
                    ProcedureId = g.Key,
                    EndDate = g.Where(ptl => ptl.EndDate >= DateTime.Now).Min(ptl => (DateTime?)ptl.EndDate) ?? g.Max(ptl => ptl.EndDate),
                })
                .ToDictionary(p => p.ProcedureId, p => p.EndDate);

            int[] projectIds = enumeratedQuery.Where(p => p.ProjectId.HasValue).Select(p => p.ProjectId.Value).Distinct().ToArray();

            var evalSessionCommunication = (from pc in this.unitOfWork.DbContext.Set<ProjectCommunication>()
                                            where projectIds.Contains(pc.ProjectId) && pc.Status != ProjectCommunicationStatus.DraftQuestion
                                            group pc by pc.ProjectId into g
                                            select new
                                            {
                                                ProjectId = g.Key,
                                                Communication = g.Select(pc =>
                                                    new
                                                    {
                                                        MessageGid = pc.Gid,
                                                        MessageDate = pc.Question.MessageDate,
                                                        RegistrationNumber = pc.RegNumber,
                                                        Status = pc.Status,
                                                        Answers = pc.Answers,
                                                    })
                                                    .ToList(),
                                            })
                                            .ToDictionary(
                                                 g => g.ProjectId,
                                                 g => g.Communication
                                                     .Select(c =>
                                                         new MessagePVO(
                                                             c.MessageGid,
                                                             c.RegistrationNumber,
                                                             c.MessageDate,
                                                             c.Status,
                                                             c.Answers))
                                                     .ToList());

            var projectCommunication = (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>()
                                        where projectIds.Contains(pc.ProjectId) &&
                                        !(pc.Source == ProjectManagingAuthorityCommunicationSource.ManagingAuthority &&
                                        pc.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.DraftQuestion)
                                        orderby pc.CreateDate descending
                                        group pc by pc.ProjectId into g
                                        select new
                                        {
                                            ProjectId = g.Key,
                                            Communication = g.Select(pc =>
                                                new
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
                                                .ToList(),
                                        })
                                        .ToDictionary(
                                             g => g.ProjectId,
                                             g => g.Communication
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
                                                    pc.Answers))
                                                 .ToList());

            var projectVersions = (from pv in this.unitOfWork.DbContext.Set<ProjectVersionXml>()
                                   group pv by pv.ProjectId into g
                                   select new
                                   {
                                       ProjectId = g.Key,
                                       ProjectVersion = g.Select(pv =>
                                           new
                                           {
                                               Gid = pv.Gid,
                                               CreateDate = pv.CreateDate,
                                               ModifyDate = pv.ModifyDate,
                                               Note = pv.CreateNote,
                                               NoteAlt = pv.CreateNoteAlt,
                                               Status = pv.Status,
                                           }),
                                   })
                                   .ToDictionary(
                                             g => g.ProjectId,
                                             g => g.ProjectVersion
                                                 .OrderByDescending(pv => pv.CreateDate)
                                                 .Select(pv => new ProjectVersionPVO(
                                                     pv.Gid,
                                                     pv.Note,
                                                     pv.NoteAlt,
                                                     pv.CreateDate,
                                                     pv.ModifyDate,
                                                     pv.Status))
                                                 .ToList());

            var results = enumeratedQuery
                .Select(p => new RegProjectXmlPVO
                {
                    Gid = p.Gid,
                    ModifyDate = p.ModifyDate,
                    Hash = p.Hash,
                    ProjectName = p.ProjectName,
                    ProjectNameAlt = p.ProjectNameAlt,
                    CompanyName = p.CompanyName,
                    CompanyNameAlt = p.CompanyNameAlt,
                    ProcedureName = p.ProcedureName,
                    ProcedureNameAlt = p.ProcedureNameAlt,
                    ProcedureCode = p.ProcedureCode,
                    ProcedureEndingDate = procedureTimeLimits[p.ProcedureId],
                    ProgrammeName = p.ProgrammeName,
                    ProgrammeNameAlt = p.ProgrammeNameAlt,

                    RegistrationNumber = p.RegistrationNumber,
                    RegistrationDate = p.RegistrationDate,
                    RegistrationType = p.RegistrationType,
                    RegistrationTypeText = p.RegistrationType,
                    ProjectStatus = p.EvalStatus,
                    ProjectStatusText = p.EvalStatus,
                    ProjectRegStatus = p.RegStatus,
                    ProjectRegStatusText = p.RegStatus,
                    Messages = p.ProjectId.HasValue && evalSessionCommunication.ContainsKey(p.ProjectId.Value) ?
                                evalSessionCommunication[p.ProjectId.Value] :
                                new List<MessagePVO>(),
                    ProjectCommunications = p.ProjectId.HasValue && projectCommunication.ContainsKey(p.ProjectId.Value) ?
                                projectCommunication[p.ProjectId.Value] :
                                new List<ProjectCommunicationQuestionPVO>(),
                    ProjectVersions = (p.EvalStatus == ProjectEvalStatus.Evaluated || p.EvalStatus == ProjectEvalStatus.Contracted) && p.ProjectId.HasValue && projectVersions.ContainsKey(p.ProjectId.Value) ?
                                projectVersions[p.ProjectId.Value] :
                                new List<ProjectVersionPVO>(),

                    SubmitDate = p.Status == RegProjectXmlStatus.Submitted ? (DateTime?)p.ModifyDate : null,
                })
                .ToList();

            return new PagePVO<RegProjectXmlPVO>
            {
                Results = results,
                Count = count,
            };
        }

        public PagePVO<RegProjectXmlPVO> GetAllProjectCommunicationsForRegistration(int registrationId, int offset = 0, int? limit = null)
        {
            var query = from xml in this.unitOfWork.DbContext.Set<RegProjectXml>()
                        join pr in this.unitOfWork.DbContext.Set<Procedure>() on xml.ProcedureId equals pr.ProcedureId
                        join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pr.ProcedureId equals ps.ProcedureId
                        join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId

                        join proj in this.unitOfWork.DbContext.Set<Project>() on xml.ProjectId equals proj.ProjectId into g1
                        from proj in g1.DefaultIfEmpty()

                        where xml.RegistrationId == registrationId && xml.Status == RegProjectXmlStatus.Registered && ps.IsPrimary
                        select new
                        {
                            Gid = xml.Gid,
                            Status = xml.Status,
                            ModifyDate = xml.ModifyDate,
                            ProjectName = xml.ProjectName,
                            ProjectNameAlt = xml.ProjectNameAlt,
                            ProjectId = (int?)proj.ProjectId,
                            RegStatus = (ProjectRegistrationStatus?)proj.RegistrationStatus,
                            CompanyName = xml.CompanyName,
                            CompanyNameAlt = xml.CompanyNameAlt,
                            RegistrationNumber = proj.RegNumber,
                            RegistrationDate = (DateTime?)proj.RegDate,
                            RegistrationType = xml.RegistrationType,
                            ProcedureId = pr.ProcedureId,
                            ProcedureName = pr.Name,
                            ProcedureNameAlt = pr.NameAlt,
                            ProcedureCode = pr.Code,
                            ProgrammeName = prog.Name,
                            ProgrammeNameAlt = prog.NameAlt,
                            Hash = xml.Hash,
                        };

            var enumeratedQuery = query
                .OrderByDescending(p => p.ModifyDate)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            int count = query.Count();

            int[] projectIds = enumeratedQuery.Where(p => p.ProjectId.HasValue).Select(p => p.ProjectId.Value).Distinct().ToArray();

            var communications = (from pc in this.unitOfWork.DbContext.Set<ProjectManagingAuthorityCommunication>()
                                  where projectIds.Contains(pc.ProjectId) &&
                                  !(pc.Source == ProjectManagingAuthorityCommunicationSource.ManagingAuthority &&
                                  pc.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.DraftQuestion)
                                  group pc by pc.ProjectId into g
                                  select new
                                  {
                                      ProjectId = g.Key,
                                      HasMessages = g.Select(pc => pc).Count() > 0,
                                      HasNewQuestions = g.Any(pc =>
                                        pc.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.Question &&
                                        pc.Source == ProjectManagingAuthorityCommunicationSource.ManagingAuthority &&
                                        !pc.QuestionReadDate.HasValue),
                                      HasNewAnswers = g.Any(pc =>
                                        pc.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.Question &&
                                        pc.Source == ProjectManagingAuthorityCommunicationSource.Beneficiary &&
                                        pc.Answers.Any(a =>
                                            a.Status == ProjectCommunicationAnswerStatus.Answer &&
                                            a.Source == ProjectCommunicationAnswerSource.ManagingAuthority &&
                                            !a.ReadDate.HasValue)),
                                  })
                                  .ToDictionary(
                                      g => g.ProjectId,
                                      g => new
                                      {
                                          g.HasMessages,
                                          g.HasNewQuestions,
                                          g.HasNewAnswers,
                                      });

            var results = enumeratedQuery
                .Select(p => new RegProjectXmlPVO
                {
                    Gid = p.Gid,
                    ModifyDate = p.ModifyDate,
                    Hash = p.Hash,
                    ProjectName = p.ProjectName,
                    ProjectNameAlt = p.ProjectNameAlt,
                    CompanyName = p.CompanyName,
                    CompanyNameAlt = p.CompanyNameAlt,
                    ProcedureName = p.ProcedureName,
                    ProcedureNameAlt = p.ProcedureNameAlt,
                    ProcedureCode = p.ProcedureCode,
                    ProgrammeName = p.ProgrammeName,
                    ProgrammeNameAlt = p.ProgrammeNameAlt,

                    RegistrationNumber = p.RegistrationNumber,
                    RegistrationDate = p.RegistrationDate,
                    RegistrationType = p.RegistrationType,
                    RegistrationTypeText = p.RegistrationType,
                    ProjectRegStatus = p.RegStatus,
                    ProjectRegStatusText = p.RegStatus,
                    HasCommunications = p.ProjectId.HasValue && communications.ContainsKey(p.ProjectId.Value) ?
                        communications[p.ProjectId.Value].HasMessages : false,
                    HasNewQuestions = p.ProjectId.HasValue && communications.ContainsKey(p.ProjectId.Value) ?
                        communications[p.ProjectId.Value].HasNewQuestions : false,
                    HasNewAnswers = p.ProjectId.HasValue && communications.ContainsKey(p.ProjectId.Value) ?
                        communications[p.ProjectId.Value].HasNewAnswers : false,

                    SubmitDate = p.Status == RegProjectXmlStatus.Submitted ? (DateTime?)p.ModifyDate : null,
                })
                .OrderByDescending(c => c.HasCommunications)
                .ToList();

            return new PagePVO<RegProjectXmlPVO>
            {
                Results = results,
                Count = count,
            };
        }

        public string[] SubmittedHashesStartingWith(string startsWith, int[] programmeIds)
        {
            if (programmeIds.Length == 0)
            {
                return Enumerable.Empty<string>().ToArray();
            }

            var draftHashes = (from rpx in this.unitOfWork.DbContext.Set<RegProjectXml>()
                               join pr in this.unitOfWork.DbContext.Set<Procedure>() on rpx.ProcedureId equals pr.ProcedureId
                               join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pr.ProcedureId equals ps.ProcedureId
                               where ps.IsPrimary && programmeIds.Contains(ps.ProgrammeId) && rpx.Hash.StartsWith(startsWith) && rpx.Status == RegProjectXmlStatus.Submitted
                               select rpx.Hash)
                                .ToList();

            return draftHashes.Distinct().ToArray();
        }

        public int? GetProjectId(Guid registrationGid)
        {
            return this.unitOfWork.DbContext.Set<RegProjectXml>()
                .Where(r => r.Gid == registrationGid)
                .Select(r => r.ProjectId)
                .SingleOrDefault();
        }
    }
}
