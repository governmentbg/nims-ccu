using Eumis.ApplicationServices.Services.FlatFinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Data.FlatFinancialCorrections.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.DataObjects;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/flatFinancialCorrections/{flatFinancialCorrectionId:int}/contractItems")]
    public class FlatFinancialCorrectionContractItemsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IFlatFinancialCorrectionService flatFinancialCorrectionService;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;
        private IContractsRepository contractsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;

        public FlatFinancialCorrectionContractItemsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IFlatFinancialCorrectionService flatFinancialCorrectionService,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository,
            IContractsRepository contractsRepository,
            IContractVersionsRepository contractVersionsRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.flatFinancialCorrectionService = flatFinancialCorrectionService;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
            this.contractsRepository = contractsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("contracts")]
        public IList<FlatFinancialCorrectionContractItemVO> GetContractsForFlatFinancialCorrection(int flatFinancialCorrectionId)
        {
            var programmeId = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId).ProgrammeId;

            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            return this.flatFinancialCorrectionsRepository.GetContractsForFlatFinancialCorrection(flatFinancialCorrectionId, programmeId);
        }

        [Route("")]
        public IList<FlatFinancialCorrectionContractItemVO> GetFlatFinancialCorrectionContractItems(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            return this.flatFinancialCorrectionsRepository.GetFlatFinancialCorrectionContractItems(flatFinancialCorrectionId);
        }

        [Route("{flatFinancialCorrectionLevelItemId:int}")]
        public FlatFinancialCorrectionItemDO GetFlatFinancialCorrectionContractItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            var flatFinancialCorrectionLevelItem = flatFinancialCorrection.FindFlatFinancialCorrectionItem<FlatFinancialCorrectionContractItem>(flatFinancialCorrectionLevelItemId);

            var contract = this.contractsRepository.Find(flatFinancialCorrectionLevelItem.ItemId);

            return new FlatFinancialCorrectionItemDO(flatFinancialCorrectionLevelItem, flatFinancialCorrection.Version, contract.ContractId, contract.Name, contract.RegNumber);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ContractItem.Create), IdParam = "flatFinancialCorrectionId")]
        public void CreateFlatFinancialCorrectionContractItem(int flatFinancialCorrectionId, string version, int[] contractIds)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            foreach (var contractId in contractIds)
            {
                this.flatFinancialCorrectionService.CreateFlatFinancialCorrectionItem<FlatFinancialCorrectionContractItem>(flatFinancialCorrectionId, vers, contractId);
            }
        }

        [HttpPut]
        [Route("{flatFinancialCorrectionLevelItemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ContractItem.Edit), IdParam = "flatFinancialCorrectionId", ChildIdParam = "flatFinancialCorrectionLevelItemId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateFlatFinancialCorrectionContractItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, FlatFinancialCorrectionItemDO flatFinancialCorrectionItem)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            this.flatFinancialCorrectionService.UpdateFlatFinancialCorrectionItem<FlatFinancialCorrectionContractItem>(
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
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ContractItem.Delete), IdParam = "flatFinancialCorrectionId", ChildIdParam = "flatFinancialCorrectionLevelItemId")]
        public void DeleteFlatFinancialCorrectionContractItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, string version)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.flatFinancialCorrectionService.DeleteFlatFinancialCorrectionItem<FlatFinancialCorrectionContractItem>(flatFinancialCorrectionId, vers, flatFinancialCorrectionLevelItemId);
        }

        [HttpPost]
        [Route("{flatFinancialCorrectionLevelItemId:int}/calculate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public FlatFinancialCorrectionItemDO CalculateFlatFinancialCorrectionContractItemAmounts(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, FlatFinancialCorrectionItemDO flatFinancialCorrectionItem)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            var contractBudget = this.contractVersionsRepository.GetLastVersion(flatFinancialCorrectionItem.ItemId)
                .GetDocument()
                .BFPContractDirectionsBudgetContract
                .BFPContractBudget
                .BFPContractProgrammeBudgetCollection;

            decimal euAmount = 0;
            decimal bgAmount = 0;
            foreach (var cb in contractBudget)
            {
                euAmount += cb.EUAmount;
                bgAmount += cb.NationalAmount;
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
