using Eumis.IntegrationRegiX.Host.Communicators;
using Eumis.RegiX.Rio.GRAO;
using System.Web.Http;

namespace Eumis.IntegrationRegiX.Api
{
    [Authorize]
    [RoutePrefix("api/regix/GraoNBD")]
    public class GraoController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRegixCommunicator communicator;

        public GraoController(IRegixCommunicator communicator)
        {
            this.communicator = communicator;
        }

        [HttpGet]
        [Route("{personalBulstat}/validPersonSearch")]
        public ValidPersonResponse ValidPersonSearch(string personalBulstat, string procedureCode = null)
        {
            Logger.Info("ValidPersonSearch");
            return this.communicator.ValidPersonSearch(personalBulstat, procedureCode);
        }

        [HttpGet]
        [Route("PersonDataSearch")]
        public PersonDataResponse PersonDataSearch(string personalBulstat, string procedureCode = null)
        {
            Logger.Info("PersonDataSearch");
            return this.communicator.PersonDataSearch(personalBulstat, procedureCode);
        }
    }
}
