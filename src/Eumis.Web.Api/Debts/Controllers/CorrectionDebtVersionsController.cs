using Eumis.ApplicationServices.Services.CorrectionDebt;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Debts.DataObjects;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Debts.Controllers
{
    [RoutePrefix("api/correctionDebts/{correctionDebtId:int}/versions")]
    public class CorrectionDebtVersionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private ICorrectionDebtService correctionDebtService;
        private ICorrectionDebtVersionsRepository correctionDebtVersionsRepository;
        private ICorrectionDebtsRepository correctionDebtsRepository;
        private IUsersRepository usersRepository;

        public CorrectionDebtVersionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            ICorrectionDebtService correctionDebtService,
            ICorrectionDebtVersionsRepository correctionDebtVersionsRepository,
            ICorrectionDebtsRepository correctionDebtsRepository,
            IUsersRepository usersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.correctionDebtService = correctionDebtService;
            this.correctionDebtVersionsRepository = correctionDebtVersionsRepository;
            this.correctionDebtsRepository = correctionDebtsRepository;
            this.usersRepository = usersRepository;
        }

        [Route("")]
        public IList<CorrectionDebtVersionVO> GetCorrectionDebtVersions(int correctionDebtId)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.View, correctionDebtId);

            return this.correctionDebtVersionsRepository.GetCorrectionDebtVersions(correctionDebtId);
        }

        [Route("{correctionDebtVersionId:int}")]
        public CorrectionDebtVersionDO GetCorrectionDebtVersion(int correctionDebtId, int correctionDebtVersionId)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.View, correctionDebtId);

            var correctionDebtVersion = this.correctionDebtVersionsRepository.Find(correctionDebtVersionId);
            var flatFinancialCorrectionId = this.correctionDebtsRepository.GetFlatFinancialCorrectionId(correctionDebtId);
            var user = this.usersRepository.GetUserFullname(correctionDebtVersion.CreatedByUserId);

            return new CorrectionDebtVersionDO(correctionDebtVersion, user, flatFinancialCorrectionId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CorrectionDebts.Edit.Versions.Create))]
        public object CreateCorrectionDebtVersion(int correctionDebtId)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.Edit, correctionDebtId);

            var correctionDebtVersion = this.correctionDebtService.CreateCorrectionDebtVersion(correctionDebtId);

            return new { CorrectionDebtVersionId = correctionDebtVersion.CorrectionDebtVersionId };
        }

        [HttpPut]
        [Route("{correctionDebtVersionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CorrectionDebts.Edit.Versions.Edit), IdParam = "correctionDebtVersionId")]
        public void UpdateCorrectionDebtVersion(int correctionDebtId, int correctionDebtVersionId, CorrectionDebtVersionDO correctionDebtVersion)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.Edit, correctionDebtId);

            this.correctionDebtService.UpdateCorrectionDebtVersion(
                correctionDebtVersionId,
                correctionDebtVersion.Version,
                correctionDebtVersion.DebtEuAmount,
                correctionDebtVersion.DebtBgAmount,
                correctionDebtVersion.CertEuAmount,
                correctionDebtVersion.CertBgAmount,
                correctionDebtVersion.ReimbursedEuAmount,
                correctionDebtVersion.ReimbursedBgAmount,
                correctionDebtVersion.CreateNotes);
        }

        [HttpDelete]
        [Route("{correctionDebtVersionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CorrectionDebts.Edit.Versions.Delete), IdParam = "correctionDebtVersionId")]
        public void DeleteCorrectionDebtVersion(int correctionDebtId, int correctionDebtVersionId, string version)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.Edit, correctionDebtId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.correctionDebtService.DeleteCorrectionDebtVersion(correctionDebtVersionId, vers);
        }

        [HttpPost]
        [Route("{correctionDebtVersionId:int}/changeStatusToActual")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CorrectionDebts.Edit.Versions.ChangeStatusToActual), IdParam = "correctionDebtVersionId")]
        public void ChangeCorrectionDebtVersionStatusToActual(int correctionDebtId, int correctionDebtVersionId, string version)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.Edit, correctionDebtId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.correctionDebtService.ChangeCorrectionDebtVersionStatusToActual(correctionDebtVersionId, vers);
        }

        [HttpPost]
        [Route("{correctionDebtVersionId:int}/canChangeStatusToActual")]
        public ErrorsDO CanChangeCorrectionDebtVersionStatusToActual(int correctionDebtId, int correctionDebtVersionId)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.Edit, correctionDebtId);

            var version = this.correctionDebtVersionsRepository.Find(correctionDebtVersionId);
            var errors = version.CanChangeStatusToActual();

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateCorrectionDebtVersion(int correctionDebtId)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.Edit, correctionDebtId);

            var errors = this.correctionDebtService.CanCreateCorrectionDebtVersion(correctionDebtId);

            return new ErrorsDO(errors);
        }
    }
}
