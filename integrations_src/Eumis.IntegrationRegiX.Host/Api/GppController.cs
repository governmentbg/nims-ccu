using Eumis.IntegrationRegiX.Host.Communicators;
using Eumis.RegiX.Rio.GPP;
using System;
using System.Web.Http;

namespace Eumis.IntegrationRegiX.Api
{
    [Authorize]
    [RoutePrefix("api/regix/GitPenalProvisions")]
    public class GppController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRegixCommunicator communicator;

        public GppController(IRegixCommunicator communicator)
        {
            this.communicator = communicator;
        }

        [HttpGet]
        [Route("GetPenalProvisionForPeriod")]
        public PenalProvisionForPeriodResponse GetPenalProvisionForPeriod(
            string id,
            DateTime dateFrom,
            DateTime dateTo,
            string procedureCode = null)
        {
            Logger.Info("GetPenalProvisionForPeriod");
            return this.communicator.GetPenalProvisionForPeriod(id, dateFrom, dateTo, procedureCode);
        }
    }
}
