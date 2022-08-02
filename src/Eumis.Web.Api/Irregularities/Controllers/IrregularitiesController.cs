using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.ApplicationServices.Services.Irregularity;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Counters;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Irregularities.DataObjects;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/irregularities")]
    public class IrregularitiesController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICountersRepository countersRepository;
        private IIrregularitiesRepository irregularitiesRepository;
        private IIrregularityService irregularityService;

        public IrregularitiesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            IIrregularitiesRepository irregularitiesRepository,
            IIrregularityService irregularityService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.countersRepository = countersRepository;
            this.irregularitiesRepository = irregularitiesRepository;
            this.irregularityService = irregularityService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<IrregularityVO> GetIrregularities()
        {
            this.authorizer.AssertCanDo(IrregularityListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            return this.irregularitiesRepository.GetIrregularities(programmeIds, this.accessContext.UserId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/irregularities")]
        public IList<IrregularityVO> GetIrregularitiesForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.irregularitiesRepository.GetIrregularitiesForProjectDossier(contractId);
        }

        [Route("~/api/financialCorrections/{financialCorrectionId:int}/irregularities")]
        public IList<IrregularityVO> GetFinancialCorrectionIrregularities(int financialCorrectionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.View, financialCorrectionId);

            return this.irregularitiesRepository.GetFinancialCorrectionIrregularities(financialCorrectionId);
        }

        [Route("quartersReport")]
        public IrrByQuarterReportVO GetQuartersReport(Year year, Quarter quarter, int programmeId)
        {
            this.authorizer.AssertCanDo(IrregularityListActions.Search);

            var programmeIds = System.Array.Empty<int>();
            if (!programmeIds.Contains(programmeId))
            {
                throw new InvalidOperationException("Missing permissions for the selected programme.");
            }

            return this.irregularitiesRepository.GetIrrByQuarterReport(year, quarter, programmeId);
        }

        [HttpGet]
        [Route("new")]
        public IrregularityCreationDO NewIrregularity()
        {
            this.authorizer.AssertCanDo(IrregularityListActions.Create);

            return new IrregularityCreationDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Create))]
        public object CreateIrregularity(IrregularityCreationDO creationData)
        {
            this.authorizer.AssertCanDo(IrregularityListActions.Create);

            var irregularity = this.irregularityService.CreateIrregularity(
                this.accessContext.UserId,
                creationData.IrregularitySignalId.Value,
                creationData.IrregularityDateFrom.Value,
                creationData.IrregularityDateTo,
                creationData.ReportYear.Value,
                creationData.ReportQuarter.Value,
                creationData.ShouldReportToOlaf.Value,
                creationData.ReasonNotReportingToOlaf,
                creationData.SanctionProcedureType.Value,
                creationData.SanctionProcedureKind,
                creationData.SanctionProcedureStartDate,
                creationData.SanctionProcedureExpectedEndDate,
                creationData.SanctionProcedureEndDate,
                creationData.SanctionProcedureStatus,
                creationData.SanctionCategoryId,
                creationData.SanctionTypeId,
                creationData.Fines);

            return new { IrregularityId = irregularity.IrregularityId };
        }

        [Route("{irregularityId:int}/info")]
        public IrregularityInfoVO GetIrregularityInfo(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.View, irregularityId);

            return this.irregularitiesRepository.GetInfo(irregularityId);
        }

        [Route("{irregularityId:int}")]
        public IrregularityDataVO GetIrregularity(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.View, irregularityId);

            return this.irregularitiesRepository.GetData(irregularityId);
        }

        [HttpPut]
        [Route("{irregularityId:int}/updatePartial")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Edit.SignalData), IdParam = "irregularityId")]
        public void UpdateIrregularityBasicData(int irregularityId, IrregularityDataDO irregularityDO)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, irregularityId);

            if (this.irregularityService.CanUpdatePartial(irregularityDO.ProgrammeId, irregularityId, irregularityDO.RegNumber).Any())
            {
                throw new InvalidOperationException("Cannot update signal");
            }

            var irregularity = this.irregularitiesRepository.FindForUpdate(irregularityId, irregularityDO.Version);

            irregularity.UpdateBasicData(irregularityDO.RegNumber);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{irregularityId:int}/canUpdatePartial")]
        public ErrorsDO CanUpdateIrregularityBasicData(int irregularityId, IrregularityDataDO irregularityDO)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, irregularityId);

            var errorList = this.irregularityService.CanUpdatePartial(irregularityDO.ProgrammeId, irregularityId, irregularityDO.RegNumber);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{irregularityId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.ChangeStatusToRemoved), IdParam = "irregularityId")]
        public void MakeRemoved(int irregularityId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            byte[] vers = System.Convert.FromBase64String(version);
            var irregularity = this.irregularitiesRepository.FindForUpdate(irregularityId, vers);

            irregularity.ChangeStatusToRemoved(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{irregularityId:int}/canDelete")]
        public ErrorsDO CanDeleteIrregularity(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            var errors = this.irregularityService.CanDeleteIrregularity(irregularityId);

            return new ErrorsDO(errors);
        }

        [HttpDelete]
        [Route("{irregularityId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Delete), IdParam = "irregularityId")]
        public void DeleteIrregularity(int irregularityId, string version)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            this.irregularityService.DeleteIrregularity(irregularityId, System.Convert.FromBase64String(version));
        }
    }
}
