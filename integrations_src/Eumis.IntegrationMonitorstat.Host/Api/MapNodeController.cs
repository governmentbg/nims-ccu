using System;
using System.Web.Http;
using Eumis.Integration.Monitorstat.Communicators;
using Monitorstat.Common.Contracts;

namespace Eumis.Integration.Monitorstat.Api
{
    [Authorize]
    [RoutePrefix("monitorstat/mapNodes")]
    public class MapNodeController : ApiController
    {
        private IMonitorstatCommunicator client;

        public MapNodeController(IMonitorstatCommunicator client)
        {
            this.client = client;
        }

        [HttpPost]
        [Route("programmes")]
        public Guid CrateProgramme([FromBody]ProgrammeDO programme)
        {
            return this.client.CreateOperationalProgramme(programme);
        }

        [HttpPost]
        [Route("programmePriorities")]
        public Guid CreateProgrammePriority([FromBody]ProgrammePriorityDO programmePriority)
        {
            return this.client.CreateProgrammePriority(programmePriority);
        }

        [HttpPost]
        [Route("procedures")]
        public Guid CreateProcedure([FromBody]ProcedureDO procedure)
        {
            Guid id = this.client.CreateProcedure(procedure);
            return id;
        }
    }
}
