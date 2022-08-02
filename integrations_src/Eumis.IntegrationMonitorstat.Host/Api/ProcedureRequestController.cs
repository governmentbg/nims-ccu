using Autofac.Extras.NLog;
using Eumis.Integration.Monitorstat.Communicators;
using Monitorstat.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Integration.Monitorstat.Api
{
    [Authorize]
    [RoutePrefix("monitorstat/procedureRequests")]
    public class ProcedureRequestController : ApiController
    {
        private IMonitorstatCommunicator client;

        public ProcedureRequestController(IMonitorstatCommunicator client)
        {
            this.client = client;
        }

        [HttpPost]
        [Route("")]
        public Guid SendProcedureRequest(ProcedureInquiryDO procedureInquiry)
        {
            return this.client.CreateProcedureInquiry(procedureInquiry);
        }
    }
}
