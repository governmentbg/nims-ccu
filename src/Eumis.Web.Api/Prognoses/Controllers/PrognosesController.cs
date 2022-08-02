using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Prognoses.Repositories;
using Eumis.Data.Prognoses.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Prognoses.DataObjects;

namespace Eumis.Web.Api.Prognoses.Controllers
{
    public abstract class PrognosesController : ApiController
    {
        private PrognosisLevel level;

        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IPrognosesRepository prognosesRepository;

        protected PrognosesController(
            PrognosisLevel level,
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IPrognosesRepository prognosesRepository)
        {
            this.level = level;
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.prognosesRepository = prognosesRepository;
        }

        public abstract IList<string> CanCreate(NewPrognosisDO prognosis);

        public abstract void AssertCanView(int prognosisId);

        public abstract void AssertCanEdit(int prognosisId);

        [Route("")]
        public IList<PrognosisVO> GetPrognoses(
            Year? year = null,
            Month? month = null)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.prognosesRepository.GetPrognoses(programmeIds, this.level, year, month);
        }

        [HttpGet]
        [Route("new")]
        public NewPrognosisDO NewPrognosis()
        {
            this.authorizer.AssertCanDo(PrognosisListActions.Create);

            return new NewPrognosisDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Prognoses.Create))]
        public object CreatePrognosis(NewPrognosisDO prognosis)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.Create);

            if (this.CanCreate(prognosis).Any())
            {
                throw new DomainValidationException("Cannot create prognosis.");
            }

            var newPrognosis = new Prognosis(
                this.level,
                prognosis.ScopeId.Value,
                prognosis.Year.Value,
                prognosis.Month.Value);

            this.prognosesRepository.Add(newPrognosis);
            this.unitOfWork.Save();

            return new { PrognosisId = newPrognosis.PrognosisId };
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreatePrognosis(NewPrognosisDO prognosis)
        {
            this.authorizer.AssertCanDo(PrognosisListActions.Create);

            return new ErrorsDO(this.CanCreate(prognosis));
        }

        [Route("{prognosisId:int}")]
        public PrognosisDO GetPrognosis(int prognosisId)
        {
            this.AssertCanView(prognosisId);

            var prognosis = this.prognosesRepository.Find(prognosisId);

            return new PrognosisDO(prognosis);
        }

        [HttpPut]
        [Route("{prognosisId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Prognoses.Edit))]
        public void UpdatePrognosis(int prognosisId, PrognosisDO prognosisDO)
        {
            this.AssertCanEdit(prognosisId);

            var prognosis = this.prognosesRepository.FindForUpdate(prognosisId, prognosisDO.Version);
            prognosis.UpdateData(
                prognosisDO.ContractedEuAmount,
                prognosisDO.ContractedBgAmount,
                prognosisDO.PaymentEuAmount,
                prognosisDO.PaymentBgAmount,
                prognosisDO.AdvancePaymentEuAmount,
                prognosisDO.AdvancePaymentBgAmount,
                prognosisDO.AdvanceVerPaymentEuAmount,
                prognosisDO.AdvanceVerPaymentBgAmount,
                prognosisDO.IntermediatePaymentEuAmount,
                prognosisDO.IntermediatePaymentBgAmount,
                prognosisDO.FinalPaymentEuAmount,
                prognosisDO.FinalPaymentBgAmount,
                prognosisDO.ApprovedEuAmount,
                prognosisDO.ApprovedBgAmount,
                prognosisDO.CertifiedEuAmount,
                prognosisDO.CertifiedBgAmount);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{prognosisId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Prognoses.ChangeStatusToDraft), IdParam = "prognosisId")]
        public void MakeDraft(int prognosisId, string version)
        {
            this.AssertCanEdit(prognosisId);

            byte[] vers = System.Convert.FromBase64String(version);
            var prognosis = this.prognosesRepository.FindForUpdate(prognosisId, vers);

            prognosis.ChangeStatusToDraft();
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{prognosisId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Prognoses.ChangeStatusToEntered), IdParam = "prognosisId")]
        public void EnterPrognosis(int prognosisId, string version)
        {
            this.AssertCanEdit(prognosisId);

            byte[] vers = System.Convert.FromBase64String(version);
            var prognosis = this.prognosesRepository.FindForUpdate(prognosisId, vers);

            prognosis.ChangeStatusToEntered();
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{prognosisId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Prognoses.ChangeStatusToRemoved), IdParam = "prognosisId")]
        public void MakeDraft(int prognosisId, string version, ConfirmDO confirm)
        {
            this.AssertCanEdit(prognosisId);

            byte[] vers = System.Convert.FromBase64String(version);
            var prognosis = this.prognosesRepository.FindForUpdate(prognosisId, vers);

            prognosis.ChangeStatusToDeleted(confirm.Note);
            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{prognosisId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Prognoses.Delete), IdParam = "prognosisId")]
        public void DeletePrognosis(int prognosisId, string version)
        {
            this.AssertCanEdit(prognosisId);

            byte[] vers = System.Convert.FromBase64String(version);
            var prognosis = this.prognosesRepository.FindForUpdate(prognosisId, vers);

            this.prognosesRepository.Remove(prognosis);
            this.unitOfWork.Save();
        }
    }
}
