using Autofac.Extras.Attributed;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Calendar.ViewObjects;
using Eumis.Public.Data.Core;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.Procedures;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;

namespace Eumis.Public.Data.Calendar.Repositories
{
    internal class CalendarsRepository : Repository, ICalendarsRepository
    {
        public CalendarsRepository([WithKey(DbKey.Umis)]IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<EventSummaryVO> GetEventSummaries(string dateString, string callbackAction)
        {
            DateTime date;
            if (!DataUtils.TryParseCalendarDate(dateString, out date))
            {
                date = DateTime.Now;
            }

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            var procedureEvents = from p in this.unitOfWork.DbContext.Set<Procedure>().Where(x => x.ActivationDate.HasValue)
                                  join tl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>().Where(x => x.EndDate >= firstDayOfMonth && x.EndDate <= lastDayOfMonth) on p.ProcedureId equals tl.ProcedureId
                                  group tl.EndDate by DbFunctions.TruncateTime(tl.EndDate) into g
                                  select new EventSummaryVO
                                  {
                                      Date = g.Key,
                                      count = g.Count(),
                                  };

            var contractProcurements = from cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>()
                                       join p in this.unitOfWork.DbContext.Set<ContractProcurementPlan>().Where(c => !c.TerminatedDate.HasValue && c.AnnouncedDate.HasValue) on cdf.ContractProcurementPlanId equals p.ContractProcurementPlanId
                                       where p.OffersDeadlineDate >= firstDayOfMonth && p.OffersDeadlineDate <= lastDayOfMonth
                                       group p.OffersDeadlineDate by DbFunctions.TruncateTime(p.OffersDeadlineDate) into g
                                       select new EventSummaryVO
                                       {
                                           Date = g.Key,
                                           count = g.Count(),
                                       };

            var publicDiscussions = from p in this.unitOfWork.DbContext.Set<Procedure>()
                                    join pd in this.unitOfWork.DbContext.Set<PublicDiscussion>().Where(x => x.EndDate >= firstDayOfMonth && x.EndDate <= lastDayOfMonth) on p.ProcedureId equals pd.ProcedureId
                                    where pd.Status == PublicDiscussionStatus.Published
                                    group pd.EndDate by DbFunctions.TruncateTime(pd.EndDate) into g
                                    select new EventSummaryVO
                                    {
                                        Date = g.Key,
                                        count = g.Count(),
                                    };

            var tmpEvents = procedureEvents.Concat(contractProcurements).Concat(publicDiscussions);

            var events = (from e in tmpEvents
                          group e.count by DbFunctions.TruncateTime(e.Date) into g
                          select new EventSummaryVO
                          {
                              Date = g.Key,
                              count = g.Sum(),
                          }).ToList();

            events.ForEach(x => x.url = callbackAction);

            return events;
        }

        public IList<EventVO> GetEvents(DateTime date)
        {
            var procedureEvents = from p in this.unitOfWork.DbContext.Set<Procedure>().Where(x => x.ActivationDate.HasValue)
                                  join tl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>().Where(x => DbFunctions.TruncateTime(x.EndDate) == date) on p.ProcedureId equals tl.ProcedureId
                                  select new EventVO
                                  {
                                      Date = tl.EndDate,
                                      Description = p.Description,
                                      DescriptionEN = p.DescriptionAlt,
                                      Title = p.Name,
                                      TitleEN = p.NameAlt,
                                      SourceId = p.ProcedureId,
                                      EventType = p.ProcedureStatus != ProcedureStatus.Canceled && p.ProcedureStatus != ProcedureStatus.Terminated && p.ProcedureStatus != ProcedureStatus.Ended ?
                                          Domain.Custom.Events.EventType.Procedure :
                                          Domain.Custom.Events.EventType.ProcedureEnded,
                                      Guid = p.Gid,
                                  };

            var procurementEvents = from cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>()
                                    join p in this.unitOfWork.DbContext.Set<ContractProcurementPlan>().Where(c => !c.TerminatedDate.HasValue && c.AnnouncedDate.HasValue) on cdf.ContractProcurementPlanId equals p.ContractProcurementPlanId
                                    where DbFunctions.TruncateTime(p.OffersDeadlineDate) == date
                                    select new EventVO
                                    {
                                        Date = p.OffersDeadlineDate.Value,
                                        Description = p.Description,
                                        DescriptionEN = p.Description,
                                        Title = p.Name,
                                        TitleEN = p.Name,
                                        SourceId = p.ContractProcurementPlanId,
                                        EventType = Domain.Custom.Events.EventType.Procurement,
                                        Guid = cdf.Gid,
                                    };

            var discussionEvents = from p in this.unitOfWork.DbContext.Set<Procedure>()
                                   join pd in this.unitOfWork.DbContext.Set<PublicDiscussion>().Where(x => DbFunctions.TruncateTime(x.EndDate) == date) on p.ProcedureId equals pd.ProcedureId
                                   where pd.Status == PublicDiscussionStatus.Published
                                   select new EventVO
                                   {
                                       Date = pd.EndDate.Value,
                                       Description = p.Description,
                                       DescriptionEN = p.DescriptionAlt,
                                       Title = p.Name,
                                       TitleEN = p.NameAlt,
                                       SourceId = p.ProcedureId,
                                       EventType = pd.EndDate > DateTime.Now ? Domain.Custom.Events.EventType.PublicDiscussion : Domain.Custom.Events.EventType.PublicDiscussionEnded,
                                       Guid = p.Gid,
                                   };

            return procedureEvents
                .Concat(procurementEvents)
                .Concat(discussionEvents)
                .OrderBy(x => x.Date)
                .ToList();
        }
    }
}
