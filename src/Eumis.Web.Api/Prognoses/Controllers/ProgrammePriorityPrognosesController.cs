using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Prognoses.Repositories;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Web.Api.Prognoses.DataObjects;

namespace Eumis.Web.Api.Prognoses.Controllers
{
    [RoutePrefix("api/programmePriorityPrognoses")]
    public class ProgrammePriorityPrognosesController : PrognosesController
    {
        private IAuthorizer authorizer;
        private IPrognosesRepository prognosesRepository;

        public ProgrammePriorityPrognosesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IPrognosesRepository prognosesRepository)
            : base(
                PrognosisLevel.ProgrammePriority,
                unitOfWork,
                authorizer,
                accessContext,
                permissionsRepository,
                prognosesRepository)
        {
            this.authorizer = authorizer;
            this.prognosesRepository = prognosesRepository;
        }

        public override IList<string> CanCreate(NewPrognosisDO prognosis)
        {
            return this.prognosesRepository.CanCreateProgrammePriorityPrognosis(
                prognosis.ScopeId.Value,
                prognosis.Year.Value,
                prognosis.Month.Value);
        }

        public override void AssertCanView(int prognosisId)
        {
            this.authorizer.AssertCanDo(ProgrammePriorityPrognosisActions.View, prognosisId);
        }

        public override void AssertCanEdit(int prognosisId)
        {
            this.authorizer.AssertCanDo(ProgrammePriorityPrognosisActions.Edit, prognosisId);
        }
    }
}
