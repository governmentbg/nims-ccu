using Eumis.IntegrationRegiX.Host.Communicators;
using Eumis.RegiX.Rio.DAEU;
using System.Web.Http;

namespace Eumis.IntegrationRegiX.Api
{
    [Authorize]
    [RoutePrefix("api/regix/DaeuReports")]
    public class DaeuController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRegixCommunicator communicator;

        public DaeuController(IRegixCommunicator communicator)
        {
            this.communicator = communicator;
        }

        [HttpGet]
        [Route("SearchByIdentifier")]
        public SearchByIdentifierResponse SearchByIdentifier(string personalBulstat, string procedureCode = null)
        {
            Logger.Info("SearchByIdentifier");
            return this.communicator.SearchByIdentifier(personalBulstat, procedureCode);
        }
    }
}
