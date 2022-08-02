using Eumis.Common.Db;
using Eumis.Data.ContractReportRevalidationCertAuthorityCorrections.Repositories;
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

namespace Eumis.ApplicationServices.Services.ContractReportRevalidationCertAuthorityCorrection
{
    public class ContractReportRevalidationCertAuthorityCorrectionService : IContractReportRevalidationCertAuthorityCorrectionService
    {
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportRevalidationCertAuthorityCorrectionsRepository contractReportRevalidationCertAuthorityCorrectionsRepository;
        private IUnitOfWork unitOfWork;

        public ContractReportRevalidationCertAuthorityCorrectionService(
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IProceduresRepository proceduresRepository,
            IContractReportRevalidationCertAuthorityCorrectionsRepository contractReportRevalidationCertAuthorityCorrectionsRepository,
            IUnitOfWork unitOfWork)
        {
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportRevalidationCertAuthorityCorrectionsRepository = contractReportRevalidationCertAuthorityCorrectionsRepository;
            this.unitOfWork = unitOfWork;
        }

        public Domain.Contracts.ContractReportRevalidationCertAuthorityCorrection CreateContractReportRevalidationCertAuthorityCorrection(
            int userId,
            ContractReportRevalidationCertAuthorityCorrectionType type,
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
                case ContractReportRevalidationCertAuthorityCorrectionType.PaymentRevalidated:
                    realContractId = this.contractReportPaymentsRepository.GetContractId(contractReportPaymentId.Value);
                    realProgrammeId = this.contractsRepository.GetProgrammeId(realContractId.Value);
                    realProcedureId = this.contractsRepository.GetProcedureId(realContractId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    realContractReportPaymentId = contractReportPaymentId.Value;
                    break;

                case ContractReportRevalidationCertAuthorityCorrectionType.ContractRevalidated:
                    realContractId = contractId.Value;
                    realProcedureId = this.contractsRepository.GetProcedureId(realContractId.Value);
                    realProgrammeId = this.contractsRepository.GetProgrammeId(realContractId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;

                case ContractReportRevalidationCertAuthorityCorrectionType.ProgrammeRevalidated:
                    realProgrammeId = programmeId.Value;
                    break;

                case ContractReportRevalidationCertAuthorityCorrectionType.ProgrammePriorityRevalidated:
                    realProgrammeId = this.programmePrioritiesRepository.GetProgrammeId(programmePriorityId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;

                case ContractReportRevalidationCertAuthorityCorrectionType.ProcedureRevalidated:
                    realProgrammeId = this.proceduresRepository.GetPrimaryProcedureProgrammeId(procedureId.Value);
                    realProcedureId = procedureId.Value;
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
            }

            var newContractReportRevalidationCertAuthorityCorrection = new Domain.Contracts.ContractReportRevalidationCertAuthorityCorrection(
                type,
                sign,
                date,
                realProgrammeId.Value,
                realProgrammePriorityId,
                realProcedureId,
                realContractId,
                realContractReportPaymentId);

            this.contractReportRevalidationCertAuthorityCorrectionsRepository.Add(newContractReportRevalidationCertAuthorityCorrection);

            this.unitOfWork.Save();

            return newContractReportRevalidationCertAuthorityCorrection;
        }

        public IList<string> CanEnterContractReportRevalidationCertAuthorityCorrection(int contractReportRevalidationCertAuthorityCorrectionId)
        {
            var errors = new List<string>();

            var certAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.Find(contractReportRevalidationCertAuthorityCorrectionId);

            if (!certAuthorityCorrection.CertifiedRevalidatedEuAmount.HasValue ||
                !certAuthorityCorrection.CertifiedRevalidatedBgAmount.HasValue ||
                !certAuthorityCorrection.CertifiedRevalidatedBfpTotalAmount.HasValue ||
                !certAuthorityCorrection.CertifiedRevalidatedSelfAmount.HasValue ||
                !certAuthorityCorrection.CertifiedRevalidatedCrossAmount.HasValue ||
                !certAuthorityCorrection.CertifiedRevalidatedTotalAmount.HasValue)
            {
                errors.Add("Не можете да въведете коригирането, защото всички полета за суми от секцията 'Коригиране' трябва да са попълнени");
            }

            return errors;
        }

        public IList<string> CanMakeDraftContractReportRevalidationCertAuthorityCorrection(int contractReportRevalidationCertAuthorityCorrectionId)
        {
            var errors = new List<string>();

            if (this.contractReportRevalidationCertAuthorityCorrectionsRepository.IsIncludedInCertReport(contractReportRevalidationCertAuthorityCorrectionId))
            {
                errors.Add("Не можете да промените статуса на коригирането на 'Чернова', защото то е включено в доклад по сертификация");
            }

            return errors;
        }
    }
}
