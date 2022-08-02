using Eumis.ApplicationServices.Services.ContractDebt;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.Debts.ViewObjects;
using Eumis.Domain.Debts.DataObjects;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace Eumis.Web.Api.Debts.Controllers
{
    [RoutePrefix("api/contractDebts")]
    public class ContractDebtsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IContractDebtService contractDebtService;
        private IContractDebtsRepository contractDebtsRepository;
        private IContractsRepository contractsRepository;
        private ICertReportsRepository certReportsRepository;

        public ContractDebtsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IContractDebtService contractDebtService,
            IContractDebtsRepository contractDebtsRepository,
            IContractsRepository contractsRepository,
            ICertReportsRepository certReportsRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.contractDebtService = contractDebtService;
            this.contractDebtsRepository = contractDebtsRepository;
            this.contractsRepository = contractsRepository;
            this.certReportsRepository = certReportsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractDebtVO> GetContractDebts()
        {
            this.authorizer.AssertCanDo(ContractDebtListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractDebtsRepository.GetContractDebts(programmeIds, this.accessContext.UserId);
        }

        [Route("report")]
        public IList<ContractDebtReportVO> GetContractDebts(Month month, Year year, int? programmeId = null)
        {
            this.authorizer.AssertCanDo(ContractDebtListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);
            if (programmeId != null)
            {
                if (Array.IndexOf(programmeIds, programmeId) == -1)
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }

                programmeIds = new int[] { programmeId.Value };
            }

            var dateFrom = new DateTime((int)year, (int)month, 1);
            var dateToMonth = month == Month.December ? Month.January : month + 1;
            var dateToYear = month == Month.December ? (int)year + 1 : (int)year;
            var dateTo = new DateTime(dateToYear, (int)dateToMonth, 1).AddDays(-1);

            return this.contractDebtsRepository.GetContractDebtReport(programmeIds, dateFrom, dateTo);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/debts")]
        public IList<ContractDebtVO> GetContractDebtsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractDebtsRepository.GetContractDebtsForProjectDossier(contractId);
        }

        [Route("~/api/contracts/{contractId:int}/debts")]
        public IList<ContractDebtVO> GetContractDebtsForProjectDossier(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.contractDebtsRepository.GetContractDebtsForProjectDossier(contractId);
        }

        [Route("~/api/irregularities/{irregularityId:int}/contractDebts")]
        public IList<ContractDebtVO> GetIrregularityContractDebts(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.View, irregularityId);

            return this.contractDebtsRepository.GetIrregularityContractDebts(irregularityId);
        }

        [Route("~/api/financialCorrections/{financialCorrectionId:int}/contractDebts")]
        public IList<ContractDebtVO> GetFinancialCorrectionContractDebts(int financialCorrectionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.View, financialCorrectionId);

            return this.contractDebtsRepository.GetFinancialCorrectionContractDebts(financialCorrectionId);
        }

        [Route("{contractDebtId:int}")]
        public ContractDebtDO GetContractDebt(int contractDebtId)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.View, contractDebtId);

            var contractDebt = this.contractDebtsRepository.Find(contractDebtId);

            var contract = this.contractsRepository.Find(contractDebt.ContractId);

            int? certReportOrderNum = null;

            if (contractDebt.CertReportId.HasValue)
            {
                certReportOrderNum = this.certReportsRepository.GetOrderNum(contractDebt.CertReportId.Value);
            }

            return new ContractDebtDO(contractDebt, contract, certReportOrderNum);
        }

        [Route("{contractDebtId:int}/info")]
        public ContractDebtInfoVO GetContractDebtInfo(int contractDebtId)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.View, contractDebtId);

            return this.contractDebtsRepository.GetInfo(contractDebtId);
        }

        [HttpGet]
        [Route("new")]
        public NewContractDebtDO NewContractDebt(string contractNum)
        {
            this.authorizer.AssertCanDo(ContractDebtListActions.Create);

            var contract = this.contractsRepository.FindByRegNumber(contractNum);

            return new NewContractDebtDO(contract);
        }

        [HttpPost]
        [Route("canCreate")]
        public object CanCreateContractDebt(string contractRegNumber)
        {
            this.authorizer.AssertCanDo(ContractDebtListActions.Create);

            var errorList = this.contractDebtService.CanCreate(contractRegNumber);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Create))]
        public object CreateContractDebt(NewContractDebtDO newContractDebt)
        {
            this.authorizer.AssertCanDo(ContractDebtListActions.Create);

            var contractDebt = this.contractDebtService.CreateContractDebt(
                newContractDebt.Contract.ContractId,
                newContractDebt.ProgrammePriorityId.Value,
                newContractDebt.RegDate.Value,
                newContractDebt.DebtStartDate.Value,
                newContractDebt.InterestStartDate.Value,
                newContractDebt.PaymentIds);

            return new { ContractDebtId = contractDebt.ContractDebtId };
        }

        [HttpPost]
        [Route("{contractDebtId:int}/cancel")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Cancel))]
        public void CancelContractDebt(int contractDebtId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            byte[] vers = System.Convert.FromBase64String(version);

            var contractDebt = this.contractDebtsRepository.FindForUpdate(contractDebtId, vers);
            contractDebt.MakeDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractDebtId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Delete), IdParam = "contractDebtId")]
        public void DeleteContractDebt(int contractDebtId, string version)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractDebtService.DeleteContractDebt(contractDebtId, vers);
        }

        [HttpPut]
        [Route("{contractDebtId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.BasicData), IdParam = "contractDebtId")]
        public void UpdateContractDebt(int contractDebtId, ContractDebtDO contractDebt)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            this.contractDebtService.UpdateContractDebt(
                contractDebtId,
                contractDebt.Version,
                contractDebt.RegDate.Value,
                contractDebt.DebtStartDate.Value,
                contractDebt.InterestStartDate.Value,
                contractDebt.IrregularityId,
                contractDebt.FinancialCorrectionId,
                contractDebt.Comment,
                contractDebt.ProgrammePriorityId.Value,
                contractDebt.PaymentIds);
        }
    }
}
