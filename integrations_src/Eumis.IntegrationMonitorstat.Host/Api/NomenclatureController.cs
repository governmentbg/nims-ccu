using Autofac.Extras.NLog;
using Eumis.Integration.Monitorstat.Communicators;
using Monitorstat.Common.Contracts;
using Monitorstat.Common.MonitorstatService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Integration.Monitorstat.Api
{
    [RoutePrefix("monitorstat/nomenclatures")]
    public class NomenclatureController : ApiController
    {
        private IMonitorstatCommunicator client;

        public NomenclatureController(IMonitorstatCommunicator client)
        {
            this.client = client;
        }

        [HttpGet]
        [Route("statuses")]
        public Nomenclature[] GetStatuses()
        {
            return this.client.GetProgrammeStatuses();
        }

        [HttpGet]
        [Route("groups")]
        public Nomenclature[] GetProgrammeGroups()
        {
            return this.client.GetProgrammeGroups();
        }
    }
}
