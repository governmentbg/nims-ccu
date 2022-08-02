using System;
using System.Web.Http;
using Eumis.ApplicationServices.Services.Contract;
using Eumis.ApplicationServices.Services.ProjectRegistration;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/service")]
    public class ContractServiceController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private ICompaniesRepository companiesRepository;
        private IProjectRegistrationService projectRegistrationService;
        private IContractService contractService;
        private IAccessContext accessContext;
        private IProceduresRepository proceduresRepository;

        public ContractServiceController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            ICompaniesRepository companiesRepository,
            IProjectRegistrationService projectRegistrationService,
            IContractService contractService,
            IProceduresRepository proceduresRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.companiesRepository = companiesRepository;
            this.projectRegistrationService = projectRegistrationService;
            this.contractService = contractService;
            this.accessContext = accessContext;
            this.proceduresRepository = proceduresRepository;
        }

        [Route("new")]
        public ContractServiceDO GetContractService(int procedureId, int companyId)
        {
            this.authorizer.AssertCanDo(ContractListActions.Create);

            var company = this.companiesRepository.Find(companyId);

            return new ContractServiceDO()
            {
                ProcedureId = procedureId,
                CompanyId = company.CompanyId,
                CompanyName = company.Name,
                CompanyUin = company.Uin,
                CompanyUinType = company.UinType,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Create))]
        public object CreateContractService(ContractServiceDO contractDO)
        {
            this.authorizer.AssertCanDo(ContractListActions.Create);

            var companyData = this.proceduresRepository.GetProgrammePriorityCompany(contractDO.ProcedureId);

            var regDate = DateTime.Now;
            var projectId = this.projectRegistrationService.RegisterEmpty(
                    contractDO.ProcedureId,
                    contractDO.Name,
                    contractDO.NameAlt,
                    companyData.CompanyId,
                    1,
                    Domain.Projects.ProjectRegistrationStatus.RegisteredService,
                    regDate,
                    Domain.Projects.ProjectRecieveType.Electronic,
                    regDate,
                    regDate,
                    string.Empty,
                    1,
                    1,
                    contractDO.Notes);

            this.unitOfWork.Save();

            var newContract = this.contractService.CreateServiceContractAgreement(
                projectId,
                contractDO.ProgrammeId.Value,
                ContractType.ServiceAgreement,
                contractDO.RegistrationType,
                companyData.CompanyId,
                this.accessContext.UserId);

            return new { newContract.ContractId };
        }
    }
}
