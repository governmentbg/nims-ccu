using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Eumis.Authentication.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Eumis.Web.Api.Core
{
    [RoutePrefix("api/authorizer")]
    public class AuthorizerController : ApiController
    {
        private IAuthorizer authorizer;

        public AuthorizerController(IAuthorizer authorizer)
        {
            this.authorizer = authorizer;
        }

        [HttpGet]
        [Route("cando")]
        public bool CanDo(string action, int? id = null)
        {
            return this.authorizer.CanDo(action, id);
        }
    }
}
