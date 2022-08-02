using Eumis.IntegrationRegiX.Host.Communicators;
using Eumis.RegiX.Rio.MVR;
using System.Web.Http;

namespace Eumis.IntegrationRegiX.Api
{
    [Authorize]
    [RoutePrefix("api/regix/MVRBDS")]
    public class MvrController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRegixCommunicator communicator;

        public MvrController(IRegixCommunicator communicator)
        {
            this.communicator = communicator;
        }

        [HttpGet]
        [Route("GetPersonalIdentity")]
        public PersonalIdentityInfoResponse GetPersonalIdentity(string personalBulstat, string identityDocumentNumber, string procedureCode = null)
        {
            Logger.Info("GetPersonalIdentity");
            return this.communicator.GetPersonalIdentity(personalBulstat, identityDocumentNumber, procedureCode);
        }
    }
}
