using Eumis.ApplicationServices.Services.ContractReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReports/{contractReportId:int}/financialCorrections")]
    public class ContractReportAttachedFinancialCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportFinancialCorrectionCSDsRepository contractReportFinancialCorrectionCSDsRepository;
        private IUsersRepository usersRepository;
        private IContractReportService contractReportService;

        public ContractReportAttachedFinancialCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportFinancialCorrectionCSDsRepository contractReportFinancialCorrectionCSDsRepository,
            IUsersRepository usersRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialCorrectionsRepository = contractReportFinancialCorrectionsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportFinancialCorrectionCSDsRepository = contractReportFinancialCorrectionCSDsRepository;
            this.usersRepository = usersRepository;
            this.contractReportService = contractReportService;
        }

        [Route("")]
        public IList<ContractReportFinancialCorrectionVO> GetContractReportAttachedFinancialCorrections(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportsRepository.GetContractReportAttachedFinancialCorrections(contractReportId);
        }

        [Route("corrections")]
        public IList<ContractReportFinancialCorrectionVO> GetFinancialCorrectionsForContractReport(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);

            return this.contractReportsRepository.GetFinancialCorrectionsForContractReport(contractReport.ContractId);
        }

        [Route("{contractReportFinancialCorrectionId:int}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ContractReportAttachedFinancialCorrectionDO GetContractReportAttachedFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId)
        {
            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(contractReportFinancialCorrectionId);

            var financial = this.contractReportFinancialsRepository.Find(financialCorrection.ContractReportFinancialId);

            var payment = this.contractReportPaymentsRepository.GetActualContractReportPayment(financialCorrection.ContractReportId);

            string username = string.Empty;

            if (financialCorrection.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCorrection.CheckedByUserId.Value);
                username = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            var csdBudgetItems = this.contractReportFinancialCorrectionCSDsRepository.GetContractReportFinancialCorrectionCSDs(contractReportFinancialCorrectionId);

            return new ContractReportAttachedFinancialCorrectionDO(financialCorrection, financial, payment, username, csdBudgetItems);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.AttachedDocuments.AttachedFinancialCorrections.Attach), IdParam = "contractReportId", ChildIdParam = "contractReportFinancialCorrectionId")]
        public object AttachContractReportAttachedFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var attachedCorrection = this.contractReportService.AttachContractReportFinancialCorrection(contractReportId, contractReportFinancialCorrectionId, vers);

            return new { ContractReportFinancialCorrectionId = attachedCorrection.ContractReportFinancialCorrectionId };
        }

        [HttpDelete]
        [Route("{contractReportFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.AttachedDocuments.AttachedFinancialCorrections.Detach), IdParam = "contractReportId", ChildIdParam = "contractReportFinancialCorrectionId")]
        public void DetachContractReportAttachedFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.DetachContractReportFinancialCorrection(contractReportId, contractReportFinancialCorrectionId, vers);
        }

        [HttpPost]
        [Route("canAttach")]
        public ErrorsDO AttachContractReportAttachedFinancialCorrection(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportService.CanAttachContractReportFinancialCorrection(contractReportId);

            return new ErrorsDO(errors);
        }
    }
}
