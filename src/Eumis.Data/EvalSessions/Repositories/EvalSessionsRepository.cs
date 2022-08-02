using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Data.EvalSessions.PortalViewObjects;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.Views;
using Eumis.Domain.Projects;
using Eumis.Domain.Registrations;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionsRepository : AggregateRepository<EvalSession>, IEvalSessionsRepository
    {
        public EvalSessionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<EvalSession, object>>[] Includes
        {
            get
            {
                return new Expression<Func<EvalSession, object>>[]
                {
                    p => p.EvalSessionUsers,
                    p => p.EvalSessionProjects,
                    p => p.EvalSessionSheets,
                    p => p.EvalSessionDistributions.Select(t => t.EvalSessionDistributionUsers),
                    p => p.EvalSessionDistributions.Select(t => t.EvalSessionDistributionProjects),
                    p => p.EvalSessionEvaluations.Select(t => t.EvalSessionEvaluationSheets),
                    p => p.EvalSessionDocuments.Select(t => t.File),
                    p => p.EvalSessionStandings,
                    p => p.EvalSessionProjectStandings,
                    p => p.EvalSessionStandpoints,
                    p => p.EvalSessionResults.Select(t => t.Projects),
                };
            }
        }

        public bool IsProjectApprovedOrReserveInEvalSession(int projectId)
        {
            return (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>() on es.EvalSessionId equals esp.EvalSessionId
                    where esp.ProjectId == projectId &&
                          !esp.IsDeleted &&
                          !esp.IsPreliminary &&
                          (esp.Status == EvalSessionProjectStandingStatus.Approved || esp.Status == EvalSessionProjectStandingStatus.Reserve)
                    select es.EvalSessionId).Any();
        }

        public bool IsEvalSessionUser(int userId, int evalSessionId, EvalSessionUserType userType)
        {
            return (from esu in this.unitOfWork.DbContext.Set<EvalSessionUser>()
                    where esu.UserId == userId && esu.EvalSessionId == evalSessionId && esu.Type == userType && esu.Status == EvalSessionUserStatus.Activated
                    select esu)
                    .Any();
        }

        public bool IsEvalSessionProjectUserAdmin(int userId, int projectId)
        {
            return (from esu in this.unitOfWork.DbContext.Set<EvalSessionUser>()
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on esu.EvalSessionId equals esp.EvalSessionId
                    where esu.UserId == userId && esp.ProjectId == projectId && esu.Type == EvalSessionUserType.Administrator && esu.Status == EvalSessionUserStatus.Activated
                    select esu)
                    .Any();
        }

        public bool IsEvalSessionProjectAssessor(int userId, int projectId)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionSheet>()
                    join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>() on ess.EvalSessionUserId equals esu.EvalSessionUserId
                    where esu.UserId == userId && ess.ProjectId == projectId && esu.Type == EvalSessionUserType.Assessor && esu.Status == EvalSessionUserStatus.Activated
                    select ess.EvalSessionSheetId).Any();
        }

        public bool IsEvalSessionProjectAssistantAssessor(int userId, int projectId)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionStandpoint>()
                    join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>() on ess.EvalSessionUserId equals esu.EvalSessionUserId
                    where esu.UserId == userId && ess.ProjectId == projectId && esu.Type == EvalSessionUserType.AssistantAssessor && esu.Status == EvalSessionUserStatus.Activated
                    select ess.EvalSessionStandpointId).Any();
        }

        public bool IsEvalSessionProjectObserver(int userId, int projectId)
        {
            return (from esu in this.unitOfWork.DbContext.Set<EvalSessionUser>()
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on esu.EvalSessionId equals esp.EvalSessionId
                    where esu.UserId == userId && esp.ProjectId == projectId && esu.Type == EvalSessionUserType.Observer && esu.Status == EvalSessionUserStatus.Activated
                    select esu)
                .Any();
        }

        public bool IsAssessorAssociatedWithEvalSessionSheet(int evalSessionSheetId, int userId)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionSheet>()
                    join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>() on ess.EvalSessionUserId equals esu.EvalSessionUserId
                    where ess.EvalSessionSheetId == evalSessionSheetId && esu.UserId == userId && esu.Type == EvalSessionUserType.Assessor && esu.Status == EvalSessionUserStatus.Activated
                    select ess)
                .Any();
        }

        public bool IsAssessorAssociatedWithEvalSessionStandpoint(int standpointId, int userId)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionStandpoint>()
                    join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>() on ess.EvalSessionUserId equals esu.EvalSessionUserId
                    where ess.EvalSessionStandpointId == standpointId && esu.UserId == userId && esu.Type == EvalSessionUserType.AssistantAssessor && esu.Status == EvalSessionUserStatus.Activated
                    select ess)
                .Any();
        }

        public int GetProgrammeId(int evalSessionId)
        {
            return (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on es.ProcedureId equals ps.ProcedureId
                    where ps.IsPrimary && es.EvalSessionId == evalSessionId
                    select ps.ProgrammeId)
                    .SingleOrDefault();
        }

        public EvalSessionStatus GetEvalSessionStatus(int evalSessionId)
        {
            return (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                    where es.EvalSessionId == evalSessionId
                    select es.EvalSessionStatus).SingleOrDefault();
        }

        public string GetEvalSessionNumber(int evalSessionId)
        {
            return (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                    where es.EvalSessionId == evalSessionId
                    select es.SessionNum).SingleOrDefault();
        }

        public IList<EvalSessionsVO> GetEvalSessions(int userId, int[] programmeIdsCanAdministrate, int[] programmeIdsCanRead, int? procedureId = null)
        {
            var predicate = PredicateBuilder.True<EvalSession>();

            predicate = predicate
                .AndEquals(p => p.ProcedureId, procedureId);

            var evalSessionsAsSessionsAdmin =
                from es in this.unitOfWork.DbContext.Set<EvalSession>().Where(predicate)
                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on es.ProcedureId equals ps.ProcedureId
                where ps.IsPrimary && programmeIdsCanAdministrate.Contains(ps.ProgrammeId)
                select es;

            var evalSessionsAsSessionAdminOrObserver = (from es in this.unitOfWork.DbContext.Set<EvalSession>().Where(predicate)
                                                        join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>() on es.EvalSessionId equals esu.EvalSessionId
                                                        where esu.UserId == userId && (esu.Type == EvalSessionUserType.Administrator || esu.Type == EvalSessionUserType.Observer) && esu.Status == EvalSessionUserStatus.Activated
                                                        select es).Distinct();

            var evalSessionsAsAssessor = (from es in this.unitOfWork.DbContext.Set<EvalSession>().Where(predicate)
                                          join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>() on es.EvalSessionId equals esu.EvalSessionId
                                          where esu.UserId == userId && esu.Type != EvalSessionUserType.Administrator && esu.Type != EvalSessionUserType.Observer && esu.Status == EvalSessionUserStatus.Activated
                                          select es).Distinct();

            var evalSessionsAsReader =
                from es in this.unitOfWork.DbContext.Set<EvalSession>().Where(predicate)
                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on es.ProcedureId equals ps.ProcedureId
                where ps.IsPrimary && programmeIdsCanRead.Contains(ps.ProgrammeId) && (es.EvalSessionStatus == EvalSessionStatus.Ended || es.EvalSessionStatus == EvalSessionStatus.EndedByLAG)
                select es;

            return (from es in this.unitOfWork.DbContext.Set<EvalSession>().Where(predicate)
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on es.ProcedureId equals p.ProcedureId

                    join es1 in evalSessionsAsSessionsAdmin on es.EvalSessionId equals es1.EvalSessionId into g1
                    from es1 in g1.DefaultIfEmpty()

                    join es2 in evalSessionsAsSessionAdminOrObserver on es.EvalSessionId equals es2.EvalSessionId into g2
                    from es2 in g2.DefaultIfEmpty()

                    join es3 in evalSessionsAsAssessor on es.EvalSessionId equals es3.EvalSessionId into g3
                    from es3 in g3.DefaultIfEmpty()

                    join es4 in evalSessionsAsReader on es.EvalSessionId equals es4.EvalSessionId into g4
                    from es4 in g4.DefaultIfEmpty()

                    where es1 != null || es2 != null || es3 != null || es4 != null
                    orderby es.CreateDate descending
                    select new EvalSessionsVO
                    {
                        EvalSessionId = es.EvalSessionId,
                        ProcedureId = es.ProcedureId,
                        ProcedureNameBg = p.Name,
                        ProcedureNameEn = p.NameAlt,
                        EvalSessionStatusName = es.EvalSessionStatus,
                        EvalSessionStatus = es.EvalSessionStatus,
                        EvalSessionType = es.EvalSessionType,
                        SessionNum = es.SessionNum,
                        SessionDate = es.SessionDate,
                        OrderDate = es.OrderDate,
                        OrderNum = es.OrderNum,
                        IsSessionsAdmin = es1 != null,
                        IsSessionAdminOrObserver = es2 != null,
                        IsAssessor = es3 != null,
                        IsReader = es4 != null,
                    })
                .ToList();
        }

        public IList<EvalSessionUsersVO> GetEvalSessionUsers(int evalSessionId)
        {
            return (from esu in this.unitOfWork.DbContext.Set<EvalSessionUser>()
                    join u in this.unitOfWork.DbContext.Set<User>() on esu.UserId equals u.UserId
                    where esu.EvalSessionId == evalSessionId
                    select new EvalSessionUsersVO
                    {
                        EvalSessionUserId = esu.EvalSessionUserId,
                        EvalSessionId = esu.EvalSessionId,
                        UserId = u.UserId,
                        Username = u.Username,
                        Fullname = u.Fullname,
                        Type = esu.Type,
                        Status = esu.Status,
                        StatusName = esu.Status,
                        Position = esu.Position,
                    })
                    .ToList();
        }

        public IList<EvalSessionProjectsVO> GetEvalSessionProjects(int evalSessionId)
        {
            return (from esp in this.unitOfWork.DbContext.Set<EvalSessionProject>()
                    join p in this.unitOfWork.DbContext.Set<Project>() on esp.ProjectId equals p.ProjectId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId

                    join pkc in this.unitOfWork.DbContext.Set<KidCode>() on p.KidCodeId equals pkc.KidCodeId into g1
                    from pkc in g1.DefaultIfEmpty()

                    join ese1 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.AdminAdmiss && p.IsDeleted == false)
                        on new { esp.EvalSessionId, esp.ProjectId } equals new { ese1.EvalSessionId, ese1.ProjectId } into g2
                    from ese1 in g2.DefaultIfEmpty()

                    join ese2 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.TechFinance && p.IsDeleted == false)
                        on new { esp.EvalSessionId, esp.ProjectId } equals new { ese2.EvalSessionId, ese2.ProjectId } into g3
                    from ese2 in g3.DefaultIfEmpty()

                    join ese3 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.Complex && p.IsDeleted == false)
                        on new { esp.EvalSessionId, esp.ProjectId } equals new { ese3.EvalSessionId, ese3.ProjectId } into g4
                    from ese3 in g4.DefaultIfEmpty()

                    join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(p => !p.IsDeleted && !p.IsPreliminary) on new { esp.EvalSessionId, esp.ProjectId } equals new { esps.EvalSessionId, esps.ProjectId } into g5
                    from esps in g5.DefaultIfEmpty()

                    join pc in this.unitOfWork.DbContext.Set<ProjectCommunication>().Where(p => !ProjectCommunication.FinalStatuses.Contains(p.Status)) on new { esp.EvalSessionId, esp.ProjectId } equals new { pc.EvalSessionId, pc.ProjectId } into g6
                    from pc in g6.DefaultIfEmpty()

                    join px in this.unitOfWork.DbContext.Set<ProjectVersionXml>().Where(p => p.Status == ProjectVersionXmlStatus.Draft) on esp.ProjectId equals px.ProjectId into g7
                    from px in g7.DefaultIfEmpty()

                    where esp.EvalSessionId == evalSessionId
                    select new
                    {
                        esp.EvalSessionId,
                        esp.ProjectId,
                        ProcedureName = proc.Name,
                        p.RegNumber,
                        ProjectName = p.Name,
                        ProjectNameAlt = p.NameAlt,
                        ProjectKidCode = pkc.Code,
                        p.CompanyName,
                        p.CompanyNameAlt,
                        p.CompanyUinType,
                        p.CompanyUin,
                        p.RegDate,
                        p.RegistrationStatus,
                        esp.IsDeleted,

                        ASDEvalIsPassed = (bool?)ese1.EvalIsPassed,
                        ASDEvalPoints = ese1.EvalPoints,

                        TFOEvalIsPassed = (bool?)ese2.EvalIsPassed,
                        TFOEvalPoints = ese2.EvalPoints,

                        ComplexEvalIsPassed = (bool?)ese3.EvalIsPassed,
                        ComplexEvalPoints = ese3.EvalPoints,

                        PreliminaryEvalIsPassed = (bool?)true,
                        PreliminaryEvalPoints = (decimal?)null,

                        EvalSessionProjectStandingId = (int?)esps.EvalSessionProjectStandingId,
                        OrderNum = (int?)esps.OrderNum,
                        StandingStatus = (EvalSessionProjectStandingStatus?)esps.Status,

                        ProjectCommunicationId = (int?)pc.ProjectCommunicationId,
                        ProjectVersionXmlId = (int?)px.ProjectVersionXmlId,
                    })
                    .ToList()
                    .Select(t => new EvalSessionProjectsVO
                    {
                        EvalSessionId = t.EvalSessionId,
                        ProjectId = t.ProjectId,
                        ProcedureName = t.ProcedureName,
                        ProjectRegNumber = t.RegNumber,
                        ProjectNameBg = t.ProjectName,
                        ProjectNameEn = t.ProjectNameAlt,
                        ProjectKidCode = t.ProjectKidCode,
                        CompanyBg = string.Format("{0} ({1}: {2})", t.CompanyName, t.CompanyUinType.GetEnumDescription(), t.CompanyUin),
                        CompanyEn = string.Format("{0} ({1}: {2})", t.CompanyNameAlt, t.CompanyUinType.GetEnumDescription(new System.Globalization.CultureInfo(SystemLocalization.En_GB)), t.CompanyUin),
                        ProjectRegDate = t.RegDate,
                        ProjectRegistrationStatus = t.RegistrationStatus,
                        ProjectRegistrationStatusName = t.RegistrationStatus,
                        IsDeleted = t.IsDeleted,

                        IsPassedASD = t.ASDEvalIsPassed,
                        PointsASD = t.ASDEvalPoints,

                        IsPassedTFO = t.TFOEvalIsPassed,
                        PointsTFO = t.TFOEvalPoints,

                        IsPassedComplex = t.ComplexEvalIsPassed,
                        PointsComplex = t.ComplexEvalPoints,

                        IsPassedPreliminary = t.PreliminaryEvalIsPassed,
                        PointsPreliminary = t.PreliminaryEvalPoints,

                        EvalSessionProjectStandingId = t.EvalSessionProjectStandingId,
                        OrderNum = t.OrderNum,
                        StandingStatus = t.StandingStatus,

                        WorkStatus =
                            t.ProjectCommunicationId.HasValue ? EvalSessionProjectWorkStatus.ComunicationInProgress :
                            t.ProjectVersionXmlId.HasValue ? EvalSessionProjectWorkStatus.DraftVersion :
                            (EvalSessionProjectWorkStatus?)null,
                    })
                    .OrderBy(t => t.ProjectRegNumber)
                    .ToList();
        }

        public IList<EvalSessionSheetsVO> GetEvalSessionSheets(
            int evalSessionId,
            int? projectId,
            ProcedureEvalTableType? evalTableType,
            int? distributionId,
            int? assessorId,
            int? userId,
            EvalSessionSheetStatus[] statuses)
        {
            var predicate = PredicateBuilder.True<EvalSessionSheet>();
            var projPredicate = PredicateBuilder.True<Project>();
            var evalSessionUserPredicate = PredicateBuilder.True<EvalSessionUser>();

            predicate = predicate
                .AndEquals(p => p.EvalTableType, evalTableType)
                .AndEquals(p => p.EvalSessionDistributionId, distributionId)
                .AndEquals(p => p.EvalSessionUserId, assessorId);

            evalSessionUserPredicate = evalSessionUserPredicate
                .AndEquals(p => p.UserId, userId);

            if (statuses != null && statuses.Count() != 0)
            {
                predicate = predicate.And(p => statuses.Contains(p.Status));
            }

            projPredicate = projPredicate
                .AndEquals(p => p.ProjectId, projectId);

            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionSheet>().Where(predicate)
                    join essXml in this.unitOfWork.DbContext.Set<EvalSessionSheetXml>() on ess.EvalSessionSheetId equals essXml.EvalSessionSheetId
                    join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>().Where(evalSessionUserPredicate) on ess.EvalSessionUserId equals esu.EvalSessionUserId
                    join p in this.unitOfWork.DbContext.Set<Project>().Where(projPredicate) on ess.ProjectId equals p.ProjectId
                    join u in this.unitOfWork.DbContext.Set<User>() on esu.UserId equals u.UserId
                    where ess.EvalSessionId == evalSessionId
                    select new EvalSessionSheetsVO
                    {
                        EvalSessionId = ess.EvalSessionId,
                        EvalSessionSheetId = ess.EvalSessionSheetId,
                        Assessor = u.Fullname + "(" + u.Username + ")",
                        ProjectRegNumber = p.RegNumber,
                        ProjectNameBg = p.Name,
                        ProjectNameEn = p.NameAlt,
                        EvalTableType = ess.EvalTableType,
                        EvalTableTypeName =
                            ess.EvalTableType == ProcedureEvalTableType.AdminAdmiss ? ProcedureEvalTableTypeShort.AdminAdmiss :
                            ess.EvalTableType == ProcedureEvalTableType.TechFinance ? ProcedureEvalTableTypeShort.TechFinance :
                            ess.EvalTableType == ProcedureEvalTableType.Complex ? ProcedureEvalTableTypeShort.Complex :
                            default(ProcedureEvalTableTypeShort),
                        StatusName = ess.Status,
                        Status = ess.Status,
                        DistributionType = ess.DistributionType,
                        XmlGid = essXml.Gid,
                        EvalType = essXml.EvalType,
                        EvalIsPassed = essXml.EvalIsPassed,
                        EvalPoints = essXml.EvalPoints,
                        EvalNote = essXml.EvalNote,
                    })
                .OrderByDescending(t => t.EvalSessionSheetId)
                .ThenBy(t => t.ProjectRegNumber)
                .ToList();
        }

        public IList<EvalSessionStandpointVO> GetEvalSessionStandpoints(
            int? evalSessionId,
            int? projectId = null,
            int? evalSessionUserId = null,
            int? userId = null,
            EvalSessionStandpointStatus[] statuses = null)
        {
            var predicate = PredicateBuilder.True<EvalSessionStandpoint>();
            var evalSessionUserPredicate = PredicateBuilder.True<EvalSessionUser>();

            predicate = predicate
                .AndEquals(s => s.EvalSessionId, evalSessionId)
                .AndEquals(s => s.EvalSessionUserId, evalSessionUserId)
                .AndEquals(s => s.ProjectId, projectId);

            evalSessionUserPredicate = evalSessionUserPredicate
                .AndEquals(p => p.UserId, userId);

            if (statuses != null && statuses.Count() != 0)
            {
                predicate = predicate.And(p => statuses.Contains(p.Status));
            }

            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionStandpoint>().Where(predicate)
                    join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>().Where(evalSessionUserPredicate) on ess.EvalSessionUserId equals esu.EvalSessionUserId
                    join essx in this.unitOfWork.DbContext.Set<EvalSessionStandpointXml>() on ess.EvalSessionStandpointId equals essx.EvalSessionStandpointId
                    join p in this.unitOfWork.DbContext.Set<Project>() on ess.ProjectId equals p.ProjectId
                    join u in this.unitOfWork.DbContext.Set<User>() on esu.UserId equals u.UserId
                    orderby ess.CreateDate descending, p.RegNumber
                    select new EvalSessionStandpointVO
                    {
                        EvalSessionId = ess.EvalSessionId,
                        EvalSessionStandpointId = ess.EvalSessionStandpointId,
                        XmlGid = essx.Gid,
                        User = u.Fullname + "(" + u.Username + ")",
                        ProjectRegNumber = p.RegNumber,
                        ProjectName = p.Name,
                        Note = ess.Note,
                        StatusName = ess.Status,
                        Status = ess.Status,
                    })
                .ToList();
        }

        public IList<EvalSessionDistributionsVO> GetEvalSessionDistributions(int evalSessionId)
        {
            return (from esd in this.unitOfWork.DbContext.Set<EvalSessionDistribution>()
                    where esd.EvalSessionId == evalSessionId
                    orderby esd.CreateDate descending
                    select new EvalSessionDistributionsVO
                    {
                        EvalSessionId = esd.EvalSessionId,
                        EvalSessionDistributionId = esd.EvalSessionDistributionId,
                        EvalTableType = esd.EvalTableType,
                        Code = esd.Code,
                        CreateDate = esd.CreateDate,
                        StatusName = esd.Status,
                        Status = esd.Status,
                        StatusNote = esd.StatusNote,
                        AssessorsPerProject = esd.AssessorsPerProject,
                    })
                    .ToList();
        }

        public IList<ProjectRegistrationsVO> GetProjectRegistrations(
            int[] programmeIds,
            int evalSessionId,
            DateTime? fromDate,
            DateTime? toDate,
            int? companySizeTypeId,
            int? companyKidCodeId,
            int? projectKidCodeId)
        {
            var allowedStatusesForChoice = new List<ProjectRegistrationStatus>()
            {
                ProjectRegistrationStatus.Registered,
                ProjectRegistrationStatus.RegisteredLate,
            };

            var predicate = PredicateBuilder.True<Project>()
                .AndEquals(p => p.KidCodeId, projectKidCodeId);

            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(t => t.RegDate, fromDate)
                .AndDateTimeLessThanOrEqual(t => t.RegDate, toDate);

            var evalSessionsSet = this.unitOfWork.DbContext.Set<EvalSession>().AsQueryable();
            var evalSessinosProjectsSet = this.unitOfWork.DbContext.Set<EvalSessionProject>().AsQueryable();

            var evalSessionProcedureId = (from es in evalSessionsSet
                                          where es.EvalSessionId == evalSessionId
                                          select es.ProcedureId)
                                         .Single();

            predicate = predicate.And(p =>
                !(from esp in evalSessinosProjectsSet
                  where esp.ProjectId == p.ProjectId &&
                        esp.EvalSessionId == evalSessionId
                  select esp.ProjectId)
                .Any());

            predicate = predicate.And(p =>
                !(from esp in evalSessinosProjectsSet
                  join es in evalSessionsSet on esp.EvalSessionId equals es.EvalSessionId
                  where esp.ProjectId == p.ProjectId &&
                      es.EvalSessionId != evalSessionId &&
                      es.EvalSessionStatus != EvalSessionStatus.Canceled &&
                      esp.IsDeleted == false
                  select esp.ProjectId)
                .Any());

            return (from p in this.unitOfWork.DbContext.Set<Project>().Where(predicate)
                    join pt in this.unitOfWork.DbContext.Set<ProjectType>() on p.ProjectTypeId equals pt.ProjectTypeId
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId

                    join pkc in this.unitOfWork.DbContext.Set<KidCode>() on p.KidCodeId equals pkc.KidCodeId into g2
                    from pkc in g2.DefaultIfEmpty()

                    where allowedStatusesForChoice.Contains(p.RegistrationStatus) &&
                            p.ProcedureId == evalSessionProcedureId &&
                            ps.IsPrimary &&
                            programmeIds.Contains(ps.ProgrammeId)

                    orderby p.RegNumber
                    select new ProjectRegistrationsVO
                    {
                        ProjectId = p.ProjectId,
                        RegNumber = p.RegNumber,
                        NameBg = p.Name,
                        NameEn = p.NameAlt,
                        KidCode = pkc.Code,
                        CompanyNameBg = p.CompanyName,
                        CompanyNameEn = p.CompanyNameAlt,
                        CompanyUinType = p.CompanyUinType,
                        CompanyUin = p.CompanyUin,
                        RegistrationStatus = p.RegistrationStatus,
                        ProjectTypeBg = pt.Name,
                        ProjectTypeEn = pt.NameAlt,
                        RegDate = p.RegDate,
                    })
                    .ToList();
        }

        public EvalSession GetWithIncludedProjectStandingsAndEvaluations(int evalSessionId)
        {
            (from es in this.unitOfWork.DbContext.Set<EvalSession>()
             join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>() on es.EvalSessionId equals esps.EvalSessionId
             where es.EvalSessionId == evalSessionId
             select esps).Load();

            (from es in this.unitOfWork.DbContext.Set<EvalSession>()
             join ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>() on es.EvalSessionId equals ese.EvalSessionId
             where es.EvalSessionId == evalSessionId
             select ese).Load();

            return (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                    where es.EvalSessionId == evalSessionId
                    select es).Single();
        }

        public IList<string> CanDeleteEvalSessionProject(int evalSessionId, int projectId)
        {
            var errors = new List<string>();

            if (this.unitOfWork.DbContext.Set<EvalSessionSheet>().Where(p => p.EvalSessionId == evalSessionId && p.ProjectId == projectId).Any())
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanDeleteEvalSessionProject_ProjectHasEvalSheet);
            }

            if (this.unitOfWork.DbContext.Set<EvalSessionStandpoint>().Where(p => p.EvalSessionId == evalSessionId && p.ProjectId == projectId).Any())
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanDeleteEvalSessionProject_ProjectHasStandpoint);
            }

            return errors;
        }

        public IList<string> CanDeleteEvalSessionUser(int evalSessionId, int evalSessionUserId)
        {
            var errors = new List<string>();

            if (this.unitOfWork.DbContext.Set<EvalSessionSheet>().Where(p => p.EvalSessionId == evalSessionId && p.EvalSessionUserId == evalSessionUserId).Any())
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanDeleteEvalSessionUser_UserHasEvalSheet);
            }

            if (this.unitOfWork.DbContext.Set<EvalSessionStandpoint>().Where(p => p.EvalSessionId == evalSessionId && p.EvalSessionUserId == evalSessionUserId).Any())
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanDeleteEvalSessionUser_UserHasStandpoint);
            }

            return errors;
        }

        public IList<EvalSessionDistributionUserDO> GetEvalSessionAsessors(int evalSessionId)
        {
            return (from esu in this.unitOfWork.DbContext.Set<EvalSessionUser>()
                    join u in this.unitOfWork.DbContext.Set<User>() on esu.UserId equals u.UserId
                    where esu.EvalSessionId == evalSessionId && esu.Type == EvalSessionUserType.Assessor
                    orderby u.Fullname
                    select new EvalSessionDistributionUserDO
                    {
                        EvalSessionId = esu.EvalSessionId,
                        EvalSessionUserId = esu.EvalSessionUserId,
                        Username = u.Username,
                        Fullname = u.Fullname,
                    })
                .ToList();
        }

        public IList<EvalSessionDistributionUserDO> GetEvalSessionNotDeactivatedAssessors(int evalSessionId)
        {
            return (from esu in this.unitOfWork.DbContext.Set<EvalSessionUser>()
                    join u in this.unitOfWork.DbContext.Set<User>() on esu.UserId equals u.UserId
                    where esu.EvalSessionId == evalSessionId && esu.Type == EvalSessionUserType.Assessor && esu.Status != EvalSessionUserStatus.Deactivated
                    orderby u.Fullname
                    select new EvalSessionDistributionUserDO
                    {
                        EvalSessionId = esu.EvalSessionId,
                        EvalSessionUserId = esu.EvalSessionUserId,
                        Username = u.Username,
                        Fullname = u.Fullname,
                    })
                .ToList();
        }

        public IList<EvalSessionDistributionProjectsVO> GetNewEvalSessionDistributionProjects(int evalSessionId)
        {
            return (from esp in this.unitOfWork.DbContext.Set<EvalSessionProject>()
                    join p in this.unitOfWork.DbContext.Set<Project>() on esp.ProjectId equals p.ProjectId
                    join pt in this.unitOfWork.DbContext.Set<ProjectType>() on p.ProjectTypeId equals pt.ProjectTypeId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId
                    where esp.EvalSessionId == evalSessionId && esp.IsDeleted == false && p.RegistrationStatus != ProjectRegistrationStatus.Withdrawn
                    orderby p.RegNumber descending
                    select new EvalSessionDistributionProjectsVO
                    {
                        EvalSessionId = esp.EvalSessionId,
                        ProjectId = esp.ProjectId,
                        ProcedureNameBg = proc.Name,
                        ProcedureNameEn = proc.NameAlt,
                        ProjectRegNumber = p.RegNumber,
                        ProjectNameBg = p.Name,
                        ProjectNameEn = p.NameAlt,
                        CompanyNameBg = p.CompanyName,
                        CompanyNameEn = p.CompanyNameAlt,
                        ProjectRegDate = p.RegDate,
                        ProjectRegistrationStatus = p.RegistrationStatus,
                    })
                .ToList();
        }

        public IList<EvalSessionDistributionProjectsVO> GetEvalSessionDistributionProjects(int evalSessionId, int distributionId)
        {
            return (from esp in this.unitOfWork.DbContext.Set<EvalSessionProject>()
                    join esdp in this.unitOfWork.DbContext.Set<EvalSessionDistributionProject>() on new { esp.EvalSessionId, esp.ProjectId } equals new { esdp.EvalSessionId, esdp.ProjectId }
                    join p in this.unitOfWork.DbContext.Set<Project>() on esp.ProjectId equals p.ProjectId
                    join pt in this.unitOfWork.DbContext.Set<ProjectType>() on p.ProjectTypeId equals pt.ProjectTypeId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId
                    where esdp.EvalSessionId == evalSessionId && esdp.EvalSessionDistributionId == distributionId
                    orderby p.RegNumber
                    select new EvalSessionDistributionProjectsVO
                    {
                        EvalSessionId = esp.EvalSessionId,
                        ProjectId = esp.ProjectId,
                        ProcedureNameBg = proc.Name,
                        ProcedureNameEn = proc.NameAlt,
                        ProjectRegNumber = p.RegNumber,
                        ProjectNameBg = p.Name,
                        ProjectNameEn = p.NameAlt,
                        CompanyNameBg = p.CompanyName,
                        CompanyNameEn = p.CompanyNameAlt,
                        ProjectRegDate = p.RegDate,
                        ProjectRegistrationStatus = p.RegistrationStatus,
                        IsDeleted = esdp.IsDeleted,
                        IsDeletedNote = esdp.IsDeletedNote,
                    })
                .ToList();
        }

        public IList<EvalSessionProjectsVO> GetEvaluativeProjects(int evalSessionId, ProcedureEvalTableType evalTableType)
        {
            var projectIdsWithActiveEvaluations =
                    (from esp in this.unitOfWork.DbContext.Set<EvalSessionProject>()
                     join ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>() on new { esp.EvalSessionId, esp.ProjectId } equals new { ese.EvalSessionId, ese.ProjectId }
                     where esp.EvalSessionId == evalSessionId && ese.EvalTableType == evalTableType && ese.IsDeleted == false
                     select esp.ProjectId)
                .ToList();

            var results = (from esp in this.unitOfWork.DbContext.Set<EvalSessionProject>()
                           join p in this.unitOfWork.DbContext.Set<Project>() on esp.ProjectId equals p.ProjectId
                           join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId

                           join ess in this.unitOfWork.DbContext.Set<EvalSessionSheet>() on new { esp.EvalSessionId, esp.ProjectId } equals new { ess.EvalSessionId, ess.ProjectId }
                           join ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>() on new { esp.EvalSessionId, esp.ProjectId, ess.EvalTableType } equals new { ese.EvalSessionId, ese.ProjectId, ese.EvalTableType } into g1
                           from ese in g1.DefaultIfEmpty()

                           join pkc in this.unitOfWork.DbContext.Set<KidCode>() on p.KidCodeId equals pkc.KidCodeId into g2
                           from pkc in g2.DefaultIfEmpty()

                           // projects must have admin admiss evaluation when tech finance evaluation is created
                           join eseAA in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(x => x.EvalTableType == ProcedureEvalTableType.AdminAdmiss && x.IsDeleted == false) on new { esp.EvalSessionId, esp.ProjectId } equals new { eseAA.EvalSessionId, eseAA.ProjectId } into g4
                           from eseAA in g4.DefaultIfEmpty()

                           where esp.EvalSessionId == evalSessionId && esp.IsDeleted == false && p.RegistrationStatus != ProjectRegistrationStatus.Withdrawn &&
                                 ess.EvalTableType == evalTableType && (ese == null || !projectIdsWithActiveEvaluations.Contains(esp.ProjectId)) &&
                                 ((evalTableType == ProcedureEvalTableType.TechFinance && eseAA != null) || evalTableType != ProcedureEvalTableType.TechFinance)
                           select new { esp, p, pkc, proc, ess })
                        .ToList();

            var projects = results
                .GroupBy(g => g.esp.ProjectId)
                .Where(p => (p.Count(s => s.ess.Status == EvalSessionSheetStatus.Ended) > 0) &&
                            (p.Count(t => t.ess.Status == EvalSessionSheetStatus.Draft) == 0))
                .Select(t => new EvalSessionProjectsVO
                {
                    EvalSessionId = evalSessionId,
                    ProjectId = t.Key,
                    ProcedureName = t.First().proc.Name,
                    ProjectRegNumber = t.First().p.RegNumber,
                    ProjectNameBg = t.First().p.Name,
                    ProjectNameEn = t.First().p.NameAlt,
                    ProjectKidCode = t.First().pkc == null ? null : t.First().pkc.Code,
                    CompanyBg = string.Format("{0} ({1}: {2})", t.First().p.CompanyName, t.First().p.CompanyUinType.GetEnumDescription(), t.First().p.CompanyUin),
                    CompanyEn = string.Format("{0} ({1}: {2})", t.First().p.CompanyNameAlt, t.First().p.CompanyUinType.GetEnumDescription(), t.First().p.CompanyUin),
                    ProjectRegDate = t.First().p.RegDate,
                    ProjectRegistrationStatus = t.First().p.RegistrationStatus,
                })
                .OrderBy(t => t.ProjectRegNumber)
                .ToList();
            return projects;
        }

        public IList<EvalSessionEvaluationVO> GetEvalSessionEvaluations(int evalSessionId, int projectId)
        {
            return (from ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>()
                    where ese.EvalSessionId == evalSessionId && ese.ProjectId == projectId
                    orderby ese.CreateDate descending
                    select new EvalSessionEvaluationVO
                    {
                        EvalSessionId = ese.EvalSessionId,
                        EvalSessionEvaluationId = ese.EvalSessionEvaluationId,
                        ProjectId = ese.ProjectId,
                        EvalTableType = ese.EvalTableType,
                        EvalTableTypeName =
                            ese.EvalTableType == ProcedureEvalTableType.AdminAdmiss ? ProcedureEvalTableTypeShort.AdminAdmiss :
                            ese.EvalTableType == ProcedureEvalTableType.TechFinance ? ProcedureEvalTableTypeShort.TechFinance :
                            ese.EvalTableType == ProcedureEvalTableType.Complex ? ProcedureEvalTableTypeShort.Complex :
                            default(ProcedureEvalTableTypeShort),
                        CalculationType = ese.CalculationType,
                        EvalType = ese.EvalType,
                        EvalIsPassed = ese.EvalIsPassed,
                        EvalPoints = ese.EvalPoints,
                        EvalNote = ese.EvalNote,
                        IsDeleted = ese.IsDeleted,
                        IsDeletedNote = ese.IsDeletedNote,
                        CreateDate = ese.CreateDate,
                    })
                .ToList();
        }

        public IList<EvalSessionEvaluationVO> GetProjectEvalSessionEvaluations(int projectId)
        {
            return (from ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>()
                    join es in this.unitOfWork.DbContext.Set<EvalSession>() on ese.EvalSessionId equals es.EvalSessionId
                    where ese.ProjectId == projectId
                    orderby ese.CreateDate descending
                    select new EvalSessionEvaluationVO
                    {
                        EvalSessionId = ese.EvalSessionId,
                        EvalSessionEvaluationId = ese.EvalSessionEvaluationId,
                        ProjectId = ese.ProjectId,
                        EvalTableType = ese.EvalTableType,
                        EvalTableTypeName =
                            ese.EvalTableType == ProcedureEvalTableType.AdminAdmiss ? ProcedureEvalTableTypeShort.AdminAdmiss :
                            ese.EvalTableType == ProcedureEvalTableType.TechFinance ? ProcedureEvalTableTypeShort.TechFinance :
                            ese.EvalTableType == ProcedureEvalTableType.Complex ? ProcedureEvalTableTypeShort.Complex :
                            default(ProcedureEvalTableTypeShort),
                        CalculationType = ese.CalculationType,
                        EvalType = ese.EvalType,
                        EvalIsPassed = ese.EvalIsPassed,
                        EvalPoints = ese.EvalPoints,
                        EvalNote = ese.EvalNote,
                        IsDeleted = ese.IsDeleted,
                        IsDeletedNote = ese.IsDeletedNote,
                        CreateDate = ese.CreateDate,
                        EvalSessionNum = es.SessionNum,
                    })
                .ToList();
        }

        public EvalSessionEvaluation GetProjectEvalSessionEvaluation(int projectId, int evaluationId)
        {
            return this.unitOfWork.DbContext.Set<EvalSessionEvaluation>()
                .Where(t => t.ProjectId == projectId && t.EvalSessionEvaluationId == evaluationId)
                .Single();
        }

        public IList<EvalSessionSheetsVO> GetEvalSessionEvaluationSheets(int evalSessionId, int evaluationId, int projectId)
        {
            return (from eses in this.unitOfWork.DbContext.Set<EvalSessionEvaluationSheet>()
                    join ess in this.unitOfWork.DbContext.Set<EvalSessionSheet>() on eses.EvalSessionSheetId equals ess.EvalSessionSheetId
                    join essXml in this.unitOfWork.DbContext.Set<EvalSessionSheetXml>() on ess.EvalSessionSheetId equals essXml.EvalSessionSheetId
                    join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>() on ess.EvalSessionUserId equals esu.EvalSessionUserId
                    join p in this.unitOfWork.DbContext.Set<Project>() on ess.ProjectId equals p.ProjectId
                    join u in this.unitOfWork.DbContext.Set<User>() on esu.UserId equals u.UserId
                    where eses.EvalSessionId == evalSessionId && eses.EvalSessionEvaluationId == evaluationId && ess.ProjectId == projectId
                    select new EvalSessionSheetsVO
                    {
                        EvalSessionId = ess.EvalSessionId,
                        EvalSessionSheetId = ess.EvalSessionSheetId,
                        Assessor = u.Fullname + "(" + u.Username + ")",
                        ProjectRegNumber = p.RegNumber,
                        ProjectNameBg = p.Name,
                        ProjectNameEn = p.NameAlt,
                        EvalTableType = ess.EvalTableType,
                        EvalTableTypeName =
                            ess.EvalTableType == ProcedureEvalTableType.AdminAdmiss ? ProcedureEvalTableTypeShort.AdminAdmiss :
                            ess.EvalTableType == ProcedureEvalTableType.TechFinance ? ProcedureEvalTableTypeShort.TechFinance :
                            ess.EvalTableType == ProcedureEvalTableType.Complex ? ProcedureEvalTableTypeShort.Complex :
                            default(ProcedureEvalTableTypeShort),
                        StatusName = ess.Status,
                        Status = ess.Status,
                        DistributionType = ess.DistributionType,
                        XmlGid = essXml.Gid,
                        EvalType = essXml.EvalType,
                        EvalIsPassed = essXml.EvalIsPassed,
                        EvalPoints = essXml.EvalPoints,
                        EvalNote = essXml.EvalNote,
                    })
                    .ToList();
        }

        public IList<EvalSessionDocumentsVO> GetEvalSessionDocuments(int evalSessionId)
        {
            return (from esd in this.unitOfWork.DbContext.Set<EvalSessionDocument>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on esd.BlobKey equals b.Key
                    where esd.EvalSessionId == evalSessionId
                    orderby esd.EvalSessionDocumentId descending
                    select new EvalSessionDocumentsVO
                    {
                        EvalSessionId = esd.EvalSessionId,
                        EvalSessionDocumentId = esd.EvalSessionDocumentId,
                        Name = esd.Name,
                        File = new FileVO
                        {
                            Key = b.Key,
                            Name = b.FileName,
                        },
                        Description = esd.Description,
                        IsDeleted = esd.IsDeleted,
                        IsDeletedNote = esd.IsDeletedNote,
                    })
                .ToList();
        }

        public IList<string> CanCancelEvalSessionProject(int evalSessionId, int projectId)
        {
            var errors = new List<string>();

            bool hasAssociatedSheets = this.unitOfWork.DbContext.Set<EvalSessionSheet>().Where(p => p.EvalSessionId == evalSessionId && p.ProjectId == projectId && p.Status != EvalSessionSheetStatus.Canceled).Any();

            bool hasAssociatedEvaluations = this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalSessionId == evalSessionId && p.ProjectId == projectId && p.IsDeleted == false).Any();

            if (hasAssociatedSheets || hasAssociatedEvaluations)
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanCancelEvalSessionProject_ProjectHasAssociatedSheetsAndEvaluations);
            }

            return errors;
        }

        public IList<string> CanRestoreEvalSessionProject(int projectId)
        {
            var errors = new List<string>();

            bool isProjectIncluded = (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                                      join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on es.EvalSessionId equals esp.EvalSessionId
                                      where esp.ProjectId == projectId && es.EvalSessionStatus != EvalSessionStatus.Canceled && esp.IsDeleted == false
                                      select esp.ProjectId)
                                .Any();

            if (isProjectIncluded)
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanRestoreEvalSessionProject_ProjectIsIncludedInAnotherSession);
            }

            return errors;
        }

        public IList<string> CanCancelEvalSessionSheet(int evalSessionId, int sheetId)
        {
            var errors = new List<string>();

            if ((from ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>()
                 join eses in this.unitOfWork.DbContext.Set<EvalSessionEvaluationSheet>() on ese.EvalSessionEvaluationId equals eses.EvalSessionEvaluationId
                 where ese.EvalSessionId == evalSessionId && ese.IsDeleted == false && eses.EvalSessionSheetId == sheetId
                 select ese.EvalSessionEvaluationId)
                   .Any())
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanCancelEvalSessionSheet_IncludedInNonDeletedEval);
            }

            return errors;
        }

        public IList<string> CanContinueEvalSessionSheet(int evalSessionId, int sheetId)
        {
            var errors = new List<string>();

            if ((from ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>()
                 join eses in this.unitOfWork.DbContext.Set<EvalSessionEvaluationSheet>() on ese.EvalSessionEvaluationId equals eses.EvalSessionEvaluationId
                 where ese.EvalSessionId == evalSessionId && ese.IsDeleted == false && eses.EvalSessionSheetId == sheetId
                 select ese.EvalSessionEvaluationId)
                   .Any())
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanContinueEvalSessionSheet_IncludedInNonDeletedEval);
            }

            return errors;
        }

        public IList<string> CanRefuseEvalSessionDistribution(int evalSessionId, int distributionId)
        {
            var errors = new List<string>();

            var sheets = this.unitOfWork.DbContext.Set<EvalSessionSheet>().Where(t => t.EvalSessionDistributionId == distributionId).ToList();
            var sheetIds = sheets.Select(t => t.EvalSessionSheetId).ToList();

            if ((from ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>()
                 join eses in this.unitOfWork.DbContext.Set<EvalSessionEvaluationSheet>() on ese.EvalSessionEvaluationId equals eses.EvalSessionEvaluationId
                 where ese.EvalSessionId == evalSessionId && ese.IsDeleted == false && sheetIds.Contains(eses.EvalSessionSheetId)
                 select ese.EvalSessionEvaluationId)
                .Any())
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanRefuseEvalSessionDistribution_CannotCancelAllSheets);
            }

            if (sheets.Where(t => t.ContinuedEvalSessionSheetId.HasValue).Any())
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanRefuseEvalSessionDistribution_HasContinuedEvalSheet);
            }

            return errors;
        }

        public IList<EvalSession> GetNonCanceledEvalSessionsByProcedure(int procedureId)
        {
            return this.Set()
                .Where(es => es.EvalSessionStatus != EvalSessionStatus.Canceled && es.ProcedureId == procedureId)
                .ToList();
        }

        public IList<EvalSessionProjectStandingVO> GetEvalSessionProjectStandings(int evalSessionId, int projectId)
        {
            return (from esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>()
                    where esps.EvalSessionId == evalSessionId && esps.ProjectId == projectId
                    orderby esps.CreateDate descending
                    select new
                    {
                        esps.EvalSessionId,
                        esps.EvalSessionProjectStandingId,
                        esps.ProjectId,
                        esps.IsPreliminary,
                        esps.OrderNum,
                        esps.EvalSessionStandingId,
                        esps.Status,
                        esps.GrandAmount,
                        esps.IsDeleted,
                        esps.IsDeletedNote,
                        esps.Notes,
                        esps.CreateDate,
                    }).ToList()
                .Select(o => new EvalSessionProjectStandingVO
                {
                    EvalSessionId = o.EvalSessionId,
                    EvalSessionProjectStandingId = o.EvalSessionProjectStandingId,
                    ProjectId = o.ProjectId,
                    IsPreliminary = o.IsPreliminary,
                    OrderNum = o.OrderNum,
                    Type = o.EvalSessionStandingId.HasValue ? EvalSessionProjectStandingType.Automatic : EvalSessionProjectStandingType.Manual,
                    Status = o.Status,
                    GrandAmount = o.GrandAmount,
                    IsDeleted = o.IsDeleted,
                    IsDeletedNote = o.IsDeletedNote,
                    Notes = o.Notes,
                    CreateDate = o.CreateDate,
                }).ToList();
        }

        public IList<EvalSessionProjectStandingVO> GetProjectEvalSessionProjectStandings(int projectId)
        {
            return (from esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>()
                    join es in this.unitOfWork.DbContext.Set<EvalSession>() on esps.EvalSessionId equals es.EvalSessionId
                    where esps.ProjectId == projectId
                    orderby esps.CreateDate descending
                    select new
                    {
                        esps.EvalSessionId,
                        esps.EvalSessionProjectStandingId,
                        esps.ProjectId,
                        esps.IsPreliminary,
                        esps.OrderNum,
                        esps.EvalSessionStandingId,
                        esps.Status,
                        esps.GrandAmount,
                        esps.IsDeleted,
                        esps.IsDeletedNote,
                        esps.Notes,
                        esps.CreateDate,
                        es.SessionNum,
                    }).ToList()
                .Select(o => new EvalSessionProjectStandingVO
                {
                    EvalSessionId = o.EvalSessionId,
                    EvalSessionProjectStandingId = o.EvalSessionProjectStandingId,
                    ProjectId = o.ProjectId,
                    IsPreliminary = o.IsPreliminary,
                    OrderNum = o.OrderNum,
                    Type = o.EvalSessionStandingId.HasValue ? EvalSessionProjectStandingType.Automatic : EvalSessionProjectStandingType.Manual,
                    Status = o.Status,
                    GrandAmount = o.GrandAmount,
                    IsDeleted = o.IsDeleted,
                    IsDeletedNote = o.IsDeletedNote,
                    Notes = o.Notes,
                    CreateDate = o.CreateDate,
                    EvalSessionNum = o.SessionNum,
                }).ToList();
        }

        public EvalSessionProjectStanding GetProjectEvalSessionProjectStanding(int projectId, int projectStandingId)
        {
            return this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>()
                .Where(t => t.ProjectId == projectId && t.EvalSessionProjectStandingId == projectStandingId)
                .Single();
        }

        public bool IsOrderNumUnique(int evalSessionId, bool isPreliminary, int orderNum)
        {
            return !this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(p => p.EvalSessionId == evalSessionId && p.IsDeleted == false && p.IsPreliminary == isPreliminary && p.OrderNum == orderNum).Any();
        }

        public IList<string> CanDeleteEvalSessionEvaluation(int evaluationId, int projectId)
        {
            var errors = new List<string>();

            var predicate = PredicateBuilder.True<EvalSessionEvaluation>()
                .AndEquals(p => p.EvalSessionEvaluationId, evaluationId)
                .AndEquals(p => p.ProjectId, projectId)
                .AndEquals(p => p.IsDeleted, false);

            var evalSessionEvaluation = (from ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(predicate)
                                         join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(e => e.IsDeleted == false) on new { ese.EvalSessionId, ese.ProjectId } equals new { esps.EvalSessionId, esps.ProjectId }
                                         select new
                                         {
                                             ese.EvalTableType,
                                             esps.IsPreliminary,
                                         })
                                         .ToList();

            if (evalSessionEvaluation.Any())
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanDeleteEvalSessionEvaluation_HasActiveStanding);
            }

            return errors;
        }

        public IList<EvalSessionEvaluationVO> GetEvalSessionProjectStandingEvaluations(int evalSessionId, int projectStandingId, int projectId)
        {
            return (from espse in this.unitOfWork.DbContext.Set<EvalSessionProjectStandingEvaluation>()
                    join ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>() on espse.EvalSessionEvaluationId equals ese.EvalSessionEvaluationId
                    where espse.EvalSessionId == evalSessionId && espse.EvalSessionProjectStandingId == projectStandingId && ese.ProjectId == projectId
                    orderby ese.CreateDate
                    select new EvalSessionEvaluationVO
                    {
                        EvalSessionId = ese.EvalSessionId,
                        EvalSessionEvaluationId = ese.EvalSessionEvaluationId,
                        ProjectId = ese.ProjectId,
                        EvalTableType = ese.EvalTableType,
                        EvalTableTypeName =
                            ese.EvalTableType == ProcedureEvalTableType.AdminAdmiss ? ProcedureEvalTableTypeShort.AdminAdmiss :
                            ese.EvalTableType == ProcedureEvalTableType.TechFinance ? ProcedureEvalTableTypeShort.TechFinance :
                            ese.EvalTableType == ProcedureEvalTableType.Complex ? ProcedureEvalTableTypeShort.Complex :
                            default(ProcedureEvalTableTypeShort),
                        CalculationType = ese.CalculationType,
                        EvalType = ese.EvalType,
                        EvalIsPassed = ese.EvalIsPassed,
                        EvalPoints = ese.EvalPoints,
                        EvalNote = ese.EvalNote,
                        IsDeleted = ese.IsDeleted,
                        IsDeletedNote = ese.IsDeletedNote,
                        CreateDate = ese.CreateDate,
                    })
                .ToList();
        }

        public IList<EvalSessionStandingsVO> GetEvalSessionStandings(int evalSessionId)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionStanding>()
                    where ess.EvalSessionId == evalSessionId
                    orderby ess.EvalSessionStandingId descending
                    select new EvalSessionStandingsVO
                    {
                        EvalSessionId = ess.EvalSessionId,
                        EvalSessionStandingId = ess.EvalSessionStandingId,
                        Code = ess.Code,
                        IsPreliminary = ess.IsPreliminary,
                        Status = ess.Status,
                        StatusName = ess.Status,
                        StatusNote = ess.StatusNote,
                        StatusDate = ess.StatusDate,
                    }).ToList();
        }

        public IList<EvalSessionStandingProjectDO> GetEvalSessionStandingProjects(int evalSessionId, int standingId)
        {
            return (from essp in this.unitOfWork.DbContext.Set<EvalSessionStandingProject>()
                    join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(t => t.EvalSessionStandingId == standingId) on new { essp.EvalSessionId, essp.ProjectId } equals new { esps.EvalSessionId, esps.ProjectId }
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on essp.ProjectId equals esp.ProjectId
                    join p in this.unitOfWork.DbContext.Set<Project>() on esp.ProjectId equals p.ProjectId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId

                    join ese1 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.AdminAdmiss && p.IsDeleted == false)
                         on new { esp.EvalSessionId, esp.ProjectId } equals new { ese1.EvalSessionId, ese1.ProjectId } into g1
                    from ese1 in g1.DefaultIfEmpty()

                    join ese2 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.TechFinance && p.IsDeleted == false)
                         on new { esp.EvalSessionId, esp.ProjectId } equals new { ese2.EvalSessionId, ese2.ProjectId } into g2
                    from ese2 in g2.DefaultIfEmpty()

                    join ese3 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.Complex && p.IsDeleted == false)
                         on new { esp.EvalSessionId, esp.ProjectId } equals new { ese3.EvalSessionId, ese3.ProjectId } into g3
                    from ese3 in g3.DefaultIfEmpty()

                    where esp.EvalSessionId == evalSessionId && essp.EvalSessionStandingId == standingId
                    select new EvalSessionStandingProjectDO
                    {
                        EvalSessionId = esp.EvalSessionId,
                        ProjectId = esp.ProjectId,
                        ProcedureName = proc.Name,
                        ProjectRegNumber = p.RegNumber,
                        ProjectName = p.Name,
                        CompanyName = p.CompanyName,
                        ProjectRegDate = p.RegDate,
                        ProjectRegistrationStatus = p.RegistrationStatus,
                        IsPassedASD = ese1 != null ? ese1.EvalIsPassed : (bool?)null,
                        PointsASD = ese1 != null ? ese1.EvalPoints : null,
                        IsPassedTFO = ese2 != null ? ese2.EvalIsPassed : (bool?)null,
                        PointsTFO = ese2 != null ? ese2.EvalPoints : null,
                        IsPassedComplex = ese3 != null ? ese3.EvalIsPassed : (bool?)null,
                        PointsComplex = ese3 != null ? ese3.EvalPoints : null,
                        IsPassedPreliminary = (bool?)null,
                        PointsPreliminary = null,
                        IsProjectDeleted = esp.IsDeleted,
                        OrderNum = esps.OrderNum,
                        ManualOrderNum = esps.ManualOrderNum,
                        Status = esps.Status,
                        StatusName = esps.Status,
                        ManualStatus = esps.ManualStatus,
                        ManualStatusName = esps.ManualStatus,
                        GrandAmount = esps.GrandAmount,
                        IsStandingDeleted = esps.IsDeleted,
                    })
                .ToList()
                .OrderByDescending(t => t, new EvalSessionStandingProjectsComparer(true))
                .ToList();
        }

        public IList<EvalSessionStandingProjectDO> GetEvalSessionRearrangedStandingProjects(int evalSessionId, int standingId)
        {
            return (from essp in this.unitOfWork.DbContext.Set<EvalSessionStandingProject>()
                    join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(t => t.EvalSessionStandingId == standingId) on new { essp.EvalSessionId, essp.ProjectId } equals new { esps.EvalSessionId, esps.ProjectId }
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on essp.ProjectId equals esp.ProjectId
                    join p in this.unitOfWork.DbContext.Set<Project>() on esp.ProjectId equals p.ProjectId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId

                    join ese1 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.AdminAdmiss && p.IsDeleted == false)
                         on new { esp.EvalSessionId, esp.ProjectId } equals new { ese1.EvalSessionId, ese1.ProjectId } into g1
                    from ese1 in g1.DefaultIfEmpty()

                    join ese2 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.TechFinance && p.IsDeleted == false)
                         on new { esp.EvalSessionId, esp.ProjectId } equals new { ese2.EvalSessionId, ese2.ProjectId } into g2
                    from ese2 in g2.DefaultIfEmpty()

                    join ese3 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.Complex && p.IsDeleted == false)
                         on new { esp.EvalSessionId, esp.ProjectId } equals new { ese3.EvalSessionId, ese3.ProjectId } into g3
                    from ese3 in g3.DefaultIfEmpty()

                    where esp.EvalSessionId == evalSessionId && essp.EvalSessionStandingId == standingId
                    select new EvalSessionStandingProjectDO
                    {
                        EvalSessionId = esp.EvalSessionId,
                        ProjectId = esp.ProjectId,
                        ProcedureName = proc.Name,
                        ProjectRegNumber = p.RegNumber,
                        ProjectName = p.Name,
                        CompanyName = p.CompanyName,
                        ProjectRegDate = p.RegDate,
                        ProjectRegistrationStatus = p.RegistrationStatus,
                        IsPassedASD = ese1 != null ? ese1.EvalIsPassed : (bool?)null,
                        PointsASD = ese1 != null ? ese1.EvalPoints : null,
                        IsPassedTFO = ese2 != null ? ese2.EvalIsPassed : (bool?)null,
                        PointsTFO = ese2 != null ? ese2.EvalPoints : null,
                        IsPassedComplex = ese3 != null ? ese3.EvalIsPassed : (bool?)null,
                        PointsComplex = ese3 != null ? ese3.EvalPoints : null,
                        IsPassedPreliminary = (bool?)null,
                        PointsPreliminary = null,
                        IsProjectDeleted = esp.IsDeleted,
                        OrderNum = esps.OrderNum,
                        ManualOrderNum = esps.ManualOrderNum,
                        Status = esps.Status,
                        StatusName = esps.Status,
                        ManualStatus = esps.ManualStatus,
                        ManualStatusName = esps.ManualStatus,
                        GrandAmount = esps.GrandAmount,
                        IsStandingDeleted = esps.IsDeleted,
                    })
                .ToList()
                .OrderByDescending(t => t, new EvalSessionStandingProjectsComparer(false))
                .ToList();
        }

        public IList<EvalSessionStandingProjectDO> GetNewEvalSessionStandingProjects(int evalSessionId)
        {
            var predicate = PredicateBuilder.True<EvalSessionProject>();

            predicate = predicate
                .AndEquals(p => p.EvalSessionId, evalSessionId)
                .AndEquals(p => p.IsDeleted, false);

            return (from esp in this.unitOfWork.DbContext.Set<EvalSessionProject>().Where(predicate)
                    join p in this.unitOfWork.DbContext.Set<Project>().Where(p => p.RegistrationStatus != ProjectRegistrationStatus.Withdrawn) on esp.ProjectId equals p.ProjectId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId

                    join ese1 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.AdminAdmiss && p.IsDeleted == false) on new { esp.EvalSessionId, esp.ProjectId } equals new { ese1.EvalSessionId, ese1.ProjectId } into g1
                    from ese1 in g1.DefaultIfEmpty()

                    join ese2 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.TechFinance && p.IsDeleted == false) on new { esp.EvalSessionId, esp.ProjectId } equals new { ese2.EvalSessionId, ese2.ProjectId } into g2
                    from ese2 in g2.DefaultIfEmpty()

                    join ese3 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.Complex && p.IsDeleted == false) on new { esp.EvalSessionId, esp.ProjectId } equals new { ese3.EvalSessionId, ese3.ProjectId } into g3
                    from ese3 in g3.DefaultIfEmpty()

                    join pps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(s => s.IsPreliminary && !s.IsDeleted) on new { esp.EvalSessionId, esp.ProjectId } equals new { pps.EvalSessionId, pps.ProjectId } into gpps
                    from pps in gpps.DefaultIfEmpty()

                    orderby p.RegNumber
                    select new EvalSessionStandingProjectDO
                    {
                        EvalSessionId = esp.EvalSessionId,
                        ProjectId = esp.ProjectId,
                        ProcedureName = proc.Name,
                        ProjectRegNumber = p.RegNumber,
                        ProjectName = p.Name,
                        CompanyName = p.CompanyName,
                        ProjectRegDate = p.RegDate,
                        ProjectRegistrationStatus = p.RegistrationStatus,
                        IsPassedASD = ese1 != null ? ese1.EvalIsPassed : (bool?)null,
                        PointsASD = ese1 != null ? ese1.EvalPoints : null,
                        IsPassedTFO = ese2 != null ? ese2.EvalIsPassed : (bool?)null,
                        PointsTFO = ese2 != null ? ese2.EvalPoints : null,
                        IsPassedComplex = ese3 != null ? ese3.EvalIsPassed : (bool?)null,
                        PointsComplex = ese3 != null ? ese3.EvalPoints : null,
                        IsPassedPreliminary = (bool?)null,
                        PointsPreliminary = null,
                        IsApprovedInPreliminaryStanding = pps != null ? pps.Status == EvalSessionProjectStandingStatus.Approved : (bool?)null,
                        IsProjectDeleted = esp.IsDeleted,
                    }).ToList();
        }

        public int[] GetProcedurePreviousEvalSessionStandingProjects(int procedureId, int evalSessionId)
        {
            var predicate = PredicateBuilder.True<EvalSessionProjectStanding>();

            predicate = predicate
                .AndEquals(p => p.IsDeleted, false)
                .AndEquals(p => p.Status, EvalSessionProjectStandingStatus.Approved)
                .AndEquals(p => p.IsPreliminary, false)
                .And(p => p.EvalSessionId != evalSessionId);

            return (from esp in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(predicate)
                    join p in this.unitOfWork.DbContext.Set<Project>() on esp.ProjectId equals p.ProjectId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>().Where(x => x.ProcedureId == procedureId) on p.ProcedureId equals proc.ProcedureId
                    select p.ProjectId)
                    .Distinct()
                    .ToArray();
        }

        public int GetEvalSessionStandpointProjectId(int evalSessionStandpointId)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionStandpoint>()
                    where ess.EvalSessionStandpointId == evalSessionStandpointId
                    select ess.ProjectId).SingleOrDefault();
        }

        public EvalSessionSheetData GetSheetData(int evalSessionSheetId)
        {
            return (from s in this.unitOfWork.DbContext.Set<EvalSessionSheet>()
                    join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>() on s.EvalSessionUserId equals esu.EvalSessionUserId
                    join p in this.unitOfWork.DbContext.Set<Project>() on s.ProjectId equals p.ProjectId
                    join u in this.unitOfWork.DbContext.Set<User>() on esu.UserId equals u.UserId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId
                    where s.EvalSessionSheetId == evalSessionSheetId
                    select new EvalSessionSheetData
                    {
                        ProjectId = s.ProjectId,
                        AssessorName = u.Fullname,
                        ProjectName = p.Name,
                        ProjectNameAlt = p.NameAlt,
                        ProjectRegNumber = p.RegNumber,
                        ProcedureName = proc.Name,
                        ProcedureNameAlt = proc.NameAlt,
                        CompanyName = p.CompanyName,
                        CompanyNameAlt = p.CompanyNameAlt,
                        EvalTableType = s.EvalTableType,
                        Status = s.Status,
                    }).Single();
        }

        public EvalSessionStandpointVO GetStandpointData(int evalSessionStandpointId)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionStandpoint>()
                    join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>() on ess.EvalSessionUserId equals esu.EvalSessionUserId
                    join essx in this.unitOfWork.DbContext.Set<EvalSessionStandpointXml>() on ess.EvalSessionStandpointId equals essx.EvalSessionStandpointId
                    join p in this.unitOfWork.DbContext.Set<Project>() on ess.ProjectId equals p.ProjectId
                    join u in this.unitOfWork.DbContext.Set<User>() on esu.UserId equals u.UserId
                    where ess.EvalSessionStandpointId == evalSessionStandpointId
                    select new EvalSessionStandpointVO
                    {
                        EvalSessionId = ess.EvalSessionId,
                        EvalSessionStandpointId = ess.EvalSessionStandpointId,
                        XmlGid = essx.Gid,
                        User = u.Fullname + "(" + u.Username + ")",
                        ProjectId = p.ProjectId,
                        ProjectRegNumber = p.RegNumber,
                        ProjectName = p.Name,
                        Note = ess.Note,
                        StatusName = ess.Status,
                        Status = ess.Status,
                    })
                .Single();
        }

        public IList<string> CanAddEvalSessionUser(int evalSessionId, int userId, EvalSessionUserType userType)
        {
            var errors = new List<string>();

            if (this.unitOfWork.DbContext.Set<EvalSessionUser>().Where(p => p.EvalSessionId == evalSessionId && p.UserId == userId && p.Type == userType).Any())
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanAddEvalSessionUser_DuplicatedUser);
            }

            return errors;
        }

        public IList<EvalSessionProjectsVO> GetProjectEvalSessionProjects(int projectId)
        {
            return (from esp in this.unitOfWork.DbContext.Set<EvalSessionProject>()
                    join es in this.unitOfWork.DbContext.Set<EvalSession>() on esp.EvalSessionId equals es.EvalSessionId
                    where esp.ProjectId == projectId
                    select new EvalSessionProjectsVO()
                    {
                        EvalSessionId = esp.EvalSessionId,
                        ProjectId = esp.ProjectId,
                        EvalSessionNum = es.SessionNum,
                        IsDeleted = esp.IsDeleted,
                        IsDeletedNote = esp.IsDeletedNote,
                    })
                .ToList();
        }

        public IList<string> CanChangeStatusToDraft(int evalSessionId)
        {
            var errors = new List<string>();

            var evalSessionProjects = (from esp in this.unitOfWork.DbContext.Set<EvalSessionProject>()
                                       where esp.EvalSessionId == evalSessionId && esp.IsDeleted == false
                                       select esp.ProjectId)
                                       .ToList();

            var projectsInOtherEvalSessions = (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                                               join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>() on es.EvalSessionId equals esp.EvalSessionId
                                               join p in this.unitOfWork.DbContext.Set<Project>() on esp.ProjectId equals p.ProjectId
                                               where evalSessionProjects.Contains(esp.ProjectId) && es.EvalSessionId != evalSessionId && es.EvalSessionStatus != EvalSessionStatus.Canceled && esp.IsDeleted == false
                                               select new
                                               {
                                                   SessionNum = es.SessionNum,
                                                   ProjectNum = p.RegNumber,
                                               })
                                               .GroupBy(
                                                   item => item.ProjectNum,
                                                   (key, group) => new { Project = key, Sessions = group.ToList() })
                                               .ToList();

            foreach (var item in projectsInOtherEvalSessions)
            {
                if (item.Sessions.Count > 1)
                {
                    errors.Add($"\tОценителната сесия не може да бъде върната в чернова, защото проектно предложение {item.Project} участва в оценителни сесии:\r\n\t\t{string.Join(";\r\n\t\t", item.Sessions.Select(s => s.SessionNum).ToArray())}");
                }
                else
                {
                    errors.Add($"\tОценителната сесия не може да бъде върната в чернова, защото проектно предложение {item.Project} участва в оценителна сесия {item.Sessions.Select(s => s.SessionNum).Single()}");
                }
            }

            return errors;
        }

        public IList<string> GetEvalSessionProjectsWithContracts(int standingId)
        {
            return (from esp in this.unitOfWork.DbContext.Set<EvalSessionStandingProject>().Where(e => e.EvalSessionStandingId == standingId)
                    join p in this.unitOfWork.DbContext.Set<Project>() on esp.ProjectId equals p.ProjectId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on p.ProjectId equals c.ProjectId
                    select p.RegNumber)
                    .Distinct()
                    .ToList();
        }

        public EvalSessionResultsVO GetEvalSessionResults(int evalSessionId)
        {
            var tables = (from ess in this.unitOfWork.DbContext.Set<EvalSessionResult>()
                          where ess.EvalSessionId == evalSessionId
                          orderby ess.OrderNum descending
                          select new EvalSessionResultTablesVO
                          {
                              EvalSessionId = ess.EvalSessionId,
                              EvalSessionResultId = ess.EvalSessionResultId,
                              CreateDate = ess.CreateDate,
                              OrderNum = ess.OrderNum,
                              Status = ess.Status,
                              StatusNote = ess.StatusNote,
                              Type = ess.Type,
                          }).ToList();

            return new EvalSessionResultsVO
            {
                Tables = tables,
                CanCreateAdminAdmiss = !this.ProcedureHasEvalTable(evalSessionId, ProcedureEvalTableType.AdminAdmiss).Any(),
                CanCreatePreliminary = false,
            };
        }

        public IList<EvalSessionAdminAdmissProjectsVO> GetEvalSessionResultAdminAdmissProjects(int evalSessionId, int resultId)
        {
            return (from esp in this.unitOfWork.DbContext.Set<EvalSessionResultProject>()
                    join esr in this.unitOfWork.DbContext.Set<EvalSessionResult>() on esp.EvalSessionResultId equals esr.EvalSessionResultId
                    where esr.EvalSessionId == evalSessionId && esp.EvalSessionResultId == resultId
                    select new EvalSessionAdminAdmissProjectsVO
                    {
                        CompanyNameBg = esp.CompanyName,
                        CompanyNameEn = esp.CompanyNameAlt,
                        EvalSessionResultProjectId = esp.EvalSessionResultProjectId,
                        ProjectId = esp.ProjectId,
                        ProjectRegNumber = esp.ProjectRegNumber,
                        ProjectRegDate = esp.ProjectRegDate,
                        ProjectNameBg = esp.ProjectName,
                        ProjectNameEn = esp.ProjectNameAlt,
                        CompanyUin = esp.CompanyUin,
                        CompanyUinType = esp.CompanyUinType,

                        AdminAdmissResult = esp.EvaluationAdminAdmissResult.HasValue && esp.EvaluationAdminAdmissResult.Value ? EvalSessionEvaluationResult.Passed : EvalSessionEvaluationResult.NotPassed,
                        NonAdmissionReason = esp.NonAdmissionReason,
                    }).ToList();
        }

        public IList<EvalSessionPreliminaryProjectsVO> GetEvalSessionResultPreliminaryProjects(int evalSessionId, int resultId)
        {
            return (from esp in this.unitOfWork.DbContext.Set<EvalSessionResultProject>()
                    join esr in this.unitOfWork.DbContext.Set<EvalSessionResult>() on esp.EvalSessionResultId equals esr.EvalSessionResultId
                    where esr.EvalSessionId == evalSessionId && esp.EvalSessionResultId == resultId
                    select new EvalSessionPreliminaryProjectsVO
                    {
                        CompanyNameBg = esp.CompanyName,
                        CompanyNameEn = esp.CompanyNameAlt,
                        EvalSessionResultProjectId = esp.EvalSessionResultProjectId,
                        ProjectId = esp.ProjectId,
                        ProjectRegNumber = esp.ProjectRegNumber,
                        ProjectRegDate = esp.ProjectRegDate,
                        ProjectNameBg = esp.ProjectName,
                        ProjectNameEn = esp.ProjectNameAlt,
                        CompanyUin = esp.CompanyUin,
                        CompanyUinType = esp.CompanyUinType,

                        PreliminaryResult = esp.StandingPreliminaryResult.HasValue && esp.StandingPreliminaryResult.Value ? EvalSessionEvaluationResult.Passed : EvalSessionEvaluationResult.NotPassed,
                        Points = esp.StandingPreliminaryPoints,
                        OrderNum = esp.ProjectStandingNumber,
                        Status = esp.ProjectStandingStatus.Value,
                        GrantAmount = esp.GrantAmount,
                        SelfAmount = esp.SelfAmount,
                        Note = esp.Note,
                    }).ToList();
        }

        public IList<EvalSessionStandingProjectsVO> GetEvalSessionResultStandingProjects(int evalSessionId, int resultId)
        {
            return (from esp in this.unitOfWork.DbContext.Set<EvalSessionResultProject>()
                    join esr in this.unitOfWork.DbContext.Set<EvalSessionResult>() on esp.EvalSessionResultId equals esr.EvalSessionResultId
                    where esr.EvalSessionId == evalSessionId && esp.EvalSessionResultId == resultId
                    select new EvalSessionStandingProjectsVO
                    {
                        CompanyNameBg = esp.CompanyName,
                        CompanyNameEn = esp.CompanyNameAlt,
                        EvalSessionResultProjectId = esp.EvalSessionResultProjectId,
                        ProjectId = esp.ProjectId,
                        ProjectRegNumber = esp.ProjectRegNumber,
                        ProjectRegDate = esp.ProjectRegDate,
                        ProjectNameBg = esp.ProjectName,
                        ProjectNameEn = esp.ProjectNameAlt,
                        CompanyUin = esp.CompanyUin,
                        CompanyUinType = esp.CompanyUinType,

                        IsPassedPreliminary = esp.StandingPreliminaryResult,
                        PointsPreliminary = esp.StandingPreliminaryPoints,
                        IsPassedASD = esp.EvaluationAdminAdmissResult,
                        IsPassedTFO = esp.StandingTechFinanceResult,
                        PointsTFO = esp.StandingTechFinancePoints,
                        IsPassedComplex = esp.StandingComplexResult,
                        PointsComplex = esp.StandingComplexPoints,
                        OrderNum = esp.ProjectStandingNumber,
                        Status = esp.ProjectStandingStatus.Value,
                        SelfAmount = esp.SelfAmount,
                        GrantAmount = esp.GrantAmount,
                        CorrectedGrantAmount = esp.GrantAmountCorrected,
                        CorrectedSelfAmount = esp.SelfAmountCorrected,
                        Note = esp.Note,
                    }).ToList();
        }

        public IList<string> EvalSessionHasUnevaluatedProjects(int evalSessionId)
        {
            List<string> errors = new List<string>();

            var rejectedAtPreliminaryProjects = this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>()
                .Where(x => x.EvalSessionId == evalSessionId && x.Status == EvalSessionProjectStandingStatus.RejectedAtPreliminary)
                .Select(x => x.ProjectId);

            var unevaluatedProjects = (from p in this.unitOfWork.DbContext.Set<EvalSessionProject>().Where(x => x.EvalSessionId == evalSessionId && !x.IsDeleted)
                                       join pr in this.unitOfWork.DbContext.Set<Project>() on p.ProjectId equals pr.ProjectId
                                       join e in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(x => x.EvalTableType == ProcedureEvalTableType.AdminAdmiss && !x.IsDeleted) on p.ProjectId equals e.ProjectId into g1
                                       from e in g1.DefaultIfEmpty()
                                       where e == null && pr.RegistrationStatus != ProjectRegistrationStatus.Withdrawn && !rejectedAtPreliminaryProjects.Contains(p.ProjectId)
                                       select pr).ToList();

            unevaluatedProjects.ForEach(x => errors.Add(string.Format(DataTexts.EvalSessionsRepository_CanLoadAdminAdmissProject_ProjectIsNotEvaluated, x.RegNumber)));

            return errors;
        }

        public IList<string> ProcedureHasEvalTable(int evalSessionId, ProcedureEvalTableType type)
        {
            IList<string> errors = new List<string>();

            var hasEvalTable = from es in this.unitOfWork.DbContext.Set<EvalSession>().Where(x => x.EvalSessionId == evalSessionId)
                               join pet in this.unitOfWork.DbContext.Set<ProcedureEvalTable>().Where(x => x.IsActivated && x.Type == type && x.Status == ProcedureEvalTableStatus.Ended) on es.ProcedureId equals pet.ProcedureId
                               select pet;

            if (!hasEvalTable.Any())
            {
                errors.Add(string.Format(DataTexts.EvalSessionsRepository_CanLoadAdminAdmissProject_ProcedureHasNotEvalTableType, type.GetEnumDescription()));
            }

            return errors;
        }

        public IList<string> GetPublishedProjectEmails(int evalSessionId)
        {
            return (from esr in this.unitOfWork.DbContext.Set<EvalSessionResult>().Where(x => x.EvalSessionId == evalSessionId)
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionResultProject>() on esr.EvalSessionResultId equals esp.EvalSessionResultId
                    join rpx in this.unitOfWork.DbContext.Set<RegProjectXml>() on esp.ProjectId equals rpx.ProjectId
                    join r in this.unitOfWork.DbContext.Set<Registration>() on rpx.RegistrationId equals r.RegistrationId
                    select r.Email)
                    .Distinct()
                    .ToList();
        }

        public void AddEvaluatedProjects(int evalSessionId, EvalSessionResult evalSessionResult)
        {
            var evaluatedProjects = (from ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(x => x.EvalSessionId == evalSessionId && x.EvalTableType == ProcedureEvalTableType.AdminAdmiss && !x.IsDeleted)
                                     join p in this.unitOfWork.DbContext.Set<Project>() on ese.ProjectId equals p.ProjectId
                                     select new
                                     {
                                         project = p,
                                         result = ese.EvalIsPassed,
                                         reason = ese.EvalNote,
                                     }).ToList();

            evaluatedProjects.ForEach(x =>
                evalSessionResult.Projects.Add(new EvalSessionResultProject(x.project)
                {
                    EvaluationAdminAdmissResult = x.result,
                    NonAdmissionReason = x.reason,
                }));
        }

        public void AddPreliminaryStandingProjects(int evalSessionId, EvalSessionResult evalSessionResult)
        {
            var standingPreliminaryProjects = (from esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(e => e.EvalSessionId == evalSessionId && e.IsDeleted == false && e.IsPreliminary)
                                               join p in this.unitOfWork.DbContext.Set<Project>() on esps.ProjectId equals p.ProjectId

                                               select new
                                               {
                                                   project = p,
                                                   IsPassedPreliminary = (bool?)null,
                                                   PointsPreliminary = (decimal?)null,
                                                   orderNum = esps.OrderNum,
                                                   status = esps.Status,
                                                   grant = p.TotalBfpAmount,
                                                   self = p.CoFinancingAmount,
                                                   note = string.Empty,
                                               }).ToList();

            standingPreliminaryProjects.ForEach(x =>
                evalSessionResult.Projects.Add(new EvalSessionResultProject(x.project)
                {
                    StandingPreliminaryPoints = x.PointsPreliminary,
                    StandingPreliminaryResult = x.IsPassedPreliminary,
                    ProjectStandingNumber = x.orderNum,
                    ProjectStandingStatus = x.status,
                    GrantAmount = x.grant,
                    SelfAmount = x.self,
                    Note = x.note,
                }));
        }

        public void AddStandingProjects(int evalSessionId, EvalSessionResult evalSessionResult)
        {
            var standingPreliminaryProjects = (from ess in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>().Where(e => e.EvalSessionId == evalSessionId && e.IsDeleted == false && e.IsPreliminary == false)
                                               join p in this.unitOfWork.DbContext.Set<Project>() on ess.ProjectId equals p.ProjectId
                                               join fpv in this.unitOfWork.DbContext.Set<ProjectVersionXml>().Where(x => x.OrderNum == 1) on ess.ProjectId equals fpv.ProjectId

                                               join ese2 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.AdminAdmiss && p.IsDeleted == false)
                                               on new { ess.EvalSessionId, ess.ProjectId } equals new { ese2.EvalSessionId, ese2.ProjectId } into g2
                                               from ese2 in g2.DefaultIfEmpty()

                                               join ese3 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.TechFinance && p.IsDeleted == false)
                                               on new { ess.EvalSessionId, ess.ProjectId } equals new { ese3.EvalSessionId, ese3.ProjectId } into g3
                                               from ese3 in g3.DefaultIfEmpty()

                                               join ese4 in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(p => p.EvalTableType == ProcedureEvalTableType.Complex && p.IsDeleted == false)
                                               on new { ess.EvalSessionId, ess.ProjectId } equals new { ese4.EvalSessionId, ese4.ProjectId } into g4
                                               from ese4 in g4.DefaultIfEmpty()

                                               select new
                                               {
                                                   project = p,
                                                   IsPassedPreliminary = (bool?)null,
                                                   PointsPreliminary = (decimal?)null,
                                                   IsPassedAdminAdmiss = ese2 != null ? ese2.EvalIsPassed : (bool?)null,
                                                   IsPassedTechfinance = ese3 != null ? ese3.EvalIsPassed : (bool?)null,
                                                   PointsTechfinance = ese3 != null ? ese3.EvalPoints : null,
                                                   IsPassedComplex = ese4 != null ? ese4.EvalIsPassed : (bool?)null,
                                                   PointsComplex = ese4 != null ? ese4.EvalPoints : null,
                                                   orderNum = ess.OrderNum,
                                                   status = ess.Status,
                                                   grant = fpv.TotalBfpAmount,
                                                   self = fpv.CoFinancingAmount,
                                                   correctedGrant = p.TotalBfpAmount,
                                                   correctedSelf = p.CoFinancingAmount,
                                                   note = ese3 == null ? ese4.EvalNote : ese3.EvalNote,
                                               }).ToList();

            standingPreliminaryProjects.ForEach(x =>
                evalSessionResult.Projects.Add(new EvalSessionResultProject(x.project)
                {
                    ProjectStandingNumber = x.orderNum,
                    ProjectStandingStatus = x.status,
                    GrantAmount = x.grant,
                    SelfAmount = x.self,
                    GrantAmountCorrected = x.correctedGrant,
                    SelfAmountCorrected = x.correctedSelf,
                    StandingPreliminaryPoints = x.PointsPreliminary,
                    StandingPreliminaryResult = x.IsPassedPreliminary,
                    EvaluationAdminAdmissResult = x.IsPassedAdminAdmiss,
                    StandingTechFinancePoints = x.PointsTechfinance,
                    StandingTechFinanceResult = x.IsPassedTechfinance,
                    StandingComplexPoints = x.PointsComplex,
                    StandingComplexResult = x.IsPassedComplex,
                    Note = x.note,
                }));
        }

        public bool CanRearrangeStanding(int evalSessionId, int standingId)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionStanding>().Where(x => x.EvalSessionId == evalSessionId && x.EvalSessionStandingId == standingId)
                    join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>() on ess.EvalSessionStandingId equals esps.EvalSessionStandingId
                    join espse in this.unitOfWork.DbContext.Set<EvalSessionProjectStandingEvaluation>() on esps.EvalSessionProjectStandingId equals espse.EvalSessionProjectStandingId
                    join ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>() on espse.EvalSessionEvaluationId equals ese.EvalSessionEvaluationId

                    group ese by ese.EvalType into g0
                    select g0.Key)
                    .Any(x => x == ProcedureEvalType.Weight);
        }

        public IList<string> CanEvaluateProject(int evalSessionId, ProcedureEvalTableType evalTableType, int projectId)
        {
            List<string> errors = new List<string>();

            var evalSheets = this.unitOfWork.DbContext.Set<EvalSessionSheet>()
                .Where(x => x.EvalSessionId == evalSessionId && x.EvalTableType == evalTableType && x.ProjectId == projectId)
                .OrderBy(x => x.EvalSessionSheetId)
                .ToList();

            bool interruptedChain = false;
            evalSheets.ForEach(x => interruptedChain = interruptedChain || this.IsInterruptedSheetChain(x, evalSheets));
            if (interruptedChain)
            {
                errors.Add(DataTexts.EvalSessionsRepository_CanEvaluateProject_PausedSheet);
            }

            if (evalTableType == ProcedureEvalTableType.TechFinance)
            {
                var hasCurrentAdminAdmissEvaluation = this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Any(x => x.IsDeleted == false && x.EvalSessionId == evalSessionId && x.ProjectId == projectId && x.EvalTableType == ProcedureEvalTableType.AdminAdmiss);
                if (!hasCurrentAdminAdmissEvaluation)
                {
                    errors.Add(DataTexts.EvalSessionsRepository_CanEvaluateProject_MissingAdminAdmiss);
                }
            }

            return errors;
        }

        public NewEvalSessionStandingType GetEvalSessionStandingType(int evalSessionStandingId)
        {
            var standings = this.unitOfWork.DbContext.Set<EvalSessionStanding>().Where(x => x.EvalSessionStandingId == evalSessionStandingId);
            var standing = standings.Single();

            if (standing.IsPreliminary)
            {
                return NewEvalSessionStandingType.Preliminary;
            }

            var tableType = (from ess in standings
                             join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>() on ess.EvalSessionStandingId equals esps.EvalSessionStandingId
                             join espse in this.unitOfWork.DbContext.Set<EvalSessionProjectStandingEvaluation>() on esps.EvalSessionProjectStandingId equals espse.EvalSessionProjectStandingId
                             join ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>() on espse.EvalSessionEvaluationId equals ese.EvalSessionEvaluationId
                             select ese.EvalTableType)
                             .Distinct()
                             .FirstOrDefault();

            if (tableType == ProcedureEvalTableType.Complex)
            {
                return NewEvalSessionStandingType.Complex;
            }

            return NewEvalSessionStandingType.TwoStep;
        }

        public EvalSessionActionsVO GetEvalSessionAvailableActions(int procedureId, int evalSessionId)
        {
            var procedureTableTypes = this.unitOfWork.DbContext.Set<ProcedureEvalTable>()
                .Where(x => x.ProcedureId == procedureId && x.Status == ProcedureEvalTableStatus.Ended && x.IsActive)
                .Select(x => x.Type)
                .ToList();

            var availableActions = new EvalSessionActionsVO(procedureTableTypes);

            return availableActions;
        }

        public bool EvalSessionHasStandingType(int evalSesionId, ProcedureEvalTableType procedureEvalTable)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionStanding>().Where(x => x.EvalSessionId == evalSesionId && x.Status != EvalSessionStandingStatus.Refused)
                    join esps in this.unitOfWork.DbContext.Set<EvalSessionProjectStanding>() on ess.EvalSessionStandingId equals esps.EvalSessionStandingId
                    join espse in this.unitOfWork.DbContext.Set<EvalSessionProjectStandingEvaluation>() on esps.EvalSessionProjectStandingId equals espse.EvalSessionProjectStandingId
                    join ese in this.unitOfWork.DbContext.Set<EvalSessionEvaluation>().Where(x => x.EvalTableType == procedureEvalTable && x.IsDeleted == false) on espse.EvalSessionEvaluationId equals ese.EvalSessionEvaluationId
                    select ese.EvalTableType)
                    .Any();
        }

        public IList<ProjectRegistrationsVO> GetProjectsForAutomaticProjectMonitorstatRequests(int evalSessionId)
        {
            var evalSessionProjectPredicate = PredicateBuilder.True<EvalSessionProject>()
                .And(esp => esp.EvalSessionId == evalSessionId)
                .And(esp => !esp.IsDeleted);

            return (from p in this.unitOfWork.DbContext.Set<Project>()
                    join esp in this.unitOfWork.DbContext.Set<EvalSessionProject>().Where(evalSessionProjectPredicate) on p.ProjectId equals esp.ProjectId
                    join pt in this.unitOfWork.DbContext.Set<ProjectType>() on p.ProjectTypeId equals pt.ProjectTypeId

                    join pkc in this.unitOfWork.DbContext.Set<KidCode>() on p.KidCodeId equals pkc.KidCodeId into g2
                    from pkc in g2.DefaultIfEmpty()

                    orderby p.SubmitDate
                    select new ProjectRegistrationsVO
                    {
                        ProjectId = p.ProjectId,
                        RegNumber = p.RegNumber,
                        NameBg = p.Name,
                        NameEn = p.NameAlt,
                        KidCode = pkc.Code,
                        CompanyNameBg = p.CompanyName,
                        CompanyNameEn = p.CompanyNameAlt,
                        CompanyUinType = p.CompanyUinType,
                        CompanyUin = p.CompanyUin,
                        RegistrationStatus = p.RegistrationStatus,
                        ProjectTypeBg = pt.Name,
                        ProjectTypeEn = pt.NameAlt,
                        RegDate = p.RegDate,
                        Version = p.Version,
                    })
                    .AsNoTracking()
                    .ToList();
        }

        public IList<int> GetEvalSessionProjectIds(int evalSessionId)
        {
            return this.unitOfWork.DbContext.Set<EvalSessionProject>()
                .Where(esp => esp.EvalSessionId == evalSessionId)
                .Select(esp => esp.ProjectId)
                .ToList();
        }

        public Procedure GetEvalSessionProcedure(int evalSessionId)
        {
            var procedure =
                (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                 join p in this.unitOfWork.DbContext.Set<Procedure>() on es.ProcedureId equals p.ProcedureId
                 where es.EvalSessionId == evalSessionId
                 select p)
                 .AsNoTracking()
                 .SingleOrDefault();

            return procedure;
        }

        private bool IsInterruptedSheetChain(EvalSessionSheet currentSheet, List<EvalSessionSheet> sheets)
        {
            if (currentSheet.Status == EvalSessionSheetStatus.Paused && !currentSheet.ContinuedEvalSessionSheetId.HasValue)
            {
                return true;
            }

            if (currentSheet.Status == EvalSessionSheetStatus.Paused)
            {
                var nextSheet = sheets.Where(x => x.EvalSessionSheetId == currentSheet.ContinuedEvalSessionSheetId.Value).FirstOrDefault();
                if (nextSheet == null)
                {
                    return true;
                }

                return this.IsInterruptedSheetChain(nextSheet, sheets);
            }

            return false;
        }

        private class EvalSessionStandingProjectsComparer : Comparer<EvalSessionStandingProjectDO>
        {
            private bool compareOrderNumProperty = true;

            public EvalSessionStandingProjectsComparer(bool compareOrderNum = true)
            {
                this.compareOrderNumProperty = compareOrderNum;
            }

            public override int Compare(EvalSessionStandingProjectDO x, EvalSessionStandingProjectDO y)
            {
                Func<int?, int?, string, string, int> compareProjects = (ofx, ofy, rnx, rny) =>
                {
                    if (ofx.HasValue && !ofy.HasValue)
                    {
                        return 1;
                    }
                    else if (!ofx.HasValue && ofy.HasValue)
                    {
                        return -1;
                    }

                    // two different EvalSessionStandigs can't have the same orderNum, but LINQ's OrderBy
                    // compares elements with themselves
                    else if (ofx.HasValue && ofy.HasValue && ofx != ofy)
                    {
                        return ofx.Value > ofy.Value ? -1 : 1;
                    }
                    else
                    {
                        return 0 - rnx.CompareTo(y.ProjectRegNumber);
                    }
                };

                if (!this.compareOrderNumProperty)
                {
                    return compareProjects(x.ManualOrderNum, y.ManualOrderNum, x.ProjectRegNumber, y.ProjectRegNumber);
                }

                return compareProjects(x.OrderNum, y.OrderNum, x.ProjectRegNumber, y.ProjectRegNumber);
            }
        }
    }
}
