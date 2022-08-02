using Eumis.IntegrationRegiX.Host.Communicators;
using Eumis.RegiX.Rio.GRAO;
using Eumis.RegiX.Rio.NRA;
using System.Web.Http;

namespace Eumis.IntegrationRegiX.Api
{
    [Authorize]
    [RoutePrefix("api/regix/NRAObligatedPersons")]
    public class NraController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRegixCommunicator communicator;

        public NraController(IRegixCommunicator communicator)
        {
            this.communicator = communicator;
        }

        [HttpGet]
        [Route("GetObligatedPersons")]
        public ObligationResponse GetObligatedPersons(string id, EikTypeTypeRequest type = EikTypeTypeRequest.EGN, ushort treshold = 100, string procedureCode = null)
        {
            Logger.Info("GetObligatedPersons");
            return this.communicator.GetObligatedPersons(id, type, treshold, procedureCode);
        }
    }
}
