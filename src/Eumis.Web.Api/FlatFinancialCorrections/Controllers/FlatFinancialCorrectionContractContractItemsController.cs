using Eumis.ApplicationServices.Services.FlatFinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
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
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/flatFinancialCorrections/{flatFinancialCorrectionId:int}/contractContractItems")]
    public class FlatFinancialCorrectionContractContractItemsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IFlatFinancialCorrectionService flatFinancialCorrectionService;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;
        private IContractsRepository contractsRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;

        public FlatFinancialCorrectionContractContractItemsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IFlatFinancialCorrectionService flatFinancialCorrectionService,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository,
            IContractsRepository contractsRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.flatFinancialCorrectionService = flatFinancialCorrectionService;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
            this.contractsRepository = contractsRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("contractContracts")]
        public IList<FlatFinancialCorrectionContractContractItemVO> GetContractContractsForFlatFinancialCorrection(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            return this.flatFinancialCorrectionsRepository.GetContractContractsForFlatFinancialCorrection(
                flatFinancialCorrectionId,
                flatFinancialCorrection.ContractId.Value,
                flatFinancialCorrection.ProgrammeId);
        }

        [Route("")]
        public IList<FlatFinancialCorrectionContractContractItemVO> GetFlatFinancialCorrectionContractContractItems(int flatFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            return this.flatFinancialCorrectionsRepository.GetFlatFinancialCorrectionContractContractItems(flatFinancialCorrectionId);
        }

        [Route("{flatFinancialCorrectionLevelItemId:int}")]
        public FlatFinancialCorrectionItemDO GetFlatFinancialCorrectionContractContractItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, flatFinancialCorrectionId);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            var flatFinancialCorrectionLevelItem = flatFinancialCorrection.FindFlatFinancialCorrectionItem<FlatFinancialCorrectionContractContractItem>(flatFinancialCorrectionLevelItemId);

            var contract = this.contractsRepository.Find(flatFinancialCorrection.ContractId.Value);
            var contractContract = contract.ContractContracts.Where(t => t.ContractContractId == flatFinancialCorrectionLevelItem.ItemId).Single();
            var contractContractor = contract.ContractContractors.Where(t => t.ContractContractorId == contractContract.ContractContractorId).Single();
            var contractContractorName = string.Format("{0} ({1}: {2})", contractContractor.Name, contractContractor.UinType.GetEnumDescription(), contractContractor.Uin);

            return new FlatFinancialCorrectionItemDO(flatFinancialCorrectionLevelItem, flatFinancialCorrection.Version, contractContract.ContractContractId, contractContractorName, contractContract.Number);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ContractContractItem.Create), IdParam = "flatFinancialCorrectionId")]
        public void CreateFlatFinancialCorrectionContractContractItem(int flatFinancialCorrectionId, string version, int[] contractContractIds)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            foreach (var contractContractId in contractContractIds)
            {
                this.flatFinancialCorrectionService.CreateFlatFinancialCorrectionItem<FlatFinancialCorrectionContractContractItem>(flatFinancialCorrectionId, vers, contractContractId);
            }
        }

        [HttpPut]
        [Route("{flatFinancialCorrectionLevelItemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ContractContractItem.Edit), IdParam = "flatFinancialCorrectionId", ChildIdParam = "flatFinancialCorrectionLevelItemId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateFlatFinancialCorrectionContractContractItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, FlatFinancialCorrectionItemDO flatFinancialCorrectionItem)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            this.flatFinancialCorrectionService.UpdateFlatFinancialCorrectionItem<FlatFinancialCorrectionContractContractItem>(
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
        [ActionLog(Action = typeof(ActionLogGroups.FlatFinancialCorrections.Edit.ContractContractItem.Delete), IdParam = "flatFinancialCorrectionId", ChildIdParam = "flatFinancialCorrectionLevelItemId")]
        public void DeleteFlatFinancialCorrectionContractContractItem(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, string version)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.flatFinancialCorrectionService.DeleteFlatFinancialCorrectionItem<FlatFinancialCorrectionContractContractItem>(flatFinancialCorrectionId, vers, flatFinancialCorrectionLevelItemId);
        }

        [HttpPost]
        [Route("{flatFinancialCorrectionLevelItemId:int}/calculate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public FlatFinancialCorrectionItemDO CalculateFlatFinancialCorrectionContractContractItemAmounts(int flatFinancialCorrectionId, int flatFinancialCorrectionLevelItemId, FlatFinancialCorrectionItemDO flatFinancialCorrectionItem)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.Edit, flatFinancialCorrectionId);

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);
            var contractContract = this.contractsRepository.Find(flatFinancialCorrection.ContractId.Value).ContractContracts.Where(t => t.ContractContractId == flatFinancialCorrectionItem.ItemId).Single();

            decimal euAmount = contractContract.TotalFundedValue;
            decimal bgAmount = 0;

            var correctedEuAmount = Eumis.Domain.Core.Calculator.RoundBy2((euAmount * flatFinancialCorrectionItem.Percent.Value) / 100);

            return new FlatFinancialCorrectionItemDO()
            {
                EuAmount = correctedEuAmount,
                BgAmount = bgAmount,
                TotalAmount = correctedEuAmount + bgAmount,
            };
        }
    }
}
