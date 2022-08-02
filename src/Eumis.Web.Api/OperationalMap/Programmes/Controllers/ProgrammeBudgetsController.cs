using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/programmes/{programmeId}/budgets")]
    public class ProgrammeBudgetsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProgrammesRepository programmesRepository;
        private IAuthorizer authorizer;

        public ProgrammeBudgetsController(IUnitOfWork unitOfWork, IProgrammesRepository programmesRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.programmesRepository = programmesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProgrammeBudgetsWrapperVO> GetProgrammeBudgets(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            return this.programmesRepository.GetProgrammeBudgets(programmeId);
        }
    }
}
