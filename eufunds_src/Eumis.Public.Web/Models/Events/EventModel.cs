using Eumis.Public.Data.Calendar.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Public.Web.Models.Events
{
    public class EventModel
    {
        public DateTime Date { get; set; }

        public IList<EventVO> Events { get; set; }
    }
}
