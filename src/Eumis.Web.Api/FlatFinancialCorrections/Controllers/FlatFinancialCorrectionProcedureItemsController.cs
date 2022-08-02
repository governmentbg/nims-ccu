using Eumis.ApplicationServices.Services.FlatFinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Data.FlatFinancialCorrections.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.DataObjects;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/flatFinancialCorrections/{flatFinancialCorrectionId:int}/procedureItems")]
    public class FlatFinancialCorrectionProcedureItemsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IFlatFinancialCorrectionService flatFinancialCorrectionService;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;
        private IProceduresRepository proceduresRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;

        public FlatFinancialCorrectionProcedureItemsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IFlatFinancialCorrectionService flatFinancialCorrectionService,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository,
            IProceduresRepository proceduresRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.flatFinancialCorrectionService = flatFinancialCorrectionService;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
            this.proceduresRepository = proceduresRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("procedures")]
        public IList<FlatFinancialCorrectionProcedureItemVO> GetProceduresForFlatFinancialCorrection(int flatFinancialCorrectionId)
        {
            var programmeId = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId).ProgrammeId;

            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            return this.flatFinancialCorrectionsRepository.GetProceduresForFlatFinancialCorrection(flatFinancialCorrectionId, programmeId);
        }

        [Route("")]
        public IList<FlatFinancialCorrectionProcedureItemVO> GetFlatFinancialCorrectionProcedureItems(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            return this.flatFinancialCorrectionsRepository.GetFlatFinancialCorrectionProcedureItems(flatFinancialCorrectionId);
        }

        [Route("{flatFinancialCorrectionLevelItemId:int}")]
        public FlatFinancialCorrectionItemDO GetFlatFinancialCorrectionProcedureItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            var flatFinancialCorrectionLevelItem = flatFinancialCorrection.FindFlatFinancialCorrectionItem<FlatFinancialCorrectionProcedureItem>(flatFinancialCorrectionLevelItemId);

            var procedure = this.proceduresRepository.Find(flatFinancialCorrectionLevelItem.ItemId);

            return new FlatFinancialCorrectionItemDO(flatFinancialCorrectionLevelItem, flatFinancialCorrection.Version, procedure.ProcedureId, procedure.Name, procedure.Code);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ProcedureItem.Create), IdParam = "flatFinancialCorrectionId")]
        public void CreateFlatFinancialCorrectionProcedureItem(int flatFinancialCorrectionId, string version, int[] procedureIds)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            foreach (var procedureId in procedureIds)
            {
                this.flatFinancialCorrectionService.CreateFlatFinancialCorrectionItem<FlatFinancialCorrectionProcedureItem>(flatFinancialCorrectionId, vers, procedureId);
            }
        }

        [HttpPut]
        [Route("{flatFinancialCorrectionLevelItemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ProcedureItem.Edit), IdParam = "flatFinancialCorrectionId", ChildIdParam = "flatFinancialCorrectionLevelItemId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateFlatFinancialCorrectionProcedureItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, FlatFinancialCorrectionItemDO flatFinancialCorrectionItem)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            this.flatFinancialCorrectionService.UpdateFlatFinancialCorrectionItem<FlatFinancialCorrectionProcedureItem>(
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
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ProcedureItem.Delete), IdParam = "flatFinancialCorrectionId", ChildIdParam = "flatFinancialCorrectionLevelItemId")]
        public void DeleteFlatFinancialCorrectionProcedureItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, string version)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.flatFinancialCorrectionService.DeleteFlatFinancialCorrectionItem<FlatFinancialCorrectionProcedureItem>(flatFinancialCorrectionId, vers, flatFinancialCorrectionLevelItemId);
        }

        [HttpPost]
        [Route("{flatFinancialCorrectionLevelItemId:int}/calculate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public FlatFinancialCorrectionItemDO CalculateFlatFinancialCorrectionProcedureItemAmounts(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, FlatFinancialCorrectionItemDO flatFinancialCorrectionItem)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            var procedureShares = this.proceduresRepository.GetProcedureShares(flatFinancialCorrectionItem.ItemId);

            decimal euAmount = 0;
            decimal bgAmount = 0;
            foreach (var ps in procedureShares)
            {
                euAmount += ps.EuAmount;
                bgAmount += ps.BgAmount;
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
