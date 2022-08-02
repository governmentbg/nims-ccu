using Eumis.IntegrationRegiX.Host.Communicators;
using Eumis.RegiX.Rio.MP;
using Eumis.RegiX.Rio.MVR;
using System.Web.Http;

namespace Eumis.IntegrationRegiX.Api
{
    [Authorize]
    [RoutePrefix("api/regix/MPNPO")]
    public class MpController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRegixCommunicator communicator;

        public MpController(IRegixCommunicator communicator)
        {
            this.communicator = communicator;
        }

        [HttpGet]
        [Route("GetNPORegistrationInfo")]
        public NPODetailsResponse GetNPORegistrationInfo(string uic, string procedureCode = null)
        {
            Logger.Info("GetNPORegistrationInfo");
            return this.communicator.GetNPORegistrationInfo(uic, procedureCode);
        }
    }
}
