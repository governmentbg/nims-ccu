using Eumis.ApplicationServices.Services.CorrectionDebt;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Domain.Debts.DataObjects;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Debts.Controllers
{
    [RoutePrefix("api/correctionDebts")]
    public class CorrectionDebtsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICorrectionDebtService correctionDebtService;
        private ICorrectionDebtsRepository correctionDebtsRepository;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;
        private IRelationsRepository relationsRepository;

        public CorrectionDebtsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICorrectionDebtService correctionDebtService,
            ICorrectionDebtsRepository correctionDebtsRepository,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.correctionDebtService = correctionDebtService;
            this.correctionDebtsRepository = correctionDebtsRepository;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<CorrectionDebtVO> GetCorrectionDebts()
        {
            this.authorizer.AssertCanDo(CorrectionDebtListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.correctionDebtsRepository.GetCorrectionDebts(programmeIds);
        }

        [Route("report")]
        public IList<CorrectionDebtReportVO> GetCorrectionDebtReport()
        {
            this.authorizer.AssertCanDo(CorrectionDebtListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.correctionDebtsRepository.GetCorrectionDebtReport(programmeIds);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/correctionDebts")]
        public IList<CorrectionDebtVO> GetContractDebtsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.correctionDebtsRepository.GetCorrectionDebtsForProjectDossier(contractId);
        }

        [Route("{correctionDebtId:int}")]
        public CorrectionDebtDO GetCorrectionDebt(int correctionDebtId)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.View, correctionDebtId);

            var correctionDebt = this.correctionDebtsRepository.Find(correctionDebtId);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(correctionDebt.FlatFinancialCorrectionId);

            return new CorrectionDebtDO(correctionDebt, flatFinancialCorrection);
        }

        [Route("{correctionDebtId:int}/info")]
        public CorrectionDebtInfoDO GetCorrectionDebtInfo(int correctionDebtId)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.View, correctionDebtId);

            var correctionDebt = this.correctionDebtsRepository.Find(correctionDebtId);

            return new CorrectionDebtInfoDO(correctionDebt);
        }

        [HttpGet]
        [Route("new")]
        public NewCorrectionDebtDO NewCorrectionDebt(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(CorrectionDebtListActions.Create);

            return new NewCorrectionDebtDO()
            {
                RegDate = DateTime.Now,
                FlatFinancialCorrectionId = flatFinancialCorrectionId,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CorrectionDebts.Create))]
        public object CreateCorrectionDebt(NewCorrectionDebtDO newCorrectionDebt)
        {
            this.authorizer.AssertCanDo(CorrectionDebtListActions.Create);

            var correctionDebt = this.correctionDebtService.CreateContractDebt(
                newCorrectionDebt.FlatFinancialCorrectionId.Value,
                newCorrectionDebt.RegDate.Value);

            return new { CorrectionDebtId = correctionDebt.CorrectionDebtId };
        }

        [HttpPost]
        [Route("{correctionDebtId:int}/cancel")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CorrectionDebts.Edit.Cancel))]
        public void CancelCorrectionDebt(int correctionDebtId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(CorrectionDebtListActions.Create);

            byte[] vers = System.Convert.FromBase64String(version);
            var correctionDebt = this.correctionDebtsRepository.FindForUpdate(correctionDebtId, vers);

            correctionDebt.MakeDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{correctionDebtId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CorrectionDebts.Edit.BasicData), IdParam = "correctionDebtId")]
        public void UpdateCorrectionDebt(int correctionDebtId, CorrectionDebtDO correctionDebtDO)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.Edit, correctionDebtId);

            var correctionDebt = this.correctionDebtsRepository.FindForUpdate(correctionDebtId, correctionDebtDO.Version);

            correctionDebt.UpdateAttributes(
                correctionDebtDO.RegDate.Value,
                correctionDebtDO.Comment);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{correctionDebtId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CorrectionDebts.Edit.Delete), IdParam = "correctionDebtId")]
        public void DeleteContractDebt(int correctionDebtId, string version)
        {
            this.authorizer.AssertCanDo(CorrectionDebtActions.Edit, correctionDebtId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.correctionDebtService.DeleteDebt(correctionDebtId, vers);
        }
    }
}
