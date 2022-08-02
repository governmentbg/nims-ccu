using Eumis.ApplicationServices.Communicators;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractProcurement
{
    internal class ContractProcurementService : IContractProcurementService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;
        private IContractReportsRepository contractReportsRepository;

        public ContractProcurementService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractVersionsRepository contractVersionsRepository,
            IContractProcurementsRepository contractProcurementsRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IContractReportsRepository contractReportsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.contractReportsRepository = contractReportsRepository;
        }

        public IList<string> CanCreateProcurement(int contractId)
        {
            var errors = new List<string>();

            var hasVersionsInProgress = this.contractVersionsRepository.HasContractBlockingVersionsInProgress(contractId);
            if (hasVersionsInProgress)
            {
                errors.Add("Не може да се създаде нова процедура, когато съществува версия на договора, която не е в статус Актуална или Архивирана и не е от тип Частично изменение или Частична промяна.");
            }

            var hasProcurementsInProgress = this.contractProcurementsRepository.HasContractProcurementsInProgress(contractId);
            if (hasProcurementsInProgress)
            {
                errors.Add("Не може да се създаде нова процедура, когато съществува процедурa за избор на изпълнител и сключени договори, която не е в статус Актуален или Архивиран.");
            }

            return errors;
        }

        public IList<string> CanCreateProcurement(Guid contractGid)
        {
            var contractId = this.contractsRepository.GetContractId(contractGid);
            return this.CanCreateProcurement(contractId);
        }

        public void ActivateProcurement(int procurementId, byte[] version)
        {
            var contractProcurement = this.contractProcurementsRepository.FindForUpdate(procurementId, version);

            contractProcurement.ChangeStatusToActive();

            var procurementsToArchive = this.contractProcurementsRepository.GetNonArchivedProcurements(contractProcurement.ContractId)
                .Where(v => v.ContractProcurementXmlId != contractProcurement.ContractProcurementXmlId);

            foreach (var procurement in procurementsToArchive)
            {
                procurement.ChangeStatusToArchived();
            }

            this.unitOfWork.Save();
        }

        public Domain.Contracts.ContractProcurementXml CreateProcurementFromAdministrativeAuthority(int contractId, string note)
        {
            return this.CreateProcurement(contractId, note, Source.AdministrativeAuthority, this.accessContext.UserId);
        }

        public Domain.Contracts.ContractProcurementXml CreateProcurementFromBeneficiary(int contractId, string note)
        {
            return this.CreateProcurement(contractId, note, Source.Beneficiary, User.SystemUserId);
        }

        private Domain.Contracts.ContractProcurementXml CreateProcurement(int contractId, string note, Source source, int userId)
        {
            var lastProcurement = this.contractProcurementsRepository.GetLastProcurementOrDefault(contractId);

            if (lastProcurement != null && lastProcurement.Status != ContractProcurementStatus.Active)
            {
                throw new Exception("Cannot create a new procurement version of the last version is not active.");
            }

            if (this.contractVersionsRepository.HasContractBlockingVersionsInProgress(contractId))
            {
                throw new Exception("To create a new version there should be no blocking versions for the contract.");
            }

            var activeContractVersion = this.contractVersionsRepository.GetActiveVersion(contractId);
            var orderNum = lastProcurement == null ? 1 : lastProcurement.OrderNum + 1;

            var xml = this.documentRestApiCommunicator.CreateContractProcurementXml(lastProcurement == null ? null : lastProcurement.Xml, activeContractVersion.Gid, activeContractVersion.Xml, orderNum);
            var newProcurement = new Domain.Contracts.ContractProcurementXml(
                contractId,
                lastProcurement == null ? 1 : lastProcurement.OrderNum + 1,
                activeContractVersion.ContractVersionXmlId,
                source,
                userId,
                note,
                xml);

            this.contractProcurementsRepository.Add(newProcurement);
            this.unitOfWork.Save();

            return newProcurement;
        }
    }
}
