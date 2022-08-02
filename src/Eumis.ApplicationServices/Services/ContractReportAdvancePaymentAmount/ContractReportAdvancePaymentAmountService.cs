using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportAdvancePaymentAmounts.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractReportAdvancePaymentAmount
{
    public class ContractReportAdvancePaymentAmountService : IContractReportAdvancePaymentAmountService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IContractReportAdvancePaymentAmountsRepository contractReportAdvancePaymentAmountsRepository;
        private IContractReportPaymentChecksRepository contractReportPaymentChecksRepository;

        public ContractReportAdvancePaymentAmountService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractProcurementsRepository contractProcurementsRepository,
            IContractReportAdvancePaymentAmountsRepository contractReportAdvancePaymentAmountsRepository,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.contractReportAdvancePaymentAmountsRepository = contractReportAdvancePaymentAmountsRepository;
            this.contractReportPaymentChecksRepository = contractReportPaymentChecksRepository;
        }

        public void CreateContractReportAdvancePaymentAmounts(ContractReportPayment payment)
        {
            IList<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount> newContractReportAdvancePaymentAmounts = new List<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>();
            var programmePriorityId = this.contractsRepository.GetContractProgrammePriority(payment.ContractId);

            var newContractReportAdvancePaymentAmount = new Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount(
                payment.ContractReportPaymentId,
                payment.ContractReportId,
                payment.ContractId,
                programmePriorityId);

            newContractReportAdvancePaymentAmounts.Add(newContractReportAdvancePaymentAmount);

            this.unitOfWork.BulkInsert<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(newContractReportAdvancePaymentAmounts);
        }

        public void DeleteContractReportAdvancePaymentAmounts(ContractReportPayment payment)
        {
            this.unitOfWork.BulkDelete<Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount>(p => p.ContractReportPaymentId == payment.ContractReportPaymentId);
        }

        public Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount UpdateContractReportAdvancePaymentAmount(
            int contractReportAdvancePaymentAmountId,
            byte[] version,
            ContractReportAdvancePaymentAmountApproval? approval,
            string notes,
            decimal? approvedEuAmount,
            decimal? approvedBgAmount,
            decimal? approvedBfpTotalAmount)
        {
            var contractReportAdvancePaymentAmount = this.contractReportAdvancePaymentAmountsRepository.FindForUpdate(contractReportAdvancePaymentAmountId, version);

            var contractReport = this.contractReportsRepository.Find(contractReportAdvancePaymentAmount.ContractReportId);

            if (contractReport.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit a ContractReportAdvancePaymentAmount when the ContractReport is in status other than 'Unchecked'");
            }

            this.AssertIsDraftContractReportAdvancePaymentAmount(contractReportAdvancePaymentAmount.Status);

            contractReportAdvancePaymentAmount.UpdateAttributes(
                approval,
                notes,
                approvedEuAmount,
                approvedBgAmount,
                approvedBfpTotalAmount);

            this.unitOfWork.Save();

            return contractReportAdvancePaymentAmount;
        }

        public void ChangeContractReportAdvancePaymentAmountStatus(
            int contractReportAdvancePaymentAmountId,
            byte[] version,
            ContractReportAdvancePaymentAmountStatus status)
        {
            var contractReportAdvancePaymentAmount = this.contractReportAdvancePaymentAmountsRepository.FindForUpdate(contractReportAdvancePaymentAmountId, version);

            var contractReport = this.contractReportsRepository.Find(contractReportAdvancePaymentAmount.ContractReportId);

            if (contractReport.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit a ContractReportAdvancePaymentAmount when the ContractReport is in status other than 'Unchecked'");
            }

            contractReportAdvancePaymentAmount.Status = status;
            contractReportAdvancePaymentAmount.ModifyDate = DateTime.Now;

            if (status == ContractReportAdvancePaymentAmountStatus.Ended)
            {
                contractReportAdvancePaymentAmount.CheckedByUserId = this.accessContext.UserId;
                contractReportAdvancePaymentAmount.CheckedDate = DateTime.Now;
            }

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportAdvancePaymentAmountStatusToEnded(int contractReportAdvancePaymentAmountId)
        {
            var errors = new List<string>();

            var contractReportAdvancePaymentAmount = this.contractReportAdvancePaymentAmountsRepository.Find(contractReportAdvancePaymentAmountId);

            if (!contractReportAdvancePaymentAmount.Approval.HasValue)
            {
                errors.Add("Полето 'Одобрение' трябва да е попълнено");
            }

            if (!contractReportAdvancePaymentAmount.ApprovedEuAmount.HasValue)
            {
                errors.Add("Полето 'Верифицирана сума БФП - ЕС' трябва да е попълнено");
            }

            if (!contractReportAdvancePaymentAmount.ApprovedBgAmount.HasValue)
            {
                errors.Add("Полето 'Верифицирана сума БФП - НФ' трябва да е попълнено");
            }

            if (!contractReportAdvancePaymentAmount.ApprovedBfpTotalAmount.HasValue)
            {
                errors.Add("Полето 'Верифицирано общо БФП' трябва да е попълнено");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportAdvancePaymentAmountStatusToDraft(int contractReportAdvancePaymentAmountId)
        {
            var errors = new List<string>();

            var contractReportAdvancePaymentAmount = this.contractReportAdvancePaymentAmountsRepository.Find(contractReportAdvancePaymentAmountId);

            if (this.contractReportPaymentChecksRepository.FindAll(contractReportAdvancePaymentAmount.ContractReportId).Where(t => t.Status != ContractReportPaymentCheckStatus.Archived).Any())
            {
                errors.Add("Всички верифицирани ИП трябва да бъдат със статус 'Архивирано', за да можете да промените статуса на верифицираното АП на 'Чернова'");
            }

            return errors;
        }

        private void AssertIsDraftContractReportAdvancePaymentAmount(ContractReportAdvancePaymentAmountStatus status)
        {
            if (status != ContractReportAdvancePaymentAmountStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportAdvancePaymentAmount when not in 'Draft' status");
            }
        }
    }
}
