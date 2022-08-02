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

namespace Eumis.ApplicationServices.Services.ContractSpendingPlan
{
    internal class ContractSpendingPlanService : IContractSpendingPlanService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IContractSpendingPlansRepository contractSpendingPlansRepository;
        private IContractReportsRepository contractReportsRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;

        public ContractSpendingPlanService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractVersionsRepository contractVersionsRepository,
            IContractSpendingPlansRepository contractSpendingPlansRepository,
            IContractReportsRepository contractReportsRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.contractSpendingPlansRepository = contractSpendingPlansRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
        }

        public IList<string> CanCreateSpendingPlan(int contractId)
        {
            var errors = new List<string>();

            var hasVersionsInProgress = this.contractVersionsRepository.HasContractBlockingVersionsInProgress(contractId);
            if (hasVersionsInProgress)
            {
                errors.Add("Не може да се създаде нов план, когато съществува версия на договора, която не е в статус Актуална или Архивирана и не е от тип Частично изменение или Частична промяна.");
            }

            var hasSpendingPlansInProgress = this.contractSpendingPlansRepository.HasContractSpendingPlansInProgress(contractId);
            if (hasSpendingPlansInProgress)
            {
                errors.Add("Не може да се създаде нов план, когато съществува процедурa за избор на изпълнител и сключени договори, която не е в статус Актуален или Архивиран.");
            }

            return errors;
        }

        public IList<string> CanCreateSpendingPlan(Guid contractGid)
        {
            var contractId = this.contractsRepository.GetContractId(contractGid);
            return this.CanCreateSpendingPlan(contractId);
        }

        public void ActivateSpendingPlan(int spendingPlanId, byte[] version)
        {
            var contractSpendingPlan = this.contractSpendingPlansRepository.FindForUpdate(spendingPlanId, version);

            contractSpendingPlan.ChangeStatusToActive();

            var spendingPlansToArchive = this.contractSpendingPlansRepository.GetNonArchivedSpendingPlans(contractSpendingPlan.ContractId)
                .Where(v => v.ContractSpendingPlanXmlId != contractSpendingPlan.ContractSpendingPlanXmlId);

            foreach (var spendingPlan in spendingPlansToArchive)
            {
                spendingPlan.ChangeStatusToArchived();
            }

            this.unitOfWork.Save();
        }

        public Domain.Contracts.ContractSpendingPlanXml CreateSpendingPlanFromAdministrativeAuthority(int contractId, string note)
        {
            return this.CreateSpendingPlan(contractId, note, Source.AdministrativeAuthority, this.accessContext.UserId);
        }

        public Domain.Contracts.ContractSpendingPlanXml CreateSpendingPlanFromBeneficiary(int contractId, string note)
        {
            return this.CreateSpendingPlan(contractId, note, Source.Beneficiary, User.SystemUserId);
        }

        private Domain.Contracts.ContractSpendingPlanXml CreateSpendingPlan(int contractId, string note, Source source, int userId)
        {
            var lastSpendingPlan = this.contractSpendingPlansRepository.GetLastSpendingPlanOrDefault(contractId);

            if (lastSpendingPlan != null && lastSpendingPlan.Status != ContractSpendingPlanStatus.Active)
            {
                throw new Exception("Cannot create a new spendingPlan version of the last version is not active.");
            }

            if (this.contractVersionsRepository.HasContractBlockingVersionsInProgress(contractId))
            {
                throw new Exception("To create a new version there should be no blocking versions for the contract.");
            }

            var activeContractVersion = this.contractVersionsRepository.GetActiveVersion(contractId);

            var xml = this.documentRestApiCommunicator.CreateContractSpendingPlanXml(lastSpendingPlan == null ? null : lastSpendingPlan.Xml, activeContractVersion.Gid, activeContractVersion.Xml);
            var newSpendingPlan = new Domain.Contracts.ContractSpendingPlanXml(
                contractId,
                lastSpendingPlan == null ? 1 : lastSpendingPlan.OrderNum + 1,
                activeContractVersion.ContractVersionXmlId,
                source,
                userId,
                note,
                xml);

            this.contractSpendingPlansRepository.Add(newSpendingPlan);
            this.unitOfWork.Save();

            return newSpendingPlan;
        }
    }
}
