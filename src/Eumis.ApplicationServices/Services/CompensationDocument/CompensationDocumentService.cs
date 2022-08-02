using System;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.CompensationDocuments.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Contracts;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.ApplicationServices.Services.CompensationDocument
{
    public class CompensationDocumentService : ICompensationDocumentService
    {
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private ICompensationDocumentsRepository compensationDocumentsRepository;
        private IUnitOfWork unitOfWork;

        public CompensationDocumentService(
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            ICompensationDocumentsRepository compensationDocumentsRepository,
            IUnitOfWork unitOfWork)
        {
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.compensationDocumentsRepository = compensationDocumentsRepository;
            this.unitOfWork = unitOfWork;
        }

        public Domain.MonitoringFinancialControl.CompensationDocuments.CompensationDocument CreateCompensationDocument(
            int userId,
            CompensationDocumentType type,
            CompensationSign compensationSign,
            DateTime compensationDocDate,
            int contractId,
            int programmePriorityId,
            int? contractReportPaymentId)
        {
            var programmeId = this.contractsRepository.GetProgrammeId(contractId);

            if (!this.CanCreate(userId, programmeId, contractId, contractReportPaymentId))
            {
                throw new InvalidOperationException("Cannot create compensation document.");
            }

            var procedureId = this.contractsRepository.GetProcedureId(contractId);
            var newCompensationDocument = new Domain.MonitoringFinancialControl.CompensationDocuments.CompensationDocument(
                type,
                compensationSign,
                compensationDocDate,
                programmeId,
                procedureId,
                programmePriorityId,
                contractId,
                contractReportPaymentId);
            this.compensationDocumentsRepository.Add(newCompensationDocument);

            this.unitOfWork.Save();

            return newCompensationDocument;
        }

        private bool CanCreate(
            int userId,
            int programmeId,
            int contractId,
            int? contractReportPaymentId)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(userId, MonitoringFinancialControlPermissions.CanWriteFinancial);
            var canCreate = programmeIds.Contains(programmeId);

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
