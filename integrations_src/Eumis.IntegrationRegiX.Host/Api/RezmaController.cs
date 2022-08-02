using Eumis.IntegrationRegiX.Host.Communicators;
using Eumis.RegiX.Rio.REZMA;
using System.Web.Http;

namespace Eumis.IntegrationRegiX.Api
{
    [Authorize]
    [RoutePrefix("api/regix/REZMA")]
    public class RezmaController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRegixCommunicator communicator;

        public RezmaController(IRegixCommunicator communicator)
        {
            this.communicator = communicator;
        }

        [HttpGet]
        [Route("CheckObligations")]
        public CheckObligationsResponse CheckObligations(string id, string procedureCode = null)
        {
            Logger.Info("CheckObligations");
            return this.communicator.CheckObligations(id, procedureCode);
        }
    }
}
