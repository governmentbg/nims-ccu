using Eumis.ApplicationServices.Services.CertReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.CertReports.DataObjects;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/certReports/{certReportId}/contractDebts")]
    public class CertReportContractDebtsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ICertReportsRepository certReportsRepository;
        private ICertReportService certReportService;
        private IContractsRepository contractsRepository;
        private IContractDebtsRepository contractDebtsRepository;
        private IContractDebtVersionsRepository contractDebtVersionsRepository;
        private IUsersRepository usersRepository;
        private IAuthorizer authorizer;

        public CertReportContractDebtsController(
            IUnitOfWork unitOfWork,
            ICertReportsRepository certReportsRepository,
            ICertReportService certReportService,
            IContractsRepository contractsRepository,
            IContractDebtsRepository contractDebtsRepository,
            IContractDebtVersionsRepository contractDebtVersionsRepository,
            IUsersRepository usersRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.certReportsRepository = certReportsRepository;
            this.certReportService = certReportService;
            this.contractsRepository = contractsRepository;
            this.contractDebtsRepository = contractDebtsRepository;
            this.contractDebtVersionsRepository = contractDebtVersionsRepository;
            this.usersRepository = usersRepository;
            this.authorizer = authorizer;
        }

        [Route("contractDebts")]
        public IList<ContractDebtVO> GetCertReportsForContractDebts(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetContractDebtsForCertReport(certReportId);
        }

        [Route("")]
        public IList<ContractDebtVO> GetCertReportContractDebts(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportContractDebts(certReportId);
        }

        [Route("{contractDebtId:int}")]
        public CertReportContractDebtDO GetCertReportContractDebt(int certReportId, int contractDebtId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var contractDebt = this.contractDebtsRepository.Find(contractDebtId);
            var contract = this.contractsRepository.Find(contractDebt.ContractId);
            var contractDebtVersion = this.contractDebtVersionsRepository.GetActualVersion(contractDebtId);
            var createdByUser = this.usersRepository.GetUserFullname(contractDebtVersion.CreatedByUserId);

            int? certReportOrderNum = null;
            if (contractDebt.CertReportId.HasValue)
            {
                certReportOrderNum = this.certReportsRepository.GetOrderNum(contractDebt.CertReportId.Value);
            }

            return new CertReportContractDebtDO(contractDebt, contract, contractDebtVersion, createdByUser, certReportOrderNum);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.ContractDebts.Create), IdParam = "certReportId")]
        public void AddCertReportContractDebt(int certReportId, string version, int[] contractDebtIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportContractDebt(certReportId, vers, contractDebtIds);
        }

        [HttpDelete]
        [Route("{contractDebtId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.ContractDebts.Delete), IdParam = "certReportId", ChildIdParam = "contractDebtId")]
        public void DeleteCertReportContractDebt(int certReportId, int contractDebtId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportContractDebt(certReportId, vers, contractDebtId);
        }

        [HttpPost]
        [Route("{contractDebtId:int}/canDelete")]
        public ErrorsDO CanDeleteCertReportContractDebt(int certReportId, int contractDebtId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            var errors = this.certReportService.CanDeleteCertReportContractDebt(certReportId, contractDebtId);

            return new ErrorsDO(errors);
        }
    }
}
