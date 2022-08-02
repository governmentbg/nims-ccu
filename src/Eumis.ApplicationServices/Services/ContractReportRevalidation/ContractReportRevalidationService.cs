using Eumis.Common.Db;
using Eumis.Data.ContractReportRevalidations.Repositories;
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

namespace Eumis.ApplicationServices.Services.ContractReportRevalidation
{
    public class ContractReportRevalidationService : IContractReportRevalidationService
    {
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportRevalidationsRepository contractReportRevalidationsRepository;
        private IUnitOfWork unitOfWork;

        public ContractReportRevalidationService(
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IProceduresRepository proceduresRepository,
            IContractReportRevalidationsRepository contractReportRevalidationsRepository,
            IUnitOfWork unitOfWork)
        {
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportRevalidationsRepository = contractReportRevalidationsRepository;
            this.unitOfWork = unitOfWork;
        }

        public Domain.Contracts.ContractReportRevalidation CreateContractReportRevalidation(
            int userId,
            ContractReportRevalidationType type,
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
                case ContractReportRevalidationType.PaymentRevalidated:
                    realContractId = this.contractReportPaymentsRepository.GetContractId(contractReportPaymentId.Value);
                    realProgrammeId = this.contractsRepository.GetProgrammeId(realContractId.Value);
                    realProcedureId = this.contractsRepository.GetProcedureId(realContractId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    realContractReportPaymentId = contractReportPaymentId.Value;
                    break;
                case ContractReportRevalidationType.ContractRevalidated:
                    realContractId = contractId.Value;
                    realProcedureId = this.contractsRepository.GetProcedureId(realContractId.Value);
                    realProgrammeId = this.contractsRepository.GetProgrammeId(realContractId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
                case ContractReportRevalidationType.ProgrameRevalidated:
                    realProgrammeId = programmeId.Value;
                    break;
                case ContractReportRevalidationType.ProgramePriorityRevalidated:
                    realProgrammeId = this.programmePrioritiesRepository.GetProgrammeId(programmePriorityId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
                case ContractReportRevalidationType.ProcedureRevalidated:
                    realProgrammeId = this.proceduresRepository.GetPrimaryProcedureProgrammeId(procedureId.Value);
                    realProcedureId = procedureId.Value;
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
            }

            if (!this.CanCreate(userId, realProgrammeId, realContractId, realContractReportPaymentId))
            {
                throw new InvalidOperationException("Cannot create ContractReportRevalidation");
            }

            var newContractReportRevalidation = new Domain.Contracts.ContractReportRevalidation(
                type,
                date,
                realProgrammeId.Value,
                realProgrammePriorityId,
                realProcedureId,
                realContractId,
                realContractReportPaymentId);

            this.contractReportRevalidationsRepository.Add(newContractReportRevalidation);

            this.unitOfWork.Save();

            return newContractReportRevalidation;
        }

        public IList<string> CanEnterContractReportRevalidation(int contractReportRevalidationId)
        {
            var errors = new List<string>();

            var revalidation = this.contractReportRevalidationsRepository.Find(contractReportRevalidationId);

            if (!revalidation.RevalidatedEuAmount.HasValue ||
                !revalidation.RevalidatedBgAmount.HasValue ||
                !revalidation.RevalidatedBfpTotalAmount.HasValue ||
                !revalidation.RevalidatedSelfAmount.HasValue ||
                !revalidation.RevalidatedCrossAmount.HasValue ||
                !revalidation.RevalidatedTotalAmount.HasValue)
            {
                errors.Add("Не можете да въведете препотвърждаването, защото всички полета за суми от секцията 'Препотвърждаване' трябва да са попълнени");
            }

            return errors;
        }

        public IList<string> CanMakeDraftContractReportRevalidation(int contractReportRevalidationId)
        {
            var errors = new List<string>();

            if (this.contractReportRevalidationsRepository.IsIncludedInCertReport(contractReportRevalidationId))
            {
                errors.Add("Не можете да промените статуса на препотвърждаването на 'Чернова', защото то е включено в доклад по сертификация");
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
