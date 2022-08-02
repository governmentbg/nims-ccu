using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Prognoses.Repositories;
using Eumis.Data.Prognoses.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Web.Api.Prognoses.DataObjects;

namespace Eumis.Web.Api.Prognoses.Controllers
{
    [RoutePrefix("api/programmePrognoses")]
    public class ProgrammePrognosesController : PrognosesController
    {
        private IAuthorizer authorizer;
        private IPrognosesRepository prognosesRepository;

        public ProgrammePrognosesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IPrognosesRepository prognosesRepository)
            : base(
                PrognosisLevel.Programme,
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
            return this.prognosesRepository.CanCreateProgrammePrognosis(
                prognosis.ScopeId.Value,
                prognosis.Year.Value,
                prognosis.Month.Value);
        }

        public override void AssertCanView(int prognosisId)
        {
            this.authorizer.AssertCanDo(ProgrammePrognosisActions.View, prognosisId);
        }

        public override void AssertCanEdit(int prognosisId)
        {
            this.authorizer.AssertCanDo(ProgrammePrognosisActions.Edit, prognosisId);
        }
    }
}
