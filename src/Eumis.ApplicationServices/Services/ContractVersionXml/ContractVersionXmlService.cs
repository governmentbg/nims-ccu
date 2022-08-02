using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.Contracts;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eumis.ApplicationServices.Services.ContractVersionXml
{
    internal class ContractVersionXmlService : IContractVersionXmlService
    {
        private IUnitOfWork unitOfWork;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IContractsRepository contractsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IContractSpendingPlansRepository contractSpendingPlansRepository;
        private IContractReportsRepository contractReportsRepository;

        public ContractVersionXmlService(
            IUnitOfWork unitOfWork,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IContractsRepository contractsRepository,
            IContractVersionsRepository contractVersionsRepository,
            IContractProcurementsRepository contractProcurementsRepository,
            IContractSpendingPlansRepository contractSpendingPlansRepository,
            IContractReportsRepository contractReportsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.contractsRepository = contractsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.contractSpendingPlansRepository = contractSpendingPlansRepository;
            this.contractReportsRepository = contractReportsRepository;
        }

        public IList<string> CanCreateVersion(int contractId, ContractVersionType type)
        {
            switch (type)
            {
                case ContractVersionType.Annex:
                case ContractVersionType.Change:
                    return this.CanCreateVersion(contractId);

                case ContractVersionType.PartialAnnex:
                case ContractVersionType.PartialChange:
                    return this.CanCreatePartialVersion(contractId);

                default:
                    throw new InvalidEnumArgumentException("Invalid ContractVersionType");
            }
        }

        private IList<string> CanCreatePartialVersion(int contractId)
        {
            var errors = new List<string>();

            var hasVersionsInProgress = this.contractVersionsRepository.HasContractVersionsInProgress(contractId);
            if (hasVersionsInProgress)
            {
                errors.Add("Не може да се създаде нова версия на договор, когато съществува версия, която не е в статус Актуална или Архивирана.");
            }

            return errors;
        }

        private IList<string> CanCreateVersion(int contractId)
        {
            var errors = new List<string>();

            var hasVersionsInProgress = this.contractVersionsRepository.HasContractVersionsInProgress(contractId);
            if (hasVersionsInProgress)
            {
                errors.Add("Не може да се създаде нова версия на договор, когато съществува версия, която не е в статус Актуална или Архивирана.");
            }

            var hasProcurementsInProgress = this.contractProcurementsRepository.HasContractProcurementsInProgress(contractId);
            if (hasProcurementsInProgress)
            {
                errors.Add("Не може да се създаде нова версия на договор, когато съществува процедурa за избор на изпълнител и сключени договори, която не е в статус Актуален или Архивиран.");
            }

            var hasSpendingPlansInProgress = this.contractSpendingPlansRepository.HasContractSpendingPlansInProgress(contractId);
            if (hasSpendingPlansInProgress)
            {
                errors.Add("Не може да се създаде нова версия на договор, когато съществува план за разходване на средствата, който не е в статус Актуален или Архивиран.");
            }

            var hasContractReportInProgress = this.contractReportsRepository.HasContractReportInProgress(contractId);
            if (hasContractReportInProgress)
            {
                errors.Add("Не може да се създаде нова версия на договор, когато съществува пакет отчетни документи, който не е в статус Приет или Отхвърлен.");
            }

            return errors;
        }
    }
}
