using Eumis.ApplicationServices.Services.FlatFinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Data.FlatFinancialCorrections.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.DataObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/flatFinancialCorrections")]
    public class FlatFinancialCorrectionsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IFlatFinancialCorrectionService flatFinancialCorrectionService;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;
        private IUsersRepository usersRepository;

        public FlatFinancialCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IFlatFinancialCorrectionService flatFinancialCorrectionService,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository,
            IUsersRepository usersRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.flatFinancialCorrectionService = flatFinancialCorrectionService;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
            this.usersRepository = usersRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<FlatFinancialCorrectionVO> GetFlatFinancialCorrections()
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            this.authorizer.AssertCanDo(FlatFinancialCorrectionListActions.Search);

            return this.flatFinancialCorrectionsRepository.GetFlatFinancialCorrections(programmeIds);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/flatFinancialCorrections")]
        public IList<FlatFinancialCorrectionVO> GetFlatFinancialCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.flatFinancialCorrectionsRepository.GetFlatFinancialCorrectionsForProjectDossier(contractId);
        }

        [Route("~/api/contracts/{contractId:int}/flatFinancialCorrections")]
        public IList<FlatFinancialCorrectionVO> GetFlatFinancialCorrectionsForProjectDossier(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.flatFinancialCorrectionsRepository.GetFlatFinancialCorrectionsForProjectDossier(contractId);
        }

        [Route("{flatFinancialCorrectionId:int}")]
        public FlatFinancialCorrectionDO GetFlatFinancialCorrection(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            return new FlatFinancialCorrectionDO(flatFinancialCorrection);
        }

        [Route("{flatFinancialCorrectionId:int}/info")]
        public FlatFinancialCorrectionInfoDO GetFlatFinancialCorrectionInfo(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            return new FlatFinancialCorrectionInfoDO(flatFinancialCorrection);
        }

        [Route("{flatFinancialCorrectionId:int}/correctionDebtInfo")]
        public FlatFinancialCorrectionInfoDO GetCorrectionDebtFlatFinancialCorrectionInfo(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(CorrectionDebtListActions.Create);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            return new FlatFinancialCorrectionInfoDO(flatFinancialCorrection);
        }

        [HttpGet]
        [Route("new")]
        public FlatFinancialCorrectionDO NewFlatFinancialCorrection()
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionListActions.Create);

            return new FlatFinancialCorrectionDO()
                {
                    Status = FlatFinancialCorrectionStatus.Draft,
                };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Create))]
        public object CreateFlatFinancialCorrection(FlatFinancialCorrectionDO flatFinancialCorrection)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionListActions.Create);

            var newFlatFinancialCorrection = this.flatFinancialCorrectionService.CreateFlatFinancialCorrection(
                flatFinancialCorrection.ProgrammeId.Value,
                flatFinancialCorrection.Name,
                flatFinancialCorrection.Level.Value,
                flatFinancialCorrection.Type.Value,
                flatFinancialCorrection.ImpositionDate,
                flatFinancialCorrection.ImpositionNumber,
                flatFinancialCorrection.Description,
                flatFinancialCorrection.File != null ? flatFinancialCorrection.File.Key : (Guid?)null,
                flatFinancialCorrection.ContractId,
                this.accessContext.UserId);

            return new { FlatFinancialCorrectionId = newFlatFinancialCorrection.FlatFinancialCorrectionId };
        }

        [HttpPut]
        [Route("{flatFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.BasicData), IdParam = "flatFinancialCorrectionId")]
        public void UpdateFlatFinancialCorrection(int flatFinancialCorrectionId, FlatFinancialCorrectionDO flatFinancialCorrection)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            this.flatFinancialCorrectionService.UpdateFlatFinancialCorrection(
                flatFinancialCorrectionId,
                flatFinancialCorrection.Version,
                flatFinancialCorrection.Name,
                flatFinancialCorrection.Type.Value,
                flatFinancialCorrection.ImpositionDate,
                flatFinancialCorrection.ImpositionNumber,
                flatFinancialCorrection.Description,
                flatFinancialCorrection.File != null ? flatFinancialCorrection.File.Key : (Guid?)null);
        }

        [HttpDelete]
        [Route("{flatFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Delete), IdParam = "flatFinancialCorrectionId")]
        public void DeleteFlatFinancialCorrection(int flatFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.flatFinancialCorrectionService.DeleteFlatFinancialCorrection(flatFinancialCorrectionId, vers);
        }

        [HttpPost]
        [Route("{flatFinancialCorrectionId:int}/changeStatusToActual")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.ChangeStatusToActual), IdParam = "flatFinancialCorrectionId")]
        public void ChangeFlatFinancialCorrectionStatusToActual(int flatFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.flatFinancialCorrectionService.ChangeFlatFinancialCorrectionStatus(flatFinancialCorrectionId, vers, FlatFinancialCorrectionStatus.Actual);
        }

        [HttpPost]
        [Route("{flatFinancialCorrectionId:int}/canChangeStatusToActual")]
        public ErrorsDO CanChangeFlatFinancialCorrectionStatusToActual(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            var errors = this.flatFinancialCorrectionService.CanChangeFlatFinancialCorrectionStatusToActual(flatFinancialCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{flatFinancialCorrectionId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.ChangeStatusToActual), IdParam = "flatFinancialCorrectionId")]
        public void ChangeFlatFinancialCorrectionStatusToDraft(int flatFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.flatFinancialCorrectionService.ChangeFlatFinancialCorrectionStatus(flatFinancialCorrectionId, vers, FlatFinancialCorrectionStatus.Draft);
        }

        [HttpPost]
        [Route("{flatFinancialCorrectionId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeFlatFinancialCorrectionStatusToDraft(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            var errors = this.flatFinancialCorrectionService.CanChangeFlatFinancialCorrectionStatusToDraft(flatFinancialCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{flatFinancialCorrectionId:int}/canDelete")]
        public ErrorsDO CanDeleteFlatFinancialCorrection(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            var errors = this.flatFinancialCorrectionService.CanDeleteFlatFinancialCorrection(flatFinancialCorrectionId);

            return new ErrorsDO(errors);
        }
    }
}
