using Eumis.Public.Data.Calendar.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Data.Calendar.Repositories
{
    public interface ICalendarsRepository
    {
        IList<EventSummaryVO> GetEventSummaries(string dateString, string callbackAction);

        IList<EventVO> GetEvents(DateTime date);
    }
}
