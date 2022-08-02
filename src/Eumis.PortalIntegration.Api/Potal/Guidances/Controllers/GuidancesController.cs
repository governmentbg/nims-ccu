using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.Common.Db;
using Eumis.Data.Guidances.Repositories;
using Eumis.Data.Guidances.ViewObjects;
using Eumis.Domain.Guidances;

namespace Eumis.PortalIntegration.Api.Portal.Guidances.Controllers
{
    [RoutePrefix("api/guidances")]
    public class GuidancesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IGuidancesRepository guidancesRepository;

        public GuidancesController(
            IUnitOfWork unitOfWork,
            IGuidancesRepository guidancesRepository)
        {
            this.unitOfWork = unitOfWork;
            this.guidancesRepository = guidancesRepository;
        }

        [AllowAnonymous]
        [Route("")]
        public IList<GuidanceVO> GetGuidances(GuidanceModule module)
        {
            if (module == GuidanceModule.InternalSystem)
            {
                throw new InvalidOperationException("Invalid guidance module.");
            }

            return this.guidancesRepository.GetGuidances(module);
        }
    }
}
