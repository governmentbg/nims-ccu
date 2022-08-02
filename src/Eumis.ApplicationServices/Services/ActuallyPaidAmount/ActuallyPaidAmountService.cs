using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.ActuallyPaidAmounts.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Counters;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.ApplicationServices.Services.ActuallyPaidAmount
{
    public class ActuallyPaidAmountService : IActuallyPaidAmountService
    {
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IActuallyPaidAmountsRepository actuallyPaidAmountsRepository;
        private ICountersRepository countersRepository;

        public ActuallyPaidAmountService(
            IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IActuallyPaidAmountsRepository actuallyPaidAmountsRepository,
            ICountersRepository countersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.actuallyPaidAmountsRepository = actuallyPaidAmountsRepository;
            this.countersRepository = countersRepository;
        }

        public bool CanCreate(int userId, int contractId, int? contractReportPaymentId)
        {
            var contract = this.contractsRepository.Find(contractId);
            var canCreate = !this.CanCreate(contract, userId).Any();

            if (canCreate && contractReportPaymentId.HasValue)
            {
                canCreate = this.CanAssignContractReportPaymentId(contractId, contractReportPaymentId.Value);
            }

            return canCreate;
        }

        public IList<string> CanCreate(string contractRegNum, int userId)
        {
            var contract = this.contractsRepository.FindByRegNumber(contractRegNum);

            return this.CanCreate(contract, userId);
        }

        public bool CanAssignContractReportPaymentId(int contractId, int contractReportPaymentId)
        {
            var reportStatus = this.contractReportPaymentsRepository.GetContractReportStatus(contractReportPaymentId);
            var paymentStatus = this.contractReportPaymentsRepository.GetContractReportPaymentStatus(contractReportPaymentId);
            var paymentContractId = this.contractReportPaymentsRepository.GetContractId(contractReportPaymentId);

            return paymentContractId == contractId &&
                reportStatus == ContractReportStatus.Accepted &&
                paymentStatus == ContractReportPaymentStatus.Actual;
        }

        private IList<string> CanCreate(Domain.Contracts.Contract contract, int userId)
        {
            IList<string> errors = new List<string>();

            if (contract == null)
            {
                errors.Add("В системата няма въведен такъв договор.");
            }
            else
            {
                var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(userId, MonitoringFinancialControlPermissions.CanWriteFinancial);
                if (!programmeIds.Contains(contract.ProgrammeId))
                {
                    errors.Add("Нямате право за писане за съответната програма.");
                }

                if (contract.ContractStatus != ContractStatus.Entered)
                {
                    errors.Add("Договорът трябва да е в статус въведен, за да може с него да се асоциира реално изплатена сума.");
                }
            }

            return errors;
        }

        public void Delete(int actuallyPaidAmountId, byte[] version)
        {
            var paidAmount = this.actuallyPaidAmountsRepository.FindForUpdate(actuallyPaidAmountId, version);

            if (paidAmount.IsActivated)
            {
                throw new DomainValidationException("Activated ActuallyPaidAmounts cannot be deleted. Set the status to Deleted instead.");
            }

            this.actuallyPaidAmountsRepository.Remove(paidAmount);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeStatusToEntered(Domain.MonitoringFinancialControl.ActuallyPaidAmount paidAmount)
        {
            var errors = new List<string>();

            if (paidAmount.PaymentDate == null)
            {
                errors.Add("Датата на плащане е задължителна за попълване");
            }

            return errors;
        }

        public void ChangeStatusToEntered(Domain.MonitoringFinancialControl.ActuallyPaidAmount paidAmount)
        {
            var errors = this.CanChangeStatusToEntered(paidAmount);
            if (errors.Count > 0)
            {
                throw new DomainValidationException("Cannot set ActuallyPaidAmounts status to entered.");
            }

            if (paidAmount.IsActivated)
            {
                paidAmount.ChangeStatusToEntered();
            }
            else
            {
                this.countersRepository.CreateActuallyPaidAmountCounter(paidAmount.ContractId);
                var regNum = this.countersRepository.GetNextActuallyPaidAmountNumber(paidAmount.ContractId);

                paidAmount.ChangeStatusToEntered(regNum);
            }

            this.unitOfWork.Save();
        }
    }
}
