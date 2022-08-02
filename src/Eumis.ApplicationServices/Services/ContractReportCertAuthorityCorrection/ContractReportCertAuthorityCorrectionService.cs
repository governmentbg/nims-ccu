using Eumis.Common.Db;
using Eumis.Data.ContractReportCertAuthorityCorrections.Repositories;
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

namespace Eumis.ApplicationServices.Services.ContractReportCertAuthorityCorrection
{
    public class ContractReportCertAuthorityCorrectionService : IContractReportCertAuthorityCorrectionService
    {
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IProceduresRepository proceduresRepository;
        private IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository;
        private IUnitOfWork unitOfWork;

        public ContractReportCertAuthorityCorrectionService(
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IProceduresRepository proceduresRepository,
            IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository,
            IUnitOfWork unitOfWork)
        {
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractReportCertAuthorityCorrectionsRepository = contractReportCertAuthorityCorrectionsRepository;
            this.unitOfWork = unitOfWork;
        }

        public Domain.Contracts.ContractReportCertAuthorityCorrection CreateContractReportCertAuthorityCorrection(
            int userId,
            ContractReportCertAuthorityCorrectionType type,
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
                case ContractReportCertAuthorityCorrectionType.PaymentCertified:
                    realContractId = this.contractReportPaymentsRepository.GetContractId(contractReportPaymentId.Value);
                    realProgrammeId = this.contractsRepository.GetProgrammeId(realContractId.Value);
                    realProcedureId = this.contractsRepository.GetProcedureId(realContractId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    realContractReportPaymentId = contractReportPaymentId.Value;
                    break;
                case ContractReportCertAuthorityCorrectionType.ContractCertified:
                    realContractId = contractId.Value;
                    realProcedureId = this.contractsRepository.GetProcedureId(realContractId.Value);
                    realProgrammeId = this.contractsRepository.GetProgrammeId(realContractId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
                case ContractReportCertAuthorityCorrectionType.ProgrameCertified:
                    realProgrammeId = programmeId.Value;
                    break;
                case ContractReportCertAuthorityCorrectionType.ProgramePriorityCertified:
                    realProgrammeId = this.programmePrioritiesRepository.GetProgrammeId(programmePriorityId.Value);
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
                case ContractReportCertAuthorityCorrectionType.ProcedureCertified:
                    realProgrammeId = this.proceduresRepository.GetPrimaryProcedureProgrammeId(procedureId.Value);
                    realProcedureId = procedureId.Value;
                    realProgrammePriorityId = programmePriorityId.Value;
                    break;
            }

            var newContractReportCertAuthorityCorrection = new Domain.Contracts.ContractReportCertAuthorityCorrection(
                type,
                sign,
                date,
                realProgrammeId.Value,
                realProgrammePriorityId,
                realProcedureId,
                realContractId,
                realContractReportPaymentId);

            this.contractReportCertAuthorityCorrectionsRepository.Add(newContractReportCertAuthorityCorrection);

            this.unitOfWork.Save();

            return newContractReportCertAuthorityCorrection;
        }

        public IList<string> CanEnterContractReportCertAuthorityCorrection(int contractReportCertAuthorityCorrectionId)
        {
            var errors = new List<string>();

            var certAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.Find(contractReportCertAuthorityCorrectionId);

            if (!certAuthorityCorrection.CertifiedEuAmount.HasValue ||
                !certAuthorityCorrection.CertifiedBgAmount.HasValue ||
                !certAuthorityCorrection.CertifiedBfpTotalAmount.HasValue ||
                !certAuthorityCorrection.CertifiedSelfAmount.HasValue ||
                !certAuthorityCorrection.CertifiedCrossAmount.HasValue ||
                !certAuthorityCorrection.CertifiedTotalAmount.HasValue)
            {
                errors.Add("Не можете да въведете коригирането, защото всички полета за суми от секцията 'Коригиране' трябва да са попълнени");
            }

            return errors;
        }

        public IList<string> CanMakeDraftContractReportCertAuthorityCorrection(int contractReportCertAuthorityCorrectionId)
        {
            var errors = new List<string>();

            if (this.contractReportCertAuthorityCorrectionsRepository.IsIncludedInCertReport(contractReportCertAuthorityCorrectionId))
            {
                errors.Add("Не можете да промените статуса на коригирането на 'Чернова', защото то е включено в доклад по сертификация");
            }

            return errors;
        }
    }
}
