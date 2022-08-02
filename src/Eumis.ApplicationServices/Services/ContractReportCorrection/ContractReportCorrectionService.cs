using Eumis.Common.Db;
using Eumis.Data.ContractReportCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractReportCorrection
{
    public class ContractReportCorrectionService : IContractReportCorrectionService
    {
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportCorrectionsRepository contractReportCorrectionsRepository;
        private IProgrammesRepository programmesRepository;
        private IUnitOfWork unitOfWork;

        public ContractReportCorrectionService(
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IProceduresRepository proceduresRepository,
            IContractReportCorrectionsRepository contractReportCorrectionsRepository,
            IProgrammesRepository programmesRepository,
            IUnitOfWork unitOfWork)
        {
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportCorrectionsRepository = contractReportCorrectionsRepository;
            this.programmesRepository = programmesRepository;
            this.unitOfWork = unitOfWork;
        }

        public Domain.Contracts.ContractReportCorrection CreateContractReportCorrection(
            int userId,
            ContractReportCorrectionType type,
            Sign sign,
            DateTime date,
            int? programmeId,
            int? programmePriorityId,
            int? procedureId,
            int? contractId,
            int? contractReportPaymentId)
        {
            int? realProgrammeId = null;
            int? realProgrammePriorityId = null;
            int? realProcedureId = null;
            int? realContractId = null;
            int? realContractReportPaymentId = null;

            switch (type)
            {
                case ContractReportCorrectionType.PaymentVerified:
                case ContractReportCorrectionType.AdvanceCovered:
                    realContractId = this.contractReportPaymentsRepository.GetContractId(contractReportPaymentId.Value);
                    realProgrammeId = this.contractsRepository.GetProgrammeId(realContractId.Value);
                    realProcedureId = this.contractsRepository.GetProcedureId(realContractId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    realContractReportPaymentId = contractReportPaymentId.Value;
                    break;
                case ContractReportCorrectionType.ContractVerified:
                    realContractId = contractId.Value;
                    realProcedureId = this.contractsRepository.GetProcedureId(realContractId.Value);
                    realProgrammeId = this.contractsRepository.GetProgrammeId(realContractId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
                case ContractReportCorrectionType.ProgrameVerified:
                    realProgrammeId = programmeId.Value;
                    break;
                case ContractReportCorrectionType.ProgramePriorityVerified:
                    realProgrammeId = this.programmePrioritiesRepository.GetProgrammeId(programmePriorityId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
                case ContractReportCorrectionType.ProcedureVerified:
                    realProgrammeId = this.proceduresRepository.GetPrimaryProcedureProgrammeId(procedureId.Value);
                    realProcedureId = procedureId.Value;
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
            }

            if (!this.CanCreate(userId, realProgrammeId, realContractId, realContractReportPaymentId))
            {
                throw new InvalidOperationException("Cannot create ContractReportCorrection");
            }

            var newContractReportCorrection = new Domain.Contracts.ContractReportCorrection(
                type,
                sign,
                date,
                realProgrammeId.Value,
                realProgrammePriorityId,
                realProcedureId,
                realContractId,
                realContractReportPaymentId);

            this.contractReportCorrectionsRepository.Add(newContractReportCorrection);

            this.unitOfWork.Save();

            return newContractReportCorrection;
        }

        public IList<string> CanEnterContractReportCorrection(int contractReportCorrectionId)
        {
            var errors = new List<string>();

            var correction = this.contractReportCorrectionsRepository.Find(contractReportCorrectionId);

            if (!correction.CorrectedApprovedEuAmount.HasValue ||
                !correction.CorrectedApprovedBgAmount.HasValue ||
                !correction.CorrectedApprovedBfpTotalAmount.HasValue ||
                !correction.CorrectedApprovedSelfAmount.HasValue ||
                !correction.CorrectedApprovedCrossAmount.HasValue ||
                !correction.CorrectedApprovedTotalAmount.HasValue)
            {
                errors.Add("Не можете да въведете корекцията, защото всички полета за суми от секцията 'Корекция' трябва да са попълнени");
            }

            return errors;
        }

        public IList<string> CanMakeDraftContractReportCorrection(int contractReportCorrectionId)
        {
            var errors = new List<string>();

            if (this.contractReportCorrectionsRepository.IsIncludedInCertReport(contractReportCorrectionId))
            {
                errors.Add("Не можете да промените статуса на корекцията на 'Чернова', защото тя е включена в доклад по сертификация");
            }

            return errors;
        }

        private bool CanCreate(
            int userId,
            int? programmeId,
            int? contractId,
            int? contractReportPaymentId)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(userId, MonitoringFinancialControlPermissions.CanWriteFinancial);

            if (programmeIds.Contains(programmeId.Value) && contractReportPaymentId.HasValue)
            {
                var reportStatus = this.contractReportPaymentsRepository.GetContractReportStatus(contractReportPaymentId.Value);
                var paymentStatus = this.contractReportPaymentsRepository.GetContractReportPaymentStatus(contractReportPaymentId.Value);
                var paymentContractId = this.contractReportPaymentsRepository.GetContractId(contractReportPaymentId.Value);

                return paymentContractId == contractId &&
                    reportStatus == ContractReportStatus.Accepted &&
                    paymentStatus == ContractReportPaymentStatus.Actual;
            }

            if (programmeIds.Contains(programmeId.Value) || (contractId.HasValue && this.contractsRepository.IsUserAssociatedWithContract(contractId.Value, userId)))
            {
                return true;
            }

            return false;
        }

        public string GetContractReportCorrectionElementNumber(Domain.Contracts.ContractReportCorrection contractReportCorrection)
        {
            string elementNumber = null;

            switch (contractReportCorrection.Type)
            {
                case ContractReportCorrectionType.PaymentVerified:
                case ContractReportCorrectionType.AdvanceCovered:
                    string contractRegNum = this.contractsRepository.GetRegNumber(contractReportCorrection.ContractId.Value);
                    elementNumber = $"{contractRegNum} ({this.contractReportPaymentsRepository.FindWithoutIncludes(contractReportCorrection.ContractReportPaymentId.Value).VersionNum.ToString()})";
                    break;
                case ContractReportCorrectionType.ContractVerified:
                    elementNumber = this.contractsRepository.FindWithoutIncludes(contractReportCorrection.ContractId.Value).RegNumber;

                    break;
                case ContractReportCorrectionType.ProgrameVerified:
                    elementNumber = this.programmesRepository.FindWithoutIncludes(contractReportCorrection.ProgrammeId).Code;

                    break;
                case ContractReportCorrectionType.ProgramePriorityVerified:
                    elementNumber = this.programmePrioritiesRepository.FindWithoutIncludes(contractReportCorrection.ProgrammePriorityId.Value).Code;

                    break;
                case ContractReportCorrectionType.ProcedureVerified:
                    elementNumber = this.proceduresRepository.FindWithoutIncludes(contractReportCorrection.ProcedureId.Value).Code;

                    break;
            }

            return elementNumber;
        }
    }
}
