using Eumis.Common.Db;
using Eumis.Data.ContractReportCertCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractReportCertCorrection
{
    public class ContractReportCertCorrectionService : IContractReportCertCorrectionService
    {
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository;
        private IUnitOfWork unitOfWork;

        public ContractReportCertCorrectionService(
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IProceduresRepository proceduresRepository,
            IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository,
            IUnitOfWork unitOfWork)
        {
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportCertCorrectionsRepository = contractReportCertCorrectionsRepository;
            this.unitOfWork = unitOfWork;
        }

        public Domain.Contracts.ContractReportCertCorrection CreateContractReportCertCorrection(
            int userId,
            ContractReportCertCorrectionType type,
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
                case ContractReportCertCorrectionType.PaymentCertified:
                    realContractId = this.contractReportPaymentsRepository.GetContractId(contractReportPaymentId.Value);
                    realProgrammeId = this.contractsRepository.GetProgrammeId(realContractId.Value);
                    realProcedureId = this.contractsRepository.GetProcedureId(realContractId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    realContractReportPaymentId = contractReportPaymentId.Value;
                    break;
                case ContractReportCertCorrectionType.ContractCertified:
                    realContractId = contractId.Value;
                    realProcedureId = this.contractsRepository.GetProcedureId(realContractId.Value);
                    realProgrammeId = this.contractsRepository.GetProgrammeId(realContractId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
                case ContractReportCertCorrectionType.ProgrameCertified:
                    realProgrammeId = programmeId.Value;
                    break;
                case ContractReportCertCorrectionType.ProgramePriorityCertified:
                    realProgrammeId = this.programmePrioritiesRepository.GetProgrammeId(programmePriorityId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
                case ContractReportCertCorrectionType.ProcedureCertified:
                    realProgrammeId = this.proceduresRepository.GetPrimaryProcedureProgrammeId(procedureId.Value);
                    realProcedureId = procedureId.Value;
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
            }

            if (!this.CanCreate(userId, realProgrammeId, realContractId, realContractReportPaymentId))
            {
                throw new InvalidOperationException("Cannot create ContractReportCertCorrection");
            }

            var newContractReportCertCorrection = new Domain.Contracts.ContractReportCertCorrection(
                type,
                sign,
                date,
                realProgrammeId.Value,
                realProgrammePriorityId,
                realProcedureId,
                realContractId,
                realContractReportPaymentId);

            this.contractReportCertCorrectionsRepository.Add(newContractReportCertCorrection);

            this.unitOfWork.Save();

            return newContractReportCertCorrection;
        }

        public IList<string> CanEnterContractReportCertCorrection(int contractReportCertCorrectionId)
        {
            var errors = new List<string>();

            var certCorrection = this.contractReportCertCorrectionsRepository.Find(contractReportCertCorrectionId);

            if (!certCorrection.CertifiedEuAmount.HasValue ||
                !certCorrection.CertifiedBgAmount.HasValue ||
                !certCorrection.CertifiedBfpTotalAmount.HasValue ||
                !certCorrection.CertifiedSelfAmount.HasValue ||
                !certCorrection.CertifiedCrossAmount.HasValue ||
                !certCorrection.CertifiedTotalAmount.HasValue)
            {
                errors.Add("Не можете да въведете изравняването, защото всички полета за суми от секцията 'Изравняване' трябва да са попълнени");
            }

            return errors;
        }

        public IList<string> CanMakeDraftContractReportCertCorrection(int contractReportCertCorrectionId)
        {
            var errors = new List<string>();

            if (this.contractReportCertCorrectionsRepository.IsIncludedInCertReport(contractReportCertCorrectionId))
            {
                errors.Add("Не можете да промените статуса на изравняването на 'Чернова', защото то е включено в доклад по сертификация");
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
            var canCreate = programmeIds.Contains(programmeId.Value);

            if (canCreate && contractReportPaymentId.HasValue)
            {
                var reportStatus = this.contractReportPaymentsRepository.GetContractReportStatus(contractReportPaymentId.Value);
                var paymentStatus = this.contractReportPaymentsRepository.GetContractReportPaymentStatus(contractReportPaymentId.Value);
                var paymentContractId = this.contractReportPaymentsRepository.GetContractId(contractReportPaymentId.Value);

                canCreate = paymentContractId == contractId &&
                    reportStatus == ContractReportStatus.Accepted &&
                    paymentStatus == ContractReportPaymentStatus.Actual;
            }

            return canCreate;
        }
    }
}
