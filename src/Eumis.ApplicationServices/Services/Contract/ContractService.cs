using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ProjectVersionXml;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.ApplicationServices.Services.Contract
{
    public class ContractService : IContractService
    {
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProjectsRepository projectsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IContractsRepository contractsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;
        private IProjectVersionXmlService projectVersionXmlService;

        public ContractService(
            IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IEvalSessionsRepository evalSessionsRepository,
            IContractsRepository contractsRepository,
            IProjectsRepository projectsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IContractVersionsRepository contractVersionsRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IProjectVersionXmlService projectVersionXmlService)
        {
            this.unitOfWork = unitOfWork;
            this.permissionsRepository = permissionsRepository;
            this.evalSessionsRepository = evalSessionsRepository;
            this.projectsRepository = projectsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.contractsRepository = contractsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.projectVersionXmlService = projectVersionXmlService;
        }

        public IList<string> CanCreate(int projectId, int programmeId, int userId)
        {
            var errors = new List<string>();

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(userId, ContractPermissions.CanWrite);
            if (!programmeIds.Contains(programmeId))
            {
                errors.Add("Не може да се създаде нов договор към програма, за която нямате право за създаване на договор.");
            }

            if (!this.evalSessionsRepository.IsProjectApprovedOrReserveInEvalSession(projectId))
            {
                errors.Add("Не може да се създаде нов договор към ПП, което не е класирано в оценителна сесия като Одобрено или Резерва.");
            }

            if (this.contractsRepository.ProjectHasContractForProgramme(projectId, programmeId))
            {
                errors.Add("Вече е създаден договор за това ПП и избраната Програма.");
            }

            var applicationFormType = this.projectsRepository.GetProcedureApplicationFormType(projectId);
            if (applicationFormType == ApplicationFormType.PreliminarySelection)
            {
                errors.Add("Не може да се създава договор от проектно предложение в процедура за предварителен подбор.");
            }

            return errors;
        }

        public Domain.Contracts.Contract CreateContract(
            int projectId,
            int programmeId,
            ContractType contractType,
            ContractRegistrationType registrationType,
            int? attachedContractId,
            int userId)
        {
            if (this.CanCreate(projectId, programmeId, userId).Any())
            {
                throw new Exception("Contract creation prerequisites not met!");
            }

            var project = this.projectsRepository.Find(projectId);

            return this.CreateContractInternal(project, programmeId, contractType, registrationType, attachedContractId, userId, "Нов договор");
        }

        public Domain.Contracts.Contract CreateServiceContractAgreement(
            int projectId,
            int programmeId,
            ContractType contractType,
            ContractRegistrationType registrationType,
            int companyId,
            int userId)
        {
            if (contractType != Domain.Contracts.ContractType.ServiceAgreement)
            {
                throw new Exception("Service contract creation prerequisites not met!");
            }

            var project = this.projectsRepository.Find(projectId);

            var projectVersionXml = this.projectVersionXmlService.CreateProjectServiceVersion(project, companyId, userId);

            return this.CreateContractInternal(project, programmeId, contractType, registrationType, null, userId, "Нов служебен договор");
        }

        public void EnterContract(int contractId, byte[] version, byte[] contractVersion)
        {
            var contract = this.contractsRepository.FindForUpdate(contractId, version);
            var contractVersionXml = this.contractVersionsRepository.FindForDraftContractForUpdate(contractId, contractVersion);
            var project = this.projectsRepository.Find(contract.ProjectId);

            contractVersionXml.ChangeStatusToActive();

            contract.ChangeStatusToEntered();

            project.UpdateEvalStatus(ProjectEvalStatus.Contracted);

            this.unitOfWork.Save();
        }

        private Domain.Contracts.Contract CreateContractInternal(
            Project project,
            int programmeId,
            ContractType contractType,
            ContractRegistrationType registrationType,
            int? attachedContractId,
            int userId,
            string createNote)
        {
            var currentDate = DateTime.Now;

            var contractData = this.contractsRepository.GetContractData(project.ProjectId, programmeId);
            var contractRegNumber = Domain.Contracts.ContractVersionXml.CreateRegNumber(contractData, 1);

            var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(project.ProjectId);

            var newContract = new Domain.Contracts.Contract(
                programmeId,
                project.ProcedureId,
                project.ProjectId,
                contractType,
                registrationType,
                project.CompanyId,
                project.CompanyName,
                project.CompanyUin,
                project.CompanyUinType,
                contractRegNumber,
                project.Name,
                currentDate,
                attachedContractId);

            newContract.NameEN = project.NameAlt;

            var contractXml = this.documentRestApiCommunicator.CreateInitialContractVersionXml(
                actualProjectVersion.Xml,
                contractData.ProgrammeCode,
                project.RegNumber,
                contractRegNumber,
                contractData.AuthorityUin,
                contractData.AuthorityUinType,
                newContract.Gid);

            this.contractsRepository.Add(newContract);
            this.unitOfWork.Save();

            var newContractVersion = new Domain.Contracts.ContractVersionXml(
                contractRegNumber,
                newContract.ContractId,
                userId,
                currentDate,
                createNote,
                contractXml);

            this.contractVersionsRepository.Add(newContractVersion);
            this.unitOfWork.Save();

            return newContract;
        }
    }
}
