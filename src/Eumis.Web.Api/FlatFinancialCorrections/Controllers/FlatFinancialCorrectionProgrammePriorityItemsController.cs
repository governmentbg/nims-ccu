using Eumis.ApplicationServices.Services.FlatFinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Data.FlatFinancialCorrections.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.DataObjects;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/flatFinancialCorrections/{flatFinancialCorrectionId:int}/programmePriorityItems")]
    public class FlatFinancialCorrectionProgrammePriorityItemsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IFlatFinancialCorrectionService flatFinancialCorrectionService;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;

        public FlatFinancialCorrectionProgrammePriorityItemsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IFlatFinancialCorrectionService flatFinancialCorrectionService,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.flatFinancialCorrectionService = flatFinancialCorrectionService;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("programmePriorities")]
        public IList<FlatFinancialCorrectionProgrammePriorityItemVO> GetProgrammePrioritysForFlatFinancialCorrection(int flatFinancialCorrectionId)
        {
            var programmeId = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId).ProgrammeId;

            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            return this.flatFinancialCorrectionsRepository.GetProgrammePrioritiesForFlatFinancialCorrection(flatFinancialCorrectionId, programmeId);
        }

        [Route("")]
        public IList<FlatFinancialCorrectionProgrammePriorityItemVO> GetFlatFinancialCorrectionProgrammePriorityItems(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            return this.flatFinancialCorrectionsRepository.GetFlatFinancialCorrectionProgrammePriorityItems(flatFinancialCorrectionId);
        }

        [Route("{flatFinancialCorrectionLevelItemId:int}")]
        public FlatFinancialCorrectionItemDO GetFlatFinancialCorrectionProgrammePriorityItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            var flatFinancialCorrectionLevelItem = flatFinancialCorrection.FindFlatFinancialCorrectionItem<FlatFinancialCorrectionProgrammePriorityItem>(flatFinancialCorrectionLevelItemId);

            var programmePriority = this.programmePrioritiesRepository.Find(flatFinancialCorrectionLevelItem.ItemId);

            return new FlatFinancialCorrectionItemDO(flatFinancialCorrectionLevelItem, flatFinancialCorrection.Version, programmePriority.MapNodeId, programmePriority.Name, programmePriority.Code);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ProgrammePriorityItem.Create), IdParam = "flatFinancialCorrectionId")]
        public void CreateFlatFinancialCorrectionProgrammePriorityItem(int flatFinancialCorrectionId, string version, int[] programmePriorityIds)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            foreach (var programmePriorityId in programmePriorityIds)
            {
                this.flatFinancialCorrectionService.CreateFlatFinancialCorrectionItem<FlatFinancialCorrectionProgrammePriorityItem>(flatFinancialCorrectionId, vers, programmePriorityId);
            }
        }

        [HttpPut]
        [Route("{flatFinancialCorrectionLevelItemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ProgrammePriorityItem.Edit), IdParam = "flatFinancialCorrectionId", ChildIdParam = "flatFinancialCorrectionLevelItemId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateFlatFinancialCorrectionProgrammePriorityItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, FlatFinancialCorrectionItemDO flatFinancialCorrectionItem)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            this.flatFinancialCorrectionService.UpdateFlatFinancialCorrectionItem<FlatFinancialCorrectionProgrammePriorityItem>(
                flatFinancialCorrectionId,
                flatFinancialCorrectionItem.Version,
                flatFinancialCorrectionItem.FlatFinancialCorrectionLevelItemId,
                flatFinancialCorrectionItem.Percent,
                flatFinancialCorrectionItem.EuAmount,
                flatFinancialCorrectionItem.BgAmount,
                flatFinancialCorrectionItem.TotalAmount);
        }

        [HttpDelete]
        [Route("{flatFinancialCorrectionLevelItemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ProgrammePriorityItem.Delete), IdParam = "flatFinancialCorrectionId", ChildIdParam = "flatFinancialCorrectionLevelItemId")]
        public void DeleteFlatFinancialCorrectionProgrammePriorityItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, string version)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.flatFinancialCorrectionService.DeleteFlatFinancialCorrectionItem<FlatFinancialCorrectionProgrammePriorityItem>(flatFinancialCorrectionId, vers, flatFinancialCorrectionLevelItemId);
        }

        [HttpPost]
        [Route("{flatFinancialCorrectionLevelItemId:int}/calculate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public FlatFinancialCorrectionItemDO CalculateFlatFinancialCorrectionProgrammePriorityItemAmounts(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, FlatFinancialCorrectionItemDO flatFinancialCorrectionItem)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            var programmePriorityBudgets = this.programmePrioritiesRepository.GetProgrammePriorityBudgets(flatFinancialCorrectionItem.ItemId);

            decimal euAmount = 0;
            decimal bgAmount = 0;
            foreach (var pb in programmePriorityBudgets)
            {
                euAmount += pb.Budgets.EuAmount ?? 0;
                bgAmount += pb.Budgets.BgAmount ?? 0;
            }

            var correctedEuAmount = Eumis.Domain.Core.Calculator.RoundBy2((euAmount * flatFinancialCorrectionItem.Percent.Value) / 100);
            var correctedBgAmount = Eumis.Domain.Core.Calculator.RoundBy2((bgAmount * flatFinancialCorrectionItem.Percent.Value) / 100);

            return new FlatFinancialCorrectionItemDO()
            {
                EuAmount = correctedEuAmount,
                BgAmount = correctedBgAmount,
                TotalAmount = correctedEuAmount + correctedBgAmount,
            };
        }
    }
}
