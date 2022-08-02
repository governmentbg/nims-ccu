using Eumis.ApplicationServices.Services.CertReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.ReimbursedAmounts.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.CertReports.DataObjects;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/certReports/{certReportId}/debtReimbursedAmounts")]
    public class CertReportDebtReimbursedAmountsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ICertReportsRepository certReportsRepository;
        private ICertReportService certReportService;
        private IContractsRepository contractsRepository;
        private IContractDebtsRepository contractDebtsRepository;
        private IDebtReimbursedAmountsRepository debtReimbursedAmountsRepository;
        private IUsersRepository usersRepository;
        private IAuthorizer authorizer;

        public CertReportDebtReimbursedAmountsController(
            IUnitOfWork unitOfWork,
            ICertReportsRepository certReportsRepository,
            ICertReportService certReportService,
            IContractsRepository contractsRepository,
            IContractDebtsRepository contractDebtsRepository,
            IDebtReimbursedAmountsRepository debtReimbursedAmountsRepository,
            IUsersRepository usersRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.certReportsRepository = certReportsRepository;
            this.certReportService = certReportService;
            this.contractsRepository = contractsRepository;
            this.contractDebtsRepository = contractDebtsRepository;
            this.debtReimbursedAmountsRepository = debtReimbursedAmountsRepository;
            this.usersRepository = usersRepository;
            this.authorizer = authorizer;
        }

        [Route("debtReimbursedAmounts")]
        public IList<DebtReimbursedAmountVO> GetCertReportsForDebtReimbursedAmounts(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetDebtReimbursedAmountsForCertReport(certReportId);
        }

        [Route("")]
        public IList<DebtReimbursedAmountVO> GetCertReportDebtReimbursedAmounts(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportDebtReimbursedAmounts(certReportId);
        }

        [Route("{debtReimbursedAmountId:int}")]
        public CertReportDebtReimbursedAmountDO GetCertReportDebtReimbursedAmount(int certReportId, int debtReimbursedAmountId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var reimbursedAmount = this.debtReimbursedAmountsRepository.Find(debtReimbursedAmountId);
            var contractDebtInfo = this.contractDebtsRepository.GetInfo(reimbursedAmount.ContractDebtId);
            var reimbursedAmountBasicData = this.debtReimbursedAmountsRepository.GetBasicData(debtReimbursedAmountId);

            return new CertReportDebtReimbursedAmountDO(reimbursedAmount, contractDebtInfo, reimbursedAmountBasicData);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.DebtReimbursedAmounts.Create), IdParam = "certReportId")]
        public void AddCertReportDebtReimbursedAmount(int certReportId, string version, int[] debtReimbursedAmountIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportDebtReimbursedAmount(certReportId, vers, debtReimbursedAmountIds);
        }

        [HttpDelete]
        [Route("{debtReimbursedAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.DebtReimbursedAmounts.Delete), IdParam = "certReportId", ChildIdParam = "debtReimbursedAmountId")]
        public void DeleteCertReportDebtReimbursedAmount(int certReportId, int debtReimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportDebtReimbursedAmount(certReportId, vers, debtReimbursedAmountId);
        }
    }
}
