using Eumis.Common.Filters;
using System.Web;
using System.Web.Mvc;
using Eumis.Portal.Web.Helplers.Filters;
using System.Web.Http;

namespace Eumis.Portal.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new NLogExceptionFilter());
            filters.Add(new NLogTraceFilter());

            filters.Add(new ServiceUnavailableFilter());
            filters.Add(new AjaxExceptionFilter());
#if DEBUG
#else
            filters.Add(new PrivateAreaExceptionFilter());
            filters.Add(new ReportAreaExceptionFilter());
#endif
        }

        public static void RegisterGlobalWebApiFilters(System.Web.Http.Filters.HttpFilterCollection filters)
        {
            filters.Add(new NLogApiExceptionFilter());
            //filters.Add(new NLogApiTraceFilter());
        }
    }
}
