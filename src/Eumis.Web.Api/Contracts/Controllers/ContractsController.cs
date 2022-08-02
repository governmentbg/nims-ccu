using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.ApplicationServices.Services.Contract;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCertCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
using Eumis.Data.ContractReportFinancialRevalidations.Repositories;
using Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReportTechnicalCorrections.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Contracts.DataObjects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts")]
    public class ContractsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IContractService contractService;
        private IContractsRepository contractsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository;
        private IContractReportTechnicalCorrectionsRepository contractReportTechnicalCorrectionsRepository;
        private IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository;
        private IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository;
        private IContractReportCertAuthorityFinancialCorrectionsRepository contractReportCertAuthorityFinancialCorrectionsRepository;
        private IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository contractReportRevalidationCertAuthorityFinancialCorrectionsRepository;

        public ContractsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IContractService contractService,
            IContractsRepository contractsRepository,
            IContractVersionsRepository contractVersionsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository,
            IContractReportTechnicalCorrectionsRepository contractReportTechnicalCorrectionsRepository,
            IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository,
            IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository,
            IContractReportCertAuthorityFinancialCorrectionsRepository contractReportCertAuthorityFinancialCorrectionsRepository,
            IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository contractReportRevalidationCertAuthorityFinancialCorrectionsRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.contractService = contractService;
            this.contractsRepository = contractsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialCorrectionsRepository = contractReportFinancialCorrectionsRepository;
            this.contractReportTechnicalCorrectionsRepository = contractReportTechnicalCorrectionsRepository;
            this.contractReportFinancialRevalidationsRepository = contractReportFinancialRevalidationsRepository;
            this.contractReportFinancialCertCorrectionsRepository = contractReportFinancialCertCorrectionsRepository;
            this.contractReportCertAuthorityFinancialCorrectionsRepository = contractReportCertAuthorityFinancialCorrectionsRepository;
            this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository = contractReportRevalidationCertAuthorityFinancialCorrectionsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractVO> GetContracts(int? programmePriorityId = null, int? procedureId = null, string contractNumber = null)
        {
            this.authorizer.AssertCanDo(ContractListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractPermissions.CanRead);

            return this.contractsRepository.GetContracts(programmeIds, programmePriorityId, procedureId, true, contractNumber, this.accessContext.UserId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/physicalExecutionActivities")]
        public IList<ContractPhysicalExecutionActivityVO> GetContractPhysicalExecutionActivitiesForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractsRepository.GetContractPhysicalExecutionActivitiesForProjectDossier(contractId);
        }

        [Route("~/api/certAuthorityCommunicationContracts")]
        public IList<ContractVO> GetCertAuthorityCommunicationContracts(int? programmePriorityId = null, int? procedureId = null)
        {
            this.authorizer.AssertCanDo(CertAuthorityCommunicationListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            return this.contractsRepository.GetContracts(programmeIds, programmePriorityId, procedureId, false);
        }

        [Route("~/api/auditAuthorityCommunicationContracts")]
        public IList<ContractVO> GetAuditAuthorityCommunicationContracts(int? programmePriorityId = null, int? procedureId = null)
        {
            this.authorizer.AssertCanDo(AuditAuthorityCommunicationListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            return this.contractsRepository.GetContracts(programmeIds, programmePriorityId, procedureId, false);
        }

        [Route("~/api/reportContracts")]
        public IList<ContractVO> GetReportContracts(int? procedureId = null, string contractNumber = null)
        {
            this.authorizer.AssertCanDo(ContractReportListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractReportPermissions.CanWrite);

            return this.contractsRepository.GetContracts(programmeIds, null, procedureId, false, contractNumber);
        }

        [Route("~/api/financialCorrectionContracts")]
        public IList<ContractVO> GetFinancialCorrectionContracts(int? programmeId = null, string contractNumber = null)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionListActions.Create);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(pId => pId == programmeId).ToArray();
            }

            return this.contractsRepository.GetUserAvailableContracts(programmeIds, this.accessContext.UserId, contractNumber);
        }

        [Route("~/api/contractDebtContracts")]
        public IList<ContractVO> GetContractDebtContracts(int? programmeId = null, string contractNumber = null)
        {
            this.authorizer.AssertCanDo(ContractDebtListActions.Create);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(pId => pId == programmeId).ToArray();
            }

            return this.contractsRepository.GetContracts(programmeIds, null, null, false, contractNumber);
        }

        [Route("~/api/correctionDebtContracts")]
        public IList<ContractVO> GetCorrectionDebtContracts(int? programmeId = null, string contractNumber = null)
        {
            this.authorizer.AssertCanDo(CorrectionDebtListActions.Create);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(pId => pId == programmeId).ToArray();
            }

            return this.contractsRepository.GetContracts(programmeIds, null, null, false, contractNumber);
        }

        [Route("~/api/irregularitySignalContracts")]
        public IList<ContractVO> GetIrregularitySignalContracts(int? programmeId = null, string contractNumber = null)
        {
            this.authorizer.AssertCanDo(IrregularitySignalListActions.Create);

            var programmeIds = System.Array.Empty<int>();

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(pId => pId == programmeId).ToArray();
            }

            return this.contractsRepository.GetContracts(programmeIds, null, null, false, contractNumber);
        }

        [Route("~/api/actuallyPaidAmountContracts")]
        public IList<ContractVO> GetActuallyPaidAmountContracts(int? programmeId = null, string contractNumber = null)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountListActions.Create);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(pId => pId == programmeId).ToArray();
            }

            return this.contractsRepository.GetContracts(programmeIds, null, null, false, contractNumber);
        }

        [Route("~/api/reimbursedAmountContracts")]
        public IList<ContractVO> GetReimbursedAmountContracts(int? programmeId = null, string contractNumber = null)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountListActions.Create);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(pId => pId == programmeId).ToArray();
            }

            return this.contractsRepository.GetContracts(programmeIds, null, null, false, contractNumber);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Create))]
        public object CreateContract(int projectId, int programmeId, ContractRegistrationType registrationType, int? attachedContractId = null)
        {
            this.authorizer.AssertCanDo(ContractListActions.Create);

            var newContract = this.contractService.CreateContract(projectId, programmeId, ContractType.Contract, registrationType, attachedContractId, this.accessContext.UserId);

            return new { ContractId = newContract.ContractId };
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateNewContract(int projectId, int programmeId)
        {
            this.authorizer.AssertCanDo(ContractListActions.Create);

            var errorList = this.contractService.CanCreate(projectId, programmeId, this.accessContext.UserId);

            return new ErrorsDO(errorList);
        }

        [Route("{contractId:int}/data")]
        public ContractDataDO GetContractData(int contractId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractActions.View, contractId),
                Tuple.Create<Enum, int?>(ContractActions.SearchAdminAuthorityCommunications, contractId),
                Tuple.Create<Enum, int?>(ContractReportListActions.Search, null),
                Tuple.Create<Enum, int?>(ContractReportCheckListActions.Search, null),
                Tuple.Create<Enum, int?>(ContractReportFinancialCorrectionListActions.Search, null),
                Tuple.Create<Enum, int?>(ContractReportCertAuthorityFinancialCorrectionListActions.Search, null),
                Tuple.Create<Enum, int?>(ContractActions.SearchCertAuthorityCommunications, contractId),
                Tuple.Create<Enum, int?>(ContractActions.SearchAuditAuthorityCommunications, contractId));

            var contract = this.contractsRepository.FindWithoutIncludes(contractId);

            return new ContractDataDO(contract);
        }

        [Route("{contractId:int}")]
        public ContractDO GetContract(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            var contract = this.contractsRepository.Find(contractId);
            var version = this.contractVersionsRepository.FindForDraftContract(contractId);

            return new ContractDO(contract, version);
        }

        [Route("{contractId:int}/info")]
        public ContractInfoVO GetContractInfo(int contractId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractActions.View, contractId),
                Tuple.Create<Enum, int?>(ContractActions.SearchAdminAuthorityCommunications, contractId),
                Tuple.Create<Enum, int?>(ContractActions.SearchCertAuthorityCommunications, contractId),
                Tuple.Create<Enum, int?>(ContractActions.SearchAuditAuthorityCommunications, contractId));

            var contractInfo = this.contractsRepository.GetContractInfo(contractId);

            return contractInfo;
        }

        [HttpPost]
        [Route("{contractId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.ChangeStatusToDraft), IdParam = "contractId")]
        public void ChnageStatusToDraft(int contractId, string contractVersion)
        {
            this.authorizer.AssertCanDo(ContractActions.ChangeStatusToDraft, contractId);

            var version = this.contractVersionsRepository.FindForDraftContractForUpdate(contractId, Convert.FromBase64String(contractVersion));

            if (version.Status == ContractVersionStatus.Entered)
            {
                version.ChangeStatusToDraft();
            }

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Delete), IdParam = "contractId")]
        public void DeleteContract(int contractId, string version, int contractVersionId, string contractVersion)
        {
            this.authorizer.AssertCanDo(ContractActions.Delete, contractId);

            if (this.contractsRepository.CanDeleteContract(contractId).Count != 0)
            {
                throw new InvalidOperationException("Cannot delete contract.");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            byte[] contractVers = System.Convert.FromBase64String(contractVersion);

            Contract contract = this.contractsRepository.FindForUpdate(contractId, vers);

            ContractVersionXml contractVersionXml = this.contractVersionsRepository.FindForUpdate(contractVersionId, contractVers);

            this.contractVersionsRepository.Remove(contractVersionXml);
            this.unitOfWork.Save();

            this.contractsRepository.Remove(contract);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractId:int}/canDelete")]
        public ErrorsDO CanDeleteContract(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.Delete, contractId);

            var errorList = this.contractsRepository.CanDeleteContract(contractId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{contractId:int}/markAsChecked")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.MarkAsChecked), IdParam = "contractId")]
        public void MarkAsChecked(int contractId, string version, string contractVersion)
        {
            this.authorizer.AssertCanDo(ContractActions.MarkAsChecked, contractId);

            this.contractService.EnterContract(
                contractId,
                Convert.FromBase64String(version),
                Convert.FromBase64String(contractVersion));
        }

        [HttpGet]
        [Route("isRegNumExisting")]
        public bool IsContractNumExisting(string contractNum, int? procedureId = null, int? projectId = null, int? programmeId = null)
        {
            return this.contractsRepository.IsContractNumExisting(contractNum, procedureId, projectId, programmeId);
        }

        [Route("~/api/contractReports/{contractReportId:int}/contractInfo")]
        public ContractInfoVO GetContractInfoByContractReport(int contractReportId)
        {
            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsNotDraftFromBeneficiary();

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractActions.View, contractReport.ContractId),
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));

            var contractInfo = this.contractsRepository.GetContractInfo(contractReport.ContractId);

            return contractInfo;
        }

        [Route("~/api/contractReportFinancialCorrections/{contractReportFinancialCorrectionId:int}/contractInfo")]
        public ContractInfoVO GetContractInfoByContractReportCorrection(int contractReportFinancialCorrectionId)
        {
            var contractReportCorrection = this.contractReportFinancialCorrectionsRepository.Find(contractReportFinancialCorrectionId);

            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.View, contractReportFinancialCorrectionId);

            var contractInfo = this.contractsRepository.GetContractInfo(contractReportCorrection.ContractId);

            return contractInfo;
        }

        [Route("~/api/contractReportTechnicalCorrections/{contractReportTechnicalCorrectionId:int}/contractInfo")]
        public ContractInfoVO GetContractInfoByContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId)
        {
            var contractReportTechnicalCorrection = this.contractReportTechnicalCorrectionsRepository.Find(contractReportTechnicalCorrectionId);

            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.View, contractReportTechnicalCorrectionId);

            var contractInfo = this.contractsRepository.GetContractInfo(contractReportTechnicalCorrection.ContractId);

            return contractInfo;
        }

        [Route("~/api/contractReportFinancialRevalidations/{contractReportFinancialRevalidationId:int}/contractInfo")]
        public ContractInfoVO GetContractInfoByContractReportRevalidation(int contractReportFinancialRevalidationId)
        {
            var contractReportRevalidation = this.contractReportFinancialRevalidationsRepository.Find(contractReportFinancialRevalidationId);

            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.View, contractReportFinancialRevalidationId);

            var contractInfo = this.contractsRepository.GetContractInfo(contractReportRevalidation.ContractId);

            return contractInfo;
        }

        [Route("~/api/contractReportFinancialCertCorrections/{contractReportFinancialCertCorrectionId:int}/contractInfo")]
        public ContractInfoVO GetContractInfoByContractReportCertCorrection(int contractReportFinancialCertCorrectionId)
        {
            var contractReportCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(contractReportFinancialCertCorrectionId);

            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.View, contractReportFinancialCertCorrectionId);

            var contractInfo = this.contractsRepository.GetContractInfo(contractReportCertCorrection.ContractId);

            return contractInfo;
        }

        [Route("~/api/contractReportCertAuthorityFinancialCorrections/{contractReportCertAuthorityFinancialCorrectionId:int}/contractInfo")]
        public ContractInfoVO GetContractInfoByContractReportCertAuthorityCorrection(int contractReportCertAuthorityFinancialCorrectionId)
        {
            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(contractReportCertAuthorityFinancialCorrectionId);

            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.View, contractReportCertAuthorityFinancialCorrectionId);

            var contractInfo = this.contractsRepository.GetContractInfo(contractReportCertAuthorityCorrection.ContractId);

            return contractInfo;
        }

        [Route("~/api/contractReportRevalidationCertAuthorityFinancialCorrections/{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/contractInfo")]
        public ContractInfoVO GetContractInfoByContractReportRevalidationCertAuthorityCorrection(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Find(contractReportRevalidationCertAuthorityFinancialCorrectionId);

            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.View, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            var contractInfo = this.contractsRepository.GetContractInfo(contractReportRevalidationCertAuthorityCorrection.ContractId);

            return contractInfo;
        }

        [Route("actuallyPaidAmountByRegNum")]
        public ContractDataDO GetActuallyPaidAmountContractByNumber(string contractNum)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountListActions.Create);

            var contract = this.contractsRepository.FindByRegNumber(contractNum);

            return new ContractDataDO(contract);
        }

        [Route("correctionDebtContractByNumber")]
        public ContractDataDO GetCorrectionDebtContractByNumber(string contractNum)
        {
            this.authorizer.AssertCanDo(CorrectionDebtListActions.Create);

            var contract = this.contractsRepository.FindByRegNumber(contractNum);

            return new ContractDataDO(contract);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}")]
        public ContractDataDO GetProjectDossierContractData(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            var contract = this.contractsRepository.FindWithoutIncludes(contractId);

            if (contract.ProjectId != projectId)
            {
                throw new DomainValidationException("Contract's ProjectId is different from the given projectId");
            }

            return new ContractDataDO(contract);
        }
    }
}
