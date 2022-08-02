using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Calendar.Repositories;
using Eumis.Public.Domain.Custom.Events;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Events;

namespace Eumis.Public.Web.Controllers
{
    public partial class EventsController : BaseWithExportController
    {
        private ICalendarsRepository calendarsRepository;

        public EventsController(ICalendarsRepository calendarsRepository)
        {
            this.calendarsRepository = calendarsRepository;
        }

        public virtual ActionResult Index(string date)
        {
            DateTime tmpDate;
            if (!DataUtils.TryParseCalendarDate(date, out tmpDate))
            {
                tmpDate = DateTime.Now;
            }

            var events = this.calendarsRepository.GetEvents(tmpDate);

            EventModel em = new EventModel()
            {
                Date = tmpDate,
                Events = events,
            };

            return this.View(em);
        }

        public override ExportTemplate RenderTemplate()
        {
            DateTime tmpDate = DateTime.Now;

            if (!string.IsNullOrEmpty(this.Request.QueryString["date"]))
            {
                if (!DataUtils.TryParseCalendarDate(this.Request.QueryString["date"], out tmpDate))
                {
                    tmpDate = DateTime.Now;
                }
            }

            var template = new ExportTemplate("EventsList");
            template.Sheet.Name = "Events";
            var events = this.calendarsRepository.GetEvents(tmpDate);
            if (events != null && events.Count > 0)
            {
                var title = string.Format(Texts.Events_Index_Caption, tmpDate.ToShortDateString());
                var table = new ExportTable(title);
                var headerRow = new ExportRow();

                for (int i = 0; i < 3; i++)
                {
                    headerRow.Cells.Add(new ExportCell { IsHeader = true });
                }

                headerRow.Cells[0].Value = Texts.Events_Index_EventType;
                headerRow.Cells[1].Value = Texts.Events_Index_Name;
                headerRow.Cells[2].Value = Texts.Events_Index_Time;

                table.Rows.Add(headerRow);

                foreach (var localEvent in events)
                {
                    var row = new ExportRow();

                    row.Cells.Add(new ExportCell { Value = localEvent.EventType == EventType.Procedure ? Texts.Events_Index_Procedure : localEvent.EventType == EventType.Procurement ? Texts.Events_Index_Procurement : Texts.Events_Index_PublicDiscussion });
                    row.Cells.Add(localEvent.TransTitle.ToExportCell());
                    row.Cells.Add(localEvent.Date.ToExportCell());

                    table.Rows.Add(row);
                }

                template.Sheet.Tables.Add(table);

                template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
                {
                    { 1, 200 },
                    { 2, 500 },
                    { 3, 200 },
                };
            }

            return template;
        }
    }
}
