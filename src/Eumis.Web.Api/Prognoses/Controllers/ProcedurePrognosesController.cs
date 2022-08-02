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
    [RoutePrefix("api/procedurePrognoses")]
    public class ProcedurePrognosesController : PrognosesController
    {
        private IAuthorizer authorizer;
        private IPrognosesRepository prognosesRepository;

        public ProcedurePrognosesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IPrognosesRepository prognosesRepository)
            : base(
                PrognosisLevel.Procedure,
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
            return this.prognosesRepository.CanCreateProcedurePrognosis(
                prognosis.ScopeId.Value,
                prognosis.Year.Value,
                prognosis.Month.Value);
        }

        public override void AssertCanView(int prognosisId)
        {
            this.authorizer.AssertCanDo(ProcedurePrognosisActions.View, prognosisId);
        }

        public override void AssertCanEdit(int prognosisId)
        {
            this.authorizer.AssertCanDo(ProcedurePrognosisActions.Edit, prognosisId);
        }
    }
}
