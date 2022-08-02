using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportAdvanceNVPaymentAmounts.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractReportAdvanceNVPaymentAmount
{
    public class ContractReportAdvanceNVPaymentAmountService : IContractReportAdvanceNVPaymentAmountService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IContractReportAdvanceNVPaymentAmountsRepository contractReportAdvanceNVPaymentAmountsRepository;
        private IContractReportPaymentChecksRepository contractReportPaymentChecksRepository;

        public ContractReportAdvanceNVPaymentAmountService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractProcurementsRepository contractProcurementsRepository,
            IContractReportAdvanceNVPaymentAmountsRepository contractReportAdvanceNVPaymentAmountsRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.contractReportAdvanceNVPaymentAmountsRepository = contractReportAdvanceNVPaymentAmountsRepository;
            this.contractReportPaymentChecksRepository = contractReportPaymentChecksRepository;
        }

        public void CreateContractReportAdvanceNVPaymentAmounts(ContractReportPayment payment)
        {
            var programmePriorityId = this.contractsRepository.GetContractProgrammePriority(payment.ContractId);
            IList<Eumis.Domain.Contracts.ContractReportAdvanceNVPaymentAmount> newContractReportAdvanceNVPaymentAmounts = new List<Eumis.Domain.Contracts.ContractReportAdvanceNVPaymentAmount>();

            var newContractReportAdvanceNVPaymentAmount = new Eumis.Domain.Contracts.ContractReportAdvanceNVPaymentAmount(
                payment.ContractReportPaymentId,
                payment.ContractReportId,
                payment.ContractId,
                programmePriorityId);

            newContractReportAdvanceNVPaymentAmounts.Add(newContractReportAdvanceNVPaymentAmount);

            this.unitOfWork.BulkInsert<Eumis.Domain.Contracts.ContractReportAdvanceNVPaymentAmount>(newContractReportAdvanceNVPaymentAmounts);
        }

        public void DeleteContractReportAdvanceNVPaymentAmounts(ContractReportPayment payment)
        {
            this.unitOfWork.BulkDelete<Eumis.Domain.Contracts.ContractReportAdvanceNVPaymentAmount>(p => p.ContractReportPaymentId == payment.ContractReportPaymentId);
        }

        public Eumis.Domain.Contracts.ContractReportAdvanceNVPaymentAmount UpdateContractReportAdvanceNVPaymentAmount(
            int contractReportAdvanceNVPaymentAmountId,
            byte[] version,
            ContractReportAdvanceNVPaymentAmountApproval? approval,
            string notes,
            decimal? euAmount,
            decimal? bgAmount,
            decimal? bfpTotalAmount)
        {
            var contractReportAdvanceNVPaymentAmount = this.contractReportAdvanceNVPaymentAmountsRepository.FindForUpdate(contractReportAdvanceNVPaymentAmountId, version);

            var contractReport = this.contractReportsRepository.Find(contractReportAdvanceNVPaymentAmount.ContractReportId);

            if (contractReport.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit a ContractReportAdvanceNVPaymentAmount when the ContractReport is in status other than 'Unchecked'");
            }

            this.AssertIsDraftContractReportAdvanceNVPaymentAmount(contractReportAdvanceNVPaymentAmount.Status);

            contractReportAdvanceNVPaymentAmount.UpdateAttributes(
                approval,
                notes,
                euAmount,
                bgAmount,
                bfpTotalAmount);

            this.unitOfWork.Save();

            return contractReportAdvanceNVPaymentAmount;
        }

        public void ChangeContractReportAdvanceNVPaymentAmountStatus(
            int contractReportAdvanceNVPaymentAmountId,
            byte[] version,
            ContractReportAdvanceNVPaymentAmountStatus status)
        {
            var contractReportAdvanceNVPaymentAmount = this.contractReportAdvanceNVPaymentAmountsRepository.FindForUpdate(contractReportAdvanceNVPaymentAmountId, version);

            var contractReport = this.contractReportsRepository.Find(contractReportAdvanceNVPaymentAmount.ContractReportId);

            if (contractReport.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit a ContractReportAdvanceNVPaymentAmount when the ContractReport is in status other than 'Unchecked'");
            }

            contractReportAdvanceNVPaymentAmount.Status = status;
            contractReportAdvanceNVPaymentAmount.ModifyDate = DateTime.Now;

            if (status == ContractReportAdvanceNVPaymentAmountStatus.Ended)
            {
                contractReportAdvanceNVPaymentAmount.CheckedByUserId = this.accessContext.UserId;
                contractReportAdvanceNVPaymentAmount.CheckedDate = DateTime.Now;
            }

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportAdvanceNVPaymentAmountStatusToEnded(int contractReportAdvanceNVPaymentAmountId)
        {
            var errors = new List<string>();

            var contractReportAdvanceNVPaymentAmount = this.contractReportAdvanceNVPaymentAmountsRepository.Find(contractReportAdvanceNVPaymentAmountId);

            if (!contractReportAdvanceNVPaymentAmount.Approval.HasValue)
            {
                errors.Add("Полето 'Одобрение' трябва да е попълнено");
            }

            if (!contractReportAdvanceNVPaymentAmount.EuAmount.HasValue)
            {
                errors.Add("Полето 'Сума БФП - ЕС' трябва да е попълнено");
            }

            if (!contractReportAdvanceNVPaymentAmount.BgAmount.HasValue)
            {
                errors.Add("Полето 'Сума БФП - НФ' трябва да е попълнено");
            }

            if (!contractReportAdvanceNVPaymentAmount.BfpTotalAmount.HasValue)
            {
                errors.Add("Полето 'Общо БФП' трябва да е попълнено");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportAdvanceNVPaymentAmountStatusToDraft(int contractReportAdvanceNVPaymentAmountId)
        {
            var errors = new List<string>();

            var contractReportAdvanceNVPaymentAmount = this.contractReportAdvanceNVPaymentAmountsRepository.Find(contractReportAdvanceNVPaymentAmountId);

            if (this.contractReportPaymentChecksRepository.FindAll(contractReportAdvanceNVPaymentAmount.ContractReportId).Where(t => t.Status != ContractReportPaymentCheckStatus.Archived).Any())
            {
                errors.Add("Всички верифицирани ИП трябва да бъдат със статус 'Архивирано', за да можете да промените статуса на АП на 'Чернова'");
            }

            return errors;
        }

        private void AssertIsDraftContractReportAdvanceNVPaymentAmount(ContractReportAdvanceNVPaymentAmountStatus status)
        {
            if (status != ContractReportAdvanceNVPaymentAmountStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportAdvanceNVPaymentAmount when not in 'Draft' status");
            }
        }
    }
}
