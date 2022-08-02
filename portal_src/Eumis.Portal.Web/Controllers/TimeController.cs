using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eumis.Portal.Web.Helpers;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    //[RoutePrefix("api/time")]
    public class TimeController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public object Now()
        {
            var now = DateTime.Now;

            return new
            {
                year = now.Year,
                month = now.Month,
                day = now.Day,
                hour = now.Hour,
                minute = now.Minute,
                second = now.Second
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public object LastSave()
        {
            if (AppContext.Current == null && !AppContext.Current.LastSaveDate.HasValue)
            {
                return Now();
            }
            else
            {
                
            }

            var lastSaveDate = AppContext.Current.LastSaveDate.Value;

            return new
            {
                year = lastSaveDate.Year,
                month = lastSaveDate.Month,
                day = lastSaveDate.Day,
                hour = lastSaveDate.Hour,
                minute = lastSaveDate.Minute,
                second = lastSaveDate.Second
            };
        }
    }
}
