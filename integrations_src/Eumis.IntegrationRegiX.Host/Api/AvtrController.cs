using Eumis.IntegrationRegiX.Host.Communicators;
using Eumis.RegiX.Rio.AVTR;
using Eumis.RegiX.Rio.DAEU;
using System.Web.Http;

namespace Eumis.IntegrationRegiX.Api
{
    [Authorize]
    [RoutePrefix("api/regix/AVTR")]
    public class AvtrController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRegixCommunicator communicator;

        public AvtrController(IRegixCommunicator communicator)
        {
            this.communicator = communicator;
        }

        [HttpGet]
        [Route("GetValidUICInfo")]
        public ValidUICResponse GetValidUICInfo(string uic, string procedureCode = null)
        {
            Logger.Info("GetValidUICInfo");
            return this.communicator.ValidUicInfo(uic, procedureCode);
        }

        [HttpGet]
        [Route("GetActualState")]
        public ActualStateResponse GetActualState(string uic, string procedureCode = null)
        {
            Logger.Info("GetActualState");
            return this.communicator.GetActualState(uic, procedureCode);
        }
    }
}
