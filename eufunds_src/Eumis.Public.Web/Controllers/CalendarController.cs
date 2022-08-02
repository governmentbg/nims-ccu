using Eumis.Public.Common.Localization;
using Eumis.Public.Data.Calendar.Repositories;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace Eumis.Public.Web.Controllers
{
    public partial class CalendarController : BaseController
    {
        private ICalendarsRepository calendarsRepository;

        public CalendarController(ICalendarsRepository calendarsRepository)
        {
            this.calendarsRepository = calendarsRepository;
        }

        [HttpGet]
        public virtual JsonResult GetEvents(string id)
        {
            return this.Json(
                this.calendarsRepository.GetEventSummaries(
                    id,
                    $"{SystemLocalization.GetCurrentCulture()}/0/0/{MVC.Events.Name}/{MVC.Events.ActionNames.Index}/?{MVC.Events.IndexParams.date}="),
                JsonRequestBehavior.AllowGet);
        }
    }
}
