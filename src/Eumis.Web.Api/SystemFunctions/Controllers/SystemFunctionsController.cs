using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Autofac.Features.Indexed;
using Eumis.Authentication.Authorization;

namespace Eumis.Web.Api.SystemFunctions.Controllers
{
    [RoutePrefix("api/system")]
    public class SystemFunctionsController : ApiController
    {
        public SystemFunctionsController()
        {
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return this.Ok();
        }
    }
}
