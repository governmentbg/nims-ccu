using Autofac.Extras.NLog;
using Eumis.Integration.Monitorstat.Communicators;
using Monitorstat.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Integration.Monitorstat.Api
{
    [Authorize]
    [RoutePrefix("monitorstat/subjectRequests")]
    public class SubjectRequestController : ApiController
    {
        private IMonitorstatCommunicator client;

        public SubjectRequestController(IMonitorstatCommunicator client)
        {
            this.client = client;
        }

        [HttpPost]
        [Route("")]
        public Guid SendSubjectRequest(SubjectRequestDO subjectRequest)
        {
            return this.client.CreateSubjectRequest(subjectRequest);
        }
    }
}
