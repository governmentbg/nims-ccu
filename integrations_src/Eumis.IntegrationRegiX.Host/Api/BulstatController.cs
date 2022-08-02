using Eumis.IntegrationRegiX.Host.Communicators;
using Eumis.RegiX.Rio.AVBULSTAT;
using System.Web.Http;

namespace Eumis.IntegrationRegiX.Api
{
    [Authorize]
    [RoutePrefix("api/regix/AVBulstat2")]
    public class BulstatController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRegixCommunicator communicator;

        public BulstatController(IRegixCommunicator communicator)
        {
            this.communicator = communicator;
        }

        [HttpGet]
        [Route("GetStateOfPlay")]
        public StateOfPlay GetStateOfPlay(string uic, string procedureCode = null)
        {
            Logger.Info("GetStateOfPlay");
            return this.communicator.GetStateOfPlay(uic, procedureCode);
        }
    }
}
