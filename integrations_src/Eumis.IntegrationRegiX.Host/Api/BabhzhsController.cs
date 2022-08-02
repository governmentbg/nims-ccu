using Eumis.IntegrationRegiX.Host.Communicators;
using Eumis.RegiX.Rio.BABHZhS;
using System.Web.Http;

namespace Eumis.IntegrationRegiX.Api
{
    [Authorize]
    [RoutePrefix("api/regix/BABHZhS")]
    public class BabhzhsController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRegixCommunicator communicator;

        public BabhzhsController(IRegixCommunicator communicator)
        {
            this.communicator = communicator;
        }

        [HttpGet]
        [Route("GetRegisteredAnimalsByCategory")]
        public RegisteredAnimalsByCategoryResponse GetRegisteredAnimalsByCategory(string personalBulstat, string procedureCode = null)
        {
            Logger.Info("GetRegisteredAnimalsByCategory");
            return this.communicator.GetRegisteredAnimalsByCategory(personalBulstat, procedureCode);
        }

        [HttpGet]
        [Route("GetRegisteredAnimalsInOEZByCategory")]
        public RegisteredAnimalsInOEZByCategoryResponse GetRegisteredAnimalsInOEZByCategory(string id, string procedureCode = null)
        {
            Logger.Info("GetRegisteredAnimalsInOEZByCategory");
            return this.communicator.GetRegisteredAnimalsInOEZByCategory(id, procedureCode);
        }
    }
}
