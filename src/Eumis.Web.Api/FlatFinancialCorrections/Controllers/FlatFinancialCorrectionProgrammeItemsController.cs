using Eumis.ApplicationServices.Services.FlatFinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.DataObjects;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/flatFinancialCorrections/{flatFinancialCorrectionId:int}/programmeItems")]
    public class FlatFinancialCorrectionProgrammeItemsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IFlatFinancialCorrectionService flatFinancialCorrectionService;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;
        private IProgrammesRepository programmesRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;

        public FlatFinancialCorrectionProgrammeItemsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IFlatFinancialCorrectionService flatFinancialCorrectionService,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository,
            IProgrammesRepository programmesRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.flatFinancialCorrectionService = flatFinancialCorrectionService;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
            this.programmesRepository = programmesRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("")]
        public FlatFinancialCorrectionItemDO GetFlatFinancialCorrectionProgrammeItem(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            var flatFinancialCorrectionLevelProgrammeItem = flatFinancialCorrection.FlatFinancialCorrectionLevelItems.Single();

            var flatFinancialCorrectionLevelItem = flatFinancialCorrection.FindFlatFinancialCorrectionItem<FlatFinancialCorrectionProgrammeItem>(flatFinancialCorrectionLevelProgrammeItem.FlatFinancialCorrectionLevelItemId);

            var programme = this.programmesRepository.Find(flatFinancialCorrectionLevelItem.ItemId);

            return new FlatFinancialCorrectionItemDO(flatFinancialCorrectionLevelItem, flatFinancialCorrection.Version, programme.MapNodeId, programme.Name, programme.Code);
        }

        [HttpPut]
        [Route("{flatFinancialCorrectionLevelItemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ProgrammeItem.Edit), IdParam = "flatFinancialCorrectionId", ChildIdParam = "flatFinancialCorrectionLevelItemId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateFlatFinancialCorrectionProgrammeItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, FlatFinancialCorrectionItemDO flatFinancialCorrectionItem)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            this.flatFinancialCorrectionService.UpdateFlatFinancialCorrectionItem<FlatFinancialCorrectionProgrammeItem>(
                flatFinancialCorrectionId,
                flatFinancialCorrectionItem.Version,
                flatFinancialCorrectionItem.FlatFinancialCorrectionLevelItemId,
                flatFinancialCorrectionItem.Percent,
                flatFinancialCorrectionItem.EuAmount,
                flatFinancialCorrectionItem.BgAmount,
                flatFinancialCorrectionItem.TotalAmount);
        }

        [HttpPost]
        [Route("{flatFinancialCorrectionLevelItemId:int}/calculate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public FlatFinancialCorrectionItemDO CalculateFlatFinancialCorrectionProgrammeItemAmounts(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, FlatFinancialCorrectionItemDO flatFinancialCorrectionItem)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            var programmeBudgets = this.programmesRepository.GetProgrammeBudgets(flatFinancialCorrectionItem.ItemId);

            decimal euAmount = 0;
            decimal bgAmount = 0;
            foreach (var pb in programmeBudgets)
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
